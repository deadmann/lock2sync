using System;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The Setting window: Skin and Security "tabs" (two check buttons), Ok and Cancel. The layout is in
/// SettingForm.Designer.cs (from the 2008 KryptonSetting designer). Choosing a theme previews it at once; Cancel (or
/// closing the window) undoes the preview.
/// </summary>
public sealed partial class SettingForm : KryptonForm
{
    private readonly AppSettings _settings;
    private readonly SettingsStore _store;
    private readonly SkinImageStore _skinStore;

    // what the user did with the picture, applied only when Ok is pressed
    private string? _pendingImagePath;
    private bool _imageRemoved;
    private bool _applied;

    /// <summary>Raised after Ok saved the settings, so the main window can show the new theme and skin.</summary>
    public event EventHandler? Applied;

    /// <summary>For the designer: works on a copy of the default settings and never saves anything.</summary>
    public SettingForm() : this(new AppSettings(), new SettingsStore(SettingsStore.DefaultFilePath), new SkinImageStore(SkinImageStore.DefaultFolderPath))
    {
    }

    public SettingForm(AppSettings settings, SettingsStore store, SkinImageStore skinStore)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _skinStore = skinStore ?? throw new ArgumentNullException(nameof(skinStore));

        InitializeComponent();
    }

    // ---------------------------------------------------------------- showing the saved settings

    private void SettingForm_Load(object? sender, EventArgs e)
    {
        chkTransparent.Checked = _settings.TransparentInfo;
        rdoPic1.Checked = _settings.SkinImageMode == SkinImageMode.Tile;
        rdoPic2.Checked = _settings.SkinImageMode == SkinImageMode.Center;
        rdoPic3.Checked = _settings.SkinImageMode == SkinImageMode.Stretch;
        rdoPic4.Checked = _settings.SkinImageMode == SkinImageMode.Zoom;
        ShowPreview(_skinStore.GetPath(_settings.SkinImageFile));
        CBoxVirtualTheme.SelectedIndex = (int)_settings.Theme;

        chkSecQuest.Checked = _settings.SecurityQuestion;
        chkKeepAdd.Checked = _settings.KeepLastAddress;
        chkPass.Checked = _settings.LoginEnabled;
        panel1.Enabled = _settings.LoginEnabled;
        txtSetUser.Text = _settings.UserName;
        txtSetPass.Text = string.Empty; // the password is only stored as a hash, so it cannot be shown
    }

    // ---------------------------------------------------------------- tabs

    private void btnSkin_Click(object? sender, EventArgs e) => ShowTab(true);

    private void btnSecurity_Click(object? sender, EventArgs e) => ShowTab(false);

    private void ShowTab(bool skin)
    {
        btnSkin.Checked = skin;
        btnSecurity.Checked = !skin;
        (skin ? btnSkin : btnSecurity).BringToFront();
        (skin ? btnSecurity : btnSkin).SendToBack();
        kryptonGroup1.Visible = skin;
        kryptonGroup2.Visible = !skin;
    }

    // ---------------------------------------------------------------- theme

    private void CBoxVirtualTheme_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (CBoxVirtualTheme.SelectedIndex >= 0) kryptonManager.GlobalPaletteMode = ThemeMapper.ToPaletteMode((AppTheme)CBoxVirtualTheme.SelectedIndex);
    }

    private void SettingForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        // Cancel, or closing the window any other way, puts the saved theme back (the 2008 window only did it for Cancel).
        if (!_applied) kryptonManager.GlobalPaletteMode = ThemeMapper.ToPaletteMode(_settings.Theme);
        ClearPreview();
    }

    private void chkPass_CheckedChanged(object? sender, EventArgs e) => panel1.Enabled = chkPass.Checked;

    // ---------------------------------------------------------------- the picture

    private void btnBrowse_Click(object? sender, EventArgs e)
    {
        openFileDialog1.Title = "Choose picture as background";
        openFileDialog1.Filter = "Pictures (*.jpg;*.jpeg;*.jpe;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.jpe;*.png;*.bmp;*.gif|All files (*.*)|*.*";
        openFileDialog1.FilterIndex = 1;
        openFileDialog1.CheckFileExists = true;
        openFileDialog1.RestoreDirectory = true; // keeps the program's current folder from moving

        if (openFileDialog1.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            ShowPreview(openFileDialog1.FileName, loadNow: true);
            _pendingImagePath = openFileDialog1.FileName;
            _imageRemoved = false;
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException || ex is ArgumentException)
        {
            MessageBox.Show(this, "Can't load image", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnUnloadImage_Click(object? sender, EventArgs e)
    {
        _pendingImagePath = null;
        _imageRemoved = true;
        ClearPreview();
    }

    private void btnRestor_Click(object? sender, EventArgs e)
    {
        _pendingImagePath = null;
        _imageRemoved = false;
        ShowPreview(_skinStore.GetPath(_settings.SkinImageFile));
    }

    private void ShowPreview(string? path, bool loadNow = false)
    {
        if (path == null)
        {
            ClearPreview();
            return;
        }

        try
        {
            var image = SkinImageLoader.Load(path);
            ClearPreview();
            pictureBox1.Image = image;
        }
        catch (Exception ex) when (!loadNow && (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException || ex is ArgumentException))
        {
            ClearPreview(); // a saved picture that cannot be read just shows nothing
        }
    }

    private void ClearPreview()
    {
        var old = pictureBox1.Image;
        pictureBox1.Image = null;
        old?.Dispose();
    }

    // ---------------------------------------------------------------- Ok and Cancel

    private void btnCancel_Click(object? sender, EventArgs e) => Close();

    private void btnOk_Click(object? sender, EventArgs e)
    {
        _settings.TransparentInfo = chkTransparent.Checked;
        _settings.SkinImageMode = rdoPic2.Checked ? SkinImageMode.Center
            : rdoPic3.Checked ? SkinImageMode.Stretch
            : rdoPic4.Checked ? SkinImageMode.Zoom
            : SkinImageMode.Tile;
        ApplyPicture();
        _settings.Theme = (AppTheme)Math.Max(0, CBoxVirtualTheme.SelectedIndex);
        ApplyLogin();
        _settings.SecurityQuestion = chkSecQuest.Checked;
        _settings.KeepLastAddress = chkKeepAdd.Checked; // the 2008 version saved this from the Security Question box by mistake

        try
        {
            _store.Save(_settings);
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            MessageBox.Show(this, "Can't save the settings: " + ex.Message, "Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        _applied = true;
        Applied?.Invoke(this, EventArgs.Empty);
        Close();
    }

    private void ApplyPicture()
    {
        try
        {
            if (_pendingImagePath != null)
            {
                _settings.SkinImageFile = _skinStore.Import(_pendingImagePath);
            }
            else if (_imageRemoved)
            {
                _skinStore.Remove(_settings.SkinImageFile);
                _settings.SkinImageFile = string.Empty;
            }
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            MessageBox.Show(this, "Can't save the image: " + ex.Message, "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyLogin()
    {
        if (!chkPass.Checked)
        {
            _settings.ClearLogin();
            return;
        }

        var user = txtSetUser.Text;
        var password = txtSetPass.Text;

        // Already active and the password box left empty: keep the stored password (it cannot be shown again).
        if (_settings.LoginEnabled && password.Length == 0 && user.Length > 0)
        {
            _settings.UserName = user;
            return;
        }

        if (user.Length == 0 || password.Length == 0)
        {
            MessageBox.Show(this, "You didn't set any username or password! Program is unable to accept none info!",
                "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string confirmation;
        using (var input = new InputForm())
        {
            input.ShowDialog(this);
            confirmation = input.Confirmation;
        }

        if (string.Equals(confirmation, password, StringComparison.Ordinal))
            _settings.SetLogin(user, password);
        else
            MessageBox.Show(this, "Error in password verify ,check it again", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

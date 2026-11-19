using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The Setting window, laid out like the 2008 KryptonSetting form: Skin and Security "tabs" (two check buttons),
/// Ok and Cancel at the bottom. Choosing a theme previews it at once; Cancel (or closing the window) undoes the preview.
/// </summary>
public sealed class SettingForm : ChromeForm
{
    private readonly AppSettings _settings;
    private readonly SettingsStore _store;
    private readonly SkinImageStore _skinStore;
    private readonly KryptonManager _manager;

    // tabs and buttons
    private readonly KryptonCheckButton _skinTab = new KryptonCheckButton();
    private readonly KryptonCheckButton _securityTab = new KryptonCheckButton();
    private readonly KryptonGroup _skinGroup = new KryptonGroup();
    private readonly KryptonGroup _securityGroup = new KryptonGroup();
    private readonly KryptonButton _okButton = new KryptonButton();
    private readonly KryptonButton _cancelButton = new KryptonButton();

    // skin tab
    private readonly KryptonCheckBox _transparentBox = new KryptonCheckBox();
    private readonly PictureBox _preview = new PictureBox();
    private readonly KryptonButton _browseButton = new KryptonButton();
    private readonly KryptonButton _unloadButton = new KryptonButton();
    private readonly KryptonButton _restoreButton = new KryptonButton();
    private readonly KryptonRadioButton _tileRadio = new KryptonRadioButton();
    private readonly KryptonRadioButton _centerRadio = new KryptonRadioButton();
    private readonly KryptonRadioButton _stretchRadio = new KryptonRadioButton();
    private readonly KryptonRadioButton _zoomRadio = new KryptonRadioButton();
    private readonly KryptonComboBox _themeBox = new KryptonComboBox();

    // security tab
    private readonly KryptonCheckBox _questionBox = new KryptonCheckBox();
    private readonly KryptonCheckBox _keepAddressBox = new KryptonCheckBox();
    private readonly KryptonCheckBox _passwordBox = new KryptonCheckBox();
    private readonly KryptonPanel _loginPanel = new KryptonPanel();
    private readonly KryptonTextBox _userBox = new KryptonTextBox();
    private readonly KryptonTextBox _passBox = new KryptonTextBox();

    // what the user did with the picture, applied only when Ok is pressed
    private string? _pendingImagePath;
    private bool _imageRemoved;
    private bool _applied;

    /// <summary>Raised after Ok saved the settings, so the main window can show the new theme and skin.</summary>
    public event EventHandler? Applied;

    public SettingForm(AppSettings settings, SettingsStore store, SkinImageStore skinStore, KryptonManager manager)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _skinStore = skinStore ?? throw new ArgumentNullException(nameof(skinStore));
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));

        BuildUi();
        LoadValues();
    }

    // ---------------------------------------------------------------- layout (same as the original designer)

    private void BuildUi()
    {
        SuspendLayout();
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Setting";

        _skinTab.ButtonStyle = ButtonStyle.Alternate;
        _skinTab.Checked = true;
        _skinTab.Text = "Skin";
        _skinTab.Location = new Point(12, 12);
        _skinTab.Size = new Size(45, 25);
        _skinTab.TabIndex = 3;
        _securityTab.ButtonStyle = ButtonStyle.Alternate;
        _securityTab.Text = "Security";
        _securityTab.Location = new Point(53, 12);
        _securityTab.Size = new Size(64, 25);
        _securityTab.TabIndex = 4;

        _okButton.Text = "&Ok";
        _okButton.Location = new Point(12, 282);
        _okButton.Size = new Size(90, 25);
        _okButton.TabIndex = 1;
        _cancelButton.Text = "&Cancel";
        _cancelButton.Location = new Point(131, 282);
        _cancelButton.Size = new Size(90, 25);
        _cancelButton.TabIndex = 2;

        // ---- skin group
        _transparentBox.Text = "Transparent for Info Section";
        _transparentBox.Location = new Point(4, 4);
        _transparentBox.Size = new Size(162, 19);
        _transparentBox.TabIndex = 23;

        var backgroundLabel = new KryptonLabel { Text = "Background Image", Location = new Point(4, 29), Size = new Size(104, 19), TabIndex = 24 };

        _preview.BackColor = SystemColors.ControlLightLight;
        _preview.Location = new Point(4, 54);
        _preview.Size = new Size(100, 94);
        _preview.SizeMode = PictureBoxSizeMode.Zoom;
        _preview.TabStop = false;

        _browseButton.Text = "&Browse";
        _browseButton.Location = new Point(110, 54);
        _browseButton.Size = new Size(90, 25);
        _browseButton.TabIndex = 26;
        _unloadButton.Text = "&Unload";
        _unloadButton.Location = new Point(110, 88);
        _unloadButton.Size = new Size(90, 25);
        _unloadButton.TabIndex = 27;
        _restoreButton.Text = "&Restor"; // spelled like the original
        _restoreButton.Location = new Point(110, 123);
        _restoreButton.Size = new Size(90, 25);
        _restoreButton.TabIndex = 28;

        _tileRadio.Text = "Tile";
        _tileRadio.Location = new Point(4, 154);
        _tileRadio.Size = new Size(40, 19);
        _tileRadio.TabIndex = 29;
        _centerRadio.Text = "Center";
        _centerRadio.Location = new Point(105, 154);
        _centerRadio.Size = new Size(55, 19);
        _centerRadio.TabIndex = 31;
        _stretchRadio.Text = "Stretch";
        _stretchRadio.Location = new Point(4, 179);
        _stretchRadio.Size = new Size(57, 19);
        _stretchRadio.TabIndex = 30;
        _zoomRadio.Text = "Zoom";
        _zoomRadio.Location = new Point(105, 179);
        _zoomRadio.Size = new Size(52, 19);
        _zoomRadio.TabIndex = 32;

        _themeBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _themeBox.DropDownWidth = 121;
        _themeBox.Items.AddRange(AppThemeNames.All);
        _themeBox.Location = new Point(7, 205);
        _themeBox.Size = new Size(121, 22);
        _themeBox.TabIndex = 33;

        _skinGroup.Location = new Point(12, 37);
        _skinGroup.Size = new Size(206, 239);
        _skinGroup.TabIndex = 5;
        _skinGroup.Panel.Controls.AddRange(new Control[]
        {
            _themeBox, _zoomRadio, _centerRadio, _stretchRadio, _tileRadio, _restoreButton, _unloadButton,
            _browseButton, _preview, backgroundLabel, _transparentBox
        });

        // ---- security group
        _questionBox.Text = "Active Security Question";
        _questionBox.Location = new Point(3, 3);
        _questionBox.Size = new Size(145, 19);
        _questionBox.TabIndex = 4;
        _keepAddressBox.Text = "Keep Last Directory Address";
        _keepAddressBox.Location = new Point(3, 28);
        _keepAddressBox.Size = new Size(163, 19);
        _keepAddressBox.TabIndex = 5;
        _passwordBox.Text = "Active Password";
        _passwordBox.Location = new Point(3, 53);
        _passwordBox.Size = new Size(104, 19);
        _passwordBox.TabIndex = 6;

        _userBox.Location = new Point(72, 3);
        _userBox.Size = new Size(123, 22);
        _userBox.TabIndex = 2;
        _passBox.Location = new Point(72, 28);
        _passBox.Size = new Size(123, 22);
        _passBox.PasswordChar = '*';
        _passBox.TabIndex = 3;
        _loginPanel.Location = new Point(3, 78);
        _loginPanel.Size = new Size(198, 53);
        _loginPanel.TabIndex = 7;
        _loginPanel.Controls.AddRange(new Control[]
        {
            _passBox, _userBox,
            new KryptonLabel { Text = "Password", Location = new Point(3, 28), Size = new Size(57, 19), TabIndex = 1 },
            new KryptonLabel { Text = "UserName", Location = new Point(3, 3), Size = new Size(63, 19), TabIndex = 0 }
        });

        _securityGroup.Location = new Point(12, 37);
        _securityGroup.Size = new Size(206, 239);
        _securityGroup.TabIndex = 6;
        _securityGroup.Visible = false;
        _securityGroup.Panel.Controls.AddRange(new Control[] { _loginPanel, _passwordBox, _keepAddressBox, _questionBox });

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.AddRange(new Control[] { _skinTab, _skinGroup, _securityTab, _cancelButton, _okButton, _securityGroup });
        Controls.Add(panel);
        ClientSize = new Size(233, 310);

        _skinTab.Click += (sender, e) => ShowTab(true);
        _securityTab.Click += (sender, e) => ShowTab(false);
        _themeBox.SelectedIndexChanged += (sender, e) => PreviewTheme();
        _browseButton.Click += (sender, e) => BrowseForPicture();
        _unloadButton.Click += (sender, e) => UnloadPicture();
        _restoreButton.Click += (sender, e) => RestorePicture();
        _passwordBox.CheckedChanged += (sender, e) => _loginPanel.Enabled = _passwordBox.Checked;
        _okButton.Click += (sender, e) => Apply();
        _cancelButton.Click += (sender, e) => Close();

        ResumeLayout(false);
        PerformLayout();

        // The designer anchors, set after the first layout (see MainForm.ApplyDesignerAnchors).
        _skinGroup.Anchor = _securityGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        _cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _browseButton.Anchor = _unloadButton.Anchor = _restoreButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    }

    private void ShowTab(bool skin)
    {
        _skinTab.Checked = skin;
        _securityTab.Checked = !skin;
        (skin ? _skinTab : _securityTab).BringToFront();
        (skin ? _securityTab : _skinTab).SendToBack();
        _skinGroup.Visible = skin;
        _securityGroup.Visible = !skin;
    }

    // ---------------------------------------------------------------- showing the saved settings

    private void LoadValues()
    {
        _transparentBox.Checked = _settings.TransparentInfo;
        _tileRadio.Checked = _settings.SkinImageMode == SkinImageMode.Tile;
        _centerRadio.Checked = _settings.SkinImageMode == SkinImageMode.Center;
        _stretchRadio.Checked = _settings.SkinImageMode == SkinImageMode.Stretch;
        _zoomRadio.Checked = _settings.SkinImageMode == SkinImageMode.Zoom;
        ShowPreview(_skinStore.GetPath(_settings.SkinImageFile));
        _themeBox.SelectedIndex = (int)_settings.Theme;

        _questionBox.Checked = _settings.SecurityQuestion;
        _keepAddressBox.Checked = _settings.KeepLastAddress;
        _passwordBox.Checked = _settings.LoginEnabled;
        _loginPanel.Enabled = _settings.LoginEnabled;
        _userBox.Text = _settings.UserName;
        _passBox.Text = string.Empty; // the password is only stored as a hash, so it cannot be shown
    }

    // ---------------------------------------------------------------- theme

    private void PreviewTheme()
    {
        if (_themeBox.SelectedIndex >= 0) _manager.GlobalPaletteMode = ThemeMapper.ToPaletteMode((AppTheme)_themeBox.SelectedIndex);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Cancel, or closing the window any other way, puts the saved theme back (the 2008 window only did it for Cancel).
        if (!_applied) _manager.GlobalPaletteMode = ThemeMapper.ToPaletteMode(_settings.Theme);
        ClearPreview();
        base.OnFormClosed(e);
    }

    // ---------------------------------------------------------------- the picture

    private void BrowseForPicture()
    {
        using (var dialog = new OpenFileDialog())
        {
            dialog.Title = "Choose picture as background";
            dialog.Filter = "Pictures (*.jpg;*.jpeg;*.jpe;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.jpe;*.png;*.bmp;*.gif|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.CheckFileExists = true;
            dialog.RestoreDirectory = true; // keeps the program's current folder from moving

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                ShowPreview(dialog.FileName, loadNow: true);
                _pendingImagePath = dialog.FileName;
                _imageRemoved = false;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException || ex is ArgumentException)
            {
                MessageBox.Show(this, "Can't load image", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void UnloadPicture()
    {
        _pendingImagePath = null;
        _imageRemoved = true;
        ClearPreview();
    }

    private void RestorePicture()
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
            _preview.Image = image;
        }
        catch (Exception ex) when (!loadNow && (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException || ex is ArgumentException))
        {
            ClearPreview(); // a saved picture that cannot be read just shows nothing
        }
    }

    private void ClearPreview()
    {
        var old = _preview.Image;
        _preview.Image = null;
        old?.Dispose();
    }

    // ---------------------------------------------------------------- Ok

    private void Apply()
    {
        _settings.TransparentInfo = _transparentBox.Checked;
        _settings.SkinImageMode = _centerRadio.Checked ? SkinImageMode.Center
            : _stretchRadio.Checked ? SkinImageMode.Stretch
            : _zoomRadio.Checked ? SkinImageMode.Zoom
            : SkinImageMode.Tile;
        ApplyPicture();
        _settings.Theme = (AppTheme)Math.Max(0, _themeBox.SelectedIndex);
        ApplyLogin();
        _settings.SecurityQuestion = _questionBox.Checked;
        _settings.KeepLastAddress = _keepAddressBox.Checked; // the 2008 version saved this from the Security Question box by mistake

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
        if (!_passwordBox.Checked)
        {
            _settings.ClearLogin();
            return;
        }

        var user = _userBox.Text;
        var password = _passBox.Text;

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

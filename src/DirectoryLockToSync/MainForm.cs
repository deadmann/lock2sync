using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The main window. The layout (positions, sizes, anchors, tab order) is in MainForm.Designer.cs, copied from the 2008
/// KryptonDLTS designer, so it can be edited in the Visual Studio / Rider designer. This file has the behaviour only.
/// All folder logic lives in <see cref="FolderSession"/> and <see cref="AppSettings"/> (tested in Core).
/// </summary>
public sealed partial class MainForm : ChromeForm
{
    // Layout numbers of the original form, in 96-dpi pixels (used when the window grows with the address lines).
    private const int FormClientHeight = 208;
    private const int LineHeight = 13;
    private const int MaxLines = 10; // pressing "more" on line 10 starts again at 1, like the original

    private readonly SettingsStore _store;
    private readonly AppSettings _settings;
    private readonly SkinImageStore _skinStore;
    private readonly FolderSession _session = new FolderSession();
    private readonly ToolTip _toolTip = new ToolTip();

    private Image? _skinImage;       // the background picture as loaded
    private Bitmap? _skinRendered;   // the part of the picture behind the buttons
    private Bitmap? _frameRendered;  // the same picture for the whole window, so the frame around it shows it too
    private SettingForm? _settingForm;

    private int _addressLines = 1;
    private int _slot; // 0 = no path slot chosen yet

    /// <summary>Used by the designer and by a normal start (settings in %AppData%).</summary>
    public MainForm() : this(new SettingsStore(SettingsStore.DefaultFilePath))
    {
    }

    public MainForm(SettingsStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _settings = store.Load();
        _skinStore = new SkinImageStore(Path.GetDirectoryName(Path.GetFullPath(store.FilePath))!);

        InitializeComponent();
        ApplyTheme(); // the saved theme; the original default is Light-Blue (Office 2007 Blue)
    }

    // ---------------------------------------------------------------- start and close

    private void MainForm_Load(object? sender, EventArgs e)
    {
        _addressLines = 1;
        ApplyAddressLines();
        ApplySkin();
        SetStatus("Nothing Loaded");
        if (_settings.KeepLastAddress)
        {
            var last = _settings.LastAddress;
            if (string.IsNullOrEmpty(last) || last == "None") SetStatus("Ready");
            else if (_session.TryLoad(last)) SetStatus("Last Directory Loaded");
            else SetStatus("Last Folder no longer exist");
        }

        RefreshUi();
        ActiveControl = null;
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_session.State == FolderState.Unlocked && _settings.SecurityQuestion)
        {
            var answer = AskLockFirst("close program");
            if (answer == DialogResult.Cancel)
            {
                e.Cancel = true;
                SetStatus("Program close ,canceled");
                return;
            }

            if (answer == DialogResult.Yes) TryLock();
        }

        _settings.LastAddress = _session.BasePath ?? string.Empty;
        SaveSettings();
    }

    // ---------------------------------------------------------------- buttons

    private void btnAbout_Click(object? sender, EventArgs e)
    {
        using (var about = new AboutForm())
        {
            about.ShowDialog(this);
        }
    }

    private void btnSetting_Click(object? sender, EventArgs e) => OpenSettings();

    private void btnBrowse_Click(object? sender, EventArgs e)
    {
        if (_session.BasePath != null) folderBrowserDialog1.SelectedPath = _session.BasePath;
        if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK) LoadFolder(folderBrowserDialog1.SelectedPath);
    }

    private void btnLock_Click(object? sender, EventArgs e)
    {
        if (TryLock()) SetStatus("Folder Locked");
        RefreshUi();
    }

    private void btnUnlock_Click(object? sender, EventArgs e)
    {
        try
        {
            _session.Unlock();
            SetStatus("Folder Unlocked");
        }
        catch (Exception ex) when (IsFileProblem(ex))
        {
            SetStatus("Can't Unlock Folder!", ex.Message);
        }

        RefreshUi();
    }

    private void btnOpen_Click(object? sender, EventArgs e)
    {
        var path = _session.CurrentPath;
        if (path == null || _session.State != FolderState.Unlocked) return;

        try
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "\"" + path + "\"") { UseShellExecute = true });
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is InvalidOperationException)
        {
            SetStatus("Can't open the folder", ex.Message);
        }
    }

    private void btnUnload_Click(object? sender, EventArgs e)
    {
        if (_session.State == FolderState.Unlocked && _settings.SecurityQuestion)
        {
            var answer = AskLockFirst("unload data");
            if (answer == DialogResult.Cancel)
            {
                SetStatus("Unload canceled by user");
            }
            else if (answer == DialogResult.Yes)
            {
                if (TryLock())
                {
                    ResetProgram();
                    SetStatus("successfull Unload and folder locked");
                }
            }
            else
            {
                ResetProgram();
                SetStatus("successfull Unload and folder didn't lock");
            }
        }
        else
        {
            ResetProgram();
            SetStatus("successfull Unload");
        }

        RefreshUi();
    }

    private void btnMoreInfo_Click(object? sender, EventArgs e)
    {
        _addressLines++;
        ApplyAddressLines();
    }

    private void btnLessInfo_Click(object? sender, EventArgs e)
    {
        _addressLines--;
        ApplyAddressLines();
    }

    // ---------------------------------------------------------------- folder actions

    private void LoadFolder(string path)
    {
        if (_session.TryLoad(path))
            SetStatus(_session.State == FolderState.Locked ? "Locked Folder Loaded" : "Unlocked Folder Loaded");
        else
            SetStatus("path not found");

        RefreshUi();
    }

    private bool TryLock()
    {
        try
        {
            _session.Lock();
            return true;
        }
        catch (Exception ex) when (IsFileProblem(ex))
        {
            SetStatus("Can't Lock Folder!", ex.Message);
            return false;
        }
    }

    /// <summary>Forgets the loaded folder and puts the window back to its starting size.</summary>
    private void ResetProgram()
    {
        _session.Unload();
        _addressLines = 1;
        ApplyAddressLines();
    }

    private DialogResult AskLockFirst(string action) =>
        MessageBox.Show(
            this,
            "Do you wanna Lock this folder before " + action + " ?\n (" + _session.BasePath + ") ",
            "Security Question",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button3);

    // ---------------------------------------------------------------- saved folders

    /// <summary>Every "Path N" menu entry runs this; N comes from the command's text.</summary>
    private void kryptonCommand_Execute(object? sender, EventArgs e)
    {
        if (sender is KryptonCommand command && int.TryParse(command.Text.Replace("Path ", string.Empty), out var slot)) ChooseSlot(slot);
    }

    private void ChooseSlot(int slot)
    {
        _slot = slot;
        DrpBtnPath.Text = "Path " + slot;
        RefreshUi();
    }

    private void btnLoadLoc_Click(object? sender, EventArgs e)
    {
        if (_slot == 0) return;

        var path = _settings.GetSlot(_slot);
        if (path.Length == 0) SetStatus("No path stored in this field");
        else LoadFolder(path);
    }

    private void btnSaveLoc_Click(object? sender, EventArgs e)
    {
        if (_slot == 0) return;
        if (_session.BasePath == null || _session.State == FolderState.Missing)
        {
            SetStatus("Nothing to save");
            return;
        }

        _settings.SetSlot(_slot, _session.BasePath);
        SaveSettings();
        SetStatus("Path " + _slot + " saved");
        RefreshUi();
    }

    private void btnClearLoc_Click(object? sender, EventArgs e)
    {
        if (_slot == 0) return;

        _settings.ClearSlot(_slot);
        SaveSettings();
        SetStatus("Path " + _slot + " cleared");
        RefreshUi();
    }

    /// <summary>Clicking the light shows what the chosen slot holds (same texts as the original).</summary>
    private void panel1_Click(object? sender, EventArgs e)
    {
        string text;
        if (_slot == 0)
        {
            text = "\nThis controller check over Memorized address,\nAnd for seeing any info you need to choose one path slot.";
        }
        else
        {
            var path = _settings.GetSlot(_slot);
            if (path.Length == 0)
            {
                text = "\nPath number " + _slot + " is empty";
            }
            else
            {
                switch (FolderLocker.GetState(path))
                {
                    case FolderState.Locked:
                        text = path + "\nPath number " + _slot + " is locked";
                        break;
                    case FolderState.Unlocked:
                        text = path + "\nPath number " + _slot + " is unlocked";
                        break;
                    default:
                        text = path + "\nPath number " + _slot + " was not found";
                        break;
                }
            }
        }

        _toolTip.Show(text, panel1, 0, panel1.Height + 2, 5000);
    }

    // ---------------------------------------------------------------- showing the state

    private void RefreshUi()
    {
        var state = _session.State;
        var loaded = _session.IsLoaded;
        lblAddress.Text = _session.BasePath ?? "None";
        btnLock.Enabled = state == FolderState.Unlocked;
        btnUnlock.Enabled = state == FolderState.Locked;
        btnOpen.Enabled = state == FolderState.Unlocked;
        btnUnload.Enabled = loaded;
        btnMoreInfo.Enabled = loaded;

        btnSaveLoc.Enabled = _slot > 0;
        btnClearLoc.Enabled = _slot > 0;

        if (_slot == 0)
        {
            panel1.BackColor = Color.Black;
            btnLoadLoc.Enabled = false;
            return;
        }

        var slotPath = _settings.GetSlot(_slot);
        if (slotPath.Length == 0)
        {
            panel1.BackColor = Color.Yellow;
            btnLoadLoc.Enabled = false;
        }
        else if (FolderLocker.GetState(slotPath) == FolderState.Missing)
        {
            panel1.BackColor = Color.Red;
            btnLoadLoc.Enabled = false;
        }
        else
        {
            panel1.BackColor = Color.Green;
            btnLoadLoc.Enabled = true;
        }
    }

    private void SetStatus(string text, string? detail = null)
    {
        lblStatus.Text = text;
        _toolTip.SetToolTip(lblStatus, detail ?? string.Empty);
    }

    private void SaveSettings()
    {
        try
        {
            _store.Save(_settings);
        }
        catch (Exception ex) when (IsFileProblem(ex))
        {
            SetStatus("Can't save settings", ex.Message);
        }
    }

    private static bool IsFileProblem(Exception ex) =>
        ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException;

    private int Scale(int pixels96) => (int)Math.Round(pixels96 * DeviceDpi / 96.0);

    /// <summary>The address box grows one line per click and the window grows with it (like the original).</summary>
    private void ApplyAddressLines()
    {
        if (_addressLines > MaxLines) _addressLines = 1;
        if (_addressLines < 1) _addressLines = 1;

        lblAddress.Height = Scale(LineHeight * _addressLines);
        btnLessInfo.Visible = _addressLines > 1;
        if (_addressLines > 1) btnLessInfo.Top = Scale(47 - 2 * LineHeight + LineHeight * _addressLines);
        ClientSize = new Size(ClientSize.Width, Scale(FormClientHeight - LineHeight + LineHeight * _addressLines));
    }

    // ---------------------------------------------------------------- theme, skin and the Setting window

    private void ApplyTheme() => kryptonManager.GlobalPaletteMode = ThemeMapper.ToPaletteMode(_settings.Theme);

    /// <summary>
    /// Loads the background picture and shows it as the window's own background (the "cast on the form" of the 2008 version),
    /// and applies the info-box transparency.
    /// </summary>
    private void ApplySkin()
    {
        var oldImage = _skinImage;
        _skinImage = null;

        var path = _skinStore.GetPath(_settings.SkinImageFile);
        if (path != null)
        {
            try
            {
                _skinImage = SkinImageLoader.Load(path);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is OutOfMemoryException || ex is ArgumentException)
            {
                SetStatus("Can't show image in main form", ex.Message);
            }
        }

        oldImage?.Dispose();
        UpdateSkinBackground();

        // "Transparent for Info Section": the info box lets the background show through.
        if (_settings.TransparentInfo)
        {
            groupBox1.StateCommon.Back.Color1 = Color.Transparent;
            groupBox1.StateCommon.Back.Color2 = Color.Transparent;
            groupBox1.Panel.StateCommon.Color1 = Color.Transparent;
            groupBox1.Panel.StateCommon.Color2 = Color.Transparent;
        }
        else
        {
            groupBox1.StateCommon.Back.Color1 = Color.Empty;
            groupBox1.StateCommon.Back.Color2 = Color.Empty;
            groupBox1.Panel.StateCommon.Color1 = Color.Empty;
            groupBox1.Panel.StateCommon.Color2 = Color.Empty;
        }

        groupBox1.Invalidate(true);
    }

    /// <summary>
    /// Draws the picture once for the whole window (frame included) and uses it twice: as the background of the panel
    /// behind the buttons, and as the background of the window frame around it. The frame then shows the same picture
    /// instead of standing out as a thick box.
    /// </summary>
    private void UpdateSkinBackground()
    {
        var oldPanelImage = _skinRendered;
        var oldFrameImage = _frameRendered;
        _skinRendered = null;
        _frameRendered = null;

        var origin = PointToScreen(Point.Empty);
        var crop = new Rectangle(origin.X - Left, origin.Y - Top, kryptonPanel.ClientSize.Width, kryptonPanel.ClientSize.Height);

        if (_skinImage != null && Width > 0 && Height > 0)
        {
            _frameRendered = SkinRenderer.Render(_skinImage, _settings.SkinImageMode, Size);
            crop.Intersect(new Rectangle(Point.Empty, _frameRendered.Size));
            if (crop.Width > 0 && crop.Height > 0) _skinRendered = _frameRendered.Clone(crop, _frameRendered.PixelFormat);
        }

        kryptonPanel.StateCommon.Image = _skinRendered;
        kryptonPanel.StateCommon.ImageStyle = PaletteImageStyle.TopLeft;
        var frame = StateCommon?.Back;
        if (frame != null)
        {
            frame.Image = _frameRendered;
            frame.ImageStyle = PaletteImageStyle.TopLeft;
        }

        oldPanelImage?.Dispose();
        oldFrameImage?.Dispose();
        kryptonPanel.Invalidate();
        Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (_skinImage != null) UpdateSkinBackground(); // the window grows with the address lines
    }

    private void OpenSettings()
    {
        if (_settingForm != null && !_settingForm.IsDisposed)
        {
            _settingForm.Activate();
            return;
        }

        _settingForm = new SettingForm(_settings, _store, _skinStore);
        _settingForm.Applied += (sender, e) =>
        {
            ApplyTheme();
            ApplySkin();
        };
        _settingForm.FormClosed += (sender, e) => _settingForm = null;
        _settingForm.Show(this);
    }
}

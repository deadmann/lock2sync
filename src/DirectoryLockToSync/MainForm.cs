using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The main window, laid out exactly like the 2008-2010 KryptonDLTS form (same positions, sizes and tab order):
/// About / Browse / Setting on the left, the saved-folder light and Path menu in the middle, Lock / Unlock / Unload
/// on the right, Open Folder across, and the Status / Address box that can grow line by line.
/// All folder logic lives in <see cref="FolderSession"/> and <see cref="AppSettings"/> (tested in Core).
/// </summary>
public sealed class MainForm : ChromeForm
{
    // Layout numbers of the original form, in 96-dpi pixels.
    private const int FormClientWidth = 326;
    private const int FormClientHeight = 208;
    private const int LineHeight = 13;
    private const int MaxLines = 10; // pressing "more" on line 10 starts again at 1, like the original

    private readonly SettingsStore _store;
    private readonly AppSettings _settings;
    private readonly SkinImageStore _skinStore;
    private readonly FolderSession _session = new FolderSession();
    private readonly KryptonManager _manager = new KryptonManager();
    private readonly ToolTip _toolTip = new ToolTip();

    private readonly KryptonButton _aboutButton = NewButton("&About", 12, 12, 90, 0);
    private readonly KryptonButton _browseButton = NewButton("&Browse", 12, 43, 90, 1);
    private readonly KryptonButton _settingButton = NewButton("&Setting", 12, 74, 90, 2);
    private readonly KryptonButton _lockButton = NewButton("&Lock", 224, 12, 90, 3);
    private readonly KryptonButton _unlockButton = NewButton("&Unlock", 224, 43, 90, 4);
    private readonly KryptonButton _unloadButton = NewButton("U&nload", 224, 74, 90, 5);
    private readonly KryptonButton _openButton = NewButton("&Open Folder", 12, 105, 302, 6);
    private readonly KryptonDropButton _pathButton = new KryptonDropButton();
    private readonly KryptonButton _loadSlotButton = NewButton("Load Location", 108, 43, 110, 8);
    private readonly KryptonButton _saveSlotButton = NewButton("Save", 108, 74, 52, 9);
    private readonly KryptonButton _clearSlotButton = NewButton("Clear", 166, 74, 52, 10);
    private readonly Panel _light = new Panel();
    private readonly KryptonPanel _panel = new KryptonPanel { Dock = DockStyle.Fill };
    private Image? _skinImage;       // the background picture as loaded
    private Bitmap? _skinRendered;   // the part of the picture behind the buttons
    private Bitmap? _frameRendered;  // the same picture for the whole window, so the frame around it shows it too
    private SettingForm? _settingForm;

    private readonly KryptonGroup _infoGroup = new KryptonGroup();
    private readonly KryptonLabel _statusCaption = new KryptonLabel();
    private readonly KryptonLabel _addressCaption = new KryptonLabel();
    private readonly Label _statusLabel = new Label();
    private readonly Label _addressLabel = new Label();
    private readonly KryptonButton _moreButton = NewButton("︾", 270, 22, 27, 4, 25);
    private readonly KryptonButton _lessButton = NewButton("︽", 270, 48, 27, 5, 25);

    private int _addressLines = 1;
    private int _slot; // 0 = no path slot chosen yet

    public MainForm(SettingsStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _settings = store.Load();
        _skinStore = new SkinImageStore(Path.GetDirectoryName(Path.GetFullPath(store.FilePath))!);

        BuildUi();
        Load += (sender, e) => OnStart();
    }

    private static KryptonButton NewButton(string text, int x, int y, int width, int tabIndex, int height = 25) =>
        new KryptonButton
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(width, height),
            TabIndex = tabIndex
        };

    // ---------------------------------------------------------------- layout (same as the original designer)

    private void BuildUi()
    {
        SuspendLayout();

        // The saved theme; the original default is Light-Blue (Office 2007 Blue).
        ApplyTheme();

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Directory Lock To Sync";
        try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch (Exception) { /* the default icon is fine */ }

        _light.BackColor = Color.Black;
        _light.BorderStyle = BorderStyle.Fixed3D;
        _light.Location = new Point(108, 12);
        _light.Size = new Size(11, 25);
        _light.TabIndex = 18;

        _pathButton.Text = "Choose Path";
        _pathButton.Location = new Point(120, 12);
        _pathButton.Size = new Size(98, 25);
        _pathButton.TabIndex = 7;
        _pathButton.KryptonContextMenu = BuildPathMenu();

        _statusCaption.Text = "Status";
        _statusCaption.Location = new Point(3, 5);
        _statusCaption.Size = new Size(41, 19);
        _addressCaption.Text = "Address";
        _addressCaption.Location = new Point(3, 30);
        _addressCaption.Size = new Size(50, 19);

        _statusLabel.AutoSize = true;
        _statusLabel.BackColor = Color.Transparent;
        _statusLabel.ForeColor = Color.Crimson;
        _statusLabel.Location = new Point(59, 8);
        _addressLabel.AutoSize = false;
        _addressLabel.BackColor = Color.Transparent;
        _addressLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        _addressLabel.ForeColor = Color.Crimson;
        _addressLabel.Location = new Point(59, 32);
        _addressLabel.Size = new Size(205, LineHeight);

        _infoGroup.Location = new Point(12, 137);
        _infoGroup.Size = new Size(302, 65);
        _infoGroup.TabIndex = 21;
        _infoGroup.Panel.Controls.AddRange(new Control[]
        {
            _lessButton, _moreButton, _addressLabel, _statusLabel, _addressCaption, _statusCaption
        });

        _panel.Controls.AddRange(new Control[]
        {
            _infoGroup, _clearSlotButton, _saveSlotButton, _loadSlotButton, _openButton, _pathButton,
            _unloadButton, _settingButton, _unlockButton, _browseButton, _lockButton, _aboutButton, _light
        });
        Controls.Add(_panel);
        ClientSize = new Size(FormClientWidth, FormClientHeight); // 96-dpi units; the form scales itself for other DPIs

        _aboutButton.Click += (sender, e) => NotPortedYet("About");
        _settingButton.Click += (sender, e) => OpenSettings();
        _browseButton.Click += (sender, e) => Browse();
        _lockButton.Click += (sender, e) => LockFolder();
        _unlockButton.Click += (sender, e) => UnlockFolder();
        _openButton.Click += (sender, e) => OpenFolder();
        _unloadButton.Click += (sender, e) => UnloadFolder();
        _loadSlotButton.Click += (sender, e) => LoadSlot();
        _saveSlotButton.Click += (sender, e) => SaveSlot();
        _clearSlotButton.Click += (sender, e) => ClearSlot();
        _moreButton.Click += (sender, e) => { _addressLines++; ApplyAddressLines(); };
        _lessButton.Click += (sender, e) => { _addressLines--; ApplyAddressLines(); };
        _light.Click += (sender, e) => ShowSlotTip();

        // Disabled until there is something to act on (like the designer).
        foreach (var button in new[] { _lockButton, _unlockButton, _unloadButton, _openButton, _loadSlotButton, _saveSlotButton, _clearSlotButton, _moreButton })
        {
            button.Enabled = false;
        }

        ResumeLayout(false);
        PerformLayout();
        ApplyDesignerAnchors();
    }

    /// <summary>
    /// The anchors of the original designer. They are set after the first layout so each control measures its
    /// distances from its final position (setting them earlier pushed the right-hand controls out of view).
    /// </summary>
    private void ApplyDesignerAnchors()
    {
        const AnchorStyles topRight = AnchorStyles.Top | AnchorStyles.Right;
        const AnchorStyles topLeftRight = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        _lockButton.Anchor = _unlockButton.Anchor = _unloadButton.Anchor = topRight;
        _clearSlotButton.Anchor = topRight;
        _moreButton.Anchor = _lessButton.Anchor = topRight;
        _openButton.Anchor = _loadSlotButton.Anchor = _pathButton.Anchor = topLeftRight;
        _addressLabel.Anchor = topLeftRight;
        _infoGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    }

    // ---------------------------------------------------------------- theme, skin and the Setting window

    private void ApplyTheme() => _manager.GlobalPaletteMode = ThemeMapper.ToPaletteMode(_settings.Theme);

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
            _infoGroup.StateCommon.Back.Color1 = Color.Transparent;
            _infoGroup.StateCommon.Back.Color2 = Color.Transparent;
            _infoGroup.Panel.StateCommon.Color1 = Color.Transparent;
            _infoGroup.Panel.StateCommon.Color2 = Color.Transparent;
        }
        else
        {
            _infoGroup.StateCommon.Back.Color1 = Color.Empty;
            _infoGroup.StateCommon.Back.Color2 = Color.Empty;
            _infoGroup.Panel.StateCommon.Color1 = Color.Empty;
            _infoGroup.Panel.StateCommon.Color2 = Color.Empty;
        }

        _infoGroup.Invalidate(true);
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
        var crop = new Rectangle(origin.X - Left, origin.Y - Top, _panel.ClientSize.Width, _panel.ClientSize.Height);

        if (_skinImage != null && Width > 0 && Height > 0)
        {
            _frameRendered = SkinRenderer.Render(_skinImage, _settings.SkinImageMode, Size);
            crop.Intersect(new Rectangle(Point.Empty, _frameRendered.Size));
            if (crop.Width > 0 && crop.Height > 0) _skinRendered = _frameRendered.Clone(crop, _frameRendered.PixelFormat);
        }

        _panel.StateCommon.Image = _skinRendered;
        _panel.StateCommon.ImageStyle = PaletteImageStyle.TopLeft;
        var frame = StateCommon?.Back;
        if (frame != null)
        {
            frame.Image = _frameRendered;
            frame.ImageStyle = PaletteImageStyle.TopLeft;
        }

        oldPanelImage?.Dispose();
        oldFrameImage?.Dispose();
        _panel.Invalidate();
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

        _settingForm = new SettingForm(_settings, _store, _skinStore, _manager);
        _settingForm.Applied += (sender, e) =>
        {
            ApplyTheme();
            ApplySkin();
        };
        _settingForm.FormClosed += (sender, e) => _settingForm = null;
        _settingForm.Show(this);
    }

    private KryptonContextMenu BuildPathMenu()
    {
        var items = new KryptonContextMenuItems { ImageColumn = false }; // no empty icon strip on the left, like the 2008 menu
        for (var slot = 1; slot <= AppSettings.PathSlotCount; slot++)
        {
            var number = slot;
            var item = new KryptonContextMenuItem("Path " + slot);
            item.Click += (sender, e) => ChooseSlot(number);
            items.Items.Add(item);
        }

        var menu = new KryptonContextMenu();
        menu.Items.Add(items);
        return menu;
    }

    private void NotPortedYet(string what) =>
        MessageBox.Show(this, "The " + what + " window is not ported to this version yet (issue #3).", Text,
            MessageBoxButtons.OK, MessageBoxIcon.Information);

    private int Scale(int pixels96) => (int)Math.Round(pixels96 * DeviceDpi / 96.0);

    /// <summary>The address box grows one line per click and the window grows with it (like the original).</summary>
    private void ApplyAddressLines()
    {
        if (_addressLines > MaxLines) _addressLines = 1;
        if (_addressLines < 1) _addressLines = 1;

        _addressLabel.Height = Scale(LineHeight * _addressLines);
        _lessButton.Visible = _addressLines > 1;
        if (_addressLines > 1) _lessButton.Top = Scale(47 - 2 * LineHeight + LineHeight * _addressLines);
        ClientSize = new Size(ClientSize.Width, Scale(FormClientHeight - LineHeight + LineHeight * _addressLines));
    }

    // ---------------------------------------------------------------- start and close

    private void OnStart()
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

    protected override void OnFormClosing(FormClosingEventArgs e)
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
        base.OnFormClosing(e);
    }

    // ---------------------------------------------------------------- folder actions

    private void Browse()
    {
        using (var dialog = new FolderBrowserDialog
        {
            ShowNewFolderButton = true,
            Description = "Please ,choose any folder must become lock or unlock"
        })
        {
            if (_session.BasePath != null) dialog.SelectedPath = _session.BasePath;
            if (dialog.ShowDialog(this) == DialogResult.OK) LoadFolder(dialog.SelectedPath);
        }
    }

    private void LoadFolder(string path)
    {
        if (_session.TryLoad(path))
            SetStatus(_session.State == FolderState.Locked ? "Locked Folder Loaded" : "Unlocked Folder Loaded");
        else
            SetStatus("path not found");

        RefreshUi();
    }

    private void LockFolder()
    {
        if (TryLock()) SetStatus("Folder Locked");
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

    private void UnlockFolder()
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

    private void OpenFolder()
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

    private void UnloadFolder()
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

    private void ChooseSlot(int slot)
    {
        _slot = slot;
        _pathButton.Text = "Path " + slot;
        RefreshUi();
    }

    private void LoadSlot()
    {
        if (_slot == 0) return;

        var path = _settings.GetSlot(_slot);
        if (path.Length == 0) SetStatus("No path stored in this field");
        else LoadFolder(path);
    }

    private void SaveSlot()
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

    private void ClearSlot()
    {
        if (_slot == 0) return;

        _settings.ClearSlot(_slot);
        SaveSettings();
        SetStatus("Path " + _slot + " cleared");
        RefreshUi();
    }

    /// <summary>Clicking the light shows what the chosen slot holds (same texts as the original).</summary>
    private void ShowSlotTip()
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

        _toolTip.Show(text, _light, 0, _light.Height + 2, 5000);
    }

    // ---------------------------------------------------------------- showing the state

    private void RefreshUi()
    {
        var state = _session.State;
        var loaded = _session.IsLoaded;
        _addressLabel.Text = _session.BasePath ?? "None";
        _lockButton.Enabled = state == FolderState.Unlocked;
        _unlockButton.Enabled = state == FolderState.Locked;
        _openButton.Enabled = state == FolderState.Unlocked;
        _unloadButton.Enabled = loaded;
        _moreButton.Enabled = loaded;

        _saveSlotButton.Enabled = _slot > 0;
        _clearSlotButton.Enabled = _slot > 0;

        if (_slot == 0)
        {
            _light.BackColor = Color.Black;
            _loadSlotButton.Enabled = false;
            return;
        }

        var slotPath = _settings.GetSlot(_slot);
        if (slotPath.Length == 0)
        {
            _light.BackColor = Color.Yellow;
            _loadSlotButton.Enabled = false;
        }
        else if (FolderLocker.GetState(slotPath) == FolderState.Missing)
        {
            _light.BackColor = Color.Red;
            _loadSlotButton.Enabled = false;
        }
        else
        {
            _light.BackColor = Color.Green;
            _loadSlotButton.Enabled = true;
        }
    }

    private void SetStatus(string text, string? detail = null)
    {
        _statusLabel.Text = text;
        _toolTip.SetToolTip(_statusLabel, detail ?? string.Empty);
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
}

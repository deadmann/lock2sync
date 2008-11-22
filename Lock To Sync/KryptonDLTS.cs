using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using ComponentFactory.Krypton.Toolkit;

namespace Lock_To_Sync
{
    public partial class KryptonDLTS : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public KryptonDLTS()
        {
            InitializeComponent();
        }
        //to have public access to Setting1 form
        public KryptonSetting Setting1 = new KryptonSetting();//ver3.1.0.0



        String _dir = Directory.GetCurrentDirectory() + "\\image.jpg";//ver3.1.0.0
        public System.Drawing.Bitmap _bitmap = new Bitmap(Lock_To_Sync.Properties.Resources._Image);
        //Counter for count of address info lines
        public int _AddressLines = 1;

        //Address String
        public string strFolderPath = "None";
        //tooltip for path storage report
        public ToolTip _tooltip = new ToolTip();

        /// <summary>
        /// void-position of button btnLessInfo
        /// </summary>
        public void btnLessInfoPosition()
        {
            if (_AddressLines ==/*lines limited*/ 11) _AddressLines = 1;
            if (_AddressLines == 1)
            {
                btnLessInfo.Visible = false;
                this.Height = /*form size*/240 - /*label line size*/13 + (/*label line size*/13 */*count of line*/ _AddressLines);
            }
            if (_AddressLines > 1)
            {
                btnLessInfo.Visible = true;
                this.btnLessInfo.Top = /*button height of GroupForm*/47 - /*label line size (2X)*/26 + (/*label line size*/13 * _AddressLines);
                this.Height = /*form size*/240 - /*label line size*/13 + (/*label line size*/13 * /*count of line*/_AddressLines);
            }
        }
        /// <summary>
        /// void-Reset all Object Parameter to starter value
        /// </summary>
        public void ResetProgram()
        {
            folderBrowserDialog1.SelectedPath = folderBrowserDialog1.RootFolder.ToString();
            lblAddress.Text = "None";
            lblStatus.Text = "Nothing Loaded";
            _AddressLines = 1;
            btnLessInfoPosition();
            btnLock.Enabled = false;
            btnUnlock.Enabled = false;
            btnOpen.Enabled = false;
            btnMoreInfo.Enabled = false;
            btnUnload.Enabled = false;
        }
        /// <summary>
        /// void-Folder is lock or unlock , and show address without type in lblAddress , and set button
        /// </summary>
        public void LockedOrUnlocked()
        {
            string[] strIntelligence = strFolderPath.Split('.');
            //folder has code (Locked)
            if (String.Compare(strIntelligence[strIntelligence.Length - 1], @"{F5175861-2688-11D0-9C5E-00AA00A45957}", true) == 0)
            {
                lblStatus.Text = "Locked Folder Loaded";
                btnLock.Enabled = false;
                btnUnlock.Enabled = true;
                btnOpen.Enabled = false;
                int i = 0;
                lblAddress.Text = "";
                for (i = 0; i < strIntelligence.Length - 1; i++)
                {
                    if (i >= 1) lblAddress.Text = lblAddress.Text + ".";
                    lblAddress.Text = lblAddress.Text + strIntelligence[i];
                }
            }
            //folder have no code(Unlocked)
            else
            {
                lblStatus.Text = "Unlocked Folder Loaded";
                btnLock.Enabled = true;
                btnUnlock.Enabled = false;
                btnOpen.Enabled = true;
                lblAddress.Text = strFolderPath;
            }
            btnUnload.Enabled = true;
            btnMoreInfo.Enabled = true;
        }
        /// <summary>
        /// void-Folder Locking
        /// </summary>
        public void FolderLocking()
        {
            try
            {
                Directory.Move(lblAddress.Text, strFolderPath = lblAddress.Text + @".{F5175861-2688-11D0-9C5E-00AA00A45957}");
                btnLock.Enabled = false;
                btnUnlock.Enabled = true;
                btnOpen.Enabled = false;
                folderBrowserDialog1.SelectedPath = strFolderPath;
                lblStatus.Text = "Folder Locked";
            }
            catch
            {
                lblStatus.Text = "Can't Lock Folder!";
                strFolderPath = lblAddress.Text;
            }
        }
        /// <summary>
        /// void-Folder Unlocking
        /// </summary>
        public void FolderUnlocking()
        {
            try
            {
                Directory.Move(strFolderPath, lblAddress.Text);
                btnLock.Enabled = true;
                btnUnlock.Enabled = false;
                btnOpen.Enabled = true;
                folderBrowserDialog1.SelectedPath = lblAddress.Text;
                lblStatus.Text = "Folder Unlocked";
            }
            catch
            {
                lblStatus.Text = "Can't Unlock Folder!";
            }
        }
        //In begin
        private void Form1_Load(object sender, EventArgs e)
        {
            //Make program design ready
            ResetProgram();
            //ver3.1.0.0 set Setting1 Form Parameters (copy of Setting1 On_Form_Load)
            if (Lock_To_Sync.Properties.Settings.Default.Image != "")
            {
                Setting1.pictureBox1.ImageLocation = _dir;//ver3.1.0.0
                Setting1.ImgStatus = "Loaded";
            }
            else
            {
                Setting1.pictureBox1.Image = null;
                Setting1.ImgStatus = "";
            }
            //If Program "Setting>>Default>>Activity==1" run Login Form
            if (Lock_To_Sync.Properties.Settings.Default.Active == "1")
            {
                Login login1 = new Login();
                login1.ShowDialog(this);
            }
            //Set last address to program & Program parameter & Info
            if (Properties.Settings.Default.KeepLastAddress == "1")
            {
                if (Directory.Exists(Properties.Settings.Default.LastAddress))
                {
                    folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastAddress;
                    strFolderPath = Properties.Settings.Default.LastAddress;
                    lblStatus.Text = "Last Directory Loaded";
                    LockedOrUnlocked();//Set parameter & set strFolderPath
                }
                else if (Properties.Settings.Default.LastAddress == "None" || Properties.Settings.Default.LastAddress == "")
                {
                    lblStatus.Text = "Ready";
                }
                else
                {
                    lblStatus.Text = "Last Folder no longer exist";
                }
            }
            //Set Skin
            //pic
            if (Lock_To_Sync.Properties.Settings.Default.Image != "")
            {
                _bitmap = new Bitmap(_dir);//ver3.1.0.0
                /*ver3.0.0.0
                 _bitmap = (Bitmap)Lock_To_Sync.Properties.Settings.Default.Imagef;
                */

                /* Normal ver
                this.BackgroundImage = _bitmap;
                 */
                pictureBox1.BackgroundImage = _bitmap;
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }
            //transparenty
            if (Lock_To_Sync.Properties.Settings.Default.Transparent == "0")
            {
                /* Normal ver
                 * groupBox1.BackColor = GroupBox.DefaultBackColor;
                 */
                groupBox1.Panel.BackColor = GroupBox.DefaultBackColor;
            }
            else
            {
                if (Lock_To_Sync.Properties.Settings.Default.Transparent == "1")
                {
                    /* Normal ver
                 * groupBox1.BackColor = GroupBox.DefaultBackColor;
                 */
                    groupBox1.Panel.BackColor = Color.Transparent;
                }
            }
            //Pic Zoom
            switch (Properties.Settings.Default.SkinPicSizeMode)
            {
                case "1":
                    /* Normal ver
                    this.BackgroundImageLayout = ImageLayout.Tile;
                     */
                    pictureBox1.BackgroundImageLayout = ImageLayout.Tile;
                    break;
                case "2":
                    /* Normal ver
                    this.BackgroundImageLayout = ImageLayout.Center;
                     */
                    pictureBox1.BackgroundImageLayout = ImageLayout.Center;
                    break;
                case "3":
                    /* Normal ver
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                     */
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case "4":
                    /* Normal ver
                    this.BackgroundImageLayout = ImageLayout.Zoom;
                     */
                    pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                default:
                    /* Normal ver
                    this.BackgroundImageLayout = ImageLayout.Tile;
                     */
                    pictureBox1.BackgroundImageLayout = ImageLayout.Tile;

                    Properties.Settings.Default.SkinPicSizeMode = "1";
                    Properties.Settings.Default.Save();
                    break;
            }
            //theme select
            switch (Properties.Settings.Default.KryptonTheme)
            {
                case "Light-Blue":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case "Light-Silver":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case "Light-Black":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case "Dark-Blue":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case "Dark-Orange":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                case "Dark-Purple":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
                    break;
                case "Simple":
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                default:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
            }
        }
        //Button for showing About dialog
        private void btnAbout_Click(object sender, EventArgs e)
        {
            //Open About Box
            KryptonAbout _AboutDialog = new KryptonAbout();
            _AboutDialog.Show(this);
        }
        //Button allow you to show address info in more line
        private void btnMoreInfo_Click(object sender, EventArgs e)
        {
            //1 line more
            _AddressLines++;
            //Change btnlessInfo position
            btnLessInfoPosition();
            //If address space is not Enough Show in more lines
            this.lblAddress.Height = 13 * _AddressLines;
        }
        //Button allow you to show address info in less line
        private void btnLessInfo_Click(object sender, EventArgs e)
        {
            //1 line less
            _AddressLines--;
            //Change btnlessInfo position
            btnLessInfoPosition();
            //If address space is more than we need Show in less lines
            this.lblAddress.Height = 13 * _AddressLines;
        }
        //Read Address Information
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Setting folderBrowserDialog parameter
            folderBrowserDialog1.ShowNewFolderButton = true;
            //Show FolderBrowseDialog and load info if user click on OK
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                strFolderPath = folderBrowserDialog1.SelectedPath;
                //choose which button must be active
                LockedOrUnlocked();
            }
        }
        //Clear Address Information (+Reset address line and program size)
        private void btnUnload_Click(object sender, EventArgs e)
        {
            //When security question is enable
            if (btnLock.Enabled == true && Properties.Settings.Default.SecurityQuestion == "1")
            {
                DialogResult _result = new DialogResult();
                _result = MessageBox.Show("Do you wanna Lock this folder before unload data ?\n (" + lblAddress.Text + ") ", "Security Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (_result == DialogResult.Yes)
                {
                    lblStatus.Text = "successfull Unload and folder locked";
                    FolderLocking();
                    ResetProgram();
                }
                else if (_result == DialogResult.No)
                {
                    lblStatus.Text = "successfull Unload and folder didn't lock";
                    ResetProgram();
                }
                else if (_result == DialogResult.Cancel)
                {
                    lblStatus.Text = "Unload canceled by user";
                }
            }
            //When security question is disable
            else
            {
                lblStatus.Text = "successfull Unload";
                ResetProgram();
            }
        }
        //lock folder
        private void btnLock_Click(object sender, EventArgs e)
        {
            FolderLocking();
        }
        //unlock folder
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            FolderUnlocking();
        }
        //Show setting form
        private void btnSetting_Click(object sender, EventArgs e)
        {
            /*ver3.0.0.0
            Setting Setting1 = new Setting();
            */
            Setting1 = new KryptonSetting();
            Setting1._dir = _dir;
            Setting1.Show(this);
        }
        //in end of program
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnLock.Enabled == true && Properties.Settings.Default.SecurityQuestion == "1")
            {
                DialogResult _result = new DialogResult();
                _result = MessageBox.Show("Do you wanna Lock this folder before close program ?\n (" + lblAddress.Text + ") ", "Security Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (_result == DialogResult.Yes)
                {
                    FolderLocking();
                }
                else if (_result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    lblStatus.Text = "Program close ,canceled";
                }
            }
            Properties.Settings.Default.LastAddress = strFolderPath;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            if (Properties.Settings.Default.LastAddress != strFolderPath)
            {
                Properties.Settings.Default.LastAddress = strFolderPath;
                Properties.Settings.Default.Save();
            }
        }
        //More and test object .... (before build must be delete)
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Setting1._ok == true)//ver3.1.0.0
            {
                Setting1._ok = false;//ver3.1.0.0
                try//ver3.1.0.0 (second config) -- Code moved from setting form
                {
                    if (Setting1.ImgStatus != "")
                    {
                        if (Setting1.pictureBox1.ImageLocation != _dir)//ver 3.1.0.0
                        {
                            string _AA = Setting1.pictureBox1.ImageLocation;//new
                            Setting1.pictureBox1.ImageLocation = "";//new
                            Setting1.pictureBox1.Image = null;//new
                            /* Normal ver
                            this.BackgroundImage = null;//new
                             */
                            pictureBox1.BackgroundImage = null;
                            this._bitmap.Dispose();//new
                            File.Copy(_AA, _dir, true);
                            _bitmap = new Bitmap(_dir);//new
                            Setting1.pictureBox1.ImageLocation = _dir;
                        }
                        Lock_To_Sync.Properties.Settings.Default.Image = "Loaded";
                    }
                    else
                    {
                        Lock_To_Sync.Properties.Settings.Default.Image = "";
                    }
                    //Save setting
                    Properties.Settings.Default.Save();
                }
                catch
                {
                    MessageBox.Show("Can't show image in main form", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    if (Lock_To_Sync.Properties.Settings.Default.Image != "")
                    {
                        _bitmap = new Bitmap(_dir);//ver3.1.0.0
                        /*ver3.0.0.0
                        _bitmap = ((Bitmap)Lock_To_Sync.Properties.Settings.Default.Imagef);
                        */

                        /* Normal ver
                        this.BackgroundImage = _bitmap;
                         */
                        pictureBox1.BackgroundImage = _bitmap;
                        pictureBox1.Visible = true;
                    }
                    else
                    {
                        pictureBox1.Visible = false;
                        pictureBox1.BackgroundImage = null;
                        /* Normal ver
                        this.BackgroundImage = null;
                         */
                    }
                }
                catch
                {
                    pictureBox1.BackgroundImage = null;
                    /* Normal ver
                    this.BackgroundImage = null;
                     */
                }
                if (Lock_To_Sync.Properties.Settings.Default.Transparent == "0")
                {
                    groupBox1.BackColor = GroupBox.DefaultBackColor;
                }
                else
                {
                    if (Lock_To_Sync.Properties.Settings.Default.Transparent == "1")
                    {
                        /* normal ver
                         groupBox1.BackColor = Color.Transparent;
                         */
                        groupBox1.Panel.BackColor = Color.Transparent;
                    }
                }
                //Pic Zoom
                switch (Properties.Settings.Default.SkinPicSizeMode)
                {
                    case "1":
                        /* Normal ver
                        this.BackgroundImageLayout = ImageLayout.Tile;
                         */
                        pictureBox1.BackgroundImageLayout = ImageLayout.Tile;
                        break;
                    case "2":
                        /* Normal ver
                        this.BackgroundImageLayout = ImageLayout.Center;
                         */
                        pictureBox1.BackgroundImageLayout = ImageLayout.Center;
                        break;
                    case "3":
                        /* Normal ver
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                         */
                        pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case "4":
                        /* Normal ver
                        this.BackgroundImageLayout = ImageLayout.Zoom;
                         */
                        pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                        break;
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", lblAddress.Text);
        }

        private void btnLoadLoc_Click(object sender, EventArgs e)
        {
            switch (DrpBtnPath.Text)
            {
                case "Path 1":
                    if (Directory.Exists(Properties.Settings.Default.Path1))
                    {
                        strFolderPath = Properties.Settings.Default.Path1;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path1 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 2":
                    if (Directory.Exists(Properties.Settings.Default.Path2))
                    {
                        strFolderPath = Properties.Settings.Default.Path2;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path2 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 3":
                    if (Directory.Exists(Properties.Settings.Default.Path3))
                    {
                        strFolderPath = Properties.Settings.Default.Path3;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path3 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 4":
                    if (Directory.Exists(Properties.Settings.Default.Path4))
                    {
                        strFolderPath = Properties.Settings.Default.Path4;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path4 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 5":
                    if (Directory.Exists(Properties.Settings.Default.Path5))
                    {
                        strFolderPath = Properties.Settings.Default.Path5;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path5 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 6":
                    if (Directory.Exists(Properties.Settings.Default.Path6))
                    {
                        strFolderPath = Properties.Settings.Default.Path6;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path6 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 7":
                    if (Directory.Exists(Properties.Settings.Default.Path7))
                    {
                        strFolderPath = Properties.Settings.Default.Path7;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path7 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 8":
                    if (Directory.Exists(Properties.Settings.Default.Path8))
                    {
                        strFolderPath = Properties.Settings.Default.Path8;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path8 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 9":
                    if (Directory.Exists(Properties.Settings.Default.Path9))
                    {
                        strFolderPath = Properties.Settings.Default.Path9;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path9 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
                case "Path 10":
                    if (Directory.Exists(Properties.Settings.Default.Path10))
                    {
                        strFolderPath = Properties.Settings.Default.Path10;
                        //choose which button must be active
                        LockedOrUnlocked();
                    }
                    else if (Properties.Settings.Default.Path10 == "")
                    {
                        lblStatus.Text = "No path stored in this field";
                    }
                    else
                    {
                        lblStatus.Text = "path not found";
                    }
                    break;
            }
        }

        private void btnSaveLoc_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(strFolderPath) || Directory.Exists(lblAddress.Text))
            {
                switch (DrpBtnPath.Text)
                {
                    case "Path 1":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path1 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path1 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 2":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path2 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path2 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 3":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path3 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path3 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 4":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path4 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path4 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 5":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path5 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path5 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 6":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path6 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path6 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 7":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path7 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path7 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 8":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path8 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path8 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 9":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path9 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path9 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                    case "Path 10":
                        if (btnUnlock.Enabled == true)
                        {
                            Properties.Settings.Default.Path10 = strFolderPath;
                        }
                        else if (btnLock.Enabled == true)
                        {
                            Properties.Settings.Default.Path10 = lblAddress.Text;
                        }
                        Properties.Settings.Default.Save();
                        break;
                }
                btnLoadLoc.Enabled = true;
                panel1.BackColor = Color.Green;
            }
        }

        private void btnClearLoc_Click(object sender, EventArgs e)
        {
            switch (DrpBtnPath.Text)
            {
                case "Path 1":
                    Properties.Settings.Default.Path1 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 2":
                    Properties.Settings.Default.Path2 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 3":
                    Properties.Settings.Default.Path3 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 4":
                    Properties.Settings.Default.Path4 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 5":
                    Properties.Settings.Default.Path5 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 6":
                    Properties.Settings.Default.Path6 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 7":
                    Properties.Settings.Default.Path7 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 8":
                    Properties.Settings.Default.Path8 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 9":
                    Properties.Settings.Default.Path9 = "";
                    Properties.Settings.Default.Save();
                    break;
                case "Path 10":
                    Properties.Settings.Default.Path10 = "";
                    Properties.Settings.Default.Save();
                    break;
            }
            panel1.BackColor = Color.Yellow;
        }

        /// <summary>
        /// It will control over location storage button and it's menu bar
        /// </summary>
        public void ScrlStorageDefine_ValueChanged()
        {
            switch (DrpBtnPath.Text)
            {
                case "Path 1":
                    if (Directory.Exists(Properties.Settings.Default.Path1))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path1 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 2":
                    if (Directory.Exists(Properties.Settings.Default.Path2))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path2 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 3":
                    if (Directory.Exists(Properties.Settings.Default.Path3))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path3 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 4":
                    if (Directory.Exists(Properties.Settings.Default.Path4))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path4 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 5":
                    if (Directory.Exists(Properties.Settings.Default.Path5))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path5 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 6":
                    if (Directory.Exists(Properties.Settings.Default.Path6))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path6 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 7":
                    if (Directory.Exists(Properties.Settings.Default.Path7))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path7 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 8":
                    if (Directory.Exists(Properties.Settings.Default.Path8))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path8 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 9":
                    if (Directory.Exists(Properties.Settings.Default.Path9))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path9 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
                case "Path 10":
                    if (Directory.Exists(Properties.Settings.Default.Path10))
                    {
                        panel1.BackColor = Color.Green;
                        btnLoadLoc.Enabled = true;
                    }
                    else if (Properties.Settings.Default.Path10 == "")
                    {
                        panel1.BackColor = Color.Yellow;
                        btnLoadLoc.Enabled = false;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                        btnLoadLoc.Enabled = false;
                    }
                    break;
            }
        }



        private void panel1_Click(object sender, EventArgs e)
        {
            string strPath = "";
            int pathNum = 0;
            switch (DrpBtnPath.Text)
            {
                case "Path 1":
                    strPath = Properties.Settings.Default.Path1;
                    pathNum = 1;
                    break;
                case "Path 2":
                    strPath = Properties.Settings.Default.Path2;
                    pathNum = 2;
                    break;
                case "Path 3":
                    strPath = Properties.Settings.Default.Path3;
                    pathNum = 3;
                    break;
                case "Path 4":
                    strPath = Properties.Settings.Default.Path4;
                    pathNum = 4;
                    break;
                case "Path 5":
                    strPath = Properties.Settings.Default.Path5;
                    pathNum = 5;
                    break;
                case "Path 6":
                    strPath = Properties.Settings.Default.Path6;
                    pathNum = 6;
                    break;
                case "Path 7":
                    strPath = Properties.Settings.Default.Path7;
                    pathNum = 7;
                    break;
                case "Path 8":
                    strPath = Properties.Settings.Default.Path8;
                    pathNum = 8;
                    break;
                case "Path 9":
                    strPath = Properties.Settings.Default.Path9;
                    pathNum = 9;
                    break;
                case "Path 10":
                    strPath = Properties.Settings.Default.Path10;
                    pathNum = 10;
                    break;
                default:
                    strPath = "";
                    break;
            }
            if (strPath == "")
            {
                if (pathNum == 0)
                {
                    _tooltip.Show("\nThis controller check over Memorized address,\nAnd for seeing any info you need to choose one path slot.", panel1);
                }
                else
                {
                    _tooltip.Show("\nPath number " + pathNum + " is empty", panel1);
                }
            }
            else
            {
                string[] strIntelligence = strPath.Split('.');
                string strPath2;
                //folder has code (Locked)
                if (String.Compare(strIntelligence[strIntelligence.Length - 1], @"{F5175861-2688-11D0-9C5E-00AA00A45957}", true) == 0)
                {
                    int i = 0;
                    strPath2 = "";
                    for (i = 0; i < strIntelligence.Length - 1; i++)
                    {
                        if (i >= 1) strPath2 = strPath2 + ".";
                        strPath2 = strPath2 + strIntelligence[i];
                    }
                    _tooltip.Show(strPath2 + "\nPath number " + pathNum + " is locked", panel1);
                }
                //folder have no code(Unlocked)
                else
                {
                    _tooltip.Show(strPath + "\nPath number " + pathNum + " is unlocked", panel1);
                }
            }
        }

        private void kryptonCommand1_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 1";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand2_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 2";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand3_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 3";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand4_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 4";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand5_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 5";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand6_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 6";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand7_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 7";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand8_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 8";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand9_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 9";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }

        private void kryptonCommand10_Execute(object sender, EventArgs e)
        {
            DrpBtnPath.Text = "Path 10";
            ScrlStorageDefine_ValueChanged();
            btnSaveLoc.Enabled = true;
            btnClearLoc.Enabled = true;
        }
    }
}
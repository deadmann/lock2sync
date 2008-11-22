using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;

namespace Lock_To_Sync
{
    public partial class DLTS : Form
    {
        
        public DLTS()
        {
            InitializeComponent();
        }
        //to have public access to Setting1 form
        public Setting Setting1 = new Setting();//ver3.1.0.0



        String _dir = Directory.GetCurrentDirectory()+"\\image.jpg";//ver3.1.0.0
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
                this.Height = /*form size*/225 - /*label line size*/13 + (/*label line size*/13 */*count of line*/ _AddressLines);
            }
            if (_AddressLines > 1)
            {
                btnLessInfo.Visible = true;
                this.btnLessInfo.Top = /*button height of GroupForm*/48 - /*label line size (2X)*/26 + (/*label line size*/13 * _AddressLines);
                this.Height = /*form size*/225 - /*label line size*/13 + (/*label line size*/13 * /*count of line*/_AddressLines);
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
                this.BackgroundImage = _bitmap;
            }
                //transparenty
            if (Lock_To_Sync.Properties.Settings.Default.Transparent == "0")
            {
                groupBox1.BackColor = GroupBox.DefaultBackColor;
            }
            else
            {
                if (Lock_To_Sync.Properties.Settings.Default.Transparent == "1")
                {
                    groupBox1.BackColor = Color.Transparent;
                }
            }
            //Pic Zoom
            switch (Properties.Settings.Default.SkinPicSizeMode)
            {
                case "1":
                    this.BackgroundImageLayout = ImageLayout.Tile;
                    break;
                case "2":
                    this.BackgroundImageLayout = ImageLayout.Center;
                    break;
                case "3":
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case "4":
                    this.BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                default:
                    this.BackgroundImageLayout = ImageLayout.Tile;
                    Properties.Settings.Default.SkinPicSizeMode = "1";
                    Properties.Settings.Default.Save();
                    break;
            }
        }
        //Button for showing About dialog
        private void btnAbout_Click(object sender, EventArgs e)
        {
            //Open About Box
            About _AboutDialog = new About();
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
            Setting1 = new Setting();
            Setting1._dir = _dir;
            Setting1.Show(this);
        }
        //in end of program
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnLock.Enabled == true && Properties.Settings.Default.SecurityQuestion == "1")
            {
                DialogResult _result = new DialogResult();
                _result = MessageBox.Show("Do you wanna Lock this folder before close program ?\n (" + lblAddress.Text + ") ", "Security Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button3);
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
                            string _AA=Setting1.pictureBox1.ImageLocation;//new
                            Setting1.pictureBox1.ImageLocation = "";//new
                            Setting1.pictureBox1.Image = null;//new
                            this.BackgroundImage = null;//new
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
                        this.BackgroundImage = _bitmap;
                    }
                    else
                    {
                        this.BackgroundImage = null;
                    }
                }
                catch
                {
                    this.BackgroundImage = null;
                }
                if (Lock_To_Sync.Properties.Settings.Default.Transparent == "0")
                {
                    groupBox1.BackColor = GroupBox.DefaultBackColor;
                }
                else
                {
                    if (Lock_To_Sync.Properties.Settings.Default.Transparent == "1")
                    {
                        groupBox1.BackColor = Color.Transparent;
                    }
                }
                //Pic Zoom
                switch (Properties.Settings.Default.SkinPicSizeMode)
                {
                    case "1":
                        this.BackgroundImageLayout = ImageLayout.Tile;
                        break;
                    case "2":
                        this.BackgroundImageLayout = ImageLayout.Center;
                        break;
                    case "3":
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case "4":
                        this.BackgroundImageLayout = ImageLayout.Zoom;
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
            switch (ScrlStorageDefine.Value)
            {
                case 1:
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
                case 2:
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
                case 3:
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
                case 4:
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
                case 5:
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
                case 6:
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
                case 7:
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
                case 8:
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
                case 9:
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
                case 10:
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
            if (Directory.Exists(strFolderPath))
            {
                switch (ScrlStorageDefine.Value)
                {
                    case 1:
                        Properties.Settings.Default.Path1 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 2:
                        Properties.Settings.Default.Path2 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 3:
                        Properties.Settings.Default.Path3 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 4:
                        Properties.Settings.Default.Path4 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 5:
                        Properties.Settings.Default.Path5 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 6:
                        Properties.Settings.Default.Path6 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 7:
                        Properties.Settings.Default.Path7 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 8:
                        Properties.Settings.Default.Path8 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 9:
                        Properties.Settings.Default.Path9 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                    case 10:
                        Properties.Settings.Default.Path10 = strFolderPath;
                        Properties.Settings.Default.Save();
                        break;
                }
                btnLoadLoc.Enabled = true;
                panel1.BackColor = Color.Green;
            }
        }

        private void btnClearLoc_Click(object sender, EventArgs e)
        {
            switch (ScrlStorageDefine.Value)
            {
                case 1:
                    Properties.Settings.Default.Path1 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 2:
                    Properties.Settings.Default.Path2 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 3:
                    Properties.Settings.Default.Path3 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 4:
                    Properties.Settings.Default.Path4 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 5:
                    Properties.Settings.Default.Path5 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 6:
                    Properties.Settings.Default.Path6 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 7:
                    Properties.Settings.Default.Path7 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 8:
                    Properties.Settings.Default.Path8 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 9:
                    Properties.Settings.Default.Path9 = "";
                    Properties.Settings.Default.Save();
                    break;
                case 10:
                    Properties.Settings.Default.Path10 = "";
                    Properties.Settings.Default.Save();
                    break;
            }
            panel1.BackColor = Color.Yellow;
        }

        private void ScrlStorageDefine_ValueChanged(object sender, EventArgs e)
        {
            switch (ScrlStorageDefine.Value)
            {
                case 1:
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
                case 2:
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
                case 3:
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
                case 4:
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
                case 5:
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
                case 6:
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
                case 7:
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
                case 8:
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
                case 9:
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
                case 10:
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
            string strPath="";
            int pathNum=0;
            switch (ScrlStorageDefine.Value)
            {
                case 1:
                    strPath = Properties.Settings.Default.Path1;
                    pathNum = 1;
                    break;
                case 2:
                    strPath = Properties.Settings.Default.Path2;
                    pathNum = 2;
                    break;
                case 3:
                    strPath = Properties.Settings.Default.Path3;
                    pathNum = 3;
                    break;
                case 4:
                    strPath = Properties.Settings.Default.Path4;
                    pathNum = 4;
                    break;
                case 5:
                    strPath = Properties.Settings.Default.Path5;
                    pathNum = 5;
                    break;
                case 6:
                    strPath = Properties.Settings.Default.Path6;
                    pathNum = 6;
                    break;
                case 7:
                    strPath = Properties.Settings.Default.Path7;
                    pathNum = 7;
                    break;
                case 8:
                    strPath = Properties.Settings.Default.Path8;
                    pathNum = 8;
                    break;
                case 9:
                    strPath = Properties.Settings.Default.Path9;
                    pathNum = 9;
                    break;
                case 10:
                    strPath = Properties.Settings.Default.Path10;
                    pathNum = 10;
                    break;
            }
            if (strPath == "")
            {
                _tooltip.Show("\nPath number " + pathNum + " is empty", panel1);
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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.IO;//ver3.1.0.0
using ComponentFactory.Krypton.Toolkit;

namespace Lock_To_Sync
{
    public partial class KryptonSetting : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public KryptonSetting()
        {
            InitializeComponent();
        }
        public bool _ok = false;//ver3.1.0.0

        public String _dir = Directory.GetCurrentDirectory() + "\\image.jpg";//ver3.1.0.0
        /*ver3.0.0.0
        System.Drawing.Bitmap _bitmap = new Bitmap(Lock_To_Sync.Properties.Resources._Image);
         */
        public String ImgStatus;
        private void Setting_Load(object sender, EventArgs e)
        {
            //Set Setting form :

            //Active username and password and after that setting
            if (Properties.Settings.Default.Active == "1")
            {
                txtSetPass.Text = Properties.Settings.Default.Pass;
                txtSetUser.Text = Properties.Settings.Default.User;
                chkPass.Checked = true;
                panel1.Enabled = true;
            }
            else
            {
                txtSetPass.Text = "";
                txtSetUser.Text = "";
                chkPass.Checked = false;
                panel1.Enabled = false;
            }
            //Set Skin
            chkTransparent.Checked = (Lock_To_Sync.Properties.Settings.Default.Transparent == "1") ? true : false;
            switch (Lock_To_Sync.Properties.Settings.Default.SkinPicSizeMode)
            {
                case "1":
                    rdoPic1.Checked = true;
                    rdoPic2.Checked = false;
                    rdoPic3.Checked = false;
                    rdoPic4.Checked = false;
                    break;
                case "2":
                    rdoPic1.Checked = false;
                    rdoPic2.Checked = true;
                    rdoPic3.Checked = false;
                    rdoPic4.Checked = false;
                    break;
                case "3":
                    rdoPic1.Checked = false;
                    rdoPic2.Checked = false;
                    rdoPic3.Checked = true;
                    rdoPic4.Checked = false;
                    break;
                case "4":
                    rdoPic1.Checked = false;
                    rdoPic2.Checked = false;
                    rdoPic3.Checked = false;
                    rdoPic4.Checked = true;
                    break;
            }
            if (Lock_To_Sync.Properties.Settings.Default.Image != "")
            {
                pictureBox1.ImageLocation = _dir;//ver3.1.0.0
                /*ver3.0.0.0
                _bitmap = (System.Drawing.Bitmap)Lock_To_Sync.Properties.Settings.Default.Imagef;
                pictureBox1.Image = _bitmap;
                */
                ImgStatus = "Loaded";
            }
            else
            {
                pictureBox1.Image = null;
                /*ver3.0.0.0
                _bitmap = null;
                 */
                ImgStatus = "";
            }
            if (Properties.Settings.Default.KryptonTheme == "" || Properties.Settings.Default.KryptonTheme == null)
            {
                CBoxVirtualTheme.SelectedItem = "Light-Blue";
            }
            else
            {
                CBoxVirtualTheme.SelectedItem = Properties.Settings.Default.KryptonTheme;
            }

            //Set other option
            chkSecQuest.Checked = (Lock_To_Sync.Properties.Settings.Default.SecurityQuestion == "1") ? true : false;
            chkKeepAdd.Checked = (Lock_To_Sync.Properties.Settings.Default.KeepLastAddress == "1") ? true : false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Set App Data :

            //Set Skin
            try
            {
                //Transparent (Data & MainForm)
                if (chkTransparent.Checked == true)
                {
                    Lock_To_Sync.Properties.Settings.Default.Transparent = "1";
                }
                else
                {
                    Lock_To_Sync.Properties.Settings.Default.Transparent = "0";
                }
                //Picture Zoom Mode Option (Data & MainForm)
                if (rdoPic1.Checked == true)
                    Properties.Settings.Default.SkinPicSizeMode = "1";
                else if (rdoPic2.Checked == true)
                    Properties.Settings.Default.SkinPicSizeMode = "2";
                else if (rdoPic3.Checked == true)
                    Properties.Settings.Default.SkinPicSizeMode = "3";
                else if (rdoPic4.Checked == true)
                    Properties.Settings.Default.SkinPicSizeMode = "4";
                //Image (Data & MainForm)
                Lock_To_Sync.Properties.Settings.Default.Image = ImgStatus;
                /*ver3.0.0.0
                if (ImgStatus != "")
                {
                    if (pictureBox1.ImageLocation != _dir)//ver 3.1.0.0(first config)
                    {
                        File.Copy(pictureBox1.ImageLocation, _dir, true);
                    }
                    /*ver3.0.0.0
                    Lock_To_Sync.Properties.Settings.Default.Imagef = _bitmap;
                    * /
                    Lock_To_Sync.Properties.Settings.Default.Image = "Loaded";
                     
                }
                else
                {
                    Lock_To_Sync.Properties.Settings.Default.Image = "";
                }
                */
            }
            catch
            {
                /*ver3.0.0.0 -- Moved to main form
                MessageBox.Show("Can't show image in main form", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                 */
            }
            Properties.Settings.Default.KryptonTheme = CBoxVirtualTheme.SelectedItem.ToString();

            //Set activity and username and password
            if (chkPass.Checked == true)
            {
                //User set password and username or they are empty
                if ((txtSetPass.Text != "" && txtSetPass != null) && (txtSetUser.Text != "" && txtSetUser != null))
                {
                    //If password setted on before don't ask for verify
                    if (String.Compare(txtSetPass.Text, Properties.Settings.Default.Pass, true) != 0)
                    {
                        KryptonInput InputVerify = new KryptonInput();
                        InputVerify.ShowDialog(this);
                        //If password verify was true or faild then ...
                        if (String.Compare(Properties.Settings.Default.PasswordVerify, txtSetPass.Text, true) == 0)
                        {
                            Properties.Settings.Default.User = txtSetUser.Text;
                            Properties.Settings.Default.Pass = txtSetPass.Text;
                            Properties.Settings.Default.Active = "1";
                        }
                        else
                        {
                            MessageBox.Show("Error in password verify ,check it again", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You didn't set any username or password! Program is unable to accept none info!", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                Properties.Settings.Default.Active = "0";
                Properties.Settings.Default.User = "";
                Properties.Settings.Default.Pass = "";
            }
            //Set other option
            Properties.Settings.Default.SecurityQuestion = (chkSecQuest.Checked == true) ? "1" : "0";
            Properties.Settings.Default.KeepLastAddress = (chkSecQuest.Checked == true) ? "1" : "0";
            //Save setting
            Properties.Settings.Default.Save();
            _ok = true;//ver3.1.0.0
            //Close setting form
            this.Close();
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = (chkPass.Checked == true) ? true : false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            //theme back to normal
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.JPG(*.JPG,*.JPE,*.JPEG)|*.jpeg;*.jpg;*.jpe;)";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Choose picture as background";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    pictureBox1.ImageLocation = openFileDialog1.FileName.ToString();
                    /*ver3.0.0.0
                    System.Drawing.Bitmap _a = new Bitmap(openFileDialog1.FileName);
                    _bitmap = _a;
                    */
                    ImgStatus = "Loaded";
                    pictureBox1.Refresh();
                }
                catch
                {
                    MessageBox.Show("Can't load image", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUnloadImage_Click(object sender, EventArgs e)
        {
            ImgStatus = "";
            pictureBox1.Image = null;
        }

        private void btnRestor_Click(object sender, EventArgs e)
        {
            ImgStatus = Lock_To_Sync.Properties.Settings.Default.Image;
            /*ver3.0.0.0
            pictureBox1.Image = (System.Drawing.Bitmap)Lock_To_Sync.Properties.Settings.Default.Imagef;
            */
            if (ImgStatus != "")//ver3.1.0.0
            {
                pictureBox1.ImageLocation = _dir;
            }
            else
            {
                pictureBox1.Image = null;
            }
            pictureBox1.Refresh();
        }

        private void btnSkin_Click(object sender, EventArgs e)
        {
            btnSkin.Checked = true;
            btnSecurity.Checked = false;
            btnSkin.BringToFront();
            btnSecurity.SendToBack();

            kryptonGroup1.Visible = true;
            kryptonGroup2.Visible = false;
        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            btnSecurity.Checked = true;
            btnSkin.Checked = false;
            btnSecurity.BringToFront();
            btnSkin.SendToBack();

            kryptonGroup1.Visible = false;
            kryptonGroup2.Visible = true;
        }

        private void CBoxVirtualTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBoxVirtualTheme.SelectedIndex)
            {
                case 0:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case 1:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case 2:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case 3:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case 4:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                case 5:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
                    break;
                case 6:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                default:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
            }
        }
    }
}
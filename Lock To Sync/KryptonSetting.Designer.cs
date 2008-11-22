namespace Lock_To_Sync
{
    partial class KryptonSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KryptonSetting));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnSkin = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.CBoxVirtualTheme = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.rdoPic4 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rdoPic2 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rdoPic3 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rdoPic1 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.btnRestor = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnUnloadImage = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnBrowse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkTransparent = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnSecurity = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonButton5 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton4 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroup2 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.panel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.txtSetPass = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSetUser = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.chkPass = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkKeepAdd = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSecQuest = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).BeginInit();
            this.kryptonGroup2.Panel.SuspendLayout();
            this.kryptonGroup2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnSkin);
            this.kryptonPanel.Controls.Add(this.kryptonGroup1);
            this.kryptonPanel.Controls.Add(this.btnSecurity);
            this.kryptonPanel.Controls.Add(this.kryptonButton5);
            this.kryptonPanel.Controls.Add(this.kryptonButton4);
            this.kryptonPanel.Controls.Add(this.kryptonGroup2);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(233, 310);
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnSkin
            // 
            this.btnSkin.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Alternate;
            this.btnSkin.Checked = true;
            this.btnSkin.Location = new System.Drawing.Point(12, 12);
            this.btnSkin.Name = "btnSkin";
            this.btnSkin.Size = new System.Drawing.Size(45, 25);
            this.btnSkin.TabIndex = 3;
            this.btnSkin.Text = "Skin";
            this.btnSkin.Values.ExtraText = "";
            this.btnSkin.Values.Image = null;
            this.btnSkin.Values.Text = "Skin";
            this.btnSkin.Click += new System.EventHandler(this.btnSkin_Click);
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroup1.Location = new System.Drawing.Point(12, 37);
            this.kryptonGroup1.Name = "kryptonGroup1";
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.CBoxVirtualTheme);
            this.kryptonGroup1.Panel.Controls.Add(this.rdoPic4);
            this.kryptonGroup1.Panel.Controls.Add(this.rdoPic2);
            this.kryptonGroup1.Panel.Controls.Add(this.rdoPic3);
            this.kryptonGroup1.Panel.Controls.Add(this.rdoPic1);
            this.kryptonGroup1.Panel.Controls.Add(this.btnRestor);
            this.kryptonGroup1.Panel.Controls.Add(this.btnUnloadImage);
            this.kryptonGroup1.Panel.Controls.Add(this.btnBrowse);
            this.kryptonGroup1.Panel.Controls.Add(this.pictureBox1);
            this.kryptonGroup1.Panel.Controls.Add(this.label1);
            this.kryptonGroup1.Panel.Controls.Add(this.chkTransparent);
            this.kryptonGroup1.Size = new System.Drawing.Size(206, 239);
            this.kryptonGroup1.TabIndex = 5;
            // 
            // CBoxVirtualTheme
            // 
            this.CBoxVirtualTheme.DropDownWidth = 121;
            this.CBoxVirtualTheme.FormattingEnabled = false;
            this.CBoxVirtualTheme.Items.AddRange(new object[] {
            "Light-Blue",
            "Light-Silver",
            "Light-Black",
            "Dark-Blue",
            "Dark-Orange",
            "Dark-Purple",
            "Simple"});
            this.CBoxVirtualTheme.Location = new System.Drawing.Point(7, 205);
            this.CBoxVirtualTheme.Name = "CBoxVirtualTheme";
            this.CBoxVirtualTheme.Size = new System.Drawing.Size(121, 22);
            this.CBoxVirtualTheme.TabIndex = 33;
            this.CBoxVirtualTheme.SelectedIndexChanged += new System.EventHandler(this.CBoxVirtualTheme_SelectedIndexChanged);
            // 
            // rdoPic4
            // 
            this.rdoPic4.Location = new System.Drawing.Point(105, 179);
            this.rdoPic4.Name = "rdoPic4";
            this.rdoPic4.Size = new System.Drawing.Size(52, 19);
            this.rdoPic4.TabIndex = 32;
            this.rdoPic4.Text = "Zoom";
            this.rdoPic4.Values.ExtraText = "";
            this.rdoPic4.Values.Image = null;
            this.rdoPic4.Values.Text = "Zoom";
            // 
            // rdoPic2
            // 
            this.rdoPic2.Location = new System.Drawing.Point(105, 154);
            this.rdoPic2.Name = "rdoPic2";
            this.rdoPic2.Size = new System.Drawing.Size(55, 19);
            this.rdoPic2.TabIndex = 31;
            this.rdoPic2.Text = "Center";
            this.rdoPic2.Values.ExtraText = "";
            this.rdoPic2.Values.Image = null;
            this.rdoPic2.Values.Text = "Center";
            // 
            // rdoPic3
            // 
            this.rdoPic3.Location = new System.Drawing.Point(4, 179);
            this.rdoPic3.Name = "rdoPic3";
            this.rdoPic3.Size = new System.Drawing.Size(57, 19);
            this.rdoPic3.TabIndex = 30;
            this.rdoPic3.Text = "Stretch";
            this.rdoPic3.Values.ExtraText = "";
            this.rdoPic3.Values.Image = null;
            this.rdoPic3.Values.Text = "Stretch";
            // 
            // rdoPic1
            // 
            this.rdoPic1.Location = new System.Drawing.Point(4, 154);
            this.rdoPic1.Name = "rdoPic1";
            this.rdoPic1.Size = new System.Drawing.Size(40, 19);
            this.rdoPic1.TabIndex = 29;
            this.rdoPic1.Text = "Tile";
            this.rdoPic1.Values.ExtraText = "";
            this.rdoPic1.Values.Image = null;
            this.rdoPic1.Values.Text = "Tile";
            // 
            // btnRestor
            // 
            this.btnRestor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestor.Location = new System.Drawing.Point(110, 123);
            this.btnRestor.Name = "btnRestor";
            this.btnRestor.Size = new System.Drawing.Size(90, 25);
            this.btnRestor.TabIndex = 28;
            this.btnRestor.Text = "&Restor";
            this.btnRestor.Values.ExtraText = "";
            this.btnRestor.Values.Image = null;
            this.btnRestor.Values.ImageStates.ImageCheckedNormal = null;
            this.btnRestor.Values.ImageStates.ImageCheckedPressed = null;
            this.btnRestor.Values.ImageStates.ImageCheckedTracking = null;
            this.btnRestor.Values.Text = "&Restor";
            this.btnRestor.Click += new System.EventHandler(this.btnRestor_Click);
            // 
            // btnUnloadImage
            // 
            this.btnUnloadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnloadImage.Location = new System.Drawing.Point(110, 88);
            this.btnUnloadImage.Name = "btnUnloadImage";
            this.btnUnloadImage.Size = new System.Drawing.Size(90, 25);
            this.btnUnloadImage.TabIndex = 27;
            this.btnUnloadImage.Text = "&Unload";
            this.btnUnloadImage.Values.ExtraText = "";
            this.btnUnloadImage.Values.Image = null;
            this.btnUnloadImage.Values.ImageStates.ImageCheckedNormal = null;
            this.btnUnloadImage.Values.ImageStates.ImageCheckedPressed = null;
            this.btnUnloadImage.Values.ImageStates.ImageCheckedTracking = null;
            this.btnUnloadImage.Values.Text = "&Unload";
            this.btnUnloadImage.Click += new System.EventHandler(this.btnUnloadImage_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(110, 54);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 25);
            this.btnBrowse.TabIndex = 26;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.Values.ExtraText = "";
            this.btnBrowse.Values.Image = null;
            this.btnBrowse.Values.ImageStates.ImageCheckedNormal = null;
            this.btnBrowse.Values.ImageStates.ImageCheckedPressed = null;
            this.btnBrowse.Values.ImageStates.ImageCheckedTracking = null;
            this.btnBrowse.Values.Text = "&Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "Background Image";
            this.label1.Values.ExtraText = "";
            this.label1.Values.Image = null;
            this.label1.Values.Text = "Background Image";
            // 
            // chkTransparent
            // 
            this.chkTransparent.Location = new System.Drawing.Point(4, 4);
            this.chkTransparent.Name = "chkTransparent";
            this.chkTransparent.Size = new System.Drawing.Size(162, 19);
            this.chkTransparent.TabIndex = 23;
            this.chkTransparent.Text = "Transparent for Info Section";
            this.chkTransparent.Values.ExtraText = "";
            this.chkTransparent.Values.Image = null;
            this.chkTransparent.Values.Text = "Transparent for Info Section";
            // 
            // btnSecurity
            // 
            this.btnSecurity.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Alternate;
            this.btnSecurity.Location = new System.Drawing.Point(53, 12);
            this.btnSecurity.Name = "btnSecurity";
            this.btnSecurity.Size = new System.Drawing.Size(64, 25);
            this.btnSecurity.TabIndex = 4;
            this.btnSecurity.Text = "Security";
            this.btnSecurity.Values.ExtraText = "";
            this.btnSecurity.Values.Image = null;
            this.btnSecurity.Values.Text = "Security";
            this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
            // 
            // kryptonButton5
            // 
            this.kryptonButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton5.Location = new System.Drawing.Point(131, 282);
            this.kryptonButton5.Name = "kryptonButton5";
            this.kryptonButton5.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton5.TabIndex = 2;
            this.kryptonButton5.Text = "&Cancel";
            this.kryptonButton5.Values.ExtraText = "";
            this.kryptonButton5.Values.Image = null;
            this.kryptonButton5.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton5.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton5.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton5.Values.Text = "&Cancel";
            this.kryptonButton5.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // kryptonButton4
            // 
            this.kryptonButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonButton4.Location = new System.Drawing.Point(12, 282);
            this.kryptonButton4.Name = "kryptonButton4";
            this.kryptonButton4.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton4.TabIndex = 1;
            this.kryptonButton4.Text = "&Ok";
            this.kryptonButton4.Values.ExtraText = "";
            this.kryptonButton4.Values.Image = null;
            this.kryptonButton4.Values.ImageStates.ImageCheckedNormal = null;
            this.kryptonButton4.Values.ImageStates.ImageCheckedPressed = null;
            this.kryptonButton4.Values.ImageStates.ImageCheckedTracking = null;
            this.kryptonButton4.Values.Text = "&Ok";
            this.kryptonButton4.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // kryptonGroup2
            // 
            this.kryptonGroup2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroup2.Location = new System.Drawing.Point(12, 37);
            this.kryptonGroup2.Name = "kryptonGroup2";
            // 
            // kryptonGroup2.Panel
            // 
            this.kryptonGroup2.Panel.Controls.Add(this.panel1);
            this.kryptonGroup2.Panel.Controls.Add(this.chkPass);
            this.kryptonGroup2.Panel.Controls.Add(this.chkKeepAdd);
            this.kryptonGroup2.Panel.Controls.Add(this.chkSecQuest);
            this.kryptonGroup2.Size = new System.Drawing.Size(206, 239);
            this.kryptonGroup2.TabIndex = 6;
            this.kryptonGroup2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSetPass);
            this.panel1.Controls.Add(this.txtSetUser);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 53);
            this.panel1.TabIndex = 7;
            // 
            // txtSetPass
            // 
            this.txtSetPass.Location = new System.Drawing.Point(72, 28);
            this.txtSetPass.Name = "txtSetPass";
            this.txtSetPass.PasswordChar = '*';
            this.txtSetPass.Size = new System.Drawing.Size(123, 22);
            this.txtSetPass.TabIndex = 3;
            // 
            // txtSetUser
            // 
            this.txtSetUser.Location = new System.Drawing.Point(72, 3);
            this.txtSetUser.Name = "txtSetUser";
            this.txtSetUser.Size = new System.Drawing.Size(123, 22);
            this.txtSetUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            this.label3.Values.ExtraText = "";
            this.label3.Values.Image = null;
            this.label3.Values.Text = "Password";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName";
            this.label2.Values.ExtraText = "";
            this.label2.Values.Image = null;
            this.label2.Values.Text = "UserName";
            // 
            // chkPass
            // 
            this.chkPass.Location = new System.Drawing.Point(3, 53);
            this.chkPass.Name = "chkPass";
            this.chkPass.Size = new System.Drawing.Size(104, 19);
            this.chkPass.TabIndex = 6;
            this.chkPass.Text = "Active Password";
            this.chkPass.Values.ExtraText = "";
            this.chkPass.Values.Image = null;
            this.chkPass.Values.Text = "Active Password";
            this.chkPass.CheckedChanged += new System.EventHandler(this.chkPass_CheckedChanged);
            // 
            // chkKeepAdd
            // 
            this.chkKeepAdd.Location = new System.Drawing.Point(3, 28);
            this.chkKeepAdd.Name = "chkKeepAdd";
            this.chkKeepAdd.Size = new System.Drawing.Size(163, 19);
            this.chkKeepAdd.TabIndex = 5;
            this.chkKeepAdd.Text = "Keep Last Directory Address";
            this.chkKeepAdd.Values.ExtraText = "";
            this.chkKeepAdd.Values.Image = null;
            this.chkKeepAdd.Values.Text = "Keep Last Directory Address";
            // 
            // chkSecQuest
            // 
            this.chkSecQuest.Location = new System.Drawing.Point(3, 3);
            this.chkSecQuest.Name = "chkSecQuest";
            this.chkSecQuest.Size = new System.Drawing.Size(145, 19);
            this.chkSecQuest.TabIndex = 4;
            this.chkSecQuest.Text = "Active Security Question";
            this.chkSecQuest.Values.ExtraText = "";
            this.chkSecQuest.Values.Image = null;
            this.chkSecQuest.Values.Text = "Active Security Question";
            // 
            // KryptonSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 310);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2.Panel)).EndInit();
            this.kryptonGroup2.Panel.ResumeLayout(false);
            this.kryptonGroup2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup2)).EndInit();
            this.kryptonGroup2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton5;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton btnSecurity;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton btnSkin;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSetPass;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSetUser;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkPass;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkKeepAdd;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSecQuest;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rdoPic4;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rdoPic2;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rdoPic3;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rdoPic1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRestor;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUnloadImage;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowse;
        public System.Windows.Forms.PictureBox pictureBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel label1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkTransparent;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox CBoxVirtualTheme;
    }
}


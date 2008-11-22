namespace Lock_To_Sync
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkKeepAdd = new System.Windows.Forms.CheckBox();
            this.chkSecQuest = new System.Windows.Forms.CheckBox();
            this.chkPass = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSetPass = new System.Windows.Forms.TextBox();
            this.txtSetUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoPic4 = new System.Windows.Forms.RadioButton();
            this.rdoPic3 = new System.Windows.Forms.RadioButton();
            this.rdoPic2 = new System.Windows.Forms.RadioButton();
            this.rdoPic1 = new System.Windows.Forms.RadioButton();
            this.btnUnloadImage = new System.Windows.Forms.Button();
            this.btnRestor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTransparent = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkKeepAdd);
            this.groupBox1.Controls.Add(this.chkSecQuest);
            this.groupBox1.Controls.Add(this.chkPass);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(11, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 161);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Security";
            // 
            // chkKeepAdd
            // 
            this.chkKeepAdd.AutoSize = true;
            this.chkKeepAdd.Location = new System.Drawing.Point(6, 42);
            this.chkKeepAdd.Name = "chkKeepAdd";
            this.chkKeepAdd.Size = new System.Drawing.Size(160, 17);
            this.chkKeepAdd.TabIndex = 15;
            this.chkKeepAdd.Text = "Keep Last Directory Address";
            this.chkKeepAdd.UseVisualStyleBackColor = true;
            // 
            // chkSecQuest
            // 
            this.chkSecQuest.AutoSize = true;
            this.chkSecQuest.Location = new System.Drawing.Point(6, 19);
            this.chkSecQuest.Name = "chkSecQuest";
            this.chkSecQuest.Size = new System.Drawing.Size(142, 17);
            this.chkSecQuest.TabIndex = 13;
            this.chkSecQuest.Text = "Active Security Question";
            this.chkSecQuest.UseVisualStyleBackColor = true;
            // 
            // chkPass
            // 
            this.chkPass.AutoSize = true;
            this.chkPass.Location = new System.Drawing.Point(6, 65);
            this.chkPass.Name = "chkPass";
            this.chkPass.Size = new System.Drawing.Size(105, 17);
            this.chkPass.TabIndex = 14;
            this.chkPass.Text = "Active Password";
            this.chkPass.UseVisualStyleBackColor = true;
            this.chkPass.CheckedChanged += new System.EventHandler(this.chkPass_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtSetPass);
            this.panel1.Controls.Add(this.txtSetUser);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(6, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 65);
            this.panel1.TabIndex = 10;
            // 
            // txtSetPass
            // 
            this.txtSetPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSetPass.Location = new System.Drawing.Point(80, 35);
            this.txtSetPass.Name = "txtSetPass";
            this.txtSetPass.PasswordChar = '*';
            this.txtSetPass.Size = new System.Drawing.Size(100, 20);
            this.txtSetPass.TabIndex = 1;
            // 
            // txtSetUser
            // 
            this.txtSetUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSetUser.Location = new System.Drawing.Point(80, 9);
            this.txtSetUser.Name = "txtSetUser";
            this.txtSetUser.Size = new System.Drawing.Size(100, 20);
            this.txtSetUser.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "UserName";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(133, 396);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(12, 396);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rdoPic4);
            this.groupBox2.Controls.Add(this.rdoPic3);
            this.groupBox2.Controls.Add(this.rdoPic2);
            this.groupBox2.Controls.Add(this.rdoPic1);
            this.groupBox2.Controls.Add(this.btnUnloadImage);
            this.groupBox2.Controls.Add(this.btnRestor);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkTransparent);
            this.groupBox2.Location = new System.Drawing.Point(11, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 211);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skin";
            // 
            // rdoPic4
            // 
            this.rdoPic4.AutoSize = true;
            this.rdoPic4.Location = new System.Drawing.Point(114, 188);
            this.rdoPic4.Name = "rdoPic4";
            this.rdoPic4.Size = new System.Drawing.Size(52, 17);
            this.rdoPic4.TabIndex = 18;
            this.rdoPic4.TabStop = true;
            this.rdoPic4.Text = "Zoom";
            this.rdoPic4.UseVisualStyleBackColor = true;
            // 
            // rdoPic3
            // 
            this.rdoPic3.AutoSize = true;
            this.rdoPic3.Location = new System.Drawing.Point(6, 188);
            this.rdoPic3.Name = "rdoPic3";
            this.rdoPic3.Size = new System.Drawing.Size(59, 17);
            this.rdoPic3.TabIndex = 17;
            this.rdoPic3.TabStop = true;
            this.rdoPic3.Text = "Stretch";
            this.rdoPic3.UseVisualStyleBackColor = true;
            // 
            // rdoPic2
            // 
            this.rdoPic2.AutoSize = true;
            this.rdoPic2.Location = new System.Drawing.Point(114, 165);
            this.rdoPic2.Name = "rdoPic2";
            this.rdoPic2.Size = new System.Drawing.Size(56, 17);
            this.rdoPic2.TabIndex = 16;
            this.rdoPic2.TabStop = true;
            this.rdoPic2.Text = "Center";
            this.rdoPic2.UseVisualStyleBackColor = true;
            // 
            // rdoPic1
            // 
            this.rdoPic1.AutoSize = true;
            this.rdoPic1.Location = new System.Drawing.Point(6, 165);
            this.rdoPic1.Name = "rdoPic1";
            this.rdoPic1.Size = new System.Drawing.Size(42, 17);
            this.rdoPic1.TabIndex = 15;
            this.rdoPic1.TabStop = true;
            this.rdoPic1.Text = "Tile";
            this.rdoPic1.UseVisualStyleBackColor = true;
            // 
            // btnUnloadImage
            // 
            this.btnUnloadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnloadImage.Location = new System.Drawing.Point(116, 100);
            this.btnUnloadImage.Name = "btnUnloadImage";
            this.btnUnloadImage.Size = new System.Drawing.Size(75, 23);
            this.btnUnloadImage.TabIndex = 12;
            this.btnUnloadImage.Text = "&Unload";
            this.btnUnloadImage.UseVisualStyleBackColor = true;
            this.btnUnloadImage.Click += new System.EventHandler(this.btnUnloadImage_Click);
            // 
            // btnRestor
            // 
            this.btnRestor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestor.Location = new System.Drawing.Point(116, 136);
            this.btnRestor.Name = "btnRestor";
            this.btnRestor.Size = new System.Drawing.Size(75, 23);
            this.btnRestor.TabIndex = 13;
            this.btnRestor.Text = "&Restor";
            this.btnRestor.UseVisualStyleBackColor = true;
            this.btnRestor.Click += new System.EventHandler(this.btnRestor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(116, 64);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "BackGround Image";
            // 
            // chkTransparent
            // 
            this.chkTransparent.AutoSize = true;
            this.chkTransparent.Location = new System.Drawing.Point(6, 19);
            this.chkTransparent.Name = "chkTransparent";
            this.chkTransparent.Size = new System.Drawing.Size(158, 17);
            this.chkTransparent.TabIndex = 9;
            this.chkTransparent.Text = "Transparent for Info Section";
            this.chkTransparent.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 431);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Setting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkKeepAdd;
        private System.Windows.Forms.CheckBox chkSecQuest;
        public System.Windows.Forms.CheckBox chkPass;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtSetPass;
        public System.Windows.Forms.TextBox txtSetUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUnloadImage;
        private System.Windows.Forms.Button btnRestor;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkTransparent;
        private System.Windows.Forms.RadioButton rdoPic4;
        private System.Windows.Forms.RadioButton rdoPic3;
        private System.Windows.Forms.RadioButton rdoPic2;
        private System.Windows.Forms.RadioButton rdoPic1;
        public System.Windows.Forms.PictureBox pictureBox1;
    }
}
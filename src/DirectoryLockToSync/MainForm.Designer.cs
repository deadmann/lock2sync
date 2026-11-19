namespace DirectoryLockToSync
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.groupBox1 = new Krypton.Toolkit.KryptonGroup();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.btnLessInfo = new Krypton.Toolkit.KryptonButton();
            this.btnMoreInfo = new Krypton.Toolkit.KryptonButton();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.btnClearLoc = new Krypton.Toolkit.KryptonButton();
            this.btnSaveLoc = new Krypton.Toolkit.KryptonButton();
            this.btnLoadLoc = new Krypton.Toolkit.KryptonButton();
            this.btnOpen = new Krypton.Toolkit.KryptonButton();
            this.DrpBtnPath = new Krypton.Toolkit.KryptonDropButton();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem2 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand2 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem3 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand3 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem4 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand4 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem5 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand5 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem6 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand6 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem7 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand7 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem8 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand8 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem9 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand9 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonContextMenuItem10 = new Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonCommand10 = new Krypton.Toolkit.KryptonCommand();
            this.btnUnload = new Krypton.Toolkit.KryptonButton();
            this.btnSetting = new Krypton.Toolkit.KryptonButton();
            this.btnUnlock = new Krypton.Toolkit.KryptonButton();
            this.btnBrowse = new Krypton.Toolkit.KryptonButton();
            this.btnLock = new Krypton.Toolkit.KryptonButton();
            this.btnAbout = new Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1.Panel)).BeginInit();
            this.groupBox1.Panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel
            //
            this.kryptonPanel.Controls.Add(this.groupBox1);
            this.kryptonPanel.Controls.Add(this.btnClearLoc);
            this.kryptonPanel.Controls.Add(this.btnSaveLoc);
            this.kryptonPanel.Controls.Add(this.btnLoadLoc);
            this.kryptonPanel.Controls.Add(this.btnOpen);
            this.kryptonPanel.Controls.Add(this.DrpBtnPath);
            this.kryptonPanel.Controls.Add(this.btnUnload);
            this.kryptonPanel.Controls.Add(this.btnSetting);
            this.kryptonPanel.Controls.Add(this.btnUnlock);
            this.kryptonPanel.Controls.Add(this.btnBrowse);
            this.kryptonPanel.Controls.Add(this.btnLock);
            this.kryptonPanel.Controls.Add(this.btnAbout);
            this.kryptonPanel.Controls.Add(this.panel1);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(326, 208);
            this.kryptonPanel.TabIndex = 0;
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(12, 137);
            this.groupBox1.Name = "groupBox1";
            //
            // groupBox1.Panel
            //
            this.groupBox1.Panel.Controls.Add(this.lblStatus);
            this.groupBox1.Panel.Controls.Add(this.lblAddress);
            this.groupBox1.Panel.Controls.Add(this.btnLessInfo);
            this.groupBox1.Panel.Controls.Add(this.btnMoreInfo);
            this.groupBox1.Panel.Controls.Add(this.label3);
            this.groupBox1.Panel.Controls.Add(this.label1);
            this.groupBox1.Size = new System.Drawing.Size(302, 65);
            this.groupBox1.TabIndex = 21;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.Crimson;
            this.lblStatus.Location = new System.Drawing.Point(59, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Nothing Loaded";
            //
            // lblAddress
            //
            this.lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = System.Drawing.Color.Crimson;
            this.lblAddress.Location = new System.Drawing.Point(59, 32);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(205, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "{Address}";
            //
            // btnLessInfo
            //
            this.btnLessInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLessInfo.Location = new System.Drawing.Point(270, 48);
            this.btnLessInfo.Name = "btnLessInfo";
            this.btnLessInfo.Size = new System.Drawing.Size(27, 25);
            this.btnLessInfo.TabIndex = 5;
            this.btnLessInfo.Values.Text = "︽";
            this.btnLessInfo.Click += new System.EventHandler(this.btnLessInfo_Click);
            //
            // btnMoreInfo
            //
            this.btnMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreInfo.Enabled = false;
            this.btnMoreInfo.Location = new System.Drawing.Point(270, 22);
            this.btnMoreInfo.Name = "btnMoreInfo";
            this.btnMoreInfo.Size = new System.Drawing.Size(27, 25);
            this.btnMoreInfo.TabIndex = 4;
            this.btnMoreInfo.Values.Text = "︾";
            this.btnMoreInfo.Click += new System.EventHandler(this.btnMoreInfo_Click);
            //
            // label3
            //
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 19);
            this.label3.TabIndex = 1;
            this.label3.Values.Text = "Address";
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "Status";
            //
            // btnClearLoc
            //
            this.btnClearLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLoc.Enabled = false;
            this.btnClearLoc.Location = new System.Drawing.Point(166, 74);
            this.btnClearLoc.Name = "btnClearLoc";
            this.btnClearLoc.Size = new System.Drawing.Size(52, 25);
            this.btnClearLoc.TabIndex = 10;
            this.btnClearLoc.Values.Text = "Clear";
            this.btnClearLoc.Click += new System.EventHandler(this.btnClearLoc_Click);
            //
            // btnSaveLoc
            //
            this.btnSaveLoc.Enabled = false;
            this.btnSaveLoc.Location = new System.Drawing.Point(108, 74);
            this.btnSaveLoc.Name = "btnSaveLoc";
            this.btnSaveLoc.Size = new System.Drawing.Size(52, 25);
            this.btnSaveLoc.TabIndex = 9;
            this.btnSaveLoc.Values.Text = "Save";
            this.btnSaveLoc.Click += new System.EventHandler(this.btnSaveLoc_Click);
            //
            // btnLoadLoc
            //
            this.btnLoadLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadLoc.Enabled = false;
            this.btnLoadLoc.Location = new System.Drawing.Point(108, 43);
            this.btnLoadLoc.Name = "btnLoadLoc";
            this.btnLoadLoc.Size = new System.Drawing.Size(110, 25);
            this.btnLoadLoc.TabIndex = 8;
            this.btnLoadLoc.Values.Text = "Load Location";
            this.btnLoadLoc.Click += new System.EventHandler(this.btnLoadLoc_Click);
            //
            // btnOpen
            //
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(12, 105);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(302, 25);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Values.Text = "&Open Folder";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            //
            // DrpBtnPath
            //
            this.DrpBtnPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrpBtnPath.KryptonContextMenu = this.kryptonContextMenu1;
            this.DrpBtnPath.Location = new System.Drawing.Point(120, 12);
            this.DrpBtnPath.Name = "DrpBtnPath";
            this.DrpBtnPath.Size = new System.Drawing.Size(98, 25);
            this.DrpBtnPath.Splitter = false;
            this.DrpBtnPath.TabIndex = 7;
            this.DrpBtnPath.Values.Text = "Choose Path";
            //
            // kryptonContextMenu1
            //
            this.kryptonContextMenu1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            //
            // kryptonContextMenuItems1
            //
            this.kryptonContextMenuItems1.ImageColumn = false;
            this.kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem3,
            this.kryptonContextMenuItem4,
            this.kryptonContextMenuItem5,
            this.kryptonContextMenuItem6,
            this.kryptonContextMenuItem7,
            this.kryptonContextMenuItem8,
            this.kryptonContextMenuItem9,
            this.kryptonContextMenuItem10});
            //
            // kryptonContextMenuItem1
            //
            this.kryptonContextMenuItem1.KryptonCommand = this.kryptonCommand1;
            this.kryptonContextMenuItem1.Text = "Path 1";
            //
            // kryptonCommand1
            //
            this.kryptonCommand1.Text = "Path 1";
            this.kryptonCommand1.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem2
            //
            this.kryptonContextMenuItem2.KryptonCommand = this.kryptonCommand2;
            this.kryptonContextMenuItem2.Text = "Path 2";
            //
            // kryptonCommand2
            //
            this.kryptonCommand2.Text = "Path 2";
            this.kryptonCommand2.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem3
            //
            this.kryptonContextMenuItem3.KryptonCommand = this.kryptonCommand3;
            this.kryptonContextMenuItem3.Text = "Path 3";
            //
            // kryptonCommand3
            //
            this.kryptonCommand3.Text = "Path 3";
            this.kryptonCommand3.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem4
            //
            this.kryptonContextMenuItem4.KryptonCommand = this.kryptonCommand4;
            this.kryptonContextMenuItem4.Text = "Path 4";
            //
            // kryptonCommand4
            //
            this.kryptonCommand4.Text = "Path 4";
            this.kryptonCommand4.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem5
            //
            this.kryptonContextMenuItem5.KryptonCommand = this.kryptonCommand5;
            this.kryptonContextMenuItem5.Text = "Path 5";
            //
            // kryptonCommand5
            //
            this.kryptonCommand5.Text = "Path 5";
            this.kryptonCommand5.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem6
            //
            this.kryptonContextMenuItem6.KryptonCommand = this.kryptonCommand6;
            this.kryptonContextMenuItem6.Text = "Path 6";
            //
            // kryptonCommand6
            //
            this.kryptonCommand6.Text = "Path 6";
            this.kryptonCommand6.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem7
            //
            this.kryptonContextMenuItem7.KryptonCommand = this.kryptonCommand7;
            this.kryptonContextMenuItem7.Text = "Path 7";
            //
            // kryptonCommand7
            //
            this.kryptonCommand7.Text = "Path 7";
            this.kryptonCommand7.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem8
            //
            this.kryptonContextMenuItem8.KryptonCommand = this.kryptonCommand8;
            this.kryptonContextMenuItem8.Text = "Path 8";
            //
            // kryptonCommand8
            //
            this.kryptonCommand8.Text = "Path 8";
            this.kryptonCommand8.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem9
            //
            this.kryptonContextMenuItem9.KryptonCommand = this.kryptonCommand9;
            this.kryptonContextMenuItem9.Text = "Path 9";
            //
            // kryptonCommand9
            //
            this.kryptonCommand9.Text = "Path 9";
            this.kryptonCommand9.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // kryptonContextMenuItem10
            //
            this.kryptonContextMenuItem10.KryptonCommand = this.kryptonCommand10;
            this.kryptonContextMenuItem10.Text = "Path 10";
            //
            // kryptonCommand10
            //
            this.kryptonCommand10.Text = "Path 10";
            this.kryptonCommand10.Execute += new System.EventHandler(this.kryptonCommand_Execute);
            //
            // btnUnload
            //
            this.btnUnload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnload.Enabled = false;
            this.btnUnload.Location = new System.Drawing.Point(224, 74);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(90, 25);
            this.btnUnload.TabIndex = 5;
            this.btnUnload.Values.Text = "U&nload";
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            //
            // btnSetting
            //
            this.btnSetting.Location = new System.Drawing.Point(12, 74);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(90, 25);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Values.Text = "&Setting";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            //
            // btnUnlock
            //
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Enabled = false;
            this.btnUnlock.Location = new System.Drawing.Point(224, 43);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(90, 25);
            this.btnUnlock.TabIndex = 4;
            this.btnUnlock.Values.Text = "&Unlock";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            //
            // btnBrowse
            //
            this.btnBrowse.Location = new System.Drawing.Point(12, 43);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 25);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Values.Text = "&Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // btnLock
            //
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Enabled = false;
            this.btnLock.Location = new System.Drawing.Point(224, 12);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(90, 25);
            this.btnLock.TabIndex = 3;
            this.btnLock.Values.Text = "&Lock";
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            //
            // btnAbout
            //
            this.btnAbout.Location = new System.Drawing.Point(12, 12);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(90, 25);
            this.btnAbout.TabIndex = 0;
            this.btnAbout.Values.Text = "&About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(108, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(11, 25);
            this.panel1.TabIndex = 18;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            //
            // folderBrowserDialog1
            //
            this.folderBrowserDialog1.Description = "Please ,choose any folder must become lock or unlock";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(326, 208);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory Lock To Sync";
            this.TitleStyle = Krypton.Toolkit.KryptonFormTitleStyle.Classic;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1.Panel)).EndInit();
            this.groupBox1.Panel.ResumeLayout(false);
            this.groupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager;
        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.Panel panel1;
        private Krypton.Toolkit.KryptonButton btnLock;
        private Krypton.Toolkit.KryptonButton btnAbout;
        private Krypton.Toolkit.KryptonDropButton DrpBtnPath;
        private Krypton.Toolkit.KryptonButton btnUnload;
        private Krypton.Toolkit.KryptonButton btnSetting;
        private Krypton.Toolkit.KryptonButton btnUnlock;
        private Krypton.Toolkit.KryptonButton btnBrowse;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem4;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem5;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem6;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem7;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem8;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem9;
        private Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem10;
        private Krypton.Toolkit.KryptonButton btnOpen;
        private Krypton.Toolkit.KryptonButton btnClearLoc;
        private Krypton.Toolkit.KryptonButton btnSaveLoc;
        private Krypton.Toolkit.KryptonButton btnLoadLoc;
        private Krypton.Toolkit.KryptonCommand kryptonCommand1;
        private Krypton.Toolkit.KryptonGroup groupBox1;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonButton btnLessInfo;
        private Krypton.Toolkit.KryptonButton btnMoreInfo;
        private Krypton.Toolkit.KryptonCommand kryptonCommand2;
        private Krypton.Toolkit.KryptonCommand kryptonCommand3;
        private Krypton.Toolkit.KryptonCommand kryptonCommand4;
        private Krypton.Toolkit.KryptonCommand kryptonCommand5;
        private Krypton.Toolkit.KryptonCommand kryptonCommand6;
        private Krypton.Toolkit.KryptonCommand kryptonCommand7;
        private Krypton.Toolkit.KryptonCommand kryptonCommand8;
        private Krypton.Toolkit.KryptonCommand kryptonCommand9;
        private Krypton.Toolkit.KryptonCommand kryptonCommand10;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

namespace DirectoryLockToSync
{
    partial class LoginForm
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
            this.btnOk = new Krypton.Toolkit.KryptonButton();
            this.btnExit = new Krypton.Toolkit.KryptonButton();
            this.txtGetUser = new Krypton.Toolkit.KryptonTextBox();
            this.txtGetPass = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(16, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnOk.Size = new System.Drawing.Size(90, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Values.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.Ok_Click);
            //
            // btnExit
            //
            this.btnExit.Location = new System.Drawing.Point(112, 90);
            this.btnExit.Name = "btnExit";
            this.btnExit.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnExit.Size = new System.Drawing.Size(90, 25);
            this.btnExit.TabIndex = 3;
            this.btnExit.Values.Text = "E&xit";
            this.btnExit.Click += new System.EventHandler(this.Cancel_Click);
            //
            // txtGetUser
            //
            this.txtGetUser.Location = new System.Drawing.Point(79, 34);
            this.txtGetUser.Name = "txtGetUser";
            this.txtGetUser.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.txtGetUser.Size = new System.Drawing.Size(123, 22);
            this.txtGetUser.TabIndex = 0;
            this.txtGetUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGetUser_KeyPress);
            //
            // txtGetPass
            //
            this.txtGetPass.Location = new System.Drawing.Point(79, 62);
            this.txtGetPass.Name = "txtGetPass";
            this.txtGetPass.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.txtGetPass.PasswordChar = '*';
            this.txtGetPass.Size = new System.Drawing.Size(123, 22);
            this.txtGetPass.TabIndex = 1;
            this.txtGetPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGetPass_KeyPress);
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.label3);
            this.kryptonPanel1.Controls.Add(this.label2);
            this.kryptonPanel1.Controls.Add(this.label1);
            this.kryptonPanel1.Controls.Add(this.txtGetPass);
            this.kryptonPanel1.Controls.Add(this.txtGetUser);
            this.kryptonPanel1.Controls.Add(this.btnExit);
            this.kryptonPanel1.Controls.Add(this.btnOk);
            this.kryptonPanel1.Location = new System.Drawing.Point(-1, -1);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(213, 124);
            this.kryptonPanel1.TabIndex = 4;
            //
            // label3
            //
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 19);
            this.label3.TabIndex = 6;
            this.label3.Values.Text = "Password";
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(13, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 5;
            this.label2.Values.Text = "UserName";
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 19);
            this.label1.TabIndex = 4;
            this.label1.Values.Text = "Enter your UserName and Password";
            //
            // LoginForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(210, 121);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonButton btnOk;
        private Krypton.Toolkit.KryptonButton btnExit;
        private Krypton.Toolkit.KryptonTextBox txtGetUser;
        private Krypton.Toolkit.KryptonTextBox txtGetPass;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonLabel label2;
    }
}

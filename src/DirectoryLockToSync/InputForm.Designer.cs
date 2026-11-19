namespace DirectoryLockToSync
{
    partial class InputForm
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
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.btnOk = new Krypton.Toolkit.KryptonButton();
            this.txtVerify = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel
            //
            this.kryptonPanel.Controls.Add(this.btnOk);
            this.kryptonPanel.Controls.Add(this.txtVerify);
            this.kryptonPanel.Controls.Add(this.label1);
            this.kryptonPanel.Controls.Add(this.label2);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(274, 93);
            this.kryptonPanel.TabIndex = 0;
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(3, 64);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(267, 25);
            this.btnOk.TabIndex = 3;
            this.btnOk.Values.Text = "Check";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            //
            // txtVerify
            //
            this.txtVerify.Location = new System.Drawing.Point(59, 36);
            this.txtVerify.Name = "txtVerify";
            this.txtVerify.PasswordChar = '*';
            this.txtVerify.Size = new System.Drawing.Size(211, 22);
            this.txtVerify.TabIndex = 2;
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 19);
            this.label1.TabIndex = 1;
            this.label1.Values.Text = "Confirm";
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 19);
            this.label2.TabIndex = 0;
            this.label2.Values.Text = "For verify your password, enter that on here ,again .";
            //
            // InputForm
            //
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(274, 93);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Confirm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private Krypton.Toolkit.KryptonButton btnOk;
        private Krypton.Toolkit.KryptonTextBox txtVerify;
        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
    }
}

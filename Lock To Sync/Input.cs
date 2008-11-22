using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lock_To_Sync
{
    public partial class Input : Form
    {
        public Input()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Input_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PasswordVerify = txtVerify.Text;
        }

        private void txtVerify_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.Close();
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.Close();
        }

        private void Input_Load(object sender, EventArgs e)
        {

        }
    }
}
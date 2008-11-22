using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;


namespace Lock_To_Sync
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This function will check entry password is true or not
        /// </summary>
        private void CheckPassword()
        {
            if ((String.Compare(txtGetPass.Text, Lock_To_Sync.Properties.Settings.Default.Pass, true) == 0)
                && (String.Compare(txtGetUser.Text, Lock_To_Sync.Properties.Settings.Default.User, true) == 0))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Your password is wrong ,please try again !!!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtGetPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                CheckPassword();
        }

        private void txtGetUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                CheckPassword();
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                CheckPassword();
        }

    }
}
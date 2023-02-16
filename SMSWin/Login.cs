using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinSMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Enter UserName and Password!!");
            }
            else if (txtUserName.Text == "admin" && txtPassword.Text == "123")
            {
                MainMenu obj = new MainMenu();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect UserName and Password!!");
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

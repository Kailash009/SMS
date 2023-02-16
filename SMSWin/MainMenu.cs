using SMSWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinSMS
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void pbTeacher_Click(object sender, EventArgs e)
        {
            Teachers tch = new Teachers();
            tch.Show();
            this.Hide();
        }

        private void pbAttendance_Click(object sender, EventArgs e)
        {
            Attendances at = new Attendances();
            at.Show();
            this.Hide();
        }

        private void pbStudent_Click(object sender, EventArgs e)
        {
            Students st = new Students();
            st.Show();
            this.Hide();
        }

        private void pbEvents_Click(object sender, EventArgs e)
        {
            SchoolEvent se = new SchoolEvent();
            se.Show();
            this.Hide();
        }

        private void pbFees_Click(object sender, EventArgs e)
        {
            Fees fe = new Fees();
            fe.Show();
            this.Hide();
        }

        private void pbLogin_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void pbDashboard_Click(object sender, EventArgs e)
        {
            Dashboard db = new Dashboard();
            db.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

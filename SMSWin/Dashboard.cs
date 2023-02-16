using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace WinSMS
{
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public Dashboard()
        {
            InitializeComponent();
        }
        void countStudent()
        {
            string sql = "select count(*) from tbl_Student";
            SqlCommand cmd = new SqlCommand(sql,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblStudent.Text = dt.Rows[0][0].ToString();
            }
        }
        void countTeacher()
        {
            string sql = "select count(*) from tbl_Teacher";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblTeacher.Text = dt.Rows[0][0].ToString();
            }
        }
        void countEvent()
        {
            string sql = "select count(*) from tbl_Events";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblEvents.Text = dt.Rows[0][0].ToString();
            }
        }
        void totalFee()
        {
            string sql = "select sum(Amt) from tbl_Fees";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblFees.Text = dt.Rows[0][0].ToString();
            }
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            countStudent();
            countTeacher();
            countEvent();
            totalFee();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainMenu mu = new MainMenu();
            mu.Show();
            this.Hide();
        }

    }
}

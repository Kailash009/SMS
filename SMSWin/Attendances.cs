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
    public partial class Attendances : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public Attendances()
        {
            InitializeComponent();
            showAttendance();
            fillStudentID();
            getStudentName();
        }

        void showAttendance()
        {
            string sql = "select * from Tbl_Attendance";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgAttendance.DataSource = dt;
            }
        }
        private void fillStudentID()
        {
            string sql = "select sid from tbl_Student";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                cbStudentID.ValueMember = "sid";
                cbStudentID.DataSource = dt;
            }
        }
        private void getStudentName()
        {
            string sql = "select * from tbl_Student where sid=@sid";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@sid", cbStudentID.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStudentName.Text = dt.Rows[0]["stname"].ToString();
            }
        }

        private void cbStudentID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getStudentName();
        }
        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            addAttendance();
        }
        void addAttendance()
        {
            if (cbStudentID.SelectedIndex == -1 || txtStudentName.Text == "" || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Filled all Information");
            }
            else
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        string sql = "insert into Tbl_Attendance(AttStdID,AttStdName,AttDate,AttStatus)values(@AttStdID,@AttStdName,@AttDate,@AttStatus)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@AttStdID", cbStudentID.SelectedValue);
                        cmd.Parameters.AddWithValue("@AttStdName", txtStudentName.Text);
                        cmd.Parameters.AddWithValue("@AttDate", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@AttStatus", cbStatus.SelectedItem.ToString());
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Attendance Taken!!");
                            showAttendance();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Attendance Taken Failed!!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        int key = 0;
        private void dgAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbStudentID.SelectedValue = dgAttendance.Rows[e.RowIndex].Cells["AttStdID"].Value.ToString();
            txtStudentName.Text = dgAttendance.Rows[e.RowIndex].Cells["AttStdName"].Value.ToString();
            dtpDate.Text = DateTime.Parse(dgAttendance.Rows[e.RowIndex].Cells["AttDate"].Value.ToString()).ToString("yyyy/MM/dd");
            cbStatus.SelectedItem = dgAttendance.Rows[e.RowIndex].Cells["AttStatus"].Value.ToString();
            if (txtStudentName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dgAttendance.Rows[e.RowIndex].Cells["AttNum"].Value.ToString());
            }
        }
        private void btnEditAttendance_Click(object sender, EventArgs e)
        {
            updateAttendance();
        }
        void updateAttendance()
        {
            if (cbStudentID.SelectedIndex == -1 || txtStudentName.Text == "" || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        string sql = "update Tbl_Attendance set AttStdID=@AttStdID,AttStdName=@AttStdName,AttDate=@AttDate,AttStatus=@AttStatus where AttNum=@AttNum";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@AttStdID", cbStudentID.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@AttStdName", txtStudentName.Text);
                        cmd.Parameters.AddWithValue("@AttDate", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@AttStatus", cbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AttNum", key);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Attendance Updated SuccessFully!!");
                            showAttendance();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Attendance Updated Failed!!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            deleteAttendance();
        }
        void deleteAttendance()
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Student!");
            }
            else
            {
                try
                {
                    con.Open();
                    string sql = "Delete from Tbl_Attendance where AttStdID=@AttStdID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@AttStdID", key);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        MessageBox.Show("Attendance Deleted SuccessFully!!");
                        showAttendance();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Attendance Deleted Failed!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }
        private void Reset()
        {
            cbStudentID.SelectedIndex = 0;
            txtStudentName.Text = "";
            cbStatus.SelectedIndex = 0;
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

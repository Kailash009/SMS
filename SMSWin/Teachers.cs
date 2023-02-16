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
    public partial class Teachers : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public Teachers()
        {
            InitializeComponent();
            showAllTeacher();
        }
        void showAllTeacher()
        {
            string sql = "select * from tbl_Teacher";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgShowTeacher.DataSource = dt;
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            addStudent();
        }
        void addStudent()
        {
            if (txtUserName.Text == "" || txtMob.Text=="" || cbGender.SelectedIndex == -1 || txtAddress.Text == "" || cbSubjects.SelectedIndex == -1)
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
                        string sql = "insert into tbl_Teacher(Tname,TGen,TPhone,TSub,TAdd,TDOB)values(@Tname,@TGen,@TPhone,@TSub,@TAdd,@TDOB)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@Tname", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@TGen", cbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@TPhone", txtMob.Text);
                        cmd.Parameters.AddWithValue("@TSub", cbSubjects.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@TAdd", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@TDOB", dtpDOB.Value.Date);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Teacher Added SuccessFully!!");
                            showAllTeacher();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Teacher Added Failed!!");
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
        private void btnUpdateTeacher_Click(object sender, EventArgs e)
        {
            updateTeacher();
        }
        void updateTeacher()
        {
            if (txtUserName.Text == "" || cbGender.SelectedIndex == -1 || txtMob.Text == "" || txtAddress.Text == "" || cbSubjects.SelectedIndex == -1)
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
                        string sql = "update tbl_Teacher set Tname=@Tname,TGen=@TGen,TPhone=@TPhone,TSub=@TSub,TAdd=@TAdd,TDOB=@TDOB where Tid=@Tid";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@Tname", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@TGen", cbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@TPhone", txtMob.Text);
                        cmd.Parameters.AddWithValue("@TSub", cbSubjects.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@TAdd", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@TDOB", dtpDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@Tid",key);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Teacher Updated SuccessFully!!");
                            showAllTeacher();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Teacher Updated Failed!!");
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

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
            deleteStudent();
        }
        void deleteStudent()
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Teacher!");
            }
            else
            {
                try
                {
                    con.Open();
                    string sql = "Delete from tbl_Teacher where Tid=@Tid";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Tid", key);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        MessageBox.Show("Teacher Deleted SuccessFully!!");
                        showAllTeacher();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Teacher Deleted Failed!!");
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
            txtUserName.Text = "";
            txtAddress.Text = "";
            cbSubjects.SelectedIndex = 0;
            cbGender.SelectedIndex = 0;
            txtMob.Text = "";
        }

        int key = 0;
        private void dgShowTeacher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserName.Text = dgShowTeacher.Rows[e.RowIndex].Cells["Tname"].Value.ToString();
            cbGender.SelectedItem = dgShowTeacher.Rows[e.RowIndex].Cells["TGen"].Value.ToString();
            txtMob.Text = dgShowTeacher.Rows[e.RowIndex].Cells["TPhone"].Value.ToString();
            cbSubjects.SelectedItem = dgShowTeacher.Rows[e.RowIndex].Cells["TSub"].Value.ToString();
            txtAddress.Text = dgShowTeacher.Rows[e.RowIndex].Cells["TAdd"].Value.ToString();
            dtpDOB.Text = DateTime.Parse(dgShowTeacher.Rows[e.RowIndex].Cells["TDOB"].Value.ToString()).ToString("yyyy/MM/dd");
            if (txtUserName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dgShowTeacher.Rows[e.RowIndex].Cells["Tid"].Value.ToString());
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainMenu mu = new MainMenu();
            mu.Show();
            this.Hide();
        }
    }
}

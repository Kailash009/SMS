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
    public partial class Students : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public Students()
        {
            InitializeComponent();
            showAllStudent();
        }
        void showAllStudent()
        {
            string sql = "select * from tbl_Student";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dtgShowStudents.DataSource = dt;
            }
        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            addStudent();
        }
        void addStudent()
        {
            if (txtName.Text == "" || cbGender.SelectedIndex == -1 || txtFees.Text == "" || txtAddress.Text == "" || cbClass.SelectedIndex == -1)
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
                        string sql = "insert into tbl_Student(stname,stGen,stDOB,stClass,stFees,stAdd)values(@stname,@stGen,@stDOB,@stClass,@stFees,@stAdd)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@stname", txtName.Text);
                        cmd.Parameters.AddWithValue("@stGen", cbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stDOB", dtDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@stClass", cbClass.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stFees", txtFees.Text);
                        cmd.Parameters.AddWithValue("@stAdd", txtAddress.Text);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Student Added SuccessFully!!");
                            showAllStudent();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Student Added Failed!!");
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
        private void dtgShowStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dtgShowStudents.Rows[e.RowIndex].Cells["stname"].Value.ToString();
            cbGender.SelectedItem = dtgShowStudents.Rows[e.RowIndex].Cells["stGen"].Value.ToString();
            dtDOB.Text = DateTime.Parse(dtgShowStudents.Rows[e.RowIndex].Cells["stDOB"].Value.ToString()).ToString("yyyy/MM/dd");
            cbClass.SelectedItem = dtgShowStudents.Rows[e.RowIndex].Cells["stClass"].Value.ToString();
            txtFees.Text = dtgShowStudents.Rows[e.RowIndex].Cells["stFees"].Value.ToString();
            txtAddress.Text = dtgShowStudents.Rows[e.RowIndex].Cells["stAdd"].Value.ToString();
            if (txtName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dtgShowStudents.Rows[e.RowIndex].Cells["sid"].Value.ToString());
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            deleteStudent();
        }
        void deleteStudent()
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
                    string sql = "Delete from tbl_Student where sid=@sid";
                    SqlCommand cmd = new SqlCommand(sql,con);
                    cmd.Parameters.AddWithValue("@sid", key);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        MessageBox.Show("Student Deleted SuccessFully!!");
                        showAllStudent();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Student Deleted Failed!!");
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
        private void Reset()
        {
            key = 0;
            txtName.Text = "";
            txtFees.Text = "";
            txtAddress.Text = "";
            cbGender.SelectedIndex = 0;
            cbClass.SelectedIndex = 0;
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            updateStudent();
        }
        void updateStudent()
        {
            if (txtName.Text == "" || cbGender.SelectedIndex == -1 || txtFees.Text == "" || txtAddress.Text == "" || cbClass.SelectedIndex == -1)
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
                        string sql = "update tbl_Student set stname=@stname,stGen=@stGen,stDOB=@stDOB,stClass=@stClass,stFees=@stFees,stAdd=@stAdd where sid=@sid";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@stname", txtName.Text);
                        cmd.Parameters.AddWithValue("@stGen", cbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stDOB", dtDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@stClass", cbClass.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stFees", txtFees.Text);
                        cmd.Parameters.AddWithValue("@stAdd", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@sid",key);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Student Updated SuccessFully!!");
                            showAllStudent();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Student Updated Failed!!");
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
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

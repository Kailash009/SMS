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
    public partial class Fees : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public Fees()
        {
            InitializeComponent();
            fillStudentID();
            showAllFees();
            getStudentName();
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
        void showAllFees()
        {
            string sql = "select * from tbl_Fees";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgFees.DataSource = dt;
            }
        }
        int key = 0;
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbStudentID.SelectedValue = dgFees.Rows[e.RowIndex].Cells["StID"].Value.ToString();
            txtStudentName.Text = dgFees.Rows[e.RowIndex].Cells["StName"].Value.ToString();
            dtPeriod.Text = DateTime.Parse(dgFees.Rows[e.RowIndex].Cells["Month"].Value.ToString()).ToString("yyyy/MM/dd");
            txtAmount.Text = dgFees.Rows[e.RowIndex].Cells["Amt"].Value.ToString();
            if (txtStudentName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dgFees.Rows[e.RowIndex].Cells["PayID"].Value.ToString());
            }
        }

        private void btnAddFee_Click(object sender, EventArgs e)
        {
            addFee();
        }
        void addFee()
        {
            if (cbStudentID.SelectedIndex == -1 || txtStudentName.Text == "" || txtAmount.Text=="")
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
                        string paymentperiod;
                        paymentperiod = dtPeriod.Value.Month.ToString() + "/" + dtPeriod.Value.Year.ToString();
                        string sql = "Select count(*) from tbl_Fees where StID='" + cbStudentID.SelectedValue.ToString() + "' and Month='" + paymentperiod.ToString() + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows[0][0].ToString()=="1")
                        {
                            MessageBox.Show("There is No Due for this Month");
                        }
                        else
                        {
                            sql = "insert into tbl_Fees(StID,StName,Month,Amt)values(@StID,@StName,@Month,@Amt)";
                            cmd = new SqlCommand(sql, con);
                            cmd.Parameters.AddWithValue("@StID", cbStudentID.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@StName", txtStudentName.Text);
                            cmd.Parameters.AddWithValue("@Month", paymentperiod);
                            cmd.Parameters.AddWithValue("@Amt", txtAmount.Text);
                            int n = cmd.ExecuteNonQuery();
                            if (n != 0)
                            {
                                MessageBox.Show("Fees SuccessFully Paid!!");
                                showAllFees();
                                Reset();
                            }
                            else
                            {
                                MessageBox.Show("Fees Added Failed!!");
                            }
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

        private void btnEditFee_Click(object sender, EventArgs e)
        {
            editFee();
        }
        void editFee()
        {
            if (cbStudentID.SelectedIndex == -1 || txtStudentName.Text == "" || txtAmount.Text=="")
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
                        string sql = "update tbl_Fees set StID=@StID,StName=@StName,Month=@Month,Amt=@Amt where PayID=@PayID";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@StID", cbStudentID.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@StName", txtStudentName.Text);
                        cmd.Parameters.AddWithValue("@Month", dtPeriod.Value.Date);
                        cmd.Parameters.AddWithValue("@Amt", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@PayID", key);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Fees Updated SuccessFully!!");
                            showAllFees();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Fees Updated Failed!!");
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

        private void btnDeleteFee_Click(object sender, EventArgs e)
        {
            deleteFee();
        }
        void deleteFee()
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Fees!");
            }
            else
            {
                try
                {
                    con.Open();
                    string sql = "Delete from tbl_Fees where PayID=@PayID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@PayID", key);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        MessageBox.Show("Fees Deleted SuccessFully!!");
                        showAllFees();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Fees Deleted Failed!!");
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
            MainMenu mu = new MainMenu();
            mu.Show();
            this.Hide();
        }
        private void Reset()
        {
            txtAmount.Text = "";
            cbStudentID.SelectedIndex = 0;
            txtStudentName.Text = "";
        }

        private void cbStudentID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getStudentName();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainMenu mu = new MainMenu();
            mu.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

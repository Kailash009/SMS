using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using MainMenu = WinSMS.MainMenu;

namespace SMSWin
{
    public partial class SchoolEvent : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString());
        public SchoolEvent()
        {
            InitializeComponent();
            showAllEvents();
        }
        void showAllEvents()
        {
            string sql = "select * from tbl_Events";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gdEvent.DataSource = dt;
            }
        }
        void addEvent()
        {
            if (txtEvent.Text == "" || txtDuration.Text == "")
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
                        string sql = "insert into tbl_Events(EDesc,EDate,EDuration)values(@EDesc,@EDate,@EDuration)";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@EDesc", txtEvent.Text);
                        cmd.Parameters.AddWithValue("@EDate", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@EDuration", txtDuration.Text);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Event Added SuccessFully!!");
                            showAllEvents();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Event Added Failed!!");
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
        private void gdEvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEvent.Text = gdEvent.Rows[e.RowIndex].Cells["EDesc"].Value.ToString();
            dtpDate.Text = DateTime.Parse(gdEvent.Rows[e.RowIndex].Cells["EDate"].Value.ToString()).ToString("yyyy/MM/dd");
            txtDuration.Text = gdEvent.Rows[e.RowIndex].Cells["EDuration"].Value.ToString();
            if (txtEvent.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(gdEvent.Rows[e.RowIndex].Cells["EID"].Value.ToString());
            }

        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            addEvent();
        }
        private void Reset()
        {
            txtEvent.Text = "";
            txtDuration.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            deleteEvent();
        }
        void deleteEvent()
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Event!");
            }
            else
            {
                try
                {
                    con.Open();
                    string sql = "Delete from tbl_Events where EID=@Eid";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Eid", key);
                    int n = cmd.ExecuteNonQuery();
                    if (n != 0)
                    {
                        MessageBox.Show("Event Deleted SuccessFully!!");
                        showAllEvents();
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Event Deleted Failed!!");
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

        void updateEvents()
        {
            if (txtEvent.Text == "" || txtDuration.Text == "")
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
                        string sql = "update tbl_Events set EDesc=@EDesc,EDate=@EDate,EDuration=@EDuration where EID=@EID";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@EDesc", txtEvent.Text);
                        cmd.Parameters.AddWithValue("@EDate", dtpDate.Value.Date);
                        cmd.Parameters.AddWithValue("@EDuration", txtDuration.Text);
                        cmd.Parameters.AddWithValue("@EID", key);
                        int n = cmd.ExecuteNonQuery();
                        if (n != 0)
                        {
                            MessageBox.Show("Event Updated SuccessFully!!");
                            showAllEvents();
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Event Updated Failed!!");
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
        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            updateEvents();
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

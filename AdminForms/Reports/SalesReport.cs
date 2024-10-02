using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capstone_Flowershop;

namespace Capstone_Flowershop.AdminForms.Reports.SalesReports
{   
    public partial class SalesReport : Form
    {

        public static SalesReport instance;
        public Label uid;
        public SalesReport()
        {
   

            InitializeComponent();
        
            instance = this;
            uid = label2;


            DateTime date = DateTime.Now;

            string datenow = DateTime.Now.Date.ToString();
            label3.Text = date.ToString();
  
           // label2.Text = "" + ", " + ui.role;
            
            
           label2.Text = UserInfo.FullName;

        }
        public void getDaily()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string day = date.Day.ToString();
                con.Open();
                string query = "Select sum(TotalPrice) as daily from FinishedTransaction where Day(DateOfTransaction) = @day";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@day", day);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        if (reader["daily"].ToString() == "0" || reader["daily"].ToString() == null || reader["daily"].ToString() == "")
                        {
                            label6.Text = "0";
                        }
                        else
                        {
                            label6.Text = reader["daily"].ToString();
                        }
                    }
                }
            }
        }
        public void getYearly()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string day = date.Year.ToString();
                con.Open();
                string query = "Select sum(TotalPrice) as output from FinishedTransaction where Year(DateOfTransaction) = @input";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@input", day);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        if (reader["output"].ToString() == "0" || reader["output"].ToString() == null || reader["output"].ToString() == "")
                        {
                            label9.Text = "0";
                        }
                        else
                        {
                            label9.Text = reader["output"].ToString();
                        }
                    }
                }
            }
        }
        public void getMonthly()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string day = date.Month.ToString();
                
                con.Open();
                string query = "Select sum(TotalPrice) as monthly from FinishedTransaction where Month(DateOfTransaction) = @input";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@input", day);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {   
                        if(reader["monthly"].ToString() == "0" || reader["monthly"].ToString() == null)
                        {
                            label7.Text = "0";
                        }
                        else
                        {
                            label7.Text = reader["monthly"].ToString();
                        }
                        
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string al = label2.Text;
            MessageBox.Show(al);
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            getDaily();
            getMonthly();
            getYearly();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

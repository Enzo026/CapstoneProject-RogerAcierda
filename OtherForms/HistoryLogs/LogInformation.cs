using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.HistoryLogs
{
    public partial class LogInformation : Form
    {
        public LogInformation()
        {
            InitializeComponent();
        }

        private void LogInformation_Load(object sender, EventArgs e)
        {
            GetInfo();
            textBox1.Enabled = false;
        }
        public void GetInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from HistoryLogs where Type = 'TransactionLog' AND Id=@ID ;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                        int rowCount = (int)countCommand.ExecuteScalar();

                        if(rowCount == 1) 
                        {
                            string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' AND Id=@ID;";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        label7.Text = reader["Title"].ToString();
                                        label6.Text = reader["Definition"].ToString();
                                        label9.Text = reader["ReferenceID"].ToString();
                                        label8.Text = reader["Employee"].ToString()+"("+ reader["EmployeeID"].ToString()+")";
                                        label11.Text = reader["Date"].ToString();
                                        textBox1.Text = reader["Headline"].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on Fetching Information | GetInfo() : " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

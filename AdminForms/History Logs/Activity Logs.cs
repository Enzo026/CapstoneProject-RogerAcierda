using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.HistoryLogs;
using Flowershop_Thesis.OtherForms.ProductMaintenance;
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

namespace Flowershop_Thesis.AdminForms.History_Logs
{
    public partial class Activity_Logs : Form
    {

        public Activity_Logs()
        {
            InitializeComponent();
            DisplayActivity();
        }
        public void DisplayActivity()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'ActivityLog' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        ActivityLogsList[] inv = new ActivityLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'ActivityLog'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new ActivityLogsList();
                                    inv[index].id = reader["Id"].ToString().Trim();
                                    inv[index].name = reader["Employee"].ToString().Trim();
                                    inv[index].date = reader["Date"].ToString().Trim();
                                    inv[index].action = reader["Title"].ToString().Trim();
                                    inv[index].HeadLine = reader["HeadLine"].ToString().Trim();

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
    }
}

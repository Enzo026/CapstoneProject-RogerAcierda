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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.AdminForms.History_Logs
{
    public partial class Activity_Logs : Form
    {

        public Activity_Logs()
        {
            InitializeComponent();
            DisplayActivity();
            date1.MaxDate = DateTime.Today;
        }
        public void SortByDate()
        {
            try
            {
                // Clear previous controls
                flowLayoutPanel1.Controls.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Adjust the end date by adding 1 day
                    DateTime adjustedEndDate = date2.Value.AddDays(1);

                    // Construct the count query to count records within the adjusted date range
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs WHERE Type = 'ActivityLog' " +
                                        "AND Date BETWEEN @startDate AND @endDate";

                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        // Add parameters to avoid SQL injection and use the selected date range
                        countCommand.Parameters.AddWithValue("@startDate", date1.Value);
                        countCommand.Parameters.AddWithValue("@endDate", adjustedEndDate);

                        // Get the total number of rows that match the criteria
                        int rowCount = (int)countCommand.ExecuteScalar();
                        ActivityLogsList[] inv = new ActivityLogsList[rowCount];

                        // Construct the data query to retrieve the filtered records
                        string sqlQuery = "SELECT * FROM HistoryLogs WHERE Type = 'ActivityLog' " +
                                          "AND Date BETWEEN @startDate AND @endDate ORDER BY Date DESC";

                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            // Add parameters for start and end date filtering
                            command.Parameters.AddWithValue("@startDate", date1.Value);
                            command.Parameters.AddWithValue("@endDate", adjustedEndDate);

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

                                    // Add the item to the FlowLayoutPanel
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
        public void DisplayActivity()
        {
            try
            {
                // Clear previous controls in the FlowLayoutPanel
                flowLayoutPanel1.Controls.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Modify the query to fetch only the top 20 records
                    string sqlQuery = "SELECT TOP 20 * FROM HistoryLogs WHERE Type = 'ActivityLog' ORDER BY Date DESC";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        // Execute the query and get the data reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;

                            // Loop through the reader to fetch the top 20 records
                            while (reader.Read())
                            {
                                // Create a new ActivityLogsList object for each record
                                ActivityLogsList log = new ActivityLogsList();
                                log.id = reader["Id"].ToString().Trim();
                                log.name = reader["Employee"].ToString().Trim();
                                log.date = reader["Date"].ToString().Trim();
                                log.action = reader["Title"].ToString().Trim();
                                log.HeadLine = reader["HeadLine"].ToString().Trim();

                                // Add the new object to the FlowLayoutPanel
                                flowLayoutPanel1.Controls.Add(log);

                                // Increment the index (this can be omitted since the loop automatically terminates after 20 records)
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                MessageBox.Show("Error Individual: " + ex.Message);
            }

        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
            date2.MinDate = date1.Value; // Update MinDate
            date2.MaxDate = DateTime.Today;
            date2.Enabled = true;
        }

        private void dateBtn_Click(object sender, EventArgs e)
        {
            SortByDate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   
            if(textBox1.Text.Length > 0)
            {
                try
                {
                    flowLayoutPanel1.Controls.Clear();
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'ActivityLog' AND Employee like @emp ";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@emp", "%" + textBox1.Text.Trim() + "%");
                            int rowCount = (int)countCommand.ExecuteScalar();
                            ActivityLogsList[] inv = new ActivityLogsList[rowCount];

                            string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'ActivityLog' AND Employee like @emp order by Date desc";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@emp", "%" + textBox1.Text.Trim() + "%");
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
}

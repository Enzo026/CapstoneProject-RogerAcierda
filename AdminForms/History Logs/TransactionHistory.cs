using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.HistoryLogs;
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

namespace Flowershop_Thesis.AdminForms.History_Logs
{
    public partial class Transaction_History : Form
    {   
        public Transaction_History()
        {
            InitializeComponent();
            ViewTransactionList();

            date1.MaxDate = DateTime.Today;
        }
        public void ViewTransactionList()
        {
            try
            {
                // Clear previous controls in the flow layout panel
                flowLayoutPanel1.Controls.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Query to fetch the top 30 most recent transaction logs, ordered by Id in descending order
                    string sqlQuery = "SELECT TOP 20 * FROM HistoryLogs WHERE Type = 'TransactionLog' ORDER BY Date DESC";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        // Execute the query and get a data reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read())
                            {
                                // Create a new TransactionLogsList object for each record
                                var log = new TransactionLogsList();
                                log.id = reader["Id"].ToString().Trim();
                                log.name = reader["Employee"].ToString().Trim();
                                log.date = reader["Date"].ToString().Trim();
                                log.customerName = reader["Title"].ToString().Trim();
                                log.price = reader["Definition"].ToString().Trim();

                                // Add the new object to the FlowLayoutPanel
                                flowLayoutPanel1.Controls.Add(log);

                                // Increment the index (though this is unnecessary with the while loop)
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
        public void SearchCustomer()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'TransactionLog' AND Title like @emp ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@emp", "%" + textBox1.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' AND Title like @emp ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@emp", "%" + textBox1.Text.Trim() + "%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionLogsList();
                                    inv[index].id = reader["Id"].ToString().Trim();
                                    inv[index].name = reader["Employee"].ToString().Trim();
                                    inv[index].date = reader["Date"].ToString().Trim();
                                    inv[index].customerName = reader["Title"].ToString().Trim();
                                    inv[index].price = reader["Definition"].ToString().Trim();

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
        public void SearchEmployee()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'TransactionLog' AND Employee like @emp ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' AND Employee like @emp ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionLogsList();
                                    inv[index].id = reader["Id"].ToString().Trim();
                                    inv[index].name = reader["Employee"].ToString().Trim();
                                    inv[index].date = reader["Date"].ToString().Trim();
                                    inv[index].customerName = reader["Title"].ToString().Trim();
                                    inv[index].price = reader["Definition"].ToString().Trim();

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
        public void SortByPrice()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'TransactionLog' order by Definition desc ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' order by Definition desc ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionLogsList();
                                    inv[index].id = reader["Id"].ToString().Trim();
                                    inv[index].name = reader["Employee"].ToString().Trim();
                                    inv[index].date = reader["Date"].ToString().Trim();
                                    inv[index].customerName = reader["Title"].ToString().Trim();
                                    inv[index].price = reader["Definition"].ToString().Trim();

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
        public void SortByDate()
        {
            try
            {
                // Clear previous controls in the FlowLayoutPanel
                flowLayoutPanel1.Controls.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Add 1 day to the end date to include the entire day in the query
                    DateTime adjustedEndDate = date2.Value.AddDays(1);

                    // Construct the count query to count records within the date range (adjusted end date)
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs WHERE Type = 'TransactionLog' " +
                                        "AND Date BETWEEN @startDate AND @endDate";

                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        // Add parameters to avoid SQL injection and use the selected date range
                        countCommand.Parameters.AddWithValue("@startDate", date1.Value);
                        countCommand.Parameters.AddWithValue("@endDate", adjustedEndDate);

                        // Add the employee filter if necessary
                        countCommand.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");

                        // Get the total number of rows that match the criteria
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        // Construct the data query to retrieve the filtered records
                        string sqlQuery = "SELECT * FROM HistoryLogs WHERE Type = 'TransactionLog' " +
                                          "AND Date BETWEEN @startDate AND @endDate ORDER BY Date DESC";

                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            // Add parameters for start and adjusted end date filtering
                            command.Parameters.AddWithValue("@startDate", date1.Value);
                            command.Parameters.AddWithValue("@endDate", adjustedEndDate);

                            // Add employee filter if necessary
                            command.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionLogsList();
                                    inv[index].id = reader["Id"].ToString().Trim();
                                    inv[index].name = reader["Employee"].ToString().Trim();
                                    inv[index].date = reader["Date"].ToString().Trim();
                                    inv[index].customerName = reader["Title"].ToString().Trim();
                                    inv[index].price = reader["Definition"].ToString().Trim();

                                    // Add the transaction log item to the FlowLayoutPanel
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
        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {   
                flowLayoutPanel1.Controls.Clear();
                SearchCustomer();
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                ViewTransactionList();  
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0) 
            {   
                flowLayoutPanel1.Controls.Clear();
                SearchEmployee();
            }
            else
            {   
                flowLayoutPanel1.Controls.Clear();
                ViewTransactionList();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date2.MinDate = date1.Value; // Update MinDate
            date2.MaxDate = DateTime.Today;
            date2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SortByDate();
        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label90_Click(object sender, EventArgs e)
        {

        }

        private void label89_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        }
        public void ViewTransactionList()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'TransactionLog' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' order by Id desc;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

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
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Type = 'TransactionLog' order by Date desc ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@emp", "%" + textBox2.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionLogsList[] inv = new TransactionLogsList[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Type = 'TransactionLog' order by Date desc ";
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



























































































            //if (radioButton1.Checked) 
            //{
            //    flowLayoutPanel1.Controls.Clear();
            //    SortByPrice();
            //}
            //else
            //{
            //    flowLayoutPanel1.Controls.Clear();
            //    SortByDate();
            //}
        }
    }
}

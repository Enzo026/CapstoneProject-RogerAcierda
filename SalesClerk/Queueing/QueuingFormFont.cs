using Flowershop_Thesis.OtherForms;
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

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class QueuingFormFont : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public QueuingFormFont()
        {
            InitializeComponent();
            testConnection();
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Build the full path to the database file
            string databaseFilePath = Path.Combine(executableDirectory, "try.mdf");

            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);
                   

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProcessingArea();
            PaymentArea();
        }
        public void ProcessingArea()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where Status = 'Processing';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
        



                    QueueBoardContent[] inv = new QueueBoardContent[rowCount];

                    string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Processing' ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new QueueBoardContent();
                                inv[index].Name = reader["CustomerName"].ToString();
                                flowLayoutPanel1.Controls.Add(inv[index]);
                                index++;
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        public void PaymentArea()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where Status = 'Payment';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();




                    QueueBoardContent[] inv = new QueueBoardContent[rowCount];

                    string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Payment' ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new QueueBoardContent();
                                inv[index].Name = reader["CustomerName"].ToString();
                                flowLayoutPanel2.Controls.Add(inv[index]);
                                index++;
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
    }
}

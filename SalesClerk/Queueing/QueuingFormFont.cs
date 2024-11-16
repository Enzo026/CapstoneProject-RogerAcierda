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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class QueuingFormFont : Form
    {

        public QueuingFormFont()
        {
            InitializeComponent();

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
            RecievingArea();
        }
        public void ProcessingArea()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        public void PaymentArea()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Assembly';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        QueueBoardContent[] inv = new QueueBoardContent[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Assembly' ;";
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
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        public void RecievingArea()
        {
            try
            {
                flowLayoutPanel3.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Receiving' AND PaymentStatus = 'Paid';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        QueueBoardContent[] inv = new QueueBoardContent[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Receiving' AND PaymentStatus = 'Paid' ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new QueueBoardContent();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    flowLayoutPanel3.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
    }
}

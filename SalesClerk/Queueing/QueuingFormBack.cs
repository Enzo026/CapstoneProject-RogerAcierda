using Flowershop_Thesis.MainForms;
using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.SalesClerk.Order_Placement;
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
using Flowershop_Thesis.OtherForms.QueuingList;

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class QueuingFormBack : Form
    {

        public static QueuingFormBack instance;
        public Label lblcounter;
        public QueuingFormBack()
        {
            InitializeComponent();
            instance = this;
            lblcounter = counter;
            GetListQueue();
            GetFinished();
            GetCancelled();
            FormIsReady = true;
        }

        private void label78_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button58_Click(object sender, EventArgs e)
        {
            //for salesclerk
            QueuingFormFont QFF = new QueuingFormFont();
            QFF.Show();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetListQueue()
        {
            
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            flowLayoutPanel5.Controls.Clear();
            try
            {
                FormIsReady = false;
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status != 'Cancelled' AND  Status != 'Completed';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        if (int.Parse(counter.Text) != rowCount)
                        {
                            counter.Text = rowCount.ToString();
                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Processing' AND Status != 'Cancelled';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        


                        QueuingListItems[] inv = new QueuingListItems[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Processing' AND Status != 'Cancelled';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new QueuingListItems();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    //  inv[index].Status = reader["Status"].ToString();
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    int CI = reader.GetOrdinal("TransactionID");
                                    inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Payment' AND Status != 'Cancelled';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();

                        PaymentList[] inv = new PaymentList[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Payment' AND Status != 'Cancelled';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new PaymentList();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    //  inv[index].Status = reader["Status"].ToString();
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    int CI = reader.GetOrdinal("TransactionID");
                                    inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);

                                    flowLayoutPanel4.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Receiving' AND Status != 'Cancelled' AND  PaymentStatus = 'Paid';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();

                        ReceivingList[] inv = new ReceivingList[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Receiving' AND Status != 'Cancelled' AND PaymentStatus = 'Paid';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new ReceivingList();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    //  inv[index].Status = reader["Status"].ToString();
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    int CI = reader.GetOrdinal("TransactionID");
                                    inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);

                                    flowLayoutPanel5.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
                FormIsReady = true;
            }
            catch (Exception ex)
            {

               Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        public void GetFinished()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Completed';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        
                        FinishedOrdersList[] inv = new FinishedOrdersList[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Completed' ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new FinishedOrdersList();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    decimal priceIndex = reader.GetOrdinal("Price");
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    int CI = reader.GetOrdinal("TransactionID");
                                    inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);

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

        public void GetCancelled()
        {
            try
            {
                flowLayoutPanel3.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status = 'Cancelled';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        // MessageBox.Show(rowCount.ToString());



                        CancelledOrderList[] inv = new CancelledOrderList[rowCount];

                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status = 'Cancelled' ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new CancelledOrderList();
                                    inv[index].Name = reader["CustomerName"].ToString();



                                    decimal priceIndex = reader.GetOrdinal("Price");
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    int CI = reader.GetOrdinal("TransactionID");
                                    inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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
        private void QueuingFormBack_Load(object sender, EventArgs e)
        {
            
        }
        bool FormIsReady;
        private void counter_TextChanged(object sender, EventArgs e)
        {
            if (FormIsReady)
            {   
                GetListQueue();
                GetCancelled();
                GetFinished();
            }

        }

        private void counter_Click(object sender, EventArgs e)
        {

        }
    }
}

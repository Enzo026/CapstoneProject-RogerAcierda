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

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class QueuingFormBack : Form
    {
        SqlConnection con;
        SqlConnection conn  ;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static QueuingFormBack instance;
        public Label lblcounter;
        public QueuingFormBack()
        {
            InitializeComponent();
            testConnection();
            instance = this;
            lblcounter = counter; 
            GetListQueue();
            GetFinished();
            GetCancelled();
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");

            // MessageBox.Show(databaseFilePath);
            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);
                    conn = con;

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

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
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where Status != 'Completed' AND Status != 'Cancelled';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    
                  
                        counter.Text = rowCount.ToString();
                    


                    QueuingListItems[] inv = new QueuingListItems[rowCount];
               
                    string sqlQuery = "SELECT * FROM TransactionsTbl where Status != 'Completed' AND Status != 'Cancelled';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new QueuingListItems();
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Status = reader["Status"].ToString();
                            

                                decimal priceIndex = reader.GetOrdinal("Price");
                               inv[index].Price = 100;
                                int CI = reader.GetOrdinal("TransactionID");
                                inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

        public void GetFinished()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where Status = 'Completed';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    // MessageBox.Show(rowCount.ToString());



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
                                inv[index].Price = 100;
                                int CI = reader.GetOrdinal("TransactionID");
                                inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

        public void GetCancelled()
        {
            try
            {
                flowLayoutPanel3.Controls.Clear();
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
                                inv[index].Price = 100;
                                int CI = reader.GetOrdinal("TransactionID");
                                inv[index].transID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





                                flowLayoutPanel3.Controls.Add(inv[index]);
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
        private void QueuingFormBack_Load(object sender, EventArgs e)
        {

        }

        private void counter_TextChanged(object sender, EventArgs e)
        {
            GetListQueue();
            GetCancelled();
            GetFinished();
        }

        private void counter_Click(object sender, EventArgs e)
        {

        }
    }
}

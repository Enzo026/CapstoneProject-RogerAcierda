using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.OtherForms.AdvanceOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class AdvanceOrdersList : Form
    {
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static AdvanceOrdersList instance;
        public Label OId;
        public Label PUD;
        public Label Price;
        public Label DP;
        public Label CancelPeriod;
        public Label CustName;
        public Label OrderQty;

        public AdvanceOrdersList()
        {
            InitializeComponent();
            instance = this;
            OId = label33;
            PUD = PUDLbl;
            Price = TotalAmountLbl;
            DP = DownpaymentLbl;
            CancelPeriod = CancelDate;
            CustName = CustomerNameLbl;
            OrderQty = OrderItmQtyLbl;
            testConnection();
            getListOrder();
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            AdvanceOrderEdit form = new AdvanceOrderEdit();
            form.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }
        public void getListOrder()
        {
            try
            {
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SortReservationDate()
        {
            try
            {   
                flowLayoutPanel1.Controls.Clear();
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' order by PickupDate ASC ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SortAtoZ()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' order by CustomerName ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SortPrice()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' order by TotalPrice ASC ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SortOrderType()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' order by OrderType ASC ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void SearchCustomerName()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];




                    string sqlQuery = "SELECT * FROM AdvanceOrders WHERE Status = @Status AND CustomerName LIKE @CustomerName";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Status", "Active");
                        command.Parameters.AddWithValue("@CustomerName", "%" + textBox1.Text.Trim() + "%");

                        

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SortReservationDate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SortAtoZ();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SortPrice();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SortOrderType();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   
            if(textBox1.Text.Length > 0 && textBox1.Text != null && textBox1.Text != "")
            {
                SearchCustomerName();
            }
            else
            {
                getListOrder();
            }
            
        }

        private void label33_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sqlQty = "SELECT COUNT(*) AS Orders FROM AdvanceOrderItems WHERE OrderID = @ID";
                using (SqlCommand comd = new SqlCommand(sqlQty, con))
                {
                    comd.Parameters.AddWithValue("@ID", label33.Text);

                    OrderItmQtyLbl.Text = comd.ExecuteScalar().ToString();
                     
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on fetching order qty :"+ex.Message);
            }
        }
        private void ordertoday() 
        {
            try
            {
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AdvanceOrderListContents[] inv = new AdvanceOrderListContents[rowCount];

                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AdvanceOrderListContents();
                                inv[index].transID = int.Parse(reader["OrderID"].ToString());
                                inv[index].Name = reader["CustomerName"].ToString();
                                inv[index].Type = reader["OrderType"].ToString();
                                inv[index].OrderPickupDate = reader["PickupDate"].ToString();
                                inv[index].Price = reader["TotalPrice"].ToString();
                                inv[index].Downpayment = reader["Downpayment"].ToString();

                                string id = inv[index].transID.ToString();



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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

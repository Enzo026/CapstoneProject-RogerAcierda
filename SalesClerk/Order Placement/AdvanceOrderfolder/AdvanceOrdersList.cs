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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class AdvanceOrdersList : Form
    {

        public static AdvanceOrdersList instance;
        public Label OId;
        public Label PUD;
        public Label Price;
        public Label DP;
        public Label CancelPeriod;
        public Label CustName;
        public Label OrderQty;
        public Label todaycounter;
        bool formload;
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
            todaycounter = label9;
            getListOrder();
            ordertoday();
            
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQty = "SELECT COUNT(*) AS Orders FROM AdvanceOrderItems WHERE OrderID = @ID";
                    using (SqlCommand comd = new SqlCommand(sqlQty, con))
                    {
                        comd.Parameters.AddWithValue("@ID", label33.Text);
                        OrderItmQtyLbl.Text = comd.ExecuteScalar().ToString();
                    }
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM AdvanceOrders where Status = 'Active' AND Pickupdate = @Date";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {

                        DateTime datetimetoday = DateTime.Now;
                        int day = datetimetoday.Day;
                        int month = datetimetoday.Month;
                        int year = datetimetoday.Year;

                        string sday = "00";
                        string smonth = "00";
                        string syear = "00";
                        if (day < 10)
                        {
                            sday = "0" + day;
                        }
                        else
                        {
                            sday = day.ToString();
                        }
                        if (month < 10)
                        {
                            smonth = "0" + month;
                        }
                        else { smonth = month.ToString(); }
                        if (year < 10)
                        {
                            syear = "0" + year;
                        }
                        else
                        {
                            syear = year.ToString();
                        }

                        string today = syear + "-" + smonth + "-" + sday;

                        countCommand.Parameters.AddWithValue("Date", today);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        label9.Text = rowCount.ToString();
                        OrdersTodayList[] inv = new OrdersTodayList[rowCount];

                        string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' AND Pickupdate =@Date";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            command.Parameters.AddWithValue("Date", today);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new OrdersTodayList();
                                    inv[index].transID = reader["OrderID"].ToString();
                                    inv[index].Name = reader["CustomerName"].ToString();
                                    inv[index].downpayment = reader["Downpayment"].ToString();
                                    inv[index].Total = reader["TotalPrice"].ToString();
                                    inv[index].discount = reader["Discount"].ToString();




                                    double price = double.Parse(reader["TotalPrice"].ToString());
                                    double downpayment = double.Parse(reader["Downpayment"].ToString());
                                    inv[index].Price = price - downpayment;




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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void flowLayoutPanel2_ControlRemoved(object sender, ControlEventArgs e)
        {
           
        }

        private void label9_TextChanged(object sender, EventArgs e)
        {   

           if(formload == true)
            {
                flowLayoutPanel2.Controls.Clear();
               flowLayoutPanel1.Controls.Clear();
                ordertoday();
                getListOrder();
            }
            

        }

        private void AdvanceOrdersList_Load(object sender, EventArgs e)
        {
            formload = true;
        }
    }
}

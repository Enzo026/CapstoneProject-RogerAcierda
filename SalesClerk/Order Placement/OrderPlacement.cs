using Flowershop_Thesis.OtherForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Flowershop_Thesis.SalesClerk.Order_Placement
{
    public partial class OrderPlacement : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public static OrderPlacement instance;
        public Label lbl;
        public FlowLayoutPanel cartlist;
        public Button refresher;
        public OrderPlacement()
        {
            InitializeComponent();
            testConnection();
            DisplayIndividual();
            instance = this;
            lbl = label4;
            cartlist = flowLayoutPanel2;
            getCartList();
            getPrice();

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
        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            DisplayIndividual();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            CustomBuoquet CB = new CustomBuoquet();
            CB.TopLevel = false;
            flowLayoutPanel1.Controls.Add(CB);
            CB.BringToFront();
            CB.Show();
        }

        public void DisplayIndividual()
        {
            try
            {
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND ItemType = 'Individual' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    TransactionItemList[] inv = new TransactionItemList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new TransactionItemList();
                                inv[index].ItemID = reader["ItemID"].ToString();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = "Individual";
                                inv[index].Color = reader["ItemColor"].ToString();
                                decimal priceIndex = reader.GetOrdinal("Price");
                                inv[index].Price = reader.IsDBNull((int)priceIndex) ? 0 : reader.GetDecimal((int)priceIndex);
                                int StockQuantity = reader.GetOrdinal("ItemQuantity");
                                inv[index].Stock = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

                                if (reader["ItemImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["ItemImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

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
        public void DisplayBuoquet()
        {
            try
            {

                con.Open();

                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND ItemType = 'Bouquet' OR ItemType = 'Custom' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    TransactionItemList[] inv = new TransactionItemList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemType = 'Bouquet' OR ItemType = 'Custom' ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new TransactionItemList();
                                inv[index].ItemID = reader["ItemID"].ToString();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = "Premade";
                                inv[index].Color = reader["ItemColor"].ToString();
                                decimal priceIndex = reader.GetOrdinal("Price");
                                inv[index].Price = reader.IsDBNull((int)priceIndex) ? 0 : reader.GetDecimal((int)priceIndex);
                                int StockQuantity = reader.GetOrdinal("ItemQuantity");
                                inv[index].Stock = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

                                if (reader["ItemImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["ItemImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

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

        private void button2_Click(object sender, EventArgs e)
        {

            flowLayoutPanel1.Controls.Clear();
            DisplayBuoquet();
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
           
            getCartList();
            getPrice();
            

        }
        private void getCartList()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ServingCart;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    int cartlbl = int.Parse(label4.Text);
                    if (rowCount != cartlbl)
                    {
                        label4.Text = rowCount.ToString();
                    }


                    CartItems[] inv = new CartItems[rowCount];
                    string sqlQuery = "SELECT * FROM ServingCart";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new CartItems();
                                inv[index].ItemID = reader["ItemID"].ToString();
                                inv[index].Name = reader["ItemName"].ToString();

                                int priceIndex = reader.GetOrdinal("OrderPrice");
                                inv[index].Price = reader.IsDBNull(priceIndex) ? 0 : reader.GetInt32(priceIndex);
                                int CI = reader.GetOrdinal("CartID");
                                inv[index].cartID = reader.IsDBNull(CI) ? 0 : reader.GetInt32(CI);
                                int StockQuantity = reader.GetOrdinal("OrderQty");
                                inv[index].qty = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

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
        public void refresh()
        {

            flowLayoutPanel2.Controls.Clear();

        }

        private void button5_Click(object sender, EventArgs e)
        { int cart = int.Parse(label4.Text);
            if(cart > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Cancel all orders in cart?", "Cancel Cart Items", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("TRUNCATE TABLE ServingCart", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    label4.Text = "0";
                    MessageBox.Show("Items in the cart are now cancelled!");
                }
                else
                {
                    //none
                }
            }
            else
            {
                MessageBox.Show("There are no items in cart");
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            DisplayIndividual();
            getCartList();
            getPrice();
        }
        public void getPrice()
        {
            try
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT SUM(OrderPrice) AS TotalPrice FROM ServingCart", con))
                {
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            label13.Text = reader["TotalPrice"].ToString();
                        }
                    }
                }
                con.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetPrice() : " + ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            int counter = int.Parse(label4.Text);
            if (counter <= 0) {
                MessageBox.Show("There are no Items in the Cart");
            }
            else
            {
                ReviewOrder RO = new ReviewOrder();
                RO.Show();
                flowLayoutPanel2.Enabled = false;
            }

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_EnabledChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.Enabled == true )
            {   
                flowLayoutPanel1.Controls.Clear();
                DisplayIndividual();
                getCartList();
                getPrice();
            }
        }
    }
}

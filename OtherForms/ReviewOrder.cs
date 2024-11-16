using Flowershop_Thesis.SalesClerk.Order_Placement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.OtherForms
{
    public partial class ReviewOrder : Form
    {
        SqlCommand cmd = new SqlCommand();

        public static ReviewOrder instance;
        public System.Windows.Forms.Label counter;
        public System.Windows.Forms.Button Proceed;
        public System.Windows.Forms.Button Cancel;

        
        public ReviewOrder()
        {
            InitializeComponent();
            instance = this;
            counter = counterlbl;
            Proceed = ProceedBtn; 
            Cancel = CancelBtn;

       
            getlist();
            NameIndicator.Visible = false;
            FormIsReady = true;
            
           
        }
        bool FormIsReady = false;
        private void getlist()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {   
         
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ServingCart;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        int cartlbl = int.Parse(counterlbl.Text);
                        if (rowCount != cartlbl)
                        {
                            counterlbl.Text = rowCount.ToString();
                            OrderContents_lbl.Text = rowCount.ToString();
                        }

                        ReviewOrderList[] inv = new ReviewOrderList[rowCount];
                        string sqlQuery = "SELECT * FROM ServingCart";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader1 = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader1.Read() && index < inv.Length)
                                {
                                    inv[index] = new ReviewOrderList();
                                    inv[index].ItemID = reader1["ItemID"].ToString();
                                    inv[index].Name = reader1["ItemName"].ToString();

                                    int priceIndex = reader1.GetOrdinal("OrderPrice");
                                    inv[index].Price = reader1.IsDBNull(priceIndex) ? 0 : reader1.GetInt32(priceIndex);
                                    int CI = reader1.GetOrdinal("CartID");
                                    inv[index].cartID = reader1.IsDBNull(CI) ? 0 : reader1.GetInt32(CI);
                                    int StockQuantity = reader1.GetOrdinal("OrderQty");
                                    inv[index].qty = reader1.IsDBNull(StockQuantity) ? 0 : reader1.GetInt32(StockQuantity);

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
                MessageBox.Show("Error Fetching List: " + ex.Message);
            }
            getPrice();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderPlacement.instance.cartlist.Enabled = true;
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }



        public void getPrice()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT SUM(OrderPrice) AS TotalPrice FROM ServingCart", con))
                    {

                        using (SqlDataReader reader4 = command.ExecuteReader())
                        {

                            while (reader4.Read())
                            {
                                Amount_lbl.Text = reader4["TotalPrice"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on GetPrice() : " + ex.Message);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {   
            if(NameIndicator.Text == "Name Available!")
            {
                TransactionTableInput();
                TransactionItemsInput();
                DeductionOfItemInventory();
                OrderPlacement.instance.cartlist.Enabled = true;
                this.Close();
            }
        }
        public void DeductionOfItemInventory()
        {
            try
            { 
                // Deduction of items ordered in Cart
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();


                    // Deletion of cart items (after all operations are completed)
                    using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE ServingCart", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on DeductionOfItemInventory(): " + ex.Message);
            }

        }
        public void DeductionOfItemInventorys()
        {
            try
            {
                int CartCounter;
                //deduction of items ordered in Cart
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    using (SqlCommand CartItems = new SqlCommand("Select Count(*) from ServingCart", con))
                    {
                        CartCounter = (int)CartItems.ExecuteScalar();

                        int counter = int.Parse(counterlbl.Text);
                        if (CartCounter != counter)
                        {
                            label4.Text = CartCounter.ToString();
                        }
                    }

                        CartItems[] inv = new CartItems[CartCounter];
                        string sqlQuery = "SELECT * FROM ServingCart";
                        using (SqlCommand cmds = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader2 = cmds.ExecuteReader())
                            {
                                int index = 0;
                                while (reader2.Read() && index < inv.Length)
                                {
                                    int ID = reader2.GetOrdinal("ItemID");
                                    int ItemID = reader2.IsDBNull(ID) ? 0 : reader2.GetInt32(ID);

                                    int StockQuantity = reader2.GetOrdinal("OrderQty");
                                    int qty = reader2.IsDBNull(StockQuantity) ? 0 : reader2.GetInt32(StockQuantity);

                                    string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @SID;";
                    
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                                    {

                                        updateCommand.Parameters.AddWithValue("@Quantity", qty);
                                        updateCommand.Parameters.AddWithValue("@SID", ItemID);

                                        updateCommand.ExecuteNonQuery();

                                    }
                                    index++;
                                }
                            }
                        
                    }
                    //deletion of cart items
                    cmd = new SqlCommand("TRUNCATE TABLE ServingCart", con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on  DeductionOfItemInventory() : " + ex.Message);
            }
        }

        public void TransactionItemsInput()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    int TransId;
                    con.Open();

                    // Use parameterized query to prevent SQL injection
                    using (SqlCommand TransactionID = new SqlCommand("SELECT TransactionID FROM transactionstbl WHERE Status = 'Payment' AND CustomerName = @CustName;", con))
                    {
                        TransactionID.Parameters.AddWithValue("@CustName", CustName_txtbox.Text);

                        // Handle null case for ExecuteScalar
                        var result = TransactionID.ExecuteScalar();
                        if (result != null)
                        {
                            TransId = (int)result;
                            //MessageBox.Show(TransId.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Transaction not found.");
                            return; // Exit if no transaction is found
                        }
                    }

                    // Deduction of items ordered in Cart
                    int CartCounter;
                    using (SqlCommand CartItems = new SqlCommand("SELECT COUNT(*) FROM ServingCart", con))
                    {
                        CartCounter = (int)CartItems.ExecuteScalar();

                        int counter = int.Parse(counterlbl.Text);
                        if (CartCounter != counter)
                        {
                            label4.Text = CartCounter.ToString();
                        }
                    }

                    // Query the ServingCart to get items
                    List<(int ItemID, string ItemName, string OrderType, int Price, int Qty)> cartItems = new List<(int, string, string, int, int)>();
                    string sqlQuery = "SELECT * FROM ServingCart";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader3 = command.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                // Collect values into list for later processing
                                int ID = reader3.GetOrdinal("ItemID");
                                int ItemID = reader3.IsDBNull(ID) ? 0 : reader3.GetInt32(ID);

                                string ItemName = reader3["ItemName"].ToString();
                                string OrderType = reader3["OrderType"].ToString();

                                int priceIndex = reader3.GetOrdinal("OrderPrice");
                                int Price = reader3.IsDBNull(priceIndex) ? 0 : reader3.GetInt32(priceIndex);

                                int StockQuantity = reader3.GetOrdinal("OrderQty");
                                int qty = reader3.IsDBNull(StockQuantity) ? 0 : reader3.GetInt32(StockQuantity);

                                cartItems.Add((ItemID, ItemName, OrderType, Price, qty));
                            }
                        }
                    }

                    // Now that the reader is closed, insert items into SalesItemTbl
                    foreach (var item in cartItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO SalesItemTbl(ItemID,TransactionID,ItemName,ItemQuantity,ItemType,ItemPrice) VALUES (@ItemID,@TransID,@Name,@Qty,@Type,@Price);", con))
                        {
                            cmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                            cmd.Parameters.AddWithValue("@TransID", TransId);
                            cmd.Parameters.AddWithValue("@Name", item.ItemName);
                            cmd.Parameters.AddWithValue("@Qty", item.Qty);
                            cmd.Parameters.AddWithValue("@Type", item.OrderType);
                            cmd.Parameters.AddWithValue("@Price", item.Price);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on TransactionItemsInput(): " + ex.Message);
            }

        }
        public void TransactionItemsInputs()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    int TransId;
                    con.Open();
                    using (SqlCommand TransactionID = new SqlCommand("Select TransactionID from transactionstbl where Status = 'Processing'  and CustomerName = '" + CustName_txtbox.Text + "';", con))
                    {
                        TransId = (int)TransactionID.ExecuteScalar();
                        MessageBox.Show(TransId.ToString());
                    }


                    //deduction of items ordered in Cart
                    int CartCounter;
                    using (SqlCommand CartItems = new SqlCommand("Select Count(*) from ServingCart", con))
                    {
                         CartCounter= (int)CartItems.ExecuteScalar();

                        int counter = int.Parse(counterlbl.Text);
                        if (CartCounter != counter)
                        {
                            label4.Text = CartCounter.ToString();
                        }

                    }

                        CartItems[] inv = new CartItems[CartCounter];
                        string sqlQuery = "SELECT * FROM ServingCart";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader3 = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader3.Read() && index < inv.Length)
                                {
                                    int ID = reader3.GetOrdinal("ItemID");
                                    int ItemID = reader3.IsDBNull(ID) ? 0 : reader3.GetInt32(ID);

                                    String ItemName = reader3["ItemName"].ToString();
                                    String OrderType = reader3["OrderType"].ToString();

                                    int priceIndex = reader3.GetOrdinal("OrderPrice");
                                    int Price = reader3.IsDBNull(priceIndex) ? 0 : reader3.GetInt32(priceIndex);

                                    int StockQuantity = reader3.GetOrdinal("OrderQty");
                                    int qty = reader3.IsDBNull(StockQuantity) ? 0 : reader3.GetInt32(StockQuantity);

                                    cmd = new SqlCommand("INSERT INTO SalesItemTbl(ItemID,TransactionID,ItemName,ItemQuantity,ItemType,ItemPrice )Values" +
                                                "(@ItemID,@TransID,@Name,@Qty,@Type,@Price);", con);

                                    cmd.Parameters.AddWithValue("@Name", ItemName);
                                    cmd.Parameters.AddWithValue("@Qty", qty);
                                    cmd.Parameters.AddWithValue("@Type", OrderType);
                                    cmd.Parameters.AddWithValue("@Price", Price);
                                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                                    cmd.Parameters.AddWithValue("@TransID", TransId);


                                    cmd.ExecuteNonQuery();


                                    index++;
                                }
                            }
                        }

                    
                }

                

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error on TransactionItemsInput() : " + ex.Message);
            }
        }

        public void TransactionTableInput()
        {
            try
            {
               using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO TransactionsTbl(CustomerName,Discount,Price,Status,PaymentStatus,DateOfTransaction,Employee, OrderStatus)Values" +
                                "(@CustName,@Discount,@Price,@Status,@PaymentStatus,getdate(),@Employee,'On-Process');", con);
                    cmd.Parameters.AddWithValue("@CustName", CustName_txtbox.Text);
                    cmd.Parameters.AddWithValue("@Discount", 0);
                    cmd.Parameters.AddWithValue("@Price", Amount_lbl.Text);
                    cmd.Parameters.AddWithValue("@Status", "Payment");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Unpaid");
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Transaction Successful!");
                }

               
            }
            catch (Exception ex)
            {
                
                MessageBox.Show ("Error TransactionTableInput() : "+ex.Message);
                
            }
        }
    
        public void CheckCustomerName()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where CustomerName='" + CustName_txtbox.Text + "' AND Status != 'Completed' AND Status != 'Cancelled';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        if (rowCount > 0)
                        {
                            NameIndicator.Text = "Name Taken!";
                        }
                        else if (rowCount == 0)
                        {
                            NameIndicator.Text = "Name Available!";
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error on CheckCustomerName() : " + ex.Message);
            }
        }

        private void CustName_txtbox_TextChanged(object sender, EventArgs e)
        {
            if(CustName_txtbox.Text.Length > 0)
            {   
                
                CheckCustomerName();
                NameIndicator.Visible = true;
            }
            else
            {
                NameIndicator.Text = "Null";
                NameIndicator.Visible = false;
            }
        }

        private void counterlbl_Click(object sender, EventArgs e)
        {

        }

        private void counterlbl_TextChanged(object sender, EventArgs e)
        {
            int counter = int.Parse(counterlbl.Text);
            if (counter > 0 && FormIsReady == true)
            {
                OrderPlacement.instance.lbl.Text = counterlbl.Text;
                getlist();
               
            }
            else if(counter == 0)
            {
                OrderPlacement.instance.lbl.Text = counterlbl.Text;
                flowLayoutPanel1.Controls.Clear();
                MessageBox.Show("There are no items in the cart");
                OrderPlacement.instance.cartlist.Enabled = true;
                this.Close();
            }
            
        }

        private void ReviewOrder_Load(object sender, EventArgs e)
        {

        }
    }

}

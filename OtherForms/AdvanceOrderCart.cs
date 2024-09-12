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

namespace Flowershop_Thesis.OtherForms
{
    public partial class AdvanceOrderCart : Form
    {
        SqlConnection con;
        SqlConnection con2;

        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public AdvanceOrderCart()
        {
            InitializeComponent();
            testConnection();
            getCartList();

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
                    con2 = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void getCartList()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from Advance_ServingCart;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    int cartlbl = int.Parse(CounterLbl.Text);
                    if (rowCount != cartlbl)
                    {
                        CounterLbl.Text = rowCount.ToString();
                    }


                    Adv_CartItems[] inv = new Adv_CartItems[rowCount];
                    string sqlQuery = "SELECT * FROM Advance_ServingCart;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new Adv_CartItems();
                                inv[index].ItemID = reader["ItemID"].ToString();
                                inv[index].Name = reader["ItemName"].ToString();

                                int priceIndex = reader.GetOrdinal("OrderPrice");
                                inv[index].Price = reader.IsDBNull(priceIndex) ? 0 : reader.GetInt32(priceIndex);
                                int CI = reader.GetOrdinal("CartID");
                                inv[index].cartID = reader.IsDBNull(CI) ? 0 : reader.GetInt32(CI);
                                int StockQuantity = reader.GetOrdinal("OrderQty");
                                inv[index].qty = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

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

                MessageBox.Show("Error on CartLsit() : " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void proceedorder()
        {

            if (NameIndicator.Text.Equals("Name Available!"))
            {
                //if (AdvancePayment_Checkbox.Checked)
                //{
                //    if (Change_lbl.Text.Equals("Payment insufficient") || Change_lbl.Text.Equals("No Payment"))
                //    {
                //        MessageBox.Show("Please Input a sufficient Payment");
                //    }
                //    else
                //    {
                //        int change = int.Parse(Change_lbl.Text);
                //        if (change >= 0)
                //        {
                //            if (CashOption.Checked)
                //            {
                //                TransactionTableInput();
                //                TransactionItemsInput();
                //                DeductionOfItemInventory();
                //                this.Close();
                //                OrderPlacement.instance.cartlist.Enabled = true;

                //            }
                //            else if (GcashOption.Checked)
                //            {
                //                TransactionTableInput();
                //                TransactionItemsInput();
                //                DeductionOfItemInventory();
                //                this.Close();
                //                OrderPlacement.instance.cartlist.Enabled = true;

                //            }
                //            else
                //            {
                //                MessageBox.Show("Please Select Payment Method!");
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    TransactionTableInput();
                //    TransactionItemsInput();
                //    DeductionOfItemInventory();
                //    OrderPlacement.instance.cartlist.Enabled = true;
                //    this.Close();
                //}

            }
            else
            {
                MessageBox.Show("Please Input a valid Customer Name");
            }
        }

        public void DeductionOfItemInventory()
        {
            try
            {

                //deduction of items ordered in Cart
                con.Open();
                using (SqlCommand CartItems = new SqlCommand("Select Count(*) from ServingCart", con))
                {
                    int CartCounter = (int)CartItems.ExecuteScalar();

                    int counter = int.Parse(CounterLbl.Text);
                    if (CartCounter != counter)
                    {
                        label4.Text = CartCounter.ToString();
                    }



                    CartItems[] inv = new CartItems[CartCounter];
                    string sqlQuery = "SELECT * FROM ServingCart";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                int ID = reader.GetOrdinal("ItemID");
                                int ItemID = reader.IsDBNull(ID) ? 0 : reader.GetInt32(ID);

                                int StockQuantity = reader.GetOrdinal("OrderQty");
                                int qty = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

                                string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @SID;";
                                con2.Open();
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con2))
                                {

                                    updateCommand.Parameters.AddWithValue("@Quantity", qty);
                                    updateCommand.Parameters.AddWithValue("@SID", ItemID);

                                    updateCommand.ExecuteNonQuery();

                                }
                                con2.Close();

                                index++;
                            }
                        }
                    }

                }

                con.Close();


                con.Open();  //deletion of cart items
                cmd = new SqlCommand("TRUNCATE TABLE ServingCart", con);
                cmd.ExecuteNonQuery();
                con.Close();



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
                int TransId;
                con.Open();
                using (SqlCommand TransactionID = new SqlCommand("Select TransactionID from transactionstbl where Status = 'Processing' and CustomerName = '" + CustNameTxtbox.Text + "';", con))
                {
                    TransId = (int)TransactionID.ExecuteScalar();
                    MessageBox.Show(TransId.ToString());
                }
                con.Close();

                //deduction of items ordered in Cart
                con.Open();
                using (SqlCommand CartItems = new SqlCommand("Select Count(*) from ServingCart", con))
                {
                    int CartCounter = (int)CartItems.ExecuteScalar();

                    int counter = int.Parse(CounterLbl.Text);
                    if (CartCounter != counter)
                    {
                        label4.Text = CartCounter.ToString();
                    }



                    CartItems[] inv = new CartItems[CartCounter];
                    string sqlQuery = "SELECT * FROM ServingCart";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                int ID = reader.GetOrdinal("ItemID");
                                int ItemID = reader.IsDBNull(ID) ? 0 : reader.GetInt32(ID);

                                String ItemName = reader["ItemName"].ToString();
                                String OrderType = reader["OrderType"].ToString();

                                int priceIndex = reader.GetOrdinal("OrderPrice");
                                int Price = reader.IsDBNull(priceIndex) ? 0 : reader.GetInt32(priceIndex);

                                int StockQuantity = reader.GetOrdinal("OrderQty");
                                int qty = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);


                                con2.Open();
                                cmd = new SqlCommand("INSERT INTO SalesItemTbl(ItemID,TransactionID,ItemName,ItemQuantity,ItemType,ItemPrice)Values" +
                                            "(@ItemID,@TransID,@Name,@Qty,@Type,@Price);", con2);

                                cmd.Parameters.AddWithValue("@Name", ItemName);
                                cmd.Parameters.AddWithValue("@Qty", qty);
                                cmd.Parameters.AddWithValue("@Type", OrderType);
                                cmd.Parameters.AddWithValue("@Price", Price);
                                cmd.Parameters.AddWithValue("@ItemID", ItemID);
                                cmd.Parameters.AddWithValue("@TransID", TransId);


                                cmd.ExecuteNonQuery();
                                con2.Close();


                                index++;
                            }
                        }
                    }

                }

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on TransactionItemsInput() : " + ex.Message);
            }
        }

        public void TransactionTableInput()
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("INSERT INTO TransactionsTbl(CustomerName,Discount,Price,Status,PaymentStatus,PaymentMethod,DateOfTransaction,Employee)Values" +
                            "(@CustName,@Discount,@Price,@Status,@PaymentStatus,@PaymentMethod,getdate(),@Employee);", con);
                cmd.Parameters.AddWithValue("@CustName", CustNameTxtbox.Text);
                cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(DiscountTxtbox.Text));
                cmd.Parameters.AddWithValue("@Price", TotalAmountLbl.Text);
                cmd.Parameters.AddWithValue("@Status", "Processing");
                cmd.Parameters.AddWithValue("@Employee", "Alexis Kent");




                    if (CashOption.Checked)
                    {
                        cmd.Parameters.AddWithValue("@PaymentMethod", "Cash");
                    }
                    else if (GcashOption.Checked)
                    {
                        cmd.Parameters.AddWithValue("@PaymentMethod", "GCash");
                    }
                

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Transaction Successful!");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error TransactionTableInput() : " + ex.Message);

            }
        }

        public void CheckCustomerName()
        {
            try
            {
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where CustomerName='" + CustNameTxtbox.Text + "' AND Status != 'Completed' AND Status != 'Cancelled';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    // cmd.Parameters.AddWithValue("@CustName", CustName_txtbox.Text);
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




                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on CheckCustomerName() : " + ex.Message);
            }
        }
    }
}

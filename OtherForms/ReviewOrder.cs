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

namespace Flowershop_Thesis.OtherForms
{
    public partial class ReviewOrder : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IH4V487\\SQLEXPRESS;Initial Catalog=CapstoneProject;Integrated Security=True");
        SqlConnection con2 = new SqlConnection("Data Source=DESKTOP-IH4V487\\SQLEXPRESS;Initial Catalog=CapstoneProject;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public static ReviewOrder instance;
        public System.Windows.Forms.Label counter;
        public System.Windows.Forms.Button Proceed;
        public System.Windows.Forms.Button Cancel;

        
        public ReviewOrder()
        {
            InitializeComponent();
            testConnection();
            instance = this;
            counter = counterlbl;
            Proceed = ProceedBtn; 
            Cancel = CancelBtn;

            Payment_txtbox.Enabled = false;
            getlist();
            NameIndicator.Visible = false;
            CashOption.Enabled = false;
            GcashOption.Enabled = false;
           
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
        private void getlist()
        {
          
            try
            {   
                flowLayoutPanel1.Controls.Clear();
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
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new ReviewOrderList();
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
                Console.WriteLine("Error Fetching List: " + ex.Message);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (AdvancePayment_Checkbox.Checked)
            {
                Payment_txtbox.Enabled = true;
                CashOption.Enabled = true;
                GcashOption.Enabled = true;
            }
            else
            {
               Payment_txtbox.Enabled=false;
                CashOption.Enabled=false;
                GcashOption.Enabled=false;
            }
        }
        
        public void TotalAmountComputation()
        {

            if (AdvancePayment_Checkbox.Checked)
            {
                if (Discount_txtbox.Text.Length > 0) //Check if the discount area are filled
                {   
                    if(Payment_txtbox.Text.Length > 0)
                    {
                        int perc = int.Parse(Discount_txtbox.Text); //Discount Percentage
                        int payment = int.Parse(Payment_txtbox.Text); //Payment

                        int TotalAmount = int.Parse(Amount_lbl.Text);
                        int change;

                        if (perc > 0 && payment > 0) //if both fields payment and discount is present or active
                        {


                            double discount = perc * .01;

                            double less = TotalAmount * discount;
                            Discount_lbl.Text = less.ToString();
                            double DiscountedPrice = TotalAmount - less;

                            change = payment - (int)DiscountedPrice;

                            if (change < 0)
                            {
                                Change_lbl.Font = new Font(Change_lbl.Font.FontFamily, 8);
                                Change_lbl.Text = "Payment insufficient";
                            }
                            else
                            {
                                Change_lbl.Font = new Font(Change_lbl.Font.FontFamily, 11);
                                Change_lbl.Text = change.ToString();
                            }

                        }
                        else if (payment > 0 && perc == 0) //if payment is the only one present
                        {

                            change = payment - TotalAmount;

                            if (change < 0)
                            {
                                Change_lbl.Font = new Font(Change_lbl.Font.FontFamily, 8);
                                Change_lbl.Text = "Payment insufficient";
                            }
                            else
                            {
                                Change_lbl.Font = new Font(Change_lbl.Font.FontFamily, 11);
                                Change_lbl.Text = change.ToString();
                            }
                        }
                    }


                }
                else
                {
                    //none
                }
            }
            else // If advance payment is not active
            {
                int perc = int.Parse(Discount_txtbox.Text);
                if (perc > 0)
                {
                    int TotalAmount = int.Parse(Amount_lbl.Text);
                    double discount = perc * .01;
                    double less = TotalAmount * discount;

                }
            }



        }
        public void DiscountComputation()
        {
            int perc = int.Parse(Discount_txtbox.Text);
            if (perc > 50 )
            {
                MessageBox.Show("Discount input is higher than max discount value");
            }
            else if(perc >= 0  && perc <= 50) 
            {
                int Amount = int.Parse(Amount_lbl.Text);
                double discount = perc * .01;
                double less = Amount * discount;
                Discount_lbl.Text = less.ToString();
                double DiscountedPrice = Amount - less;
                TotalAmountlbl.Text = DiscountedPrice.ToString();

            }
            else
            {
                MessageBox.Show("Invalid Discount Value");
            }


        }
        private void Discount_txtbox_TextChanged(object sender, EventArgs e)
        {
            if(Discount_txtbox.Text.Length > 0)
            {   
                DiscountComputation();
                TotalAmountComputation();
            }

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
                            Amount_lbl.Text = reader["TotalPrice"].ToString();
                        }
                    }
                    

                    if(Discount_txtbox.Text.Length == 0)
                    {
                        Discount_lbl.Text = "0";
                        TotalAmountlbl.Text = Amount_lbl.Text;
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on GetPrice() : " + ex.Message);
            }

        }
        private void Payment_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (Discount_txtbox.Text.Length > 0)
            {
                TotalAmountComputation();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (NameIndicator.Text.Equals("Name Available!"))
            {
                if (AdvancePayment_Checkbox.Checked)
                {
                    if (Change_lbl.Text.Equals("Payment insufficient") || Change_lbl.Text.Equals("No Payment"))
                    {
                        MessageBox.Show("Please Input a sufficient Payment");
                    }
                    else
                    {
                        int change = int.Parse(Change_lbl.Text);
                        if (change >= 0)
                        {
                            if (CashOption.Checked)
                            {
                                TransactionTableInput();
                                TransactionItemsInput();
                                DeductionOfItemInventory();
                                this.Close();
                                OrderPlacement.instance.cartlist.Enabled = true;
                                
                            }
                            else if (GcashOption.Checked)
                            {
                                TransactionTableInput();
                                TransactionItemsInput();
                                DeductionOfItemInventory();
                                this.Close();
                                OrderPlacement.instance.cartlist.Enabled = true;
                               
                            }
                            else
                            {
                                MessageBox.Show("Please Select Payment Method!");
                            }
                        }
                    }
                }
                else
                {
                    TransactionTableInput();
                    TransactionItemsInput();
                    DeductionOfItemInventory();
                    OrderPlacement.instance.cartlist.Enabled = true;
                    this.Close();
                }
                
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

                    int counter = int.Parse(counterlbl.Text);
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
                using (SqlCommand TransactionID = new SqlCommand("Select TransactionID from transactionstbl where Status = 'Processing' and CustomerName = '" + CustName_txtbox.Text + "';", con))
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

                    int counter = int.Parse(counterlbl.Text);
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

                                //string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @SID;";
                                //con2.Open();
                                //using (SqlCommand updateCommand = new SqlCommand(updateQuery, con2))
                                //{

                                //    updateCommand.Parameters.AddWithValue("@Quantity", qty);
                                //    updateCommand.Parameters.AddWithValue("@SID", ItemID);

                                //    updateCommand.ExecuteNonQuery();

                                //}
                                //con2.Close();

                                index++;
                            }
                        }
                    }

                }

                con.Close();
                

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
               
                con.Open();
                cmd = new SqlCommand("INSERT INTO TransactionsTbl(CustomerName,Discount,Price,Status,PaymentStatus,PaymentMethod,DateOfTransaction,Employee)Values" +
                            "(@CustName,@Discount,@Price,@Status,@PaymentStatus,@PaymentMethod,getdate(),@Employee);", con);
                cmd.Parameters.AddWithValue("@CustName", CustName_txtbox.Text);
                cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(Discount_txtbox.Text));
                cmd.Parameters.AddWithValue("@Price", TotalAmountlbl.Text);
                cmd.Parameters.AddWithValue("@Status", "Processing");
                cmd.Parameters.AddWithValue("@Employee", "Alexis Kent");

                if(AdvancePayment_Checkbox.Checked )
                {   
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");


                    if(CashOption.Checked)
                    {
                        cmd.Parameters.AddWithValue("@PaymentMethod", "Cash");
                    }else if(GcashOption.Checked)
                    {
                        cmd.Parameters.AddWithValue("@PaymentMethod", "GCash");
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Unpaid");
                    cmd.Parameters.AddWithValue("@PaymentMethod", "Unknown");
                }
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Transaction Successful!");
               
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
                con.Open();
                string countQuery = "select count(*) from TransactionsTbl where CustomerName='"+CustName_txtbox.Text+"' AND Status != 'Completed' AND Status != 'Cancelled';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                   // cmd.Parameters.AddWithValue("@CustName", CustName_txtbox.Text);
                    int rowCount = (int)countCommand.ExecuteScalar();
                    if (rowCount > 0)
                    {
                        NameIndicator.Text = "Name Taken!";
                    }
                    else if(rowCount == 0)
                    {
                        NameIndicator.Text = "Name Available!";
                    }


                }




                con.Close();
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
            if (counter > 0)
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
    }

}

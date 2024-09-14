using Flowershop_Thesis.SalesClerk.Order_Placement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Flowershop_Thesis.MainForms;
using Flowershop_Thesis.SalesClerk.Transaction;
using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.AdvanceOrder;
using Flowershop_Thesis.OtherForms.PaymentConfirmation;

namespace Flowershop_Thesis.OtherForms
{
    public partial class AdvanceOrderCart : Form
    {
        SqlConnection con;
        SqlConnection con2;

        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static AdvanceOrderCart instance;
        public Label emplbl;
        public AdvanceOrderCart()
        {
            InitializeComponent();
            getEmployeeName();
            EmployeeName.Text = UserInfo.Empleyado;
            instance = this;
            emplbl = EmployeeName;
            testConnection();
           // getcounter();
            getCartList();
            getPrice();
            label21.Visible = false;
            DiscountTxtbox.Enabled = false;
            
            long date = DateTime.Now.Day;
            long month = DateTime.Now.Month;
            long year = DateTime.Now.Year;
            long tom = date + 1;
            MessageBox.Show(tom +"/"+month+"/"+year);
            string tommdate = tom + "," + month + "," + year;
            PickupDate.MinDate = new DateTime((int)year,(int)month,(int)tom);
            PickupDate.Value = new DateTime((int)year, (int)month, (int)tom);

            computetotal();
        }
        string EmpNameVal;
        public void getEmployeeName()
        {
            SalesClerk_BasePlatform SB = new SalesClerk_BasePlatform();
            EmpNameVal = SB.empName;
            EmployeeName.Text = SB.empName;
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

        double totalValue=0;
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
        public void getcounter()
        {

            try
            {

                string countQuery = "select count(*) from Advance_ServingCart;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    con.Open();
                    int rowCount = (int)countCommand.ExecuteScalar();
                    int cartlbl = int.Parse(CounterLbl.Text);
                    if (rowCount != cartlbl)
                    {
                        CounterLbl.Text = rowCount.ToString();
                    }






                    con.Close();
                }







            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on cart number : " + ex.Message);
            }

        }
        private void getPrice()
        {
            


            try
            {
                string countQuery = "SELECT SUM(OrderPrice) AS Total FROM Advance_ServingCart;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    con.Open();
                    int amount = (int)countCommand.ExecuteScalar();
                    TotalLbl.Text = amount.ToString();  
                    
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on getting total amount : " + ex.Message);
            }


        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //paymentcheck();
            //OrderIdentityCheck();

            //SalesClerk_BasePlatform SB = new SalesClerk_BasePlatform();
            //string EmpName = SalesClerk_BasePlatform.instance.EMP.Text;

            //if(NameIndicator.Text.Equals("Available") && label21.Text !=  "Insufficient Payment" && label21.Visible == true && textBox1.Text.Length == 11)
            //{
            //    MessageBox.Show("Order Placed \n" + "Customer Name :" + CustomerName + "\n" + "Total Amount of :" + TotalAmountLbl.Text + "\n" + "DownPayment of (30%) :" + DownpaymentLbl.Text + "\n" + "Paid with" + paymentmethod + "\n" + "---------------------"+ "\n"+ "Pickup Date : "+ PickupDate.Text + "\n" + "Pickup Time :" + pickuptime + "\n" + "Employee Name : " + EmployeeName.Text);
            //}
            //else
            //{
            //    MessageBox.Show("Please fill the the required fields");
            //}


            try
            {
                if(NameIndicator.Text.Equals("Name Taken!"))
                {
                    MessageBox.Show("Name Taken Please Input Another");
                }
                else if(OrderType.SelectedIndex < 0)
                {
                    MessageBox.Show("Please Select An Order Type");
                }
                else if(textBox1.Text.Length < 11) 
                {
                    MessageBox.Show("Please Check Contact Number");
                }
                else if(GcashOption.Checked == false && CashOption.Checked == false)
                {
                    MessageBox.Show("Wala kang balak magbayad?!");
                }
                else if(DiscountCheckBox.Checked && DiscountTxtbox.Text.Equals("0"))
                {
                    MessageBox.Show("Kung wala namang Laman yung discount i uncheck mo!");
                }
                else if(PaymentTxtBox.Text.Equals("0") && CashOption.Checked) 
                {
                    MessageBox.Show("Ano Di ka magbabayad?");
                }
                else if(GcashOption.Checked == true && paymentStatus == "Unpaid")
                {
                   if(PaymentConfirm.isPaid == true) 
                    {
                        paymentStatus = "Paid";
                    }
                   else if (PaymentConfirm.isPaid == false)
                    {
                        ConfirmPayment PC = new ConfirmPayment();
                        PC.Show();

                    }
                }
                else if (TotalAmountLbl.Text.Equals("0"))
                {
                    MessageBox.Show("Ano babayaran mo? TAnga!");
                }
                else if (DownpaymentLbl.Text.Equals("0"))
                {
                    MessageBox.Show("Ano babayaran mo nga? TAnga!");
                }
                else
                {
                    runthishit();
                }

            }
            catch(Exception ex)
            {

            }
        }

        public void runthishit()
        {   
            
            if(CashOption.Checked == true && GcashOption.Checked == false)
            {
                CreateAdvanceOrder.CustomerName = CustNameTxtbox.Text;
                CreateAdvanceOrder.TotalAmount = TotalAmountLbl.Text;
               
                CreateAdvanceOrder.Date = DateTime.Today.ToString();
                CreateAdvanceOrder.Downpayment = DownpaymentLbl.Text;
                CreateAdvanceOrder.OrderType = OrderType.Text;
                CreateAdvanceOrder.PickUpDate = PickupDate.Text;
                CreateAdvanceOrder.ContactNumber = textBox1.Text;

                CashOrder CO = new CashOrder();
                CO.Show();
            }
            if(CashOption.Checked == false && GcashOption.Checked == true)
            {
                CreateAdvanceOrder.CustomerName = CustNameTxtbox.Text;
                CreateAdvanceOrder.TotalAmount = TotalAmountLbl.Text;
      
                CreateAdvanceOrder.Date = DateTime.Today.ToString();
                CreateAdvanceOrder.Downpayment = DownpaymentLbl.Text;
                CreateAdvanceOrder.OrderType = OrderType.Text;
                CreateAdvanceOrder.PickUpDate = PickupDate.Text;
                CreateAdvanceOrder.ContactNumber = textBox1.Text;
                GcashOrderFrm G = new GcashOrderFrm();
                G.Show();
               
                
            }
            





            //MessageBox.Show("Order Placed \n" + "Customer Name :" + CustNameTxtbox.Text + "\n" +"Contact Number :" + textBox1.Text +"\n"+ "Total Amount of :" + TotalAmountLbl.Text + "\n" + "DownPayment of (30%) :" + DownpaymentLbl.Text + "\n" + "Paid with" + paymentmethod + "\n" + "---------------------" + "\n" + "Pickup Date : " + PickupDate.Text + "\n"  + "Employee Name : " + EmployeeName.Text);
        }
        int discount = 0;
        string paymentmethod;
        int payment;
        string paymentStatus;
        string pickuptime;
        string orderType;
        string CustomerName;
        public void paymentcheck()
        {
            if(DiscountCheckBox.Checked)
            {   
                if(int.Parse(DiscountLbl.Text) > 0  && DiscountLbl.Enabled) {

                    discount = int.Parse(DiscountLbl.Text);

                }
                
            }
            if(CashOption.Checked == true && GcashOption.Checked == false)
            {
                paymentmethod = "Cash";

                if (int.Parse(PaymentTxtBox.Text) < int.Parse(TotalAmountLbl.Text))
                {
                    MessageBox.Show("Please Input a Valid Payment Amount");
                }
                else
                {
                    paymentStatus = "Paid";
                }
            }
            else if (CashOption.Checked == false && GcashOption.Checked == true)
            {
                paymentmethod = "GCash";
                DialogResult result = MessageBox.Show(
                "Do you receieved the downpayment amounting of "+ DownpaymentLbl.Text + " ?",     // Message text
                "Payment Confirmation",                // Title of the message box
                MessageBoxButtons.YesNo,       // Buttons to display
                MessageBoxIcon.Question        // Icon to display
                );

                // Handle the result
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Payment Recieved!");
                    paymentStatus = "Paid";
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("Please Pay the Downpayment before proceeding the order");
                }
            }
            else
            {
                MessageBox.Show("Please Insert Payment Method!");
            }

            


        }

        public void OrderIdentityCheck()
        {
            if(OrderType.Text.Equals("Advance Order"))
            {
                orderType = "Advance Order";
            }
            else if (OrderType.Text.Equals("Events"))
            {
                orderType = "Event";
            }
            else
            {
                MessageBox.Show("You must select and Order type");
            }
            if(NameIndicator.Text == "Available")
            {
                CustomerName = CustNameTxtbox.Text;
            }
            else
            {
                MessageBox.Show("Name Taken Please Input a new name!");
            }


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
                using (SqlCommand TransactionID = new SqlCommand("Select TransactionID from transactionstbl where Status = 'Processing' and CustomerName = '" + PaymentTxtBox.Text + "';", con))
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
                cmd.Parameters.AddWithValue("@CustName", PaymentTxtBox.Text);
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
                string countQuery = "select count(*) from TransactionsTbl where CustomerName='" + PaymentTxtBox.Text + "' AND Status != 'Completed' AND Status != 'Cancelled';";
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

        private void OrderType_SelectedIndexChanged(object sender, EventArgs e)
        {   
            double dp = int.Parse(TotalAmountLbl.Text) * .3; 
            DownpaymentLbl.Text = dp.ToString();
        }

        private void DiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {   
            if(DiscountCheckBox.Checked)
            {
                DiscountTxtbox.Enabled = true;
            }
            else
            {
                DiscountTxtbox.Text = "0";
                DiscountLbl.Text = "0";
                DiscountTxtbox.Enabled = false;
            }
           
        }

        private void DiscountTxtbox_TextChanged(object sender, EventArgs e)
        {
            if(DiscountTxtbox.Text.Length > 0 )
            {
                int indc = int.Parse(DiscountTxtbox.Text);
                if (indc <= 50)
                {

   
                    // discount = int.Parse(TotalLbl.Text) * indc;
                    if (PaymentTxtBox.Enabled == false)
                    {
                        string inp = DiscountTxtbox.Text;
                        double dc = .01 * double.Parse(inp);
                        double finaldc = dc * int.Parse(TotalLbl.Text);
                        DiscountLbl.Text = finaldc.ToString();

                        PaymentTxtBox.Text = DownpaymentLbl.Text;
                        MessageBox.Show("Condition 1 running!");
                    }
                    else
                    {
                        string inp = DiscountTxtbox.Text;
                        double dc = .01 * double.Parse(inp);
                        double finaldc = dc * int.Parse(TotalLbl.Text);
                        DiscountLbl.Text = finaldc.ToString();
                        MessageBox.Show("Condition 2 running!");
                    }
                }
                else
                {
                    DiscountTxtbox.Text = "0";
                    MessageBox.Show("Discount exceeds the 50%");
                    
                }


            }
        }

        private void DiscountTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void DiscountLbl_TextChanged(object sender, EventArgs e)
        {
            computetotal();
            if(OrderType.SelectedIndex >= 0)
            {
                PaymentTxtBox.Text = "0";
                double dp = int.Parse(TotalAmountLbl.Text) * .3;
                DownpaymentLbl.Text = dp.ToString();
            }
    
        }
        public void computetotal()
        {

            double dc = 0;
            
            if(DiscountTxtbox.Text.Length > 0 )
            {
                dc = double.Parse(DiscountTxtbox.Text);
            }
           
            double amount = double.Parse(TotalLbl.Text);

            TotalAmountLbl.Text = (amount - dc).ToString();
        }

        private void PaymentTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void PaymentTxtBox_TextChanged(object sender, EventArgs e)
        {

            TotalAmountComputation();
        }
        

        public void TotalAmountComputation()
        {

            if (CashOption.Checked)
            {
                if (DiscountTxtbox.Text.Length > 0) //Check if the discount area are filled
                {
                    if (PaymentTxtBox.Text.Length > 0)
                    {
                        int perc = int.Parse(DiscountTxtbox.Text); //Discount Percentage
                        int payment = int.Parse(PaymentTxtBox.Text); //Payment

                        int TotalAmount = int.Parse(TotalAmountLbl.Text);
                        int change;

                        if (perc > 0 && payment > 0) //if both fields payment and discount is present or active
                        {


                            double discount = perc * .01;

                            double less = TotalAmount * discount;
                            DiscountLbl.Text = less.ToString();
                            double DiscountedPrice = TotalAmount - less;

                            change = payment - (int)DiscountedPrice;

                            if (change < 0)
                            {
                                ChangeLbl.Text = "Payment insufficient";
                            }
                            else
                            {
                                ChangeLbl.Text = change.ToString();
                            }

                        }
                        else if (payment > 0 && DiscountCheckBox.Checked == false) //if payment is the only one present
                        {

                            change = payment - TotalAmount;

                            if (change < 0)
                            {
                                ChangeLbl.Text = "Payment insufficient";
                            }
                            else
                            {
                                ChangeLbl.Text = change.ToString();
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
                PaymentTxtBox.Text = DownpaymentLbl.Text;
            }



        }

        private void GcashOption_CheckedChanged(object sender, EventArgs e)
        {
            if(GcashOption.Checked)
            {
                paymentStatus = "Unpaid";
                PaymentTxtBox.Text = DownpaymentLbl.Text;
                PaymentTxtBox.ReadOnly = true;
                CreateAdvanceOrder.ModeOfPayment = "Gcash";
            }
            

        }

        private void CashOption_CheckedChanged(object sender, EventArgs e)
        {
            if (CashOption.Checked)
            {
                paymentmethod = "Cash";
                PaymentTxtBox.Clear();
                PaymentTxtBox.Enabled = true;
                CreateAdvanceOrder.ModeOfPayment = "Cash";

           
            }
        }

        private void DownpaymentLbl_TextChanged(object sender, EventArgs e)
        {
            if(GcashOption.Checked && CashOption.Checked == false) 
            {
                PaymentTxtBox.Text = DiscountLbl.Text;
            }
        }
    }
}

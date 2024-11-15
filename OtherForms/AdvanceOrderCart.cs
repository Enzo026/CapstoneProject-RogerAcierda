﻿using Flowershop_Thesis.SalesClerk.Order_Placement;
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
using static System.Resources.ResXFileRef;

namespace Flowershop_Thesis.OtherForms
{
    public partial class AdvanceOrderCart : Form
    {
        SqlCommand cmd = new SqlCommand();
        public static AdvanceOrderCart instance;
        public Label Loadinglbl;
        public AdvanceOrderCart()
        {
            InitializeComponent();
            instance = this;
            Loadinglbl = label22;
            EmployeeName.Text = UserInfo.Empleyado;
            // getcounter();
            getCartList();
            getPrice();
            label21.Visible = false;

            NameIndicator.Visible = false;

            long date = DateTime.Now.Day;
            long month = DateTime.Now.Month;
            long year = DateTime.Now.Year;
            long tom = date + 1;
            // MessageBox.Show(tom +"/"+month+"/"+year);
            string tommdate = tom + "," + month + "," + year;
            PickupDate.MinDate = new DateTime((int)year, (int)month, (int)tom);
            PickupDate.Value = new DateTime((int)year, (int)month, (int)tom);
            Progress.Visible = false;
            computetotal();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        double totalValue = 0;
        private void getCartList()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con =  new SqlConnection(Connect.connectionString))
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
                    }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    string countQuery = "SELECT SUM(OrderPrice) AS Total FROM Advance_ServingCart;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        con.Open();
                        int amount = (int)countCommand.ExecuteScalar();
                        TotalLbl.Text = amount.ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on getting total amount : " + ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Progress.Visible = true;
            Progress.Text = "Placing Your Order";



            try
            {
                if (NameIndicator.Text.Equals("Name Taken!"))
                {
                    MessageBox.Show("Name Taken Please Input Another");
                }
                else if (OrderType.SelectedIndex < 0)
                {
                    MessageBox.Show("Please Select An Order Type");
                }
                else if (textBox1.Text.Length < 11)
                {
                    MessageBox.Show("Please Check Contact Number");
                }
                else if (GcashOption.Checked == false && CashOption.Checked == false)
                {
                    MessageBox.Show("No Payment");
                }
                else if (PaymentTxtBox.Text.Equals("0") && CashOption.Checked)
                {
                    MessageBox.Show("No Payment");
                }
                else if (GcashOption.Checked == true && paymentStatus == "Unpaid")
                {
                    if (PaymentConfirm.isPaid == true)
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
                    MessageBox.Show("No Downpayment Value");
                }
                else if (DownpaymentLbl.Text.Equals("0"))
                {
                    MessageBox.Show("No Downpayment Value");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Place Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        runthishit();

                        if (AdvanceOrderPlacement.InitializeDone == true)
                        {
                            Progress.Text = "Intitialize Done";
                            TransactionTableInput();
                            if (AdvanceOrderPlacement.InsertAdvanceOrder == true)
                            {
                                Progress.Text = "InsertAdvance Order Done";
                                TransactionItemsInput();
                                if (AdvanceOrderPlacement.InsertAdvanceOrderItems == true)
                                {
                                    Progress.Text = "Insert Items Done";
                                    DeductionOfItemInventory();

                                    if (CashOption.Checked == true && GcashOption.Checked == false)
                                    {
                                        Progress.Text = "Order Placed";
                                        CashOrder form = new CashOrder();
                                        form.ShowDialog();
                                    }
                                    else if (CashOption.Checked == false && GcashOption.Checked == true)
                                    {
                                        Progress.Text = "Order Placed";
                                        GcashOrderFrm form = new GcashOrderFrm();
                                        form.ShowDialog();
                                    }
                                    this.Close();
                                }
                            }
                        }



                    }
                    else
                    {

                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on Placing order :" + ex.Message);
            }
        }

        public void runthishit()
        {
            try
            {
                if (CashOption.Checked == true && GcashOption.Checked == false)
                {
                    CreateAdvanceOrder.CustomerName = CustNameTxtbox.Text;
                    CreateAdvanceOrder.TotalAmount = TotalAmountLbl.Text;

                    CreateAdvanceOrder.Date = DateTime.Today.ToString();
                    CreateAdvanceOrder.Downpayment = DownpaymentLbl.Text;
                    CreateAdvanceOrder.OrderType = OrderType.Text;
                    CreateAdvanceOrder.PickUpDate = PickupDate.Text;
                    CreateAdvanceOrder.ContactNumber = textBox1.Text;

                    AdvanceOrderPlacement.InitializeDone = true;

                }
                else if (CashOption.Checked == false && GcashOption.Checked == true)
                {
                    CreateAdvanceOrder.CustomerName = CustNameTxtbox.Text;
                    CreateAdvanceOrder.TotalAmount = TotalAmountLbl.Text;
                    CreateAdvanceOrder.Date = DateTime.Today.ToString();
                    CreateAdvanceOrder.Downpayment = DownpaymentLbl.Text;
                    CreateAdvanceOrder.OrderType = OrderType.Text;
                    CreateAdvanceOrder.PickUpDate = PickupDate.Text;
                    CreateAdvanceOrder.ContactNumber = textBox1.Text;

                    AdvanceOrderPlacement.InitializeDone = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Compiling Order Failed! :" + ex.Message);
            }
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

            if (CashOption.Checked == true && GcashOption.Checked == false)
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
                "Do you receieved the downpayment amounting of " + DownpaymentLbl.Text + " ?",     // Message text
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
            if (OrderType.Text.Equals("Advance Order"))
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
            if (NameIndicator.Text == "Available")
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();  //deletion of cart items
                    cmd = new SqlCommand("TRUNCATE TABLE Advance_ServingCart", con);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on  DeductionOfCartItems : " + ex.Message);
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

                    // Retrieve the active order's OrderID
                    using (SqlCommand TransactionID = new SqlCommand("Select OrderID from AdvanceOrders where Status = 'Active' and CustomerName = @CustomerName;", con))
                    {
                        TransactionID.Parameters.AddWithValue("@CustomerName", CustNameTxtbox.Text.Trim());
                        TransId = (int)TransactionID.ExecuteScalar();
                    }

                    if (TransId != 0)
                    {
                        // Retrieve item count in the Advance_ServingCart
                        int CartCounter;
                        using (SqlCommand CartItems = new SqlCommand("Select Count(*) from Advance_ServingCart", con))
                        {
                            CartCounter = (int)CartItems.ExecuteScalar();

                            int counter = int.Parse(CounterLbl.Text);
                            if (CartCounter != counter)
                            {
                                label4.Text = CartCounter.ToString();
                            }
                        }

                        // Retrieve items from Advance_ServingCart
                        List<(int ItemID, string ItemName, string OrderType, int Price, int Quantity)> items = new List<(int, string, string, int, int)>();
                        string sqlQuery = "SELECT ItemID, ItemName, OrderType, OrderPrice, OrderQty FROM Advance_ServingCart";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int ItemID = reader.IsDBNull(reader.GetOrdinal("ItemID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ItemID"));
                                    string ItemName = reader["ItemName"].ToString();
                                    string OrderType = reader["OrderType"].ToString();
                                    int Price = reader.IsDBNull(reader.GetOrdinal("OrderPrice")) ? 0 : reader.GetInt32(reader.GetOrdinal("OrderPrice"));
                                    int Quantity = reader.IsDBNull(reader.GetOrdinal("OrderQty")) ? 0 : reader.GetInt32(reader.GetOrdinal("OrderQty"));

                                    items.Add((ItemID, ItemName, OrderType, Price, Quantity));
                                }
                            }
                        }

                        // Insert items into AdvanceOrderItems and update inventory
                        foreach (var item in items)
                        {
                            // Insert into AdvanceOrderItems
                            string insertQuery = "INSERT INTO AdvanceOrderItems (OrderID, ItemID, Name, Price, Quantity, Type) " +
                                                 "VALUES (@OrderID, @ItemID, @Name, @Price, @Qty, @Type)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                            {
                                insertCommand.Parameters.AddWithValue("@OrderID", TransId);
                                insertCommand.Parameters.AddWithValue("@ItemID", item.ItemID);
                                insertCommand.Parameters.AddWithValue("@Name", item.ItemName);
                                insertCommand.Parameters.AddWithValue("@Price", item.Price);
                                insertCommand.Parameters.AddWithValue("@Qty", item.Quantity);
                                insertCommand.Parameters.AddWithValue("@Type", item.OrderType);
                                insertCommand.ExecuteNonQuery();
                            }

                            // Update ItemInventory for each item
                            string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @ItemID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                                updateCommand.Parameters.AddWithValue("@ItemID", item.ItemID);
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        // Clear the Advance_ServingCart after operations
                        using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Advance_ServingCart", con))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        AdvanceOrderPlacement.InsertAdvanceOrderItems = true;
                    }
                    else
                    {
                        MessageBox.Show("OrderID not fetched");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on TransactionItemsInput() : " + ex.Message);
            }


        }
        Image image;
        public void TransactionTableInput()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    Image s_img = CreateAdvanceOrder.ProofOfPayment;
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));

                    // Validate inputs before conversion
                    decimal totalPrice, downpayment;
                    int discount;

                    if (!decimal.TryParse(CreateAdvanceOrder.TotalAmount, out totalPrice))
                    {
                        MessageBox.Show("Invalid Total Amount format.");
                        return;
                    }

                    if (!decimal.TryParse(CreateAdvanceOrder.Downpayment, out downpayment))
                    {
                        MessageBox.Show("Invalid Downpayment format.");
                        return;
                    }


                    con.Open();

                    cmd = new SqlCommand("INSERT INTO AdvanceOrders (CustomerName, TotalPrice, ModeOfPayment, DateOfReservation, Downpayment, Discount, OrderType, PickupDate, ContactNo, EmployeeName, Status, Image) " +
                        "VALUES (@CustomerName, @TotalPrice, @MOP, @DateOfReservation, @Downpayment, @Discount, @OrderType, @PickupDate, @ContactNo, @EmployeeName, 'Active', @Image);", con);

                    cmd.Parameters.AddWithValue("@CustomerName", CreateAdvanceOrder.CustomerName);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);  // Use validated value
                    cmd.Parameters.AddWithValue("@MOP", CreateAdvanceOrder.ModeOfPayment);
                    cmd.Parameters.AddWithValue("@DateOfReservation", DateTime.Now);  // Current datetime
                    cmd.Parameters.AddWithValue("@Downpayment", downpayment);  // Use validated value
                    cmd.Parameters.AddWithValue("@Discount", 0);  // Use validated value
                    cmd.Parameters.AddWithValue("@OrderType", CreateAdvanceOrder.OrderType);

                    DateTime date = PickupDate.Value;
                    cmd.Parameters.AddWithValue("@PickupDate", date);
                    cmd.Parameters.AddWithValue("@ContactNo", CreateAdvanceOrder.ContactNumber);
                    cmd.Parameters.AddWithValue("@EmployeeName", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@Image", ImageConvert);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Transaction Successful!");
                    AdvanceOrderPlacement.InsertAdvanceOrder = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in TransactionTableInput(): " + ex.Message);
            }


        }

        public void CheckCustomerName()
        {
            try
            {   using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from AdvanceOrders where CustomerName='" + CustNameTxtbox.Text + "' AND Status != 'Completed' AND Status != 'Cancelled';";
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
                            NameIndicator.Text = "Available";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on CheckCustomerName() : " + ex.Message);
            }
        }

        private void OrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            double dp = double.Parse(TotalAmountLbl.Text) * .3;
            DownpaymentLbl.Text = Math.Round(dp, 0).ToString();
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
            if (OrderType.SelectedIndex >= 0)
            {
                //PaymentTxtBox.Text = "0";
                double dp = int.Parse(TotalAmountLbl.Text) * .3;
                DownpaymentLbl.Text = Math.Round(dp, 0).ToString();
            }

        }
        public void computetotal()
        {

            double dc = 0;


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

            if (CashOption.Checked == true && GcashOption.Checked == false)
            {
                if (PaymentTxtBox.Text.Length > 0)
                {
                    int perc = int.Parse(DownpaymentLbl.Text.Trim()); //Discount Percentage
                    int payment = int.Parse(PaymentTxtBox.Text); //Payment

                    int TotalAmount = int.Parse(DownpaymentLbl.Text);
                    int change;

                    if (perc > 0 && payment > 0) //if both fields payment and discount is present or active
                    {

                        change = payment - TotalAmount;

                        if (change < 0)
                        {
                            label21.Visible = true;
                            label21.Text = "Payment insufficient";
                            ChangeLbl.Text = "0";

                        }
                        else
                        {
                            label21.Text = " ";
                            label21.Visible = false;
                            ChangeLbl.Text = change.ToString();
                        }

                    }
                    else if (payment > 0 ) //if payment is the only one present
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
            else if (GcashOption.Checked == true && CashOption.Checked == false)// If advance payment is not active
            {
                //  PaymentTxtBox.Text = DownpaymentLbl.Text.Trim();
            }
            else
            {
                MessageBox.Show("Select payment method");
            }



        }

        private void GcashOption_CheckedChanged(object sender, EventArgs e)
        {
            if (GcashOption.Checked == true && CashOption.Checked == false)
            {
                paymentStatus = "Unpaid";
                 PaymentTxtBox.Text = DownpaymentLbl.Text;
                PaymentTxtBox.ReadOnly = true;
                CreateAdvanceOrder.ModeOfPayment = "Gcash";
            }


        }

        private void CashOption_CheckedChanged(object sender, EventArgs e)
        {
            if (CashOption.Checked == true && GcashOption.Checked == false)
            {
                paymentmethod = "Cash";
                PaymentTxtBox.Clear();
                PaymentTxtBox.ReadOnly = false;
                CreateAdvanceOrder.ModeOfPayment = "Cash";
            }
        }

        private void DownpaymentLbl_TextChanged(object sender, EventArgs e)
        {
            if (GcashOption.Checked && CashOption.Checked == false)
            {
                PaymentTxtBox.Text = DownpaymentLbl.Text;
            }
        }

        private void CustNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            if (CustNameTxtbox.Text.Length > 0)
            {
                NameIndicator.Visible = true;
                CheckCustomerName();
            }
            else
            {
                NameIndicator.Visible = false;
                NameIndicator.Text = "NULL";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int cart = int.Parse(CounterLbl.Text);
            if (cart > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Cancel all orders in cart?", "Cancel Cart Items", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   using(SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("TRUNCATE TABLE Advance_ServingCart", con);
                        cmd.ExecuteNonQuery();
                        CounterLbl.Text = "0";
                        MessageBox.Show("Items in the cart are now cancelled!");
                        this.Close();
                    }
                }
            }
        }

        private void label22_VisibleChanged(object sender, EventArgs e)
        {
            if (label22.Visible) {
                getCartList();
                getPrice();
                label22.Visible = false;
            }
        }

        private void AdvanceOrderCart_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}

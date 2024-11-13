using Flowershop_Thesis.SalesClerk.Order_Placement;
using System;
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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.OtherForms
{
    public partial class AddToCart : Form
    {
        public static AddToCart instance;
        public AddToCart()
        {
            InitializeComponent();
            instance = this;
        }


        #region Myregion
        private string name;
        private string itemID;
        private Decimal price;
        private int stocks;
        private string OrderType;


        [Category("ItmList")]
        public int Stock
        {
            get { return stocks; }
            set { stocks = value; label5.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; label7.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        [Category("ItmList")]
        public string type
        {
            get { return OrderType; }
            set { OrderType = value; }
        }

        [Category("ItmList")]
        public string Name
        {
            get { return name; }
            set { name = value; label2.Text = value; }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

         
            if (textBox1.Text.Length > 0)
            {
                int OrderQty = int.Parse(textBox1.Text.ToString());
                int minOrder = int.Parse(label13.Text.ToString());
                if (OrderQty == 0 || OrderQty.Equals(null) || OrderQty.Equals(""))
                {
                    MessageBox.Show("Please input a quantity");
                }
                else if (OrderQty > stocks)
                {
                    MessageBox.Show("The order Quantity are higher than the available maximum order Quantity Please input equal or below the maximum");
                }
                else if(OrderQty < minOrder)
                {
                    MessageBox.Show("The order Quantity are lower than the minimum order Quantity. Please input equal or above the minimum Quantity or uncheck the bulk order checkbox");
                }
                else
                {

                    // Deduct Item in item in the inventory
                    try
                    {
                        using (SqlConnection con = new SqlConnection(Connect.connectionString))
                        {
                            con.Open();

                            // Correct the SQL query syntax
                            string query = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @input WHERE ItemID = @ID";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                // Add parameters
                                cmd.Parameters.AddWithValue("@input", textBox1.Text);  
                                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(this.ItemID));  

                                // Execute the query
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Item Inventory Deduction Failed! : " + ex.Message);
                    }






                    //Insert to cart database
                    try
                    {   
                        using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("INSERT INTO ServingCart(ItemID,ItemName,OrderQty,OrderPrice,OrderType)Values" +
                                        "(@ID,@Name,@Qty,@Price,@Type);", con);
                            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(this.ItemID));
                            cmd.Parameters.AddWithValue("@Name", this.label2.Text);
                            cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(this.textBox1.Text));
                            int cprice = (int)decimal.Parse(label6.Text);
                            cmd.Parameters.AddWithValue("@Price", cprice);
                            cmd.Parameters.AddWithValue("@Type", this.OrderType);

                            cmd.ExecuteNonQuery();

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("AddingItem Failed!" + " : " + ex);
                    }


                    //activation of textchange event to refresh the item list in main panel
                    int cart = int.Parse(OrderPlacement.instance.lbl.Text);
                    cart++;
                    MessageBox.Show("Item Successfully Added Cart Items: " + cart.ToString());
                    OrderPlacement.instance.lbl.Text = cart.ToString();
                    OrderPlacement.instance.update.Visible = true;
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Please input a quantity");
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {   
                
                int OrderQty = int.Parse(textBox1.Text.ToString());
                int minOrder = int.Parse(label13.Text.ToString());
                if (OrderQty > stocks)
                {
                    MessageBox.Show("The order Quantity are higher than the available maximum order Quantity Please input equal or below the maximum");
                    textBox1.Text = "1";
                }
                else
                {
                    decimal OrderPrice = OrderQty * price;
                    decimal discountedvalue = OrderPrice * SystemInfo.discount;
                    decimal discountedPrice = OrderPrice - discountedvalue;
                    if (checkBox1.Checked)
                    {
           
                        label6.Text = discountedPrice.ToString();
                    }
                    else
                    {
                        label6.Text = OrderPrice.ToString();
                    }

                   
                }
            }
            else
            {
                //none
                label6.Text = "0";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void AddToCart_Load(object sender, EventArgs e)
        {
            if(WalkInTransaction.OrderType == "Individual")
            {
                checkBox1.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
            }
            else
            {
                checkBox1.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            int OrderQty = int.Parse(textBox1.Text.ToString());
            if (checkBox1.Checked == true)
            {
                label13.Text = SystemInfo.MinimumOrder.ToString();
        
                if (OrderQty > SystemInfo.MinimumOrder) {
                    decimal OrderPrice = OrderQty * price;
                    decimal discountedvalue = OrderPrice * SystemInfo.discount;
                    decimal discountedPrice = OrderPrice - discountedvalue;
                    label6.Text = discountedPrice.ToString();

                }
                else
                {
                    textBox1.Text = SystemInfo.MinimumOrder.ToString();
                }
              


            }
            else
            {
                label13.Text = "0";
                decimal OrderPrice = OrderQty * price;
                label6.Text = OrderPrice.ToString();
            }
        }
    }
}

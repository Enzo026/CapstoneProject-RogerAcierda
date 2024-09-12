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

namespace Flowershop_Thesis.OtherForms
{
    public partial class AddToCart : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public static AddToCart instance;
        public AddToCart()
        {
            InitializeComponent();
            instance = this;
            testConnection();
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

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
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
                if (OrderQty == 0 || OrderQty.Equals(null) || OrderQty.Equals(""))
                {
                    MessageBox.Show("Please input a quantity");
                }
                else if (OrderQty > stocks)
                {
                    MessageBox.Show("The order Quantity are higher than the available maximum order Quantity Please input equal or below the maximum");
                }
                else
                {
                    //Insert to cart database
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("INSERT INTO ServingCart(ItemID,ItemName,OrderQty,OrderPrice,OrderType)Values" +
                                    "(@ID,@Name,@Qty,@Price,@Type);", con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(this.ItemID));
                        cmd.Parameters.AddWithValue("@Name", this.label2.Text);
                        cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(this.textBox1.Text));
                        int cprice = (int)decimal.Parse(label6.Text);
                        cmd.Parameters.AddWithValue("@Price", cprice);
                        cmd.Parameters.AddWithValue("@Type", this.OrderType);

                        cmd.ExecuteNonQuery();
                        con.Close();
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



                    //CartItems[] CI = new CartItems[cart];
                    //int index = 0;
                    //while(index < cart)
                    //{
                    //    CI[index] = new CartItems();
                    //    CI[index].Name = name;
                    //    CI[index].ItemID = itemID;
                    //    CI[index].Price = int.Parse(label6.Text);
                    //    CI[index].qty = int.Parse(textBox1.Text);
                        
                    //}

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please input a quantity");
            }
           

            //if(OrderQty >1 && OrderQty <= stocks)
            //{
            //    CartItems CI = new CartItems();
            //    CI.Name = name;
            //    CI.ItemID = itemID;
            //    CI.Price = int.Parse(label6.Text);
            //    CI.qty = int.Parse(textBox1.Text);
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid Quantity input");
            //}
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
                if(OrderQty > stocks)
                {
                    MessageBox.Show("The order Quantity are higher than the available maximum order Quantity Please input equal or below the maximum");
                }
                else
                {
                    decimal OrderPrice = OrderQty * price;
                    label6.Text = OrderPrice.ToString();
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
    }
}

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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Flowershop_Thesis.OtherForms
{
    public partial class CartItems : UserControl
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public CartItems()
        {
            InitializeComponent();
            testConnection();
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

        #region CartOrderDetails
        private string name;
        private string itemID;
        private int price;
        private int Quantity;
        private int CartID;


        [Category("CartList")]
        public int qty
        {
            get { return Quantity; }
            set { Quantity = value; label8.Text = value.ToString(); }
        }
        [Category("CartList")]
        public int Price
        {
            get { return price; }
            set { price = value; label10.Text = value.ToString(); }
        }
        [Category("CartList")]
        public int cartID
        {
            get { return CartID; }
            set { CartID = value;}
        }
        [Category("CartList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("CartList")]
        public string Name
        {
            get { return name; }
            set { name = value; label7.Text = value; }
        }
        #endregion

        private void button14_Click(object sender, EventArgs e)
        {
           
            int cartqty = int.Parse(OrderPlacement.instance.lbl.Text);
            if(cartqty <= 0)
            {
                MessageBox.Show("No more items in cart");
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Delete from ServingCart where CartID = " + CartID + " ;", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    int cart = cartqty - 1;
                    if (cart == 0)
                    {
                        OrderPlacement.instance.lbl.Text = cart.ToString();
                    }
                    else if (cart > 0)
                    {
                        this.Parent.Controls.Remove(this);
                        OrderPlacement.instance.lbl.Text = cart.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }



            }
           
        }
    }
}

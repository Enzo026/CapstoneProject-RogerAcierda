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

namespace Flowershop_Thesis.OtherForms
{
    public partial class ReviewOrderList : UserControl
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public ReviewOrderList()
        {
            InitializeComponent();
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
        #region CartOrderDetails
        private string name;
        private string itemID;
        private int price;
        private int Quantity;
        private int CartID;


        [Category("OrderList")]
        public int qty
        {
            get { return Quantity; }
            set { Quantity = value; ItemQty.Text = value.ToString(); }
        }
        [Category("OrderList")]
        public int Price
        {
            get { return price; }
            set { price = value; ItemPrice.Text = value.ToString(); }
        }
        [Category("OrderList")]
        public int cartID
        {
            get { return CartID; }
            set { CartID = value; }
        }
        [Category("OrderList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("OrderList")]
        public string Name
        {
            get { return name; }
            set { name = value; ItemName.Text = value; }
        }
        #endregion


        private void Editlbl_Click(object sender, EventArgs e)
        {

        }

        private void Deletelbl_Click(object sender, EventArgs e)
        {
            int cartqty = int.Parse(ReviewOrder.instance.counter.Text);
            if (cartqty <= 0)
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
                        ReviewOrder.instance.counter.Text = cart.ToString();
                    }
                    else if (cart > 0)
                    {
                        this.Parent.Controls.Remove(this);
                        ReviewOrder.instance.counter.Text = cart.ToString();
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

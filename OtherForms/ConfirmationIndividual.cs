using Flowershop_Thesis.SalesClerk.Order_Placement;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms
{
    public partial class ConfirmationIndividual : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public ConfirmationIndividual()
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
                    label6.Text = connectionString;
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
        private decimal price;
        private Image pic;
        private int stocks;


        [Category("ItmList")]
        public int Stock
        {
            get { return stocks; }
            set { stocks = value; label9.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; label4.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("ItmList")]
        public string Name
        {
            get { return name; }
            set { name = value; label3.Text = value; }
        }

        [Category("ItmList")]
        public Image img
        {
            get { return pic; }
            set { pic = value; pictureBox1.Image = value; }
        }

        #endregion
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length > 0)
            {   int qty = int.Parse(textBox2.Text.Trim());
                int min = int.Parse(label8.Text.Trim());

                if(qty < stocks && qty >=  min)
                {
                    string amount = label4.Text.Remove(label4.Text.Length - 3);
                    double sum = qty * int.Parse(amount); 
                    label11.Text =sum.ToString();
                }
                else
                {
                    MessageBox.Show("Please follow the Minimum and Maximum order Quantity");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled)
            {
                if (checkBox1.Checked)
                {
                    if (stocks >= 12)
                    {
                        label8.Text = "12";
                        textBox2.Text = "12";
                        MessageBox.Show("Quantity automatically updated!");
                    }
                    else
                    {
                        MessageBox.Show("Cannot Accomodate bulk order ");
                        checkBox1.Enabled = false;
                        checkBox1.Checked = false;
                    }
                }
                else
                {
                    label8.Text = "1";
                    textBox2.Text = "1";
                    MessageBox.Show("Quantity automatically updated!");
                }
            }
            
      
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addtocart();
        }
        public void addtocart()
        {
            string type;
            if(checkBox1.Checked && checkBox1.Enabled)
            {
                type = "Bulk";
            }
            else
            {
                type = "Individual";
            }
            if (textBox2.Text.Length > 0)
            {
                int OrderQty = int.Parse(textBox2.Text.ToString());
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
                        cmd = new SqlCommand("INSERT INTO Advance_ServingCart(ItemID,ItemName,OrderQty,OrderPrice,OrderType)Values" +
                                    "(@ID,@Name,@Qty,@Price,@Type);", con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(this.ItemID));
                        cmd.Parameters.AddWithValue("@Name", this.label3.Text);
                        cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(this.textBox2.Text));
                        int cprice = (int)decimal.Parse(label11.Text);
                        cmd.Parameters.AddWithValue("@Price", cprice);
                        cmd.Parameters.AddWithValue("@Type", type);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("AddingItem Failed!" + " : " + ex);
                    }


                    //activation of textchange event to refresh the item list in main panel
                    int cart = int.Parse(AdvanceOrderFrm.instance.cartbtn.Text.Trim());
                    cart++;
                    MessageBox.Show("Item Successfully Added Cart Items: " + cart.ToString());

                    if (cart > 0)
                    {
                        if (cart > 1999)
                        {
                            AdvanceOrderFrm.instance.cartbtn.Text = " " + cart.ToString();
                           
                        }
                        else
                        {
                            AdvanceOrderFrm.instance.cartbtn.Text = "  " + cart.ToString();
                           
                        }
                    }
                    



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

        }
    }
}

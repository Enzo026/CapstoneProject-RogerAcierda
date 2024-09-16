using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.OtherForms.Abuel;
using Flowershop_Thesis.OtherForms.StockAdjustments;
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

namespace Flowershop_Thesis.InventoryClerk.StockAdjustment
{
    public partial class StockAdjustmentFrmcs : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static StockAdjustmentFrmcs Instance;
        public Label Idhandler;
        public Label ItmName;
        public Label currentqty;
        public StockAdjustmentFrmcs()
        {
            InitializeComponent();
            testConnection();
            loadTableData();
            Instance = this;
            Idhandler = label55;
            ItmName = label22;
            currentqty = label26;
            UserInfo.AdminCode = "admin1233";
            label29.Visible = false;
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
        public void loadTableData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity > 20 ;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 20";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new StockAdjustmentListItems();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void h2l()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity > 20 ;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 20 order by itemQuantity desc";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new StockAdjustmentListItems();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void bySuppliedDate()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity > 20 ;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 20 order by SuppliedDate desc;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new StockAdjustmentListItems();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Searchbar()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity >20 AND  ItemName like @ItemName;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                    int rowCount = (int)countCommand.ExecuteScalar();
                    StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 20 AND  ItemName like @ItemName;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new StockAdjustmentListItems();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            bySuppliedDate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            h2l();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                Searchbar();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length > 0 && textBox2.Text != "0" )
            {   
                int currentqty = int.Parse(label26.Text);
                int input = int.Parse(textBox2.Text);

                if(input < currentqty)
                {
                    int output = currentqty - input;
                    label27.Text = "Quantity will be " + output + " after change";
                }
          
                else
                {
                    MessageBox.Show("Denied This will result in 0 inventory!");
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

        private void button5_Click(object sender, EventArgs e)
        {
            proceedadjust();
        }
        public void proceedadjust()
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed Stock adjustment " + label22.Text + " deducting qty of " + textBox2.Text + " in the current inventory?", "Restock Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (textBox3.Text == UserInfo.AdminCode)
                    {
                        label29.Visible = false;
                        try
                        {
                            string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @qty, SuppliedDate = GETDATE() WHERE ItemID = @ID;";
                            con.Open();
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", label55.Text);
                                updateCommand.Parameters.AddWithValue("@qty", textBox2.Text);

                                int rows = updateCommand.ExecuteNonQuery();

                                MessageBox.Show("Quantity Adjusted!");

                            }
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error on :" + ex.Message);
                        }


                        loadTableData();
                        reset();
                    }
                    else
                    {
                        label29.Visible = true;
                    }

                }
                else
                {
                    //none
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void reset()
        {
            textBox2.Text = " ";
            textBox3.Text = " ";
            label22.Text = " ";
            label55.Text = " ";

        }
    }
}

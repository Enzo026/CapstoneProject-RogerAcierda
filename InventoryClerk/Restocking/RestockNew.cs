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
using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.Abuel;
using Flowershop_Thesis.OtherForms.AdvanceOrder;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;

namespace Flowershop_Thesis.InventoryClerk.Restocking
{
    public partial class RestockNew : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static RestockNew Instance;
        public Label Idhandler;
        public Label ItmName;
        public RestockNew()
        {
            InitializeComponent();
            testConnection();
            loadTableData();
            Instance = this;
            Idhandler = label55;
            ItmName = label7;
            UserInfo.AdminCode = "admin1233";
            
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
                string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;

                                if (demo == 0)
                                {
                                    itemList[index].stocklevelData = "Out of Stock";
                                }
                                else if(demo > 0 && demo <= 20)
                                {
                                    itemList[index].stocklevelData = "Low Stock";
                                }
                                else
                                {
                                    itemList[index].stocklevelData = "gagu";
                                }
                                itemList[index].supplierData = reader["Supplier"].ToString();
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }con.Close();
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
                string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 AND  ItemName like @ItemName;;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 AND  ItemName like @ItemName;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;

                                if (demo == 0)
                                {
                                    itemList[index].stocklevelData = "Out of Stock";
                                }
                                else if (demo > 0 && demo <= 20)
                                {
                                    itemList[index].stocklevelData = "Low Stock";
                                }
                                else
                                {
                                    itemList[index].stocklevelData = "gagu";
                                }
                                itemList[index].supplierData = reader["Supplier"].ToString();
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

        public void SortH2L()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 order by ItemQuantity desc";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;

                                if (demo == 0)
                                {
                                    itemList[index].stocklevelData = "Out of Stock";
                                }
                                else if (demo > 0 && demo <= 20)
                                {
                                    itemList[index].stocklevelData = "Low Stock";
                                }
                                else
                                {
                                    itemList[index].stocklevelData = "gagu";
                                }
                                itemList[index].supplierData = reader["Supplier"].ToString();
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

        public void Lowstock()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity > 0 AND ItemQuantity <=20;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 0 AND ItemQuantity <=20 ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;

                                if (demo == 0)
                                {
                                    itemList[index].stocklevelData = "Out of Stock";
                                }
                                else if (demo > 0 && demo <= 20)
                                {
                                    itemList[index].stocklevelData = "Low Stock";
                                }
                                else
                                {
                                    itemList[index].stocklevelData = "gagu";
                                }
                                itemList[index].supplierData = reader["Supplier"].ToString();
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

        public void Outofstock()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemQuantity = 0;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity = 0";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
                                itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                itemList[index].itemnameData = reader["ItemName"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                itemList[index].itemquantityData = demo;

                                if (demo == 0)
                                {
                                    itemList[index].stocklevelData = "Out of Stock";
                                }
                                else if (demo > 0 && demo <= 20)
                                {
                                    itemList[index].stocklevelData = "Low Stock";
                                }
                                else
                                {
                                    itemList[index].stocklevelData = "gagu";
                                }
                                itemList[index].supplierData = reader["Supplier"].ToString();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed Restocking " + label7.Text + " adding qty of " + textBox2.Text + " in the current inventory", "Restock Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   
                    if(textBox3.Text == UserInfo.AdminCode)
                    {
                        try
                        {
                            string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity + @qty, SuppliedDate = GETDATE() WHERE ItemID = @ID;";
                            con.Open();
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", label55.Text);
                                updateCommand.Parameters.AddWithValue("@qty", textBox2.Text);

                                int rows = updateCommand.ExecuteNonQuery();

                                MessageBox.Show("Item Updated!");

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
                        MessageBox.Show("Please Make sure admin code is correct!");
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
            label7.Text = " ";
            label55.Text = " ";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SortH2L();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Outofstock();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lowstock();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                Searchbar();
            }
        }
    }
}

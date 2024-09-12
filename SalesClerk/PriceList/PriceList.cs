using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.SalesClerk.Order_Placement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Flowershop_Thesis.SalesClerk.PriceList
{
    public partial class PriceList : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static PriceList instance;
        public Label ID;
        public PriceList()
        {
            InitializeComponent();
            testConnection();
            instance = this;
            ID = IDTrigger;
            GetListQueue();
            ItmName.Checked = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            GetListQueue();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   if(textBox1.Text.Length > 0)
            {
                if (ItmName.Checked)
                {
                    ItemName();
                }
                else if (ItmCat.Checked == true)
                {
                    Category();
                }
                else if (ItmColor.Checked == true)
                {
                    Color();
                }
                else if (ItmPrice.Checked == true)
                {
                    if (Regex.IsMatch(textBox1.Text, "[a-zA-Z]"))
                    {
                        MessageBox.Show("TextBox contains letters.");
                    }
                    else
                    {
                        ItemPrice();
                    }

                }
                else if (ItmId.Checked == true)
                {
                    if (Regex.IsMatch(textBox1.Text, "[a-zA-Z]"))
                    {
                        MessageBox.Show("TextBox contains letters.");
                    }
                    else
                    {
                        ItemID();
                    }

                }
                else
                {
                    MessageBox.Show("Please Select a Filter");
                }

            }
          
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public void GetListQueue()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();


                   // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);

                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        public void Category()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND ItemType Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {   
                    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                    int rowCount = (int)countCommand.ExecuteScalar();


                    // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemType Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {   
                        command.Parameters.AddWithValue("@Search", textBox1.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);


                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        public void Color()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND ItemColor Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                //    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                    int rowCount = (int)countCommand.ExecuteScalar();


                    // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemColor Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                      //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);


                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        public void ItemName()
        {
            try
            {
                string search = "'" + textBox1.Text + "%'";
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND ItemName Like '"+textBox1.Text+ "%' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                  //  countCommand.Parameters.AddWithValue("@Search", search);
                    int rowCount = (int)countCommand.ExecuteScalar();


                    // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemName Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                     //   command.Parameters.AddWithValue("@Search", search);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);


                               
                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                MessageBox.Show("Error on SearchName : " + ex.Message);
            }
        }

        public void ItemPrice()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND Price Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                    int rowCount = (int)countCommand.ExecuteScalar();


                    // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND Price Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Search", textBox1.Text);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);


                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                MessageBox.Show("Error on ItemPrice : " + ex.Message);
            }
        }

        public void ItemID()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemStatus = 'Available' AND ItemID Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();


                    // counter.Text = rowCount.ToString();



                    PriceListContents[] inv = new PriceListContents[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemID Like '" + textBox1.Text + "%' AND ItemType != 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new PriceListContents();
                                inv[index].Name = reader["ItemName"].ToString();
                                inv[index].Type = reader["ItemStatus"].ToString();
                                int qty = reader.GetOrdinal("ItemQuantity");
                                inv[index].Quantity = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                inv[index].Price = reader["Price"].ToString();
                                int CI = reader.GetOrdinal("ItemID");
                                inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);





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

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        private void IDTrigger_TextChanged(object sender, EventArgs e)
        {
            getdetails();
        }
        public void getdetails()
        {
            try
            {
                string id = IDTrigger.Text;
                MessageBox.Show(id);
                con.Open();
                string sqlQuery = "SELECT * FROM ItemInventory where ItemID = "+id+" ;";
                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NameLbl.Text = reader["ItemName"].ToString();
                            TypeLbl.Text = reader["ItemType"].ToString();
                            QtyLbl.Text = reader["ItemQuantity"].ToString();
                            PriceLbl.Text = reader["Price"].ToString();
                            SuppDateLbl.Text = reader["SuppliedDate"].ToString();
                            DescLbl.Text = reader["ItemDescription"].ToString();
                            SuppLbl.Text = reader["Supplier"].ToString();
                            if (reader["ItemImage"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["ItemImage"];
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    ImgBox.Image = Image.FromStream(ms);
                                }
                            }
                        }

                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

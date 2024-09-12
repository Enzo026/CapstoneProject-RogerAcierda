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

namespace Flowershop_Thesis.OtherForms
{
    public partial class AdvanceOrderCart : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public AdvanceOrderCart()
        {
            InitializeComponent();
            testConnection();
            getCartList();

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

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
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
                    int cartlbl = int.Parse(label6.Text);
                    if (rowCount != cartlbl)
                    {
                        label6.Text = rowCount.ToString();
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
    }
}

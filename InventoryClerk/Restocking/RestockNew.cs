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
using Flowershop_Thesis.OtherForms.Abuel;

namespace Flowershop_Thesis.InventoryClerk.Restocking
{
    public partial class RestockNew : Form
    {
        public RestockNew()
        {
            InitializeComponent();
            testConnection();
            loadTableData();
            
        }


        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
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
                string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    RestockList[] itemList = new RestockList[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new RestockList();
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

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

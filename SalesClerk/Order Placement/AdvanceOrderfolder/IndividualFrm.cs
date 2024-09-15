using Flowershop_Thesis.OtherForms;
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

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class IndividualFrm : Form
    {
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public IndividualFrm()
        {
            InitializeComponent();
            testConnection();
            DisplayIndividual();
        }

        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        public void DisplayIndividual()
        {
            try
            {
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND ItemType = 'Individual' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    Adv_IndividualListItems[] inv = new Adv_IndividualListItems[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemType = 'Individual'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new Adv_IndividualListItems();
                                inv[index].ItemID = reader["ItemID"].ToString();
                                inv[index].Name = reader["ItemName"].ToString();
                                decimal priceIndex = reader.GetOrdinal("Price");
                                inv[index].Price = reader.IsDBNull((int)priceIndex) ? 0 : reader.GetDecimal((int)priceIndex);
                                int StockQuantity = reader.GetOrdinal("ItemQuantity");
                                inv[index].Stock = reader.IsDBNull(StockQuantity) ? 0 : reader.GetInt32(StockQuantity);

                                if (reader["ItemImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["ItemImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

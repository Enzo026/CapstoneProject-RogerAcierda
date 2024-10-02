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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class IndividualFrm : Form
    {
        public IndividualFrm()
        {
            InitializeComponent();
            DisplayIndividual();
        }
        public void DisplayIndividual()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

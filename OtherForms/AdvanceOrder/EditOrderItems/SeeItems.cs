using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class SeeItems : UserControl
    {
        public SeeItems()
        {
            InitializeComponent();
        }

        private void SeeItems_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM AdvanceOrderItems where OrderID = @id ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        label6.Text = rowCount.ToString();
                        SeeItemsListItems[] inv = new SeeItemsListItems[rowCount];

                        string sqlQuery = "SELECT * FROM AdvanceOrderItems where OrderID = @id ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {   
                            command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new SeeItemsListItems();
                                    inv[index].Price = decimal.Parse(reader["Price"].ToString());
                                    inv[index].Name = reader["Name"].ToString();
                                    inv[index].OrderQuantity = reader["Quantity"].ToString();

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

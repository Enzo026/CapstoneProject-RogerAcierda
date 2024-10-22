using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class EditItemOrders : UserControl
    {
        public static EditItemOrders instance;
        public System.Windows.Forms.Label amount;
        public EditItemOrders()
        {
            InitializeComponent();
            instance = this;
            amount = label2;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EditItemOrders_Load(object sender, EventArgs e)
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
                        EditItemList[] inv = new EditItemList[rowCount];

                        string sqlQuery = "SELECT * FROM AdvanceOrderItems where OrderID = @id ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new EditItemList();
                                    inv[index].ItmID = reader["ItemID"].ToString();
                                    inv[index].Oid = reader["OrderItemID"].ToString();
                                    inv[index].Price = int.Parse(reader["Price"].ToString());
                                    inv[index].Name = reader["Name"].ToString();
                                    inv[index].OrderQuantity = reader["Quantity"].ToString();

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM AdvanceOrders where OrderID = @id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                label2.Text = reader["TotalPrice"].ToString();
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

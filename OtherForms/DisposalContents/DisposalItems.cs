using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Disposal;
using Flowershop_Thesis.OtherForms.Abuel;
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

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class DisposalItems : Form
    {
        public DisposalItems()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DisposalItems_Load(object sender, EventArgs e)
        {   
            if(DisposalInfo.type == "WalkIn")
            {
                getorderlist();
            }
            else if(DisposalInfo.type == "AdvanceOrder")
            {
                getAdvanceorderlist();
            }
            else
            {
                MessageBox.Show("Having trouble fetching the disposal order items");
            }
       
        }
        public void getorderlist()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                try
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();

                        // Get row count
                        string countQuery = "SELECT COUNT(*) FROM SalesItemTbl WHERE TransactionID = @TransactionID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                            int rowCount = (int)countCommand.ExecuteScalar();
                            DisposalOrderListItems[] itemList = new DisposalOrderListItems[rowCount];

                            // Retrieve items
                            string sqlQuery = "SELECT * FROM SalesItemTbl WHERE TransactionID = @TransactionID";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new DisposalOrderListItems
                                        {
                                            id = reader["ItemID"].ToString(),
                                            ItmName = reader["ItemName"].ToString(),
                                            qty = reader["ItemQuantity"].ToString(), 
                                            price = string.Format("{0:C}", Convert.ToDecimal(reader["ItemPrice"])), // Assuming there is a Price column
                                            stat = reader["Status"].ToString()
                                        };

                                        flowLayoutPanel1.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log the error or show a message to the user)
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void getAdvanceorderlist()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                try
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();

                        // Get row count
                        string countQuery = "SELECT COUNT(*) FROM AdvanceOrderItems WHERE OrderID = @TransactionID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                            int rowCount = (int)countCommand.ExecuteScalar();
                            DisposalOrderListItems[] itemList = new DisposalOrderListItems[rowCount];

                            // Retrieve items
                            string sqlQuery = "SELECT * FROM AdvanceOrderItems WHERE OrderID = @TransactionID";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new DisposalOrderListItems
                                        {
                                            id = reader["ItemID"].ToString(),
                                            ItmName = reader["Name"].ToString(),
                                            qty = reader["Quantity"].ToString(),
                                            price = string.Format("{0:C}", Convert.ToDecimal(reader["Price"])), // Assuming there is a Price column
                                            stat = reader["Status"].ToString()
                                        };

                                        flowLayoutPanel1.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., log the error or show a message to the user)
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string updateQuery = "UPDATE CancelledTransactions SET Evaluation = 'Evaluated' WHERE TransactionID = @input";

                using (SqlCommand command = new SqlCommand(updateQuery, con))
                {
                    command.Parameters.AddWithValue("@input", DisposalInfo.ID); // Replace 'yourTransactionID' with the actual value

                    int rowsAffected = command.ExecuteNonQuery();
                    // Optionally, check how many rows were affected
                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Order Evaluated");
                        this.Close();
                        DisposalMain.Instance.Reset.Visible = true;
                    }
                    else
                    {
                        // No rows were updated (TransactionID might not exist)
                    }
                }
            }
        }
    }
}

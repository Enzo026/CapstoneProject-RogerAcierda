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
        public static DisposalItems instance;
        public Label loadingLbl;
        public DisposalItems()
        {
            InitializeComponent();
            instance = this;
            loadingLbl = label11;
            GetTransactionInfo();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void DisposalItems_Load(object sender, EventArgs e)
        {

            loaditems();
       
        }
        public void loaditems()
        {
            if (DisposalInfo.OrderStatus == "Evaluated")
            {
                button1.Visible = false;

            }
            if (DisposalInfo.OrderType == "WalkIn" || DisposalInfo.OrderType == "Walk-inTransaction")
            {
                getorderlist();
                
            }
            else if (DisposalInfo.OrderType == "AdvanceOrder")
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
                            int evaluatedOrderCount;

                            // Check for orders evaluated with the same TransactionID
                            string orderCountQuery = "SELECT COUNT(*) FROM SalesItemTbl WHERE Status = 'Evaluated' and TransactionID = @TransactionID";
                            using (SqlCommand orderCountCommand = new SqlCommand(orderCountQuery, con))
                            {
                                orderCountCommand.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                                evaluatedOrderCount = (int)orderCountCommand.ExecuteScalar();
                            }
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
                                            stat = reader["Status"].ToString(),
                                            type = reader["ItemType"].ToString(),
                                            SalesItmId = reader["SalesItemID"].ToString()
                                        };

                                        flowLayoutPanel1.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                            if (evaluatedOrderCount == rowCount && DisposalInfo.OrderStatus != "Evaluated")
                            {
                                DialogResult result = MessageBox.Show(
                                "It Seems All of the Items are evaluated do you wish this order to be marked as evaluated also?", // The message to display
                                "Confirmation",             // The title of the MessageBox
                                MessageBoxButtons.YesNo,   // The buttons to display
                                MessageBoxIcon.Question     // The icon to display
);

                                if (result == DialogResult.Yes)
                                {
                                    EvaluationProcess();
                                    this.Close();
                                }
                                else
                                {
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

                        // Get row count for AdvanceOrderItems
                        string countQuery = "SELECT COUNT(*) FROM AdvanceOrderItems WHERE OrderID = @TransactionID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                            int rowCount = (int)countCommand.ExecuteScalar();
                            int evaluatedOrderCount;
                            // Check for orders evaluated with the same TransactionID
                            string orderCountQuery = "SELECT COUNT(*) FROM AdvanceOrderItems WHERE Status = 'Evaluated' and OrderID = @TransactionID";
                            using (SqlCommand orderCountCommand = new SqlCommand(orderCountQuery, con))
                            {
                                orderCountCommand.Parameters.AddWithValue("@TransactionID", DisposalInfo.ID);
                                evaluatedOrderCount = (int)orderCountCommand.ExecuteScalar();
                            }

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
                                            stat = reader["Status"].ToString(),
                                            type = reader["Type"].ToString(),
                                            SalesItmId = reader["OrderItemID"].ToString()
                                        };

                                        flowLayoutPanel1.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                            if(evaluatedOrderCount == rowCount && DisposalInfo.OrderStatus != "Evaluated")
                            {
                                DialogResult result = MessageBox.Show(
                                "It Seems All of the Items are evaluated do you wish this order to be marked as evaluated also?", // The message to display
                                "Confirmation",             // The title of the MessageBox
                                MessageBoxButtons.YesNo,   // The buttons to display
                                MessageBoxIcon.Question     // The icon to display
);

                                if (result == DialogResult.Yes)
                                {
                                    EvaluationProcess();
                                    this.Close();
                                }
                                else
                                {
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
            EvaluationProcess();
        }
        public void EvaluationProcess()
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

        private void label11_VisibleChanged(object sender, EventArgs e)
        {
            if (label11.Visible) {
                loaditems();
                label11.Visible = false;
            }
        }

        private void DisposalItems_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }

        private void DisposalItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposalMain.Instance.Reset.Visible = true;
        }
        public void GetTransactionInfo()
        {
            try
            {
                // Assume DisposalInfo.ID contains the TransactionID
                string transactionID = DisposalInfo.ID;

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // SQL query to get transaction information based on TransactionID
                    string query = @"
                SELECT * 
                FROM CancelledTransactions
                WHERE TransactionID = @TransactionID;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter for TransactionID
                        cmd.Parameters.AddWithValue("@TransactionID", transactionID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())  // Check if a record is found
                            {
                                CustomerNameLbl.Text = reader["CustomerName"].ToString();
                                TypeLbl.Text = reader["Type"].ToString();
                                DateTime cancellationDate = Convert.ToDateTime(reader["CancellationDate"]);
                                
                                DateLbl.Text = cancellationDate.ToString("MMM dd, yyyy");
                                decimal totalPrice = 0.0m;

                                if (reader["TotalPrice"] != DBNull.Value)
                                {
                                    totalPrice = Convert.ToDecimal(reader["TotalPrice"]);
                                }

                                // Format the TotalPrice as currency and assign it to the label
                                TotalPriceLbl.Text = totalPrice.ToString("C");

                            }
                            else
                            {
                                MessageBox.Show("No transaction found with the provided TransactionID.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving transaction information: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

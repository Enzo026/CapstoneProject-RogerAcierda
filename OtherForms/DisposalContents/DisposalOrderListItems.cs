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

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class DisposalOrderListItems : UserControl
    {
        public DisposalOrderListItems()
        {
            InitializeComponent();
        }

        #region FinishedQueue
        private string Id, ItemName, Price, Quantity, status, ItemType, SalesItemID;

        [Category("ActivityList")]
        public string id //ID
        {
            get { return Id; }
            set { Id = value; }
        }
        [Category("ActivityList")]
        public string SalesItmId //ID
        {
            get { return SalesItemID; }
            set { SalesItemID = value; }
        }

        [Category("ActivityList")]
        public string ItmName //Customer Name
        {
            get { return ItemName; }
            set { ItemName = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string price // Price
        {
            get { return Price; }
            set { Price = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string qty
        {
            get { return Quantity; }
            set { Quantity = value; QtyLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string stat
        {
            get { return status; }
            set { status = value; }
        }
        [Category("ActivityList")]
        public string type
        {
            get { return ItemType; }
            set { ItemType = value; }
        }

        #endregion

        private void QtyLbl_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {






        }

        private void PriceLbl_Click(object sender, EventArgs e)
        {
            string cleanedString = PriceLbl.Text.Replace("₱", "").Replace(",", "").Trim();
            MessageBox.Show(cleanedString);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ItemType == "Individual" || ItemType == "Premade")
            {
                DisposalInfo.ItemType = ItemType;
                DisposalInfo.SalesItemID = SalesItemID;
                DisposalInfo.EvID = Id;
                DisposalInfo.EvQty = Quantity;
                DisposalInfo.EvName = ItemName;
                DisposalInfo.EvPrice = Price;

                DisposalEvaluation frm = new DisposalEvaluation();
                frm.ShowDialog();
            }
            else if (ItemType == "Custom" || ItemType == "AdvanceCustom")
            {
                DisposalInfo.ItemType = ItemType;
                DisposalInfo.SalesItemID = SalesItemID;
                DisposalInfo.EvID = Id;
                DisposalInfo.EvName = ItemName;
                DisposalInfo.EvPrice = Price;

                DisposalBouquetRetrieval frm = new DisposalBouquetRetrieval();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Im having a hard time picking up the type of order");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Mark this item as done?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {

                EvaluationProcess();

            }
            else
            {
                // User clicked "No"
            }
        }


        private void DisposalOrderListItems_Load(object sender, EventArgs e)
        {

            if (status == "Evaluated")
            {
                button3.Visible = false;
                button2.Visible = false;
            }
            else
            {
                if (Quantity == "0")
                {
                    EvaluationProcess();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void EvaluationProcess()
        {
            if (DisposalInfo.OrderType == "WalkIn" || DisposalInfo.OrderType == "Walk-inTransaction")
            {
                EvaluatedStatusWalkInOrder();
                button3.Visible = false;
                button2.Visible = false;
            }
            else if (DisposalInfo.OrderType == "AdvanceOrder")
            {
                EvaluatedStatusAdvanceOrder();
                button3.Visible = false;
                button2.Visible = false;
            }
            else
            {
                MessageBox.Show("Having trouble fetching the disposal order items");
            }
        }
        public void EvaluatedStatusAdvanceOrder()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                try
                {
                    con.Open();


                    string cleanedString = PriceLbl.Text.Replace("₱", "").Replace(",", "").Trim();
                    string insertquery = "INSERT INTO DisposedItems(TransactionID, ItemID, ItemName, Price, Quantity, DisposalDate, Employee, EmployeeID) " +
                                   "VALUES (@transactionID, @ItemID, @ItemName,@Price, @Qty, GETDATE(), @EmpName, @EmpID)";

                    using (SqlCommand cmd = new SqlCommand(insertquery, con))
                    {
                        // Set the parameter values
                        cmd.Parameters.AddWithValue("@transactionID", DisposalInfo.ID);
                        cmd.Parameters.AddWithValue("@ItemID", Id);
                        cmd.Parameters.AddWithValue("@ItemName", ItemNameLbl.Text);
                        cmd.Parameters.AddWithValue("@Price", cleanedString);
                        cmd.Parameters.AddWithValue("@Qty", QtyLbl.Text); // Ensure it's an int
                        cmd.Parameters.AddWithValue("@EmpName", UserInfo.Empleyado);
                        cmd.Parameters.AddWithValue("@EmpID", UserInfo.EmpID);

                        // Execute the insert command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Optionally inform the user of success
                        MessageBox.Show($"{rowsAffected} record(s) inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., show a message box with the error)
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            string query = "UPDATE AdvanceOrderItems SET Status = 'Evaluated' WHERE OrderItemID = @SalesItemID;";

            // Ensure you have a valid connection string in your project
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter to the command
                    command.Parameters.Add(new SqlParameter("@SalesItemID", SalesItemID));

                    try
                    {
                        // Open the connection
                        connection.Open();
                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were updated
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No items were updated. Please check the SalesItemID.");
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public void EvaluatedStatusWalkInOrder()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                try
                {
                    con.Open();
                    string cleanedString = PriceLbl.Text.Replace("₱", "").Replace(",", "").Trim();
                    string insertquery = "INSERT INTO DisposedItems(TransactionID, ItemID, ItemName, Price, Quantity, DisposalDate, Employee, EmployeeID) " +
                                   "VALUES (@transactionID, @ItemID, @ItemName,@Price, @Qty, GETDATE(), @EmpName, @EmpID)";

                    using (SqlCommand cmd = new SqlCommand(insertquery, con))
                    {
                        // Set the parameter values
                        cmd.Parameters.AddWithValue("@transactionID", DisposalInfo.ID);
                        cmd.Parameters.AddWithValue("@ItemID", Id);
                        cmd.Parameters.AddWithValue("@ItemName", ItemNameLbl.Text);
                        cmd.Parameters.AddWithValue("@Price", cleanedString);
                        cmd.Parameters.AddWithValue("@Qty", QtyLbl.Text); // Ensure it's an int
                        cmd.Parameters.AddWithValue("@EmpName", UserInfo.Empleyado);
                        cmd.Parameters.AddWithValue("@EmpID", UserInfo.EmpID);

                        // Execute the insert command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Optionally inform the user of success
                        MessageBox.Show($"{rowsAffected} record(s) inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., show a message box with the error)
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            string query = "UPDATE SalesItemTbl SET Status = 'Evaluated' WHERE SalesItemID = @SalesItemID;";

            // Ensure you have a valid connection string in your project
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter to the command
                    command.Parameters.Add(new SqlParameter("@SalesItemID", SalesItemID));

                    try
                    {
                        // Open the connection
                        connection.Open();
                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were updated
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No items were updated. Please check the SalesItemID.");
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}

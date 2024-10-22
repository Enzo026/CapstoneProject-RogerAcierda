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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class EditPickupDate : UserControl
    {
        public EditPickupDate()
        {
            InitializeComponent();
            setdate();
            
        }
        public void setdate()
        {
            
            DateTime today = DateTime.Today;
            DateTime mindate  = today.AddDays(1);

            dateTimePicker1.MinDate = mindate;
        }

        private void EditPickupDate_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT PickupDate FROM AdvanceOrders WHERE OrderID = @id";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameter for the OrderID
                        command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Fetch PickupDate as DateTime
                                DateTime pickupDate = reader.GetDateTime(reader.GetOrdinal("PickupDate"));

                                // Format the date to "MMM dd, yyyy"
                                string formattedDate = pickupDate.ToString("MMM dd, yyyy");

                                // Display the formatted date on label3
                                label3.Text = formattedDate;
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int numId = 0; // Initialize numId
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {
                    // Query to count the number of records with the given OrderID
                    string countQuery = "Select count(*) from AdvanceOrders where OrderID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();

                        // Add parameter for OrderID (ensure correct type)
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);

                        // Execute the count query and store the result in numId
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }

                // Check if exactly one record exists for the given OrderID
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {
                        // Query to update Pickupdate
                        string updateQuery = "UPDATE AdvanceOrders SET Pickupdate = @In WHERE OrderID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();

                            // Add parameters for the update query
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                            updateCommand.Parameters.AddWithValue("@In", dateTimePicker1.Value);

                            // Execute the update query
                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            // Check if any rows were updated
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Pickupdate Changed!");
                            }
                            else
                            {
                                MessageBox.Show("No row was updated. Please check the OrderID.");
                            }
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple items with this OrderID.");
                }
                else
                {
                    MessageBox.Show("No item found with the given OrderID.");
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (InvalidCastException castEx)
            {
                MessageBox.Show("Data type error: " + castEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on changing PickupDate: " + ex.Message);
            }

        }
    }
}

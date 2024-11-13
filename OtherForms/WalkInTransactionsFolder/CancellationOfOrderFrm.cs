using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement;
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

namespace Flowershop_Thesis.OtherForms.WalkInTransactionsFolder
{
    public partial class CancellationOfOrderFrm : Form
    {
        private string AllItemsTxt = "Are you sure you want to cancel all items in cart? if so please provide the security code";
        private string SingleTxt = "Are you sure you want to cancel this item in cart? if so please provide the security code";

        public CancellationOfOrderFrm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   if(textBox1.Text == SystemInfo.SecurityCode)
            {
                if(WalkInTransaction.CancellationType == "AllItems")
                {
                    AllItemCancellation();
                }
                else if(WalkInTransaction.CancellationType == "Single")
                {
                    SingelItemCancellation();
                }
            }
            else
            {
                MessageBox.Show("Invalid Security Code");
            }

        }
        public void AllItemCancellation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // 1. Query to get all CartIDs from ServingCart
                    string query = @"
            SELECT CartID
            FROM ServingCart;
        ";

                    // List to store CartIDs
                    List<int> cartIDs = new List<int>();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Store CartIDs in the list
                            while (reader.Read())
                            {
                                cartIDs.Add(reader.GetInt32(reader.GetOrdinal("CartID")));
                            }
                        }
                    }

                    // 2. Loop through each CartID and call the "Deletion of Item In Cart" method
                    foreach (int cartID in cartIDs)
                    {
                        // Call the method to delete item in cart with the CartID
                        DeletionOfItemInCart(cartID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }
        // Method to delete an item in the cart based on CartID
        private void DeletionOfItemInCart(int cartID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string query = @"
                                    DECLARE @CartQty INT;
                                    DECLARE @ItemID INT;
                                    -- Retrieve the quantity from the cart
                                    SELECT @CartQty = OrderQty,@ItemID = ItemID
                                    FROM ServingCart 
                                    WHERE CartID = @CartID;

                                    -- Update the inventory by adding the quantity from the cart
                                    UPDATE ItemInventory
                                    SET ItemQuantity = ItemQuantity + @CartQty
                                    WHERE ItemID = @ItemID;
                                ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@CartID", cartID);
                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating inventory: " + ex.Message);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Query to delete the item in the cart (assuming some delete logic based on CartID)
                    string deleteQuery = @"
                DELETE FROM ServingCart
                WHERE CartID = @CartID;
            ";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        // Add parameter for CartID
                        cmd.Parameters.AddWithValue("@CartID", cartID);

                        // Execute the deletion
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("All Items in cart are deleted!");
                        OrderPlacement.instance.update.Visible = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item from cart for CartID " + cartID + ": " + ex.Message);
            }
        }
        public void SingelItemCancellation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string query = @"
                                    DECLARE @CartQty INT;
                                    DECLARE @ItemID INT;
                                    -- Retrieve the quantity from the cart
                                    SELECT @CartQty = OrderQty,@ItemID = ItemID
                                    FROM ServingCart 
                                    WHERE CartID = @CartID;

                                    -- Update the inventory by adding the quantity from the cart
                                    UPDATE ItemInventory
                                    SET ItemQuantity = ItemQuantity + @CartQty
                                    WHERE ItemID = @ItemID;
                                ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@CartID", WalkInTransaction.CancellationCartId);   
                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating inventory: " + ex.Message);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ServingCart where CartID = @id ;", con);
                    cmd.Parameters.AddWithValue("@id", WalkInTransaction.CancellationCartId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item cancelled!");
                    OrderPlacement.instance.update.Visible = true;
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        private void CancellationOfOrderFrm_Load(object sender, EventArgs e)
        {
            if(WalkInTransaction.CancellationType == "AllItems")
            {
                label1.Text = AllItemsTxt;
            }
            else if( WalkInTransaction.CancellationType == "Single")
            {
                label1.Text = SingleTxt;
                label3.Text = WalkInTransaction.CancellationItemName;
            }
        }
    }
}

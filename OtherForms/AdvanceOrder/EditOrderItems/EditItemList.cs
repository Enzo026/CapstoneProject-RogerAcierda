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
using Capstone_Flowershop;
using Flowershop_Thesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class EditItemList : UserControl
    {
        public EditItemList()
        {
            InitializeComponent();
        }
        #region AdvanceOrderListItems
        private int ItemPrice;
        private string OrderItemID;
        private string name;
        private string qty;
        private string ItemID;
        int RawPrice;
        int orderamount;

        [Category("ListItems")]
        public string Oid
        {
            get { return OrderItemID; }
            set { OrderItemID = value;}
        }
        [Category("ListItems")]
        public string ItmID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }
        [Category("ListItems")]
        public int Price
        {
            get { return ItemPrice; }
            set { ItemPrice = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ListItems")]
        public string Name
        {
            get { return name; }
            set { name = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ListItems")]
        public string OrderQuantity
        {
            get { return qty; }
            set { qty = value; QtyLbl.Text = value.ToString(); }
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE AdvanceOrders SET TotalPrice = @In WHERE OrderID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            decimal totalitemprice = RawPrice * decimal.Parse(qty);
                            decimal inputdb = orderamount - totalitemprice;
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                            updateCommand.Parameters.AddWithValue("@In", inputdb.ToString());

                            updateCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on changing the total order amount : " + ex.Message);
                }
                deletefrominventory();
            }
        }

        private void EditItemList_Load(object sender, EventArgs e)
        {
            try
            {
                string orderamounts = EditItemOrders.instance.amount.Text;
                orderamount = Convert.ToInt32(orderamounts);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error getting the total order amount" + ex.Message);
            }
            try
            {   

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM ItemInventory where ItemID = @id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@id", ItemID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                decimal output = decimal.Parse(reader["Price"].ToString());
                                RawPrice = Convert.ToInt32(output);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int totalitemprice = Price - RawPrice;
          //  MessageBox.Show(Price.ToString() +" - " + RawPrice.ToString() + " = " + totalitemprice.ToString());
            int newqty = int.Parse(qty);
            newqty--;
            if (newqty == 0)
            {
                string totalamount = EditItemOrders.instance.amount.Text;
                decimal totalamountvalue = Convert.ToDecimal(totalamount);
                decimal inputvalue = totalamountvalue - Convert.ToDecimal(Price);
                MessageBox.Show(inputvalue.ToString());
                int oldamount = Convert.ToInt32(EditItemOrders.instance.amount.Text);
                int newamount = oldamount - totalitemprice;
                MessageBox.Show(newamount.ToString());
                deletefrominventory();
            }
            else
            {
                OrderQuantity = newqty.ToString();
                EditItem(totalitemprice);
                PriceLbl.Text = totalitemprice.ToString();
                Price = totalitemprice;
                decimal oldamount = decimal.Parse(EditItemOrders.instance.amount.Text);
                decimal newamount = oldamount- RawPrice;
                updateorderprice(decimal.Parse(newamount.ToString()));
                EditItemOrders.instance.amount.Text = newamount.ToString();
            }


        }
        public void deletefrominventory()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    // Open the database connection
                    con.Open();

                    // SQL DELETE command with a parameter placeholder
                    string query = "DELETE FROM AdvanceOrderItems WHERE OrderItemID = @id";

                    // Create the SqlCommand object to execute the query
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Add the parameter and its value to the SqlCommand
                        command.Parameters.AddWithValue("@id", OrderItemID);

                        // Execute the command (use ExecuteNonQuery for commands that don't return data)
                        int rowsAffected = command.ExecuteNonQuery();

                        // Optionally, check if any rows were affected (deleted)
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item successfully deleted.");
                        }
                        else
                        {
                            Console.WriteLine("No rows were deleted. OrderItemID may not exist.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle potential exceptions, such as connection issues or SQL errors
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

                int totalitemprice = Price + RawPrice;
                int newqty = int.Parse(qty);
                newqty++;


                OrderQuantity = newqty.ToString();
                EditItem(totalitemprice);
                PriceLbl.Text = totalitemprice.ToString();
                Price = totalitemprice;


                decimal oldamount = decimal.Parse(EditItemOrders.instance.amount.Text);
                decimal newamount = oldamount + RawPrice;
                updateorderprice(decimal.Parse(newamount.ToString()));
                EditItemOrders.instance.amount.Text = newamount.ToString();


        }
        public void EditItem(int totalitemprice)
        {
            using (SqlConnection conn = new SqlConnection(Connect.connectionString))
            {
                string updateQuery = "UPDATE AdvanceOrderItems SET Quantity = @qty , Price = @price WHERE OrderItemID = @ID;";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                {
                    conn.Open();

                    // Calculate total item price and inputdb

                  
                    // Add parameters for the SQL query
                    updateCommand.Parameters.AddWithValue("@ID", OrderItemID);
                    updateCommand.Parameters.AddWithValue("@qty", QtyLbl.Text.Trim());
                    updateCommand.Parameters.AddWithValue("@price", totalitemprice.ToString());

                    // Execute the SQL update command
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    // Check if any rows were affected, and show a message if successful
                    if (rowsAffected > 0)
                    {
                        //MessageBox.Show("Item Deducted.");

                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Please check the OrderID.");
                    }
                }
            }
        }
        public void updateorderprice(decimal inputdb)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {
                    string updateQuery = "UPDATE AdvanceOrders SET TotalPrice = @In WHERE OrderID = @ID;";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                    {
                        conn.Open();
                        updateCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                        updateCommand.Parameters.AddWithValue("@In", inputdb.ToString());

                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on changing the total order amount : " + ex.Message);
            }
        }
    }
}

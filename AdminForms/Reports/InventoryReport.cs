using Flowershop_Thesis.OtherForms.InventoryReports;
using Flowershop_Thesis.OtherForms.Reports;
using Flowershop_Thesis.OtherForms.Restocking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Capstone_Flowershop.AdminForms.Reports
{
    public partial class InventoryReport : Form
    {
        public InventoryReport()
        {
            InitializeComponent();
            getLowStockFlowers();
            getSoonToExpireAndExpired();
            getLowStockMaterials();
  
            FastMovingProduct();
            SlowMovingProduct();
            TopSellingProduct();
            DisposalList();
            getrecentBatch();
            dateTimePicker4.MaxDate = DateTime.Today;
            dateTimePicker4.Value = DateTime.Today;
            dateTimePicker3.Value = DateTime.Today;
        }
        private void label88_Click(object sender, EventArgs e)
        {

        }
        public void getLowStockFlowers()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = "SELECT COALESCE(Count(*), 0) AS output FROM ItemInventory WHERE ItemType = 'Individual' AND ItemQuantity < 20 and ItemStatus = 'Available';";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label7.Text = reader["output"].ToString();
                    }
                }
            }
        }
        public void getSoonToExpireAndExpired()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = " SELECT count(*) as output FROM ItemInventory WHERE DATEADD(DAY, lifespan, SuppliedDate) <= DATEADD(DAY, 1, CAST(GETDATE() AS DATE));";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label12.Text = reader["output"].ToString();
                    }
                }
            }
        }
        public void getLowStockMaterials()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = " SELECT COALESCE(Count(*), 0) AS output FROM Materials WHERE ItemQuantity < 4;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label8.Text = reader["output"].ToString();
                    }
                }
            }
        }

        public void FastMovingProduct()
        {
            string FastMovingID = "0";
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT TOP 1 ItemID, ItemName, SUM(Quantity) AS TotalQuantity, SUM(Price) AS TotalPrice FROM OrderedItems  GROUP BY ItemID, ItemName ORDER BY TotalQuantity DESC";

                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    using (SqlDataReader reader = countCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Ensures there is a row to read
                        {
                            FastMovingID = reader["ItemID"].ToString().Trim();
                            label20.Text= reader["ItemName"].ToString().Trim();
                            label21.Text = "Sold "+reader["TotalQuantity"].ToString().Trim() + " with the total of " + reader["TotalPrice"].ToString().Trim() + " Pesos";
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT ItemImage FROM ItemInventory WHERE ItemID = @ID";

                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@ID", FastMovingID);
                    using (SqlDataReader reader = countCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Ensures there is a row to read
                        {
                            byte[] imageBytes = (byte[])reader["ItemImage"];
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }


        }
        public void SlowMovingProduct()
        {
            string FastMovingID = "0";
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT TOP 1 ItemID, ItemName, SUM(Quantity) AS TotalQuantity, SUM(Price) AS TotalPrice FROM OrderedItems  GROUP BY ItemID, ItemName ORDER BY TotalQuantity ASC;";

                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    using (SqlDataReader reader = countCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Ensures there is a row to read
                        {
                            FastMovingID = reader["ItemID"].ToString().Trim();
                            label25.Text = reader["ItemName"].ToString().Trim();
                            label24.Text = "Sold " + reader["TotalQuantity"].ToString().Trim() + " with the total of " + reader["TotalPrice"].ToString().Trim() + " Pesos";
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT ItemImage FROM ItemInventory WHERE ItemID = @ID";

                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@ID", FastMovingID);
                    using (SqlDataReader reader = countCommand.ExecuteReader())
                    {
                        if (reader.Read()) // Ensures there is a row to read
                        {
                            byte[] imageBytes = (byte[])reader["ItemImage"];
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                pictureBox2.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }


        }
        public void TopSellingProduct()
        {
            try
            {   
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Top5OrderedItems;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        top5ProductList[] inv = new top5ProductList[rowCount];

                        string sqlQuery = "Select * from Top5OrderedItems order by totalquantity desc;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new top5ProductList();
                                    inv[index].name = reader["ItemName"].ToString();
                                    inv[index].Qty = reader["TotalQuantity"].ToString();
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
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }

        }
        public void DisposalList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransactions where Evaluation = 'Pending' and OrderStatus = 'Complete' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransactions where Evaluation = 'Pending' and OrderStatus = 'Complete' order by CancellationDate desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DisposalListItems();
                                    inv[index].LocalID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].status = reader["Evaluation"].ToString().Trim();
                                    inv[index].date = reader["CancellationDate"].ToString().Trim();
                                    inv[index].Qty = reader["TotalPrice"].ToString().Trim();
                                    inv[index].Ordertype = reader["Type"].ToString().Trim();
                                    flowLayoutPanel2.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }
        }
        string batchID;
        public void SelectRecentBatch()
        {
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                string query = "SELECT TOP(1) BatchID FROM RestockingTbl ORDER BY RestockingDate DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Ensure BatchID is not null
                                if (reader["BatchID"] != DBNull.Value)
                                {
                                    label3.Text = reader["BatchID"].ToString();
                                    batchID = reader["BatchID"].ToString();
                                }
                                else
                                {
                                    label3.Text = "No Batch ID found";
                                    batchID = null; // or set to some default value
                                }
                            }
                            else
                            {
                                label3.Text = "No records found";
                                batchID = null; // or set to some default value
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception more robustly if needed
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }
        public void getrecentBatch()
        {
            SelectRecentBatch();
            if (batchID != "No Batch ID found")
            {
                showinfo();
    

            }
            else
            {
                label7.Text = "No Batch ID found";
            }
        }
        public void showinfo()
        {
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                SqlCommand command = new SqlCommand("GetRestockingInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add the BatchID parameter
                command.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.NVarChar));
                command.Parameters["@BatchID"].Value = batchID;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        // Parse and format RestockingDate
                        if (DateTime.TryParse(reader["RestockingDate"].ToString(), out DateTime restockingDate))
                        {
                            label15.Text = restockingDate.ToString("MMM dd, yyyy"); // Formats to "Oct 21, 2024"
                        }
                        else
                        {
                            label15.Text = "Invalid Date"; // Handle invalid date scenario
                        }

                        label12.Text = reader["employee"].ToString();
                        label10.Text = reader["totalItems"].ToString();
                        label11.Text = reader["ReceiptID"].ToString();

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

        }


        public void EvaluatedList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransactions where Evaluation = 'Evaluated' and OrderStatus = 'Complete' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransactions where Evaluation = 'Evaluated' and OrderStatus = 'Complete' order by CancellationDate desc;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DisposalListItems();
                                    inv[index].LocalID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].status = reader["Evaluation"].ToString().Trim();
                                    inv[index].date = reader["CancellationDate"].ToString().Trim();
                                    inv[index].Qty = reader["TotalPrice"].ToString().Trim();
                                    inv[index].Ordertype = reader["Type"].ToString().Trim();
                                    flowLayoutPanel2.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }
        }
        public void PendingList()
        {
            try
            {   flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransaction where Evaluation = 'Pending'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransaction where Evaluation = 'Pending';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DisposalListItems();
                                    inv[index].LocalID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].status = reader["Evaluation"].ToString().Trim();
                                    inv[index].date = reader["CancellationDate"].ToString().Trim();
                                    inv[index].Qty = reader["TotalPrice"].ToString().Trim();
                                    inv[index].Ordertype = reader["TransactionType"].ToString().Trim();

                                    flowLayoutPanel2.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }
        }
        private void button23_Click(object sender, EventArgs e)
        {   
            flowLayoutPanel2.Controls.Clear();
            EvaluatedList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            PendingList();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransaction where CustomerName like @Name";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@name", textBox2.Text+"%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransaction where CustomerName like @Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@name", textBox2.Text + "%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DisposalListItems();
                                    inv[index].LocalID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].status = reader["Evaluation"].ToString().Trim();
                                    inv[index].date = reader["CancellationDate"].ToString().Trim();
                                    inv[index].Ordertype = reader["TransactionType"].ToString().Trim();
                                    inv[index].Qty = reader["TotalPrice"].ToString().Trim();

                                    flowLayoutPanel2.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Define your start and end dates
                    DateTime startDate = dateTimePicker4.Value.Date;
                    DateTime endDate = dateTimePicker3.Value.Date;

                    // Query to select data within the date range
                    string sqlQuery = "SELECT * FROM CancelledTransactions WHERE CancellationDate BETWEEN @startDate AND @endDate;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // List to hold all transactions
                            List<DisposalListItems> transactions = new List<DisposalListItems>();

                            while (reader.Read())
                            {
                                // Extracting fields from the reader

                                string ID = reader["TransactionID"].ToString().Trim();
                                string CustName = reader["CustomerName"].ToString().Trim();
                                string Employee = reader["EmployeeName"].ToString().Trim();
                                string status = reader["Evaluation"].ToString().Trim();
                                string date = reader["CancellationDate"].ToString().Trim();
                                string Qty = reader["TotalPrice"].ToString().Trim();
                                string Type = reader["Type"].ToString().Trim();

                                // Create a new TransactionsList instance
                                DisposalListItems inv = new DisposalListItems
                                {


                                    LocalID = ID,
                                    CustName = CustName,
                                    Employee = Employee,
                                    status = status,
                                    date = date,
                                    Qty = Qty,
                                    Ordertype = Type
                                };

                                // Add to the list of transactions
                                transactions.Add(inv);
                            }

                            // Add all controls to the FlowLayoutPanel
                            foreach (var inv in transactions)
                            {
                                flowLayoutPanel2.Controls.Add(inv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List: " + ex.Message);
            }

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.MinDate = dateTimePicker4.Value; // Update MinDate
            dateTimePicker3.MaxDate = DateTime.Today;
            dateTimePicker3.Enabled = true;
        }
    }
}


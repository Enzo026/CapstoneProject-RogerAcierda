using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.OtherForms.Restocking;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Flowershop_Thesis.InventoryClerk.LandingPage
{
    public partial class DashboardFrm : Form
    {
        public DashboardFrm()
        {
            InitializeComponent();
            RestockNum();
            SupplierNum();
            ForEvaluation();
            getrecentBatch();
            GetTotalAmounts();
        }
        public void RestockNum()
        {
            int num1 = 0;
            int num2 = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COALESCE(Count(*), 0) AS output FROM ItemInventory WHERE ItemType = 'Individual' AND ItemQuantity < 20 and ItemStatus = 'Available' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        num1 = Count;
                    }
                }
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COALESCE(Count(*), 0) AS output FROM Materials WHERE ItemQuantity < 4 ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        num2 = Count;
                    }
                }
                int final = num1 + num2;
                label9.Text = final.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void SupplierNum()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        label11.Text = Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void ForEvaluation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM CancelledTransactions where Evaluation = 'Pending' and OrderStatus = 'Complete' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        label10.Text = Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DashboardFrm_Load(object sender, EventArgs e)
        {

            label1.Text = "Welcome " + UserInfo.Empleyado + "!";
            DateTime date = DateTime.Now;
            label2.Text = "Inventory Clerk , " + date.ToString();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        string batchID;

        public void getrecentBatch()
        {
            SelectRecentBatch();
            if (batchID != "No Batch ID found")
            {
                showinfo();
                getList();

            }
            else
            {
                label7.Text = "No Batch ID found";
            }
        }
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
                                    label7.Text = reader["BatchID"].ToString();
                                    batchID = reader["BatchID"].ToString();
                                }
                                else
                                {
                                    label7.Text = "No Batch ID found";
                                    batchID = null; // or set to some default value
                                }
                            }
                            else
                            {
                                label7.Text = "No records found";
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
                            label17.Text = restockingDate.ToString("MMM dd, yyyy"); // Formats to "Oct 21, 2024"
                        }
                        else
                        {
                            label17.Text = "Invalid Date"; // Handle invalid date scenario
                        }

                        label15.Text = reader["employee"].ToString();
                        label14.Text = reader["totalItems"].ToString();
                        label22.Text = reader["ReceiptID"].ToString();

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

        }

        public void getList()
        {
            flowLayoutPanel1.Controls.Clear();
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT Count(*) FROM RestockingTbl where BatchID = @BatchId;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@BatchId", batchID);
                    int rowCount = (int)countCommand.ExecuteScalar();
                    BatchInfoList[] itemList = new BatchInfoList[rowCount];

                    string sqlQuery = "SELECT * FROM RestockingTbl where BatchID = @BatchId";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@BatchId", batchID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new BatchInfoList();
                                itemList[index].ItemName = reader["ItemName"].ToString();
                                itemList[index].SupplierName = reader["Supplier"].ToString();

                                // Parse and format ExpirationDate
                                if (DateTime.TryParse(reader["ExpirationDate"].ToString(), out DateTime expirationDate))
                                {
                                    itemList[index].ExpirationDate = expirationDate.ToString("MMM dd, yyyy"); // Formats to "Oct 21, 2024"
                                }
                                else
                                {
                                    itemList[index].ExpirationDate = "Invalid Date"; // Handle invalid date scenario
                                }

                                itemList[index].ItemQuantity = int.Parse(reader["Qty"].ToString());
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;
                            }
                        }
                    }
                }
            }

        }
        public void GetTotalAmounts( )
        {
            decimal totalAmountDisposed = 0;
            decimal totalAmountRetrieved = 0;

            string query = @"
            SELECT 
                (SELECT SUM(Price) FROM DisposedItems) AS TotalAmountDisposed,
                (SELECT SUM(TotalPrice) FROM RetrieveItems) AS TotalAmountRetrieved;";

            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    totalAmountDisposed = reader.GetDecimal(0);
                                    int index1 = chart1.Series["PieChart1"].Points.AddXY("Disposed :" + totalAmountDisposed, totalAmountDisposed);
                                    chart1.Series["PieChart1"].Points[index1].Color = Color.FromArgb(255, 128, 128);
                                }

                                if (!reader.IsDBNull(1))
                                {
                                    totalAmountRetrieved = reader.GetDecimal(1);
                                    int index2 = chart1.Series["PieChart1"].Points.AddXY("Retrieved :" + totalAmountRetrieved, totalAmountRetrieved);
                                    chart1.Series["PieChart1"].Points[index2].Color = Color.LightGreen;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }
    }
}

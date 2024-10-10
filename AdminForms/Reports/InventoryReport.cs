using Flowershop_Thesis.OtherForms.InventoryReports;
using Flowershop_Thesis.OtherForms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            DonutChartOutofStock();
            FastMovingProduct();
            SlowMovingProduct();
            TopSellingProduct();
            DisposalList();
        }
        private void label88_Click(object sender, EventArgs e)
        {

        }
        public void getLowStockFlowers()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = "SELECT COALESCE(Count(*), 0) AS output FROM ItemInventory WHERE ItemType = 'Individual' AND ItemQuantity < 20;";
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
                string query = " SELECT COALESCE(Count(*), 0) AS output FROM Materials WHERE ItemQuantity < 2;";
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
        public void DonutChartOutofStock()
        {
            int Flowers = 0;
            int Materials = 0;

            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {

                con.Open();
                string query = "SELECT COALESCE(Count(*), 0) AS output FROM ItemInventory WHERE ItemType = 'Individual' AND ItemQuantity = 0 OR ItemStatus = 'Expired';";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["output"] != DBNull.Value && !string.IsNullOrEmpty(reader["output"].ToString()))
                        {
                            Flowers = int.Parse(reader["output"].ToString());
                        }
                        else
                        {
                            Flowers = 0;
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = "SELECT COALESCE(Count(*), 0) AS output FROM Materials WHERE ItemQuantity <= 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["output"] != DBNull.Value && !string.IsNullOrEmpty(reader["output"].ToString()))
                        {
                            Materials = int.Parse(reader["output"].ToString());
                        }
                        else
                        {
                            Materials = 0;
                        }
                    }
                }
            }
            if(Materials != 0)
            {
                label13.Visible = false;
                int index1 = chart2.Series["PieChart1"].Points.AddXY("Materials: " + Materials.ToString(), Materials);
                chart2.Series["PieChart1"].Points[index1].Color = Color.FromArgb(255, 128, 128);
            }
            if(Flowers != 0)
            {
                label13.Visible = false;
                int index2 = chart2.Series["PieChart1"].Points.AddXY("Flowers: " + Flowers.ToString(), Flowers);
                chart2.Series["PieChart1"].Points[index2].Color = Color.LightGreen;
            }
            if(Materials == 0 &&  Flowers == 0)
            {
                label13.Visible = true;
            }



        }
        public void FastMovingProduct()
        {
            string FastMovingID = "0";
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT TOP 1 ItemID, ItemName, SUM(Quantity) AS TotalQuantity, SUM(Price) AS TotalPrice FROM OrderedItems where Type= 'Individual' GROUP BY ItemID, ItemName ORDER BY TotalQuantity DESC;";

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
                string countQuery = "SELECT TOP 1 ItemID, ItemName, SUM(Quantity) AS TotalQuantity, SUM(Price) AS TotalPrice FROM OrderedItems where Type= 'Individual' GROUP BY ItemID, ItemName ORDER BY TotalQuantity ASC;";

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
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT ItemName, SUM(Quantity) AS TotalQuantity FROM OrderedItems GROUP BY ItemName;";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["ItemName"].ToString();
                                int quantity = int.Parse(reader["TotalQuantity"].ToString());
                                chart1.Series["TSP"].Points.AddXY($"{name}: {quantity}", quantity);
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
        public void DisposalList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransaction";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransaction;";
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
        public void EvaluatedList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM CancelledTransaction where Evaluation = 'Evaluated'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DisposalListItems[] inv = new DisposalListItems[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransaction where Evaluation = 'Evaluated';";
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
            {
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
    }
}


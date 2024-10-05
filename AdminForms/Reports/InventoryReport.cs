using Flowershop_Thesis.OtherForms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT top 1 Name, SUM(Quantity) AS TotalQuantity FROM AdvanceOrderItems GROUP BY Name ORDER BY TotalQuantity DESC;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    TransactionsList[] inv = new TransactionsList[rowCount];

                    string sqlQuery = "SELECT * FROM FinishedTransactionList";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index].LocalID = reader["ID"].ToString().Trim();
                                inv[index].TransID = reader["TransactionID"].ToString().Trim();
                                inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                inv[index].Price = reader["TotalPrice"].ToString().Trim();
                                inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                inv[index].Type = reader["TransactionType"].ToString().Trim();
                                inv[index].Date = reader["DOC"].ToString().Trim();

                            }
                        }
                    }

                }
            }
        }


    }
}


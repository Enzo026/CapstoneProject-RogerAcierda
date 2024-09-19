using Flowershop_Thesis.OtherForms.StockAdjustments;
using Flowershop_Thesis.OtherForms.Supplier;
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

namespace Flowershop_Thesis.InventoryClerk.Supplier
{
    public partial class SupplierFrm : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public SupplierFrm()
        {
            InitializeComponent();
            testConnection();
            loadTableData();
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");

            // MessageBox.Show(databaseFilePath);
            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        public void loadTableData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from Supplier where Status='active';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    Inv_Supplier[] itemList = new Inv_Supplier[rowCount];

                    string sqlQuery = "select * from Supplier where Status='active';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new Inv_Supplier();
                                itemList[index].SuppID = int.Parse(reader["SupplierID"].ToString());
                                itemList[index].Suppname = reader["SupplierName"].ToString();
                                itemList[index].SuppType = reader["SupplierType"].ToString();
                                itemList[index].SuppContact= reader["ContactNumber"].ToString();
                                itemList[index].SuppAddress = reader["SupplierAddress"].ToString();

                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void flower()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from Supplier where Status='active' AND SupplierType = 'Flowers';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    Inv_Supplier[] itemList = new Inv_Supplier[rowCount];

                    string sqlQuery = "select * from Supplier where Status='active' AND SupplierType = 'Flowers';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new Inv_Supplier();
                                itemList[index].SuppID = int.Parse(reader["SupplierID"].ToString());
                                itemList[index].Suppname = reader["SupplierName"].ToString();
                                itemList[index].SuppType = reader["SupplierType"].ToString();
                                itemList[index].SuppContact = reader["ContactNumber"].ToString();
                                itemList[index].SuppAddress = reader["SupplierAddress"].ToString();

                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Materials()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                con.Open();
                string countQuery = "select count(*) from Supplier where Status='active' AND SupplierType = 'Materials';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    Inv_Supplier[] itemList = new Inv_Supplier[rowCount];

                    string sqlQuery = "select * from Supplier where Status='active' AND SupplierType = 'Materials';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new Inv_Supplier();
                                itemList[index].SuppID = int.Parse(reader["SupplierID"].ToString());
                                itemList[index].Suppname = reader["SupplierName"].ToString();
                                itemList[index].SuppType = reader["SupplierType"].ToString();
                                itemList[index].SuppContact = reader["ContactNumber"].ToString();
                                itemList[index].SuppAddress = reader["SupplierAddress"].ToString();

                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;

                            }
                        }
                    }

                }
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flower();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Materials();
        }
    }
}

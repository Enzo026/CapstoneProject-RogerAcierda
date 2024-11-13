using Capstone_Flowershop;
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
        public SupplierFrm()
        {
            InitializeComponent();
            loadTableData();
        }

        public void loadTableData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM Supplier WHERE Status='active';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Inv_Supplier[] itemList = new Inv_Supplier[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier WHERE Status='active';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new Inv_Supplier
                                    {
                                        SuppID = Convert.ToInt32(reader["SupplierID"]),
                                        Suppname = reader["SupplierName"].ToString(),
                                        SuppType = reader["SupplierType"].ToString(),
                                        SuppContact = reader["ContactNumber"].ToString(),
                                        SuppAddress = reader["SupplierAddress"].ToString()
                                    };

                                    // Read the image data from the database
                                    if (reader["Image"] != DBNull.Value)
                                    {
                                        byte[] imgData = (byte[])reader["Image"];
                                        using (MemoryStream ms = new MemoryStream(imgData))
                                        {
                                            itemList[index].Img = Image.FromStream(ms);
                                        }
                                    }

                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        public void flower()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                                    if (reader["Image"] != DBNull.Value)
                                    {
                                        byte[] imgData = (byte[])reader["Image"];
                                        using (MemoryStream ms = new MemoryStream(imgData))
                                        {
                                            itemList[index].Img = Image.FromStream(ms);
                                        }
                                    }
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }
                }
               
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                                    if (reader["Image"] != DBNull.Value)
                                    {
                                        byte[] imgData = (byte[])reader["Image"];
                                        using (MemoryStream ms = new MemoryStream(imgData))
                                        {
                                            itemList[index].Img = Image.FromStream(ms);
                                        }
                                    }
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }

                }
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

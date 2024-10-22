using Capstone_Flowershop;
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

namespace Flowershop_Thesis.OtherForms.Reports.Calendar
{
    public partial class AdvanceOrderListItems : Form
    {
        public AdvanceOrderListItems()
        {
            InitializeComponent();
            label25.Visible = false;
            pictureBox1.Visible = false;
        }
        public void getListInfo()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM AdvanceOrderItems where OrderID = @Id";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Id", ChangeIds.TransactionLogID);
                        
                        int rowCount = (int)countCommand.ExecuteScalar();
                        AO_ListItems[] inv = new AO_ListItems[rowCount];
                        label7.Text = rowCount.ToString();

                        string sqlQuery = "SELECT * FROM AdvanceOrderItems where OrderID = @Id";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Id", ChangeIds.TransactionLogID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new AO_ListItems();
                                    inv[index].Price = reader["Price"].ToString().Trim();
                                    inv[index].Name = reader["Name"].ToString().Trim();
                                    inv[index].OrderQuantity = reader["Quantity"].ToString();
                         
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
                MessageBox.Show("Error on Displaying Order List :" + ex.Message);
            }
        }
        public void getInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM AdvanceOrders WHERE OrderID = @Id";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", ChangeIds.TransactionLogID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                label6.Text = reader["CustomerName"].ToString().Trim();
                                label11.Text = reader["ContactNo"].ToString().Trim();
                                label9.Text = reader["OrderType"].ToString().Trim();
                                label13.Text = reader["PickupDate"].ToString().Trim();
                                label15.Text = reader["EmployeeName"].ToString().Trim();
                                label28.Text = reader["OrderID"].ToString().Trim();
                                label21.Text = reader["Downpayment"].ToString().Trim();
                                label23.Text = reader["Discount"].ToString().Trim();
                                label17.Text = reader["TotalPrice"].ToString().Trim();
                                label19.Text = reader["TotalPrice"].ToString().Trim();
                                label30.Text = reader["ModeOfPayment"].ToString().Trim();
                                // Assuming the image is stored in a column named "ImageData"

                                if(reader["ModeOfPayment"].ToString() == "Gcash")
                                {
                                    label25.Visible = true;
                                    pictureBox1.Visible = true;
                                    if (reader["Image"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["Image"];
                                        using (var ms = new MemoryStream(imageData))
                                        {
                                            pictureBox1.Image = Image.FromStream(ms);
                                        }
                                    }
                                }
                                else
                                {
                                    label25.Visible = false;
                                    pictureBox1.Visible = false;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction Info: " + ex.Message);
            }

        }

        private void AdvanceOrderListItems_Load(object sender, EventArgs e)
        {
            getListInfo();
            getInfo();
        }
    }
}

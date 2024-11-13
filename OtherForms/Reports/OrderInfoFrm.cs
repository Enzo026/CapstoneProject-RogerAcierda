using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.Reports.Calendar;
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

namespace Flowershop_Thesis.OtherForms.Reports
{
    public partial class OrderInfoFrm : Form
    {
        public OrderInfoFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void AOgetListInfo()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM AdvanceOrderItems where OrderID = @Id";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Id", ViewInfo.ID);

                        int rowCount = (int)countCommand.ExecuteScalar();
                        OIF_List[] inv = new OIF_List[rowCount];
                        label43.Text = rowCount.ToString();

                        string sqlQuery = "SELECT * FROM AdvanceOrderItems where OrderID = @Id";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Id", ViewInfo.ID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new OIF_List();
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
        public void WIgetListInfo()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM SalesItemTbl where TransactionID = @Id";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Id", ViewInfo.ID);

                        int rowCount = (int)countCommand.ExecuteScalar();
                        OIF_List[] inv = new OIF_List[rowCount];
                        label43.Text = rowCount.ToString();

                        string sqlQuery = "SELECT * FROM SalesItemTbl where TransactionID = @Id";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Id", ViewInfo.ID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new OIF_List();
                                    inv[index].Price = reader["ItemPrice"].ToString().Trim();
                                    inv[index].Name = reader["ItemName"].ToString().Trim();
                                    inv[index].OrderQuantity = reader["ItemQuantity"].ToString();

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
        public void AO_Info()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM AdvanceOrders WHERE OrderID = @Id"; 
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", ViewInfo.ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                label4.Text = reader["CustomerName"].ToString().Trim();
                                label10.Text = reader["DateOfReservation"].ToString().Trim();
                                label17.Text = reader["ContactNo"].ToString().Trim();
                                label19.Text = reader["PickupDate"].ToString().Trim();
                                label8.Text = reader["EmployeeName"].ToString().Trim();
                                label9.Text = reader["OrderID"].ToString().Trim();
                                label47.Text = reader["Discount"].ToString().Trim();
                                label41.Text = reader["TotalPrice"].ToString().Trim();
                                label45.Text = reader["ModeOfPayment"].ToString().Trim();
                                // Assuming the image is stored in a column named "ImageData"

                                if (reader["ModeOfPayment"].ToString() == "GCash")
                                {
                                    label49.Visible = true;
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
                                    label49.Visible = false;
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

        public void WI_Info()
        {
            try
            {   
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM TransactionsTbl WHERE TransactionID = @Id";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", ViewInfo.ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                label4.Text = reader["CustomerName"].ToString().Trim();
                                label10.Text = reader["DateOfTransaction"].ToString().Trim();
                                label8.Text = reader["Employee"].ToString().Trim();
                                label9.Text = reader["TransactionID"].ToString().Trim();
                                label47.Text = reader["Discount"].ToString().Trim();
                                label41.Text = reader["Price"].ToString().Trim();
                                label45.Text = reader["PaymentMethod"].ToString().Trim();
                                // Assuming the image is stored in a column named "ImageData"

                                if (reader["PaymentMethod"].ToString() == "GCash")
                                {
                                    label49.Visible = true;
                                    pictureBox1.Visible = true;
                                    if (reader["PaymentImage"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["PaymentImage"];
                                        using (var ms = new MemoryStream(imageData))
                                        {
                                            pictureBox1.Image = Image.FromStream(ms);
                                        }
                                    }
                                }
                                else
                                {
                                    label49.Visible = false;
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

        private void OrderInfoFrm_Load(object sender, EventArgs e)
        {   
            try
            {

                if (ViewInfo.type.Trim() == "Walk-inTransaction")
                {
                    WI_Info();
                    WIgetListInfo();
                }
                else if (ViewInfo.type.Trim() == "AdvanceOrder")
                {
                    AO_Info();
                    AOgetListInfo();
                }
                else
                {
                    MessageBox.Show("Im having Trouble pickup up the type");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OrderInfoFrm Error : " + ex.Message);
            }

        }
    }
}

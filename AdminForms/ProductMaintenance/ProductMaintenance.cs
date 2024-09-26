using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
using Flowershop_Thesis.OtherForms.Accounts;
using Flowershop_Thesis.OtherForms.ProductMaintenance;
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

namespace Capstone_Flowershop.AdminForms.ProductMaintenance
{
    public partial class ProductMaintenance : Form
    {
        #region SQL Connection things?
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

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
                    con2 = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        #endregion
        public ProductMaintenance()
        {
            InitializeComponent();
            testConnection();
            DisplayFlowers();
            ChangeIds.ItemType = "ItemInventory";
        }
        public void DisplayFlowers()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                con.Open();
                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new ProductMaintenanceListItem();
                                inv[index].ItmID = reader["ItemID"].ToString().Trim();
                                inv[index].ItmName = reader["ItemName"].ToString().Trim();
                                inv[index].ItmQty = reader["ItemQuantity"].ToString().Trim();
                                inv[index].ItmPrice = reader["Price"].ToString().Trim();

                                if (reader["ItemImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["ItemImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

                                flowLayoutPanel1.Controls.Add(inv[index]);
                                index++;
                            }
                        }
                    }

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void DisplayMaterials()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                con.Open();
                string countQuery = "SELECT COUNT(*) FROM Materials where ItemStatus = 'Available' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                    string sqlQuery = "SELECT * FROM Materials where ItemStatus = 'Available'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new ProductMaintenanceListItem();
                                inv[index].ItmID = reader["ItemID"].ToString().Trim();
                                inv[index].ItmName = reader["ItemName"].ToString().Trim();
                                inv[index].ItmQty = reader["ItemQuantity"].ToString().Trim();
                                inv[index].ItmPrice = reader["Price"].ToString().Trim();

                                if (reader["Image"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["Image"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

                                flowLayoutPanel1.Controls.Add(inv[index]);
                                index++;
                            }
                        }
                    }

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Admin_Supplier form = new Admin_Supplier(); 
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PickAddProduct frm = new PickAddProduct();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisplayFlowers();
            label2.Text = "Flowers and Bouquet";
            ChangeIds.ItemType = "ItemInventory";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayMaterials();
            label2.Text = "Materials";
            ChangeIds.ItemType = "Materials";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeactivatedItemsFrm frm = new DeactivatedItemsFrm();
            frm.ShowDialog();
        }
    }
}

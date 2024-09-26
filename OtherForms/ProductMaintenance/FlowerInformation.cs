using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class FlowerInformation : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        string connectionString;
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

            // MessageBox.Show(databaseFilePath);
            // Build the connection string
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

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
        public FlowerInformation()
        {
            InitializeComponent();
            testConnection();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            deactFlower();
        }
        public void deactFlower()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to mark this item unavailable?", "Mark Unavailable Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {

                        string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                        {
                            conn.Open();
                            countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                            numId = (int)countCommand.ExecuteScalar();
                        }
                    }
                    if (numId == 1)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string updateQuery = "UPDATE ItemInventory SET ItemStatus = 'Unavailable' WHERE ItemID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                            {
                                updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                                conn.Open();
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Item Marked As Unavailable! Please refresh list to see changes");
                        this.Close();
                    }
                    else if (numId > 1){MessageBox.Show("There are multiple Users in this ID");}
                    else{ MessageBox.Show("No Account Found!");}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void LoadFlower()
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemID = @ID ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                        int rowCount = (int)countCommand.ExecuteScalar();
                      
                        if(rowCount == 1) 
                        {
                            string sqlQuery = "SELECT * FROM ItemInventory where ItemID = @ID";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read())
                                    {
                       
   
                                        label1.Text = reader["ItemName"].ToString().Trim();
                                        label15.Text = reader["ItemQuantity"].ToString().Trim();
                                        label14.Text = reader["Price"].ToString().Trim();
                                        label7.Text = reader["ItemDescription"].ToString().Trim();
                                        label13.Text = reader["ItemType"].ToString().Trim();
                                        label12.Text = reader["ItemColor"].ToString().Trim();
                                        label16.Text = reader["LifeSpan"].ToString().Trim();
                                        label17.Text = reader["SuppliedDate"].ToString().Trim();
                                        label18.Text = reader["Supplier"].ToString().Trim();
                                        label19.Text = reader["ItemStatus"].ToString().Trim();

                                        if (reader["ItemImage"] != DBNull.Value)
                                        {
                                            byte[] imageData = (byte[])reader["ItemImage"];
                                            using (MemoryStream ms = new MemoryStream(imageData))
                                            {
                                                pictureBox1.Image = Image.FromStream(ms);
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }

        private void FlowerInformation_Load(object sender, EventArgs e)
        {
            LoadFlower();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditFlower frm = new EditFlower();
            frm.ShowDialog();
        }
    }
}

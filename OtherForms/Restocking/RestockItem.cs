using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.OtherForms.Abuel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.Restocking
{
    public partial class RestockItem : Form
    {
        public RestockItem()
        {
            InitializeComponent();
        }
        string type;
        private void RestockItem_Load(object sender, EventArgs e)
        {
            if(RestockingProcess.type == "Flowers")
            {
                getinfoFlower();
                LoadSuppliers();
                type = "Flowers";
            }
            else if (RestockingProcess.type == "Materials")
            {
                getinfoMaterials();
                LoadSuppliers();
                type = "Materials";
            }
            else
            {
                MessageBox.Show("Error on getting the item information");
                this.Close();
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                // Clear existing items in the ComboBox
                comboBox1.Items.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT SupplierName FROM Supplier where status = 'Active'"; 

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["SupplierName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading suppliers: " + e.Message);
            }
        }

        public void getinfoFlower()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM ItemInventory WHERE ItemStatus = 'Available'  AND ItemID = @Id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", RestockingProcess.ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                label4.Text = reader["ItemID"].ToString();
                                label2.Text = reader["ItemName"].ToString();
                                label7.Text = reader["ItemQuantity"].ToString();
                                pictureBox1.Image = GetImageFromDatabase(reader["ItemImage"]);
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
        private Image GetImageFromDatabase(object imgData)
        {
            if (imgData is DBNull)
                return null;

            byte[] imageBytes = (byte[])imgData; // Cast to byte array
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms); // Create Image from MemoryStream
            }
        }
        public void getinfoMaterials()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Materials WHERE ItemStatus = 'Available'  AND ItemID = @Id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Id", RestockingProcess.ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                label4.Text = reader["ItemID"].ToString();
                                label2.Text = reader["ItemName"].ToString();
                                label7.Text = reader["ItemQuantity"].ToString();
                                pictureBox1.Image = GetImageFromDatabase(reader["Image"]);
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
            bool stat = ValidateInputs();
            if(stat == true)
            {
                InsertIntoTempRestockTbl();
            }
            else
            {
                MessageBox.Show("Please make sure all needed fields are filled");
            }
        }
        private void InsertIntoTempRestockTbl()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO TempRestockTbl (ItemID, Qty, Supplier, ItemName, Type) VALUES (@ItemID, @Qty, @Supplier, @ItemName , @Type)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        // Set parameters with values from controls
                        command.Parameters.AddWithValue("@ItemID", label4.Text);
                        command.Parameters.AddWithValue("@Qty", int.Parse(textBox1.Text)); // Make sure to handle potential parsing errors
                        command.Parameters.AddWithValue("@Supplier", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@ItemName", label2.Text);
                        command.Parameters.AddWithValue("@Type", type);


                        // Execute the command
                        int affectedrows  = command.ExecuteNonQuery();
                        
                        if(affectedrows > 0)
                        {
                            RestockNew.instance.RestockNum.Text = "loading";
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error on inputting items in restockingTable: " + e.Message);
            }
        }
        private bool ValidateInputs()
        {
            // Check if an item is selected in comboBox1
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a supplier from the list.");
                return false;
            }

            // Check if textbox1 is not empty and can be parsed to an integer
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return false;
            }

            return true; // All checks passed
        }

    }
}

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
    public partial class ProductMaintenanceFrm : Form
    {
        public static ProductMaintenanceFrm instance;
        public Label refresh;
        public ProductMaintenanceFrm()
        {
            InitializeComponent();
            instance = this;
            refresh = refresher;
            DisplayFlowers();
            ChangeIds.ItemType = "ItemInventory";
        }
        public void DisplayFlowers()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                {
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
                }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
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
                }
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
            selectedPage = "Flower";

            button3.ForeColor = Color.White;
            button3.BackColor = Color.DarkSlateBlue;

            button4.ForeColor = Color.DarkSlateBlue;
            button4.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayMaterials();
            label2.Text = "Materials";
            ChangeIds.ItemType = "Materials";
            selectedPage = "Material";


            button4.ForeColor = Color.White;
            button4.BackColor = Color.DarkSlateBlue;

            button3.ForeColor = Color.DarkSlateBlue;
            button3.BackColor = Color.White;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeactivatedItemsFrm frm = new DeactivatedItemsFrm();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisplayFlowers();
        }
        string selectedPage = "Flower";
        private void refresher_VisibleChanged(object sender, EventArgs e)
        {
            if (refresher.Visible) {
            if(selectedPage == "Flower")
                {
                    DisplayFlowers();
                    refresher.Visible = false;
                }
            else if(selectedPage == "Material")
                {
                    DisplayMaterials();
                    refresher.Visible = false;
                }
                
            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                if(selectedPage == "Flower")
                {
                    if (radioButton1.Checked)
                    {
                        try
                        {
                            flowLayoutPanel1.Controls.Clear();
                            using (SqlConnection con = new SqlConnection(Connect.connectionString))
                            {
                                con.Open();
                                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND ItemName Like @Name ";
                                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                                {
                                    countCommand.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
                                    int rowCount = (int)countCommand.ExecuteScalar();
                                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND ItemName Like @Name ";
                                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                                    {
                                        command.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Individual: " + ex.Message);
                        }

                    }
                    else if (radioButton2.Checked)
                    {
                        try
                        {
                            flowLayoutPanel1.Controls.Clear();
                            using (SqlConnection con = new SqlConnection(Connect.connectionString))
                            {
                                con.Open();
                                string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND Supplier Like @Name ";
                                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                                {
                                    countCommand.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
                                    int rowCount = (int)countCommand.ExecuteScalar();
                                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                                    string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Available' AND Supplier Like @Name ";
                                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                                    {
                                        command.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Individual: " + ex.Message);
                        }

                    }
                }
                else if (selectedPage == "Material")
                {
                    if (radioButton1.Checked)
                    {
                        try
                        {
                            flowLayoutPanel1.Controls.Clear();
                            using (SqlConnection con = new SqlConnection(Connect.connectionString))
                            {
                                con.Open();
                                string countQuery = "SELECT COUNT(*) FROM Materials where ItemStatus = 'Available' AND ItemName Like @Name ";
                                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                                {
                                    countCommand.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
                                    int rowCount = (int)countCommand.ExecuteScalar();
                                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                                    string sqlQuery = "SELECT * FROM Materials where ItemStatus = 'Available' AND ItemName Like @Name ";
                                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                                    {
                                        command.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Individual: " + ex.Message);
                        }

                    }
                    else if (radioButton2.Checked)
                    {
                        try
                        {
                            flowLayoutPanel1.Controls.Clear();
                            using (SqlConnection con = new SqlConnection(Connect.connectionString))
                            {
                                con.Open();
                                string countQuery = "SELECT COUNT(*) FROM Materials where ItemStatus = 'Available' AND Supplier Like @Name ";
                                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                                {
                                    countCommand.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
                                    int rowCount = (int)countCommand.ExecuteScalar();
                                    ProductMaintenanceListItem[] inv = new ProductMaintenanceListItem[rowCount];

                                    string sqlQuery = "SELECT * FROM Materials where ItemStatus = 'Available' AND Supplier Like @Name ";
                                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                                    {
                                        command.Parameters.AddWithValue("@Name", textBox1.Text.Trim() + "%");
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Individual: " + ex.Message);
                        }

                    }
                }
            }
        }
    }
}

using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.OtherForms.Supplier;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
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

namespace Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier
{
    public partial class Admin_Supplier : Form
    {

        public static Admin_Supplier instance;
        public Label SuppName;
        public Label SuppContactNo;
        public Label SuppAddress;
        public Label SuppType;
        public PictureBox SuppImg;
        public Label inactiveCounter;


        bool formisload;
        public Admin_Supplier()
        {
            InitializeComponent();
            instance = this;
            DisplayList();
            DisplayDeactivated();

      
            SuppName = label3;
            SuppContactNo = label5;
            SuppType = label7;
            SuppAddress = label10;
            SuppImg = pictureBox1;
            inactiveCounter = label15;
            button5.Enabled = false;
            button4.Enabled = false;

        }
        #region methods
        public void SortItemsTextBox()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' AND SupplierName like @name  ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@name", textBox1.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Admin_SupplierList[] inv = new Admin_SupplierList[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Active' AND SupplierName like @name ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@name", textBox1.Text.Trim()+"%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Admin_SupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();
                                    inv[index].SuppType = reader["SupplierType"].ToString();
                                    inv[index].SuppContact = reader["ContactNumber"].ToString();
                                    inv[index].SuppAdd = reader["SupplierAddress"].ToString();

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
                MessageBox.Show("Error Materials: " + ex.Message);
            }
        }
        public void SortMaterialSupplier()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' AND SupplierType = 'Materials'  ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Admin_SupplierList[] inv = new Admin_SupplierList[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Active' AND SupplierType = 'Materials' ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Admin_SupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();
                                    inv[index].SuppType = reader["SupplierType"].ToString();
                                    inv[index].SuppContact = reader["ContactNumber"].ToString();
                                    inv[index].SuppAdd = reader["SupplierAddress"].ToString();

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
                MessageBox.Show("Error Materials: " + ex.Message);
            }
        }
        public void SortFlowerSupplier()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' AND SupplierType = 'Flowers'  ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Admin_SupplierList[] inv = new Admin_SupplierList[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Active' AND SupplierType = 'Flowers' ";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Admin_SupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();
                                    inv[index].SuppType = reader["SupplierType"].ToString();
                                    inv[index].SuppContact = reader["ContactNumber"].ToString();
                                    inv[index].SuppAdd = reader["SupplierAddress"].ToString();

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
                MessageBox.Show("Error Flower: " + ex.Message);
            }
        }
        public void DisplayListAZ()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active'  ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Admin_SupplierList[] inv = new Admin_SupplierList[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Active' order by SupplierName asc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Admin_SupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();
                                    inv[index].SuppType = reader["SupplierType"].ToString();
                                    inv[index].SuppContact = reader["ContactNumber"].ToString();
                                    inv[index].SuppAdd = reader["SupplierAddress"].ToString();

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
                //MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void DisplayList()
        {
            try
            {   flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Admin_SupplierList[] inv = new Admin_SupplierList[rowCount];

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Active'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Admin_SupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();
                                    inv[index].SuppType = reader["SupplierType"].ToString();
                                    inv[index].SuppContact = reader["ContactNumber"].ToString();
                                    inv[index].SuppAdd = reader["SupplierAddress"].ToString();

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
                //MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void DisplayDeactivated()
        {
            try
            {   
                flowLayoutPanel2.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Inactive' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {

                        int rowCount = (int)countCommand.ExecuteScalar();
                        InactiveSupplierList[] inv = new InactiveSupplierList[rowCount];
                        label15.Text = rowCount.ToString();

                        string sqlQuery = "SELECT * FROM Supplier where Status = 'Inactive'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new InactiveSupplierList();
                                    inv[index].SuppID = reader["SupplierID"].ToString();
                                    inv[index].Suppname = reader["SupplierName"].ToString();

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
                MessageBox.Show("Error Displaying Inactivated User: " + ex.Message);
            }
        }
        public void DeactivateUser()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to make this supplier inactive?", "Mark as Inactive Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            con.Open();
                            countCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);
                            numId = (int)countCommand.ExecuteScalar();
                            con.Close();
                        }
                        string updateQuery = "UPDATE Supplier SET Status = 'Inactive' WHERE SupplierID = @ID;";
                        if (numId == 1)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                con.Open();
                                updateCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);

                                updateCommand.ExecuteNonQuery();
                                con.Close();

                            }

                            MessageBox.Show("User Deactivated!");
                            label15.Text = "null";
                        }
                        else if (numId > 1)
                        {
                            MessageBox.Show("There are multiple Users in this ID");
                        }
                        else
                        {
                            MessageBox.Show("No Account Found!");
                        }



                    }
                }
 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region UI Elements
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
           DeactivateUser();
            label3.Text = "Null";
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            EditSupplier form = new EditSupplier();
            form.SuppID = ChangeIds.SupplierId;
            form.Suppname = label3.Text;
            form.SuppContact = label5.Text;
            form.SuppType = label7.Text;
            form.SuppAdd = label10.Text;
            form.img = pictureBox1.Image;



            form.Show();


        }

        private void label3_TextChanged(object sender, EventArgs e)
        {
            if(label3.Text == "Null")
            {
                DisplayList();
                pictureBox1.Image = null;
                label5.Text = null;
                label7.Text = null;
                label10.Text = null;
                button5.Enabled = false;
                button4.Enabled = false;

            }
            else
            {
                button5.Enabled= true;
                button4.Enabled= true; 
            }
        }

        private void label15_TextChanged(object sender, EventArgs e)
        {
            if (formisload == true)
            {
                DisplayDeactivated();
                DisplayList();
            }

        }
        
        private void Admin_Supplier_Load(object sender, EventArgs e)
        {
            formisload = true;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayListAZ();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SortFlowerSupplier();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SortMaterialSupplier();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                SortItemsTextBox();
            }
            else
            {
                DisplayList();
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddSupplier frm = new AddSupplier();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DisplayList();
        }
    }
}

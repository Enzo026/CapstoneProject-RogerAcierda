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
        #region SQL Connection things?
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        #endregion

        public static Admin_Supplier instance;
        public Label SuppName;
        public Label SuppContactNo;
        public Label SuppAddress;
        public Label SuppType;
        public PictureBox SuppImg;
        public Label inactiveCounter;
        public Admin_Supplier()
        {
            InitializeComponent();
            instance = this;
            testConnection();
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
        public void DisplayList()
        {
            try
            {   flowLayoutPanel1.Controls.Clear();

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

                con.Close();
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
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Displaying Inactivated User: " + ex.Message);
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
                else
                {
                    //none
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
        bool formisload;
        private void Admin_Supplier_Load(object sender, EventArgs e)
        {
            formisload = true;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}

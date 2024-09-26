using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.System_Maintenance;
using Flowershop_Thesis.OtherForms.Accounts.EditAccountContents;
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

namespace Flowershop_Thesis.OtherForms.Accounts
{
    public partial class EditAccount : Form
    {

        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));
            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

            // Build the connection string with explicit pooling parameters
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;Pooling=true;Max Pool Size=100;Min Pool Size=5;Connection Lifetime=600;";


            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions
                    MessageBox.Show("SQL error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            } // Connection is automatically closed and returned to the pool here
        }
        
        
        public EditAccount()
        {
            InitializeComponent();
            testConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditUserInfo frm = new EditUserInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditRole frm = new EditRole();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AccountInfo frm = new AccountInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void EditAccount_Load(object sender, EventArgs e)
        {
            label3.Text = ChangeIds.AccountID;
            panel2.Controls.Clear();
            EditUserInfo frm = new EditUserInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        public void deact()
        {
          if(UserInfo.EmpID == ChangeIds.AccountID)
            {
                try
                {
                    DialogResult result = MessageBox.Show("You are about Deactivate your Account?", "Deactivate Account", MessageBoxButtons.YesNo);
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
                            Form1 frm = new Form1();
                            frm.Show();
                            this.Close();

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
            else
            {
                try
                {
                    DialogResult result = MessageBox.Show("You are about Deactivate this user?", "Deactivate", MessageBoxButtons.YesNo);
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
               
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

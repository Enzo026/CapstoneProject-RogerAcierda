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

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class EditUserInfo : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));


            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");



            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //MessageBox.Show("Database connection opened successfully.");

                    con = new SqlConnection(connectionString);
                    //label6.Text = connectionString;
                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        public EditUserInfo()
        {
            InitializeComponent();
            testConnection();
        }

        public void change()
        {
            try
            {

                DialogResult result = MessageBox.Show("You are about to proceed Changes on this supplier", "Proceed Edit Information", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    int numId;

                    string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        con.Open();
                        countCommand.Parameters.AddWithValue("@ID", SupplierID);
                        numId = (int)countCommand.ExecuteScalar();
                        con.Close();
                    }

                    if (numId == 1)
                    {
                        if (Image.Image != SupplierImage)
                        {
                            editinfowithImage();
                        }
                        else
                        {
                            editinfo();
                        }
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
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

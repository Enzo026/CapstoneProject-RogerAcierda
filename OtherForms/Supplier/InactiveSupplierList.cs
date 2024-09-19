using Capstone_Flowershop;
using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
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

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class InactiveSupplierList : UserControl
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
        public InactiveSupplierList()
        {
            InitializeComponent();
            testConnection();
        }
        #region Myregion
        private string SupplierID;
        private string SupplierName;


        [Category("ItemList")]
        public string SuppID
        {
            get { return SupplierID; }
            set { SupplierID = value; }
        }
        [Category("ItemList")]
        public string Suppname
        {
            get { return SupplierName; }
            set { SupplierName = value; SuppName.Text = value.ToString(); }
        }


        #endregion

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to make this supplier Active?", "Mark as Inactive Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    con.Open();
                    string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@ID", SupplierID);
                        numId = (int)countCommand.ExecuteScalar();

                    }
                    string updateQuery = "UPDATE Supplier SET Status = 'Active' WHERE SupplierID = @ID;";
                    if (numId == 1)
                    {
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", SupplierID);

                            updateCommand.ExecuteNonQuery();


                        }

                        MessageBox.Show("User Activated!");
                        Admin_Supplier.instance.inactiveCounter.Text = "0";
                    }
                    else if (numId > 1)
                    {
                        MessageBox.Show("There are multiple Users in this ID");
                    }
                    else
                    {
                        MessageBox.Show("No Account Found!");
                    }
                    con.Close();


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
}

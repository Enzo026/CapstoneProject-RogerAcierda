using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class DeactivatedListItems : UserControl
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
        public DeactivatedListItems()
        {
            InitializeComponent();
            testConnection();
        }
        #region Myregion
        private string ItemID;
        private string ItemName;
        private string ItemType;


        [Category("ItemList")]
        public string ItmID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }
        [Category("ItemList")]
        public string Itmname
        {
            get { return ItemName; }
            set { ItemName = value; NameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Itmtype
        {
            get { return ItemType; }
            set { ItemType = value; }
        }


        #endregion

        public void MarkAvailable()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to make this Item Available?", "Mark as Available Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   
                    if(ItemType == "Flower")
                    {
                        int numId;
                        con.Open();
                        string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@ID", ItemID);
                            numId = (int)countCommand.ExecuteScalar();

                        }
                        string updateQuery = "UPDATE ItemInventory SET ItemStatus = 'Available' WHERE ItemID = @ID;";
                        if (numId == 1)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", ItemID);

                                updateCommand.ExecuteNonQuery();


                            }

                            MessageBox.Show("Item Marked as Available! Please Refresh List to view Changes");
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
                    else if(ItemType == "Materials")
                    {
                        int numId;
                        con.Open();
                        string countQuery = "Select count(*) from Materials where ItemID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@ID", ItemID);
                            numId = (int)countCommand.ExecuteScalar();

                        }
                        string updateQuery = "UPDATE Materials SET ItemStatus = 'Available' WHERE ItemID = @ID;";
                        if (numId == 1)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", ItemID);

                                updateCommand.ExecuteNonQuery();


                            }

                            MessageBox.Show("Item Marked as Available! Please Refresh List to view Changes");
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

        private void button1_Click(object sender, EventArgs e)
        {
            MarkAvailable();
        }
    }
}

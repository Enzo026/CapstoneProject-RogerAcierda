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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class DeactivatedListItems : UserControl
    {

        public DeactivatedListItems()
        {
            InitializeComponent();

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
                        using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
                 
                                addActivityLog();
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
                        }
                     
                      
                    }
                    else if(ItemType == "Materials")
                    {   
                        using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
                                addActivityLog();
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

                        }

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
        public void addActivityLog()
        {
            try
            {   using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Item changed Status");
                    cmd.Parameters.AddWithValue("@Definition", "NotGiven");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", ItemID);
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Marked " + ItemName + " as available item");


                    cmd.ExecuteNonQuery();
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MarkAvailable();
        }
    }
}

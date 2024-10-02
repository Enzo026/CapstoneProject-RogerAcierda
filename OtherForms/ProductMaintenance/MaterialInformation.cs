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
    public partial class MaterialInformation : Form
    {

        public MaterialInformation()
        {
            InitializeComponent();

            LoadFlower();
          
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        public void LoadFlower()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Materials where ItemID = @ID ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                        int rowCount = (int)countCommand.ExecuteScalar();

                        if (rowCount == 1)
                        {
                            string sqlQuery = "SELECT * FROM Materials where ItemID = @ID";
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
                                        label23.Text = reader["UsageQuantity"].ToString().Trim()+ "/" + reader["UsageQuantity"].ToString().Trim();
                                        label13.Text = reader["ItemType"].ToString().Trim();
                                        label12.Text = reader["ItemColor"].ToString().Trim();
                                        label17.Text = reader["SuppliedDate"].ToString().Trim();
                                        label18.Text = reader["Supplier"].ToString().Trim();
                                        label19.Text = reader["ItemStatus"].ToString().Trim();
                                        label21.Text = reader["UnitPrice"].ToString().Trim();

                                        int usageQty = int.Parse(reader["UsageQuantity"].ToString().Trim());
                                        int usage = int.Parse(reader["Usage"].ToString().Trim());

                                        if (reader["Image"] != DBNull.Value)
                                        {
                                            byte[] imageData = (byte[])reader["Image"];
                                            using (MemoryStream ms = new MemoryStream(imageData))
                                            {
                                                pictureBox1.Image = Image.FromStream(ms);
                                            }
                                        }
                                        statusbar(usageQty,usage);
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
        public void statusbar(int obj, int usage)
        {

            int panel1width = 230; // the full width of panel 1
            int panel2width;   // the width to be adjusted based on the percentage

            // Calculate the percentage
            double percentage = ((double)usage / obj) * 100;

            // Adjust panel2width based on the percentage
            panel2width = (int)((percentage / 100) * panel1width);

            panel3.Width = panel2width;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deactMaterial();
        }
        public void addActivityLog()
        {
            try
            {   using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Item(Material) Deactivated");
                    cmd.Parameters.AddWithValue("@Definition", "NotGiven");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", ChangeIds.ItemID);
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Deactivated an Item(Material) named as " + label1.Text);


                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }
        public void deactMaterial()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to mark this item unavailable?", "Mark Unavailable Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string countQuery = "Select count(*) from Materials where ItemID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                        {
                            conn.Open();
                            countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                            numId = (int)countCommand.ExecuteScalar();
                        }
                    }
                    if (numId == 1)
                    {
                        using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                        {
                            string updateQuery = "UPDATE Materials SET ItemStatus = 'Unavailable' WHERE ItemID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                            {
                                updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID);
                                conn.Open();
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Item Marked As Unavailable! Please refresh list to see changes");
                        addActivityLog();
                        this.Close();
                    }
                    else if (numId > 1) { MessageBox.Show("There are multiple Users in this ID"); }
                    else { MessageBox.Show("No Account Found!"); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditMaterials frm = new EditMaterials();
            frm.ShowDialog();
        }
    }
}

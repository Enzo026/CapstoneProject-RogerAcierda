using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.Accounts;
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
    public partial class DeactivatedItemsFrm : Form
    {

        public DeactivatedItemsFrm()
        {
            InitializeComponent();
       
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void getDeactivatedFlowers()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Unavailable' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DeactivatedListItems[] inv = new DeactivatedListItems[rowCount];
                       

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemStatus = 'Unavailable'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DeactivatedListItems();
                                    inv[index].ItmID = reader["ItemID"].ToString().Trim();
                                    inv[index].Itmname = reader["ItemName"].ToString().Trim();
                                    inv[index].Itmtype = "Flower";


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
                MessageBox.Show("Error Deactivated Flowers : " + ex.Message);
            }
        }
        public void getDeactivatedMaterials()
        {
            try
            {

                using (SqlConnection con =  new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM Materials where ItemStatus = 'Unavailable' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DeactivatedListItems[] inv = new DeactivatedListItems[rowCount];


                        string sqlQuery = "SELECT * FROM Materials where ItemStatus = 'Unavailable'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DeactivatedListItems();
                                    inv[index].ItmID = reader["ItemID"].ToString().Trim();
                                    inv[index].Itmname = reader["ItemName"].ToString().Trim();
                                    inv[index].Itmtype = "Materials";


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
                MessageBox.Show("Error Deactivated Materials: " + ex.Message);
            }
        }
        public void refresh()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            getDeactivatedFlowers();
            getDeactivatedMaterials();
        }
        private void DeactivatedItemsFrm_Load(object sender, EventArgs e)
        {   flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            getDeactivatedFlowers();
            getDeactivatedMaterials();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            getDeactivatedFlowers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();  
            getDeactivatedMaterials();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

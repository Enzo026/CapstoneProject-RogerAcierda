using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
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

namespace Flowershop_Thesis.InventoryClerk.LandingPage
{
    public partial class DashboardFrm : Form
    {
        public DashboardFrm()
        {
            InitializeComponent();
            RestockNum();
            SupplierNum();
        }
        public void RestockNum()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM ItemInventory where ItemStatus = 'Available' AND ItemQuantity <= 20 ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        label9.Text = Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void SupplierNum()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM Supplier where Status = 'Active' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int Count = (int)countCommand.ExecuteScalar();
                        label11.Text = Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DashboardFrm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

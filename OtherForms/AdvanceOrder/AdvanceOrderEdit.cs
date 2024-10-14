using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    public partial class AdvanceOrderEdit : Form
    {
        public AdvanceOrderEdit()
        {
            InitializeComponent();
            panel2.Controls.Clear();
            NormalBackground form = new NormalBackground();
            panel2.Controls.Add(form);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditContactNumber editContactNumber = new EditContactNumber();
           panel2.Controls.Add(editContactNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditItemOrders form = new EditItemOrders();
            panel2.Controls.Add(form);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditPickupDate form = new EditPickupDate();
            panel2.Controls.Add(form);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            SeeItems form = new SeeItems();
            panel2.Controls.Add(form);
        }

        public void GetInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM AdvanceOrders where Status = 'Active' AND OrderID = @id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read())
                            {
                                label2.Text = reader["OrderID"].ToString();
                                label7.Text = reader["CustomerName"].ToString();
                                label8.Text = reader["OrderType"].ToString();
                                label10.Text = reader["ContactNo"].ToString();
                                label12.Text = reader["PickupDate"].ToString();
                                label14.Text = reader["TotalPrice"].ToString();
                                label16.Text = reader["Discount"].ToString();
                                label4.Text= reader["Downpayment"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AdvanceOrderEdit_Load(object sender, EventArgs e)
        {
            GetInfo();
        }
    }
}

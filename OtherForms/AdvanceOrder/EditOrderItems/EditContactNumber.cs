using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class EditContactNumber : UserControl
    {
        public EditContactNumber()
        {
            InitializeComponent();
        }

        private void EditContactNumber_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM AdvanceOrders where OrderID = @id ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@id", ChangeIds.TransactionLogID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                label3.Text = reader["ContactNo"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {
                    string countQuery = "Select count(*) from AdvanceOrders where OrderID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE AdvanceOrders SET ContactNo = @In WHERE OrderID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.TransactionLogID);
                            updateCommand.Parameters.AddWithValue("@In", textBox1.Text.Trim());
                            
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Contact Number Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Contact Number :" + ex.Message);
            }
        }
    }
}

using Capstone_Flowershop;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    public partial class SA_AdjustQuantity : Form
    {
        public SA_AdjustQuantity()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadinfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string sqlQuery = "SELECT * FROM RestockingTbl where  Id = @RID";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
      
                        command.Parameters.AddWithValue("@RID", SA_Info.RestockingID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                ItemNameLbl.Text = reader["ItemName"].ToString();
                                QuantityLbl.Text = reader["Qty"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SA_AdjustQuantity_Load(object sender, EventArgs e)
        {
            loadinfo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                label1.Visible = false;
                int MaxQty = int.Parse(QuantityLbl.Text);
                int LessQty = int.Parse(textBox1.Text);

                if(LessQty > MaxQty)
                {
                    MessageBox.Show("Input must be lower to the max value");
                    textBox1.Text = null;
                }
                else
                {
                    int total = MaxQty - LessQty;
                 
                    if (total < 0) {
                        MessageBox.Show("Item will be create negative quantity please input a new one");
                        textBox1.Text = null;
                    }
                    else
                    {
                        label6.Text = total.ToString();
                    }

                }
            

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the key is not a digit or control key, suppress the keypress
                e.Handled = true;
            }
            if (textBox1.Text.Length == 0)
            {
                // If the first character is '0', disable the TextBox
                if (e.KeyChar == '0')
                {
                    e.Handled = true; // Suppress the input
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            
            if(textBox1.Text.Length > 0)
            {
                if (textBox2.Text.Length > 0)
                {
                    if (textBox2.Text == SystemInfo.SecurityCode)
                    {
                        DialogResult result = MessageBox.Show(
                        "You are about to edit item quantity setting the value of " + ItemNameLbl.Text + " into " + label6.Text, // Message text
                        "Confirmation",                      // Title of the MessageBox
                        MessageBoxButtons.YesNo,            // Buttons to show
                        MessageBoxIcon.Question              // Icon to display
                        );

                        // Check the result
                        if (result == DialogResult.Yes)
                        {
                            // User clicked Yes
                            proceedUpdate();
                            // Add your logic for Yes here
                        }
                        else
                        {
                            // User clicked No
                            
                            // Add your logic for No here
                        }
                    }
                    else
                    {
                        label9.Text = "* Wrong Security Code";
                        label9.Visible = true;
                    }
                }
                else
                {
                    label9.Text = "* Must Fill This Field";
                    label9.Visible = true;
                }
            }
            else
            {
                label1.Visible = true;
            }
        }

        public void proceedUpdate()
        {
            bool proceed = false;
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateRestockingAndInventory", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Quantity", int.Parse(textBox1.Text));
                    command.Parameters.AddWithValue("@ItemId", SA_Info.ItemID);
                    command.Parameters.AddWithValue("@restockingid", SA_Info.RestockingID);
                    command.Parameters.AddWithValue("@type", SA_Info.Type);

                    try
                    {
                        con.Open();
                        command.ExecuteNonQuery(); // Execute the stored procedure
                        MessageBox.Show("Inventory updated successfully.");
                        proceed = true;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                }
            }
            if (proceed)
            {
                insertLogs();
            }
        }
        public void insertLogs()
        {
            try
            {

                string logdesc = UserInfo.Empleyado + " adjusted " + ItemNameLbl.Text + " quantity from " + QuantityLbl.Text + " to " + label6.Text + ". Batch ID : " + SA_Info.BatchID;
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Stock Adjusted");
                    cmd.Parameters.AddWithValue("@Definition", logdesc);
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", SA_Info.BatchID);
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Adjusted Qty of " + ItemNameLbl.Text);


                    cmd.ExecuteNonQuery();
                    this.Close();
                    SA_BatchItems.instance.loading.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

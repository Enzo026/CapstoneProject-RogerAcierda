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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class AddUserRole : Form
    {
        public AddUserRole()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddUserRole_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateRole();
        }
        private void CreateRole() {
            string tab1 = "none";
            string tab2 = "none";
            string tab3 = "none";
            string tab4 = "none";
            string tab5 = "none";
            string tab6 = "none";
            string tab7 = "none";
            string tab8 = "none";
            string tab9 = "none";
            string tab10 = "none";
            string tab11 = "none";
            string tab12 = "none";

            if (checkBox1.Checked) tab1 = "Reports";
            if (checkBox2.Checked) tab2 = "ProductMaintenance";
            if (checkBox3.Checked) tab3 = "AccountsMaintenance";
            if (checkBox4.Checked) tab4 = "HistoryLogs";
            if (checkBox5.Checked) tab5 = "SystemMaintenance";
            if (checkBox6.Checked) tab6 = "Transaction";
            if (checkBox7.Checked) tab7 = "Supplier";
            if (checkBox8.Checked) tab8 = "Disposal";
            if (checkBox9.Checked) tab9 = "StockAdjustment";
            if (checkBox10.Checked) tab10 = "Restocking";
            if (checkBox11.Checked) tab11 = "Overview";
            if (checkBox12.Checked) tab12 = "PriceList";

            string insertQuery = "INSERT INTO UserRoles (Name, Tab1, Tab2, Tab3, Tab4, Tab5, Tab6, Tab7, Tab8, Tab9, Tab10, Tab11, Tab12) VALUES (@Name, @Tab1, @Tab2, @Tab3, @Tab4, @Tab5, @Tab6, @Tab7, @Tab8, @Tab9, @Tab10, @Tab11, @Tab12)";

            // Create a connection object
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                // Create the command object
                SqlCommand command = new SqlCommand(insertQuery, connection);

                // Add parameters to the command (prevents SQL injection)
                command.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                command.Parameters.AddWithValue("@Tab1", tab1);
                command.Parameters.AddWithValue("@Tab2", tab2);
                command.Parameters.AddWithValue("@Tab3", tab3);
                command.Parameters.AddWithValue("@Tab4", tab4);
                command.Parameters.AddWithValue("@Tab5", tab5);
                command.Parameters.AddWithValue("@Tab6", tab6);
                command.Parameters.AddWithValue("@Tab7", tab7);
                command.Parameters.AddWithValue("@Tab8", tab8);
                command.Parameters.AddWithValue("@Tab9", tab9);
                command.Parameters.AddWithValue("@Tab10", tab10);
                command.Parameters.AddWithValue("@Tab11", tab11);
                command.Parameters.AddWithValue("@Tab12", tab12);

                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the INSERT command
                    int rowsAffected = command.ExecuteNonQuery();

                    MessageBox.Show("Role Created");
                    EditRole.instance.reload.Visible = true;
                    this.Close();
                    // Provide feedback to the user
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL-specific exceptions
                    MessageBox.Show("Role Cannot be create SQL Error");
                    Console.WriteLine("SQL Error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Role Cannot be created");
                    // Handle other types of exceptions (e.g., network issues, invalid input)
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Optionally log or execute any cleanup if necessary
                    Console.WriteLine("Insert operation completed.");
                }
            }

        }



    }
}

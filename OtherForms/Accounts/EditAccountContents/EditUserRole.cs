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
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.DataVisualization.Charting;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class EditUserRole : Form
    {
        public EditUserRole()
        {
            InitializeComponent();
            loadrole();
        }
        private void loadrole()
        {
            // Assuming ChangeIds.EditUserRole holds the name you're looking for
            string roleName = ChangeIds.EditUserRole.Trim();

            // Connection string to your database
            string connectionString = Connect.connectionString;

            // SQL query with a parameter placeholder
            string query = "SELECT * FROM UserRoles WHERE Name = @Name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    conn.Open();

                    // Create the SQL command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter with its value
                        cmd.Parameters.AddWithValue("@Name", roleName);

                        // Execute the command and get the data into a DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if any rows are returned
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // You can access columns by column name or index
                                    textBox1.Text = reader["Name"].ToString();
                                    if (reader["Tab1"].ToString().Trim() != "none" && reader["Tab1"].ToString().Trim() != "None") checkBox1.Checked = true;
                                    if (reader["Tab2"].ToString().Trim() != "none" && reader["Tab2"].ToString().Trim() != "None") checkBox2.Checked = true;
                                    if (reader["Tab3"].ToString().Trim() != "none" && reader["Tab3"].ToString().Trim() != "None") checkBox3.Checked = true;
                                    if (reader["Tab4"].ToString().Trim() != "none" && reader["Tab4"].ToString().Trim() != "None") checkBox4.Checked = true;
                                    if (reader["Tab5"].ToString().Trim() != "none" && reader["Tab5"].ToString().Trim() != "None") checkBox5.Checked = true;
                                    if (reader["Tab6"].ToString().Trim() != "none" && reader["Tab6"].ToString().Trim() != "None") checkBox6.Checked = true;
                                    if (reader["Tab7"].ToString().Trim() != "none" && reader["Tab7"].ToString().Trim() != "None") checkBox7.Checked = true;
                                    if (reader["Tab8"].ToString().Trim() != "none" && reader["Tab8"].ToString().Trim() != "None") checkBox8.Checked = true;
                                    if (reader["Tab9"].ToString().Trim() != "none" && reader["Tab9"].ToString().Trim() != "None") checkBox9.Checked = true;
                                    if (reader["Tab10"].ToString().Trim() != "none" && reader["Tab10"].ToString().Trim() != "None") checkBox10.Checked = true;
                                    if (reader["Tab11"].ToString().Trim() != "none" && reader["Tab11"].ToString().Trim() != "None") checkBox11.Checked = true;
                                    if (reader["Tab12"].ToString().Trim() != "none" && reader["Tab12"].ToString().Trim() != "None") checkBox12.Checked = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("No roles found with the specified name.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the query execution
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateRole()
        {
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

            string updateQuery = "UPDATE UserRoles SET Tab1 = @Tab1, Tab2 = @Tab2, Tab3 = @Tab3, Tab4 = @Tab4, Tab5 = @Tab5, Tab6 = @Tab6, Tab7 = @Tab7, Tab8 = @Tab8, Tab9 = @Tab9, Tab10 = @Tab10, Tab11 = @Tab11, Tab12 = @Tab12 WHERE Name = @Name";

            // Create a connection object
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                // Create the command object
                SqlCommand command = new SqlCommand(updateQuery, connection);

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

                    // Execute the UPDATE command
                    int rowsAffected = command.ExecuteNonQuery();

                    MessageBox.Show("Role Updated");
                    // Provide feedback to the user
                    Console.WriteLine($"{rowsAffected} row(s) updated.");
                    EditRole.instance.reload.Visible = true;
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL-specific exceptions
                    MessageBox.Show("Role Cannot be updated. SQL Error");
                    Console.WriteLine("SQL Error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Role Cannot be updated");
                    // Handle other types of exceptions (e.g., network issues, invalid input)
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Optionally log or execute any cleanup if necessary
                    Console.WriteLine("Update operation completed.");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateRole();
        }
    }
}

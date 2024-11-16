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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class UserRoleList : UserControl
    {
        public UserRoleList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string Name;

        [Category("ActivityList")]
        public string Role //ID
        {
            get { return Name; }
            set
            {
                Name = value; label4.Text = value;
            }
        }
        #endregion
        public void LoadRole()
        {
            // Connection string to your database
            string connectionString = Connect.connectionString;

            // The user ID you want to query
            string userId = ChangeIds.AccountID.Trim(); // Replace with the actual user ID

            // SQL query with a parameter placeholder
            string query = "SELECT Role FROM UserAccounts WHERE AccountID = @Id";

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
                        cmd.Parameters.AddWithValue("@Id", userId);

                        // Execute the command and get the data into a DataReader
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if any rows are returned
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Access the Role column
                                    string role = reader["Role"].ToString().Trim();

                                    if (role == Name)
                                    {
                                        button5.Visible = false;
                                        button6.Visible = false;
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("No user found with the specified ID.");
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
        private void UserRoleList_Load(object sender, EventArgs e)
        {
            LoadRole();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateRole();
        }
        public void UpdateRole()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from UserAccounts where AccountID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    if (UserInfo.Role == label4.Text)
                    {
                        MessageBox.Show("This is your current Role");
                        button5.Enabled = false;
                    }
                    else
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                            {


                                string updateQuery = "UPDATE UserAccounts SET Role = @input WHERE AccountID = @ID;";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                                {
                                    conn.Open();
                                    updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                                    updateCommand.Parameters.AddWithValue("@input", label4.Text.Trim());
                                    SqlDataReader reader = updateCommand.ExecuteReader();
                                    MessageBox.Show("Role Updated please logout");
                                    button5.Enabled = false;
                                    UserInfo.Role = label4.Text.Trim();
                                    EditRole.instance.reload.Visible = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error on putting updating the account: " + ex.Message);
                        }
                    }

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
            catch (Exception ex)
            {
                MessageBox.Show("Error on Deactivating account :" + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeIds.EditUserRole = label4.Text.Trim();
            EditUserRole frm = new EditUserRole();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this role?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string connectionString = Connect.connectionString;
                string deleteQuery = "DELETE FROM UserRoles WHERE Name = @Name";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", Name);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Role deleted successfully.");
                                EditRole.instance.reload.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("No role found with the specified name.");
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error occurred: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }    }
    }
}

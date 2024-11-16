using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.DisposalContents;
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
using static Flowershop_Thesis.MainForms.CustomAccount_BasePlatform;

namespace Flowershop_Thesis.MainForms
{
    public partial class CustomAccount_BasePlatform : Form
    {   public static CustomAccount_BasePlatform instance;
        public Panel MainPanel;
        public CustomAccount_BasePlatform()
        {
            InitializeComponent();
            instance = this;
            MainPanel = panel2;

        }
        // Modify the method to accept a 'name' parameter
        public static List<UserRole> GetUserRoles(string userRoleName)
        {
            var userRoles = new List<UserRole>();

            // SQL query to fetch data based on the given Name
            string query = "SELECT * FROM UserRoles WHERE Name = @Name"; // Use parameterized query

            // Establish a connection to the database
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                connection.Open();

                // Create a command with the query and the parameter
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter to the SQL query to prevent SQL injection
                    command.Parameters.AddWithValue("@Name", userRoleName);

                    // Execute the query and use a data reader to read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a list to store valid tabs
                            var validTabs = new List<string>();

                            // Get values from the reader
                            var tab1 = reader["Tab1"].ToString();
                            var tab2 = reader["Tab2"].ToString();
                            var tab3 = reader["Tab3"].ToString();
                            var tab4 = reader["Tab4"].ToString();
                            var tab5 = reader["Tab5"].ToString();
                            var tab6 = reader["Tab6"].ToString();
                            var tab7 = reader["Tab7"].ToString();
                            var tab8 = reader["Tab8"].ToString();
                            var tab9 = reader["Tab9"].ToString();
                            var tab10 = reader["Tab10"].ToString();
                            var tab11 = reader["Tab11"].ToString();
                            var tab12 = reader["Tab12"].ToString();

                            // Only add tabs that are not "None"
                            if (tab1 != "None" || tab1 != "none") validTabs.Add(tab1);
                            if (tab2 != "None" || tab2 != "none") validTabs.Add(tab2);
                            if (tab3 != "None" || tab3 != "none") validTabs.Add(tab3);
                            if (tab4 != "None" || tab4 != "none") validTabs.Add(tab4);
                            if (tab5 != "None" || tab5 != "none") validTabs.Add(tab5);
                            if (tab6 != "None" || tab6 != "none") validTabs.Add(tab6);
                            if (tab7 != "None" || tab7 != "none") validTabs.Add(tab7);
                            if (tab8 != "None" || tab8 != "none") validTabs.Add(tab8);
                            if (tab9 != "None" || tab9 != "none") validTabs.Add(tab9);
                            if (tab10 != "None" || tab10 != "none") validTabs.Add(tab10);
                            if (tab11 != "None" || tab11 != "none") validTabs.Add(tab11);
                            if (tab12 != "None" || tab12 != "none") validTabs.Add(tab12);

                            // If there are valid tabs, add the user role to the list
                            if (validTabs.Count > 0)
                            {
                                var userRole = new UserRole
                                {
                                    Name = reader["Name"].ToString(),
                                    Tabs = validTabs // Assign the list of valid tabs
                                };

                                // Add the user role to the list
                                userRoles.Add(userRole);
                            }
                        }
                    }
                }
            }

            return userRoles;
        }

        // UserRole class
        public class UserRole
        {
            public string Name { get; set; }
            public List<string> Tabs { get; set; } = new List<string>(); // Store tabs as a list
        }

        public void PopulateSidePanel(string roleName)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear(); // Clear any existing controls in the FlowLayoutPanel

                // Fetch the user roles (using GetUserRoles method)
                List<UserRole> userRoles = GetUserRoles(roleName); // Fetch the user roles for the specified role

                // Ensure that the userRoles list is not empty
                if (userRoles.Count > 0)
                {
                    // Assuming only one user role in the list
                    UserRole userRole = userRoles[0];

                    // Loop through each tab in the user role
                    foreach (var tab in userRole.Tabs)
                    {
                        if (!string.Equals(tab, "None", StringComparison.OrdinalIgnoreCase)) // Skip tabs that are "None"
                        {
                            // Create a new CustomRoleList for each tab
                            CustomRoleList roleItem = new CustomRoleList
                            {
                                formname = tab // Set the formname to the tab name
                            };

                            // Add the CustomRoleList to the FlowLayoutPanel
                            flowLayoutPanel1.Controls.Add(roleItem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No user roles found for the specified role name.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); // Handle any errors
            }




        }

        private void CustomAccount_BasePlatform_Load(object sender, EventArgs e)
        {

            EmpName.Text = UserInfo.Empleyado + ", " + UserInfo.Role;
            var userRoles = GetUserRoles(UserInfo.Role);
            PopulateSidePanel(UserInfo.Role);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

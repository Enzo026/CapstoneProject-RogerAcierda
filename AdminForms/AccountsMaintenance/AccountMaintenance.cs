using Flowershop_Thesis.OtherForms.Accounts;
using Flowershop_Thesis.OtherForms.Supplier;
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
using System.Xml.Linq;

namespace Capstone_Flowershop.AdminForms.AccountsMaintenance
{
    public partial class AccountMaintenance : Form
    {
        #region SQL Connection things?
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
     
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");

            // MessageBox.Show(databaseFilePath);
            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);
                    con2 = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        #endregion


        public AccountMaintenance()
        {
            InitializeComponent();
            testConnection();
            DisplayList();
            
        }
        public void DisplayList()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                con.Open();
                string countQuery = "SELECT COUNT(*) FROM UserAccounts where Status = 'Available' ";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    AccountsList[] inv = new AccountsList[rowCount];

                    string sqlQuery = "SELECT * FROM UserAccounts where Status = 'Available'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < inv.Length)
                            {
                                inv[index] = new AccountsList();
                                inv[index].AccID = reader["AccountID"].ToString().Trim();
                                inv[index].AccName = reader["FirstName"].ToString().Trim() + " " + reader["LastName"].ToString().Trim(); 
                                inv[index].AccUsername = reader["Username"].ToString().Trim();
                                inv[index].AccContact = reader["ContactNumber"].ToString().Trim();

                                inv[index].AccType = reader["Role"].ToString();

                                if (reader["AccountImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["AccountImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        inv[index].img = Image.FromStream(ms);
                                    }
                                }

                                flowLayoutPanel1.Controls.Add(inv[index]);
                                index++;
                            }
                        }
                    }

                }

                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Individual: " + ex.Message);
            }
        }
        public void AccInfo()
        {
            try
            {
                using (con)
                {
                    con.Open();
                    string sqlQuery = "SELECT FirstName ,  LastName,Username, ContactNumber, Role, Status, AccountImage FROM UserAccounts WHERE AccountID = @ID";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@ID", 2);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Make sure to call reader.Read() to advance to the first row
                            if (reader.Read())
                            {
                                label11.Text = reader["FirstName"].ToString().Trim() + " " + reader["LastName"].ToString().Trim();
                                label12.Text = reader["Username"].ToString();
                                label14.Text = reader["ContactNumber"].ToString();
                                label16.Text = reader["Role"].ToString();
                                label18.Text = reader["Status"].ToString();

                                // Check if AccountImage exists and is not null
                                if (reader["AccountImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["AccountImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        pictureBox2.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No data found for the given AccountID.");
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

        private void AccountMaintenance_Load(object sender, EventArgs e)
        {
            AccInfo();

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditAccount frm = new EditAccount();
            frm.ShowDialog();
        }
    }
}

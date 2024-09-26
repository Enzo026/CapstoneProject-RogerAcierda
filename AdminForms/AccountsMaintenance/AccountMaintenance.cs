using Flowershop_Thesis.OtherForms.Accounts;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Capstone_Flowershop.AdminForms.AccountsMaintenance
{
    public partial class AccountMaintenance : Form
    {
        #region SQL Connection things?
     //   SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        string connectionString;

        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));
            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

            // Build the connection string with explicit pooling parameters
             connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;Pooling=true;Max Pool Size=100;Min Pool Size=5;Connection Lifetime=600;";


            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                   // con = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions
                    MessageBox.Show("SQL error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            } // Connection is automatically closed and returned to the pool here
        }
        #endregion

        public static AccountMaintenance instance;
        public Label AccID;
        public Label AccName;
        public Label AccUsername;
        public Label AccNum;
        public Label AccRole;
        public Label AccStatus;
        public PictureBox AccImg;
        public Label DeactCounter;

        public bool isLoaded;

        public AccountMaintenance()
        {
            InitializeComponent();
            testConnection();
            LoadList();

            instance = this;
            AccID = label37;
            AccName = label11;
            AccUsername = label12;
            AccNum = label14;
            AccRole = label16;
            AccStatus = label18;
            AccImg = pictureBox2;
            DeactCounter = label2;


        }
        public void DisplayList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string countQuery = "SELECT COUNT(*) FROM UserAccounts where Status = 'Available' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        AccountsList[] inv = new AccountsList[rowCount];

                        string sqlQuery = "SELECT * FROM UserAccounts where Status = 'Available'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
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
                                    inv[index].AccRole = reader["Role"].ToString().Trim();
                                    inv[index].AccStatus = reader["Status"].ToString().Trim();
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

                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error AccountList: " + ex.Message);
            }
        }
        public void AccInfo()
        {
            try
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    
                    string sqlQuery = "SELECT AccountID,FirstName ,  LastName,Username, ContactNumber, Role, Status, AccountImage FROM UserAccounts WHERE AccountID = @ID";
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ID", UserInfo.EmpID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {   
                            if (reader.Read())
                            {
                                label37.Text = reader["AccountID"].ToString();
                                label11.Text = reader["FirstName"].ToString().Trim() + " " + reader["LastName"].ToString().Trim();
                                label12.Text = reader["Username"].ToString();
                                label14.Text = reader["ContactNumber"].ToString();
                                label16.Text = reader["Role"].ToString();
                                label18.Text = reader["Status"].ToString();

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
               MessageBox.Show("Error Account Info: " + ex.Message);
            }

        }
        public void DisplayDeactivated()
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string countQuery = "SELECT COUNT(*) FROM UserAccounts where Status = 'Deactivated' ";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        DeactivatedAccounts[] inv = new DeactivatedAccounts[rowCount];
                        label2.Text = rowCount.ToString();

                        string sqlQuery = "SELECT * FROM UserAccounts where Status = 'Deactivated'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new DeactivatedAccounts();
                                    inv[index].AccID = reader["AccountID"].ToString().Trim();
                                    inv[index].AccRole = reader["Role"].ToString().Trim();
                                    inv[index].AccName = reader["FirstName"].ToString().Trim() + " " + reader["LastName"].ToString().Trim();

                                    if (reader["AccountImage"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["AccountImage"];
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            inv[index].img = Image.FromStream(ms);
                                        }
                                    }

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
                MessageBox.Show("Error Deactivated: " + ex.Message);
            }
        }
        public void DeactivateAcc()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to Deactivate this Account?", "Deactivate Account Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        
                        string countQuery = "Select count(*) from UserAccounts where AccountID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                        {
                            conn.Open();
                            countCommand.Parameters.AddWithValue("@ID", label37.Text);
                            numId = (int)countCommand.ExecuteScalar();
                            

                        }
                    }

                 
                   
                  
                    if (numId == 1)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            
                            string updateQuery = "UPDATE UserAccounts SET Status = 'Deactivated' WHERE AccountID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", label37.Text);
                                conn.Open();
                                updateCommand.ExecuteNonQuery();


                            }
                        }

                        
                        MessageBox.Show("User Deactivated!");
                        //AccountMaintenance.instance.AccList.ControlRemoved();
                        label2.Text ="0";

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
                else
                {
                    //none
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Deactivating account :" + ex.Message);
            }
        }
        public void LoadList()
        {   
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            DisplayList();
            DisplayDeactivated();
        }
        private void AccountMaintenance_Load(object sender, EventArgs e)
        {
            AccInfo();
            isLoaded = true;
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
            ChangeIds.AccountID = label37.Text;
            frm.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DeactivateAcc();
        }

        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            if (isLoaded == true)
            {
                flowLayoutPanel2.Controls.Clear();
                LoadList();
            }
        }
    }
}

using Capstone_Flowershop.AdminForms.Reports.SalesReports;
using Capstone_Flowershop.MainForms;
using Flowershop_Thesis.MainForms;
using Flowershop_Thesis.Temporary_Forms;
using Flowershop_Thesis;
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
using System.IO;
using System.Text.Json;
using Microsoft.Win32.TaskScheduler;

namespace Capstone_Flowershop
{   

    public partial class Form1 : Form
    {
        SqlDataReader sdr;
        private  string configFilePath = "SystemConfig.json";
        private AppConfig appConfig;

        public Form1()
        {
            InitializeComponent();
            LoadConfig();

            
        }
   
        #region Commentedregion
        //#region olddb-ToBeRemoved
        //public void LocalDBConnection()
        //{
        //    string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));
        //    string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

        //    // Build the connection string with explicit pooling parameters
        //     //connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;Pooling=true;Max Pool Size=100;Min Pool Size=5;Connection Lifetime=600;";


        //    // Use the connection string to connect to the database
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            label6.Text = "Database Connected";
        //            label7.Text = databaseFilePath;
        //            con = new SqlConnection(connectionString);

        //            // Perform database operations here

        //        }
        //        catch (SqlException sqlEx)
        //        {
        //            // Handle SQL exceptions
        //            MessageBox.Show("SQL error occurred: " + sqlEx.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle other exceptions
        //            MessageBox.Show("An error occurred: " + ex.Message);
        //        }
        //    } // Connection is automatically closed and returned to the pool here
        //}

        //public void testConnections()
        //{
        //    string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));


        //    string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");



        //    // Build the connection string
        //    string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

        //    // Use the connection string to connect to the database
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            //MessageBox.Show("Database connection opened successfully.");
        //            label6.Text = "Database Connected";
        //            label7.Text = databaseFilePath;
        //            con = new SqlConnection(connectionString);
        //            //label6.Text = connectionString;
        //            // Perform database operations here

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("An error occurred: " + ex.Message);
        //        }
        //    }
        //}
        ////create a method name ewan to show a messagebox upon upon clicking button 1


        //public void oldconnection()
        //{
        //    string conn = "Data Source=DESKTOP-IH4V487\\NEWMSSQL;Initial Catalog=try;Integrated Security=True";
        //}
        //#endregion
        #endregion
        private void LoadConfig()
        {
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                appConfig = JsonSerializer.Deserialize<AppConfig>(json);
            }
            else
            {
                // Default values
                appConfig = new AppConfig
                {
                    IP = "192.168.8.205",
                    Port = "2626",
                    MainPC = true
                };
            }

            // Populate UI elements
            //textBox5.Text = appConfig.IP;
            //textBox6.Text = appConfig.Port;
            //checkBox1.Checked = appConfig.MainPC;
            //label8.Text = appConfig.CreateConnectionString();
        }
        public void CheckDB()
        {
            try
            {
                if (AppConfig.TestConnectionString(Connect.connectionString) == true)
                {
                    label7.Text = "Database Connected";
                }
                else
                {
                    label7.Text = "Cannot Connect to Database";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   
            using(SqlConnection conn = new SqlConnection(Connect.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT AccountID,Username,Password,Role,FirstName,LastName from UserAccounts where Username =@User  AND Status = 'Available'", conn);
                cmd.Parameters.AddWithValue("User", textBox1.Text.Trim());
                sdr = cmd.ExecuteReader();


                if (sdr.Read())
                {
                    string iAccID = sdr["AccountID"].ToString().Trim();
                    string iUserName = sdr["Username"].ToString().Trim();
                    string iPassword = sdr["Password"].ToString().Trim();
                    string Position = sdr["Role"].ToString().Trim();
                    string FirstName = sdr["FirstName"].ToString().Trim();
                    string LastName = sdr["LastName"].ToString().Trim();

                    if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "Admin")
                    {
                        Admin_BasePlatform admin = new Admin_BasePlatform();
                        //   admin.empName = FirstName;
                        UserInfo.Empleyado = FirstName;
                        UserInfo.FullName = FirstName + " " + LastName;
                        UserInfo.EmpID = iAccID;

                        this.Hide();
                        admin.Show();

                    }
                    else if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "SalesClerk")
                    {
                        SalesClerk_BasePlatform admin = new SalesClerk_BasePlatform();
                        //   admin.empName = FirstName;
                        UserInfo.Empleyado = FirstName;
                        UserInfo.FullName = FirstName + " " + LastName;
                        UserInfo.EmpID = iAccID;
                        this.Hide();
                        admin.Show();
                    }
                    else if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "InventoryClerk")
                    {
                        InventoryClerk_BasePlatform admin = new InventoryClerk_BasePlatform();
                        //     admin.empName = FirstName;
                        UserInfo.Empleyado = FirstName;
                        UserInfo.FullName = FirstName + " " + LastName;
                        UserInfo.EmpID = iAccID;
                        this.Hide();
                        admin.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid account");
                    }

                }
                else
                {
                    MessageBox.Show("No account Found");
                }
               
            }
            


        }
        public void DatabaseConnection()
        {
            try
            {
                if (AppConfig.TestConnectionString(appConfig.ConnectionString) == true)
                {
                    MessageBox.Show("Connected");
                }
                else
                {
                    MessageBox.Show("Cannot Connect");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            InvForm inventory = new InvForm();
            inventory.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TryCamera frm = new TryCamera();
            frm.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SystemSettings frm = new SystemSettings();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckDB();
        }
    }
}

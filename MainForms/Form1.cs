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

namespace Capstone_Flowershop
{   

    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public Form1()
        {
            InitializeComponent();
            testConnection();
           
        }

        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));
            
          
            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");

            label6.Text = databaseFilePath;

            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Database connection opened successfully.");
                    con = new SqlConnection(connectionString);
                    //label6.Text = connectionString;
                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT AccountID,Username,Password,Role from UserAccounts where Username =@User", con);
            cmd.Parameters.AddWithValue("User", textBox1.Text.Trim());
            sdr = cmd.ExecuteReader();


            if (sdr.Read())
            {
                string iAccID = sdr["AccountID"].ToString().Trim();
                string iUserName = sdr["Username"].ToString().Trim();
                string iPassword = sdr["Password"].ToString().Trim();
                string Position = sdr["Role"].ToString().Trim();



                if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "Admin")
                {
                    Admin_BasePlatform admin = new Admin_BasePlatform();
                    this.Hide();
                    admin.Show();

                    SalesReport.instance.uid.Text = iUserName + ", " + Position;
                    UserInfozz ui = new UserInfozz();

                    ui.Name = iUserName;
                    

                }
                else if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "SalesClerk")
                {
                    SalesClerk_BasePlatform admin = new SalesClerk_BasePlatform();
                    this.Hide();
                    admin.Show();
                }
                else if (textBox1.Text == iUserName && textBox2.Text == iPassword && Position == "InventoryClerk")
                {
                    InventoryClerk_BasePlatform admin = new InventoryClerk_BasePlatform();
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
            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            InvForm inventory = new InvForm();
            inventory.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddInventorie inventory = new AddInventorie();
            inventory.Show();
            this.Hide();
        }
    }
}

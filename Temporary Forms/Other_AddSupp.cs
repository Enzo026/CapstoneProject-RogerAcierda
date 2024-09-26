using Capstone_Flowershop;
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

namespace Flowershop_Thesis.Temporary_Forms
{
    public partial class Other_AddSupp : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public Other_AddSupp()
        {
            InitializeComponent();
            testConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image.Image = new Bitmap(open.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("MM-dd-yyyy");

            Image s_img = Image.Image;
            ImageConverter converter = new ImageConverter();
            try
            {
                var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                con.Open();
                cmd = new SqlCommand("INSERT INTO Supplier(SupplierName,ContactNumber,SupplierType,SupplierAddress,Status, Image)Values" +
                            "(@Name,@Number,@Type,@add,'Active',@Image);", con);
                cmd.Parameters.AddWithValue("@Name", SuppName.Text);
                cmd.Parameters.AddWithValue("@Number", ContactNum.Text);
                cmd.Parameters.AddWithValue("@Type", comboBox1.Text);
                cmd.Parameters.AddWithValue("@add", Address.Text);
                cmd.Parameters.AddWithValue("@Image", ImageConvert);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item Added Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("AddingItem Failed!" + " : " + ex);
            }

        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

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

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }
    }
}

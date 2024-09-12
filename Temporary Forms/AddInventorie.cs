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
    public partial class AddInventorie : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public AddInventorie()
        {
            InitializeComponent();
            testConnection();
        }
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

                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        public void addflower()
        {
            string date = DateTime.Now.ToString("MM-dd-yyyy");

            Image s_img = Image.Image;
            ImageConverter converter = new ImageConverter();
            try
            {
                var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                con.Open();
                cmd = new SqlCommand("INSERT INTO ItemInventory(ItemName,ItemQuantity,ItemType,ItemColor,LifeSpan,SuppliedDate,Supplier,ItemDescription,Price,ItemImage,ItemStatus)Values" +
                            "(@Name,@Qty,@Type,@Color,@LifeSpan,getdate(),@Supplier,@Desc,@RSP,@ItemImage,'Available');", con);
                cmd.Parameters.AddWithValue("@Name", Name.Text);
                cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(this.Qty.Text));
                cmd.Parameters.AddWithValue("@Type", Type.Text);
                cmd.Parameters.AddWithValue("@Color", Color.Text);
                cmd.Parameters.AddWithValue("@LifeSpan", Convert.ToInt32(this.UsageQty.Text));
                //  cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Supplier", Supplier.Text);
                  cmd.Parameters.AddWithValue("@Desc", UnitPrice.Text);
                cmd.Parameters.AddWithValue("@RSP", Convert.ToDecimal(this.Price.Text));
                cmd.Parameters.AddWithValue("@ItemImage", ImageConvert);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item Added Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("AddingItem Failed!" + " : " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addflower();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
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
    }
}

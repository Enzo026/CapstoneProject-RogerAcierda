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

namespace Flowershop_Thesis.AdminForms.AccountsMaintenance
{
    public partial class AddAccount : Form
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                PB1.Image = new Bitmap(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length <= 1 || textBox2.Text.Length <= 1 || textBox2.Text.Length <= 1 || textBox2.Text.Length <= 1  || textBox2.Text.Length <= 1 || textBox2.Text.Length <= 1 || !radioButton1.Checked || !radioButton2.Checked ) { MessageBox.Show("Please fill all the needed information"); }
            else
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    string date = DateTime.Now.ToString("MM-dd-yyyy");

                    Image s_img = PB1.Image;
                    ImageConverter converter = new ImageConverter();
                    string AccountRole = "0";
                    if (radioButton1.Checked)
                    {
                        AccountRole = "SalesClerk";
                    }
                    else if (radioButton2.Checked) { AccountRole = "InventoryClerk"; }
                    try
                    {
                        var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO UserAccounts(FirstName,LastName,Username,Password,ContactNumber,Role, Status, AccountImage)Values" +
                                    "(@Fname,@Lname,@Uname,@Pass,@CN,@Role,'Available',@Img);", con);

                        //varchar
                        cmd.Parameters.AddWithValue("@Fname", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Lname", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Uname", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Pass", textBox5.Text);
                        cmd.Parameters.AddWithValue("@CN", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Role", AccountRole);
                        cmd.Parameters.AddWithValue("@Img", ImageConvert);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item Added Successfully!");

                        this.Close();
                        // addActivityLog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("AddingItem Failed!" + " : " + ex.Message);
                    }
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

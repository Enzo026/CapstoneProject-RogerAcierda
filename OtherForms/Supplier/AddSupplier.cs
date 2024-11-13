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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class AddSupplier : Form
    {
        public AddSupplier()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checker();
            if(canProceed == true)
            {
                AddSupp();
            }
        }
        public void AddSupp()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
              

                Image s_img = Image.Image;
                ImageConverter converter = new ImageConverter();
                try
                {
                    var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Supplier(SupplierName,ContactNumber,SupplierType,SupplierAddress,Image,Status)Values" +
                                "(@Name,@Contact,@Type,@address,@Image,'Active');", con);
                    cmd.Parameters.AddWithValue("@Name", SuppNameTxtBox.Text);
                    cmd.Parameters.AddWithValue("@Contact", ContactNumTxtBox.Text);
                    cmd.Parameters.AddWithValue("@Type", SuppTypeInput.Text);
                    cmd.Parameters.AddWithValue("@address", AddressTxtBox.Text);
                    cmd.Parameters.AddWithValue("@Image", ImageConvert);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier Added Successfully!");
                    addActivityLog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Adding Supplier Failed!" + " : " + ex);
                }
            }

        }

        public void addActivityLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Added New Supplier");
                    cmd.Parameters.AddWithValue("@Definition", UserInfo.Empleyado + " Added a New Supplier for "+SuppTypeInput.Text+" to Supplier Tab named as " + SuppNameTxtBox.Text);
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", "0");
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Add New Item Flower to Inventory named as " + SuppNameTxtBox.Text);


                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }
        bool canProceed = false;
        public void checker()
        {
            if (SuppNameTxtBox.Text.Length <= 0 || ContactNumTxtBox.Text.Length <= 0 || SuppTypeInput.SelectedIndex == -1 || AddressTxtBox.Text.Length <= 0 || Image.Image == null)
            {
                MessageBox.Show("Please Fill all the needed information");
                canProceed = false;
            }
            else
            {
                canProceed = true;
            }
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

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

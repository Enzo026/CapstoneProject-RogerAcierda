using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.AccountsMaintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class EditUserInfo : Form
    {
        bool ChangePicture= false;
        Image oldimg;

        public EditUserInfo()
        {
            InitializeComponent();
            setup();
            DisplayInfo();
            
        }
        public void DisplayInfo()
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
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = " Select AccountImage from UserAccounts where AccountID = @ID";
                        //string updateQuery = "UPDATE UserAccounts SET Username = @input WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            SqlDataReader reader = updateCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                if (reader["AccountImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["AccountImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        Img.Image = Image.FromStream(ms);
                                        oldimg = Image.FromStream(ms);
                                    }
                                }
                            }
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
                MessageBox.Show("Error on retrieving account :" + ex.Message);
            }
        }

        public void ChangeFirstName()
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
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE UserAccounts SET FirstName = @In WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox1.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("FirstName Changed!");


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
                MessageBox.Show("Error on Changing FirstName account :" + ex.Message);
            }
        }

        public void ChangeLastName()
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
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE UserAccounts SET LastName = @In WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox2.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("LastName Changed!");


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
                MessageBox.Show("Error on Changing LastName account :" + ex.Message);
            }
        }

        public void ChangeContactNum()
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
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE UserAccounts SET ContactNumber = @In WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox3.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Contact Number Changed!");


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
                MessageBox.Show("Error on Changing ContactNumber account :" + ex.Message);
            }
        }

        public void ChangeImage()
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
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {
                        Image s_img = Img.Image;
                        ImageConverter converter = new ImageConverter();
                        var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                        string updateQuery = "UPDATE UserAccounts SET AccountImage = @In WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", ImageConvert);
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Contact Number Changed!");


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
                MessageBox.Show("Error on Changing ContactNumber account :" + ex.Message);
            }
        }
        public void setup()
        {
            checkBox1.Checked = false;
            checkBox2.Checked =false;
            checkBox3.Checked =false;
            checkBox4.Checked =false;

            textBox1.Text = string.Empty; textBox1.Enabled = false;
            textBox2.Text = string.Empty; textBox2.Enabled = false;
            textBox3.Text = string.Empty; textBox3.Enabled = false;

            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Img.Image = new Bitmap(open.FileName);
                ChangePicture = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && textBox1.Text.Length <= 0) { MessageBox.Show("Please fill up the FirstName to change or uncheck the checkbox if you do not intend to change"); }
            else if (checkBox2.Checked && textBox2.Text.Length <= 0) { MessageBox.Show("Please fill up the Last Name to change or uncheck the checkbox if you do not intend to change"); }
            else if (checkBox3.Checked && textBox3.Text.Length <= 0) { MessageBox.Show("Please fill up the Contact Number to change or uncheck the checkbox if you do not intend to change"); }
            else if (checkBox4.Checked && Img.Image == oldimg) {
                MessageBox.Show("Image is not changed please uncheck the Account Image Checkbox if you do not intend to change");
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && !checkBox4.Checked)
            {
                MessageBox.Show("Please click a checkbox to edit");
            }
            else {
                change();
                MessageBox.Show("Order Proceeded"); }
        }
        public void change()
        {
            try
            {
                if (checkBox1.Checked)
                {
                    ChangeFirstName();
                }
                if (checkBox2.Checked)
                {
                    ChangeLastName();
                }
                if (checkBox3.Checked)
                {
                    ChangeContactNum();
                }
                if (checkBox4.Checked)
                {
                    ChangeImage();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Unable to proceed change :"+ex.Message);
            }


        }
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                button3.Visible = true;
            }
            else
            {
                button3.Visible = false;
            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void Img_Click(object sender, EventArgs e)
        {

        }
    }
}

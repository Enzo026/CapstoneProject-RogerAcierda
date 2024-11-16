using Capstone_Flowershop;
using Capstone_Flowershop.MainForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class AccountInfo : Form
    {
        public bool IsCheckBoxChecked
        {
            get => checkBox2.Checked;
            set => checkBox2.Checked = value;
        }


        public AccountInfo()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            panel1.Visible = false;
            textBox5.Visible = false;
            label7.Visible = false;
        }
        string oldpass = "pass123";
       
        private void button1_Click(object sender, EventArgs e)
        {
            bool p1=false;
            if (checkBox1.Checked == false && checkBox2.Checked == false) MessageBox.Show("Please Checkbox to let the system know what to change");
            else if (checkBox1.Checked && textBox1.Text.Trim().Length == 0 ) MessageBox.Show("New Username textbox is not supplied please uncheck if you      dont intend to make changes on your username or fill up new username to change");
            else if(checkBox2.Checked && textBox2.Text.Trim().Length <= 0) MessageBox.Show("New Password textbox is not supplied please uncheck if you dont intend to make changes on your password or fill up new password to change");
            else if (checkBox2.Checked && textBox2.Text != textBox5.Text) MessageBox.Show("Password input incorrect please make sure repeat password is the same with the new password!");
  
            
            else
            {
                p1 = true;
                panel1.Visible=true;

            }
            if(p1 == true)
            {

                if (radioButton1.Checked == false && radioButton2.Checked == false) MessageBox.Show("Please Choose a confirmation type to proceed");
                else if (textBox3.Text == null || textBox3.Text.Length == 0 || textBox3.Text == " ") MessageBox.Show("Please fill the confirmation textbox with the needed information");
                else if (radioButton1.Checked == true && textBox3.Text == oldpass)
                {
                    MessageBox.Show("Accepted");
                    accountchanges();

                }
                else if (radioButton2.Checked == true && textBox3.Text == UserInfo.AdminCode)
                {
                    MessageBox.Show("Accepted");
                    accountchanges();
                }
                else
                {
                    MessageBox.Show("Incorrect Input");
                }
            }
            else
            {
             
            }
          
        }
        public void accountchanges()
        {
            if(checkBox1.Checked == false && checkBox2.Checked ==true)
            {
                //passchange
                ChangePass();
            }
            else if(checkBox1.Checked == true && checkBox2.Checked == false) 
            {
                //username
                ChangeUsername();
                
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                try
                {
                    ChangePass();
                    ChangeUsername();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }

        }
        public void ChangePass()
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

                        string updateQuery = "UPDATE UserAccounts SET Password = @Pass WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@Pass", textBox2.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("PasswordChanged!");
             

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
                MessageBox.Show("Error on Deactivating account :" + ex.Message);
            }
        }
        public void ChangeUsername()
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

                        string updateQuery = "UPDATE UserAccounts SET Username = @input WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            updateCommand.Parameters.AddWithValue("@input", textBox1.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Username Changed!");


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
                MessageBox.Show("Error on Deactivating account :" + ex.Message);
            }
        }

        public void reset()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox5.Text = string.Empty;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            panel1.Visible= false;


        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public string asd { get; set; }
        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.Enabled = true;
                label7.Visible = true;
                textBox5.Visible = true;
            }
            else
            {
                textBox2.Enabled = false;
                label7.Visible = false;
                textBox5.Visible = false;
                panel1.Visible = false;
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                panel1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void AccountInfo_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        public string TextBoxValue
        {
            get => textBox2.Text;
            set => textBox2.Text = value;
        }
        public void textBox2_TextChanged(object sender, EventArgs e)
        {
               
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

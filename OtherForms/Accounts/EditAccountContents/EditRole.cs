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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.OtherForms.Accounts.EditAccountContents
{
    public partial class EditRole : Form
    {
        string initRole;
        string SelectedRole;
        bool FrmLoad;
        public EditRole()
        {
            InitializeComponent();
        }

        private void EditRole_Load(object sender, EventArgs e)
        {
            LoadRole();
            FrmLoad = true;
        }
        public void LoadRole()
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
                       
                        string updateQuery = " Select Role from UserAccounts where AccountID = @ID";
                        //string updateQuery = "UPDATE UserAccounts SET Username = @input WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                            SqlDataReader reader = updateCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                initRole = reader["Role"].ToString().Trim();

                                if(initRole == "Admin")
                                {
                                    label3.Visible = true;
                                    radioButton1.Enabled = false;
                                    radioButton2.Enabled = false;
                                }
                                else if(initRole == "SalesClerk")
                                {
                                    radioButton1.Checked = true;
                                }
                                else if(initRole == "InventoryClerk")
                                {
                                    radioButton2.Checked = true;
                                }
                                else
                                {
                                    MessageBox.Show("The system does not find a condition for " + initRole);
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
                MessageBox.Show("Error on Deactivating account :" + ex.Message);
            }
        }
        public void UpdateRole()
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
                {   if(initRole == SelectedRole)
                    {
                        MessageBox.Show("This is your current Role");
                        button1.Enabled = false;
                    }
                    else
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                            {


                                string updateQuery = "UPDATE UserAccounts SET Role = @input WHERE AccountID = @ID;";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                                {
                                    conn.Open();
                                    updateCommand.Parameters.AddWithValue("@ID", ChangeIds.AccountID.Trim());
                                    updateCommand.Parameters.AddWithValue("@input", SelectedRole.Trim());
                                    SqlDataReader reader = updateCommand.ExecuteReader();
                                    MessageBox.Show("Role Updated");
                                    button1.Enabled = false;
                                    initRole = SelectedRole;
                                }
                            }
                        }catch (Exception ex)
                        {
                            MessageBox.Show("Error on putting updating the account: " + ex.Message);
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(FrmLoad == true)
            {
                if(radioButton1.Checked == true)
                {
                    SelectedRole = "SalesClerk";
                    button1.Enabled = true;
                }
                else if (radioButton2.Checked == true)
                {
                    SelectedRole = "InventoryClerk";
                    button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error on selecting Role");
                    button1.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateRole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadRole();
            button1.Enabled=false;
        }
    }
}

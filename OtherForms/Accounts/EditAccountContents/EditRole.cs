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
        public static EditRole instance;
        public Label reload;
        string initRole;
        string SelectedRole;
        bool FrmLoad;
        public EditRole()
        {
            InitializeComponent();
            instance = this;
            reload = label4;
            LoadRole();
        }

        private void EditRole_Load(object sender, EventArgs e)
        {
           // LoadRole();
            FrmLoad = true;
        }
        public void LoadRole()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from UserRoles where Name != 'Admin';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();


                        // counter.Text = rowCount.ToString();



                        UserRoleList[] inv = new UserRoleList[rowCount];

                        string sqlQuery = "select Name from UserRoles where Name != 'Admin';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new UserRoleList();
                                    inv[index].Role = reader["Name"].ToString();
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

               MessageBox.Show("Error on CartLsit() : " + ex.Message);
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


        private void button1_Click(object sender, EventArgs e)
        {
            UpdateRole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadRole();
            button1.Enabled=false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddUserRole frm = new AddUserRole();
            frm.ShowDialog();
        }

        private void label4_VisibleChanged(object sender, EventArgs e)
        {
            if (label4.Visible)
            {
                LoadRole();
                label4.Visible = false;
            }
        }
    }
}

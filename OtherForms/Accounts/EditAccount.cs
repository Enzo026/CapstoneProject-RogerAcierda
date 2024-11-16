using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.System_Maintenance;
using Flowershop_Thesis.OtherForms.Accounts.EditAccountContents;
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

namespace Flowershop_Thesis.OtherForms.Accounts
{
    public partial class EditAccount : Form
    {

        public EditAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditUserInfo frm = new EditUserInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditRole frm = new EditRole();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AccountInfo frm = new AccountInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void EditAccount_Load(object sender, EventArgs e)
        {
            label3.Text = ChangeIds.AccountID;
            panel2.Controls.Clear();
            EditUserInfo frm = new EditUserInfo();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        public void deact()
        {
          if(UserInfo.EmpID == ChangeIds.AccountID)
            {
                try
                {
                    DialogResult result = MessageBox.Show("You are about Deactivate your Account?", "Deactivate Account", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int numId;
                        using(SqlConnection con = new SqlConnection(Connect.connectionString))
                        {
                            string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                            using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                            {
                                con.Open();
                                countCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);
                                numId = (int)countCommand.ExecuteScalar();
                                con.Close();
                            }
                            string updateQuery = "UPDATE Supplier SET Status = 'Inactive' WHERE SupplierID = @ID;";
                            if (numId == 1)
                            {
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                                {
                                    con.Open();
                                    updateCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);

                                    updateCommand.ExecuteNonQuery();
                                    con.Close();
                                }

                                MessageBox.Show("User Deactivated!");
                                Form1 frm = new Form1();
                                frm.Show();
                                this.Close();

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
                    }
                    else
                    {
                        //none
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                try
                {
                    DialogResult result = MessageBox.Show("You are about Deactivate this user?", "Deactivate", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int numId;
                        using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                        {
                            string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                            using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                            {
                                con.Open();
                                countCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);
                                numId = (int)countCommand.ExecuteScalar();
                                con.Close();
                            }
                            string updateQuery = "UPDATE Supplier SET Status = 'Inactive' WHERE SupplierID = @ID;";
                            if (numId == 1)
                            {
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                                {
                                    con.Open();
                                    updateCommand.Parameters.AddWithValue("@ID", ChangeIds.SupplierId);

                                    updateCommand.ExecuteNonQuery();
                                    con.Close();
                                }

                                MessageBox.Show("User Deactivated!");

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


                    }
                    else
                    {
                        //none
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
               
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

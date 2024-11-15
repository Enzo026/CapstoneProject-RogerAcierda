using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.AccountsMaintenance;
using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
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
    public partial class DeactivatedAccounts : UserControl
    {

        public DeactivatedAccounts()
        {
            InitializeComponent();
  
        }

        #region Myregion
        private string AccountID;
        private string AccountName;
        private string AccountRole;
        private Image AccountImage;

        [Category("ItemList")]
        public string AccID
        {
            get { return AccountID; }
            set { AccountID = value; }
        }
        [Category("ItemList")]
        public string AccRole
        {
            get { return AccountRole; }
            set { AccountRole = value; Name.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string AccName
        {
            get { return AccountName; }
            set { AccountName = value; Role.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return AccountImage; }
            set { AccountImage = value; pictureBox1.Image = value; }
        }
        #endregion

        public void activateAcc()
        {
            try
            {
                DialogResult result = MessageBox.Show("You are about to Activate this Account?", "Activate Account Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int numId;
                    using(SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        string countQuery = "Select count(*) from UserAccounts where AccountID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            countCommand.Parameters.AddWithValue("@ID", AccountID);
                            numId = (int)countCommand.ExecuteScalar();

                        }
                        string updateQuery = "UPDATE UserAccounts SET Status = 'Available' WHERE AccountID = @ID;";
                        if (numId == 1)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {

                                updateCommand.Parameters.AddWithValue("@ID", AccountID);

                                updateCommand.ExecuteNonQuery();


                            }

                            MessageBox.Show("User Activated!");
                            //AccountMaintenance.instance.AccList.ControlRemoved();
                            AccountMaintenance.instance.refresh.Visible = true;

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

        private void button1_Click(object sender, EventArgs e)
        {
            activateAcc();
            
        }
    }
}

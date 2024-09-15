using Flowershop_Thesis.OtherForms;
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

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class AdvanceOrderFrm : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;

        public static AdvanceOrderFrm instance; //this
        public System.Windows.Forms.Button cartbtn; //this
        public AdvanceOrderFrm()
        {
            InitializeComponent();
            testConnection();
            ShowInd();
            instance = this; //this
            cartbtn = button1; //this
            cartNo();
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



        public void btnTxtChange(int items)
        {

            if (items > 0)
            {
                if (items > 1999)
                {
                    button1.Text = " " + items.ToString();
                }
                else
                {
                    button1.Text = "  " + items.ToString();
                }
            }




        }

        public void ShowInd()
        {
            panel2.Controls.Clear();
            IndividualFrm frm = new IndividualFrm();
            frm.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(frm); //ilalagay na natin yung form
            frm.BringToFront(); //front yung form 
            frm.Show();
        }



        private void button1_TextChanged_1(object sender, EventArgs e)
        {
            int btntext = int.Parse(button1.Text.Trim());
            if (btntext > 0)
            {
                button1.Image = Properties.Resources.b2_24x24;
                button1.ForeColor = Color.SlateBlue;
            }
            else
            {
                button1.Image = Properties.Resources.b1_24x24;
                button1.ForeColor = Color.DimGray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ShowInd();
        }
        public void cartNo()
        {
            try
            {
                con.Open();

                string countQuery = "SELECT COUNT(*) FROM Advance_ServingCart";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    int rowCount = (int)countCommand.ExecuteScalar();
                    if (rowCount > 0)
                    {
                        if (rowCount > 1999)
                        {
                            button1.Text = " " + rowCount.ToString();
                        }
                        else
                        {
                            button1.Text = "  " + rowCount.ToString();
                        }
                    }


                   
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if(button1.Text.Trim() != "0") {
                AdvanceOrderCart cart = new AdvanceOrderCart();
                cart.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("No items In Cart");
            }
         
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            PreBuilt frm = new PreBuilt();
            frm.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(frm); //ilalagay na natin yung form
            frm.BringToFront(); //front yung form 
            frm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Adv_Custom frm = new Adv_Custom();
            frm.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(frm); //ilalagay na natin yung form
            frm.BringToFront(); //front yung form 
            frm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AdvanceOrdersList form = new AdvanceOrdersList();
            form.Show();
        }
    }
}

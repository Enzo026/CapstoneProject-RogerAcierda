﻿using Capstone_Flowershop;
using Flowershop_Thesis.AdminForms.History_Logs;
using Flowershop_Thesis.SalesClerk.PriceList;
using Flowershop_Thesis.SalesClerk.Transaction;
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

namespace Flowershop_Thesis.MainForms
{
    public partial class SalesClerk_BasePlatform : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IH4V487\\SQLEXPRESS;Initial Catalog=CapstoneProject;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public SalesClerk_BasePlatform()
        {
            InitializeComponent();
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            TransactionForm TF = new TransactionForm(); //tatawagin tapos papangalanan yung form na papalabasin
            TF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(TF); //ilalagay na natin yung form
            TF.BringToFront(); //front yung form 
            TF.Show(); //para lumitaw
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("TRUNCATE TABLE ServingCart",con);
                cmd.ExecuteNonQuery();
                con.Close();    
                
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Cart " + ex.Message);
            }
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 logout = new Form1();
            this.Hide();
            logout.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            TransactionForm TF = new TransactionForm(); //tatawagin tapos papangalanan yung form na papalabasin
            TF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(TF); //ilalagay na natin yung form
            TF.BringToFront(); //front yung form 
            TF.Show(); //para lumitaw
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            PriceList PL = new PriceList(); //tatawagin tapos papangalanan yung form na papalabasin
            PL.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(PL); //ilalagay na natin yung form
            PL.BringToFront(); //front yung form 
            PL.Show(); //para lumitaw
        }
    }
}
using Capstone_Flowershop.AdminForms.AccountsMaintenance;
using Capstone_Flowershop.AdminForms.History_Logs;
using Capstone_Flowershop.AdminForms.ProductMaintenance;
using Capstone_Flowershop.AdminForms.Reports.SalesReports;
using Capstone_Flowershop.AdminForms.System_Maintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Flowershop.MainForms
{
    public partial class Admin_BasePlatform : Form
    {
        public Admin_BasePlatform()
        {
            InitializeComponent();
           
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ProductMaintenance SR = new ProductMaintenance();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            panel2.Controls.Clear();
            Reports SR = new Reports();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 logout = new Form1();
            this.Hide();
            logout.Show();

        }

        private void Admin_BasePlatform_Load(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Reports SR = new Reports();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
            EmpName.Text = UserInfo.Empleyado;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            SystemMaintenance SR = new SystemMaintenance();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AccountMaintenance SR = new AccountMaintenance();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            HistoryLogs SR = new HistoryLogs();
            SR.TopLevel = false;
            panel2.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }
    }
}

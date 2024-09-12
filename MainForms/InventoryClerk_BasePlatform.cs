using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.LandingPage;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.SalesClerk.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Flowershop_Thesis.MainForms
{
    public partial class InventoryClerk_BasePlatform : Form
    {
        public InventoryClerk_BasePlatform()
        {
            InitializeComponent();

        }
        private string EmployeeName;
        public string empName
        {
            get { return EmployeeName; }
            set
            {
                EmployeeName = value; EmpName.Text = value;
                MessageBox.Show(value);
            }

        }
 

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            DashboardFrm DF = new DashboardFrm(); //tatawagin tapos papangalanan yung form na papalabasin
            DF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(DF); //ilalagay na natin yung form
            DF.BringToFront(); //front yung form 
            DF.Show(); //para lumitaw
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            RestockingForm RF = new RestockingForm(); //tatawagin tapos papangalanan yung form na papalabasin
            RF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(RF); //ilalagay na natin yung form
            RF.BringToFront(); //front yung form 
            RF.Show(); //para lumitaw
        }
    }
}

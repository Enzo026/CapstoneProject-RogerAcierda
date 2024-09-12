using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Disposal;
using Flowershop_Thesis.InventoryClerk.LandingPage;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.InventoryClerk.StockAdjustment;
using Flowershop_Thesis.InventoryClerk.Supplier;
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
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            DashboardFrm DF = new DashboardFrm(); //tatawagin tapos papangalanan yung form na papalabasin
            DF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(DF); //ilalagay na natin yung form
            DF.BringToFront(); //front yung form 
            DF.Show(); //para lumitaw

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

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            StockAdjustmentFrmcs SAF = new StockAdjustmentFrmcs(); //tatawagin tapos papangalanan yung form na papalabasin
            SAF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(SAF); //ilalagay na natin yung form
            SAF.BringToFront(); //front yung form 
            SAF.Show(); //para lumitaw

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 logout = new Form1();
            this.Hide();
            logout.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            SupplierFrm SF = new SupplierFrm(); //tatawagin tapos papangalanan yung form na papalabasin
            SF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(SF); //ilalagay na natin yung form
            SF.BringToFront(); //front yung form 
            SF.Show(); //para lumitaw
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisposalFrm DF = new DisposalFrm(); //tatawagin tapos papangalanan yung form na papalabasin
            DF.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(DF); //ilalagay na natin yung form
            DF.BringToFront(); //front yung form 
            DF.Show(); //para lumitaw
        }
    }
}

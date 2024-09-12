using Flowershop_Thesis.SalesClerk.Order_Placement;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
using Flowershop_Thesis.SalesClerk.Queueing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.SalesClerk.Transaction
{
    public partial class TransactionForm : Form
    {
        public TransactionForm()
        {
            InitializeComponent();
            panel1.Controls.Clear(); //tatanggalin yung current na laman ng panel
            OrderPlacement OP = new OrderPlacement(); //tatawagin tapos papangalanan yung form na papalabasin
            OP.TopLevel = false; //para di mag agaw ng place
            panel1.Controls.Add(OP); //ilalagay na natin yung form
            OP.BringToFront(); //front yung form 
            OP.Show(); //para lumitaw

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear(); //tatanggalin yung current na laman ng panel
            OrderPlacement OP = new OrderPlacement(); //tatawagin tapos papangalanan yung form na papalabasin
            OP.TopLevel = false; //para di mag agaw ng place
            panel1.Controls.Add(OP); //ilalagay na natin yung form
            OP.BringToFront(); //front yung form 
            OP.Show(); //para lumitaw
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear(); //tatanggalin yung current na laman ng panel
            QueuingFormBack QF = new QueuingFormBack(); //tatawagin tapos papangalanan yung form na papalabasin
            QF.TopLevel = false; //para di mag agaw ng place
            panel1.Controls.Add(QF); //ilalagay na natin yung form
            QF.BringToFront(); //front yung form 
            QF.Show(); //para lumitaw
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear(); //tatanggalin yung current na laman ng panel
            AdvanceOrderFrm AO = new AdvanceOrderFrm();
            AO.TopLevel = false; //para di mag agaw ng place
            panel1.Controls.Add(AO); //ilalagay na natin yung form
            AO.BringToFront(); //front yung form 
            AO.Show(); //para lumitaw
        }
    }
}

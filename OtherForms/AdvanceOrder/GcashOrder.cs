using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    public partial class GcashOrderFrm : Form
    {
        public GcashOrderFrm()
        {
            InitializeComponent();
            setup();
        }
        public void setup()
        {
            pictureBox1.Image = CreateAdvanceOrder.ProofOfPayment;
            Name.Text = CreateAdvanceOrder.CustomerName;
            TotalAmount.Text = CreateAdvanceOrder.TotalAmount;
            MOP.Text = CreateAdvanceOrder.ModeOfPayment;
            Date.Text = CreateAdvanceOrder.Date;
            DP.Text = CreateAdvanceOrder.Downpayment;
            PUD.Text = CreateAdvanceOrder.PickUpDate;
            Contact.Text = CreateAdvanceOrder.ContactNumber;
            Employee.Text = UserInfo.Empleyado;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvanceOrderFrm.instance.cartbtn.Text = "0";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wala pang resibo tanga! HAHAHAHA");
            AdvanceOrderFrm.instance.cartbtn.Text = "0";
            this.Close();
        }
        public void changecartvalue()
        {
            AdvanceOrderFrm.instance.cartbtn.Text = "0";
        }
    }
}

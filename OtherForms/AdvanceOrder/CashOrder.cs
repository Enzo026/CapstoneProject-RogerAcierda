using Capstone_Flowershop;
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
    public partial class CashOrder : Form
    {
        public CashOrder()
        {
            InitializeComponent();
            filldata();
        }

        public void filldata()
        {
            CustomerName.Text = CreateAdvanceOrder.CustomerName;
            TotalAmount.Text = CreateAdvanceOrder.TotalAmount;
            MOP.Text = CreateAdvanceOrder.ModeOfPayment;
            Date.Text = CreateAdvanceOrder.Date;
            DP.Text = CreateAdvanceOrder.Downpayment;
            OrderType.Text = CreateAdvanceOrder.OrderType;
            PUD.Text = CreateAdvanceOrder.PickUpDate;
            Contact.Text = CreateAdvanceOrder.ContactNumber;
            EmpName.Text = UserInfo.Empleyado;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

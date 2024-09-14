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
    public partial class OrderPlace : Form
    {
        public OrderPlace()
        {
            InitializeComponent();
            setup();
        }
        public void setup()
        {
            pictureBox1.Image = CreateAdvanceOrder.ProofOfPayment;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

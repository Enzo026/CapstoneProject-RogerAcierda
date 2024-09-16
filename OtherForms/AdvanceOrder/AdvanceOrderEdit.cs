using Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems;
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
    public partial class AdvanceOrderEdit : Form
    {
        public AdvanceOrderEdit()
        {
            InitializeComponent();
            panel2.Controls.Clear();
            NormalBackground form = new NormalBackground();
            panel2.Controls.Add(form);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditContactNumber editContactNumber = new EditContactNumber();
           panel2.Controls.Add(editContactNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditItemOrders form = new EditItemOrders();
            panel2.Controls.Add(form);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EditPickupDate form = new EditPickupDate();
            panel2.Controls.Add(form);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            SeeItems form = new SeeItems();
            panel2.Controls.Add(form);
        }
    }
}

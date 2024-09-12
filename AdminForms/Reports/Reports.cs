
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Flowershop.AdminForms.Reports.SalesReports
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            SalesReport SR = new SalesReport();
            SR.TopLevel = false;
            panel1.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            panel1.Controls.Clear();
            InventoryReport IR = new InventoryReport();
            IR.TopLevel = false;
            panel1.Controls.Add(IR);
            IR.BringToFront();
            IR.Show();

            button1.BackColor =  Color.White;
            button1.ForeColor =  Color.Black;

            button2.BackColor = Color.SlateBlue;
            button2.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            SalesReport SR = new SalesReport();
            SR.TopLevel = false;
            panel1.Controls.Add(SR);
            SR.BringToFront();
            SR.Show();

            button1.BackColor = Color.SlateBlue;
            button1.ForeColor = Color.White;

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
        }
    }
}

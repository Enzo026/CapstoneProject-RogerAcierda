using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capstone_Flowershop;

namespace Capstone_Flowershop.AdminForms.Reports.SalesReports
{   
    public partial class SalesReport : Form
    {
        public static SalesReport instance;
        public Label uid;
        public SalesReport()
        {
   

            InitializeComponent();
            instance = this;
            uid = label2;


            DateTime date = DateTime.Now;

            string datenow = DateTime.Now.Date.ToString();
            label3.Text = date.ToString();
  
           // label2.Text = "" + ", " + ui.role;
            
            
           label2.Text = UserInfo.FullName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string al = label2.Text;
            MessageBox.Show(al);
        }
    }
}

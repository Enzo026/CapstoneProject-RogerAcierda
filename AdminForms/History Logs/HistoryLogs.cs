using Flowershop_Thesis.AdminForms.History_Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Flowershop.AdminForms.History_Logs
{
    public partial class HistoryLogs : Form
    {
        public HistoryLogs()
        {
            InitializeComponent();

            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            Transaction_History SR = new Transaction_History(); //tatawagin tapos papangalanan yung form na papalabasin
            SR.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(SR); //ilalagay na natin yung form
            SR.BringToFront(); //front yung form 
            SR.Show(); //para lumitaw
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            Activity_Logs SR = new Activity_Logs(); //tatawagin tapos papangalanan yung form na papalabasin
            SR.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(SR); //ilalagay na natin yung form
            SR.BringToFront(); //front yung form 
            SR.Show(); //para lumitaw
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel2.Controls.Clear(); //tatanggalin yung current na laman ng panel
            Transaction_History SR = new Transaction_History(); //tatawagin tapos papangalanan yung form na papalabasin
            SR.TopLevel = false; //para di mag agaw ng place
            panel2.Controls.Add(SR); //ilalagay na natin yung form
            SR.BringToFront(); //front yung form 
            SR.Show(); //para lumitaw
        }
    }
}

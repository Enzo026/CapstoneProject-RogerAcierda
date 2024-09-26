using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdminDashboard
{
    public partial class ReportTabContents : Form
    {
        public ReportTabContents()
        {
            InitializeComponent();
        }

        private void ReportTabContents_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

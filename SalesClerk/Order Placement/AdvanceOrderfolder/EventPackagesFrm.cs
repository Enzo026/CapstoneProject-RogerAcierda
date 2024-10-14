using Flowershop_Thesis.OtherForms.AdvanceOrder.EventPackagesModalsAndList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class EventPackagesFrm : Form
    {
        public EventPackagesFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventPackagesModal form = new EventPackagesModal();
            form.ShowDialog();
        }
    }
}

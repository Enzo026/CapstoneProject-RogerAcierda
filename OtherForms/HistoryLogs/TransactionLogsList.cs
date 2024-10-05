using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.HistoryLogs
{
    public partial class TransactionLogsList : UserControl
    {
        public TransactionLogsList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string Id, CustName, Price, EmpName, Date;

        [Category("ActivityList")]
        public string id //ID
        {
            get { return Id; }
            set { Id = value; label16.Text = value.ToString(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeIds.TransactionLogID = Id;
            LogInformation frm = new LogInformation();
            frm.ShowDialog();
        }

        [Category("ActivityList")]
        public string customerName //Customer Name
        {
            get { return CustName; }
            set { CustName = value; label15.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string price // Price
        {
            get { return Price; }
            set { Price = value; label14.Text = value.ToString(); }
        }
        [Category("ActivityList")] 
        public string date
        {
            get { return Date; }
            set { Date = value; label13.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string name
        {
            get { return EmpName; }
            set { EmpName = value; label4.Text = value.ToString(); }
        }


        #endregion
    }
}

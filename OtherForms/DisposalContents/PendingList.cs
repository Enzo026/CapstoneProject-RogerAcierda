using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.HistoryLogs;
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

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class PendingList : UserControl
    {
        public PendingList()
        {
            InitializeComponent();
        }

        #region FinishedQueue
        private string Id, CustName, Price, Status, Date,OrderType;

        [Category("ActivityList")]
        public string id //ID
        {
            get { return Id; }
            set { Id = value; IdLbl.Text = value.ToString(); }
        }


        private void EvaluateBtn_Click_1(object sender, EventArgs e)
        {
            DisposalInfo.ID = id;
            DisposalInfo.OrderType = OrderType;
            DisposalInfo.OrderStatus = Status;
            DisposalItems frm = new DisposalItems();
            frm.ShowDialog();
        }

        private void PendingList_Load(object sender, EventArgs e)
        {
            if (Status == "Evaluated")
            {
                StatusLbl.ForeColor = Color.Green;
                DisposalInfo.OrderStatus = Status;
                EvaluateBtn.Text = "See Info...";

            }
        }

        [Category("ActivityList")]
        public string customerName //Customer Name
        {
            get { return CustName; }
            set { CustName = value; NameLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string price // Price
        {
            get { return Price; }
            set { Price = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string date
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string status
        {
            get { return Status; }
            set { Status = value; StatusLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string type
        {
            get { return OrderType; }
            set { OrderType = value; TypeLbl.Text = value.ToString(); }
        }

        #endregion
    }
}

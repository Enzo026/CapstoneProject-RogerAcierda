using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Reports
{
    public partial class SalesTransactionListItems : UserControl
    {
        public SalesTransactionListItems()
        {
            InitializeComponent();
        }
        #region Myregion
        private string ItemID;
        private string ID;
        private string CustomerName;
        private string EmployeeName;
        private string TotalPrice;
        private string OrderType;
        private string DOC;

        [Category("ItemList")]
        public string TransID
        {
            get { return ItemID; }
            set { ItemID = value;  }
        }
        [Category("ItemList")]
        public string LocalID
        {
            get { return ID; }
            set { ID = value; IDLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string CustName
        {
            get { return CustomerName; }
            set { CustomerName = value; CustNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Price
        {
            get { return TotalPrice; }
            set { TotalPrice = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Employee
        {
            get { return EmployeeName; }
            set { EmployeeName = value; EmpLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Type
        {
            get { return OrderType; }
            set { OrderType = value; TypeLbl.Text = value.ToString(); }
        }
        public string Date
        {
            get { return DOC; }
            set { DOC = value; DateLbl.Text = value.ToString(); }
        }
        #endregion

        private void DetailsBtn_Click(object sender, EventArgs e)
        {
            ViewInfo.ID = TransID;
            ViewInfo.type = OrderType;


            OrderInfoFrm frm = new OrderInfoFrm();
            frm.ShowDialog();
        }
    }
}

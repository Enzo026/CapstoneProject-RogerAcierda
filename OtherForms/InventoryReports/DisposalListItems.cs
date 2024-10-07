using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.InventoryReports
{
    public partial class DisposalListItems : UserControl
    {
        public DisposalListItems()
        {
            InitializeComponent();
        }

        #region Myregion
        private string ItemID;
        private string ID;
        private string CustomerName;
        private string EmployeeName;
        private string TotalQuantity;
        private string Status;
        private string Date;

        [Category("ItemList")]
        public string TransID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }
        [Category("ItemList")]
        public string LocalID
        {
            get { return ID; }
            set { ID = value; IdLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string CustName
        {
            get { return CustomerName; }
            set { CustomerName = value; CustNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Qty
        {
            get { return TotalQuantity; }
            set { TotalQuantity = value; ItemsLbl.Text = value.ToString() + " Php"; }
        }
        [Category("ItemList")]
        public string Employee
        {
            get { return EmployeeName; }
            set { EmployeeName = value; EmpLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string status
        {
            get { return Status; }
            set { Status = value; StatusLbl.Text = value.ToString(); }
        }
        public string date
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value.ToString(); }
        }
        #endregion
    }
}

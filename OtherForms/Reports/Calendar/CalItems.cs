using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Reports.Calendar
{
    public partial class CalItems : UserControl
    {
        public CalItems()
        {
            InitializeComponent();
        }
        #region Myregion
        private string ID;
        private string CustomerName;
        private string TotalPrice;


        [Category("ItemList")]
        public string LocalID
        {
            get { return ID; }
            set { ID = value; }
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
            set { TotalPrice = value; PriceLbl.Text = value.ToString() + " Php"; }
        }

        #endregion

        private void button24_Click(object sender, EventArgs e)
        {
            ChangeIds.TransactionLogID = ID;
            AdvanceOrderListItems form = new AdvanceOrderListItems();
            form.ShowDialog();
        }
    }
}

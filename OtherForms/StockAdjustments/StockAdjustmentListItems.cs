using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.StockAdjustment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    public partial class StockAdjustmentListItems : UserControl
    {
        public StockAdjustmentListItems()
        {
            InitializeComponent();
        }

        #region Myregion
        private string BatchID,Qty,Date;

        [Category("ItemList")]
        public string ID
        {
            get { return BatchID; }
            set { BatchID = value; BatchIDLbl.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public string qty
        {
            get { return Qty; }
            set { Qty = value; QtyLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string date
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value.ToString(); }
        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {   
            SA_Info.BatchID = BatchID;
            SA_BatchItems frm = new SA_BatchItems();
            frm.ShowDialog();
        }
    }
}

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

namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    public partial class BatchListItems : UserControl
    {
        public BatchListItems()
        {
            InitializeComponent();
        }

        #region Myregion
        private string ItemID, ItemName, Quantity, BatchID, RestockingID, Type;

        [Category("ItemList")]
        public string ID
        {
            get { return BatchID; }
            set { BatchID = value; }
        }
        [Category("ItemList")]
        public string type
        {
            get { return Type; }
            set { Type = value; }
        }
        [Category("ItemList")]
        public string RID
        {
            get { return RestockingID; }
            set { RestockingID = value; }
        }
        [Category("ItemList")]
        public string Name
        {
            get { return ItemName; }
            set { ItemName = value; ItemNameLbl.Text = value; }
        }



        [Category("ItemList")]
        public string ItmID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }

        [Category("ItemList")]
        public string qty
        {
            get { return Quantity; }
            set { Quantity = value; QtyLbl.Text = value.ToString(); }
        }

        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {   
            SA_Info.Type = Type;
            SA_Info.RestockingID = RestockingID;
            SA_Info.ItemID = ItemID;
            SA_AdjustQuantity frm = new SA_AdjustQuantity();
            frm.ShowDialog();

        }
    }
}

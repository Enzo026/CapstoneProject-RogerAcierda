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
        private int itemId;
        private string itemName;
        private int itemQuantity;

        [Category("ItemList")]
        public int itemidData
        {
            get { return itemId; }
            set { itemId = value; ItmIDLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string itemnameData
        {
            get { return itemName; }
            set { itemName = value; ItmNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public int itemquantityData
        {
            get { return itemQuantity; }
            set { itemQuantity = value; ItmQtyLbl.Text = value.ToString(); }
        }

        #endregion

        private void AdjustBtn_Click(object sender, EventArgs e)
        {
            StockAdjustmentFrmcs.Instance.Idhandler.Text = itemId.ToString();
            StockAdjustmentFrmcs.Instance.ItmName.Text = itemName.ToString();
            StockAdjustmentFrmcs.Instance.currentqty.Text = itemQuantity.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class SeeItemsListItems : UserControl
    {
        public SeeItemsListItems()
        {
            InitializeComponent();
        }

        #region AdvanceOrderListItems
        private decimal ItemPrice;
        private string name;
        private string qty;



        [Category("ListItems")]
        public decimal Price
        {
            get { return ItemPrice; }
            set { ItemPrice = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ListItems")]
        public string Name
        {
            get { return name; }
            set { name = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ListItems")]
        public string OrderQuantity
        {
            get { return qty; }
            set { qty = value; QtyLbl.Text = value.ToString(); }
        }
        #endregion
    }
}

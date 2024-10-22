using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Reports
{
    public partial class OIF_List : UserControl
    {
        public OIF_List()
        {
            InitializeComponent();
        }
        #region AdvanceOrderListItems
        private string name;
        private string price;
        private string qty;




        [Category("ListItems")]
        public string Name
        {
            get { return name; }
            set { name = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public string Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        public string OrderQuantity
        {
            get { return qty; }
            set { qty = value; QtyLbl.Text = value.ToString(); }
        }
        #endregion
    }
}

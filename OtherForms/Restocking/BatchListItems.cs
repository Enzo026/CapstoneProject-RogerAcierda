using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Restocking
{
    public partial class BatchListItems : UserControl
    {
        public BatchListItems()
        {
            InitializeComponent();
        }
        #region Myregion
        private string itemId;
        private int itemQuantity;
        private string date;

        [Category("ItemList")]
        public string itemidData
        {
            get { return itemId; }
            set { itemId = value; BatchIDLbl.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public int itemquantityData
        {
            get { return itemQuantity; }
            set
            {
                itemQuantity = value; QtyLbl.Text = value.ToString();

            }
        }
        [Category("ItemList")]
        public string Date
        {
            get { return date; }
            set { date = value;  DateLbl.Text = value; }
        }
        #endregion
    }
}

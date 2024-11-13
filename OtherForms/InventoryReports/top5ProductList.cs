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
    public partial class top5ProductList : UserControl
    {
        public top5ProductList()
        {
            InitializeComponent();
        }
        #region Myregion
        private string Name;
        private string TotalQuantity;

        [Category("ItemList")]
        public string name
        {
            get { return Name; }
            set { Name = value; NameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Qty
        {
            get { return TotalQuantity; }
            set { TotalQuantity = value; QtyLbl.Text = value.ToString(); }
        }
        #endregion
    }
}

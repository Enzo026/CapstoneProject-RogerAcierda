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
    public partial class BatchInfoList : UserControl
    {
        public BatchInfoList()
        {
            InitializeComponent();
        }
        #region Myregion
        private string Name;
        private int Quantity;
        private string ExpDate;
        private string Supplier;

        [Category("ItemList")]
        public string ItemName
        {
            get { return Name; }
            set { Name = value; NameLbl.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public int ItemQuantity
        {
            get { return Quantity; }
            set
            {
                Quantity = value; QtyLbl.Text = value.ToString();
            }
        }
        [Category("ItemList")]
        public string ExpirationDate
        {
            get { return ExpDate; }
            set { ExpDate = value; ExpLbl.Text = value; }
        }

        [Category("ItemList")]
        public string SupplierName
        {
            get { return Supplier; }
            set { Supplier = value; SuppLbl.Text = value; }
        }
        #endregion
    }
}

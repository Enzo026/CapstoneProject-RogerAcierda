using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Abuel
{
    public partial class RestockList : UserControl
    {
        public RestockList()
        {
            InitializeComponent();
        }

        #region Myregion
        private int itemId;
        private string itemName;
        private int itemQuantity;
        private string stockLevel;
        private string supplier;

        [Category("ItemList")]
        public int itemidData
        {
            get { return itemidData; }
            set { itemidData = value; }
        }
        [Category("ItemList")]
        public string itemnameData
        {
            get { return itemName; }
            set { itemName = value; itemNameLabel.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public int itemquantityData
        {
            get { return itemQuantity; }
            set { itemQuantity = value; itemQuantityLabel.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public string supplierData
        {
            get { return supplier; }
            set { supplier = value; supplierLabel.Text = value; }
        }

        public string stocklevelData
        {
            get { return stockLevel; }
            set { stockLevel = value; stockLevelLabel.Text = value; }
        }


        #endregion
       
    }
}

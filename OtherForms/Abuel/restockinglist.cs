using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Flowershop_Thesis.OtherForms.Abuel
{
    public partial class restockinglist : Form
    {
        public restockinglist()
        {
            InitializeComponent();
            stockLevelCondition();
        }

        #region Myregion
        private int itemId;
        private string itemName;
        private int itemQuantity;
        private int stockLevel;
        private string supplier;

        [Category("ItemList")]
        public int itemidData
        {
            get { return itemidData; }
            set { itemidData = value;}
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
            get { return itemQuantity;}
            set { itemQuantity = value; itemQuantityLabel.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public string supplierData
        {
            get { return supplier; }
            set { supplier = value; supplierLabel.Text = value; }
        }


        #endregion
        public void stockLevelCondition()
        {
            if (itemQuantity == 0)
            {
                itemQuantityLabel.Text = "Out of Stock";
            }
            else if (itemQuantity> 0 && itemQuantity <= 20)
            {
                itemQuantityLabel.Text = "Low Stock";
            }
        }


    }
}

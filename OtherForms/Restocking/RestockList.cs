using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.OtherForms.Restocking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        private string type;

        [Category("ItemList")]
        public int itemidData
        {
            get { return itemId; }
            set { itemId = value; }
        }
        [Category("ItemList")]
        public string itemnameData
        {
            get { return itemName; }
            set { itemName = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public int itemquantityData
        {
            get { return itemQuantity; }
            set { itemQuantity = value; QtyLbl.Text = value.ToString();
                
            }
        }
        [Category("ItemList")]
        public string Type
        {
            get { return type; }
            set {   type = value; }
        }
       




        #endregion
        public void FlowerStatus(int value)
        {
            if (value >= 1 && value < 12)
            {
                StatusLbl.ForeColor = Color.DarkOrange;
                StatusLbl.Text = "Critical Stock Level";
            }
            else if (value >= 12 && value < 20)
            {
                StatusLbl.ForeColor = Color.Goldenrod;
                StatusLbl.Text = "Low Stock Level";
            }
            else if (value >= 20 && value < 40)
            {
                StatusLbl.ForeColor = Color.Green;
                StatusLbl.Text = "Mid Stock Level";
            }
            else if (value >= 40)
            {
                StatusLbl.ForeColor = Color.LightSeaGreen;
                StatusLbl.Text = "High Stock Level";
            }
            else
            {
                StatusLbl.ForeColor = Color.Crimson;
                StatusLbl.Text = "Out of Stock";
            }
        }
        public void MaterialStatus(int value)
        {
            if (value >= 1 && value < 2)
            {
                StatusLbl.ForeColor = Color.DarkOrange;
                StatusLbl.Text = "Critical Stock Level";
            }
            else if (value >= 2 && value < 4)
            {
                StatusLbl.ForeColor = Color.Goldenrod;
                StatusLbl.Text = "Low Stock Level";
            }
            else if (value >= 4 && value < 6)
            {
                StatusLbl.ForeColor = Color.Green;
                StatusLbl.Text = "Mid Stock Level";
            }
            else if (value >= 6)
            {
                StatusLbl.ForeColor = Color.LightSeaGreen;
                StatusLbl.Text = "High Stock Level";
            }
            else
            {
                StatusLbl.ForeColor = Color.Crimson;
                StatusLbl.Text = "Out of Stock";
            }
        }


        private void RestockList_Load(object sender, EventArgs e)
        {
            if(type == "Flowers")
            {
                FlowerStatus(itemQuantity);
            }
            else if (type == "Materials")
            {
               MaterialStatus(itemQuantity);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            RestockingProcess.ID = itemId;
            RestockingProcess.type = type;
            RestockItem frm = new RestockItem();
            frm.ShowDialog();
        }
    }
}

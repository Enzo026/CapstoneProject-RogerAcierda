using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class ProductMaintenanceListItem : UserControl
    {
        public ProductMaintenanceListItem()
        {
            InitializeComponent();
        }
        #region Myregion
        private string ItemID;
        private string ItemName;
        private string Quantity;
        private string ItemPrice;
        private Image ItemImage;
        private string type = "Flowers and Bouquet";

        [Category("ItemList")]
        public string ItmID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }
        [Category("ItemList")]
        public string ItmName
        {
            get { return ItemName; }
            set { ItemName = value; Name.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string ItmPrice
        {
            get { return ItemPrice; }
            set { ItemPrice = value; Price.Text = value.ToString() + " Php"; }
        }
        [Category("ItemList")]
        public string ItmQty
        {
            get { return Quantity; }
            set { Quantity = value; Qty.Text = value.ToString() + " Qty"; }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return ItemImage; }
            set { ItemImage = value; Img.Image = value; }
        }
        [Category("ItemList")]
        public string Type
        {
            get { return type; }
            set { type= value; }
        }
        #endregion

        private void SeemoreBtn_Click(object sender, EventArgs e)
        {   ChangeIds.ItemID = ItemID;
            if(ChangeIds.ItemType == "ItemInventory")
            {   
                FlowerInformation form = new FlowerInformation();   
                form.ShowDialog();
            }
            else if(ChangeIds.ItemType == "Materials")
            {
                //input materials form information
                MaterialInformation form = new MaterialInformation();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("There are no valid argument for this kind of type: " + type);
            }
        }
    }
}

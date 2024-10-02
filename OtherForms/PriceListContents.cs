using Flowershop_Thesis.SalesClerk.PriceList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms
{
    public partial class PriceListContents : UserControl
    {
        public PriceListContents()
        {
            InitializeComponent();
        }
        #region PriceList
        private string name;
        private int itemID;
        private int quantity;
        private string price;
        private string type;



        [Category("PriceList")]
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; IdLbl.Text = value.ToString(); }
        }
        [Category("PriceList")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; QuantityLbl.Text = value.ToString(); }
        }
        [Category("PriceList")]
        public string Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        [Category("PriceList")]
        public string Type
        {
            get { return type; }
            set { type = value; TypeLbl.Text = value.ToString(); }
        }

        [Category("PriceList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void DetailsBtn_Click(object sender, EventArgs e)
        {
            PriceList.instance.ID.Text = itemID.ToString();
        }
    }
}

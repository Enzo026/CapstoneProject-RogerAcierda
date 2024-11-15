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
using Capstone_Flowershop;
using Flowershop_Thesis;

namespace Flowershop_Thesis.OtherForms
{
    public partial class TransactionItemList : UserControl
    {   
        public TransactionItemList()
        {
            InitializeComponent();
        }

        #region Myregion
        private string name;
        private string color;
        private string itemID;
        private decimal price;
        private Image pic;
        private int stocks;
        private string OrderType;


        [Category("ItmList")]
        public int Stock
        {
            get { return stocks; }
            set { stocks = value; label19.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; label17.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        [Category("ItmList")]
        public string Type
        {
            get { return OrderType; }
            set { OrderType = value; }
        }

        [Category("ItmList")]
        public string Name
        {
            get { return name; }
            set { name = value; label16.Text = value; }
        }

        [Category("ItmList")]
        public string Color
        {
            get { return color; }
            set { color = value; label21.Text = value; }
        }


        [Category("ItmList")]
        public Image img
        {
            get { return pic; }
            set { pic = value; pictureBox1.Image = value; }
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            AddToCart ATC = new AddToCart();
            ATC.Name = name;
            ATC.Price = price;
            ATC.ItemID = itemID;
            ATC.Stock = stocks;
            ATC.type = OrderType;
            WalkInTransaction.OrderType = OrderType;
            ATC.Show();

        }
    }
}

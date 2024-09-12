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

namespace Flowershop_Thesis.OtherForms
{
    public partial class Adv_IndividualListItems : UserControl
    {
        public Adv_IndividualListItems()
        {
            InitializeComponent();
        }

        #region Myregion
        private string name;
        private string itemID;
        private decimal price;
        private Image pic;
        private int stocks;


        [Category("ItmList")]
        public int Stock
        {
            get { return stocks; }
            set { stocks = value; label13.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; label15.Text = value.ToString(); }
        }
        [Category("ItmList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("ItmList")]
        public string Name
        {
            get { return name; }
            set { name = value; label16.Text = value; }
        }



        [Category("ItmList")]
        public Image img
        {
            get { return pic; }
            set { pic = value; pictureBox4.Image = value; }
        }

        #endregion

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Click(object sender, EventArgs e)
        {
            ConfirmationIndividual CI = new ConfirmationIndividual();
            CI.Name = name;
            CI.Price =price;
            CI.ItemID = itemID;
            CI.Stock = stocks;
            CI.img = pic;
            CI.Show();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms
{
    public partial class CustomList : UserControl
    {
        public CustomList()
        {
            InitializeComponent();
            

        }
        #region OrderQueue
        private string name;
        private int itemID;
        private string price;
        private string Selection;
        private int qty;



        [Category("QueueList")]
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        [Category("QueueList")]
        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        [Category("QueueList")]
        public string selection
        {
            get { return Selection; }
            set { Selection = value;}
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void AddBtn_Click(object sender, EventArgs e)
        {
            CustomPopup CP = new CustomPopup();
            CP.ItemID = itemID;
            CP.Name = name;
            CP.Price = price;
            CP.Qty = qty;
            CP.selection = Selection;
            CP.Show();

        }
    }
}

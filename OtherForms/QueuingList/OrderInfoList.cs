using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.QueuingList
{
    public partial class OrderInfoList : UserControl
    {
        public OrderInfoList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string ItemName;
        private int ItemID;
        private decimal price;




        [Category("QueueList")]
        public int ItmId
        {
            get { return ItemID; }
            set { ItemID= value; IDLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string ItmName
        {
            get { return ItemName; }
            set { ItemName = value; NameLbl.Text = value; }
        }
        #endregion
    }
}

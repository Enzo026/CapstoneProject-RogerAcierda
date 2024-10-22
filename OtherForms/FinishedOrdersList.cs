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
    public partial class FinishedOrdersList : UserControl
    {
        public FinishedOrdersList()
        {
            InitializeComponent();
        }

        #region FinishedQueue
        private string name;
        private int transactionID;
        private decimal price;




        [Category("QueueList")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; IDLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion
    }
}

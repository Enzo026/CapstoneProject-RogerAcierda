using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class DisposalOrderListItems : UserControl
    {
        public DisposalOrderListItems()
        {
            InitializeComponent();
        }

        #region FinishedQueue
        private string Id, ItemName, Price, Quantity, status, Type;

        [Category("ActivityList")]
        public string id //ID
        {
            get { return Id; }
            set { Id = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisposalInfo.EvID = Id;
            DisposalInfo.EvQty = Quantity;
            DisposalInfo.EvName = ItemName;
            DisposalInfo.EvPrice = Price;

            DisposalEvaluation frm = new DisposalEvaluation();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DisposalInfo.type == "WalkIn")
            {
              
            }
            else if (DisposalInfo.type == "AdvanceOrder")
            {
               a
            }
            else
            {
                MessageBox.Show("Having trouble fetching the disposal order items");
            }
        }

        [Category("ActivityList")]
        public string ItmName //Customer Name
        {
            get { return ItemName; }
            set { ItemName = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string price // Price
        {
            get { return Price; }
            set { Price = value; PriceLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string qty
        {
            get { return Quantity; }
            set { Quantity = value; QtyLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string stat
        {
            get { return status; }
            set { status = value;  }
        }
        [Category("ActivityList")]
        public string type
        {
            get { return Type; }
            set { Type= value; TypeLbl.Text = value.ToString(); }
        }

        #endregion
    }
}

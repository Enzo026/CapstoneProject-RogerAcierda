using Flowershop_Thesis.SalesClerk.Order_Placement;
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
    public partial class CustomPopup : Form
    {
        public CustomPopup()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

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
            set { qty = value; QtyLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public string Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string selection
        {
            get { return Selection; }
            set { Selection = value; SelectionLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   if(textBox1.Text.Length > 0)
            {
                int input = int.Parse(textBox1.Text);
                if (input <= qty)
                {
                    double totalprice = input * double.Parse(PriceLbl.Text);
                    TotalPriceLbl.Text = totalprice.ToString();
                }
                else
                {
                    MessageBox.Show("Order Price Exceeds the maximum order Quantity");
                }

            }
  
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }

        private void ProceedBtn_Click(object sender, EventArgs e)
        {
            if (Selection.Equals("Primary"))
            {

                CustomBuoquet.instance.iPFlowerName.Visible = true;
                CustomBuoquet.instance.iPFlowerQty.Visible = true;
                CustomBuoquet.instance.iPFlowerRSP.Visible = true;
                CustomBuoquet.instance.iPFlowerPrice.Visible = true;

                CustomBuoquet.instance.iPFlowerName.Text = name;
                CustomBuoquet.instance.iPFlowerQty.Text = textBox1.Text.ToString();
                CustomBuoquet.instance.iPFlowerRSP.Text = price;
                CustomBuoquet.instance.iPFlowerPrice.Text = TotalPriceLbl.Text;

                this.Close();

            }
            else if (Selection.Equals("Secondary"))
            {
                CustomBuoquet.instance.iSFlowerName.Visible = true;
                CustomBuoquet.instance.iSFlowerQty.Visible = true;
                CustomBuoquet.instance.iSFlowerRSP.Visible = true;
                CustomBuoquet.instance.iSFlowerPrice.Visible = true;

                CustomBuoquet.instance.iSFlowerName.Text = name;
                CustomBuoquet.instance.iSFlowerQty.Text = textBox1.Text.ToString();
                CustomBuoquet.instance.iSFlowerRSP.Text = price;
                CustomBuoquet.instance.iSFlowerPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
            else if (Selection.Equals("Cover"))
            {
                CustomBuoquet.instance.iCoverName.Visible = true;
                CustomBuoquet.instance.iCoverQty.Visible = true;
                CustomBuoquet.instance.iCoverRSP.Visible = true;
                CustomBuoquet.instance.iCoverPrice.Visible = true;

                CustomBuoquet.instance.iCoverName.Text = name;
                CustomBuoquet.instance.iCoverQty.Text = textBox1.Text.ToString();
                CustomBuoquet.instance.iCoverRSP.Text = price;
                CustomBuoquet.instance.iCoverPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
            else if (Selection.Equals("Ribbon"))
            {
                CustomBuoquet.instance.iRibbonName.Visible = true;
                CustomBuoquet.instance.iRibbonQty.Visible = true;
                CustomBuoquet.instance.iRibbonRSP.Visible = true;
                CustomBuoquet.instance.iRibbonPrice.Visible = true;

                CustomBuoquet.instance.iRibbonName.Text = name;
                CustomBuoquet.instance.iRibbonQty.Text = textBox1.Text.ToString();
                CustomBuoquet.instance.iRibbonRSP.Text = price;
                CustomBuoquet.instance.iRibbonPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
        }
    }
}

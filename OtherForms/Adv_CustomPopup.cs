using Flowershop_Thesis.SalesClerk.Order_Placement;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
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
    public partial class Adv_CustomPopup : Form
    {
        public Adv_CustomPopup()
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
        {
            if (textBox1.Text.Length > 0)
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

                Adv_Custom.instance.iPFlowerName.Visible = true;
                Adv_Custom.instance.iPFlowerQty.Visible = true;
                Adv_Custom.instance.iPFlowerRSP.Visible = true;
                Adv_Custom.instance.iPFlowerPrice.Visible = true;

                Adv_Custom.instance.iPFlowerName.Text = name;
                Adv_Custom.instance.iPFlowerQty.Text = textBox1.Text.ToString();
                Adv_Custom.instance.iPFlowerRSP.Text = price;
                Adv_Custom.instance.iPFlowerPrice.Text = TotalPriceLbl.Text;

                this.Close();

            }
            else if (Selection.Equals("Secondary"))
            {
                Adv_Custom.instance.iSFlowerName.Visible = true;
                Adv_Custom.instance.iSFlowerQty.Visible = true;
                Adv_Custom.instance.iSFlowerRSP.Visible = true;
                Adv_Custom.instance.iSFlowerPrice.Visible = true;

                Adv_Custom.instance.iSFlowerName.Text = name;
                Adv_Custom.instance.iSFlowerQty.Text = textBox1.Text.ToString();
                Adv_Custom.instance.iSFlowerRSP.Text = price;
                Adv_Custom.instance.iSFlowerPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
            else if (Selection.Equals("Cover"))
            {
                Adv_Custom.instance.iCoverName.Visible = true;
                Adv_Custom.instance.iCoverQty.Visible = true;
                Adv_Custom.instance.iCoverRSP.Visible = true;
                Adv_Custom.instance.iCoverPrice.Visible = true;

                Adv_Custom.instance.iCoverName.Text = name;
                Adv_Custom.instance.iCoverQty.Text = textBox1.Text.ToString();
                Adv_Custom.instance.iCoverRSP.Text = price;
                Adv_Custom.instance.iCoverPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
            else if (Selection.Equals("Ribbon"))
            {
                Adv_Custom.instance.iRibbonName.Visible = true;
                Adv_Custom.instance.iRibbonQty.Visible = true;
                Adv_Custom.instance.iRibbonRSP.Visible = true;
                Adv_Custom.instance.iRibbonPrice.Visible = true;

                Adv_Custom.instance.iRibbonName.Text = name;
                Adv_Custom.instance.iRibbonQty.Text = textBox1.Text.ToString();
                Adv_Custom.instance.iRibbonRSP.Text = price;
                Adv_Custom.instance.iRibbonPrice.Text = TotalPriceLbl.Text;

                this.Close();
            }
        }
    }
}

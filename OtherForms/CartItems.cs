using Flowershop_Thesis.SalesClerk.Order_Placement;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Flowershop_Thesis;
using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.WalkInTransactionsFolder;

namespace Flowershop_Thesis.OtherForms
{
    public partial class CartItems : UserControl
    {
        SqlCommand cmd = new SqlCommand();


        public CartItems()
        {
            InitializeComponent();
   
        }

        #region CartOrderDetails
        private string name;
        private string itemID;
        private int price;
        private int Quantity;
        private int CartID;


        [Category("CartList")]
        public int qty
        {
            get { return Quantity; }
            set { Quantity = value; label8.Text = value.ToString(); }
        }
        [Category("CartList")]
        public int Price
        {
            get { return price; }
            set { price = value; label10.Text = value.ToString(); }
        }
        [Category("CartList")]
        public int cartID
        {
            get { return CartID; }
            set { CartID = value;}
        }
        [Category("CartList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("CartList")]
        public string Name
        {
            get { return name; }
            set { name = value; label7.Text = value; }
        }
        #endregion

        private void button14_Click(object sender, EventArgs e)
        {
           
            int cartqty = int.Parse(OrderPlacement.instance.lbl.Text);
            if(cartqty <= 0)
            {
                MessageBox.Show("No more items in cart");
            }
            else
            {
                WalkInTransaction.CancellationType = "Single";
                WalkInTransaction.CancellationItemName = name;
                WalkInTransaction.CancellationCartId = CartID.ToString();
                CancellationOfOrderFrm frm = new CancellationOfOrderFrm();
                frm.ShowDialog();



            }
           
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
    }
}

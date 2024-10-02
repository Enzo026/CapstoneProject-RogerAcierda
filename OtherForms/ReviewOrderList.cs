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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.OtherForms
{
    public partial class ReviewOrderList : UserControl
    {
        SqlCommand cmd = new SqlCommand();


        public ReviewOrderList()
        {
            InitializeComponent();
        }

        #region CartOrderDetails
        private string name;
        private string itemID;
        private int price;
        private int Quantity;
        private int CartID;


        [Category("OrderList")]
        public int qty
        {
            get { return Quantity; }
            set { Quantity = value; ItemQty.Text = value.ToString(); }
        }
        [Category("OrderList")]
        public int Price
        {
            get { return price; }
            set { price = value; ItemPrice.Text = value.ToString(); }
        }
        [Category("OrderList")]
        public int cartID
        {
            get { return CartID; }
            set { CartID = value; }
        }
        [Category("OrderList")]
        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        [Category("OrderList")]
        public string Name
        {
            get { return name; }
            set { name = value; ItemName.Text = value; }
        }
        #endregion


        private void Editlbl_Click(object sender, EventArgs e)
        {

        }

        private void Deletelbl_Click(object sender, EventArgs e)
        {
            int cartqty = int.Parse(ReviewOrder.instance.counter.Text);
            if (cartqty <= 0)
            {
                MessageBox.Show("No more items in cart");
            }
            else
            {
                try
                {   
                    using(SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("Delete from ServingCart where CartID = " + CartID + " ;", con);
                        cmd.ExecuteNonQuery();
                        int cart = cartqty - 1;
                        if (cart == 0)
                        {
                            ReviewOrder.instance.counter.Text = cart.ToString();
                        }
                        else if (cart > 0)
                        {
                            this.Parent.Controls.Remove(this);
                            ReviewOrder.instance.counter.Text = cart.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
    }
}

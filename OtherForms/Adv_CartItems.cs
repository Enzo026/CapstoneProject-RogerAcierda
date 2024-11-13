using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms
{
    public partial class Adv_CartItems : UserControl
    {   
        public Adv_CartItems()
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
            set { CartID = value; }
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
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Advance_ServingCart where CartID = @id ;", con);
                    cmd.Parameters.AddWithValue("@id", CartID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item cancelled!");
                    AdvanceOrderCart.instance.Loadinglbl.Visible = true;
                
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
    }
}

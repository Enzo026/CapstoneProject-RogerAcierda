using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Restocking;
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

namespace Flowershop_Thesis.OtherForms.Restocking
{
    public partial class RestockingProcessItems : UserControl
    {
        public RestockingProcessItems()
        {
            InitializeComponent();
        }
        #region Myregion
        private int Id;
        private int itemId;
        private string itemName;
        private int itemQuantity;
        private string type;
        private string Supplier;

        [Category("ItemList")]
        public int itemidData
        {
            get { return itemId; }
            set { itemId = value; }
        }
        [Category("ItemList")]
        public int itemid
        {
            get { return Id; }
            set { Id = value; }
        }
        [Category("ItemList")]
        public string itemnameData
        {
            get { return itemName; }
            set { itemName = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public int itemquantityData
        {
            get { return itemQuantity; }
            set
            {
                itemQuantity = value; QtyLbl.Text = value.ToString();

            }
        }

        [Category("ItemList")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        [Category("ItemList")]
        public string Supp
        {
            get { return Supplier; }
            set { Supplier = value; SupplierLbl.Text = value; }
        }





        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteItemById(Id);
        }
        public void DeleteItemById(int itemId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM TempRestockTbl WHERE Id = @ItemID";
                    using (SqlCommand command = new SqlCommand(deleteQuery, con))
                    {
                        // Use parameterized query to prevent SQL injection
                        command.Parameters.AddWithValue("@ItemID", itemId);

                        int rowsAffected = command.ExecuteNonQuery();
               
                        RestockNew.instance.loading.Visible = true;
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No item found with the given ID.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting item: " + e.Message);
            }
        }

    }
}

using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class Inv_Supplier : UserControl
    {
        public Inv_Supplier()
        {
            InitializeComponent();
        }

        #region Myregion
        private int SupplierID;
        private string SupplierName;
        private string SupplierType;
        private string SupplierAddress;
        private string SupplierContact;

        private Image SupplierImage;

        [Category("ItemList")]
        public int SuppID
        {
            get { return SupplierID; }
            set { SupplierID = value; }
        }
        [Category("ItemList")]
        public string Suppname
        {
            get { return SupplierName; }
            set { SupplierName = value; SuppNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppContact
        {
            get { return SupplierContact; }
            set { SupplierContact = value; ContactNumLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppType
        {
            get { return SupplierType; }
            set { SupplierType = value; SuppTypeLbl.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public string SuppAddress
        {
            get { return SupplierAddress; }
            set { SupplierAddress = value; SuppAddressLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public Image Img
        {
            get { return SupplierImage; }
            set { SupplierImage = value; pictureBox1.Image = value; }
        }

        #endregion

        private void Inv_Supplier_Load(object sender, EventArgs e)
        {
            if (SupplierType == "Flowers")
            {
                FlowerItems();
            }
            else if (SupplierType == "Materials")
            {
                MaterialItems();
            }
            else if (SupplierType == "Flowers and Materials")
            {
                MaterialItems();
            }
            else
            {
                MessageBox.Show("Missing Supplier Type");
            }
        }
        public void FlowerItems()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where Supplier=@Name;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@Name", SupplierName);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        if (rowCount > 4) { button1.Visible = true; }
                        SuppliedItemList[] itemList = new SuppliedItemList[rowCount];

                        string sqlQuery = "select * from ItemInventory where Supplier=@Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Name", SupplierName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SuppliedItemList();
                                    itemList[index].Name = reader["ItemName"].ToString();
                                    flowLayoutPanel2.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void MaterialItems()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where Supplier=@Name;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Name", SupplierName);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        if (rowCount > 4) { button1.Visible = true; }
                        SuppliedItemList[] itemList = new SuppliedItemList[rowCount];

                        string sqlQuery = "select * from Materials where Supplier=@Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Name", SupplierName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SuppliedItemList();
                                    itemList[index].Name = reader["ItemName"].ToString();
                                    flowLayoutPanel2.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           SupplierInfo.SupplierName = SupplierName;
            SupplierInfo.SupplierType = SupplierType;

            SupplierSuppliedItemList frm = new SupplierSuppliedItemList();
            frm.ShowDialog();
        }
    }
}

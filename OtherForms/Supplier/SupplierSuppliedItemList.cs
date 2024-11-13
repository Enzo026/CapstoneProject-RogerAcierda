using Capstone_Flowershop;
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

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class SupplierSuppliedItemList : Form
    {
        public SupplierSuppliedItemList()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupplierSuppliedItemList_Load(object sender, EventArgs e)
        {
            SupplierNameLbl.Text = SupplierInfo.SupplierName;
            if(SupplierInfo.SupplierType == "Flowers")
            {
                FlowerItems();
            }
            else if (SupplierInfo.SupplierType == "Materials")
            {
                MaterialItems();
            }

        }
        public void FlowerItems()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where Supplier=@Name;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Name", SupplierInfo.SupplierName);
                        int rowCount = (int)countCommand.ExecuteScalar();
                     
                        SuppliedItemList[] itemList = new SuppliedItemList[rowCount];

                        string sqlQuery = "select * from ItemInventory where Supplier=@Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Name", SupplierInfo.SupplierName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SuppliedItemList();
                                    itemList[index].Name = reader["ItemName"].ToString();
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
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
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where Supplier=@Name;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Name", SupplierInfo.SupplierName);
                        int rowCount = (int)countCommand.ExecuteScalar();
            
                        SuppliedItemList[] itemList = new SuppliedItemList[rowCount];

                        string sqlQuery = "select * from Materials where Supplier=@Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Name", SupplierInfo.SupplierName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SuppliedItemList();
                                    itemList[index].Name = reader["ItemName"].ToString();
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
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

    }
}

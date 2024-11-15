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
using Capstone_Flowershop;
using Flowershop_Thesis;
using Flowershop_Thesis.InventoryClerk.StockAdjustment;

namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    public partial class SA_BatchItems : Form
    {
        public static SA_BatchItems instance;
        public Label loading;
        public SA_BatchItems()
        {
            InitializeComponent();
            instance = this;
            loading = label30;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SA_AdjustQuantity frm = new SA_AdjustQuantity();
            frm.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
            StockAdjustmentFrmcs.Instance.loading.Visible = true;
        }

        private void SA_BatchItems_Load(object sender, EventArgs e)
        {
            label3.Text = SA_Info.BatchID;
            loadItems();    
        }
        public void loadItems()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM RestockingTbl where BatchID =  @BatchID;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@BatchID", SA_Info.BatchID);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        BatchListItems[] itemList = new BatchListItems[rowCount];

                        string sqlQuery = "SELECT * FROM RestockingTbl where BatchID =  @BatchID;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@BatchID", SA_Info.BatchID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new BatchListItems();
                                    itemList[index].ID = reader["BatchID"].ToString();
                                    itemList[index].qty = reader["Qty"].ToString();
                                    itemList[index].RID = reader["Id"].ToString();
                                    itemList[index].Name = reader["ItemName"].ToString();
                                    itemList[index].ItmID = reader["ItemID"].ToString();
                                    itemList[index].type = reader["Type"].ToString();


                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label30_VisibleChanged(object sender, EventArgs e)
        {
            if (label30.Visible) { 
            loadItems();
            label30.Visible = false;
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using Capstone_Flowershop;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.OtherForms.Abuel;
using Flowershop_Thesis.OtherForms.StockAdjustments;
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

namespace Flowershop_Thesis.InventoryClerk.StockAdjustment
{
    public partial class StockAdjustmentFrmcs : Form
    {

        public static StockAdjustmentFrmcs Instance;
        public Label loading;
        public StockAdjustmentFrmcs()
        {
            InitializeComponent();
            Instance = this;
            loading = label3;
            loadTableData();
            showLogs();
        }

        public void loadTableData()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM TodayBatchRestocks;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                        string sqlQuery = "SELECT * FROM TodayBatchRestocks order by BatchID desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new StockAdjustmentListItems();
                                    itemList[index].ID = reader["BatchID"].ToString();
                                    itemList[index].qty = reader["TotalCount"].ToString();

                                    // Read the RestockingDate as DateTime and format it
                                    DateTime restockingDate = (DateTime)reader["RestockingDate"];
                                    itemList[index].date = restockingDate.ToString("MMM dd, yyyy"); // Format the date

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    flowLayoutPanel1.Controls.Clear();
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();

                        string countQuery = "SELECT COUNT(*) FROM TodayBatchRestocks where BatchID like @input;";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {   
                            countCommand.Parameters.AddWithValue("@input", textBox1.Text + "%");
                            int rowCount = (int)countCommand.ExecuteScalar();
                            StockAdjustmentListItems[] itemList = new StockAdjustmentListItems[rowCount];

                            string sqlQuery = "SELECT * FROM TodayBatchRestocks where BatchID like @input";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@input", textBox1.Text + "%");
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new StockAdjustmentListItems();
                                        itemList[index].ID = reader["BatchID"].ToString();
                                        itemList[index].qty = reader["TotalCount"].ToString();

                                        // Read the RestockingDate as DateTime and format it
                                        DateTime restockingDate = (DateTime)reader["RestockingDate"];
                                        itemList[index].date = restockingDate.ToString("MMM dd, yyyy"); // Format the date

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
            else
            {
                loadTableData();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SA_BatchItems frm = new SA_BatchItems();
            frm.ShowDialog();
            
        }

        private void label3_VisibleChanged(object sender, EventArgs e)
        {
            if (label3.Visible) { 
                loadTableData();
                showLogs();
                label3.Visible = false;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void showLogs()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string countQuery = "SELECT COUNT(*) FROM HistoryLogs where Title = 'Stock Adjusted';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        SA_ActivityLogs[] itemList = new SA_ActivityLogs[rowCount];

                        string sqlQuery = "SELECT * FROM HistoryLogs where Title = 'Stock Adjusted' order by Date desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SA_ActivityLogs();
                                    itemList[index].EmpName = reader["Employee"].ToString();
                                    itemList[index].desc = reader["Definition"].ToString();

                                    // Read the RestockingDate as DateTime and format it
                                    DateTime Date = (DateTime)reader["Date"];
                                    itemList[index].date = Date.ToString("MMM dd, yyyy"); // Format the date

                                    flowLayoutPanel2.Controls.Add(itemList[index]);
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
    }
}

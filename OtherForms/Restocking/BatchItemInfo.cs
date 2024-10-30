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

namespace Flowershop_Thesis.OtherForms.Restocking
{
    public partial class BatchItemInfo : Form
    {
        public BatchItemInfo()
        {
            InitializeComponent();
        }

        private void BatchItemInfo_Load(object sender, EventArgs e)
        {
            showinfo();
            getList();
        }
        public void showinfo()
        {
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                SqlCommand command = new SqlCommand("GetRestockingInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add the BatchID parameter
                command.Parameters.Add(new SqlParameter("@BatchID", SqlDbType.NVarChar));
                command.Parameters["@BatchID"].Value = ViewInfo.RI_Id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        label2.Text = reader["BatchID"].ToString();

                        // Parse and format RestockingDate
                        if (DateTime.TryParse(reader["RestockingDate"].ToString(), out DateTime restockingDate))
                        {
                            label5.Text = restockingDate.ToString("MMM dd, yyyy"); // Formats to "Oct 21, 2024"
                        }
                        else
                        {
                            label5.Text = "Invalid Date"; // Handle invalid date scenario
                        }

                        label3.Text = reader["employee"].ToString();
                        label7.Text = reader["totalItems"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

        }
        
        public void getList()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "SELECT Count(*) FROM RestockingTbl where BatchID = @BatchId;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@BatchId", ViewInfo.RI_Id);
                    int rowCount = (int)countCommand.ExecuteScalar();
                    BatchInfoList[] itemList = new BatchInfoList[rowCount];

                    string sqlQuery = "SELECT * FROM RestockingTbl where BatchID = @BatchId";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@BatchId", ViewInfo.RI_Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read() && index < itemList.Length)
                            {
                                itemList[index] = new BatchInfoList();
                                itemList[index].ItemName = reader["ItemName"].ToString();
                                itemList[index].SupplierName = reader["Supplier"].ToString();

                                // Parse and format ExpirationDate
                                if (DateTime.TryParse(reader["ExpirationDate"].ToString(), out DateTime expirationDate))
                                {
                                    itemList[index].ExpirationDate = expirationDate.ToString("MMM dd, yyyy"); // Formats to "Oct 21, 2024"
                                }
                                else
                                {
                                    itemList[index].ExpirationDate = "Invalid Date"; // Handle invalid date scenario
                                }

                                itemList[index].ItemQuantity = int.Parse(reader["Qty"].ToString());
                                flowLayoutPanel1.Controls.Add(itemList[index]);
                                index++;
                            }
                        }
                    }
                }
            }

        }

    }
}

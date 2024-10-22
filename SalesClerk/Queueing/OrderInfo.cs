using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.OtherForms.QueuingList;
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

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class OrderInfo : Form
    {
        public OrderInfo()
        {
            InitializeComponent();
        }

        public void getorderitems()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from SalesItemTbl where TransactionID = @info;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        OrderInfoList[] inv = new OrderInfoList[rowCount];

                        string sqlQuery = "SELECT * FROM SalesItemTbl where TransactionID = @info ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new OrderInfoList();
                                    inv[index].ItmId = int.Parse(reader["ItemID"].ToString());
                                    inv[index].ItmName = reader["ItemName"].ToString();
                                    inv[index].Price = decimal.Parse(reader["ItemPrice"].ToString());
                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        public void getInfo()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM TransactionsTbl where TransactionID = @info ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                   
                                TransactionIdLbl.Text = reader["TransactionID"].ToString();
                                CustomerNameLbl.Text = reader["CustomerName"].ToString();
                                TotalAmountLbl.Text = reader["Price"].ToString();
                                StatusLbl.Text = reader["Status"].ToString();
                                PaymentStatusLbl.Text = reader["PaymentStatus"].ToString();
                                PaymentMethodLbl.Text = reader["PaymentMethod"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }
        private void OrderInfo_Load(object sender, EventArgs e)
        {
            MessageBox.Show(ChangeIds.OrderInfo);
            getInfo();
            getorderitems();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

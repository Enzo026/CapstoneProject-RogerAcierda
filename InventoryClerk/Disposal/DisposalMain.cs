using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.Abuel;
using Flowershop_Thesis.OtherForms.DisposalContents;
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

namespace Flowershop_Thesis.InventoryClerk.Disposal
{
    public partial class DisposalMain : Form
    {
        public static DisposalMain Instance;
        public Label Reset;
        public DisposalMain()
        {
            InitializeComponent();
            Instance = this;
            Reset = LoadingLbl;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void getPending()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM CancelledTransactions WHERE Evaluation = 'Pending'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        PendingList[] itemList = new PendingList[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransactions WHERE Evaluation = 'Pending'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new PendingList
                                    {
                                        id = reader["TransactionID"].ToString(),
                                        customerName = reader["CustomerName"].ToString(),
                                        date = reader["CancellationDate"] != DBNull.Value
                                   ? Convert.ToDateTime(reader["CancellationDate"]).ToString("MMM dd, yyyy")
                                   : string.Empty,
                                        type = reader["Type"].ToString(),
                                        status = reader["Evaluation"].ToString(),
                                        price = reader["TotalPrice"] != DBNull.Value ? ((decimal)reader["TotalPrice"]).ToString("C") : string.Empty
                                };

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
        public void getFinished()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM CancelledTransactions WHERE Evaluation = 'Evaluated'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        PendingList[] itemList = new PendingList[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransactions WHERE Evaluation = 'Evaluated'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new PendingList
                                    {
                                        id = reader["TransactionID"].ToString(),
                                        customerName = reader["CustomerName"].ToString(),
                                        date = reader["CancellationDate"] != DBNull.Value
                                   ? Convert.ToDateTime(reader["CancellationDate"]).ToString("MMM dd, yyyy")
                                   : string.Empty,
                                        type = reader["Type"].ToString(),
                                        status = reader["Evaluation"].ToString(),
                                        price = reader["TotalPrice"] != DBNull.Value ? ((decimal)reader["TotalPrice"]).ToString("C") : string.Empty
                                    };

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

        private void DisposalMain_Load(object sender, EventArgs e)
        {
            getPending();
            getFinished();
        }

        private void label54_Click(object sender, EventArgs e)
        {


        }

        private void LoadingLbl_VisibleChanged(object sender, EventArgs e)
        {
            if(LoadingLbl.Visible)
            {
                getPending();
                getFinished();
                LoadingLbl.Visible = false;
            }
        }
    }
}

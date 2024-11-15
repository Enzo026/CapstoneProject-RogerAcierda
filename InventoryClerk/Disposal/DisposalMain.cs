﻿using Capstone_Flowershop;
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
        public void SoonToExpire()
        {
            try
            {
                flowLayoutPanel5.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM DisposedItems";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        SoonToExpiredList[] itemList = new SoonToExpiredList[rowCount];

                        string sqlQuery = "  Select * from RestockingTbl where ExpirationDate >= getdate() order by ExpirationDate asc;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new SoonToExpiredList()
                                    {
                                        qty = reader["Qty"].ToString(),
                                        name = reader["ItemName"].ToString(),
                                        date = reader["ExpirationDate"] != DBNull.Value
                                   ? Convert.ToDateTime(reader["ExpirationDate"]).ToString("MMM dd, yyyy")
                                   : string.Empty,
                                        type = reader["Type"].ToString()
       

                                    };

                                    flowLayoutPanel5.Controls.Add(itemList[index]);
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
        public void getDisposed()
        {
            try
            {
                flowLayoutPanel3.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM DisposedItems";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RetrievedItemsList[] itemList = new RetrievedItemsList[rowCount];

                        string sqlQuery = "SELECT * FROM DisposedItems order by DisposalDate desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RetrievedItemsList
                                    {
                                        id = reader["DisposalID"].ToString(),
                                        ItmName = reader["ItemName"].ToString(),
                                        date = reader["DisposalDate"] != DBNull.Value
                                   ? Convert.ToDateTime(reader["DisposalDate"]).ToString("MMM dd, yyyy")
                                   : string.Empty,
                                        price = reader["Price"].ToString(),
                                        qty = reader["Quantity"].ToString(),
                                        Emp = reader["Employee"].ToString()

                                    };

                                    flowLayoutPanel3.Controls.Add(itemList[index]);
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
        public void getRetrieved()
        {
            try
            {
                flowLayoutPanel4.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM RetrieveItems";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RetrievedItemsList[] itemList = new RetrievedItemsList[rowCount];

                        string sqlQuery = "SELECT * FROM RetrieveItems order by RetrievalDate desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RetrievedItemsList
                                    {
                                        id = reader["RetrievalID"].ToString(),
                                        ItmName = reader["ItemName"].ToString(),
                                        date = reader["RetrievalDate"] != DBNull.Value
                                   ? Convert.ToDateTime(reader["RetrievalDate"]).ToString("MMM dd, yyyy")
                                   : string.Empty,
                                        price = "0.00",
                                        //price = reader["Price"].ToString(),
                                        qty = reader["RetrievedQuantity"].ToString(),
                                        Emp = reader["Employee"].ToString()
                                  
                                    };

                                    flowLayoutPanel4.Controls.Add(itemList[index]);
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
        public void getPending()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM CancelledTransactions WHERE Evaluation = 'Pending' AND OrderStatus = 'Complete'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        PendingList[] itemList = new PendingList[rowCount];

                        string sqlQuery = "SELECT * FROM CancelledTransactions WHERE Evaluation = 'Pending' AND OrderStatus = 'Complete' order by CancellationDate desc";
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

                        string sqlQuery = "SELECT * FROM CancelledTransactions WHERE Evaluation = 'Evaluated' order by CancellationDate desc";
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
            getRetrieved();
            getDisposed();
            SoonToExpire();
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
                getRetrieved();
                getDisposed ();
                LoadingLbl.Visible = false;
            }
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }
    }
}

using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Queueing;
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
using System.Xml.Serialization;

namespace Flowershop_Thesis.OtherForms.QueuingList
{
    public partial class PaymentList : UserControl
    {
        public PaymentList()
        {
            InitializeComponent();
        }
        #region OrderQueue
        private string name;
        private int transactionID;
        private decimal price;
        private string status;



        [Category("QueueList")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; IdLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public decimal Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString() + " php"; }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        public void addTransactionLog(string CustomerName, string Price, string TId, string definition)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", CustomerName);
                    cmd.Parameters.AddWithValue("@Definition", Price);
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "TransactionLog");
                    cmd.Parameters.AddWithValue("@RefID", TId.Trim());
                    cmd.Parameters.AddWithValue("@HeadLine", definition);


                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed to Payments", "Order Update", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Payment' WHERE TransactionID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", transactionID);
                            updateCommand.ExecuteNonQuery();
                            int queue = int.Parse(QueuingFormBack.instance.lblcounter.Text);
                            int addqueue = queue - 1;
                            QueuingFormBack.instance.lblcounter.Text = addqueue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Cancelling the order" + ex.Message);
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            UpdateStatus US = new UpdateStatus();
            US.Name = name;
            US.transID = transactionID;
            US.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want this order to be Canceled?", "Cancel Order", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Cancelled', OrderStatus = 'Complete' WHERE TransactionID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", transactionID);
                            updateCommand.ExecuteNonQuery();
                            int queue = int.Parse(QueuingFormBack.instance.lblcounter.Text);
                            int addqueue = queue - 1;
                            QueuingFormBack.instance.lblcounter.Text = addqueue.ToString();
                        }
                        insertCancelledTransaction();
                        string def = UserInfo.Empleyado + " Cancelled the order  of "+ name + " with the transaction id of " + transactionID ;
                        addTransactionLog(name, price.ToString(), transactionID.ToString(), def);
                        MessageBox.Show("Order cancelled!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Cancelling the order" + ex.Message);
            }
        }

        public void insertCancelledTransaction()
        {
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand("CancellationOfOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@ID", transactionID));
                    command.Parameters.Add(new SqlParameter("@TransactionType", "WalkIn"));

                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Optionally, log success
                        MessageBox.Show("Cancellation executed successfully.");
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL errors
                        MessageBox.Show("SQL Error: " + ex.Message);
                        // Optionally log the error details
                    }
                    catch (Exception ex)
                    {
                        // Handle general errors
                        MessageBox.Show("Error: " + ex.Message);
                        // Optionally log the error details
                    }
                    finally
                    {
                        // Ensure the connection is closed
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        public void Processing()
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Processing' WHERE TransactionID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", transactionID);
                            updateCommand.ExecuteNonQuery();
                       
                        }
                        MessageBox.Show("Status Updated!");
                        string def = UserInfo.Empleyado + " Reverted back the status of order(" + transactionID + ") to Processing ";
                        addTransactionLog(name, price.ToString(), transactionID.ToString(), def);
                        QueuingFormBack.instance.lblcounter.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            Processing();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeIds.OrderInfo = transactionID.ToString();
            OrderInfo frm = new OrderInfo();
            frm.ShowDialog();
        }
    }
}

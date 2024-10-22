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

namespace Flowershop_Thesis.OtherForms.QueuingList
{
    public partial class ReceivingList : UserControl
    {
        public ReceivingList()
        {
            InitializeComponent();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button47_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Item Recieved?", "Order Update", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Completed' WHERE TransactionID = @ID;";
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
        public void Payment()
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Payment', PaymentStatus = 'Unpaid', PaymentMethod = 'None' WHERE TransactionID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", transactionID);
                            updateCommand.ExecuteNonQuery();

                        }
                        MessageBox.Show("Status Updated!");
                        string def = UserInfo.Empleyado + " Reverted back the status of order(" + transactionID + ") to Payment ";
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

        private void button48_Click(object sender, EventArgs e)
        {
            Payment();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            ChangeIds.OrderInfo = transactionID.ToString();
            OrderInfo frm = new OrderInfo();
            frm.ShowDialog();
        }
    }
}

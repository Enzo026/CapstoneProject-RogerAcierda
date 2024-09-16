using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    public partial class OrdersTodayList : UserControl
    {
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public OrdersTodayList()
        {
            InitializeComponent();
            testConnection();
          
       //     MessageBox.Show(transactionID);
        }
        #region FinishedQueue
        private string name;
        private string transactionID;
        private string Downpayment;
        private double Payable;
        private string Discount;
        public string OrderItems;
        private string TotalAmount;






        [Category("QueueList")]
        public string transID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }
        [Category("QueueList")]
        public double Price
        {
            get { return Payable; }
            set { Payable = value; 
               // OrderPrice.Text = value.ToString(); 
            }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; CustomerName.Text = value; }
        }
        [Category("QueueList")]
        public string downpayment
        {
            get { return Downpayment; }
            set { Downpayment = value; }
        }
        [Category("QueueList")]
        public string discount
        {
            get { return Discount; }
            set { Discount = value; }
        }
        [Category("QueueList")]
        public string Total
        {
            get { return TotalAmount; }
            set { TotalAmount = value; }
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            AdvanceOrderPaymentDetails.OrderID = transactionID;
            AdvanceOrderPaymentDetails.CustomerName = name;
            AdvanceOrderPaymentDetails.TotalAmount = TotalAmount;
            AdvanceOrderPaymentDetails.Downpayment = downpayment;
            AdvanceOrderPaymentDetails.AmountPayable = Payable.ToString();
            AdvanceOrderPaymentDetails.Discount = discount;
            AdvanceOrderPaymentDetails.OrderItems = OrderItems;

            AdvanceOrderFinishPayment form = new AdvanceOrderFinishPayment();
            form.ShowDialog();

        }
        public void getTotalItems()
        {
            try
            {
                con.Open();
                string sqlQty = "SELECT COUNT(*) AS Orders FROM AdvanceOrderItems WHERE OrderID = @ID;";
                using (SqlCommand comd = new SqlCommand(sqlQty, con))
                {

                     comd.Parameters.AddWithValue("@ID", transactionID);
                    if(int.Parse(comd.ExecuteScalar().ToString()) > 0)
                    {
                        OrderItems = comd.ExecuteScalar().ToString();
                    }
                    else
                    {
                        OrderItems = "0";
                    }
                    
           

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on fetching order qty :" + ex.Message);
            }
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));

            string databaseFilePath = Path.Combine(parentDirectory, "try.mdf");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void CustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrdersTodayList_Load(object sender, EventArgs e)
        {
            getTotalItems();
        }
    }
}
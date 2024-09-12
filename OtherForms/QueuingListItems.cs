using Flowershop_Thesis.SalesClerk.Queueing;
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

namespace Flowershop_Thesis.OtherForms
{

    public partial class QueuingListItems : UserControl
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public QueuingListItems()
        {
            InitializeComponent();
            testConnection();
        }
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Build the full path to the database file
            string databaseFilePath = Path.Combine(executableDirectory, "try.mdf");

            // Build the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;";

            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);
                
                    // Perform database operations here

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        #region OrderQueue
        private string name;
        private int transactionID;
        private double price;
        private string status;
        


        [Category("QueueList")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; IdLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public double Price
        {
            get { return price; }
            set { price = value; PriceLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string Status
        {
            get { return status; }
            set { status = value; StatusLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void CancelLbl_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want this order to be Canceled?", "Cancel Order", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string updateQuery = "UPDATE TransactionsTbl SET Status = 'Cancelled' WHERE TransactionID = @ID;";
                con.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                {

                    updateCommand.Parameters.AddWithValue("@ID", transactionID);

                    updateCommand.ExecuteNonQuery();

                    int queue = int.Parse(QueuingFormBack.instance.lblcounter.Text);
                    int addqueue = queue - 1;
                    QueuingFormBack.instance.lblcounter.Text = addqueue.ToString();

                }
                con.Close();
                MessageBox.Show("Order cancelled!");


            }
            else
            {
                //none
            }
        }

        private void Change_StatusLbl_Click(object sender, EventArgs e)
        {
            UpdateStatus US = new UpdateStatus();
            US.Name = name;
            US.transID = transactionID;
            US.status = status;
            US.Show();

        }
    }
}

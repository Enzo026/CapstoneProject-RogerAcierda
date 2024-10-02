using Capstone_Flowershop;
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
    public partial class CancelledOrderList : UserControl
    {
        public CancelledOrderList()
        {
            InitializeComponent();
 
        }
        #region FinishedQueue
        private string name;
        private int transactionID;
        private double price;




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
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want this order to be Reverted back to processing?", "Revert Order", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    string updateQuery = "UPDATE TransactionsTbl SET Status = 'Processing' WHERE TransactionID = @ID;";
                    con.Open();
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                    {

                        updateCommand.Parameters.AddWithValue("@ID", transactionID);

                        updateCommand.ExecuteNonQuery();

                        int queue = int.Parse(QueuingFormBack.instance.lblcounter.Text);
                        int addqueue = queue + 1;
                        QueuingFormBack.instance.lblcounter.Text = addqueue.ToString();

                    }
                    MessageBox.Show("Order Reverted");
                }
            }
        }
    }
}

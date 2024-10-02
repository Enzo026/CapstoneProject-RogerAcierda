using Flowershop_Thesis.SalesClerk.Queueing;
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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.OtherForms
{
    public partial class UpdateStatus : Form
    {

        public UpdateStatus()
        {
            InitializeComponent();
        }

        #region UpdateStatus
        private string name;
        private int transactionID;
        private string Status;




        [Category("QueueList")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; TransID.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public string status
        {
            get { return Status; }
            set
            {
                Status = value;
                if (Status.Equals("Processing"))
                {
                    radioButton1.Checked = true;
                }
                else if (Status.Equals("Payment"))
                {
                    radioButton2.Checked = true;
                }
                else if (Status.Equals("Complete"))
                {
                    radioButton3.Checked = true;
                }
                else
                {
                    MessageBox.Show("Unknown Status please verify");
                }


            }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Processing();
            }
            else if(radioButton2.Checked)
            {
                Payment();
            }
            else if (radioButton3.Checked)
            {
                Complete();

            }
          

        }

        public void Processing()
        {
            try
            {   
                DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
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
                            QueuingFormBack.instance.lblcounter.Text = " ";
                        }
                        MessageBox.Show("Status Updated!");
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Payment()
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   
                    using(SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        string updateQuery = "UPDATE TransactionsTbl SET Status = 'Payment' WHERE TransactionID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", transactionID);
                            updateCommand.ExecuteNonQuery();
                            QueuingFormBack.instance.lblcounter.Text = " ";
                        }
                        MessageBox.Show("Status Updated!");
                        this.Close();
                    }
                }
                else
                {
                    //none
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Complete()
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   
                    using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
                        MessageBox.Show("Status Updated!");
                        this.Close();
                    }
                }
                else
                {
                    //none
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

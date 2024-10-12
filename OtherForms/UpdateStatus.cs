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
using System.Reflection;
using System.Diagnostics;

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
        public string Name
        {
            get { return name; }
            set { name = value; label1.Text = value; }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string TotalPrice, PaymentStatus, OrderStatus, CustomerName;
        public void GetInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TransactionsTbl where Status != 'Completed' AND Status != 'Cancelled' AND TransactionID = @ID;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@ID", transactionID);
                        int rowCount = (int)countCommand.ExecuteScalar();


                        string sqlQuery = "SELECT * FROM TransactionsTbl where Status != 'Completed' AND Status != 'Cancelled' AND TransactionID = @ID;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@ID", transactionID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    CustomerName = reader["CustomerName"].ToString();
                                    TotalPrice = reader["Price"].ToString();
                                    PaymentStatus = reader["PaymentStatus"].ToString();
                                    label7.Text = reader["Price"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error on Fetching Information | GetInfo() : " + ex.Message);
            }
        }
        //public void Processing()
        //{
        //    try
        //    {   
        //        DialogResult result = MessageBox.Show("Proceed with changes?", "Update Status", MessageBoxButtons.YesNo);
        //        if (result == DialogResult.Yes)
        //        {   
        //            using(SqlConnection con = new SqlConnection(Connect.connectionString))
        //            {
        //                string updateQuery = "UPDATE TransactionsTbl SET Status = 'Processing' WHERE TransactionID = @ID;";
        //                con.Open();
        //                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
        //                {
        //                    updateCommand.Parameters.AddWithValue("@ID", transactionID);
        //                    updateCommand.ExecuteNonQuery();
        //                    QueuingFormBack.instance.lblcounter.Text = "0";
        //                }
        //                MessageBox.Show("Status Updated!");
        //                string def = UserInfo.Empleyado + " Updated the order("+transactionID+") status to Processing ";
        //                addTransactionLog(CustomerName, TotalPrice, transactionID.ToString(), def);
        //                this.Close();
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        //public void Complete()
        //{
        //    try
        //    {
        //        DialogResult result = MessageBox.Show("Proceed Completion?", "Update Status", MessageBoxButtons.YesNo);
        //        if (result == DialogResult.Yes)
        //        {   
        //            using(SqlConnection con = new SqlConnection(Connect.connectionString))
        //            {
        //                string updateQuery = "UPDATE TransactionsTbl SET Status = 'Completed' WHERE TransactionID = @ID;";
        //                con.Open();
        //                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
        //                {
        //                    updateCommand.Parameters.AddWithValue("@ID", transactionID);
        //                    updateCommand.ExecuteNonQuery();
        //                    int queue = int.Parse(QueuingFormBack.instance.lblcounter.Text);
        //                    int addqueue = queue - 1;
        //                    QueuingFormBack.instance.lblcounter.Text = addqueue.ToString();
        //                }
        //                MessageBox.Show("Status Updated!");
        //                string def = UserInfo.Empleyado + " Updated the order (" + transactionID + ") Order Status to Complete ";
        //                addTransactionLog(CustomerName, TotalPrice, transactionID.ToString(), def);
        //                this.Close();
        //            }
        //        }
        //        else
        //        {
        //            //none
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}


        private void UpdateStatus_Load(object sender, EventArgs e)
        {   
            GetInfo();
            label5.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            pictureBox2.Visible = false;
        }




        public void CashOption()
        {   
            if(label9.Text != "Insufficient Payment" && textBox1.Text.Length > 0)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Proceed Payment?", "Payment", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(Connect.connectionString))
                        {
                            string updateQuery = "UPDATE TransactionsTbl SET Status='Receiving',PaymentStatus = 'Paid', PaymentMethod = 'Cash' WHERE TransactionID = @ID;";
                            con.Open();
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@ID", transactionID);
                                updateCommand.ExecuteNonQuery();
                                QueuingFormBack.instance.lblcounter.Text = "0";
                            }
                            MessageBox.Show("Payment Updated!");
                            string def = name + " settled his/her order amounting of " + TotalPrice + " and paid using Cash transaction";
                            addTransactionLog(name, TotalPrice, transactionID.ToString(), def);
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on Cash Payment" + ex.Message);
                }
            }

        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        public void GCashOption()
        {
            if (pictureBox2.Image != null)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Proceed Payment?", "Payment", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(Connect.connectionString))
                        {
                            string updateQuery = "UPDATE TransactionsTbl SET Status='Receiving',PaymentStatus = 'Paid', PaymentMethod = 'GCash', PaymentImage = @Image WHERE TransactionID = @ID;";

                            con.Open();
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                // Convert image to byte array
                                byte[] imageBytes = ImageToByteArray(pictureBox2.Image);
                                // Add parameters to SQL command
                                updateCommand.Parameters.AddWithValue("@ID", transactionID);
                                updateCommand.Parameters.AddWithValue("@Image", imageBytes);

                                updateCommand.ExecuteNonQuery();
                        
                            }
                            MessageBox.Show("Payment Updated!");
                            string def = name + " settled his/her order amounting of " + TotalPrice + " and paid using gcash transaction";
                            addTransactionLog(name,TotalPrice,transactionID.ToString(),def);
                            QueuingFormBack.instance.lblcounter.Text = "0";
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                   MessageBox.Show("Error on Gcash Payment" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Insert Proof of Payment");
            }


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                CashOption();
            }
            else if (radioButton5.Checked)
            {
                GCashOption();
            }
            else
            {
                MessageBox.Show("Please Select Payment Option");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                double price = double.Parse(label7.Text);
                double payment = double.Parse(textBox1.Text.Trim());

                double change = payment - price;
                if (change < 0)
                {
                    label9.Text = "Insufficient Payment";
                }
                else
                {
                    label9.Text = change.ToString();
                }

            }
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox1.Enabled = true;
                textBox1.Text = string.Empty;

                label5.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                pictureBox2.Visible = false;
            }
            else if (radioButton5.Checked)
            {
                textBox1.Text = label7.Text;
                textBox1.Enabled = false;

                label5.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                pictureBox2.Visible = true;


            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            // Set the filter for image files
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            // Set the initial directory to a specific folder (e.g., "C:\\Users\\YourUserName\\Pictures")
            open.InitialDirectory = @"C:\Users\ENZO\OneDrive\Pictures\Camera Roll";

            if (open.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                pictureBox2.Image = new Bitmap(open.FileName);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("microsoft.windows.camera:") { UseShellExecute = true });
        }


        public void addTransactionLog(string CustomerName, string Price, string TId,string definition)
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
    }
}

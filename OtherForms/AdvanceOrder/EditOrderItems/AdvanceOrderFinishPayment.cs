using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
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
using static System.Resources.ResXFileRef;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class AdvanceOrderFinishPayment : Form
    {
        SqlConnection con;
        SqlConnection con2;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        public AdvanceOrderFinishPayment()
        {
            InitializeComponent();
            testConnection();
            setup();
            radioButton1.Checked = true;
            label17.Visible = false;
            pictureBox1.Visible = false;
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string id;
        public void setup()
        {
            label5.Text = AdvanceOrderPaymentDetails.OrderID;
            CustNameLbl.Text = AdvanceOrderPaymentDetails.CustomerName;
            TotalAmountLbl.Text = AdvanceOrderPaymentDetails.TotalAmount;
            DPLbl.Text = AdvanceOrderPaymentDetails.Downpayment;
            PayableLbl.Text = AdvanceOrderPaymentDetails.AmountPayable;
            DiscountLbl.Text = AdvanceOrderPaymentDetails.Discount;
            ItemsLbl.Text = AdvanceOrderPaymentDetails.OrderItems;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_TextChanged(object sender, EventArgs e)
        {

        }
        string paymentmethod;
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true && radioButton2.Checked == false)
            {
                paymentmethod = "Cash";
                textBox1.ReadOnly = false;
                label17.Visible = false;
                pictureBox1.Visible = false;
                textBox1.Text = "0";
            }
            if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                paymentmethod = "GCash";
                textBox1.Text = PayableLbl.Text;

                textBox1.ReadOnly = true;
                label17.Visible = true;
                pictureBox1.Visible = true;
            }
        }

        Image payment;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //call add image form 
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                payment = new Bitmap(open.FileName);
                pictureBox1.Image = payment;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            char firstchar = textBox1.Text.Length > 0 ? textBox1.Text[0]:'0';
       
            if (textBox1.Text.Length > 0 && firstchar != '0') 
            {   
               
                int payable = int.Parse(PayableLbl.Text.Trim());
                int payment = int.Parse(textBox1.Text.Trim());
                int change = payment - payable;

               
                if (change >= 0)
                {
                    label19.Text = change.ToString();
                }
                else
                {
                    label19.Text = "Insufficient Payment";
                }
                
            }
            else
            {
                label19.Text = "Insufficient Payment";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {   if(label19.Text != "Insufficient Payment")
            {
                int change = int.Parse(label19.Text);
                if (change >= 0)
                {
                    paymentImplementDatabase();
                }
                else
                {
                    MessageBox.Show("Please fill payment");
                }
            }
            else
            {
                MessageBox.Show("Please fill payment");
            }

        }
        public void paymentImplementDatabase()
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false) 
            {
                try
                {
                    DialogResult result = MessageBox.Show("Proceed Payment?", "Payment Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    { 
                        string updateQuery = "UPDATE AdvanceOrders SET PaymentStatus = 'Paid', ModeOfPayment = 'Cash',Status='Complete' WHERE OrderID = @ID;";
                        con.Open();
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", label5.Text);

                            updateCommand.ExecuteNonQuery();


                        }
                        con.Close();
                        MessageBox.Show("Order Paid!");
                        int counter = int.Parse(AdvanceOrdersList.instance.todaycounter.Text);
                        counter--;
                        AdvanceOrdersList.instance.todaycounter.Text = counter.ToString();
                        this.Close();

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
            else if(radioButton1.Checked == false &&radioButton2.Checked == true)
            {   
                try
                {
                    Image s_img = pictureBox1.Image;
                    ImageConverter converter = new ImageConverter();
                    DialogResult result = MessageBox.Show("Proceed Payment?", "Payment Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {   
                        string updateQuery = "UPDATE AdvanceOrders SET PaymentStatus = 'Paid', ModeOfPayment = 'GCash',Status='Complete', Image = @Image WHERE OrderID = @ID;";
                        
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                        {
                            con.Open();
                            var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                            updateCommand.Parameters.AddWithValue("@Image", ImageConvert);
                            updateCommand.Parameters.AddWithValue("@ID", label5.Text);

                            updateCommand.ExecuteNonQuery();


                        }
                       
                        MessageBox.Show("Order Paid!");
                        int counter = int.Parse(AdvanceOrdersList.instance.todaycounter.Text);
                        counter--;
                        AdvanceOrdersList.instance.todaycounter.Text = counter.ToString();
                        this.Close();

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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input if it's not a number
            }
        }
    }
}

using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.PaymentConfirmation
{
    public partial class ConfirmPayment : Form
    {
        public ConfirmPayment()
        {
            InitializeComponent();
            StockImages.StockAddImg = pictureBox1.Image;
        }
        Image Imagein;
        Image StockImg = StockImages.StockAddImg;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
               Imagein  = new Bitmap(open.FileName);
               pictureBox1.Image = Imagein;
            }
        }
        
      
        private void Received_Click(object sender, EventArgs e)
        {
            if(Imagein == StockImg)
            {
                MessageBox.Show("Please Insert Image");
            }
            else
            {
                PaymentConfirm.isPaid= true;
                CreateAdvanceOrder.ProofOfPayment = Imagein;
                MessageBox.Show("Payment Recieved!");
                this.Close();
            }
        }
    }
}

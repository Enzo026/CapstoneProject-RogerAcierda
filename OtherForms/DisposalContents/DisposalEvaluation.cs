using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class DisposalEvaluation : Form
    {
        public DisposalEvaluation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisposalEvaluation_Load(object sender, EventArgs e)
        {
             QtyLbl.Text = DisposalInfo.EvQty;
             NameLbl.Text = DisposalInfo.EvName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DisposalInfo.type == "WalkIn")
            {
                string input = textBox1.Text;
                string qtyinput = QtyLbl.Text;
                string oldprice = DisposalInfo.EvPrice.ToString();

                // Declare a numerical variable
                int number;
                int qty;
                int finalqty;
                decimal PrevPrice;
                decimal newPrice = 0;

                
                // Try to parse the input to a double
                if (int.TryParse(input, out number) && int.TryParse(qtyinput, out qty) && decimal.TryParse(oldprice, NumberStyles.Currency, CultureInfo.CurrentCulture, out PrevPrice))
                {
                    // Now you can perform your mathematical computations
                    int result = qty - number; // Example computation
                    finalqty = result;

                    decimal eachprice = PrevPrice / qty;
                    newPrice = eachprice * finalqty;
                    MessageBox.Show("The result is: " + result + " Amounting of" + newPrice + " with a individual price of " + eachprice);
                    MessageBox.Show("The Quantity of " + number + " Will be added to inventory");
                    MessageBox.Show("This Item Will be set to Evaluated");
                }
                else
                {
                    // Handle invalid input
                    MessageBox.Show("Please enter a valid number.");
                }


                // getorderlist();
            }
            else if (DisposalInfo.type == "AdvanceOrder")
            {
               // getAdvanceorderlist();
            }
            else
            {
                MessageBox.Show("Having trouble fetching the disposal order Type");
            }
        }
    }
}

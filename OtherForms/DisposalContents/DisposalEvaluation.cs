using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            if (DisposalInfo.OrderType == "WalkIn" || DisposalInfo.OrderType == "Walk-inTransaction")
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
                    ExecuteRetrieveItemsProcedure(newPrice, "Walk-inTransaction");
                }
                else
                {
                    // Handle invalid input
                    MessageBox.Show("Please enter a valid number.");
                }


                // getorderlist();
            }
            else if (DisposalInfo.OrderType == "AdvanceOrder")
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

                    ExecuteRetrieveItemsProcedure(newPrice, "AdvanceOrder");

                }
                else
                {
                    // Handle invalid input
                    MessageBox.Show("Please enter a valid number.");
                }
            }
            else
            {
                MessageBox.Show("Having trouble fetching the disposal order Type");
            }
        }
        private void ExecuteRetrieveItemsProcedure(decimal calculatedPrice , string OrderType)
        {
            // Define parameters (you can replace these with actual values from your application)
            int inputQty = int.Parse(textBox1.Text);

            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand("RetrieveItemsIndividual_Bouquet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@transactionID", DisposalInfo.ID));
                    command.Parameters.Add(new SqlParameter("@ItemID", DisposalInfo.EvID));
                    command.Parameters.Add(new SqlParameter("@ItemName", DisposalInfo.EvName));
                    command.Parameters.Add(new SqlParameter("@InputQty", inputQty));
                    command.Parameters.Add(new SqlParameter("@EmpName", UserInfo.Empleyado));
                    command.Parameters.Add(new SqlParameter("@EmpID", UserInfo.EmpID));
                    command.Parameters.Add(new SqlParameter("@CalculatedPrice", calculatedPrice));
                    command.Parameters.Add(new SqlParameter("@SalesItemID", DisposalInfo.SalesItemID));
                    command.Parameters.Add(new SqlParameter("@OrderType", OrderType));

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery(); // Execute the stored procedure
                        MessageBox.Show("Item Evaluated");
                        this.Close();
                        DisposalItems.instance.loadingLbl.Visible = true;

                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

    }
}

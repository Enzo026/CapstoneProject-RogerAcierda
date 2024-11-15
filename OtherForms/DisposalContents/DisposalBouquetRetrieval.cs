using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class DisposalBouquetRetrieval : Form
    {
        public DisposalBouquetRetrieval()
        {
            InitializeComponent();
        }

        private void DisposalBouquetRetrieval_Load(object sender, EventArgs e)
        {
            label4.Text = DisposalInfo.EvPrice.ToString();

            string formattedString = DisposalInfo.EvPrice.ToString();

            // Remove currency symbol and commas
            string cleanedString = formattedString.Replace("₱", "").Replace(",", "").Trim();

            // Parse the cleaned string into a decimal
            if (decimal.TryParse(cleanedString, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal result))
            {
                textBox2.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("Unable to parse the value.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "Do you want to proceed? The item will be named as " + textBox1.Text + " and will be priced " + textBox2.Text,
           "Confirm",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            // Handle the response
            if (result == DialogResult.Yes)
            {
                proceedprocess();
                
            }
            else
            {
                Console.WriteLine("You clicked No");
            }
        }
    
        public void proceedprocess()
        {   
            byte[] image = ImageToByteArray(pictureBox1.Image);
            using (SqlConnection connection = new SqlConnection(Connect.connectionString))
            {
                using (SqlCommand command = new SqlCommand("RetrieveItemsCustom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@transactionID", DisposalInfo.ID);
                    command.Parameters.AddWithValue("@ItemID", DisposalInfo.EvID);
                    command.Parameters.AddWithValue("@ItemName", textBox1.Text.Trim());
                    command.Parameters.AddWithValue("@InputQty", 1);
                    command.Parameters.AddWithValue("@EmpName", UserInfo.Empleyado);
                    command.Parameters.AddWithValue("@EmpID", UserInfo.EmpID);
                    command.Parameters.AddWithValue("@Image", (object)image ?? DBNull.Value); // Handle null image
                    command.Parameters.AddWithValue("@NewPrice", textBox2.Text.Trim());
                    command.Parameters.AddWithValue("@CalculatedPrice", textBox2.Text.Trim());
                    command.Parameters.AddWithValue("@SalesItemID", DisposalInfo.SalesItemID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Stored procedure executed successfully.");
                        DisposalItems.instance.loadingLbl.Visible = true;
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }



        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
            {
                return null; // Handle null case
            }

            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}

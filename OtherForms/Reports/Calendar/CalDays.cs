using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.Reports.SalesReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Reports.Calendar
{
    public partial class CalDays : UserControl
    {
        public CalDays()
        {
            InitializeComponent();
           
           

        }
        int q_day,q_month, q_year;

        private void DayLbl_Click(object sender, EventArgs e)
        {
            string givendate = q_month + "/" + q_day + "/" + q_year;
            DateTime parsedDate = DateTime.ParseExact(givendate, "MM/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = parsedDate.ToString("MMM dd, yyyy");
           // MessageBox.Show(formattedDate);
            SalesReport.instance.PickDate.Text = formattedDate;
        }

        private void panel1_Click(object sender, EventArgs e)
        {   
        }

        public void days(int day , int monthnow, int month, int year)
        {   
            q_day = day;
            q_month = month;
            q_year = year;
            DayLbl.Text = day.ToString();

            DateTime date = DateTime.Now;
            string days = date.Day.ToString();

            if (DayLbl.Text == days && month == monthnow)
            {
                lineLbl.Visible = true;
            }
            else
            {
                lineLbl.Visible = false;
            }
            CheckReservation();
        }
        public void CheckReservation()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM AdvanceOrders WHERE Status = 'Active' AND DAY(PickupDate) = @day AND MONTH(PickupDate) = @month AND YEAR(PickupDate) = @year;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        // Add the parameter to the query
                        countCommand.Parameters.AddWithValue("@day", q_day);
                        countCommand.Parameters.AddWithValue("@month", q_month);
                        countCommand.Parameters.AddWithValue("@year", q_year);

                        // Execute the query and get the count
                        int count = (int)countCommand.ExecuteScalar();

                        // Change background color based on count
                        if (count > 0)
                        {
                            panel1.BackColor = Color.PaleGreen; 
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}

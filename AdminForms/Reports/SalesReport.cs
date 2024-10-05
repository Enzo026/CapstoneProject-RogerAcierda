using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.AdvanceOrder;
using Flowershop_Thesis.OtherForms.ProductMaintenance;
using Flowershop_Thesis.OtherForms.Reports;
using Flowershop_Thesis.OtherForms.Reports.Calendar;

namespace Capstone_Flowershop.AdminForms.Reports.SalesReports
{   
    public partial class SalesReport : Form
    {
        int month, year, monthnow;
        public static SalesReport instance;
        public Label uid;
        public SalesReport()
        {
   

            InitializeComponent();
        
            instance = this;
            uid = label2;


            DateTime date = DateTime.Now;
            string datenow = DateTime.Now.Date.ToString();
            label3.Text = date.ToString();
           label2.Text = UserInfo.FullName;

            getDaily();
            getMonthly();
            getYearly();

        }

        public void getDaily()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string day = date.Day.ToString();
                string month = date.Month.ToString();
                string year = date.Year.ToString();
                con.Open();
                string query = "SELECT COALESCE(SUM(TotalPrice), 0) AS daily FROM FinishedTransactionList WHERE DAY(DOC) = @day AND MONTH(DOC) = @month AND YEAR(DOC) = @year;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label6.Text = reader["daily"].ToString();
                    }
                }
            }
        }
        public void getYearly()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string year = date.Year.ToString();
                con.Open();
                string query = "SELECT COALESCE(SUM(TotalPrice), 0) AS yearly FROM FinishedTransactionList WHERE YEAR(DOC) = @year;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label9.Text = reader["yearly"].ToString();
                    }
                }
            }
        }
        public void getMonthly()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                DateTime date = DateTime.Now;
                string month = date.Month.ToString();
                string year = date.Year.ToString();
                con.Open();
                string query = "SELECT COALESCE(SUM(TotalPrice), 0) AS monthly FROM FinishedTransactionList WHERE MONTH(DOC) = @month AND YEAR(DOC) = @year;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        label7.Text = reader["monthly"].ToString();
                    }
                }
            }
        }
        private void SalesReport_Load(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            monthnow = now.Month;
            year = now.Year;
            GetCalendar();
            getTransactions();
            donutChart();
        
        }
        private void button19_Click(object sender, EventArgs e)
        {
            
            if(month >= 12)
            {
                year++;
                month = 1;
            }
            else
            {
                month++;
            }
            GetCalendar();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            
            if(month <= 1)
            {
                year--;
                month = 12;
            }
            else
            {   

                month--;

            }
            GetCalendar();
        }
        public void GetCalendar()
        {   
            flowLayoutPanel2.Controls.Clear();
            
            string monthname = DateTimeFormatInfo.CurrentInfo.MonthNames[month-1];
            label108.Text = monthname + "|" + year;
          
            DateTime StartOfTheMonth = new DateTime(year,month,1);

            int days = DateTime.DaysInMonth(year,month);
            int dayofweek = Convert.ToInt32(StartOfTheMonth.DayOfWeek.ToString("d"))+1;

            for(int i = 1; i < dayofweek; i ++)
            {
                CalBlank blank = new CalBlank();
                flowLayoutPanel2.Controls.Add(blank);
            }
            for (int i = 1; i <= days; i++)
            {
                CalDays dayz = new CalDays();
                dayz.days(i,monthnow,month,year);
                flowLayoutPanel2.Controls.Add(dayz);
            }
        }

        public void getTransactions()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM FinishedTransactionList";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionsList[] inv = new TransactionsList[rowCount];

                        string sqlQuery = "SELECT * FROM FinishedTransactionList";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionsList();
                                    inv[index].LocalID = reader["ID"].ToString().Trim();
                                    inv[index].TransID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Price = reader["TotalPrice"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].Type = reader["TransactionType"].ToString().Trim();
                                    inv[index].Date = reader["DOC"].ToString().Trim();

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BarChart();
        }

        public void donutChart()
        {
            DateTime date = DateTime.Now;
            string month = date.Month.ToString();
            string year = date.Year.ToString();
            label18.Text = new DateTime(1, int.Parse(month), 1).ToString("MMMM");
            decimal Sales  = 0.00m;
            decimal Cancellation = 0.00m;

            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {

                con.Open();
                string query = "SELECT COALESCE(SUM(TotalPrice), 0) AS monthly FROM FinishedTransactionList WHERE MONTH(DOC) = @month AND YEAR(DOC) = @year;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["monthly"] != DBNull.Value && !string.IsNullOrEmpty(reader["monthly"].ToString()))
                        {
                            Sales = decimal.Parse(reader["monthly"].ToString());
                        }
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = "SELECT COALESCE(SUM(TotalPrice), 0) AS monthly FROM CancelledTransaction WHERE MONTH(CancellationDate) = @month AND YEAR(CancellationDate) = @year;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["monthly"] != DBNull.Value && !string.IsNullOrEmpty(reader["monthly"].ToString()))
                        {
                            Cancellation = decimal.Parse(reader["monthly"].ToString());
                        }
                    }
                }
            }

            if(Cancellation != 0)
            {
                int index1 = chart2.Series["VisualComparison"].Points.AddXY(Cancellation.ToString(), Cancellation);
                chart2.Series["VisualComparison"].Points[index1].Color = Color.FromArgb(255, 128, 128);
            }
            if(Sales != 0)
            {
                int index2 = chart2.Series["VisualComparison"].Points.AddXY(Sales.ToString(), Sales);
                chart2.Series["VisualComparison"].Points[index2].Color = Color.LightGreen;
            }
        }
        public void BarChart()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Define your start and end dates (assuming they come from DateTimePickers)
                    DateTime startDate = dateTimePicker1.Value.Date; // Assuming dateTimePicker1 is defined
                    DateTime endDate = dateTimePicker2.Value.Date;   // Assuming dateTimePicker2 is defined

                    // Count query to get the number of rows within the date range
                    string countQuery = "SELECT COUNT(*) FROM FinishedTransactionList WHERE DOC BETWEEN @startDate AND @endDate;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@startDate", startDate);
                        countCommand.Parameters.AddWithValue("@endDate", endDate);

                        int rowCount = (int)countCommand.ExecuteScalar(); // Getting the count of rows

                        // Query to select the DOC and TotalPrice
                        string sqlQuery = "SELECT DOC, TotalPrice FROM FinishedTransactionList WHERE DOC BETWEEN @startDate AND @endDate;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@startDate", startDate);
                            command.Parameters.AddWithValue("@endDate", endDate);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Dictionary to hold the total amount per date
                                Dictionary<DateTime, decimal> dateAmountMap = new Dictionary<DateTime, decimal>();

                                while (reader.Read())
                                {
                                    // Cast DOC directly to DateTime
                                    DateTime date = (DateTime)reader["DOC"];
                                    decimal amount = decimal.Parse(reader["TotalPrice"].ToString());

                                    // If the date already exists, sum the amounts; otherwise, add it to the dictionary
                                    if (dateAmountMap.ContainsKey(date))
                                    {
                                        dateAmountMap[date] += amount;
                                    }
                                    else
                                    {
                                        dateAmountMap[date] = amount;
                                    }
                                }

                                // Now add the aggregated data to the chart
                                foreach (var entry in dateAmountMap)
                                {
                                    // Get month and day
                                    int month = entry.Key.Month;
                                    int day = entry.Key.Day;
                                    string smonth = new DateTime(1, month, 1).ToString("MMMM"); // Get the month name

                                    // Add aggregated data to the chart
                                    chart1.Series["SalesChart"].Points.AddXY($"{smonth} {day}", entry.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List: " + ex.Message);
            }

        }
    }
}

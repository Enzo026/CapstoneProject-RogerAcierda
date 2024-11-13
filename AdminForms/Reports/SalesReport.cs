using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        public Label PickDate;
        bool FormisReady = false;
        public SalesReport()
        {
   

            InitializeComponent();
        
            instance = this;
            uid = label2;
            PickDate = label14;
  

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
            label14.Text= now.ToString("MMM dd, yyyy");
            dateTimePicker4.MaxDate = DateTime.Today;
            dateTimePicker4.Value = DateTime.Today;
            dateTimePicker3.Value = DateTime.Today;
            dateTimePicker3.Enabled = false;
            GetCalendar();
            getTransactions();
            donutChart();
            getTransactions();
            getAdvanceOrders();
            FormisReady = true;
        
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
                DateTime d1 = DateTime.Today;
                DateTime d2 = DateTime.Today;

                TotalInfoDate(d1, d2);
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    // Adjusted to count transactions for today, without time
                    string countQuery = "SELECT count(*) FROM FinishedTransactionList  WHERE DOC >= CONVERT(DATE, GETDATE())  AND DOC < DATEADD(DAY, 1 ,CONVERT(DATE, GETDATE()))";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionsList[] inv = new TransactionsList[rowCount];

                        // Clear previous items to avoid duplicates
                        flowLayoutPanel1.Controls.Clear();

                        if (rowCount > 0)
                        {
                            label43.Visible = false;
                            string sqlQuery = "SELECT * FROM FinishedTransactionList  WHERE DOC >= CONVERT(DATE, GETDATE())  AND DOC < DATEADD(DAY, 1 ,CONVERT(DATE, GETDATE())) order by DOC desc";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < inv.Length)
                                    {
                                        inv[index] = new TransactionsList();

                                        // Use DBNull.Value checks
                                        inv[index].sID = reader["ID"] != DBNull.Value ? reader["ID"].ToString().Trim() : string.Empty;
                                        inv[index].TransID = reader["TransactionID"] != DBNull.Value ? reader["TransactionID"].ToString().Trim() : string.Empty;
                                        inv[index].CustName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString().Trim() : string.Empty;
                                        inv[index].Price = reader["TotalPrice"] != DBNull.Value ? ((decimal)reader["TotalPrice"]).ToString("C") : string.Empty;
                                        inv[index].Employee = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString().Trim() : string.Empty;
                                        inv[index].Type = reader["TransactionType"] != DBNull.Value ? reader["TransactionType"].ToString().Trim() : string.Empty;
                                        inv[index].Date = reader["DOC"] != DBNull.Value ? reader["DOC"].ToString().Trim() : string.Empty;

                                        // Add to FlowLayoutPanel
                                        flowLayoutPanel1.Controls.Add(inv[index]);
                                        index++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            label43.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on displaying Transaction List: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Titles.Clear(); // Clear any titles
            chart1.Legends.Clear(); // Clear any legends
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
            decimal reservation = 0.00m;
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string query = "SELECT COALESCE(SUM(Downpayment), 0) AS monthly FROM AdvanceOrders WHERE MONTH(DateOfReservation) = @month AND YEAR(DateOfReservation) = @year AND Status = 'Cancelled';";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["monthly"] != DBNull.Value && !string.IsNullOrEmpty(reader["monthly"].ToString()))
                        {
                            reservation = decimal.Parse(reader["monthly"].ToString());
                        }
                    }
                }
            }

            if (Cancellation != 0)
            {
                int index1 = chart2.Series["VisualComparison"].Points.AddXY(Cancellation.ToString(), Cancellation);
                chart2.Series["VisualComparison"].Points[index1].Color = Color.FromArgb(255, 128, 128);
            }
            if(Sales != 0)
            {
                int index2 = chart2.Series["VisualComparison"].Points.AddXY(Sales.ToString(), Sales);
                chart2.Series["VisualComparison"].Points[index2].Color = Color.LightGreen;
            }
            if(reservation != 0)
            {
                int index3 = chart2.Series["VisualComparison"].Points.AddXY(reservation.ToString(), Sales);
                chart2.Series["VisualComparison"].Points[index3].Color = Color.LightSkyBlue;
            }
        }

        private void label14_TextChanged(object sender, EventArgs e)
        {   
            if(FormisReady)
            {
                getAdvanceOrders();
            }
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT COUNT(*) FROM FinishedTransactionList Where CustomerName = @name";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@name", textBox1.Text);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        TransactionsList[] inv = new TransactionsList[rowCount];

                        string sqlQuery = "SELECT * FROM FinishedTransactionList  Where CustomerName = @name";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@name", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new TransactionsList();
                                    inv[index].sID = reader["ID"].ToString().Trim();
                                    inv[index].TransID = reader["TransactionID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Price = reader["TotalPrice"].ToString().Trim();
                                    inv[index].Employee = reader["EmployeeName"].ToString().Trim();
                                    inv[index].Type = reader["TransactionType"].ToString().Trim();
                                    inv[index].Date = reader["DOC"].ToString().Trim();

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                                TotalInfo(textBox1.Text.Trim());
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

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                DateTime startDate = dateTimePicker4.Value.Date;
                DateTime endDate = dateTimePicker3.Value.Date;
                TotalInfoDate(startDate, endDate);
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string sqlQuery = "SELECT * FROM FinishedTransactionList WHERE DOC BETWEEN @startDate AND @endDate;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // List to hold all transactions
                            List<TransactionsList> transactions = new List<TransactionsList>();

                            while (reader.Read())
                            {
                                // Extracting fields from the reader
                                DateTime date = (DateTime)reader["DOC"];
                                decimal amount = decimal.Parse(reader["TotalPrice"].ToString());
                                string localID = reader["ID"].ToString();
                                string tID = reader["TransactionID"].ToString();
                                string transactionType = reader["TransactionType"].ToString();
                                string CustName = reader["CustomerName"].ToString();
                                string Emp = reader["EmployeeName"].ToString();

                                // Create a new TransactionsList instance
                                TransactionsList inv = new TransactionsList
                                {
                                    sID = localID, // Use appropriate ID
                                    Price = amount.ToString("C"), // Format as currency
                                    Date = date.ToString("d"), // Format date as desired
                                    CustName = CustName,
                                    Employee = Emp,
                                    Type = transactionType,
                                    TransID = tID,

                                };

                                // Add to the list of transactions
                                transactions.Add(inv);
                            }

                            // Add all controls to the FlowLayoutPanel
                            foreach (var inv in transactions)
                            {
                                flowLayoutPanel1.Controls.Add(inv);
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
        private void TotalInfo(string input)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string sqlQuery = "SELECT COUNT(*) AS TotalOrders, SUM(TotalPrice) AS TotalOrderPrice " +
                                      "FROM FinishedTransactionList " +
                                      "WHERE CustomerName = @name;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@name", input);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get TotalOrders value and handle null or zero
                                int totalOrders = reader["TotalOrders"] != DBNull.Value ? Convert.ToInt32(reader["TotalOrders"]) : 0;
                                decimal totalOrderPrice = reader["TotalOrderPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalOrderPrice"]) : 0;

                                // Update labels
                                label40.Text = totalOrders.ToString(); // TotalOrders label
                                label41.Text = totalOrderPrice.ToString("C"); // Format TotalOrderPrice as currency

                                // Show/hide label43 based on TotalOrders
                                label43.Visible = totalOrders == 0;

                                // If needed, you could check if the totalOrderPrice is zero as well
                                // e.g., label42.Visible = totalOrderPrice == 0; (optional, if you want to handle price too)
                            }
                            else
                            {
                                // If no data found
                                label40.Text = "0";
                                label41.Text = "$0.00"; // or any default value
                                label43.Visible = true; // No transactions
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List bottom info: " + ex.Message);
            }


        }
        private void TotalInfoDate(DateTime d1, DateTime d2)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();

                    string sqlQuery = "SELECT COUNT(*) AS TotalOrders, SUM(TotalPrice) AS TotalOrderPrice " +
                                      "FROM FinishedTransactionList " +
                                      "WHERE DOC BETWEEN @startDate AND @endDate;";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@startDate", d1);
                        command.Parameters.AddWithValue("@endDate", d2);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalOrders = reader["TotalOrders"] != DBNull.Value ? Convert.ToInt32(reader["TotalOrders"]) : 0;
                                decimal totalOrderPrice = reader["TotalOrderPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalOrderPrice"]) : 0;

                                label40.Text = totalOrders.ToString();
                                label41.Text = totalOrderPrice.ToString("C");

                                if (totalOrders > 0)
                                {
                                    label43.Visible = false;
                                }
                                else
                                {
                                    label43.Visible = true;
                                }
                            }
                            else
                            {
                                label40.Text = "0";
                                label41.Text = "$0.00";
                                label43.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List bottom info: " + ex.Message);
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.MinDate = dateTimePicker4.Value; // Update MinDate
            dateTimePicker3.MaxDate = DateTime.Today;
            dateTimePicker3.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value; // Update MinDate
            dateTimePicker2.MaxDate = DateTime.Today;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pie Chart info \n " +
                            "Green is for completed transactions \n " +
                            "Red is for cancelled orders \n " +
                            "Blue is for the reservations that is not attended by the customer or did not complete the whole advance order process","Information");
        }

        public void BarChart()
        {
            try
            {
                // Clear existing series
                chart1.Series.Clear();

                // Create a new series for the sales chart
                Series salesChart = new Series("SalesChart")
                {
                    ChartType = SeriesChartType.Bar, // Set the chart type to Bar
                    Color = Color.Green // Set the color of the bars
                };
                chart1.Series.Add(salesChart); // Add the series to the chart

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
                    }

                    // Query to select the DOC and TotalPrice
                    string sqlQuery = "SELECT DOC, TotalPrice FROM FinishedTransactionList WHERE DOC BETWEEN @startDate AND @endDate order by DOC desc";
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
                                // Ensure DOC is not null before casting
                                if (reader["DOC"] != DBNull.Value)
                                {
                                    DateTime date = (DateTime)reader["DOC"];
                                    decimal amount = reader["TotalPrice"] != DBNull.Value ? decimal.Parse(reader["TotalPrice"].ToString()) : 0;

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
                            }

                            // Now add the aggregated data to the chart
                            foreach (var entry in dateAmountMap)
                            {
                                // Get month and day
                                int month = entry.Key.Month;
                                int day = entry.Key.Day;
                                string smonth = new DateTime(1, month, 1).ToString("MMMM"); // Get the month name

                                // Add aggregated data to the chart
                                salesChart.Points.AddXY($"{smonth} {day}", entry.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on displaying Transaction chart: " + ex.Message);
            }

        }
        public void getAdvanceOrders()
        {
            try
            {
                FormisReady = false;
                flowLayoutPanel3.Controls.Clear();
                DateTime parsedDate = DateTime.ParseExact(label14.Text.Trim(), "MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string formattedDate = parsedDate.ToString("dd/MM/yyyy");
                
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM AdvanceOrders WHERE CONVERT(varchar, PickupDate, 103) = @Date AND Status = 'Active';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@Date", formattedDate);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        CalItems[] inv = new CalItems[rowCount];

                        string sqlQuery = "SELECT * FROM AdvanceOrders WHERE CONVERT(varchar, PickupDate, 103) = @Date AND Status = 'Active';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Date", formattedDate);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new CalItems();
                                    inv[index].LocalID = reader["OrderID"].ToString().Trim();
                                    inv[index].CustName = reader["CustomerName"].ToString().Trim();
                                    inv[index].Price = reader["TotalPrice"].ToString().Trim();

                                    flowLayoutPanel3.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }

                    }
                }
                FormisReady = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Displaying Transaction List :" + ex.Message);
            }

        }
    }
}

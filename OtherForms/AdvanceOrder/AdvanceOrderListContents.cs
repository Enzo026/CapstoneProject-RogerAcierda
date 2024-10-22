using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
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

namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    public partial class AdvanceOrderListContents : UserControl
    {
        public AdvanceOrderListContents()
        {
            InitializeComponent();
 
        }
        public void checkdate(string givendate)
        {
            try
            {
                DateTime pud = DateTime.ParseExact(givendate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                DateTime dateOnly = pud.Date; // This will set the time to 00:00:00

                // Format it as a string if needed
                string formattedDate = dateOnly.ToString("MMM dd,yyyy"); // Format as desired
                PickupDateLbl.Text = formattedDate;

                // Determine today's date
                DateTime today = DateTime.Today;

                // Calculate difference in days
                int daysDifference = (pud - today).Days;

                // Change the label color based on the conditions
                if (daysDifference == 0) // Today
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.Salmon;
                }
                else if (daysDifference == 1) // Tomorrow
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.Yellow;
                }
                else if (daysDifference >= 2 && daysDifference <= 6) // 3 days before pickup date
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.PaleGreen;
                }
                else if (daysDifference >= 7) // 1 week or more
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.LightBlue;
                }
                else if (daysDifference < 0 ) // 1 week or more
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    PickupDateLbl.BackColor = System.Drawing.Color.White; // Default color
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on retrieving date " + ex.Message);
            }

        }
        #region AdvanceOrderListItems
        private int transactionID;
        private string name;
        private string pickupdate;
        private string price;
        private string DP;
        private string CancelPeriod;
        private string type;
        private int qty;



        [Category("ListItems")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }
        [Category("ListItems")]
        public string Name
        {
            get { return name; }
            set { name = value; CustomerNameLbl.Text = value.ToString(); }
        }
        [Category("ListItems")]
        public string Downpayment
        {
            get { return DP; }
            set { DP = value; }
        }
        [Category("ListItems")]
        public string Cancelperiod
        {
            get { return CancelPeriod; }
            set { CancelPeriod = value; }
        }
        public string OrderPickupDate
        {
            get { return pickupdate; }
            set { pickupdate = value; PickupDateLbl.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public string Price
        {
            get { return price; }
            set { price = value; TotalAmountLbl.Text = value.ToString(); }
        }

        [Category("QueueList")]
        public string Type
        {
            get { return type; }
            set { type = value; OrderTypeLbl.Text = value; }
        }

        public int OrderQuantity
        {
            get { return qty; }
            set { qty = value; }
        }
        #endregion


        private void label73_Click(object sender, EventArgs e)
        {


        }

        private void AdvanceOrderListContents_Load(object sender, EventArgs e)
        {
            checkdate(pickupdate);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeIds.TransactionLogID = transactionID.ToString();
            string completedate = "null";
            string cancelperioddate = "null";
            DateTime newdate;
            bool success = DateTime.TryParse(pickupdate, out newdate);
            if (success)
            {
                string day = newdate.Day.ToString();
                string month = newdate.Month.ToString();
                string year = newdate.Year.ToString();
                completedate = month + "/" + day + "/" + year;

                AdvanceOrdersList.instance.PUD.Text = completedate;


                if (type == "Events")
                {
                    DateTime cancelperiod = newdate.AddDays(-7);
                    string cday = cancelperiod.Day.ToString();
                    string cmonth = cancelperiod.Month.ToString();
                    string cyear = cancelperiod.Year.ToString();
                    cancelperioddate = cmonth + "/" + cday + "/" + cyear;

                }
                else if (type == "Advance Order")
                {
                    DateTime cancelperiod = newdate.AddDays(-7);
                    string cday = cancelperiod.Day.ToString();
                    string cmonth = cancelperiod.Month.ToString();
                    string cyear = cancelperiod.Year.ToString();
                    cancelperioddate = cmonth + "/" + cday + "/" + cyear;

                }
                else
                {
                    MessageBox.Show("Having trouble on cancellation date;");
                }

            }
            else
            {
                MessageBox.Show("Having trouble on pickupdate;");
            }
            AdvanceOrdersList.instance.CancelPeriod.Text = cancelperioddate;
            AdvanceOrdersList.instance.OId.Text = transactionID.ToString();
            AdvanceOrdersList.instance.Price.Text = price;
            AdvanceOrdersList.instance.DP.Text = DP;
            //  AdvanceOrdersList.instance.CancelPeriod.Text = CancelPeriod;
            AdvanceOrdersList.instance.CustName.Text = name;
            //   AdvanceOrdersList.instance.OrderQty.Text = qty.ToString();
        }
    }
}

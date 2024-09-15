using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            set { transactionID = value; CustomerNameLbl.Text = value.ToString(); }
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
            set { pickupdate = value; PickupDate.Text = value.ToString(); }
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
            string completedate= "null";
            string cancelperioddate = "null";
            DateTime newdate;
            bool success = DateTime.TryParse(pickupdate, out newdate);
            if (success)
            {   
                string day = newdate.Day.ToString();
                string month = newdate.Month.ToString();
                string year = newdate.Year.ToString();
                completedate = month+ "/" +day + "/"+year;

                AdvanceOrdersList.instance.PUD.Text = completedate;


                 if(type == "Events")
                {
                    DateTime cancelperiod = newdate.AddDays(-30);
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

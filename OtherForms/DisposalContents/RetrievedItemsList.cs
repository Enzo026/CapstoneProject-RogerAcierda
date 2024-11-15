using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class RetrievedItemsList : UserControl
    {
        public RetrievedItemsList()
        {
            InitializeComponent();
        }

        #region FinishedQueue
        private string Id, ItemName, Quantity,  Employee, Date, Price;

        [Category("ActivityList")]
        public string id //ID
        {
            get { return Id; }
            set { Id = value; IdLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string price 
        {
            get { return Price; }
            set { Price = value; if (decimal.TryParse(value, out decimal parsedPrice))
                {
                    PriceLbl.Text = $"₱{parsedPrice:N2}"; // Use N2 for two decimal places
                }
                else
                {
                    // Handle invalid input
                    PriceLbl.Text = "₱0.00"; // Default value if input is invalid
                }
            }
        }
        [Category("ActivityList")]
        public string date //ID
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value.ToString(); }
        }

        [Category("ActivityList")]
        public string ItmName //Customer Name
        {
            get { return ItemName; }
            set { ItemName = value; ItemNameLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string qty
        {
            get { return Quantity; }
            set { Quantity = value; QtyLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string Emp
        {
            get { return Employee; }
            set { Employee = value; EmpLbl.Text = value.ToString(); }
        }

        #endregion
    }
}

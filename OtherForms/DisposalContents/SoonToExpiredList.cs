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

namespace Flowershop_Thesis.OtherForms.DisposalContents
{
    public partial class SoonToExpiredList : UserControl
    {
        public SoonToExpiredList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string Qty, Name, Date, Type;

        [Category("ActivityList")]
        public string qty //ID
        {
            get { return Qty; }
            set { Qty = value; QtyLbl.Text = value.ToString(); }
        }

        private void SoonToExpiredList_Load(object sender, EventArgs e)
        {
            DateTime expirationDate;
            if (DateTime.TryParseExact(Date, "MMM dd, yyyy", null, System.Globalization.DateTimeStyles.None, out expirationDate))
            {
                DateTime today = DateTime.Today;
                int daysUntilExpiration = (expirationDate - today).Days;

                if (daysUntilExpiration <= 1)
                {
                    DateLbl.BackColor = Color.IndianRed;
                }
                else if (daysUntilExpiration <= 3)
                {
                    DateLbl.BackColor = Color.SandyBrown;
                   
                }
                else if (daysUntilExpiration <= 7)
                {
                    DateLbl.BackColor = Color.LightGreen;
                }
                else
                {
                    DateLbl.BackColor = Color.PaleTurquoise;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid date in the format 'MMM dd, yyyy'.");
            }
        }

        [Category("ActivityList")]
        public string name //Customer Name
        {
            get { return Name; }
            set { Name = value; NameLbl.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string date
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value.ToString(); }
        }

        [Category("ActivityList")]
        public string type
        {
            get { return Type; }
            set { Type = value; TypeLbl.Text = value.ToString(); }
        }

        #endregion
    }
}

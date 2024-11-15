using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.StockAdjustments
{
    public partial class SA_ActivityLogs : UserControl
    {
        public SA_ActivityLogs()
        {
            InitializeComponent();
        }
        #region Myregion
        private string  EmployeeName, Date, Desc;

        [Category("ItemList")]
        public string EmpName
        {
            get { return EmployeeName; }
            set { EmployeeName = value; NameLbl.Text = value; }
        }
        [Category("ItemList")]
        public string date
        {
            get { return Date; }
            set { Date = value; DateLbl.Text = value; }
        }
        [Category("ItemList")]
        public string desc
        {
            get { return Desc; }
            set { Desc = value; DescLbl.Text = value; }
        }

        #endregion
    }
}

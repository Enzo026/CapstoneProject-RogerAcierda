using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.HistoryLogs
{
    public partial class ActivityLogsList : UserControl
    {
        public ActivityLogsList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string Id,Action,Desc,EmpName,Date;

        [Category("ActivityList")]
        public string id
        {
            get { return Id; }
            set { Id = value;  label8.Text = value.ToString(); }
        }

        [Category("ActivityList")]
        public string action
        {
            get { return Action; }
            set { Action = value; label9.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string HeadLine
        {
            get { return Desc; }
            set { Desc = value; label10.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string name
        {
            get { return EmpName; }
            set { EmpName = value;  label12.Text = value.ToString(); }
        }
        [Category("ActivityList")]
        public string date
        {
            get { return Date; }
            set { Date = value; label11.Text = value.ToString(); }
        }

        #endregion
    }
}

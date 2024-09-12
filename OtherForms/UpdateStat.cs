using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms
{
    public partial class UpdateStat : UserControl
    {
        public UpdateStat()
        {
            InitializeComponent();
        }
        #region UpdateStatus
        private string name;
        private int transactionID;
        private string Status;




        [Category("QueueList")]
        public int transID
        {
            get { return transactionID; }
            set { transactionID = value; TransID.Text = value.ToString(); }
        }
        [Category("QueueList")]
        public string status
        {
            get { return Status; }
            set { Status = value;
                if (Status.Equals("Processing"))
                {
                    radioButton1.Checked = true;
                }
                else if (Status.Equals("Payment"))
                {
                    radioButton2.Checked = true;
                }
                else if (Status.Equals("Complete"))
                {
                    radioButton3.Checked = true;
                }
                else
                {
                    MessageBox.Show("Unknown Status please verify");
                }
            
            
            }
        }

        [Category("QueueList")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

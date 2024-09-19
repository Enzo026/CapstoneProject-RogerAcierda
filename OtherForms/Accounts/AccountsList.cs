using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Accounts
{
    public partial class AccountsList : UserControl
    {
        public AccountsList()
        {
            InitializeComponent();
        }
        #region Myregion
        private string AccountID;
        private string AccountName;
        private string AccountType;
        private string AccountUsername;
        private string AccountContactNum;
        private Image AccountImage;

        [Category("ItemList")]
        public string AccID
        {
            get { return AccountID; }
            set { AccountID = value; }
        }
        [Category("ItemList")]
        public string AccUsername
        {
            get { return AccountUsername; }
            set { AccountUsername = value; label1.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string AccName
        {
            get { return AccountName; }
            set { AccountName = value; label8.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string AccType
        {
            get { return AccountType; }
            set { AccountType = value; label25.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string AccContact
        {
            get { return AccountContactNum; }
            set { AccountContactNum = value; label7.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return AccountImage; }
            set { AccountImage = value; pictureBox7.Image = value; }
        }
        #endregion

    }
}

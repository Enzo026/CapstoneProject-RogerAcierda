using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.AccountsMaintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        private string AccountRole;
        private string AccountStatus;
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
        public string AccRole
        {
            get { return AccountRole; }
            set { AccountRole = value; }
        }
        [Category("ItemList")]
        public string AccStatus
        {
            get { return AccountStatus; }
            set { AccountStatus = value; }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return AccountImage; }
            set { AccountImage = value; pictureBox7.Image = value; }
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            AccountMaintenance.instance.AccID.Text = AccountID;
            AccountMaintenance.instance.AccName.Text = AccountName;
            AccountMaintenance.instance.AccUsername.Text = AccountUsername;
            AccountMaintenance.instance.AccNum.Text = AccountContactNum;
            AccountMaintenance.instance.AccRole.Text = AccountRole;
            AccountMaintenance.instance.AccStatus.Text = AccountStatus;
            AccountMaintenance.instance.AccImg.Image = AccountImage;
        }
    }
}

using Capstone_Flowershop;
using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class Admin_SupplierList : UserControl
    {
        public Admin_SupplierList()
        {
            InitializeComponent();
        }
        #region Myregion
        private string SupplierID;
        private string SupplierName;
        private string SupplierType;
        private string SupplierContactNum;
        private string SupplierAddress;
        private Image SupplierImage;

        [Category("ItemList")]
        public string SuppID
        {
            get { return SupplierID; }
            set { SupplierID = value; SuppIDLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string Suppname
        {
            get { return SupplierName; }
            set { SupplierName = value; SuppName.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppType
        {
            get { return SupplierType; }
            set { SupplierType = value; SuppTypeLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppContact
        {
            get { return SupplierContactNum; }
            set { SupplierContactNum = value;}
        }
        [Category("ItemList")]
        public string SuppAdd
        {
            get { return SupplierAddress; }
            set { SupplierAddress = value; }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return SupplierImage; }
            set { SupplierImage = value; }
        }

        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            Admin_Supplier.instance.SuppName.Text = SupplierName;
            Admin_Supplier.instance.SuppContactNo.Text = SupplierContactNum;
            Admin_Supplier.instance.SuppType.Text = SupplierType;
            Admin_Supplier.instance.SuppAddress.Text = SupplierAddress;
            Admin_Supplier.instance.SuppImg.Image = SupplierImage;

            ChangeIds.SupplierId = SupplierID;
        }
    }
}

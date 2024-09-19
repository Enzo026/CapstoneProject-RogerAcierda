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
    public partial class Inv_Supplier : UserControl
    {
        public Inv_Supplier()
        {
            InitializeComponent();
        }

        #region Myregion
        private int SupplierID;
        private string SupplierName;
        private string SupplierType;
        private string SupplierAddress;
        private string SupplierContact;

        private Image SupplierImage;

        [Category("ItemList")]
        public int SuppID
        {
            get { return SupplierID; }
            set { SupplierID = value; }
        }
        [Category("ItemList")]
        public string Suppname
        {
            get { return SupplierName; }
            set { SupplierName = value;SuppNameLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppContact
        {
            get { return SupplierContact; }
            set { SupplierContact = value; ContactNumLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppType
        {
            get { return SupplierType; }
            set { SupplierType = value; SuppTypeLbl.Text = value.ToString(); }
        }

        [Category("ItemList")]
        public string SuppAddress
        {
            get { return SupplierAddress; }
            set { SupplierAddress = value; SuppAddressLbl.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public Image Img
        {
            get { return SupplierImage; }
            set { SupplierImage = value; pictureBox1.Image = value; }
        }

        #endregion
    }
}

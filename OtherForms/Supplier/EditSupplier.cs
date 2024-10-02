using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.OtherForms.Supplier
{
    public partial class EditSupplier : Form
    {


 
        public EditSupplier()
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
            set { SupplierID = value; label1.Text = "Edit Supplier ID:" + value.ToString(); }
        }
        [Category("ItemList")]
        public string Suppname
        {
            get { return SupplierName; }
            set { SupplierName = value; SuppNameTxtBox.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppType
        {
            get { return SupplierType; }
            set { SupplierType = value; SuppTypeInput.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppContact
        {
            get { return SupplierContactNum; }
            set { SupplierContactNum = value; ContactNumTxtBox.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public string SuppAdd
        {
            get { return SupplierAddress; }
            set { SupplierAddress = value; AddressTxtBox.Text = value.ToString(); }
        }
        [Category("ItemList")]
        public Image img
        {
            get { return SupplierImage; }
            set { SupplierImage = value; Image.Image = value; }
        }

        #endregion

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close ();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image.Image = new Bitmap(open.FileName);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProceedChange();
        }
        public void ProceedChange()
        {
            try
            {
                
                DialogResult result = MessageBox.Show("You are about to proceed Changes on this supplier", "Proceed Edit Information", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        int numId;

                        string countQuery = "Select count(*) from Supplier where SupplierID = @ID";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {
                            con.Open();
                            countCommand.Parameters.AddWithValue("@ID", SupplierID);
                            numId = (int)countCommand.ExecuteScalar();
                            con.Close();
                        }

                        if (numId == 1)
                        {
                            if (Image.Image != SupplierImage)
                            {
                                editinfowithImage();
                            }
                            else
                            {
                                editinfo();
                            }
                        }
                        else if (numId > 1)
                        {
                            MessageBox.Show("There are multiple Users in this ID");
                        }
                        else
                        {
                            MessageBox.Show("No Account Found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void editinfowithImage()
        {
           using(SqlConnection con  = new SqlConnection(Connect.connectionString))
            {
                string updateQuery = "UPDATE Supplier SET SupplierName = @SuppName , ContactNumber = @Num , SupplierType = @Type, SupplierAddress = @add, Image = @Img WHERE SupplierID = @ID;";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    updateCommand.Parameters.AddWithValue("@ID", SupplierID);
                    updateCommand.Parameters.AddWithValue("@SuppName", SuppNameTxtBox.Text);
                    updateCommand.Parameters.AddWithValue("@Num", ContactNumTxtBox.Text);
                    updateCommand.Parameters.AddWithValue("@Type", SuppTypeInput.Text);
                    updateCommand.Parameters.AddWithValue("@add", AddressTxtBox.Text);


                    //Image s_img = Image.Image;
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(Image.Image, typeof(byte[]));
                    updateCommand.Parameters.AddWithValue("@Img", ImageConvert);

                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Supplier Info Edited!");
                    Admin_Supplier.instance.SuppName.Text = "Null";
                    this.Close();
                }
            }

        
        }
        public void editinfo()
        {   using(SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                string updateQuery = "UPDATE Supplier SET SupplierName = @SuppName , ContactNumber = @Num , SupplierType = @Type, SupplierAddress = @add WHERE SupplierID = @ID;";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    updateCommand.Parameters.AddWithValue("@ID", SupplierID);
                    updateCommand.Parameters.AddWithValue("@SuppName", SuppNameTxtBox.Text);
                    updateCommand.Parameters.AddWithValue("@Num", ContactNumTxtBox.Text);
                    updateCommand.Parameters.AddWithValue("@Type", SuppTypeInput.Text);
                    updateCommand.Parameters.AddWithValue("@add", AddressTxtBox.Text);

                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Supplier Info Edited!");
                    Admin_Supplier.instance.SuppName.Text = "Null";
                    this.Close();
                }
            }

          
        }
    }
}

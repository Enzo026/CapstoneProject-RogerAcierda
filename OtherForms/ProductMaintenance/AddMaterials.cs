using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class AddMaterials : Form
    {

        public AddMaterials()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
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
            addMaterials();
        }
        public void addMaterials()
        {   
            using(SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                string date = DateTime.Now.ToString("MM-dd-yyyy");

                Image s_img = Image.Image;
                ImageConverter converter = new ImageConverter();
                try
                {
                    var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Materials(ItemName,ItemType,ItemColor,Price,UnitPrice,Size , Usage , UsageQuantity, ItemQuantity,Supplier,SuppliedDate, ItemStatus,Image)Values" +
                                "(@Name,@Type,@Color,@Price,@UnitPrice,@Size,@Usage,@UsageQuantity,@ItemQuantity,@Supplier,getdate(),@Status,@Image);", con);

                    //varchar
                    cmd.Parameters.AddWithValue("@Name", Name.Text);
                    cmd.Parameters.AddWithValue("@Type", Type.Text);
                    cmd.Parameters.AddWithValue("@Color", Color.Text);
                    cmd.Parameters.AddWithValue("@Size", Size.Text);
                    cmd.Parameters.AddWithValue("@Supplier", Supplier.Text);
                    cmd.Parameters.AddWithValue("@Status", "Available");

                    //int
                    cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(this.Price.Text));
                    cmd.Parameters.AddWithValue("@UnitPrice", Convert.ToInt32(this.UnitPrice.Text));
                    cmd.Parameters.AddWithValue("@Usage", Convert.ToInt32(this.UsageQty.Text));
                    cmd.Parameters.AddWithValue("@UsageQuantity", Convert.ToInt32(this.UsageQty.Text));
                    cmd.Parameters.AddWithValue("@ItemQuantity", Convert.ToInt32(this.Qty.Text));

                    cmd.Parameters.AddWithValue("@Image", ImageConvert);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Added Successfully!");
                    addActivityLog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddingItem Failed!" + " : " + ex);
                }
            }
       
        }
        public void addActivityLog()
        {
            try
            {   
                using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Added New Item(Material)");
                    cmd.Parameters.AddWithValue("@Definition", "NotGiven");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", "0");
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Add New Item Material to Inventory named as " + Name.Text);


                    cmd.ExecuteNonQuery();

                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }
    }
}

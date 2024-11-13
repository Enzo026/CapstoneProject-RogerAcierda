using Capstone_Flowershop;
using Flowershop_Thesis.AdminForms.ProductMaintenance.Supplier;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Flowershop_Thesis;
using Capstone_Flowershop.AdminForms.ProductMaintenance;
namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            LoadSuppliers();
        }
        public void addflower()
        {   
            using(SqlConnection con =  new SqlConnection(Connect.connectionString))
            {
                string date = DateTime.Now.ToString("MM-dd-yyyy");

                Image s_img = Image.Image;
                ImageConverter converter = new ImageConverter();
                try
                {
                    var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ItemInventory(ItemName,ItemQuantity,ItemType,ItemColor,LifeSpan,SuppliedDate,Supplier,ItemDescription,Price,ItemImage,ItemStatus)Values" +
                                "(@Name,@Qty,@Type,@Color,@LifeSpan,getdate(),@Supplier,@Desc,@RSP,@ItemImage,'Available');", con);
                    cmd.Parameters.AddWithValue("@Name", Name.Text);
                    cmd.Parameters.AddWithValue("@Qty", 0);
                    cmd.Parameters.AddWithValue("@Type", "Individual");
                    cmd.Parameters.AddWithValue("@Color", Color.Text);
                    cmd.Parameters.AddWithValue("@LifeSpan", Convert.ToInt32(this.UsageQty.Text));
                    cmd.Parameters.AddWithValue("@Supplier", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Desc", UnitPrice.Text);
                    cmd.Parameters.AddWithValue("@RSP", Convert.ToDecimal(this.Price.Text));
                    cmd.Parameters.AddWithValue("@ItemImage", ImageConvert);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Added Successfully!");
                    addActivityLog();
                    ProductMaintenanceFrm.instance.refresh.Visible = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddingItem Failed!" + " : " + ex);
                }
            }
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checker();
            if(CanProceed == true)
            {
                addflower();
            }
         
        }
        bool CanProceed =false;
        private void checker()
        {
            if (Name.Text.Length <= 0 || UsageQty.Text.Length <= 0 ||
            Color.Text.Length <= 0 || Price.Text.Length <= 0 ||
                UnitPrice.Text.Length <= 0 || comboBox1.SelectedIndex == -1||
                Image.Image == null)
            {
                MessageBox.Show("Please Fill All the Needed Information");
            }
            else
            {
                CanProceed = true;
            }
        }
        public void addActivityLog()
        {
            try
            {   using(SqlConnection con  =  new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Added New Item(Flower)");
                    cmd.Parameters.AddWithValue("@Definition", "NotGiven");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", "0");
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Add New Item Flower to Inventory named as " + Name.Text);


                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
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
        private void LoadSuppliers()
        {
            try
            {
                // Clear existing items in the ComboBox
                comboBox1.Items.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT SupplierName FROM Supplier where status = 'Active' and SupplierType = 'Flowers' or SupplierType = 'Flowers and Materials'";

                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["SupplierName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading suppliers: " + e.Message);
            }
        }
        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

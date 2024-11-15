using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.ProductMaintenance;
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

namespace Flowershop_Thesis.OtherForms.ProductMaintenance
{
    public partial class AddMaterials : Form
    {

        public AddMaterials()
        {
            InitializeComponent();
            LoadSuppliers();
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
                    cmd.Parameters.AddWithValue("@Type", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Color", Color.Text);
                    cmd.Parameters.AddWithValue("@Size", Size.Text);
                    cmd.Parameters.AddWithValue("@Supplier", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Status", "Available");

                    //int
                    cmd.Parameters.AddWithValue("@Price", 0);
                    cmd.Parameters.AddWithValue("@UnitPrice", Convert.ToInt32(this.UnitPrice.Text));
                    cmd.Parameters.AddWithValue("@Usage", 2);
                    cmd.Parameters.AddWithValue("@UsageQuantity", Convert.ToInt32(this.UsageQty.Text));
                    cmd.Parameters.AddWithValue("@ItemQuantity", 0);

                    cmd.Parameters.AddWithValue("@Image", ImageConvert);

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

        private void LoadSuppliers()
        {
            try
            {
                // Clear existing items in the ComboBox
                comboBox1.Items.Clear();

                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT SupplierName FROM Supplier where status = 'Active' and SupplierType = 'Materials' or SupplierType = 'Flowers and Materials'";

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
    }
}

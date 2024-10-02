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
    public partial class EditMaterials : Form
    {



        //default item information
        Image oldImg;
        string ItemName;
        string Type;
        string Color;
        string Supplier;
        string Price;
        string PricePerUsage;
      
        public EditMaterials()
        {
            InitializeComponent();
            DisplayInfo();
            setup();
        }
        void setup()
        {   
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            comboBox1.Enabled = false;
            pictureBox1.Enabled = false;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked)
            {
                this.Close();
            }
            else
            {
                DialogResult = MessageBox.Show("You are about to cancel all changes", "Edit Cancellation Confirmation", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked)
            {
                this.Close();
            }
            else
            {
                DialogResult = MessageBox.Show("You are about to cancel all changes", "Edit Cancellation Confirmation", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }

            
        }
    
        public void EditItem()
        {
            if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked)
            {
                MessageBox.Show("No changes to be done please click the checkbox on the information you want to change");
            }
            else
            {
                try
                {
                    //ItemnName
                    if (checkBox1.Checked && textBox1.Text.Length > 0)
                    {
                        ChangeItemName();
                    }
                    else if (checkBox1.Checked && textBox1.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Item Name");
                    }
                    //ItemType
                    if (checkBox2.Checked && comboBox1.Text.Length > 0)
                    {
                        ChangeItemType();
                    }
                    else if (checkBox2.Checked && comboBox1.Text == "Select Item Type   >>")

                    {
                        MessageBox.Show("Please select a ItemType");
                    }
                    //ItemColor
                    if (checkBox3.Checked && textBox3.Text.Length > 0)
                    {
                        ChangeItemColor();
                    }
                    else if (checkBox3.Checked && textBox3.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Item Color");
                    }
                    //ItemSupplier
                    if (checkBox4.Checked && textBox4.Text.Length > 0)
                    {
                        ChangeItemSupplier();
                    }
                    else if (checkBox4.Checked && textBox4.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Supplier");
                    }
                    //ItemPrice
                    if (checkBox5.Checked && textBox5.Text.Length > 0)
                    {
                        ChangeItemPrice();
                    }
                    else if (checkBox5.Checked && textBox5.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Price");
                    }
                    //ItemPrice
                    if (checkBox6.Checked && textBox6.Text.Length > 0)
                    {
                        ChangeUnitPrice();
                    }
                    else if (checkBox6.Checked && textBox6.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Price per Usage");
                    }
                    //ItemImage
                    if (checkBox7.Checked && pictureBox1.Image != oldImg)
                    {
                        ChangeItemPrice();
                    }
                    else if (checkBox7.Checked && pictureBox1.Image != oldImg)
                    {
                        MessageBox.Show("Image is not changed please input another");
                    }
                    //add to activity logs
                    addActivityLog();
                    this.Close();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error on editing items:" + ex.Message);                
                }
                

            }
        }
        public void addActivityLog()
        {
            try
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO HistoryLogs(Title,Definition,Employee,EmployeeID,Date,Type,ReferenceID,HeadLine)Values" +
                                "(@Title,@Definition,@Employee,@EmployeeID,getdate(),@Type,@RefID,@HeadLine);", con);
                    cmd.Parameters.AddWithValue("@Title", "Item Material Edited");
                    cmd.Parameters.AddWithValue("@Definition", "NotGiven");
                    cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                    cmd.Parameters.AddWithValue("@EmployeeID", UserInfo.EmpID);
                    cmd.Parameters.AddWithValue("@Type", "ActivityLog");
                    cmd.Parameters.AddWithValue("@RefID", ChangeIds.ItemID);
                    cmd.Parameters.AddWithValue("@HeadLine", UserInfo.Empleyado + " Edited Item Material Information on " + ItemName);


                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adding Activity Failed!" + " : " + ex);
            }
        }
        public void DisplayInfo()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = " Select * from Materials where ItemID = @ID";
                        //string updateQuery = "UPDATE UserAccounts SET Username = @input WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            SqlDataReader reader = updateCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                ItemName= reader["ItemName"].ToString().Trim();
                                Type = reader["ItemType"].ToString().Trim();
                                Color = reader["ItemColor"].ToString().Trim();
                                Supplier = reader["Supplier"].ToString().Trim();
                                Price = reader["Price"].ToString().Trim();
                                PricePerUsage = reader["UnitPrice"].ToString().Trim();

                                textBox1.Text = reader["ItemName"].ToString().Trim();
                                comboBox1.Text = reader["ItemType"].ToString().Trim();
                                textBox3.Text = reader["ItemColor"].ToString().Trim();
                                textBox4.Text = reader["Supplier"].ToString().Trim();
                                textBox5.Text = reader["Price"].ToString().Trim();
                                textBox6.Text = reader["UnitPrice"].ToString().Trim();
                                if (reader["Image"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["Image"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                        oldImg = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
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
            catch (Exception ex)
            {
                MessageBox.Show("Error on retrieving account :" + ex.Message);
            }
        }
        public void ChangeItemName()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE Materials SET ItemName = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox1.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("ItemName Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Item Name :" + ex.Message);
            }
        }
        public void ChangeItemType()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE Materials SET ItemType = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", comboBox1.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("ItemType Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing ItemType :" + ex.Message);
            }
        }
        public void ChangeItemColor()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                        

                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE Materials SET ItemColor = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox3.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("ItemColor Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Item Color :" + ex.Message);
            }
        }
        public void ChangeItemSupplier()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE Materials SET Supplier = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox4.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item Supplier Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Supplier :" + ex.Message);
            }
        }
        public void ChangeItemPrice()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {

                        string updateQuery = "UPDATE Materials SET Price = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox5.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item Price Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Price :" + ex.Message);
            }
        }
        public void ChangeUnitPrice()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {   

                        string updateQuery = "UPDATE Materials SET UnitPrice = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox6.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item Price per Usage Changed!");
                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Item Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Price Per Usage :" + ex.Message);
            }
        }
        public void ChangeImage()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                {

                    string countQuery = "Select count(*) from Materials where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(Connect.connectionString))
                    {
                        Image s_img = pictureBox1.Image;
                        ImageConverter converter = new ImageConverter();
                        var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                        string updateQuery = "UPDATE Materials SET Image = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {

                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", ImageConvert);
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item Image Changed!");


                        }
                    }
                }
                else if (numId > 1)
                {
                    MessageBox.Show("There are multiple Items in this ID");
                }
                else
                {
                    MessageBox.Show("No Account Found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Changing Item Image :" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.Clear();
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Text = ItemName;
                textBox1.Enabled = false;
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                comboBox1.Text = "Select Item Type   >>" ;
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Text = Type;
                comboBox1.Enabled = false;
            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox3.Clear();
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Text = Color;
                textBox3.Enabled = false;
            }
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox4.Clear();
                textBox4.Enabled = true;
            }
            else
            {
                textBox1.Text = Supplier;
                textBox4.Enabled = false;
            }
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox5.Clear();
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Text = Price;
                textBox5.Enabled = false;
            }
        }

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox6.Clear();
                textBox6.Enabled = true;
            }
            else
            {
                textBox6.Text = PricePerUsage;
                textBox6.Enabled = false;
            }
        }

        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                pictureBox1.Enabled = true;
                button1.Visible = true;
            }
            else
            {
                pictureBox1.Image = oldImg;
                pictureBox1.Enabled = false;
                button1.Visible = false;
            }
        }
    }
}

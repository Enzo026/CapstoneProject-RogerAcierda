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
    public partial class EditFlower : Form
    {
        SqlConnection con;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        SqlDataAdapter sda;
        string connectionString;
        public void testConnection()
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\"));
            string databaseFilePath = Path.Combine(parentDirectory, "FlowershopSystemDB.mdf");

            // Build the connection string with explicit pooling parameters
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Initial Catalog=try;Integrated Security=True;Pooling=true;Max Pool Size=100;Min Pool Size=5;Connection Lifetime=600;";


            // Use the connection string to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    con = new SqlConnection(connectionString);

                    // Perform database operations here

                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions
                    MessageBox.Show("SQL error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            } // Connection is automatically closed and returned to the pool here
        }

        //default item information
        Image oldImg;
        string ItemName;
        string Type;
        string Color;
        string Supplier;
        string Price;
        string lifespan;
        string desc;
        public EditFlower()
        {
            InitializeComponent();
            testConnection();
            DisplayInfo();
            setup();
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
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "image Files(*.jpg; *.jpeg; *png; )|*.jpg; *.jpeg; *png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            changeItems();
        }
        #region methods
        void setup()
        {
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            comboBox1.Enabled = false;
            pictureBox1.Enabled = false;
            textBox2.Enabled= false;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            button1.Visible = false;
        }
        public void changeItems()
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked && !checkBox8.Checked)
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
                    if (checkBox3.Checked && textBox2.Text.Length > 0)
                    {
                        ChangeItemColor();
                    }
                    else if (checkBox3.Checked && textBox2.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Item Color");
                    }

                    //ItemSupplier
                    if (checkBox5.Checked && textBox4.Text.Length > 0)
                    {
                        ChangeSupplier();
                    }
                    else if (checkBox5.Checked && textBox4.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Supplier");
                    }

                    //ItemPrice
                    if (checkBox6.Checked && textBox5.Text.Length > 0)
                    {
                        ChangePrice();
                    }
                    else if (checkBox6.Checked && textBox5.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Price");
                    }
                    //lifespan
                    if (checkBox4.Checked && textBox3.Text.Length > 0)
                    {
                        Changelifespan();
                    }
                    else if (checkBox4.Checked && textBox3.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Lifespan");
                    }
                    //ItemImage
                    if (checkBox7.Checked && pictureBox1.Image != oldImg)
                    {
                        ChangeImage();
                    }
                    else if (checkBox7.Checked && pictureBox1.Image != oldImg)
                    {
                        MessageBox.Show("Image is not changed please input another");
                    }
                    //description
                    if (checkBox8.Checked && textBox6.Text.Length > 0)
                    {
                        ChangeDesc();
                    }
                    else if (checkBox8.Checked && textBox6.Text.Length <= 0)
                    {
                        MessageBox.Show("Please insert a Description");
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error on editing items :" + ex.Message);
                }


            }
        }
        public void DisplayInfo()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {

                        string updateQuery = " Select * from ItemInventory where ItemID = @ID";
                        //string updateQuery = "UPDATE UserAccounts SET Username = @input WHERE AccountID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            conn.Open();
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            SqlDataReader reader = updateCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                ItemName = reader["ItemName"].ToString().Trim();
                                Type = reader["ItemType"].ToString().Trim();
                                Color = reader["ItemColor"].ToString().Trim();
                                Supplier = reader["Supplier"].ToString().Trim();
                                Price = reader["Price"].ToString().Trim();
                                lifespan = reader["LifeSpan"].ToString().Trim();
                                desc = reader["ItemDescription"].ToString().Trim();

                                textBox1.Text = reader["ItemName"].ToString().Trim();
                                comboBox1.Text = reader["ItemType"].ToString().Trim();
                                textBox2.Text = reader["ItemColor"].ToString().Trim();
                                textBox4.Text = reader["Supplier"].ToString().Trim();
                                textBox5.Text = reader["Price"].ToString().Trim();
                                textBox3.Text = reader["LifeSpan"].ToString().Trim();
                                textBox6.Text = reader["ItemDescription"].ToString().Trim();
                                if (reader["ItemImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["ItemImage"];
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
                MessageBox.Show("Error on retrieving Item :" + ex.Message);
            }
        }
        public void ChangeItemName()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET ItemName = @In WHERE ItemID = @ID;";
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET ItemType = @In WHERE ItemID = @ID;";
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
                MessageBox.Show("Error on Changing Item type :" + ex.Message);
            }
        }
        public void ChangeItemColor()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET ItemColor = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox2.Text.Trim());
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
                MessageBox.Show("Error on Changing Item color :" + ex.Message);
            }
        }
        public void Changelifespan()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET LifeSpan = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox3.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item lifespan Changed!");
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
                MessageBox.Show("Error on Changing Item lifespan :" + ex.Message);
            }
        }
        public void ChangePrice()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET Price = @In WHERE ItemID = @ID;";
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
                MessageBox.Show("Error on Changing Item Price :" + ex.Message);
            }
        }
        public void ChangeDesc()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET ItemDescription = @In WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                            updateCommand.Parameters.AddWithValue("@In", textBox6.Text.Trim());
                            conn.Open();
                            updateCommand.ExecuteNonQuery();
                            MessageBox.Show("Item Description Changed!");
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
                MessageBox.Show("Error on Changing Item Description :" + ex.Message);
            }
        }
        public void ChangeSupplier()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();
                    }
                }
                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string updateQuery = "UPDATE ItemInventory SET Supplier = @In WHERE ItemID = @ID;";
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
                MessageBox.Show("Error on Changing Item Supplier :" + ex.Message);
            }
        }
        public void ChangeImage()
        {
            try
            {
                int numId;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    string countQuery = "Select count(*) from ItemInventory where ItemID = @ID";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, conn))
                    {
                        conn.Open();
                        countCommand.Parameters.AddWithValue("@ID", ChangeIds.ItemID.Trim());
                        numId = (int)countCommand.ExecuteScalar();


                    }
                }




                if (numId == 1)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        Image s_img = pictureBox1.Image;
                        ImageConverter converter = new ImageConverter();
                        var ImageConvert = converter.ConvertTo(s_img, typeof(byte[]));
                        string updateQuery = "UPDATE ItemInventory SET ItemImage = @In WHERE ItemID = @ID;";
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
        #endregion
        #region Checkstates
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
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
                comboBox1.Text = "Select Item Type   >>";
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
                textBox2.Clear();
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Text = Color;
                textBox2.Enabled = false;
            }
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox3.Clear();
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Text = lifespan;
                textBox3.Enabled = false;
            }
        }

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
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

        private void checkBox8_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                textBox6.Clear();
                textBox6.Enabled = true;
            }
            else
            {
                textBox6.Text = desc;
                textBox6.Enabled = false;
            }
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox4.Clear();
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Text = Supplier;
                textBox4.Enabled = false;
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
        #endregion
        #region trash
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion


    }
}

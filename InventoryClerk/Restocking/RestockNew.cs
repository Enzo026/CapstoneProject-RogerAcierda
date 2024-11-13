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
using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms.Abuel;
using Flowershop_Thesis.OtherForms.AdvanceOrder;
using Flowershop_Thesis.OtherForms.Restocking;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;

namespace Flowershop_Thesis.InventoryClerk.Restocking
{
    public partial class RestockNew : Form
    {
        public static RestockNew instance;
        public Label RestockNum;
        public Label Idhandler;
        public Label ItmName;
        public Label loading;
        public RestockNew()
        {
            InitializeComponent();
            instance = this;
           
           loadTableData();
            loadTableMaterials();
            RestockNum = label78;
            loading = label9;
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value= DateTime.Today;
        }

        public void loadTableData()
        {
            try
            {
                flowLayoutPanel3.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT count(*) FROM ItemInventory WHERE ItemStatus = 'Available' and ItemType = 'Individual'";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory WHERE ItemStatus = 'Available' and ItemType = 'Individual' ORDER BY ItemQuantity ASC";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList
                                    {
                                        itemidData = int.Parse(reader["ItemID"].ToString()),
                                        itemnameData = reader["ItemName"].ToString(),
                                        itemquantityData = reader.GetInt32(reader.GetOrdinal("ItemQuantity")),
                                        Type = "Flowers",

                                    };

                                    flowLayoutPanel3.Controls.Add(itemList[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }
        public void loadTableMaterials()
        {
            try
            {
                flowLayoutPanel4.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials "; //select count(*) from ItemInventory where ItemType = 'Individual'
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM Materials order by ItemQuantity asc"; //SELECT * FROM ItemInventory where ItemType = 'Individual' order by ItemQuantity asc
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList();
                                    itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                    itemList[index].itemnameData = reader["ItemName"].ToString();
                                    int qty = reader.GetOrdinal("ItemQuantity");
                                    int demo = int.Parse(reader["ItemQuantity"].ToString());
                                    itemList[index].itemquantityData = demo;
                                    itemList[index].Type = "Materials";




                                    flowLayoutPanel4.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Searchbar()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con =  new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 AND  ItemName like @ItemName;;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 AND  ItemName like @ItemName;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@ItemName", "%" + textBox1.Text.Trim() + "%");
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList();
                                    itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                    itemList[index].itemnameData = reader["ItemName"].ToString();
                                    int qty = reader.GetOrdinal("ItemQuantity");
                                    int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                    itemList[index].itemquantityData = demo;

                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SortH2L()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity >= 0 AND ItemQuantity <=20 order by ItemQuantity desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList();
                                    itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                    itemList[index].itemnameData = reader["ItemName"].ToString();
                                    int qty = reader.GetOrdinal("ItemQuantity");
                                    int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                    itemList[index].itemquantityData = demo;
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Lowstock()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity > 0 AND ItemQuantity <=20;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 0 AND ItemQuantity <=20 ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList();
                                    itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                    itemList[index].itemnameData = reader["ItemName"].ToString();
                                    int qty = reader.GetOrdinal("ItemQuantity");
                                    int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                    itemList[index].itemquantityData = demo;
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Outofstock()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity = 0;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        RestockList[] itemList = new RestockList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity = 0";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new RestockList();
                                    itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                    itemList[index].itemnameData = reader["ItemName"].ToString();
                                    int qty = reader.GetOrdinal("ItemQuantity");
                                    int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                    itemList[index].itemquantityData = demo;
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Proceed Restocking " + label7.Text + " adding qty of " + textBox1.Text + " in the current inventory", "Restock Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {   
                    if(textBox1.Text == UserInfo.AdminCode)
                    {
                        try
                        {   
                            using(SqlConnection con = new SqlConnection(Connect.connectionString)) 
                            {
                                string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity + @qty, SuppliedDate = GETDATE() WHERE ItemID = @ID;";
                                con.Open();
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                                {

                              
                                    updateCommand.Parameters.AddWithValue("@qty", textBox1.Text);
                                    int rows = updateCommand.ExecuteNonQuery();
                                    MessageBox.Show("Item Updated!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error on :" + ex.Message);
                        }


                       // loadTableData();
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("Please Make sure admin code is correct!");
                    }

                }
                else
                {
                    //none
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void reset()
        {
            textBox1.Text = " ";
            textBox1.Text = " ";
            label7.Text = " ";
  

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                Searchbar();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        bool ready;
        private void label78_TextChanged(object sender, EventArgs e)
        {
            if (ready)
            {
                loadrestock();
            }
            else
            {
                //none
            }
        }
        public void loadrestock()
        {
            try
            {
                ready = false;
                flowLayoutPanel2.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from TempRestockTbl";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        label78.Text = rowCount.ToString();
                        if(rowCount > 0)
                        {
                            flowLayoutPanel2.Visible = true;
                            label6.Visible = false;

                            RestockingProcessItems[] itemList = new RestockingProcessItems[rowCount];

                            string sqlQuery = "SELECT * FROM TempRestockTbl";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new RestockingProcessItems();
                                        itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                        itemList[index].itemid = int.Parse(reader["Id"].ToString());
                                        itemList[index].itemnameData = reader["ItemName"].ToString();
                                        itemList[index].Supp = reader["Supplier"].ToString();
                                        itemList[index].Type = reader["Type"].ToString();
                                        int qty = reader.GetOrdinal("Qty");
                                        int demo = reader.IsDBNull((int)qty) ? 0 : reader.GetInt32((int)qty);
                                        itemList[index].itemquantityData = demo;
                                        flowLayoutPanel2.Controls.Add(itemList[index]);
                                        index++;

                                    }
                                }
                            }
                        }
                        else
                        {
                            flowLayoutPanel2.Visible = false;
                            label6.Visible = true; 
                        }


                    }
                }
                ready = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error on loading Restocking Data : " + e.Message);
            }
        }

        private void RestockNew_Load(object sender, EventArgs e)
        {

            loadrestock();
            getbatch();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {   
            if(textBox4.Text.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    try
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("RestockingProcess", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Employee", UserInfo.Empleyado);
                            cmd.Parameters.AddWithValue("@ReceiptID", textBox4.Text.Trim());
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show($"Stored procedure executed successfully. Rows affected: {rowsAffected}");
                            flowLayoutPanel2.Controls.Clear();
                            loadrestock();
                            label9.Visible = true;
                            textBox4.Text = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Input Receipt ID");
            }

        }
        public void getbatch()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT Count(*) FROM BatchRestockCompiled;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        BatchListItems[] itemList = new BatchListItems[rowCount];

                        string sqlQuery = "SELECT * FROM BatchRestockCompiled order by RestockingDate desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new BatchListItems();
                                    itemList[index].itemidData = reader["BatchID"].ToString();

                                    // Convert string to DateTime
                                    string givendate = reader["RestockingDate"].ToString();
                                    DateTime rdate;
                                    if (DateTime.TryParse(givendate, out rdate))
                                    {
                                        itemList[index].Date = rdate.ToString("MMM dd, yyyy");
                                    }
                                    else
                                    {
                                        itemList[index].Date = "Invalid Date";
                                    }

                                    itemList[index].itemquantityData = int.Parse(reader["TotalCount"].ToString());
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = int.Parse(label78.Text);
            if(count > 0)
            {
                DialogResult result = MessageBox.Show(
                 "Do you really want to cancel all items in restocking?",
                 "Confirm Cancellation",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question
                 );

                if (result == DialogResult.Yes)
                {
                    // Call the method to delete items
                    DeleteAllItems();

                }
                else
                {
                    // User selected No, handle accordingly
                    MessageBox.Show("Cancellation aborted.");
                }
            }
            else
            {
                MessageBox.Show("There are no Items in the Cart");
            }
         
        }
        private void DeleteAllItems()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string deleteQuery = "DELETE FROM TempRestockTbl;";

                using (SqlCommand command = new SqlCommand(deleteQuery, con))
                {
                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} rows deleted from temprestocktbl.");
                        loadrestock();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void label9_VisibleChanged(object sender, EventArgs e)
        {
            if (label9.Visible) 
            {
                getbatch();
                loadTableMaterials();
                loadTableData();
                label9.Visible = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {   
            dateTimePicker2.Enabled = true;
            dateTimePicker2.MinDate = dateTimePicker1.Value; // Update MinDate
            dateTimePicker2.MaxDate = DateTime.Today;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                DateTime date1 = dateTimePicker1.Value.Date;
                DateTime date2 = dateTimePicker2.Value.Date;
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "SELECT Count(*) FROM BatchRestockCompiled WHERE RestockingDate BETWEEN @startDate AND @endDate;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {   
                        countCommand.Parameters.AddWithValue("@startDate", date1);
                        countCommand.Parameters.AddWithValue("@endDate", date2);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        BatchListItems[] itemList = new BatchListItems[rowCount];

                        string sqlQuery = "SELECT * FROM BatchRestockCompiled WHERE RestockingDate BETWEEN @startDate AND @endDate order by BatchID desc";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@startDate", date1);
                            command.Parameters.AddWithValue("@endDate", date2);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < itemList.Length)
                                {
                                    itemList[index] = new BatchListItems();
                                    itemList[index].itemidData = reader["BatchID"].ToString();

                                    // Convert string to DateTime
                                    string givendate = reader["RestockingDate"].ToString();
                                    DateTime rdate;
                                    if (DateTime.TryParse(givendate, out rdate))
                                    {
                                        itemList[index].Date = rdate.ToString("MMM dd, yyyy");
                                    }
                                    else
                                    {
                                        itemList[index].Date = "Invalid Date";
                                    }

                                    itemList[index].itemquantityData = int.Parse(reader["TotalCount"].ToString());
                                    flowLayoutPanel1.Controls.Add(itemList[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) 
            {
                try
                {
                    flowLayoutPanel1.Controls.Clear();
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        string countQuery = "SELECT Count(*) FROM BatchRestockCompiled where BatchID Like @input;";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {   
                            countCommand.Parameters.AddWithValue("@input", "%"+textBox1.Text+"%");
                            int rowCount = (int)countCommand.ExecuteScalar();
                            BatchListItems[] itemList = new BatchListItems[rowCount];

                            string sqlQuery = "SELECT * FROM BatchRestockCompiled where BatchID Like @input order by BatchID desc";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@input", "%" + textBox1.Text + "%");
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new BatchListItems();
                                        itemList[index].itemidData = reader["BatchID"].ToString();

                                        // Convert string to DateTime
                                        string givendate = reader["RestockingDate"].ToString();
                                        DateTime rdate;
                                        if (DateTime.TryParse(givendate, out rdate))
                                        {
                                            itemList[index].Date = rdate.ToString("MMM dd, yyyy");
                                        }
                                        else
                                        {
                                            itemList[index].Date = "Invalid Date";
                                        }

                                        itemList[index].itemquantityData = int.Parse(reader["TotalCount"].ToString());
                                        flowLayoutPanel1.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                getbatch();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                try
                {
                    flowLayoutPanel4.Controls.Clear();
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        string countQuery = "select count(*) from Materials where ItemName like @input"; //select count(*) from ItemInventory where ItemType = 'Individual'
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {   
                            countCommand.Parameters.AddWithValue("@input", textBox3.Text+"%");
                            int rowCount = (int)countCommand.ExecuteScalar();
                            RestockList[] itemList = new RestockList[rowCount];

                            string sqlQuery = "SELECT * FROM Materials where ItemName like @input order by ItemQuantity asc"; //SELECT * FROM ItemInventory where ItemType = 'Individual' order by ItemQuantity asc
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@input",  textBox3.Text + "%");
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new RestockList();
                                        itemList[index].itemidData = int.Parse(reader["ItemID"].ToString());
                                        itemList[index].itemnameData = reader["ItemName"].ToString();
                                        int qty = reader.GetOrdinal("ItemQuantity");
                                        int demo = int.Parse(reader["ItemQuantity"].ToString());
                                        itemList[index].itemquantityData = demo;
                                        itemList[index].Type = "Materials";




                                        flowLayoutPanel4.Controls.Add(itemList[index]);
                                        index++;

                                    }
                                }
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                loadTableMaterials();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                try
                {
                    flowLayoutPanel3.Controls.Clear();
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        string countQuery = "SELECT count(*) FROM ItemInventory WHERE ItemStatus = 'Available' AND ItemName like @input";
                        using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                        {   
                            countCommand.Parameters.AddWithValue("@input", textBox2.Text + "%");
                            int rowCount = (int)countCommand.ExecuteScalar();
                            RestockList[] itemList = new RestockList[rowCount];

                            string sqlQuery = "SELECT * FROM ItemInventory WHERE ItemStatus = 'Available' AND ItemName like @input ORDER BY ItemQuantity ASC";
                            using (SqlCommand command = new SqlCommand(sqlQuery, con))
                            {
                                command.Parameters.AddWithValue("@input", textBox2.Text + "%");
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    int index = 0;
                                    while (reader.Read() && index < itemList.Length)
                                    {
                                        itemList[index] = new RestockList
                                        {
                                            itemidData = int.Parse(reader["ItemID"].ToString()),
                                            itemnameData = reader["ItemName"].ToString(),
                                            itemquantityData = reader.GetInt32(reader.GetOrdinal("ItemQuantity")),
                                            Type = "Flowers",

                                        };

                                        flowLayoutPanel3.Controls.Add(itemList[index]);
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                loadTableData();
            }
        }
    }
}

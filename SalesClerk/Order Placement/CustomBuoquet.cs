using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
using System;
using System.Collections;
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

namespace Flowershop_Thesis.SalesClerk.Order_Placement
{
    public partial class CustomBuoquet : Form
    {
        SqlCommand cmd = new SqlCommand();
 

        public static CustomBuoquet instance;

     //   public System.Windows.Forms.Label trye;

        //Primary Flower
        public Label iPFlowerName;
        public Label iPFlowerQty;

        public Label iPFlowerPrice;

        //Secondary
        public Label iSFlowerName;
        public Label iSFlowerQty;

        public Label iSFlowerPrice;

        //Cover
        public Label iCoverName;
        public Label iCoverQty;

        public Label iCoverPrice;

        //Ribbon
        public Label iRibbonName;
        public Label iRibbonQty;

        public Label iRibbonPrice;

        //Size
        public RadioButton iSmall;
        public RadioButton iMedium;
        public RadioButton iLarge;

        public Label loadinglbl;


        string Selected;
        public CustomBuoquet()
        {
            InitializeComponent();

            Setup();
            instance = this;


            iPFlowerName = PFlowerName;
            iPFlowerQty = PFlowerQty;
         
            iPFlowerPrice = PFlowerPrc;

            iSFlowerName = SFlowerName;
            iSFlowerQty = SFlowerQty;
   
            iSFlowerPrice = SFlowerPrc;

            iRibbonName = RibbonName;
            iRibbonQty = RibbonQty;
    
            iRibbonPrice = RibbonPrc;

            iCoverName = CoverName;
            iCoverQty = CoverQty;

            iCoverPrice = CoverPrc;

            Selected = "Primary";

            computePrice();
            FillFlowerTbl();



        }

       

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void CustomBuoquet_Load(object sender, EventArgs e)
        {


 
            computePrice();
            FillFlowerTbl();
        }
    

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        public void computePrice()
        {
            int size = 0;
            int PFlower = 0;
            int SFlower = 0;
            int Ribbon = 0;
            int Cover = 0;
            int Card = 0;

            if (SizePrc.Visible = true && SizePrc.Text != "0")
            {
                int prc = int.Parse(SizePrc.Text);
                size = prc;
            }
            if (PFlowerPrc.Visible = true && PFlowerPrc.Text != "0")
            {
                int prc = int.Parse(PFlowerPrc.Text);
                PFlower = prc;
            }
            if (SFlowerPrc.Visible = true && SFlowerPrc.Text != "0" && SFlowerCheckBox.Checked == true)
            {
                int prc = int.Parse(SFlowerPrc.Text);
                SFlower = prc;
            }
            if (RibbonPrc.Visible = true && RibbonPrc.Text != "0" && RibbonCheckBox.Checked == true)
            {
                int prc = int.Parse(RibbonPrc.Text);
                Ribbon = prc;
            }
            if (CoverPrc.Visible = true && CoverPrc.Text != "0" && CoverCheckBox.Checked == true)
            {
                int prc = int.Parse(CoverPrc.Text);
                Cover = prc;
            }
            if (CardPrc.Visible = true && CardPrc.Text != "0")
            {
                int prc = int.Parse(CardPrc.Text);
                Card = prc;
            }

            int TotalPrice = size + PFlower + SFlower + Ribbon + Cover + Card;
            TotalPrc.Text = TotalPrice.ToString();

        }

        public void Setup()
        {
            SFlowerCheckBox.Checked = false;
            RibbonCheckBox.Checked = false;
            CoverCheckBox.Checked = false;
            CardCheckBox.Checked = false;

            SFlowerBtn.Enabled = false;
            RibbonBtn.Enabled = false;
            CoverBtn.Enabled = false;
   

            //Secondary Flower
            SFlowerName.Visible = false;
            SFlowerPrc.Visible = false;
            SFlowerQty.Visible = false;
     
            SFlowerPrc.Text = "0";

            //Primary Flower
            PFlowerName.Visible = false;
            PFlowerPrc.Visible = false;
            PFlowerQty.Visible = false;
       
            PFlowerPrc.Text = "0";

            //Ribbon
            RibbonName.Visible = false;
            RibbonPrc.Visible = false;
            RibbonQty.Visible = false;
      
            RibbonPrc.Text = "0";

            //Cover
            CoverName.Visible = false;
            CoverPrc.Visible = false;
            CoverQty.Visible = false;
        
            CoverPrc.Text = "0";

            //Card
            CardName.Visible = false;
            CardPrc.Visible = false;
            CardQty.Visible = false;
            CardPrc.Text = "0";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void SizePrc_TextChanged(object sender, EventArgs e)
        {
            int price = int.Parse(PFlowerPrc.Text);
            if (price > 0)
            {
                computePrice();
                PFlowerName.Visible = true;
                PFlowerQty.Visible = true;
         
                PFlowerPrc.Visible = true;
            }
            if (price <= 0)
            {
                computePrice();
                PFlowerName.Visible = false;
                PFlowerQty.Visible = false;
     
                PFlowerPrc.Visible = false;
            }
        }
        public void sizeprice()
        {
            if (Small.Checked == true)
            {
                SizePrc.Text = "300";
                SizePrc.Visible = true;

            }
            else if (Medium.Checked == true)
            {
                SizePrc.Text = "700";
                SizePrc.Visible = true;
            }
            else if (Large.Checked == true)
            {
                SizePrc.Text = "1000";
                SizePrc.Visible = true;
            }
        }

        private void Small_CheckedChanged(object sender, EventArgs e)
        {
            sizeprice();
        }

        private void SFlowerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SFlowerCheckBox.Checked == true)
            {
                SFlowerBtn.Enabled = true;
            }
            else
            {
                SFlowerBtn.Enabled = false;
                SFlowerName.Visible = false;
                SFlowerQty.Visible = false;

                SFlowerPrc.Visible = false;
                SFlowerPrc.Text = "0";
                computePrice();
            }


        }

        private void RibbonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RibbonCheckBox.Checked == true)
            {
                RibbonBtn.Enabled = true;
            }
            else
            {
                RibbonBtn.Enabled = false;
                RibbonName.Visible = false;
                RibbonQty.Visible = false;

                RibbonPrc.Visible = false;
                RibbonPrc.Text = "0";
                computePrice();
            }
        }

        private void CoverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CoverCheckBox.Checked == true)
            {
                CoverBtn.Enabled = true;
            }
            else
            {
                CoverBtn.Enabled = false;
                CoverName.Visible = false;
                CoverQty.Visible = false;
 
                CoverPrc.Visible = false;
                CoverPrc.Text = "0";
                computePrice();
            }
        }

        private void CardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CardCheckBox.Checked == true)
            {
                CardName.Visible = true;
                CardPrc.Visible = true;
                CardQty.Visible = true;

                CardName.Text = "Dedication Letter";
                CardQty.Text = "1";
                CardPrc.Text = "100";
            }
            else
            {
                CardName.Visible = false;
                CardPrc.Visible = false;
                CardQty.Visible = false;
                CardPrc.Text = "0";
            }
        }

        private void PFlowerBtn_Click(object sender, EventArgs e)
        {
            Selected = "Primary";
            FillFlowerTbl();
        }
        
        public void FillFlowerTbl()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity > 0 AND ItemType = 'Individual';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        //    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                        int rowCount = (int)countCommand.ExecuteScalar();


                        // counter.Text = rowCount.ToString();



                        CustomList[] inv = new CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 0 AND ItemType = 'Individual';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["Price"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("ItemQuantity");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        public void FillCover()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Cover';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        //    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                        int rowCount = (int)countCommand.ExecuteScalar();


                        // counter.Text = rowCount.ToString();



                        CustomList[] inv = new CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Cover';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["UnitPrice"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("Usage");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        public void FillRibbon()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Ribbon';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        //    countCommand.Parameters.AddWithValue("@Search", textBox1.Text);
                        int rowCount = (int)countCommand.ExecuteScalar();


                        // counter.Text = rowCount.ToString();



                        CustomList[] inv = new CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Ribbon';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["UnitPrice"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("Usage");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);

                                    flowLayoutPanel1.Controls.Add(inv[index]);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
        }

        private void SFlowerBtn_Click(object sender, EventArgs e)
        {
            Selected = "Secondary";
            FillFlowerTbl();
        }

        private void CoverBtn_Click(object sender, EventArgs e)
        {
            Selected = "Cover";
            FillCover();
        }

        private void RibbonBtn_Click(object sender, EventArgs e)
        {
            Selected = "Ribbon";
            FillRibbon();
        }
        bool g2g = false;
        private void ProceedBtn_Click(object sender, EventArgs e)
        {

            checker();
            addInventory();
            AddCart();
            Deduction();


        }
        public void addInventory()
        {


            if (desc != "null" && BuoquetName.Text.Length > 0 && PrimaryColorTxtBox.Text.Length > 0)
            {

                try
                {
                    using (SqlConnection con = new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("INSERT INTO ItemInventory(ItemName,ItemQuantity,ItemType,ItemColor,LifeSpan,SuppliedDate,Supplier,ItemDescription,Price,ItemStatus)Values" +
                                    "(@Name,@Qty,@Type,@Color,@LifeSpan,getdate(),@Supplier,@Desc,@RSP,'Available');", con);
                        cmd.Parameters.AddWithValue("@Name", BuoquetName.Text);
                        cmd.Parameters.AddWithValue("@Qty", 1);
                        cmd.Parameters.AddWithValue("@Type", "Custom");
                        cmd.Parameters.AddWithValue("@Color", PrimaryColorTxtBox.Text);
                        cmd.Parameters.AddWithValue("@LifeSpan", 3);
                        cmd.Parameters.AddWithValue("@Supplier", "Instore");
                        cmd.Parameters.AddWithValue("@Desc", desc);
                        cmd.Parameters.AddWithValue("@RSP", Convert.ToDecimal(this.TotalPrc.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item Added Successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddingItem Failed!" + " : " + ex);
                }
            }
        }
        string desc = "null";
        public void checker()
        {   
            string sizeb = "NULL";
            string primary = "NULL";
            string secondary = "";
            string ribbon = "";
            string cover = "";
            if (PFlowerName.Text != "null" && PFlowerQty.Text !="null")
            {
                if (Small.Checked)
                {
                    sizeb = "Small sized";
                }
                else if (Medium.Checked)
                {
                    sizeb = "Medium sized";
                }
                else if (Large.Checked)
                {
                    sizeb = "Large sized";
                }
                else
                {
                    MessageBox.Show("Please Pick Size of the Buoquet first");
                }
                if (SFlowerName.Text != "null" && SFlowerQty.Text != "null" && SFlowerCheckBox.Checked == true)
                {
                    primary = PFlowerQty.Text + " " + PFlowerName.Text;
                    if (RibbonName.Text != "null" && RibbonCheckBox.Checked == true)
                    {
                        secondary = ", " + SFlowerQty.Text + " " + SFlowerName.Text + ",";
                        if (CoverName.Text != "null" && CoverCheckBox.Checked == true)
                        {
                            ribbon = RibbonName.Text + " Ribbon" + ", and ";
                            cover = CoverName.Text + " Cover";
                            
                        }
                        else
                        {
                            ribbon = " and "+RibbonName.Text + " Ribbon";
                        }
                       
                    }
                    else
                    {
                        secondary = " and " + SFlowerQty.Text + " " + SFlowerName.Text;
                    } 
                }
                else 
                {
                    primary = PFlowerQty.Text + " " + PFlowerName.Text ;
                }
              
            }
            else
            {
                MessageBox.Show("Please Pick Primary flower first");
            }
            if(sizeb != "NULL" || primary != "NULL")
            {
                desc = "Contents : " + sizeb + " Buoquet with " + primary + secondary + ribbon + cover;
            }
            if(NoteTxtBox.Text.Length > 0)
            {
                desc = desc + " Additional Notes : " + NoteTxtBox.Text;
            }
           



        }
        
        public void Deduction()
        {
            DeductBuoquet(BuoquetName.Text.Trim(), 1);
            FlowerDeduct(PFlowerName.Text.Trim(), int.Parse(PFlowerQty.Text));
            if (SFlowerCheckBox.Checked == true)
            {
                FlowerDeduct(SFlowerName.Text.Trim(), int.Parse(SFlowerQty.Text));
            }
            if (CoverCheckBox.Checked == true)
            {
                MaterialDeduct(CoverName.Text.Trim(), int.Parse(CoverQty.Text));
            }
            if (RibbonCheckBox.Checked == true)
            {
                MaterialDeduct(RibbonName.Text.Trim(), int.Parse(RibbonQty.Text));
            }

        }

        public void FlowerDeduct(string ItemName, int quantitee)
        {
            int rowCount;

            //Check if theres more than 1 items in the inventory making sure it will be 
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemName = @Name;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@Name", ItemName);
                    rowCount = (int)countCommand.ExecuteScalar();

                }

                if (rowCount == 1)
                {

                    string invID = "0";
                    string sqlQuery = "SELECT * FROM ItemInventory where ItemName = @Name;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Name", ItemName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                invID = reader["ItemID"].ToString();
                            }
                        }
                    }


                    string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @ID;";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                    {

                        updateCommand.Parameters.AddWithValue("@Quantity", quantitee);
                        updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));

                        updateCommand.ExecuteNonQuery();

                    }
                }
                else
                {
                    MessageBox.Show("Error on inventory name is duplicated!");
                }
            }


        }

        public void DeductBuoquet(string ItemName, int quantitee)
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                int rowCount;
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemName = @Name AND ItemType = 'Custom';";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@Name", ItemName);
                    rowCount = (int)countCommand.ExecuteScalar();

                }
                if (rowCount == 1)
                {
                    string invID = "0";
                    string sqlQuery = "SELECT * FROM ItemInventory where ItemName = @Name AND ItemType = 'Custom';";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@Name", ItemName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                invID = reader["ItemID"].ToString();
                            }
                        }
                    }

                    string updateQuery = "UPDATE ItemInventory SET ItemQuantity = ItemQuantity - @Quantity WHERE ItemID = @ID;";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                    {

                        updateCommand.Parameters.AddWithValue("@Quantity", quantitee);
                        updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));

                        updateCommand.ExecuteNonQuery();

                    }
                }
                else
                {
                    MessageBox.Show("Error on inventory name is duplicated!");
                }
            }

        }

        public void MaterialDeduct(string ItemName, int qty)
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                try
                {
                    // Open the connection before executing any command
                    con.Open();

                    int rowCount;

                    // Count query to check if item exists
                    string countQuery = "SELECT COUNT(*) FROM Materials WHERE ItemName = @Name;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@Name", ItemName);
                        rowCount = (int)countCommand.ExecuteScalar();
                    }

                    if (rowCount == 1)
                    {
                        // Variables to store values retrieved from the database
                        string invID = "0";
                        string usage = "0";
                        string UsageQuantity = "0";

                        // Retrieve data for the item
                        string sqlQuery = "SELECT * FROM Materials WHERE ItemName = @Name;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@Name", ItemName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    invID = reader["ItemID"].ToString();
                                    usage = reader["Usage"].ToString();
                                    UsageQuantity = reader["UsageQuantity"].ToString();
                                }
                            }
                        }

                        // Calculate new usage quantity
                        int quantityUse = int.Parse(usage) - qty;

                        // Update the Usage or ItemQuantity depending on the result of quantityUse
                        if (quantityUse > 0)
                        {
                            // Update the Usage value in Materials
                            string updateUsage = "UPDATE Materials SET Usage = @Quantity WHERE ItemID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateUsage, con))
                            {
                                updateCommand.Parameters.AddWithValue("@Quantity", quantityUse);
                                updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));

                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else if (quantityUse == 0)
                        {
                            // Decrease the ItemQuantity by 1
                            string updateItemQuantity = "UPDATE Materials SET ItemQuantity = ItemQuantity - 1 WHERE ItemID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateItemQuantity, con))
                            {
                                updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));

                                updateCommand.ExecuteNonQuery();
                            }

                            // Reset the Usage value to the original usage quantity
                            string updateUsage = "UPDATE Materials SET Usage = @Quantity WHERE ItemID = @ID;";
                            using (SqlCommand updateCommand = new SqlCommand(updateUsage, con))
                            {
                                updateCommand.Parameters.AddWithValue("@Quantity", int.Parse(UsageQuantity));
                                updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));

                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: Inventory name is duplicated!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }




        int ItemID;
        public void AddCart()
        {
            using (SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string invID = "0";
                string sqlQuery = "SELECT * FROM ItemInventory where ItemName = @Name;";
                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {
                    command.Parameters.AddWithValue("@Name", BuoquetName.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            invID = reader["ItemID"].ToString();
                        }
                    }
                }
                if (invID != "0")
                {
                    try
                    {
                        cmd = new SqlCommand("INSERT INTO ServingCart(ItemID,ItemName,OrderQty,OrderPrice,OrderType)Values" +
                                    "(@ID,@Name,@Qty,@Price,@Type);", con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(invID));
                        cmd.Parameters.AddWithValue("@Name", BuoquetName.Text);
                        cmd.Parameters.AddWithValue("@Qty", 1);
                        cmd.Parameters.AddWithValue("@Price", int.Parse(TotalPrc.Text));
                        cmd.Parameters.AddWithValue("@Type", "Custom");

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("AddingItem Failed!" + " : " + ex);
                    }


                    //activation of textchange event to refresh the item list in main panel
                    int cart = int.Parse(OrderPlacement.instance.lbl.Text);
                    cart++;
                    MessageBox.Show("Item Successfully Added Cart Items: " + cart.ToString());
                    OrderPlacement.instance.lbl.Text = cart.ToString();
                }
            }


        }
    }
}


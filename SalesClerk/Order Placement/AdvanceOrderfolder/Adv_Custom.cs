using Flowershop_Thesis.OtherForms;
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
using Flowershop_Thesis;
using Capstone_Flowershop;

namespace Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder
{
    public partial class Adv_Custom : Form
    {
        SqlCommand cmd = new SqlCommand();

        public static Adv_Custom instance;

        //   public System.Windows.Forms.Label trye;

        //Primary Flower
        public Label iPFlowerName;
        public Label iPFlowerQty;
        public Label iPFlowerRSP;
        public Label iPFlowerPrice;

        //Secondary
        public Label iSFlowerName;
        public Label iSFlowerQty;
        public Label iSFlowerRSP;
        public Label iSFlowerPrice;

        //Cover
        public Label iCoverName;
        public Label iCoverQty;
        public Label iCoverRSP;
        public Label iCoverPrice;

        //Ribbon
        public Label iRibbonName;
        public Label iRibbonQty;
        public Label iRibbonRSP;
        public Label iRibbonPrice;

        //Size
        public RadioButton iSmall;
        public RadioButton iMedium;
        public RadioButton iLarge;

        string Selected;
        public Adv_Custom()
        {
            InitializeComponent();
            Setup();
            instance = this;


            iPFlowerName = PFlowerName;
            iPFlowerQty = PFlowerQty;
            iPFlowerRSP = PFlowerRSP;
            iPFlowerPrice = PFlowerPrc;

            iSFlowerName = SFlowerName;
            iSFlowerQty = SFlowerQty;
            iSFlowerRSP = SFlowerRSP;
            iSFlowerPrice = SFlowerPrc;

            iRibbonName = RibbonName;
            iRibbonQty = RibbonQty;
            iRibbonRSP = RibbonRSP;
            iRibbonPrice = RibbonPrc;

            iCoverName = CoverName;
            iCoverQty = CoverQty;
            iCoverRSP = CoverRSP;
            iCoverPrice = CoverPrc;

            Selected = "Primary";

            computePrice();
            FillFlowerTbl();
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
            SFlowerRSP.Visible = false;
            SFlowerPrc.Text = "0";

            //Primary Flower
            PFlowerName.Visible = false;
            PFlowerPrc.Visible = false;
            PFlowerQty.Visible = false;
            PFlowerRSP.Visible = false;
            PFlowerPrc.Text = "0";

            //Ribbon
            RibbonName.Visible = false;
            RibbonPrc.Visible = false;
            RibbonQty.Visible = false;
            RibbonRSP.Visible = false;
            RibbonPrc.Text = "0";

            //Cover
            CoverName.Visible = false;
            CoverPrc.Visible = false;
            CoverQty.Visible = false;
            CoverRSP.Visible = false;
            CoverPrc.Text = "0";

            //Card
            CardName.Visible = false;
            CardPrc.Visible = false;
            CardQty.Visible = false;
            CardPrc.Text = "0";
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

        public void FillFlowerTbl()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from ItemInventory where ItemQuantity > 0 AND ItemType = 'Individual';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Adv_CustomList[] inv = new Adv_CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM ItemInventory where ItemQuantity > 0 AND ItemType = 'Individual';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Adv_CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["Price"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("ItemQuantity");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);
                                    if (reader["ItemImage"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["ItemImage"];
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            inv[index].img = Image.FromStream(ms);
                                        }
                                    }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Cover';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Adv_CustomList[] inv = new Adv_CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Cover';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Adv_CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["Price"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("Usage");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);
                                    if (reader["ItemImage"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["ItemImage"];
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            inv[index].img = Image.FromStream(ms);
                                        }
                                    }
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
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Ribbon';";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        int rowCount = (int)countCommand.ExecuteScalar();
                        Adv_CustomList[] inv = new Adv_CustomList[rowCount];

                        string sqlQuery = "SELECT * FROM Materials where ItemQuantity > 0 AND Usage > 1 AND ItemType = 'Ribbon';";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            //  command.Parameters.AddWithValue("@Search", textBox1.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new Adv_CustomList();
                                    inv[index].Name = reader["ItemName"].ToString();
                                    inv[index].selection = Selected;
                                    inv[index].Price = reader["Price"].ToString();

                                    int CI = reader.GetOrdinal("ItemID");
                                    inv[index].ItemID = reader.IsDBNull((int)CI) ? 0 : reader.GetInt32((int)CI);
                                    int IQ = reader.GetOrdinal("Usage");
                                    inv[index].Qty = reader.IsDBNull((int)IQ) ? 0 : reader.GetInt32((int)IQ);

                                    if (reader["ItemImage"] != DBNull.Value)
                                    {
                                        byte[] imageData = (byte[])reader["ItemImage"];
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            inv[index].img = Image.FromStream(ms);
                                        }
                                    }
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

        public void addInventory()
        {

            if (desc != "null" && BuoquetName.Text.Length > 0 && PrimaryColorTxtBox.Text.Length > 0)
            {

                try
                {
                    using (SqlConnection con =  new SqlConnection(Connect.connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("INSERT INTO ItemInventory(ItemName,ItemQuantity,ItemType,ItemColor,LifeSpan,SuppliedDate,Supplier,ItemDescription,Price,ItemStatus)Values(@Name,@Qty,@Type,@Color,@LifeSpan,getdate(),@Supplier,@Desc,@RSP,'Available');", con);
                        cmd.Parameters.AddWithValue("@Name", BuoquetName.Text);
                        cmd.Parameters.AddWithValue("@Qty", 1);
                        cmd.Parameters.AddWithValue("@Type", "AdvanceCustom");
                        cmd.Parameters.AddWithValue("@Color", PrimaryColorTxtBox.Text);
                        cmd.Parameters.AddWithValue("@LifeSpan", 3);
                        cmd.Parameters.AddWithValue("@Supplier", "Instore");
                        cmd.Parameters.AddWithValue("@Desc", desc);
                        cmd.Parameters.AddWithValue("@RSP", Convert.ToDecimal(this.TotalPrc.Text));

                        cmd.ExecuteNonQuery();
                    }
       
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddingItem Failed!" + " : " + ex);
                }
            }
            else
            {
                MessageBox.Show("Error on items please make sure all items needed are supplied");
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
            if (PFlowerName.Text != "null" && PFlowerQty.Text != "null")
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
                            ribbon = " and " + RibbonName.Text + " Ribbon";
                        }

                    }
                    else
                    {
                        secondary = " and " + SFlowerQty.Text + " " + SFlowerName.Text;
                    }
                }
                else
                {
                    primary = PFlowerQty.Text + " " + PFlowerName.Text;
                }

            }
            else
            {
                MessageBox.Show("Please Pick Primary flower first");
            }
            if (sizeb != "NULL" || primary != "NULL")
            {
                desc = "Contents : " + sizeb + " Buoquet with " + primary + secondary + ribbon + cover;
            }
            if (NoteTxtBox.Text.Length > 0)
            {
                desc = desc + " Additional Notes : " + NoteTxtBox.Text;
            }




        }
        int ItemNameCount;
        public void CheckName()
        {   
            using(SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string countQuery = "select count(*) from ItemInventory where ItemName = @Name;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@Name", BuoquetName.Text);
                    ItemNameCount = (int)countCommand.ExecuteScalar();
                }
            }
        }
        public void FlowerDeduct(string ItemName)
        {
          

            //Check if theres more than 1 items in the inventory making sure it will be 

            if (ItemNameCount == 1)
            {   
                using(SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
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
                }
            }
            else
            {
                MessageBox.Show("Error on inventory name is duplicated!");
            }

        }

        public void DeductBuoquet(string ItemName, int quantitee)
        {   
            using(SqlConnection con = new SqlConnection(Connect.connectionString))
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
            using(SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                int rowCount;
                con.Open();
                string countQuery = "select count(*) from Materials where ItemName = @Name;";
                using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                {
                    countCommand.Parameters.AddWithValue("@Name", ItemName);
                    rowCount = (int)countCommand.ExecuteScalar();

                }
                if (rowCount == 1)
                {
                    string invID = "0";
                    string usage = "0";
                    string UsageQuantity = "0";

                    string sqlQuery = "SELECT * FROM Materials where ItemName = @Name;";
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
                    int quantityuse = int.Parse(usage) - qty;

                    if (quantityuse > 0)
                    {
                        string updateUsage = "UPDATE Materials SET Usage=@Quantity WHERE ItemID = @ID;";
                        using (SqlCommand updateCommand = new SqlCommand(updateUsage, con))
                        {

                            updateCommand.Parameters.AddWithValue("@Quantity", quantityuse);
                            updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else if (quantityuse == 0)
                    {
                        string updateItemQuantity = "UPDATE Materials SET ItemQuantity=ItemQuantity - 1 WHERE ItemID = @ID";
                        using (SqlCommand updateCommand = new SqlCommand(updateItemQuantity, con))
                        {
                            updateCommand.Parameters.AddWithValue("@ID", int.Parse(invID));
                            updateCommand.ExecuteNonQuery();
                        }

                        string updateUsage = "UPDATE Materials SET Usage=@Quantity WHERE ItemID = @ID;";
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
                    MessageBox.Show("Error on inventory name is duplicated!");
                }
            }


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
        int ItemID;
        public void AddCart()
        {   
            using(SqlConnection con = new SqlConnection(Connect.connectionString))
            {
                con.Open();
                string invID = "0";
                string sqlQuery = "SELECT * FROM ItemInventory where ItemName = @Name AND ItemType = 'AdvanceCustom';";
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
                        cmd = new SqlCommand("INSERT INTO Advance_ServingCart(ItemID,ItemName,OrderQty,OrderPrice,OrderType)Values(@ID,@Name,@Qty,@Price,@Type);", con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(invID));
                        cmd.Parameters.AddWithValue("@Name", BuoquetName.Text);
                        cmd.Parameters.AddWithValue("@Qty", 1);
                        cmd.Parameters.AddWithValue("@Price", int.Parse(TotalPrc.Text));
                        cmd.Parameters.AddWithValue("@Type", "AdvanceCustom");
                        cmd.ExecuteNonQuery();
                        //activation of textchange event to refresh the item list in main panel
                        int cart = int.Parse(AdvanceOrderFrm.instance.cartbtn.Text);
                        cart++;
                        AdvanceOrderFrm.instance.cartbtn.Text = cart.ToString();
                        MessageBox.Show("Item Successfully Added Cart Items: " + cart.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("AddingItem Failed!" + " : " + ex);
                    }
                }
            }
        }
        public void Deduction()
        {
            DeductBuoquet(BuoquetName.Text.Trim(), 1);
            FlowerDeduct(PFlowerName.Text.Trim());
            if (SFlowerCheckBox.Checked == true)
            {
                FlowerDeduct(SFlowerName.Text.Trim());
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
        private void ProceedBtn_Click(object sender, EventArgs e)
        {
            CheckName();
            if(ItemNameCount == 0)
            {
                checker();
                addInventory();
                AddCart();
            }
            else
            {
                MessageBox.Show("Item Name Taken Please Choose Another one");
            }
    
           // Deduction();
        }

        private void SizePrc_TextChanged(object sender, EventArgs e)
        {
            int price = int.Parse(PFlowerPrc.Text);
            if (price > 0)
            {
                computePrice();
                PFlowerName.Visible = true;
                PFlowerQty.Visible = true;
                PFlowerRSP.Visible = true;
                PFlowerPrc.Visible = true;
            }
            if (price <= 0)
            {
                computePrice();
                PFlowerName.Visible = false;
                PFlowerQty.Visible = false;
                PFlowerRSP.Visible = false;
                PFlowerPrc.Visible = false;
            }
        }

        private void SFlowerBtn_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void SFlowerBtn_Click(object sender, EventArgs e)
        {
            Selected = "Secondary";
            FillFlowerTbl();
        }

        private void RibbonBtn_Click(object sender, EventArgs e)
        {
            Selected = "Ribbon";
            FillRibbon();
        }

        private void CoverBtn_Click(object sender, EventArgs e)
        {
            Selected = "Cover";
            FillCover();
        }

        private void PFlowerBtn_Click(object sender, EventArgs e)
        {
            Selected = "Individual";
            FillFlowerTbl();
        }

        private void Small_CheckedChanged(object sender, EventArgs e)
        {
            sizeprice();
        }

        private void Medium_CheckedChanged(object sender, EventArgs e)
        {
            sizeprice();
        }

        private void Large_CheckedChanged(object sender, EventArgs e)
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
                SFlowerRSP.Visible = false;
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
                RibbonRSP.Visible = false;
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
                CoverRSP.Visible = false;
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
    }
}

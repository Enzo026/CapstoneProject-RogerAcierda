using Capstone_Flowershop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms
{
    public partial class EditOrderFrm : Form
    {
        public EditOrderFrm()
        {
            InitializeComponent();
        }
        public void getinfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                

                        string sqlQuery = "SELECT OrderQty FROM ServingCart where CartID = @Id";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {   
                            command.Parameters.AddWithValue("@Id", WalkInTransaction.EditItemCartID);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
    
                                while (reader.Read())
                                {
            
                                    textBox1.Text = reader["OrderQty"].ToString();

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
        public void getqty()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();


                    string sqlQuery = "SELECT * FROM BatchRestockCompiled order by RestockingDate desc";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 0;
                            while (reader.Read())
                            {

                                label5.Text = reader["BatchID"].ToString();

                                // Convert string to DateTime
                                string givendate = reader["RestockingDate"].ToString();
                                DateTime rdate;
                                if (DateTime.TryParse(givendate, out rdate))
                                {
                                  //  itemList[index].Date = rdate.ToString("MMM dd, yyyy");
                                }
                                else
                                {
                                    //itemList[index].Date = "Invalid Date";
                                }

                                // itemList[index].itemquantityData = int.Parse(reader["TotalCount"].ToString());

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
    }
}

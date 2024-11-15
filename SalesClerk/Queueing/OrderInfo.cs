using Capstone_Flowershop;
using Flowershop_Thesis.OtherForms;
using Flowershop_Thesis.OtherForms.QueuingList;
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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;

namespace Flowershop_Thesis.SalesClerk.Queueing
{
    public partial class OrderInfo : Form
    {
        public OrderInfo()
        {
            InitializeComponent();
        }

        public void getorderitems()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from SalesItemTbl where TransactionID = @info;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        OrderInfoList[] inv = new OrderInfoList[rowCount];

                        string sqlQuery = "SELECT * FROM SalesItemTbl where TransactionID = @info ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    inv[index] = new OrderInfoList();
                                    inv[index].ItmId = int.Parse(reader["ItemID"].ToString());
                                    inv[index].ItmName = reader["ItemName"].ToString();
                                    inv[index].Price = decimal.Parse(reader["ItemPrice"].ToString());
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

        public void getInfo()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM TransactionsTbl where TransactionID = @info ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                TransactionIdLbl.Text = reader["TransactionID"].ToString();
                                CustomerNameLbl.Text = reader["CustomerName"].ToString();
                                TotalAmountLbl.Text = reader["Price"].ToString();
                                StatusLbl.Text = reader["Status"].ToString();
                                PaymentStatusLbl.Text = reader["PaymentStatus"].ToString();
                                PaymentMethodLbl.Text = reader["PaymentMethod"].ToString();
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
        private void OrderInfo_Load(object sender, EventArgs e)
        {
            getInfo();
            getorderitems();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.ExecutablePath, "..", "OrderInfo.pdf");
            PdfWriter writer = new PdfWriter(pdfPath);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);
            document.Add(new Paragraph("Order Information").SetFontSize(16).SetBold().SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)));

            OrderInfoData data = getInfoData();
            document.Add(new Paragraph("=== FLOWER SHOP ORDER RECEIPT ===").SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph("--------------------------------").SetTextAlignment(TextAlignment.CENTER));
            // Create a table with 2 columns
            Table table = new Table(2);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Add rows with labels and values
            table.AddCell(new Cell().Add(new Paragraph("Transaction ID:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(data.TransactionId)));

            table.AddCell(new Cell().Add(new Paragraph("Customer Name:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(data.CustomerName)));

            table.AddCell(new Cell().Add(new Paragraph("Total Amount:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph("PHP " + data.TotalAmount)));

            table.AddCell(new Cell().Add(new Paragraph("Status:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(data.Status)));

            table.AddCell(new Cell().Add(new Paragraph("Payment Status:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(data.PaymentStatus)));

            table.AddCell(new Cell().Add(new Paragraph("Payment Method:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(data.PaymentMethod)));

            document.Add(table);
            document.Add(new Paragraph("--------------------------------").SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph("Order Items:").SetBold());
            foreach (var item in data.OrderItems)
            {
                document.Add(new Paragraph($"   {item.ItemName} - PHP {item.Price:N2}"));
            }
            document.Add(new Paragraph("--------------------------------").SetTextAlignment(TextAlignment.CENTER));


            document.Add(new Paragraph("Thank you for your purchase!").SetTextAlignment(TextAlignment.CENTER));

            document.Close();

            if (MessageBox.Show("Order information has been saved as PDF.\nWould you like to view it now?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(pdfPath);
            }
        }

        public class OrderInfoData
        {
            public string TransactionId { get; set; }
            public string CustomerName { get; set; }
            public string TotalAmount { get; set; }
            public string Status { get; set; }
            public string PaymentStatus { get; set; }
            public string PaymentMethod { get; set; }
            public List<OrderItemsData> OrderItems { get; set; }
        }

        public class OrderItemsData
        {
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public decimal Price { get; set; }
        }

        public OrderInfoData getInfoData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM TransactionsTbl where TransactionID = @info ;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, con))
                    {
                        command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            OrderInfoData data = new OrderInfoData();
                            data.TransactionId = reader["TransactionID"].ToString();
                            data.CustomerName = reader["CustomerName"].ToString();
                            data.TotalAmount = reader["Price"].ToString();
                            data.Status = reader["Status"].ToString();
                            data.PaymentStatus = reader["PaymentStatus"].ToString();
                            data.PaymentMethod = reader["PaymentMethod"].ToString();
                            data.OrderItems = getOrderItemsData();
                            return data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }
            return null;
        }

        public List<OrderItemsData> getOrderItemsData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connect.connectionString))
                {
                    con.Open();
                    string countQuery = "select count(*) from SalesItemTbl where TransactionID = @info;";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, con))
                    {
                        countCommand.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                        int rowCount = (int)countCommand.ExecuteScalar();
                        OrderItemsData[] inv = new OrderItemsData[rowCount];

                        string sqlQuery = "SELECT * FROM SalesItemTbl where TransactionID = @info ;";
                        using (SqlCommand command = new SqlCommand(sqlQuery, con))
                        {
                            command.Parameters.AddWithValue("@info", ChangeIds.OrderInfo);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                int index = 0;
                                while (reader.Read() && index < inv.Length)
                                {
                                    OrderItemsData data = new OrderItemsData();
                                    data.ItemId = int.Parse(reader["ItemID"].ToString());
                                    data.ItemName = reader["ItemName"].ToString();
                                    data.Price = decimal.Parse(reader["ItemPrice"].ToString());

                                    inv[index] = data;
                                    index++;
                                }
                                return inv.ToList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error on CartLsit() : " + ex.Message);
            }

            return null;
        }
    }
}

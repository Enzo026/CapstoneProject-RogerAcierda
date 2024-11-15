using Capstone_Flowershop;
using Flowershop_Thesis.SalesClerk.Order_Placement.AdvanceOrderfolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace Flowershop_Thesis.OtherForms.AdvanceOrder
{
    public partial class CashOrder : Form
    {
        public CashOrder()
        {
            InitializeComponent();
            filldata();
        }

        public void filldata()
        {
            CustomerName.Text = CreateAdvanceOrder.CustomerName;
            TotalAmount.Text = CreateAdvanceOrder.TotalAmount;
            MOP.Text = CreateAdvanceOrder.ModeOfPayment;
            Date.Text = CreateAdvanceOrder.Date;
            DP.Text = CreateAdvanceOrder.Downpayment;
            OrderType.Text = CreateAdvanceOrder.OrderType;
            PUD.Text = CreateAdvanceOrder.PickUpDate;
            Contact.Text = CreateAdvanceOrder.ContactNumber;
            EmpName.Text = UserInfo.Empleyado;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            changecartvalue();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wala pang resibo tanga! HAHAHA");
            changecartvalue();
            this.Close();
        }
        public void changecartvalue()
        {
            AdvanceOrderFrm.instance.cartbtn.Text = "0";
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(Application.ExecutablePath, "..", "AdvanceOrderInfo.pdf");
            PdfWriter writer = new PdfWriter(pdfPath);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);

            document.Add(new Paragraph("Order Information").SetFontSize(16).SetBold().SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)));

            Table table = new Table(2);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            table.AddCell(new Cell().Add(new Paragraph("Customer Name:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.CustomerName)));

            table.AddCell(new Cell().Add(new Paragraph("Total Amount:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.TotalAmount)));

            table.AddCell(new Cell().Add(new Paragraph("Mode of Payment:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.ModeOfPayment)));

            table.AddCell(new Cell().Add(new Paragraph("Date:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.Date)));

            table.AddCell(new Cell().Add(new Paragraph("Downpayment:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.Downpayment)));

            table.AddCell(new Cell().Add(new Paragraph("Order Type:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.OrderType)));

            table.AddCell(new Cell().Add(new Paragraph("Pick-up Date:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.PickUpDate)));

            table.AddCell(new Cell().Add(new Paragraph("Contact Number:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(CreateAdvanceOrder.ContactNumber)));

            table.AddCell(new Cell().Add(new Paragraph("Employee Name:").SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(UserInfo.Empleyado)));

            document.Add(table);

            document.Close();


            if (MessageBox.Show("Order information has been saved as PDF.\nWould you like to view it now?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(pdfPath);
            }
        }
    }
}

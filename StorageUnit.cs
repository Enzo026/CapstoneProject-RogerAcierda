using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Flowershop
{

    public static class UserInfo
    {
        public static string Empleyado { get; set; }
        public static string FullName { get; set; }

        public static string AdminCode { get; set; }
    }
    public static class CreateAdvanceOrder
    {
        public static string CustomerName { get; set; }
        public static string TotalAmount { get; set; }
        public static string ModeOfPayment { get; set; }
        public static string Date { get; set; }
        public static string Downpayment { get; set; }
        public static string OrderType { get; set; }
        public static string PickUpDate { get; set; }
        public static string ContactNumber { get; set; }
        public static Image ProofOfPayment { get; set; }

    }
    public static class PaymentConfirm
    {
        public static bool isPaid { get; set; }
    }
    public static class StockImages
    {
        public static Image StockAddImg { get; set; }
    }
    public static class AdvanceOrderPlacement
    {
        public static bool InitializeDone { get; set; }
        public static bool InsertAdvanceOrder { get; set; }
        public static bool InsertAdvanceOrderItems { get; set; }
    }
    public static class AdvanceOrderPaymentDetails
    {
        public static string OrderID { get; set; }
        public static string CustomerName { get; set; }
        public static string TotalAmount { get; set; }
        public static string Downpayment { get; set; }
        public static string AmountPayable { get; set; }
        public static string Discount { get; set; }
        public static string OrderItems { get; set; }
        public static string PaymentMethod { get; set; }
        public static Image ProofofPayment { get; set; }

    }
}

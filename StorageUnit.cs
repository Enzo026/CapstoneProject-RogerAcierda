using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Capstone_Flowershop
{
    public static class DevTools
    {
        public static string SecretUser = "RogerAcierdaSecretSystemAccountv1.0";
        public static string Pass = "DevToolsCode081124-1";
    }
    public class AppConfig
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public bool MainPC { get; set; }
        public string ConnectionString { get; set; }

        // Method to construct the connection string
        public string CreateConnectionString()
        {
            string newconString = $"Server={IP},{Port};Database=RogerAcierdaDatabase;User Id=sa;Password=ApplicationDb123;";
            ConnectionString = newconString;
            return newconString;
        }

        public static bool TestConnectionString(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Try to open the connection
                    return true; // Connection successful
                }
            }
            catch (SqlException)
            {
                return false; // Connection failed
            }
        }
        //create backup

    }
    public static class Connect
    {
        public static string sqlServer = "DESKTOP-IH4V487\\NEWMSSQL"; // Replace with your SQL server name
        public static string connectionStrings = $"Server=DESKTOP-IH4V487\\NEWMSSQL;Database=D:\\CAPSTONEPROJECT-ROGERACIERDA\\FLOWERSHOPSYSTEMDB.MDF;Integrated Security=True;";
        public static string connectionString2 = $"Data Source=DESKTOP-IH4V487\\NEWMSSQL;AttachDbFilename=D:\\CapstoneProject-RogerAcierda\\FlowershopSystemDB.mdf;Initial Catalog=FlowershopSystemDB;Integrated Security=True";

        //laptop
        public static string connectionStringz = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Thesis Repository Capstone\\CapstoneProject-RogerAcierda\\FlowershopSystemDB.mdf\";Integrated Security=True";

        public static string connectionString = "Server=192.168.8.205,2626;Initial Catalog=RogerAcierdaFlowerShop;Persist Security Info=True;User ID=sa;Password=ApplicationDb123;Encrypt=True;TrustServerCertificate=True;";
        public static string csRestore = "Server=192.168.8.205,2626;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=ApplicationDb123;Encrypt=True;TrustServerCertificate=True;";

    }
    public static class UserInfo
    {
        public static string Empleyado { get; set; }
        public static string EmpID { get; set; }
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
    public static class ChangeIds
    {
        public static string SupplierId { get; set; }
        public static string ItemID { get; set; }
        public static string AccountID { get; set; }
        public static string ItemType { get; set;}

        public static string TransactionLogID { get; set; } 

        public static string OrderInfo { get; set; }

 

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

    public static class ViewInfo
    {
        public static string ID { get; set; }
        public static string type { get; set; }

        public static string  RI_Id { get; set; }
    }
    public static class RestockingProcess
    {
        public static int ID { get; set; }
        public static string type { get; set; }
    }
    public static class DisposalInfo
    {
        public static string ID { get; set; }
        public static string OrderType { get; set; }

        public static string ItemType { get; set; }

        public static string EvID { get; set; }
        public static string EvName { get; set; }
        public static string EvQty { get; set; }
        public static string SalesItemID { get; set; }
        public static string EvPrice { get; set; }

        public static string OrderStatus { get; set; }


    }
    public static class SystemInfo
    {
        public static string SecurityCode { get; set; }
        public static decimal discount { get; set; }
        public static decimal MinimumOrder { get; set; }

        public static string IpAdd = "192.168.8.205";

    }

    public static class SA_Info
    {
        public static string BatchID { get; set; }
        public static string RestockingID { get; set; }
        public static string ItemID { get; set; }
        public static string Type { get; set; }

    }
    public static class SupplierInfo
    {
        public static string SupplierName { get; set; }
        public static string SupplierType { get; set; }

    }
    public static class WalkInTransaction
    {
        public static string OrderType { get; set; }
        public static string CancellationType { get; set; }
        public static string CancellationItemName { get; set; }
        public static string CancellationCartId{ get;  set; }
        public static string EditItemCartID { get; set; }

    }

}

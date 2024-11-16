using Capstone_Flowershop;
using Capstone_Flowershop.AdminForms.AccountsMaintenance;
using Capstone_Flowershop.AdminForms.History_Logs;
using Capstone_Flowershop.AdminForms.ProductMaintenance;
using Capstone_Flowershop.AdminForms.Reports.SalesReports;
using Capstone_Flowershop.AdminForms.System_Maintenance;
using Flowershop_Thesis.InventoryClerk.Disposal;
using Flowershop_Thesis.InventoryClerk.LandingPage;
using Flowershop_Thesis.InventoryClerk.Restocking;
using Flowershop_Thesis.InventoryClerk.StockAdjustment;
using Flowershop_Thesis.InventoryClerk.Supplier;
using Flowershop_Thesis.OtherForms.DisposalContents;
using Flowershop_Thesis.SalesClerk.PriceList;
using Flowershop_Thesis.SalesClerk.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.MainForms
{
    public partial class CustomRoleList : UserControl
    {
        public CustomRoleList()
        {
            InitializeComponent();
        }
        #region FinishedQueue
        private string FormName;

        [Category("ActivityList")]
        public string formname //ID
        {
            get { return FormName; }
            set { FormName = value; FormBtn.Text = value; }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            switch (FormName)
            {
                case "Reports":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    Reports RFrm = new Reports();
                    RFrm.TopLevel = false;
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(RFrm);
                    RFrm.BringToFront();
                    RFrm.Show();
                    break;
                case "ProductMaintenance":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    ProductMaintenanceFrm PMf = new ProductMaintenanceFrm();
                    PMf.TopLevel = false;
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(PMf);
                    PMf.BringToFront();
                    PMf.Show();
                    break;
                case "AccountsMaintenance":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    AccountMaintenance AMf = new AccountMaintenance();
                    AMf.TopLevel = false;
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(AMf);
                    AMf.BringToFront();
                    AMf.Show();
                    break;
                case "HistoryLogs":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    HistoryLogs HLf = new HistoryLogs();
                    HLf.TopLevel = false;
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(HLf);
                    HLf.BringToFront();
                    HLf.Show();
                    break;
                case "SystemMaintenance":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    SystemMaintenance SMf = new SystemMaintenance();
                    SMf.TopLevel = false;
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(SMf);
                    SMf.BringToFront();
                    SMf.Show();
                    break;
                case "Disposal":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    DisposalMain DF = new DisposalMain(); //tatawagin tapos papangalanan yung form na papalabasin
                    DF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(DF);
                    DF.BringToFront(); //front yung form 
                    DF.Show(); //para lumitaw
                    break;

                case "Overview":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    DashboardFrm OF = new DashboardFrm(); //tatawagin tapos papangalanan yung form na papalabasin
                    OF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(OF); //ilalagay na natin yung form
                    OF.BringToFront(); //front yung form 
                    OF.Show(); //para lumitaw
                    break;

                case "Restocking":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    RestockNew RF = new RestockNew(); //tatawagin tapos papangalanan yung form na papalabasin
                    RF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(RF);
                    RF.BringToFront(); //front yung form 
                    RF.Show(); //para lumitaw
                    break;

                case "StockAdjustment":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    StockAdjustmentFrmcs SAF = new StockAdjustmentFrmcs(); //tatawagin tapos papangalanan yung form na papalabasin
                    SAF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(SAF);
                    SAF.BringToFront(); //front yung form 
                    SAF.Show(); //para lumitaw
                    break;

                case "Supplier":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    SupplierFrm SF = new SupplierFrm(); //tatawagin tapos papangalanan yung form na papalabasin
                    SF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(SF);
                    SF.BringToFront(); //front yung form 
                    SF.Show(); //para lumitaw
                    break;
                case "Transaction":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    TransactionForm TF = new TransactionForm(); //tatawagin tapos papangalanan yung form na papalabasin
                    TF.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(TF);
                    TF.BringToFront(); //front yung form 
                    TF.Show(); //para lumitaw
                    break;
                case "PriceList":
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Clear();
                    PriceList PL = new PriceList(); //tatawagin tapos papangalanan yung form na papalabasin
                    PL.TopLevel = false; //para di mag agaw ng place
                    CustomAccount_BasePlatform.instance.MainPanel.Controls.Add(PL);
                    PL.BringToFront(); //front yung form 
                    PL.Show(); //para lumitaw
                    break;

                default:
                    MessageBox.Show("Please select a valid form type.");
                    break;
            }
        }
    }
}

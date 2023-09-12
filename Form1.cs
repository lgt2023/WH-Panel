using System;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace WH_Panel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Rectangle workingArea = Screen.GetWorkingArea(this);
            //this.Location = new Point(-1040,1300);
            //DateTime fileCreatedDate = File.GetCreationTime(@"ImperiumTabulaPrincipalis.exe");
            DateTime fileModifiedDate = File.GetLastWriteTime(@"ImperiumTabulaPrincipalis.exe");
            this.Text = "Imperium Tabula Principalis UPDATED " + fileModifiedDate.ToString();
        }
        private void openWHexcelDB(string thePathToFile)
        {
            Process excel = new Process();
            excel.StartInfo.FileName = "C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.exe";
            excel.StartInfo.Arguments = thePathToFile;
            excel.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Process.Start("C:\\1\\source\\repos\\1.Txt");
            var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
            //Process excel = new Process();
            //excel.StartInfo.FileName = "C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.exe";
            //excel.StartInfo.Arguments = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm"; 
            //excel.Start();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\WareHouse\STOCK_CUSTOMERS\NETLINE\NETLINE_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void AuthorizedExcelFileOpening(string fp)
        {
            if (Environment.UserName == "lgt")
            {
                openWHexcelDB(fp);
            }
            else
            {
                MessageBox.Show("Unauthorized ! Access denied !", "Unauthorized ! Access denied !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void btnFIELDIN_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\FIELDIN\\FIELDIN_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void btnWorkProgramm_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\\Data\\DocumentsForProduction\\WORK_PROGRAM.xlsm";
            openWHexcelDB(fp);
        }
        private void btnLEADERTECH_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void btnVAYYAR_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_stock.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void btnSHILAT_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\SHILAT\\SHILAT_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            //Form f = new frmkitLabelPrint();
            //f.Show();
            //f.Focus();
            //var fp = @"\\\\dbr1\\Data\\WareHouse\\KitLabel.xlsm";
            //openWHexcelDB(fp);
            frmkitLabelPrint frmkit = new frmkitLabelPrint();
            frmkit.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\\Data\\WareHouse\\PACKING_SLIPS\\_template.xlsm";
            openWHexcelDB(fp);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            FrmUberSearch frmUber = new FrmUberSearch();
            frmUber.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\CIS\\CIS_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FrmKITShistory frmkit = new FrmKITShistory();
            frmkit.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ST_MICRO\\ST_MICRO_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmPackingSlips fps = new FrmPackingSlips();
            fps.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmkitLabelPrint frmkit = new frmkitLabelPrint();
            frmkit.Show();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            FrmPackingSlipShip ps = new FrmPackingSlipShip();
            ps.Show();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            FrmClientAgnosticWH cl = new FrmClientAgnosticWH();
            cl.Show();
        }
        private void button11_Click_1(object sender, EventArgs e)
        {
            FrmBOM frmBOM = new FrmBOM();
            frmBOM.Show();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_AVL.xlsm";
            AuthorizedExcelFileOpening(fp);
        }
        private void button16_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_AVL.xlsx";
            AuthorizedExcelFileOpening(fp);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\WareHouse\STOCK_CUSTOMERS\NETLINE\NETLINE_AVL.xlsx";
            AuthorizedExcelFileOpening(fp);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_AVL.xlsx";
            AuthorizedExcelFileOpening(fp);
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            FrmFinishedGoodsLog ff = new FrmFinishedGoodsLog();
            ff.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\DIGITRONIX\\DIGITRONIX_STOCK.xlsm";
            AuthorizedExcelFileOpening(fp);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\DIGITRONIX\\DIGITRONIX_AVL.xlsx";
            AuthorizedExcelFileOpening(fp);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenWebAppInBroser();


        }
        static void OpenWebAppInBroser()
        {
            string url = "http://192.168.69.37:81/"; // Change this to the desired web address
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            FrmExcelFormatter fr = new FrmExcelFormatter();
            fr.Show();
            fr.Focus();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            FrmQRPrint fq = new FrmQRPrint();
            fq.Show();
        }
    }
}
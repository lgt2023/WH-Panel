﻿using FastMember;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using GroupBox = System.Windows.Forms.GroupBox;
using TextBox = System.Windows.Forms.TextBox;

namespace WH_Panel
{
    public partial class FrmBomWHS : Form
    {
        public List<KitHistoryItem> fromTheMainBom { get; set; }
        public DataTable misItemsDT = new DataTable();
        public List<KitHistoryItem> misItemsLST = new List<KitHistoryItem>();
        public List<BOMitem> misBOMItemsLST = new List<BOMitem>();
        public List<ClientWarehouse> clList = new List<ClientWarehouse>()
        {
            {new ClientWarehouse
                {
                clName="NETLINE",
                clSuffix="NET",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_STOCK.xlsm"
                }
            },
              {new ClientWarehouse
                {
                clName="LEADER-TECH",
                clSuffix="C100",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_AVL.xlsm",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_STOCK.xlsm"
                 }
              }
            ,
              {new ClientWarehouse
                {
                clName="VAYYAR",
                clSuffix="VAY",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_stock.xlsm"
                }
              },
                 {new ClientWarehouse
                {
                clName="VALENS",
                clSuffix="VAL",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm"
                }
              }
            ,
                 {new ClientWarehouse
                {
                clName="ROBOTRON",
                clSuffix="ROB",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ROBOTRON\\ROBOTRON_AVL.xlsm",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ROBOTRON\\ROBOTRON_STOCK.xlsm"
                }
              }
            ,
                 {new ClientWarehouse
                {
                clName="ENERCON",
                clSuffix="ENE",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ENERCON\\ENERCON_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ENERCON\\ENERCON_STOCK.xlsm"
                }
              },
                   {new ClientWarehouse
                {
                clName="HEPTAGON",
                clSuffix="HEP",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\HEPTAGON\\HEPTAGON_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\HEPTAGON\\HEPTAGON_STOCK.xlsm"
                }
              },
                 {new ClientWarehouse
                {
                clName="DIGITRONIX",
                clSuffix="DGT",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\DIGITRONIX\\DIGITRONIX_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\DIGITRONIX\\DIGITRONIX_STOCK.xlsm"
                }
            },
                 {new ClientWarehouse
                {
                clName="EPS",
                clSuffix="EPS",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\EPS\\EPS_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\EPS\\EPS_STOCK.xlsm"
                }
              }
            ,
                 {new ClientWarehouse
                {
                clName="SOS",
                clSuffix="SOS",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\SOS\\SOS_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\SOS\\SOS_STOCK.xlsm"
                }
              }
            ,
                 {new ClientWarehouse
                {
                clName="ARAN",
                clSuffix="ARN",
                clAvlFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ARAN\\ARAN_AVL.xlsx",
                clStockFile="\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ARAN\\ARAN_STOCK.xlsm"
                }
              }
        };
        private void UpdateControlColors(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Update control colors based on your criteria
                control.BackColor = Color.LightGray;
                control.ForeColor = Color.Black;

                // Handle Button controls separately
                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat; // Set FlatStyle to Flat
                    button.FlatAppearance.BorderColor = Color.DarkGray; // Change border color
                    button.ForeColor = Color.Black;
                }

                // Handle Button controls separately
                if (control is GroupBox groupbox)
                {
                    groupbox.FlatStyle = FlatStyle.Flat; // Set FlatStyle to Flat
                    groupbox.ForeColor = Color.Black;
                }

                // Handle TextBox controls separately
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle; // Set border style to FixedSingle
                    textBox.BackColor = Color.LightGray; // Change background color
                    textBox.ForeColor = Color.Black; // Change text color
                }

                // Handle Label controls separately
                if (control is Label label)
                {
                    label.BorderStyle = BorderStyle.FixedSingle; // Set border style to FixedSingle
                    label.BackColor = Color.Gray; // Change background color
                    label.ForeColor = Color.Black; // Change text color
                }


                // Handle TabControl controls separately
                if (control is TabControl tabControl)
                {
                    //tabControl.BackColor = Color.Black; // Change TabControl background color
                    tabControl.ForeColor = Color.Black;
                    // Handle each TabPage within the TabControl
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        tabPage.BackColor = Color.Gray; // Change TabPage background color
                        tabPage.ForeColor = Color.Black; // Change TabPage text color
                    }
                }

                // Handle DataGridView controls separately
                if (control is DataGridView dataGridView)
                {
                    // Update DataGridView styles
                    dataGridView.EnableHeadersVisualStyles = false;
                    dataGridView.BackgroundColor = Color.DarkGray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Gray;
                    dataGridView.DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
                    dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                    // Change the header cell styles for each column
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        column.HeaderCell.Style.BackColor = Color.DarkGray;
                        column.HeaderCell.Style.ForeColor = Color.Black;
                    }
                }
                // Handle ComboBox controls separately
                if (control is ComboBox comboBox)
                {
                    comboBox.FlatStyle = FlatStyle.Flat; // Set FlatStyle to Flat
                    comboBox.BackColor = Color.DarkGray; // Change ComboBox background color
                    comboBox.ForeColor = Color.Black; // Change ComboBox text color
                }
                // Handle DateTimePicker controls separately
                if (control is DateTimePicker dateTimePicker)
                {
                    // Change DateTimePicker's custom properties here
                    dateTimePicker.BackColor = Color.DarkGray; // Change DateTimePicker background color
                    dateTimePicker.ForeColor = Color.White; // Change DateTimePicker text color
                                                            // Customize other DateTimePicker properties as needed
                }
                // Recursively update controls within containers
                if (control.Controls.Count > 0)
                {
                    UpdateControlColors(control);
                }
            }
        }

        //public string avlNETLINE = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_AVL.xlsx";
        //public string stockNETLINE = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_STOCK.xlsm";
        //public string stockLeader_Tech = @"\\dbr1\Data\WareHouse\STOCK_CUSTOMERS\G.I.Leader_Tech\G.I.Leader_Tech_STOCK.xlsm";
        //public string avlLeader_Tech = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_AVL.xlsm";
        //public string avlVAYAR = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_AVL.xlsx";
        //public string stockVAYAR = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_stock.xlsm";
        //public string avlVALENS = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_AVL.xlsx";
        //public string stockVALENS = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm";
        //public string avlROBOTRON = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ROBOTRON\\ROBOTRON_AVL.xlsm";
        //public string stockROBOTRON = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ROBOTRON\\ROBOTRON_STOCK.xlsm";
        //public string avlENERCON = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ENERCON\\ENERCON_AVL.xlsx";
        //public string stockENERCON = "\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ENERCON\\ENERCON_STOCK.xlsm";

        public string avlFile;
        public string stockFile { get; set; }

        public string projectName = string.Empty;
        public List<WHitem> avlItems = new List<WHitem>();
        public List<WHitem> stockItems = new List<WHitem>();
        public DataTable avlDTable = new DataTable();
        public DataTable stockDTable = new DataTable();
        public int countAVLItems = 0;
        public int countStockItems = 0;
        int iAVL = 0;
        int iStock = 0;
        public FrmBomWHS()
        {
            InitializeComponent();
            UpdateControlColors(this);
        }
        private void FrmBomWHS_Load(object sender, EventArgs e)
        {
            foreach (KitHistoryItem it in fromTheMainBom)
            {
                BOMitem n = new BOMitem
                {
                    IPN = it.IPN,
                    MFPN = it.MFPN,
                    Description = it.Description,
                    WHbalance = 0,
                    QtyInKit = it.QtyInKit,
                    Delta = it.Delta,
                    QtyPerUnit = it.QtyPerUnit,
                    Calc = it.Calc,
                    Alts = it.Alts
                };
                misBOMItemsLST.Add(n);
                //misItemsLST.Add(it);
            }
            //MessageBox.Show(misItemsLST.Count.ToString());
            projectName = fromTheMainBom[0].ProjectName;
            //comboBox1.SelectedItem = warehouseSelectorOnLoad();

            comboBox1.Items.Clear();
            foreach (ClientWarehouse cw in clList)
            {
                comboBox1.Items.Add(cw.clName);
            }
            comboBox1.Sorted = true;
            comboBox1.SelectedItem = warehouseClWHSelectorOnLoad();
            //MasterReload(avlFile, stockFile);
            foreach (BOMitem b in misBOMItemsLST)
            {
                b.WHbalance = calculateWBbalance(b.IPN);
            }
            PopulateMissingGridView();
        }
        public int calculateWBbalance(string IPN)
        {
            int balance = 0;
            try
            {
                var dv = stockDTable.DefaultView;
                dv.RowFilter = $"[IPN] LIKE '%{IPN}%'";
                dataGridView2.DataSource = dv;
                var qtys = new List<int>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (int.TryParse(row.Cells["Stock"].Value?.ToString(), out int qty))
                    {
                        qtys.Add(qty);
                    }
                }
                balance = qtys.Sum();
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return balance;
        }
        private string warehouseClWHSelectorOnLoad()
        {
            string selection = string.Empty;
            foreach (ClientWarehouse clientWH in clList)
                if (misBOMItemsLST[0].IPN.StartsWith(clientWH.clSuffix))
                {
                    selection = clientWH.clName;
                    MasterReload(clientWH.clAvlFile, clientWH.clStockFile);

                }
            return selection;
        }
        //private string warehouseSelectorOnLoad()
        //{
        //    string selection = string.Empty;

        //    if (misBOMItemsLST[0].IPN.StartsWith("C100") || misBOMItemsLST[0].IPN.StartsWith("A00"))
        //    {
        //        selection = "LEADER-TECH";
        //        MasterReload(avlLeader_Tech, stockLeader_Tech);
        //    }
        //    else if (misBOMItemsLST[0].IPN.StartsWith("NET"))
        //    {
        //        selection = "NETLINE";
        //        MasterReload(avlNETLINE, stockNETLINE);
        //    }
        //    else if (misBOMItemsLST[0].IPN.StartsWith("VAY"))
        //    {
        //        selection = "VAYYAR";
        //        MasterReload(avlVAYAR, stockVAYAR);
        //    }
        //    else if (misBOMItemsLST[0].IPN.StartsWith("VAL"))
        //    {
        //        selection = "VALENS";
        //        MasterReload(avlVALENS, stockVALENS);
        //    }
        //    else if (misBOMItemsLST[0].IPN.StartsWith("ENE"))
        //    {
        //        selection = "ENERCON";
        //        MasterReload(avlENERCON, stockENERCON);
        //    }
        //    else
        //    {
        //        selection = "ROBOTRON";
        //        MasterReload(avlROBOTRON, stockROBOTRON);
        //    }
        //    return selection;
        //}
        private void PopulateMissingGridView()
        {
            misItemsDT.Clear();
            IEnumerable<BOMitem> data = misBOMItemsLST;
            using (var reader = ObjectReader.Create(data))
            {
                misItemsDT.Load(reader);
            }
            dataGridView1.DataSource = misItemsDT;
            int perCounter = 0;
            foreach (BOMitem b in misBOMItemsLST)
            {
                if (b.WHbalance >= (b.Delta * -1))
                {
                    perCounter++;
                }
                else
                {
                    //
                }
            }

            SetColumsOrder(dataGridView1);
            double percentageCalc = Convert.ToDouble(misBOMItemsLST.Count);
            double kitPerSim = Math.Round((double)((perCounter / (percentageCalc / 100))), 2);

            groupBox1.Text = String.Format("Missing items : {0} . In stock: {1}/{0} . Simulation:({2})%", misBOMItemsLST.Count, perCounter, kitPerSim);
        }
        private void SetColumsOrder(DataGridView dgw)
        {
            dgw.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgw.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgw.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgw.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgw.Columns["DateOfCreation"].DisplayIndex = 0;
            dgw.Columns["DateOfCreation"].Visible = false;
            //dgw.Columns["ProjectName"].DisplayIndex = 1;
            dgw.Columns["ProjectName"].Visible = false;
            dgw.Columns["IPN"].DisplayIndex = 0;
            dgw.Columns["MFPN"].DisplayIndex = 1;
            dgw.Columns["Description"].DisplayIndex = 2;
            dgw.Columns["WHbalance"].DisplayIndex = 3;
            dgw.Columns["QtyInKit"].DisplayIndex = 4;
            dgw.Columns["Delta"].DisplayIndex = 5;
            //dgw.Columns["QtyPerUnit"].DisplayIndex = 7;
            dgw.Columns["QtyPerUnit"].Visible = false;
            dgw.Columns["Calc"].DisplayIndex = 6;
            dgw.Columns["Alts"].DisplayIndex = 7;
            dgw.Sort(dgw.Columns["IPN"], ListSortDirection.Ascending);
            // Attach CellFormatting event handler
            dgw.CellFormatting += Dgw_CellFormatting;
        }
        private void Dgw_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["WHbalance"].Index && e.RowIndex >= 0)
            {
                var whbalanceCell = dataGridView1.Rows[e.RowIndex].Cells["WHbalance"];
                var deltaCell = dataGridView1.Rows[e.RowIndex].Cells["Delta"];

                if (whbalanceCell.Value != null && deltaCell.Value != null)
                {
                    var whbalanceValue = Convert.ToDecimal(whbalanceCell.Value);
                    var deltaValue = Convert.ToDecimal(deltaCell.Value);

                    if (whbalanceValue + deltaValue >= 0)
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.IndianRed;
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ClientWarehouse client in clList)
            {
                if (comboBox1.Text == client.clSuffix)
                {
                    MasterReload(client.clAvlFile, client.clStockFile);
                    break;
                }
            }
            //if (comboBox1.Text == "ROBOTRON")
            //{
            //    MasterReload(avlROBOTRON, stockROBOTRON);
            //}
            //else if (comboBox1.Text == "LEADER-TECH")
            //{
            //    MasterReload(avlLeader_Tech, stockLeader_Tech);
            //}
            //else if (comboBox1.Text == "NETLINE")
            //{
            //    MasterReload(avlNETLINE, stockNETLINE);
            //}
            //else if (comboBox1.Text == "VAYYAR")
            //{
            //    MasterReload(avlVAYAR, stockVAYAR);
            //}
            //else if (comboBox1.Text == "VALENS")
            //{
            //    MasterReload(avlVALENS, stockVALENS);
            //}
            //else if (comboBox1.Text == "ENERCON")
            //{
            //    MasterReload(avlENERCON, stockENERCON);
            //}
        }
        private void MasterReload(string avlParam, string stockParam)
        {
            avlFile = avlParam;
            stockFile = stockParam;
            label1.BackColor = Color.LightGreen;
            StockViewDataLoader(stockParam, "STOCK");
            button3_Click(this, new EventArgs());
        }
        private void DataLoaderAVL(string fp, string thesheetName)
        {
            try
            {
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fp + "; Extended Properties=\"Excel 12.0 Macro;HDR=NO;IMEX=1\"";
                using (OleDbConnection conn = new OleDbConnection(constr))
                {
                    conn.Open();
                    OleDbCommand command = new OleDbCommand("Select * from [" + thesheetName + "$]", conn);
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WHitem abc = new WHitem
                            {
                                IPN = reader[1].ToString(),
                                Manufacturer = reader[2].ToString(),
                                MFPN = reader[3].ToString(),
                                Description = reader[4].ToString(),
                                Stock = 0,
                                UpdatedOn = string.Empty,
                                ReelBagTrayStick = string.Empty,
                                SourceRequester = string.Empty
                            };
                            if (iAVL > 0)
                            {
                                countAVLItems = iAVL;
                                label1.Text = "Rows in AVL: " + (countAVLItems).ToString();
                                if (countAVLItems % 1000 == 0)
                                {
                                    label1.Update();
                                }
                                avlItems.Add(abc);
                            }
                            iAVL++;
                        }
                    }
                    conn.Close();
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error");
            }
        }
        private void StockViewDataLoader(string fp, string thesheetName)
        {
            try
            {
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fp + "; Extended Properties=\"Excel 12.0 Macro;HDR=YES;IMEX=0\"";
                using (OleDbConnection conn = new OleDbConnection(constr))
                {
                    conn.Open();
                    OleDbCommand command = new OleDbCommand("Select * from [" + thesheetName + "$]", conn);
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                int res = 0;
                                int toStk;
                                bool stk = int.TryParse(reader[4].ToString(), out res);
                                if (stk)
                                {
                                    toStk = res;
                                }
                                else
                                {
                                    toStk = 0;
                                }
                                WHitem abc = new WHitem
                                {
                                    IPN = reader[0].ToString().Trim().Replace(" ", ""),
                                    Manufacturer = reader[1].ToString(),
                                    MFPN = reader[2].ToString(),
                                    Description = reader[3].ToString(),
                                    Stock = toStk,
                                    UpdatedOn = reader[5].ToString(),
                                    ReelBagTrayStick = reader[6].ToString(),
                                    SourceRequester = reader[7].ToString()
                                };
                                countStockItems = iStock;
                                label1.Text = "Rows in STOCK: " + (countStockItems).ToString();
                                if (countStockItems % 1000 == 0)
                                {
                                    label1.Update();
                                }
                                stockItems.Add(abc);
                                iStock++;
                            }
                            catch (Exception E)
                            {
                                MessageBox.Show(E.Message);
                                throw;
                            }
                        }
                    }
                    label1.BackColor = Color.LightGreen;
                    conn.Close();
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error");
            }
        }
        private void PopulateStockView()
        {
            dataGridView2.DataSource = null;
            IEnumerable<WHitem> data = stockItems;
            stockDTable.Clear();
            using (var reader = ObjectReader.Create(data))
            {
                stockDTable.Load(reader);
            }
            dataGridView2.DataSource = stockDTable;
            //dataGridView2.Update();
            SetSTOCKiewColumsOrder();
        }
        private void SetSTOCKiewColumsOrder()
        {
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["IPN"].DisplayIndex = 0;
            dataGridView2.Columns["Manufacturer"].DisplayIndex = 1;
            dataGridView2.Columns["MFPN"].DisplayIndex = 2;
            dataGridView2.Columns["Description"].DisplayIndex = 3;
            dataGridView2.Columns["Stock"].DisplayIndex = 4;
            dataGridView2.Columns["UpdatedOn"].DisplayIndex = 5;
            dataGridView2.Columns["ReelBagTrayStick"].DisplayIndex = 6;
            dataGridView2.Columns["SourceRequester"].DisplayIndex = 7;
            dataGridView2.Sort(dataGridView2.Columns["UpdatedOn"], ListSortDirection.Descending);
        }
        private void FilterStockDataGridView(string IPN)
        {
            try
            {
                DataView dv = stockDTable.DefaultView;
                dv.RowFilter = $"[IPN] LIKE '%{IPN}%'";
                dataGridView2.DataSource = dv;
                List<int> qtys = new List<int>();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (int.TryParse(row.Cells["Stock"].Value?.ToString(), out int qty))
                    {
                        qtys.Add(qty);
                    }
                }
                int balance = qtys.Sum();
                foreach (BOMitem bi in misBOMItemsLST.Where(bi => bi.IPN == IPN))
                {
                    bi.WHbalance = balance;
                }
                label15.Text = $"BALANCE: {balance}";
                label15.BackColor = balance > 0 ? Color.LightGreen : Color.IndianRed;
                label15.Update();
                SetSTOCKiewColumsOrder();
                dataGridView2.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect search pattern, remove invalid character and try again!");
                throw;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                //int columnindex = dataGridView1.CurrentCell.ColumnIndex;
                string cellValue = dataGridView1.Rows[rowindex].Cells[dataGridView1.Columns["IPN"].Index].Value.ToString();
                textBox10.Text = cellValue;
                FilterStockDataGridView(cellValue);
                if (chkBlockInWHonly.Checked)
                {
                    FilterInStockItemsOnly();
                }
            }
        }
        private bool dataGridViewIsBound = false;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewIsBound && dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                //int columnindex = dataGridView1.CurrentCell.ColumnIndex;
                string cellValue = dataGridView1.Rows[rowindex].Cells[dataGridView1.Columns["IPN"].Index].Value.ToString();
                FilterStockDataGridView(cellValue);
            }
        }
        private void dataGridView1_BindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset && dataGridView1.Rows.Count > 0)
            {
                //dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridViewIsBound = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.IndianRed;
            stockItems.Clear();
            stockDTable.Clear();
            countStockItems = 0;
            iStock = 0;
            label1.Text = "RELOAD STOCK";
            StockViewDataLoader(stockFile, "STOCK");
            PopulateStockView();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FilterInStockItemsOnly();
        }
        private void FilterInStockItemsOnly()
        {
            //dataGridView2.DataSource = dv;
            dataGridView2.DataSource = createFilteredInStockDataview();
            dataGridView2.Update();
            SetSTOCKiewColumsOrder();
        }
        private DataView createFilteredInStockDataview()
        {
            List<WHitem> inWHstock = new List<WHitem>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                int res = 0;
                int toStk;
                bool stk = int.TryParse(dataGridView2.Rows[i].Cells[dataGridView2.Columns["Stock"].Index].Value.ToString(), out res);
                if (stk)
                {
                    toStk = res;
                }
                else
                {
                    toStk = 0;
                }
                WHitem wHitemABC = new WHitem()
                {
                    IPN = dataGridView2.Rows[i].Cells[dataGridView2.Columns["IPN"].Index].Value.ToString(),
                    Manufacturer = dataGridView2.Rows[i].Cells[dataGridView2.Columns["Manufacturer"].Index].Value.ToString(),
                    MFPN = dataGridView2.Rows[i].Cells[dataGridView2.Columns["MFPN"].Index].Value.ToString(),
                    Description = dataGridView2.Rows[i].Cells[dataGridView2.Columns["Description"].Index].Value.ToString(),
                    Stock = toStk,
                    UpdatedOn = dataGridView2.Rows[i].Cells[dataGridView2.Columns["UpdatedOn"].Index].Value.ToString(),
                    ReelBagTrayStick = dataGridView2.Rows[i].Cells[dataGridView2.Columns["ReelBagTrayStick"].Index].Value.ToString(),
                    SourceRequester = dataGridView2.Rows[i].Cells[dataGridView2.Columns["SourceRequester"].Index].Value.ToString()
                };
                inWHstock.Add(wHitemABC);
            }
            var negativeQtys = inWHstock.Where(item => item.Stock < 0).ToList();
            var positiveQtys = inWHstock.Where(item => item.Stock > 0).ToList();
            foreach (var negQty in negativeQtys.ToList())
            {
                foreach (var posQty in positiveQtys.ToList())
                {
                    if (Math.Abs(negQty.Stock) == posQty.Stock)
                    {
                        positiveQtys.Remove(posQty);
                        break;
                    }
                }
            }
            var inWHdata = positiveQtys.AsEnumerable();
            var inWHTable = new DataTable();
            using (var reader = ObjectReader.Create(inWHdata))
            {
                inWHTable.Load(reader);
            }
            var dv = inWHTable.DefaultView;
            return dv;
        }
        private void chkBlockInWHonly_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox chk = (System.Windows.Forms.CheckBox)sender;
            if (chk.Checked)
            {
                StockViewDataLoader(stockFile, "STOCK");
                FilterInStockItemsOnly();
            }
            else
            {
                StockViewDataLoader(stockFile, "STOCK");
            }
        }
        private void btnFound_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            string cellValue = dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString();
            string selIPN = dataGridView1.Rows[rowindex].Cells["IPN"].Value.ToString();
            string selMFPN = dataGridView1.Rows[rowindex].Cells["MFPN"].Value.ToString();
            misBOMItemsLST.Remove(misBOMItemsLST.Find(r => r.IPN == selIPN && r.MFPN == selMFPN));
            PopulateMissingGridView();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string _fileTimeStamp = DateTime.Now.ToString("yyyyMMddHHmm");
            //ExportToHTML(dataGridView1, "\\\\dbr1\\Data\\WareHouse\\2023\\WHsearcher\\"+ _fileTimeStamp+"_"+projectName.Substring(0, projectName.Length - 5) + ".html");
            //ExportToHTML20(dataGridView1, "\\\\dbr1\\Data\\WareHouse\\2023\\WHsearcher\\" + _fileTimeStamp + "_" + projectName.Substring(0, projectName.Length - 5) + ".html");
            GenerateHTML();
        }
        // ...
        private void GenerateHTML()
        {
            //string fileName = "output.html";
            string _fileTimeStamp = DateTime.Now.ToString("yyyyMMddHHmm");
            string filename = "\\\\dbr1\\Data\\WareHouse\\2023\\WHsearcher\\" + _fileTimeStamp + "_" + projectName.Substring(0, projectName.Length - 5) + ".html";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("<html style='text-align:center'>");
                writer.WriteLine("<head>");
                writer.WriteLine("<title>" + projectName.Substring(0, projectName.Length - 5) + "</title>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                //writer.WriteLine("<h1>" + projectName.Substring(0, projectName.Length - 5) + "</h1>");
                writer.WriteLine("<table border='1'>");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string IPN = dataGridView1.Rows[i].Cells["IPN"].Value.ToString();
                    writer.WriteLine("<tr>");
                    writer.WriteLine("<td colspan='" + dataGridView1.Columns.Count + "'>");
                    //writer.WriteLine("<h2>Movements log for IPN " + IPN + "</h2>");
                    writer.WriteLine("<table border='1' style='text-align:center; width:auto; margin-right: 0px;margin-left: auto;'>");
                    writer.WriteLine("</tr>");
                    //writer.WriteLine("<tr>");
                    //for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    //{
                    //    if(dataGridView1.Columns[j].HeaderText.ToString()!= "ProjectName")
                    //    {
                    //        if (dataGridView1.Columns[j].HeaderText.ToString() != "DateOfCreation")
                    //        {
                    //            if (dataGridView1.Columns[j].HeaderText.ToString() != "QtyPerUnit")
                    //            {
                    //                if (dataGridView1.Columns[j].HeaderText.ToString() != "Calc")
                    //                {
                    //                    writer.WriteLine("<th>" + dataGridView1.Columns[j].HeaderText + "</th>");
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //writer.WriteLine("</tr>");
                    writer.WriteLine("<tr>");
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Columns[j].HeaderText.ToString() != "ProjectName")
                        {
                            if (dataGridView1.Columns[j].HeaderText.ToString() != "DateOfCreation")
                            {
                                if (dataGridView1.Columns[j].HeaderText.ToString() != "QtyPerUnit")
                                {
                                    if (dataGridView1.Columns[j].HeaderText.ToString() != "Calc")
                                    {
                                        if (dataGridView1.Columns[j].HeaderText.ToString() == "Description")
                                        {
                                            writer.WriteLine("<td style='font-size: x-small'>" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "</td>");
                                        }
                                        else if (dataGridView1.Columns[j].HeaderText.ToString() == "MFPN")
                                        {
                                            writer.WriteLine("<td style='font-size: x-small'>" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "</td>");
                                        }
                                        else
                                        {
                                            writer.WriteLine("<td style='align-vertical:middle;font-size:18px;font-weight: bold'>" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "</td>");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    writer.WriteLine("</tr>");
                    DataView dv = new DataView();
                    var negativeQtys = stockItems.Where(item => item.IPN == IPN && item.Stock < 0).ToList();
                    var positiveQtys = stockItems.Where(item => item.IPN == IPN && item.Stock > 0).ToList();
                    foreach (var negQty in negativeQtys.ToList())
                    {
                        foreach (var posQty in positiveQtys.ToList())
                        {
                            if (Math.Abs(negQty.Stock) == posQty.Stock)
                            {
                                positiveQtys.Remove(posQty);
                                break;
                            }
                        }
                    }
                    var inWHdata = positiveQtys.AsEnumerable();
                    var inWHTable = new DataTable();
                    using (var reader = ObjectReader.Create(inWHdata))
                    {
                        inWHTable.Load(reader);
                    }
                    DataTable filteredData = inWHTable;
                    filteredData.Columns["Manufacturer"].ColumnMapping = MappingType.Hidden;
                    //    for (int k = 0; k < filteredData.Columns.Count; k++)
                    //{
                    //        if(filteredData.Columns[k].ColumnName!= "Manufacturer")
                    //        {
                    //            writer.WriteLine("<th>" + filteredData.Columns[k].ColumnName + "</th>");
                    //        }
                    //}
                    writer.WriteLine("</tr>");
                    for (int l = 0; l < filteredData.Rows.Count; l++)
                    {
                        writer.WriteLine("<tr>");
                        for (int m = 0; m < filteredData.Columns.Count; m++)
                        {
                            if (filteredData.Columns[m].ColumnName != "Manufacturer")
                            {
                                if (filteredData.Columns[m].ColumnName != "IPN")
                                {
                                    if (filteredData.Columns[m].ColumnName == "Description")
                                    {
                                        writer.WriteLine("<td style='font-size: x-small'>" + filteredData.Rows[l][m].ToString() + "</td>");
                                    }
                                    else if (filteredData.Columns[m].ColumnName == "MFPN")
                                    {
                                        writer.WriteLine("<td style='font-size: x-small'>" + filteredData.Rows[l][m].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        writer.WriteLine("<td>" + filteredData.Rows[l][m].ToString() + "</td>");
                                    }
                                }
                            }
                        }
                        writer.WriteLine("</tr>");
                    }
                    writer.WriteLine("</table>");
                    writer.WriteLine("</td>");
                    writer.WriteLine("</tr>");
                }
                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
            // Open the file in default browser
            // Process.Start(fileName);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private void ExportToHTML20(DataGridView dataGridView, string fileName)
        {
            dataGridView.Columns["Calc"].Visible = false;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            // Create HTML header
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<title>" + projectName + "</title>");
            sb.Append("</head>");
            // Create HTML body
            sb.Append("<body>");
            sb.Append("<table border='1px' cellpadding='1px' cellspacing='0' style='text-align:center;magrin-left:auto;margin-right:auto'>");
            sb.Append("<h2 style='text-align:center'>" + projectName + "</h2>");
            // Add header row
            sb.Append("<tr>");
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Visible)
                {
                    sb.Append("<th>" + column.HeaderText + "</th>");
                }
            }
            sb.Append("</tr>");
            // Add data rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                sb.Append("<tr>");
                List<WHitem> filteredByIPN = new List<WHitem>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible)
                    {
                        sb.Append("<td><h4>" + cell.Value + "</h4></td>");
                    }
                    sb.Append("</tr>");
                    if (cell.ColumnIndex == dataGridView.Columns["IPN"].Index)
                    {
                        filteredByIPN = stockItems.Where(n => n.IPN == cell.Value).ToList();
                        if (filteredByIPN.Count > 0)
                        {
                            List<WHitem> negativeStockWHItems = filteredByIPN.Where(item => item.Stock < 0).ToList();
                            List<WHitem> positiveStockWHItems = filteredByIPN.Where(item => item.Stock > 0).ToList();
                            positiveStockWHItems.RemoveAll(item => negativeStockWHItems.Any(negativeItem => Math.Abs(negativeItem.Stock) == item.Stock));
                            DataTable INWH = new DataTable();
                            using (var reader = ObjectReader.Create(positiveStockWHItems))
                            {
                                INWH.Load(reader);
                            }
                            DataView dv = INWH.DefaultView;
                            dataGridView2.DataSource = dv;
                            foreach (DataGridViewRow r2 in dataGridView2.Rows)
                            {
                                sb.Append("<tr>");
                                foreach (DataGridViewCell c2 in r2.Cells)
                                {
                                    //if (c2.Visible)
                                    sb.Append("<td>" + c2.Value + "</td>");
                                }
                                sb.Append("</tr>");
                            }
                        }
                    }
                }
                // Close HTML tags
                sb.Append("</table>");
                sb.Append("</body>");
                sb.Append("</html>");
                // Write HTML to file
                File.WriteAllText(fileName, sb.ToString());
                // Open HTML file in default browser
                //System.Diagnostics.Process.Start(fileName);
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(@fileName)
                {
                    UseShellExecute = true
                };
                p.Start();
                //dataGridView.Columns["Calc"].Visible = true;
                dataGridView2.Columns["SourceRequester"].Visible = true;
                dataGridView2.Columns["Manufacturer"].Visible = true;
                dataGridView2.Columns["Description"].Visible = true;
                dataGridView.Columns["Calc"].Visible = true;
            }
        }
        private void ExportToHTML(DataGridView dataGridView, string fileName)
        {
            dataGridView.Columns["Calc"].Visible = false;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            // Create HTML header
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<title>" + projectName + "</title>");
            sb.Append("</head>");
            // Create HTML body
            sb.Append("<body>");
            sb.Append("<table border='1px' cellpadding='1px' cellspacing='0' style='text-align:center;magrin-left:auto;margin-right:auto'>");
            sb.Append("<h2 style='text-align:center;padding:0px'>" + projectName + "</h2>");
            // Add header row
            sb.Append("<tr>");
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Visible)
                {
                    sb.Append("<th>" + column.HeaderText + "</th>");
                }
            }
            sb.Append("</tr>");
            // Add data rows
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                sb.Append("<tr>");
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible)
                    {
                        sb.Append("<td><h4>" + cell.Value + "</h4></td>");
                    }
                }
                sb.Append("</tr>");
                //DataView dv = INWH.DefaultView;
                sb.Append("<tr>");
                //DataView dv = createFilteredInStockDataview();
                List<WHitem> inWHstock = new List<WHitem>();
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    int res = 0;
                    int toStk;
                    bool stk = int.TryParse(dataGridView2.Rows[i].Cells[dataGridView2.Columns["Stock"].Index].Value.ToString(), out res);
                    if (stk)
                    {
                        toStk = res;
                    }
                    else
                    {
                        toStk = 0;
                    }
                    WHitem wHitemABC = new WHitem()
                    {
                        IPN = dataGridView2.Rows[i].Cells[dataGridView2.Columns["IPN"].Index].Value.ToString(),
                        Manufacturer = dataGridView2.Rows[i].Cells[dataGridView2.Columns["Manufacturer"].Index].Value.ToString(),
                        MFPN = dataGridView2.Rows[i].Cells[dataGridView2.Columns["MFPN"].Index].Value.ToString(),
                        Description = dataGridView2.Rows[i].Cells[dataGridView2.Columns["Description"].Index].Value.ToString(),
                        Stock = toStk,
                        UpdatedOn = dataGridView2.Rows[i].Cells[dataGridView2.Columns["UpdatedOn"].Index].Value.ToString(),
                        ReelBagTrayStick = dataGridView2.Rows[i].Cells[dataGridView2.Columns["ReelBagTrayStick"].Index].Value.ToString(),
                        SourceRequester = dataGridView2.Rows[i].Cells[dataGridView2.Columns["SourceRequester"].Index].Value.ToString()
                    };
                    inWHstock.Add(wHitemABC);
                }
                List<WHitem> negatiVEQTYs = new List<WHitem>();
                for (int i = 0; i < inWHstock.Count; i++)
                {
                    if (inWHstock[i].Stock < 0)
                    {
                        negatiVEQTYs.Add(inWHstock[i]);
                    }
                }
                List<WHitem> positiveInWH = new List<WHitem>();
                for (int k = 0; k < inWHstock.Count; k++)
                {
                    if (inWHstock[k].Stock > 0)
                    {
                        positiveInWH.Add(inWHstock[k]);
                    }
                }
                for (int i = 0; i < negatiVEQTYs.Count; i++)
                {
                    for (int j = 0; j < positiveInWH.Count; j++)
                    {
                        if (Math.Abs(negatiVEQTYs[i].Stock) == positiveInWH[j].Stock)
                        {
                            positiveInWH.Remove((WHitem)positiveInWH[j]);
                            break;
                        }
                    }
                }
                IEnumerable<WHitem> WHdata = positiveInWH;
                DataTable INWH = new DataTable();
                using (var reader = ObjectReader.Create(WHdata))
                {
                    INWH.Load(reader);
                }
                DataView dv = INWH.DefaultView;
                dv.RowFilter = "[IPN] LIKE '%" + row.Cells["IPN"].Value + "%'";
                dataGridView2.DataSource = dv;
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    dataGridView2.Columns["SourceRequester"].Visible = false;
                    dataGridView2.Columns["Manufacturer"].Visible = false;
                    dataGridView2.Columns["Description"].Visible = false;
                    if (column.Visible)
                    {
                        //sb.Append("<th>" + column.HeaderText + "</th>");
                    }
                }
                sb.Append("</tr>");
                foreach (DataGridViewRow r2 in dataGridView2.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataGridViewCell c2 in r2.Cells)
                    {
                        //if (c2.Visible)
                        sb.Append("<td>" + c2.Value + "</td>");
                    }
                    sb.Append("</tr>");
                }
            }
            // Close HTML tags
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");
            // Write HTML to file
            File.WriteAllText(fileName, sb.ToString());
            // Open HTML file in default browser
            //System.Diagnostics.Process.Start(fileName);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@fileName)
            {
                UseShellExecute = true
            };
            p.Start();
            //dataGridView.Columns["Calc"].Visible = true;
            dataGridView2.Columns["SourceRequester"].Visible = true;
            dataGridView2.Columns["Manufacturer"].Visible = true;
            dataGridView2.Columns["Description"].Visible = true;
            dataGridView.Columns["Calc"].Visible = true;
        }

        private void btnFontIncrease_Click(object sender, EventArgs e)
        {
            // Increase font size of DataGridView1 rows
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, dataGridView1.DefaultCellStyle.Font.Size + 1);
            dataGridView1.RowsDefaultCellStyle.Font = dataGridView1.DefaultCellStyle.Font;
            dataGridView1.AlternatingRowsDefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, dataGridView1.DefaultCellStyle.Font.Size + 1);

            // Increase font size of DataGridView1 headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView1.ColumnHeadersDefaultCellStyle.Font.Size + 1);

            // Update row height for DataGridView1
            dataGridView1.RowTemplate.Height = (int)(dataGridView1.RowTemplate.Height * 1.2);

            // Increase font size of DataGridView2 rows
            dataGridView2.DefaultCellStyle.Font = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, dataGridView2.DefaultCellStyle.Font.Size + 1);
            dataGridView2.RowsDefaultCellStyle.Font = dataGridView2.DefaultCellStyle.Font;
            dataGridView2.AlternatingRowsDefaultCellStyle.Font = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, dataGridView2.DefaultCellStyle.Font.Size + 1);

            // Increase font size of DataGridView2 headers
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView2.ColumnHeadersDefaultCellStyle.Font.Size + 1);

            // Update row height for DataGridView2
            dataGridView2.RowTemplate.Height = (int)(dataGridView2.RowTemplate.Height * 1.2);

        }

        private void btnFontDecrease_Click(object sender, EventArgs e)
        {
            // Decrease font size of DataGridView1 rows
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, dataGridView1.DefaultCellStyle.Font.Size - 1);
            dataGridView1.RowsDefaultCellStyle.Font = dataGridView1.DefaultCellStyle.Font;
            dataGridView1.AlternatingRowsDefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, dataGridView1.DefaultCellStyle.Font.Size - 1);

            // Decrease font size of DataGridView1 headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView1.ColumnHeadersDefaultCellStyle.Font.Size - 1);

            // Update row height for DataGridView1
            dataGridView1.RowTemplate.Height = (int)(dataGridView1.RowTemplate.Height / 1.2);

            // Decrease font size of DataGridView2 rows
            dataGridView2.DefaultCellStyle.Font = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, dataGridView2.DefaultCellStyle.Font.Size - 1);
            dataGridView2.RowsDefaultCellStyle.Font = dataGridView2.DefaultCellStyle.Font;
            dataGridView2.AlternatingRowsDefaultCellStyle.Font = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, dataGridView2.DefaultCellStyle.Font.Size - 1);

            // Decrease font size of DataGridView2 headers
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView2.ColumnHeadersDefaultCellStyle.Font.FontFamily, dataGridView2.ColumnHeadersDefaultCellStyle.Font.Size - 1);

            // Update row height for DataGridView2
            dataGridView2.RowTemplate.Height = (int)(dataGridView2.RowTemplate.Height / 1.2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateHTMLsim();
        }
        private void GenerateHTMLsim()
        {
            string _fileTimeStamp = DateTime.Now.ToString("yyyyMMddHHmm");
            string filename = "\\\\dbr1\\Data\\WareHouse\\2023\\WHsim\\" + _fileTimeStamp + "_" + projectName.Substring(0, projectName.Length - 5) + ".html";

            using (StreamWriter writer = new StreamWriter(filename))
            {
                StringBuilder htmlContent = new StringBuilder();

                // Start writing the HTML table
                htmlContent.AppendLine("<html>");
                htmlContent.AppendLine("<head>");
                htmlContent.AppendLine("<title>" + projectName.Substring(0, projectName.Length - 5) + " SIMULATION" + "</title>");
                htmlContent.AppendLine("</head>");
                htmlContent.AppendLine("<body>");
                htmlContent.Append("<h2 style='text-align:center;padding:0px'>" + projectName.Substring(0, projectName.Length - 5) + "</h2>");
                htmlContent.Append("<h3 style='text-align:center;padding:0px'>" + groupBox1.Text.ToString() + "</h3>");
                htmlContent.AppendLine("<table border='1'>");

                // Write the column headers
                // Write the column headers in specific order
                htmlContent.AppendLine("<tr>");
                //WriteHtmlHeaderCell(htmlContent, "IPN");
                WriteHtmlHeaderCell(htmlContent, "IPN", "ipnHeader"); //
                WriteHtmlHeaderCell(htmlContent, "MFPN");
                WriteHtmlHeaderCell(htmlContent, "Description");
                WriteHtmlHeaderCell(htmlContent, "WHbalance");
                //WriteHtmlHeaderCell(htmlContent, "Delta");
                WriteHtmlHeaderCell(htmlContent, "Delta"); //
                WriteHtmlHeaderCell(htmlContent, "DELTA", "deltaHeader");
                htmlContent.AppendLine("</tr>");
                htmlContent.AppendLine("</tr>");

                // Map column names to their respective indices in dataGridView1
                Dictionary<string, int> columnIndexMap = new Dictionary<string, int>();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    string columnName = column.HeaderText;
                    if (columnName == "IPN" || columnName == "MFPN" || columnName == "Description" || columnName == "WHbalance" || columnName == "Delta")
                    {
                        columnIndexMap[columnName] = column.Index;
                    }
                }

                // Write the rows and data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    htmlContent.AppendLine("<tr>");

                    WriteHtmlCell(htmlContent, row, columnIndexMap, "IPN");
                    WriteHtmlCell(htmlContent, row, columnIndexMap, "MFPN");
                    WriteHtmlCell(htmlContent, row, columnIndexMap, "Description");
                    WriteHtmlCell(htmlContent, row, columnIndexMap, "WHbalance");
                    WriteHtmlCell(htmlContent, row, columnIndexMap, "Delta");
                    WriteMissingCell(htmlContent, row, columnIndexMap); // Write "Missing" cell
                    htmlContent.AppendLine("</tr>");
                }

                // Finish writing the HTML table
                htmlContent.AppendLine("</table>");

                htmlContent.AppendLine(GenerateJavascript());

                htmlContent.AppendLine("</body>");
                htmlContent.AppendLine("</html>");

                // Write the content to the file
                writer.Write(htmlContent.ToString());

                var p = new Process();
                p.StartInfo = new ProcessStartInfo(@filename)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
        }
        private void WriteHtmlHeaderCell(StringBuilder htmlContent, string text, string id = "")
        {
            htmlContent.Append("<th");
            if (!string.IsNullOrEmpty(id))
            {
                htmlContent.AppendFormat(" id='{0}'", id);
            }
            if (text == "Delta")
            {
                text = "REQUIRED";
            }
            if (id == "ipnHeader" || id == "deltaHeader")
            {
                htmlContent.AppendFormat("><a href='#' onclick='sortTableByColumn({0}); return false;'>{1}</a>", id == "ipnHeader" ? 0 : 4, text);
            }
            else
            {
                htmlContent.AppendFormat(">{0}", text);
            }

            htmlContent.Append("</th>");
        }

        private void WriteHtmlCell(StringBuilder sb, DataGridViewRow row, Dictionary<string, int> columnIndexMap, string columnName)
        {
            int columnIndex = columnIndexMap[columnName];
            DataGridViewCell cell = row.Cells[columnIndex];
            if (columnName == "Delta")
            {
                sb.AppendLine("<td>" + Math.Abs(int.Parse(cell.Value.ToString())) + "</td>");
            }
            else
            {
                sb.AppendLine("<td>" + cell.Value + "</td>");
            }

        }

        private string GenerateJavascript()
        {
            StringBuilder jsContent = new StringBuilder();
            jsContent.AppendLine("<script>");
            jsContent.AppendLine("window.onload = function() {");
            jsContent.AppendLine("var table = document.getElementsByTagName('table')[0];");
            jsContent.AppendLine("var deltaHeader = table.rows[0].cells[5];"); // Delta column header cell
            jsContent.AppendLine("var ipnHeader = table.rows[0].cells[1];"); // IPN column header cell

            // Add click event listeners to enable sorting
            jsContent.AppendLine("deltaHeader.addEventListener('click', function() {");
            jsContent.AppendLine("sortTableByColumn(table, 5);"); // Sort by Delta column (index 4)
            jsContent.AppendLine("});");
            jsContent.AppendLine("ipnHeader.addEventListener('click', function() {");
            jsContent.AppendLine("sortTableByColumn(table, 1);"); // Sort by IPN column (index 1)
            jsContent.AppendLine("});");

            // Sorting function
            jsContent.AppendLine("function sortTableByColumn(table, columnIndex) {");
            jsContent.AppendLine("var rows = Array.from(table.rows).slice(1);"); // Skip the header row
            jsContent.AppendLine("rows.sort(function(a, b) {");
            jsContent.AppendLine("var aValue = a.cells[columnIndex].textContent;");
            jsContent.AppendLine("var bValue = b.cells[columnIndex].textContent;");
            jsContent.AppendLine("return aValue.localeCompare(bValue, undefined, { numeric: true, sensitivity: 'base' });"); // Numeric sorting
            jsContent.AppendLine("});");
            jsContent.AppendLine("for (var i = 0; i < rows.length; i++) {");
            jsContent.AppendLine("table.appendChild(rows[i]);");
            jsContent.AppendLine("}");
            jsContent.AppendLine("}");

            // Rest of your existing code
            jsContent.AppendLine("for (var i = 1; i < table.rows.length; i++) {");
            jsContent.AppendLine("var deltaCell = table.rows[i].cells[4];");
            jsContent.AppendLine("var whBalanceCell = table.rows[i].cells[3];");
            jsContent.AppendLine("var deltaValue = parseInt(deltaCell.textContent);");
            jsContent.AppendLine("var whBalanceValue = parseInt(whBalanceCell.textContent);");
            jsContent.AppendLine("if (Math.abs(deltaValue) <= whBalanceValue) {");
            jsContent.AppendLine("table.rows[i].style.backgroundColor = 'lightgreen';");
            jsContent.AppendLine("} else {");
            jsContent.AppendLine("table.rows[i].style.backgroundColor = 'lightcoral';");
            jsContent.AppendLine("}");
            jsContent.AppendLine("}");

            jsContent.AppendLine("}");
            jsContent.AppendLine("</script>");

            return jsContent.ToString();
        }

        private void WriteMissingCell(StringBuilder sb, DataGridViewRow row, Dictionary<string, int> columnIndexMap)
        {
            int whbalanceColumnIndex = columnIndexMap["WHbalance"];
            int deltaColumnIndex = columnIndexMap["Delta"];

            DataGridViewCell whbalanceCell = row.Cells[whbalanceColumnIndex];
            DataGridViewCell deltaCell = row.Cells[deltaColumnIndex];

            int whbalanceValue = Convert.ToInt32(whbalanceCell.Value);
            int deltaValue = Convert.ToInt32(deltaCell.Value);
            int missingValue = whbalanceValue - Math.Abs(deltaValue);

            sb.AppendLine("<td>" + missingValue + "</td>");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmClientAgnosticWH h = new FrmClientAgnosticWH();
            foreach (ClientWarehouse cw in clList)
            {
                if (cw != null && comboBox1.Text == cw.clName)
                {
                    h.SetComboBoxText(cw.clName);
                    h.MasterReload(cw.clAvlFile, cw.clStockFile);
                    h.Show();
                }
            }

        }
    }
}

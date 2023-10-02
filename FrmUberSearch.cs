﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using FastMember;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using Label = System.Windows.Forms.Label;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Button = System.Windows.Forms.Button;
using GroupBox = System.Windows.Forms.GroupBox;
using TextBox = System.Windows.Forms.TextBox;
using ComboBox = System.Windows.Forms.ComboBox;

namespace WH_Panel
{
    public partial class FrmUberSearch : Form
    {
        public FrmUberSearch()
        {
            InitializeComponent();

        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            txtbColorGreenOnEnter((TextBox)sender);
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            txtbColorWhiteOnLeave((TextBox)sender);
        }

        public List<WHitem> wHitems = new List<WHitem>();
        public DataTable UDtable = new DataTable();
        public int countItems = 0;
        int i = 0;
        public class KeyValueList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
        {
            public void Add(TKey key, TValue value)
            {
                Add(new KeyValuePair<TKey, TValue>(key, value));
            }
        }
        private void FrmUberSearch_Load(object sender, EventArgs e)
        {
            UpdateControlColors(this);
            startUpLogic();

        }
        private void startUpLogic()
        {
            List<Label> _seachableFieldsLabels = new List<Label>();
            _seachableFieldsLabels.Add(label2);
            _seachableFieldsLabels.Add(label4);
            _seachableFieldsLabels.Add(label5);
            _seachableFieldsLabels.Add(label9);
            foreach (Label l in _seachableFieldsLabels)
            {
                l.BackColor = Color.LightGreen;
            }



            List<TextBox> _searchableFieldsTextBoxes = new List<TextBox>();
            _searchableFieldsTextBoxes.Add(textBox2);
            _searchableFieldsTextBoxes.Add(textBox4);
            _searchableFieldsTextBoxes.Add(textBox5);
            _searchableFieldsTextBoxes.Add(textBox9);

            foreach (TextBox textBox in _searchableFieldsTextBoxes)
            {
                textBox.Enter += TextBox_Enter;
                textBox.Leave += TextBox_Leave;
            }

            label1.BackColor = Color.IndianRed;
            var listOfWareHouses = new KeyValueList<string, string>
                   {
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm", "STOCK" },
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_stock.xlsm","STOCK" },
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_STOCK.xlsm","STOCK" },
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\FIELDIN\\FIELDIN_STOCK.xlsm","STOCK" },
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ARAN\\ARAN_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\DIGITRONIX\\DIGITRONIX_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\ENERCON\\ENERCON_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\EPS\\EPS_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\HEPTAGON\\HEPTAGON_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\SOLANIUM\\SOLANIUM_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\SONOTRON\\SONOTRON_STOCK.xlsm","STOCK"},
                        {"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\SOS\\SOS_STOCK.xlsm","STOCK"}
                   };
            for (int i = 0; i < listOfWareHouses.Count; i++)
            {
                DataLoader(listOfWareHouses[i].Key, listOfWareHouses[i].Value);
            }
            PopulateGridView();

            // Create a list to hold the buttons
            List<Button> buttons = new List<Button>();

            // Create and add the buttons to the list
            for (int i = 0; i < listOfWareHouses.Count; i++)
            {
                string warehousePath = listOfWareHouses[i].Key; // Get the warehouse path from the KeyValuePair

                // Extract the warehouse name from the warehouse path
                string[] pathParts = warehousePath.Split('\\');
                string warehouseName = pathParts[pathParts.Length - 2];

                Button button = new Button();
                button.Text = warehouseName;
                button.Tag = warehousePath;
                button.AutoSize = true; // Adjust the button size based on the text length
                button.Click += Button_Click; // Assign a common event handler for button click event

                buttons.Add(button); // Add the button to the list
            }

            // Sort the buttons alphabetically based on their text
            buttons.Sort((x, y) => x.Text.CompareTo(y.Text));

            flowLayoutPanel1.Controls.Clear();
            // Add the sorted buttons to the flowLayoutPanel1 control
            foreach (Button button in buttons)
            {
                flowLayoutPanel1.Controls.Add(button); // Add the button to the FlowLayoutPanel
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string warehousePath = (string)clickedButton.Tag;
            string[] pathParts = warehousePath.Split('\\');
            string warehouseName = pathParts[pathParts.Length - 2];

            FrmClientAgnosticWH w = new FrmClientAgnosticWH();
            w.Show();
            w.Focus();

            // Call the public method to set the ComboBox text
            w.SetComboBoxText(warehouseName);
        }
        private void DataLoader(string fp, string thesheetName)
        {
            try
            {
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fp + "; Extended Properties=\"Excel 12.0 Macro;HDR=YES;IMEX=1\"";
                using (OleDbConnection conn = new OleDbConnection(constr))
                {
                    conn.Open();
                    OleDbCommand command = new OleDbCommand("Select * from [" + thesheetName + "$]", conn);
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int number;
                            bool success = int.TryParse(reader[4].ToString(), out number);
                            if (success)
                            {
                                WHitem abc = new WHitem
                                {
                                    IPN = reader[0].ToString(),
                                    Manufacturer = reader[1].ToString(),
                                    MFPN = reader[2].ToString(),
                                    Description = reader[3].ToString(),
                                    Stock = number,
                                    UpdatedOn = reader[5].ToString(),
                                    ReelBagTrayStick = reader[6].ToString(),
                                    SourceRequester = reader[7].ToString()
                                };
                                if (i > 0)
                                {
                                    countItems = i;
                                    label1.Text = "Rows:" + (countItems).ToString();
                                    if (countItems % 5000 == 0)
                                    { label1.Update(); }
                                    wHitems.Add(abc);
                                }
                                i++;
                            }
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
        private void PopulateGridView()
        {
            //MessageBox.Show(wHitems.Count.ToString()); 
            IEnumerable<WHitem> data = wHitems;
            //UDtable.Clear();
            using (var reader = ObjectReader.Create(data))
            {
                UDtable.Load(reader);
            }
            dataGridView1.DataSource = UDtable;
            SetColumsOrder();
            label1.BackColor = Color.LightGreen;
        }
        private void SetColumsOrder()
        {
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["IPN"].DisplayIndex = 0;
            dataGridView1.Columns["Manufacturer"].DisplayIndex = 1;
            dataGridView1.Columns["MFPN"].DisplayIndex = 2;
            dataGridView1.Columns["Description"].DisplayIndex = 3;
            dataGridView1.Columns["Stock"].DisplayIndex = 4;
            dataGridView1.Columns["UpdatedOn"].DisplayIndex = 5;
            dataGridView1.Columns["ReelBagTrayStick"].DisplayIndex = 6;
            dataGridView1.Columns["SourceRequester"].DisplayIndex = 7;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ResetViews();
            startUpLogic();
            //SetColumsOrder();
            //ResetViews();
            //LoadDataFromFile();
            //PopulateGW();
        }
        private void ResetViews()
        {
            wHitems.Clear();
            countItems = 0;
            i = 0;
            label1.Text = "";
            UDtable.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.BackColor = Color.IndianRed;
            FilterTheDataGridView();
        }
        private void FilterTheDataGridView()
        {
            try
            {
                DataView dv = UDtable.DefaultView;
                dv.RowFilter = "[IPN] LIKE '%" + textBox2.Text.ToString() +
                    "%' AND [MFPN] LIKE '%" + textBox4.Text.ToString() +
                    "%' AND [Description] LIKE '%" + textBox5.Text.ToString() +
                    "%' AND [SourceRequester] LIKE '%" + textBox9.Text.ToString() +
                    "%'";
                dataGridView1.DataSource = dv;
                SetColumsOrder();
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect search pattern, remove invalid character and try again !");
                throw;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label4.BackColor = Color.IndianRed;
            FilterTheDataGridView();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label5.BackColor = Color.IndianRed;
            FilterTheDataGridView();
        }
        private void openWHexcelDB(string thePathToFile)
        {
            Process excel = new Process();
            excel.StartInfo.FileName = "C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.exe";
            excel.StartInfo.Arguments = thePathToFile;
            excel.Start();
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //MessageBox.Show(Environment.MachineName.ToString());   
        //    if (Environment.MachineName.ToString() == "RT12")
        //    {
        //        var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\VALENS\\VALENS_STOCK.xlsm";
        //        openWHexcelDB(fp);
        //    }
        //    else
        //    {
        //        MessageBox.Show("ACCESS DENIED");
        //    }
        //}
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    if (Environment.MachineName.ToString() == "RT12")
        //    {
        //        var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\VAYAR\\VAYAR_stock.xlsm";
        //        openWHexcelDB(fp);
        //    }
        //    else
        //    {
        //        MessageBox.Show("ACCESS DENIED");
        //    }
        //}
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    if (Environment.MachineName.ToString() == "RT12")
        //    {
        //        var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\FIELDIN\\FIELDIN_STOCK.xlsm";
        //        openWHexcelDB(fp);
        //    }
        //    else
        //    {
        //        MessageBox.Show("ACCESS DENIED");
        //    }
        //}
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (Environment.MachineName.ToString() == "RT12")
        //    {
        //        var fp = @"\\\\dbr1\Data\\WareHouse\\STOCK_CUSTOMERS\\G.I.Leader_Tech\\G.I.Leader_Tech_STOCK.xlsm";
        //        openWHexcelDB(fp);
        //    }
        //    else
        //    {
        //        MessageBox.Show("ACCESS DENIED");
        //    }
        //}
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            label9.BackColor = Color.IndianRed;
            FilterTheDataGridView();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            label2.BackColor = Color.LightGreen;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            label4.BackColor = Color.LightGreen;
        }
        private void label5_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            label5.BackColor = Color.LightGreen;
        }
        private void label9_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            label9.BackColor = Color.LightGreen;
        }
        private void label2_DoubleClick(object sender, EventArgs e)
        {
            clearAllsearchTextboxes();
        }
        private void clearAllsearchTextboxes()
        {
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            label2.BackColor = Color.LightGreen;
            label4.BackColor = Color.LightGreen;
            label5.BackColor = Color.LightGreen;
            label9.BackColor = Color.LightGreen;
        }
        private void label4_DoubleClick(object sender, EventArgs e)
        {
            clearAllsearchTextboxes();
        }
        private void label5_DoubleClick(object sender, EventArgs e)
        {
            clearAllsearchTextboxes();
        }
        private void label9_DoubleClick(object sender, EventArgs e)
        {
            clearAllsearchTextboxes();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (Environment.MachineName.ToString() == "RT12")
            {
                var fp = @"\\\\dbr1\\Data\\WareHouse\\STOCK_CUSTOMERS\\NETLINE\\NETLINE_STOCK.xlsm";
                openWHexcelDB(fp);
            }
            else
            {
                MessageBox.Show("ACCESS DENIED");
            }
        }

        private void UpdateControlColors(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Update control colors based on your criteria
                control.BackColor = Color.LightGray;
                control.ForeColor = Color.White;

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
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.Gray;
                    dataGridView.DefaultCellStyle.BackColor = Color.Gray;
                    dataGridView.DefaultCellStyle.ForeColor = Color.White;
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

        private void textBox3_Enter(object sender, EventArgs e)
        {
            txtbColorGreenOnEnter(sender);
        }
        private static void txtbColorGreenOnEnter(object sender)
        {
            TextBox? tb = (TextBox)sender;
            tb.BackColor = Color.LightGreen;
        }
        private static void txtbColorWhiteOnLeave(object sender)
        {
            TextBox? tb = sender as TextBox;
            tb.BackColor = Color.LightGray;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            txtbColorWhiteOnLeave(sender);
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            txtbColorGreenOnEnter(sender);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            txtbColorWhiteOnLeave(sender);
        }
        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MessageBox.Show(e.ColumnIndex.ToString());
        //}
    }
}

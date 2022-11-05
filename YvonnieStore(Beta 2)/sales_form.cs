using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Security.Cryptography;
using System.Drawing.Printing;

namespace YvonnieStore_Beta_2_
{
    public partial class sales_form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(111111, 999999);
        public sales_form()
        {
            InitializeComponent();
            fill_SalesView(); fill_BillsView(); fill_SalaryView();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        private void sales_form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
        }
        public void fill_SalesView()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                salesForm_sales_view_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from sales_table where sales_type = '" + "CASH" + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["sales_date"].ToString());
                    item.SubItems.Add(read["sales_cash"].ToString());
                    item.SubItems.Add(read["sales_credit"].ToString());
                    item.SubItems.Add(read["sales_total"].ToString());
                    item.SubItems.Add(read["sales_cashier"].ToString());
                    item.SubItems.Add(read["sales_gross"].ToString());
                    item.SubItems.Add(read["sales_net"].ToString());
                    item.SubItems.Add(read["sales_transactotal"].ToString());

                    salesForm_sales_view_lstView.Items.Add(item);
                    salesForm_sales_view_lstView.FullRowSelect = true;




                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }
        public void fill_BillsView()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                salesForm_bills_view_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from bills_table";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["bills_ID "].ToString());
                    item.SubItems.Add(read["bills_Accnum"].ToString());
                    item.SubItems.Add(read["bills_transactionname"].ToString());
                    item.SubItems.Add(read["bills_dateDue"].ToString());
                    item.SubItems.Add(read["bills_dueAmount"].ToString());
                    item.SubItems.Add(read["bills_amountPayed"].ToString());
                    item.SubItems.Add(read["bills_datePayed"].ToString());
                    item.SubItems.Add(read["bills_change"].ToString());
                    item.SubItems.Add(read["bills_receiptNumber"].ToString());

                    salesForm_bills_view_lstView.Items.Add(item);
                    salesForm_bills_view_lstView.FullRowSelect = true;




                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }
        public void fill_SalaryView()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                salesForm_Salaries_view_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from salaries_table";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["salary_employee_ID"].ToString());
                    item.SubItems.Add(read["salary_payout_date"].ToString());
                    item.SubItems.Add(read["salary_payout_received"].ToString());
                    item.SubItems.Add(read["salary_payout_amount"].ToString());
                    item.SubItems.Add(read["salary_deductions"].ToString());

                    salesForm_Salaries_view_lstView.Items.Add(item);
                    salesForm_Salaries_view_lstView.FullRowSelect = true;




                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }

        private void salesForm_sales_view_lstView_DoubleClick(object sender, EventArgs e)
        {
            int i = 0;
            ListViewItem SalesList = salesForm_sales_view_lstView.SelectedItems[i];
            salesForm_sales_detailed_date_lbl.Text = SalesList.SubItems[0].Text;
            salesForm_sales_detailed_cashSales_lbl.Text = SalesList.SubItems[1].Text;
            salesForm_sales_detailed_creditSales_lbl.Text = SalesList.SubItems[2].Text;
            salesForm_sales_detailed_sales_lbl.Text = SalesList.SubItems[3].Text;
            salesForm_sales_detailed_cashier_lbl.Text = SalesList.SubItems[4].Text;
            salesForm_sales_detailed_gross_lbl.Text = SalesList.SubItems[5].Text;
            salesForm_sales_detailed_net_lbl.Text = SalesList.SubItems[6].Text;
            salesForm_sales_detailed_numoftransacmade_lbl.Text = SalesList.SubItems[7].Text;

        }

        private void salesForm_bills_view_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            ListViewItem SalesList = salesForm_bills_view_lstView.SelectedItems[i];
            salesForm_billsandwages_detailed_accnum_lbl.Text = SalesList.SubItems[1].Text;
            salesForm_billsandwages_detailed_transacname_lbl.Text = SalesList.SubItems[2].Text;
            salesForm_billsandwages_detailed_duedate_lbl.Text = SalesList.SubItems[3].Text;
            salesForm_billsandwages_detailed_dueamount_lbl.Text = SalesList.SubItems[4].Text;
            salesForm_billsandwages_detailed_amountpayed_lbl.Text = SalesList.SubItems[5].Text;
            salesForm_billsandwages_detailed_datepayed_lbl.Text = SalesList.SubItems[6].Text;
            salesForm_billsandwages_detailed_change_lbl.Text = SalesList.SubItems[7].Text;
            salesForm_billsandwages_detailed_receipt_lbl.Text = SalesList.SubItems[8].Text;

            //MySqlConnection conn = new MySqlConnection(myConnection);

            //MySqlCommand copro3 = new MySqlCommand();
            //conn.Open();
            //copro3.Connection = conn;
            //copro3.CommandText = "select * from bills_table where bills_ID   = '" + SalesList.SubItems[1].Text + "'";
            //MySqlDataReader copro = copro3.ExecuteReader();
            //while (copro.Read())
            //{

            //    salesForm_sales_detailed_gross_lbl.Text = copro["bills_Accnum"].ToString();
            //    salesForm_sales_detailed_net_lbl.Text = copro["bills_transactionname"].ToString();
            //    salesForm_sales_detailed_numoftransacmade_lbl.Text = copro["bills_dateDue"].ToString();
            //    salesForm_sales_detailed_cashier_lbl.Text = copro["sales_cashier"].ToString();
            //}
            //conn.Close();

            //MySqlDataAdapter Adapter = new MySqlDataAdapter(copro3);
            //DataSet SetDatas = new DataSet();
            //Adapter.Fill(SetDatas);
        }

        private void salesForm_Salaries_view_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            ListViewItem SalesList = salesForm_Salaries_view_lstView.SelectedItems[i];
            salesForm_billsandwages_detailed_accnum_lbl.Text = SalesList.SubItems[1].Text;
            salesForm_billsandwages_detailed_transacname_lbl.Text = SalesList.SubItems[2].Text;
            salesForm_billsandwages_detailed_duedate_lbl.Text = SalesList.SubItems[3].Text;
            salesForm_billsandwages_detailed_dueamount_lbl.Text = SalesList.SubItems[4].Text;
            salesForm_billsandwages_detailed_amountpayed_lbl.Text = SalesList.SubItems[5].Text;
            salesForm_billsandwages_detailed_datepayed_lbl.Text = SalesList.SubItems[6].Text;
            salesForm_billsandwages_detailed_change_lbl.Text = SalesList.SubItems[7].Text;
            salesForm_billsandwages_detailed_receipt_lbl.Text = SalesList.SubItems[8].Text;
        }

        private void salesForm_addnoninventorysales_btn_Click(object sender, EventArgs e)
        {
            salesForm_noninventoryID_lbl.Text = NumberRandom.ToString(); salesForm_addnoninventorybox_grpBox.Visible = true; panel3.Enabled = false; panel1.Enabled = false;
        }

        private void salesForm_Canceladdsales_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_noninventoryID_lbl.Text = NumberRandom.ToString();
            salesForm_addnoninventorybox_grpBox.Visible = false; salesForm_nonsalesname_txtBox.Clear(); salesForm_nonsalesTotal_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true;
        }

        private void salesForm_addnonsales_btn_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();
             MySqlCommand command = new MySqlCommand("insert into sales_table (sales_noninventoryID,sales_noninventoryname,sales_noninventorytotal,sales_noninventorydate,sales_type) values " +
                "('" + salesForm_noninventoryID_lbl.Text + "','" + salesForm_nonsalesname_txtBox.Text + "','" + salesForm_nonsalesTotal_txtBox.Text + "','" + DateTime.Now.ToString() + "','" + "NONINVENTORY " + "')", connection);

            command.ExecuteNonQuery();

            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_noninventoryID_lbl.Text = NumberRandom.ToString();
            salesForm_addnoninventorybox_grpBox.Visible = false; salesForm_nonsalesname_txtBox.Clear(); salesForm_nonsalesTotal_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true;
            MessageBox.Show("Non-inventory added successfully!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void stopdebug_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

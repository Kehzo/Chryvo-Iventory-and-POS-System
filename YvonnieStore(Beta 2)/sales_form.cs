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
            fill_SalesView(); fill_BillsView(); fill_SalaryView(); resetFields();
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
        public void resetFields()
        {
            salesForm_sales_detailed_numoftransacmade_lbl.Text = "-"; salesForm_sales_detailed_date_lbl.Text = "-"; salesForm_sales_detailed_cashSales_lbl.Text = "-"; salesForm_sales_detailed_creditSales_lbl.Text = "-";
            salesForm_sales_detailed_sales_lbl.Text = "-"; salesForm_sales_detailed_cashier_lbl.Text = "-"; salesForm_sales_detailed_gross_lbl.Text = "-";
            salesForm_sales_detailed_net_lbl.Text = "-"; salesForm_salary_detailed_employeeID_lbl.Text = "-"; salesForm_salary_detailed_payoutdate_lbl.Text = "-";
            salesForm_salary_detailed_payoutamount_lbl.Text = "-"; salesForm_salary_detailed_deduction_lbl.Text = "-"; salesForm_salary_detailed_received_lbl.Text = "-";
            salesForm_billsandwages_detailed_accnum_lbl.Text = "-"; salesForm_billsandwages_detailed_transacname_lbl.Text = "-"; salesForm_billsandwages_detailed_duedate_lbl.Text = "-";
            salesForm_billsandwages_detailed_dueamount_lbl.Text = "-"; salesForm_billsandwages_detailed_amountpayed_lbl.Text = "-"; salesForm_billsandwages_detailed_datepayed_lbl.Text = "-";
            salesForm_billsandwages_detailed_change_lbl.Text = "-"; salesForm_billsandwages_detailed_receipt_lbl.Text = "-";
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
        public void populateEmployeeComboBox()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                salesForm_addSalaryRecord_employeeID_cmbBox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from useraccounts ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    salesForm_addSalaryRecord_employeeID_cmbBox.Items.Add(read["id_number"].ToString());
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
            salesForm_SalaryDetailedView_grpBox.Visible = false; salesForm_billDetailedView_grpBox.Visible = true;
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
            salesForm_SalaryDetailedView_grpBox.Visible = true; salesForm_billDetailedView_grpBox.Visible = false;
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
            MySqlConnection conn = new MySqlConnection(myConnection);

            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            string query0 = "select * from sales_table where sales_noninventoryID  = '" + salesForm_noninventoryID_lbl.Text + "'";
            command.CommandText = query0;
            MySqlDataReader read = command.ExecuteReader();

            int count = 0;
            while (read.Read())
            {
                count++;
            }

            if (count >= 1)
            {
                salesForm_noninventoryID_lbl.Text = "";
                Random rand = new Random();
                NumberRandom = rand.Next(111111, 999999);
                salesForm_noninventoryID_lbl.Text = NumberRandom.ToString();
                MessageBox.Show("Duplicate non-inventory found with the same ID, Creating nother new ID number. Please click add again. Thank you.", "Duplicate ID found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (count == 0)
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand commandz = new MySqlCommand("insert into sales_table (sales_noninventoryID,sales_noninventoryname,sales_noninventorytotal,sales_noninventorydate,sales_type) values " +
                   "('" + salesForm_noninventoryID_lbl.Text + "','" + salesForm_nonsalesname_txtBox.Text + "','" + salesForm_nonsalesTotal_txtBox.Text + "','" + DateTime.Now.ToString() + "','" + "NONINVENTORY" + "')", connection);

                commandz.ExecuteNonQuery();

                Random rand = new Random();
                NumberRandom = rand.Next(111111, 999999);
                salesForm_noninventoryID_lbl.Text = NumberRandom.ToString();
                salesForm_addnoninventorybox_grpBox.Visible = false; salesForm_nonsalesname_txtBox.Clear(); salesForm_nonsalesTotal_txtBox.Clear();
                panel3.Enabled = true; panel1.Enabled = true;
                MessageBox.Show("Non-inventory added successfully!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            conn.Close();
        }

        private void stopdebug_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void salesForm_addnewbill_Cancel_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_addnewbill_billID_lbl.Text = NumberRandom.ToString();
            salesForm_addnewbillRecord_grpBox.Visible = false; salesForm_addnewbill_billnumber_txtBox.Clear(); salesForm_addnewbill_transactionName_txtBox.Clear();
            salesForm_addnewbill_Duedate_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_dueamount_txtBox.Clear(); salesForm_addnewbill_amountpaid_txtBox.Clear();
            salesForm_addnewbill_DOP_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_change_txtBox.Clear(); salesForm_addnewbill_receiptnumber_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true; salesForm_SalaryDetailedView_grpBox.Visible = true;
        }

        private void salesForm_billsandwages_addbill_btn_Click(object sender, EventArgs e)
        {
            salesForm_addnewbillRecord_grpBox.Visible = true; panel3.Enabled = false; panel1.Enabled = false; salesForm_SalaryDetailedView_grpBox.Visible = false;
            salesForm_addnewbill_billID_lbl.Text = NumberRandom.ToString();
        }

        private void salesForm_addnewbill_add_btn_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();
            MySqlCommand command = new MySqlCommand("insert into bills_table (bills_ID,bills_Accnum,bills_transactionname,bills_dateDue,bills_dueAmount,bills_amountPayed,bills_datePayed,bills_change,bills_receiptNumber,bills_addedBy) values " +
               "('" + salesForm_addnewbill_billID_lbl.Text + "','" + salesForm_addnewbill_billnumber_txtBox.Text + "','" + salesForm_addnewbill_transactionName_txtBox.Text + "','" + salesForm_addnewbill_Duedate_DTpicker.Text + "','" + salesForm_addnewbill_dueamount_txtBox.Text + "','" + salesForm_addnewbill_amountpaid_txtBox.Text + "','" + salesForm_addnewbill_DOP_DTpicker.Text + "','" + salesForm_addnewbill_change_txtBox.Text + "','" + salesForm_addnewbill_receiptnumber_txtBox.Text + "','" + salesForm_user_Firstname.Text + "')", connection);

            command.ExecuteNonQuery();

            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_addnewbill_billID_lbl.Text = NumberRandom.ToString();
            salesForm_addnewbillRecord_grpBox.Visible = false; salesForm_addnewbill_billnumber_txtBox.Clear(); salesForm_addnewbill_transactionName_txtBox.Clear();
            salesForm_addnewbill_Duedate_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_dueamount_txtBox.Clear(); salesForm_addnewbill_amountpaid_txtBox.Clear();
            salesForm_addnewbill_DOP_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_change_txtBox.Clear(); salesForm_addnewbill_receiptnumber_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true; salesForm_SalaryDetailedView_grpBox.Visible = true; fill_BillsView();
            MessageBox.Show("Bill record added successfully!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void salesForm_addSalaryRecord_employeeID_cmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            salesForm_addSalaryRecord_employeeID_cmbBox.Items.Clear();
            connection.Close();

            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string query = "select * from useraccounts where id_number = '" + salesForm_addSalaryRecord_employeeID_cmbBox.Text + "'";
            command.CommandText = query;
            MySqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                salesForm_addSalaryForm_selectedID_lbl.Text = (read["firstname"].ToString()) + " " + (read["lastname"].ToString());
            }
            connection.Close();
        }

        private void salesForm_addSalaryRecord_Add_btn_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();
            MySqlCommand command = new MySqlCommand("insert into salaries_table (salary_ID,salary_employee_ID,salary_payout_date,salary_payout_received,salary_payout_amount,salary_deductions,salary_addedBy) values " +
               "('" + salesForm_addSalaryRecord_RecordID_lbl.Text + "','" + salesForm_addSalaryRecord_employeeID_cmbBox.Text + "','" + salesForm_addSalaryRecord_PayoutDate_DTpicker.Text + "','" + salesForm_addSalaryRecord_payoutAmount_txtBox.Text + "','" + salesForm_addSalaryRecord_SalaryDeduc_txtBox.Text + "','" + salesForm_addSalaryRecord_PayoutReceived_txtBox.Text + "','" + salesForm_user_Firstname.Text + "')", connection);

            command.ExecuteNonQuery();

            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_addSalaryRecord_RecordID_lbl.Text = NumberRandom.ToString();
            salesForm_addnewbillRecord_grpBox.Visible = false; salesForm_addnewbill_billnumber_txtBox.Clear(); salesForm_addnewbill_transactionName_txtBox.Clear();
            salesForm_addnewbill_Duedate_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_dueamount_txtBox.Clear(); salesForm_addnewbill_amountpaid_txtBox.Clear();
            salesForm_addnewbill_DOP_DTpicker.Text = DateTime.Now.ToString(); salesForm_addnewbill_change_txtBox.Clear(); salesForm_addnewbill_receiptnumber_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true; salesForm_SalaryDetailedView_grpBox.Visible = true; fill_SalaryView();
            MessageBox.Show("Salary record added successfully!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void salesForm_addSalaryRecord_cancel_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            NumberRandom = rand.Next(111111, 999999);
            salesForm_addSalaryRecord_RecordID_lbl.Text = NumberRandom.ToString();
            salesForm_addnewSalaryRecord_grpBox.Visible = false; salesForm_addSalaryRecord_employeeID_cmbBox.Items.Clear(); salesForm_addSalaryRecord_PayoutDate_DTpicker.Text = DateTime.Now.ToString();
            salesForm_addnewbill_Duedate_DTpicker.Text = DateTime.Now.ToString();
            salesForm_addSalaryRecord_payoutAmount_txtBox.Clear(); salesForm_addSalaryRecord_SalaryDeduc_txtBox.Clear(); salesForm_addSalaryRecord_PayoutReceived_txtBox.Clear();
            panel3.Enabled = true; panel1.Enabled = true; salesForm_SalaryDetailedView_grpBox.Visible = true;
        }

        private void salesForm_salaries_addSalary_btn_Click(object sender, EventArgs e)
        {
            salesForm_addnewSalaryRecord_grpBox.Visible = true; panel3.Enabled = false; panel1.Enabled = false; salesForm_SalaryDetailedView_grpBox.Visible = false;
            salesForm_addSalaryRecord_RecordID_lbl.Text = NumberRandom.ToString();
        }

        private void salesForm_bills_refreshList_btn_Click(object sender, EventArgs e)
        {
            fill_BillsView();
        }

        private void salesForm_salaries_refreshList_btn_Click(object sender, EventArgs e)
        {
            fill_SalaryView();
        }

        private void salesForm_sales_refresh_btn_Click(object sender, EventArgs e)
        {
            fill_SalesView();
        }

        private void salesForm_Home_Btn_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = salesForm_user_Firstname.Text;
            toPOSForm.POS_user_idnumber.Text = salesForm_user_idnumber.Text;
            toPOSForm.account_level.Text = account_level.Text;
            this.Close();
            toPOSForm.Show();
        }

        private void salesForm_Supplier_Btn_Click(object sender, EventArgs e)
        {
            supplier_Form toSupplier = new supplier_Form();
            toSupplier.Supplier_user_Firstname.Text = salesForm_user_Firstname.Text;
            toSupplier.Supplier_user_idnumber.Text = salesForm_user_idnumber.Text;
            toSupplier.account_level.Text = account_level.Text;
            this.Close();
            toSupplier.Show();
        }

        private void salesForm_Customer_Btn_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = salesForm_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = salesForm_user_idnumber.Text;
            toCustomer.account_level.Text = account_level.Text;
            this.Close();
            toCustomer.Show();
        }

        private void salesForm_Inventory_Btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_Firstname.Text = salesForm_user_Firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = salesForm_user_idnumber.Text;
            toInventory.account_level.Text = account_level.Text;
            this.Close();
            toInventory.Show();
        }

        private void salesForm_Users_Btn_Click(object sender, EventArgs e)
        {
            employees_Form toUserForm = new employees_Form();
            toUserForm.employeeForm_user_firstname.Text = salesForm_user_Firstname.Text;
            toUserForm.employeeForm_user_idnumber.Text = salesForm_user_idnumber.Text;
            toUserForm.account_level.Text = account_level.Text;
            this.Close();
            toUserForm.Show();
        }

        private void salesForm_Transactions_Btn_Click(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = salesForm_user_Firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = salesForm_user_idnumber.Text;
            toTransactionForm.account_level.Text = account_level.Text;
            this.Close();
            toTransactionForm.Show();
        }

        private void salesForm_sales_search_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (salesForm_sales_search_txtBox.Text == "")
            {
                fill_SalesView();
            }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    salesForm_sales_view_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM sales_table WHERE sales_date LIKE '%" + salesForm_sales_search_txtBox.Text + "%' OR sales_cashier LIKE '%" + salesForm_sales_search_txtBox.Text + "%' OR sales_type LIKE '%" + salesForm_sales_search_txtBox.Text + "%'";
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
                catch (Exception x)
                {
                    MessageBox.Show("ERROR SA SEARCH SALES. Contact admin");
                }
            }
        }

        private void salesForm_billsandwages_search_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (salesForm_bills_search_txtBox.Text == "")
            {
                fill_BillsView();
            }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    salesForm_bills_view_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM bills_table WHERE bills_Accnum LIKE '%" + salesForm_bills_search_txtBox.Text + "%' OR bills_transactionname LIKE '%" + salesForm_bills_search_txtBox.Text + "%' OR bills_receiptNumber LIKE '%" + salesForm_bills_search_txtBox.Text + "%'";
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
                catch (Exception x)
                {
                    MessageBox.Show("ERROR SA SEARCH BILLS. Contact admin");
                }
            }
        }

        private void salesForm_salary_search_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (salesForm_salary_search_txtBox.Text == "")
            {
                fill_SalaryView();
            }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    salesForm_Salaries_view_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM salaries_table WHERE salary_employee_ID LIKE '%" + salesForm_salary_search_txtBox.Text + "%' OR salary_payout_date LIKE '%" + salesForm_salary_search_txtBox.Text + "%' OR salary_addedBy LIKE '%" + salesForm_salary_search_txtBox.Text + "%'";
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
                catch (Exception x)
                {
                    MessageBox.Show("ERROR SA SEARCH BILLS. Contact admin");
                }
            }
        }
    }
}

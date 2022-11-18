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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YvonnieStore_Beta_2_
{
    public partial class Transaction_Form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public Transaction_Form()
        {
            InitializeComponent();
            fill_TransactionList();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(000000000, 999999999);

        private void Transaction_Form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
        }
        public void fill_TransactionList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                transactionForm_viewTransactionLog_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from transaction_table";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["transaction_ID"].ToString());
                    item.SubItems.Add(read["transaction_item_ID"].ToString());
                    item.SubItems.Add(read["transaction_item_name"].ToString());
                    item.SubItems.Add(read["transaction_item_count"].ToString());
                    item.SubItems.Add(read["transaction_item_category"].ToString());
                    item.SubItems.Add(read["transaction_item_price"].ToString());
                    item.SubItems.Add(read["transaction_item_product_line"].ToString());
                    item.SubItems.Add(read["transaction_item_supplier"].ToString());
                    item.SubItems.Add(read["transaction_item_additional_details"].ToString());
                    item.SubItems.Add(read["transaction_item_expiration_date"].ToString());
                    item.SubItems.Add(read["transaction_item_item_subtotal"].ToString());
                    item.SubItems.Add(read["transaction_total_price"].ToString());
                    item.SubItems.Add(read["transaction_cash_rendered"].ToString());
                    item.SubItems.Add(read["transaction_change_rendered"].ToString());
                    item.SubItems.Add(read["transaction_user"].ToString());
                    item.SubItems.Add(read["transaction_date_made"].ToString());
                    item.SubItems.Add(read["transaction_status"].ToString());
                    item.SubItems.Add(read["transaction_customer_name"].ToString());

                    transactionForm_viewTransactionLog_lstView.Items.Add(item);
                    transactionForm_viewTransactionLog_lstView.FullRowSelect = true;

                }
                connection.Close();
            }

            catch (Exception ec)
            {
                MessageBox.Show("ERROR: " + ec);
            }
       }

        private void transactionForm_HistoryList_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void stopdebug_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = Transaction_user_Firstname.Text;
            toPOSForm.POS_user_idnumber.Text = Transaction_user_idnumber.Text;
            toPOSForm.account_level.Text = account_level.Text;
            this.Close();
            toPOSForm.Show();
        }

        private void POSForm_inventory_btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_Firstname.Text = Transaction_user_Firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = Transaction_user_idnumber.Text;
            toInventory.account_level.Text = account_level.Text;
            this.Close();
            toInventory.Show();
        }

        private void POS_toUsers_btn_Click(object sender, EventArgs e)
        {
            employees_Form toEmployeeForm = new employees_Form();
            toEmployeeForm.employeeForm_user_firstname.Text = Transaction_user_Firstname.Text;
            toEmployeeForm.employeeForm_user_idnumber.Text = Transaction_user_idnumber.Text;
            toEmployeeForm.account_level.Text = account_level.Text;
            this.Close();
            toEmployeeForm.Show();
        }

        private void transanctionForm_Sales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = Transaction_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = Transaction_user_idnumber.Text;
            toSalesForm.account_level.Text = account_level.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void transactionForm_toSuppliers_btn_Click(object sender, EventArgs e)
        {
            supplier_Form toSupplier = new supplier_Form();
            toSupplier.Supplier_user_Firstname.Text = Transaction_user_Firstname.Text;
            toSupplier.Supplier_user_idnumber.Text = Transaction_user_idnumber.Text;
            toSupplier.account_level.Text = account_level.Text;
            this.Close();
            toSupplier.Show();
        }

        private void transactionForm_toCustomer_btn_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = Transaction_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = Transaction_user_idnumber.Text;
            toCustomer.account_level.Text = account_level.Text;
            this.Close();
            toCustomer.Show();
        }

        private void transactionForm_refreshlist_btn_Click(object sender, EventArgs e)
        {
            fill_TransactionList();
        }

        private void transactionForm_displayOptions_search_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (transactionForm_displayOptions_search_txtBox.Text == "")
            {
                fill_TransactionList();
            }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    transactionForm_viewTransactionLog_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM transaction_table WHERE transaction_ID LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%' OR transaction_user LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%' OR transaction_customer_name LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%'";
                    command.CommandText = query;
                    MySqlDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {

                        ListViewItem item = new ListViewItem(read["transaction_ID"].ToString());
                        item.SubItems.Add(read["transaction_item_ID"].ToString());
                        item.SubItems.Add(read["transaction_item_name"].ToString());
                        item.SubItems.Add(read["transaction_item_count"].ToString());
                        item.SubItems.Add(read["transaction_item_category"].ToString());
                        item.SubItems.Add(read["transaction_item_price"].ToString());
                        item.SubItems.Add(read["transaction_item_product_line"].ToString());
                        item.SubItems.Add(read["transaction_item_supplier"].ToString());
                        item.SubItems.Add(read["transaction_item_additional_details"].ToString());
                        item.SubItems.Add(read["transaction_item_expiration_date"].ToString());
                        item.SubItems.Add(read["transaction_item_item_subtotal"].ToString());
                        item.SubItems.Add(read["transaction_total_price"].ToString());
                        item.SubItems.Add(read["transaction_cash_rendered"].ToString());
                        item.SubItems.Add(read["transaction_change_rendered"].ToString());
                        item.SubItems.Add(read["transaction_user"].ToString());
                        item.SubItems.Add(read["transaction_date_made"].ToString());
                        item.SubItems.Add(read["transaction_status"].ToString());
                        item.SubItems.Add(read["transaction_customer_name"].ToString());

                        transactionForm_viewTransactionLog_lstView.Items.Add(item);
                        transactionForm_viewTransactionLog_lstView.FullRowSelect = true;

                    }
                    connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show("ERROR SA SEARCH TRANSACTION. Contact admin");
                }
            }
        }

        private void transactionForm_displayoption_search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                transactionForm_viewTransactionLog_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "SELECT * FROM transaction_table WHERE transaction_ID LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%' OR transaction_user LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%' OR transaction_customer_name LIKE '%" + transactionForm_displayOptions_search_txtBox.Text + "%'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["transaction_ID"].ToString());
                    item.SubItems.Add(read["transaction_item_ID"].ToString());
                    item.SubItems.Add(read["transaction_item_name"].ToString());
                    item.SubItems.Add(read["transaction_item_count"].ToString());
                    item.SubItems.Add(read["transaction_item_category"].ToString());
                    item.SubItems.Add(read["transaction_item_price"].ToString());
                    item.SubItems.Add(read["transaction_item_product_line"].ToString());
                    item.SubItems.Add(read["transaction_item_supplier"].ToString());
                    item.SubItems.Add(read["transaction_item_additional_details"].ToString());
                    item.SubItems.Add(read["transaction_item_expiration_date"].ToString());
                    item.SubItems.Add(read["transaction_item_item_subtotal"].ToString());
                    item.SubItems.Add(read["transaction_total_price"].ToString());
                    item.SubItems.Add(read["transaction_cash_rendered"].ToString());
                    item.SubItems.Add(read["transaction_change_rendered"].ToString());
                    item.SubItems.Add(read["transaction_user"].ToString());
                    item.SubItems.Add(read["transaction_date_made"].ToString());
                    item.SubItems.Add(read["transaction_status"].ToString());
                    item.SubItems.Add(read["transaction_customer_name"].ToString());

                    transactionForm_viewTransactionLog_lstView.Items.Add(item);
                    transactionForm_viewTransactionLog_lstView.FullRowSelect = true;

                }
                connection.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show("ERROR SA SEARCH TRANSACTION. Contact admin");
            }
        }
    }
}

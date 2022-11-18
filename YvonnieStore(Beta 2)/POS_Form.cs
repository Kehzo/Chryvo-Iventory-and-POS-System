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
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YvonnieStore_Beta_2_
{
    public partial class POS_Form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public POS_Form()
        {
            InitializeComponent();
            fill_POSwithProducts();
            populateCustomerComboBoxUpdate();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(000000000, 999999999);
        private void POS_Form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
            POSForm_shoppingCart_totalitems_lbl.Text = "0";
            POSForm_shoppingCart_transactionNumber_lbl.Text = NumberRandom.ToString();
            //FormBorderStyle = FormBorderStyle.None;
           // WindowState = FormWindowState.Maximized;
        }
        public void fill_POSwithProducts()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                POSForm_items_list_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //string query = "select * from inventory_table WHERE stock_forReplacement = '" + "false" +"' AND stock_forScrap = '" + "false" + "' AND stock_status = '" + 1 + "' ";
                string query = "select * from inventory_table WHERE stock_status = '" + 1 + "' ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    int lowValue = Convert.ToInt32(read["stock_low_stock_alert"].ToString());
                    ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                    item.SubItems.Add(read["stock_product_name"].ToString());
                    item.SubItems.Add(read["stock_additional_details"].ToString());
                    item.SubItems.Add(read["stock_selling_price"].ToString());
                    item.SubItems.Add(read["stock_number_of_items"].ToString());
                    if (Convert.ToInt32(read["stock_number_of_items"].ToString()) == 0)
                    {
                        item.BackColor = Color.Red;
                    }
                    else if (Convert.ToInt32(read["stock_number_of_items"].ToString()) <= lowValue)
                    {
                        item.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        item.BackColor = Color.Transparent;
                    }
                    item.SubItems.Add(read["stock_category"].ToString());
                    item.SubItems.Add(read["stock_purchase_price"].ToString());

                    POSForm_items_list_lstView.Items.Add(item);
                    POSForm_items_list_lstView.FullRowSelect = true;




                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void populateCustomerComboBoxUpdate()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                POSForm_customerinfo_name_lbl.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from customer_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    POSForm_customerinfo_name_lbl.Items.Add(read["customer_ID"].ToString());
                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login toLogin = new login();
            this.Close();
            toLogin.Show();
        }

        private void POSForm_inventory_btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_picture.Image = POS_employeeCurrentPhoto_pctrBox.Image;
            toInventory.inventoryForm_user_Firstname.Text = POS_user_Firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = POS_user_idnumber.Text;
            toInventory.account_level.Text = account_level.Text;
            this.Close();
            toInventory.Show();
            
        }

        private void POS_toUsers_btn_Click(object sender, EventArgs e)
        {
            employees_Form toEmployeeForm = new employees_Form();
            //toEmployeeForm.employeeForm_employeeCurrentPicture_pctrBox.Image = POS_employeeCurrentPhoto_pctrBox.Image;
            toEmployeeForm.employeeForm_user_firstname.Text = POS_user_Firstname.Text;
            toEmployeeForm.employeeForm_user_idnumber.Text = POS_user_idnumber.Text;
            toEmployeeForm.account_level.Text = account_level.Text;
            this.Close();
            toEmployeeForm.Show();
        }

        private void POSForm_logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand command2 = connection.CreateCommand();
                string query1 = "update useraccounts set last_out = '" + DateTime.Now.ToString() + "' where id_number  = '" + POS_user_idnumber.Text + "'";
                command2.CommandText = query1;
                command2.ExecuteNonQuery();
                MessageBox.Show("You are now logged out!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
                //Environment.Exit(0);
            }
        }
        public void populateCart()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                POSForm_shoppingCart_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from cart_table WHERE cart_transactionID = '" + POSForm_shoppingCart_transactionNumber_lbl.Text + "' && cart_status = '" + "ACTIVE" +"' ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["cart_stockIDNumber"].ToString());
                    item.SubItems.Add(read["cart_productName"].ToString());
                    item.SubItems.Add(read["cart_numberofItems"].ToString());
                    item.SubItems.Add(read["cart_itemPrice"].ToString());
                    item.SubItems.Add(read["cart_subtotal"].ToString());
                    item.SubItems.Add(read["cart_category"].ToString());
                    item.SubItems.Add(read["cart_profit"].ToString());

                    POSForm_shoppingCart_lstView.Items.Add(item);
                    POSForm_shoppingCart_lstView.FullRowSelect = true;
                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }
        public void addtoCart()
        {
            int i = 0;
            ListViewItem selectedItemOnInventory = POSForm_items_list_lstView.SelectedItems[i];
            MySqlConnection conn = new MySqlConnection(myConnection);
                conn.Close();
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select * from cart_table where cart_stockIDNumber  = '" + selectedItemOnInventory.SubItems[0].Text + "' && cart_status  = '" + "ACTIVE" + "'";

                MySqlDataReader read = command.ExecuteReader();

                int count = 0;
                while (read.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    MessageBox.Show("Item is already in the cart, remove it first then update quantity");
                    POSForm_Quantity_txtBox.Text = "1";
                    POSForm_Quantity_txtBox.Focus();
                    conn.Close();
                }
                else if (count > 1)
                {
                    MessageBox.Show("Item has 1 or more duplicates in the cart. Please contact admin");
                    POSForm_Quantity_txtBox.Text = "1";
                    POSForm_Quantity_txtBox.Focus();
                    conn.Close();
                }
                else
                {
                string containsThis = selectedItemOnInventory.SubItems[0].Text;
                decimal profit1 = Convert.ToDecimal(selectedItemOnInventory.SubItems[3].Text) - Convert.ToDecimal(selectedItemOnInventory.SubItems[6].Text); decimal profit = profit1 * Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                decimal total = Convert.ToDecimal(selectedItemOnInventory.SubItems[3].Text) * Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                int numberofItemsonList = Convert.ToInt32(selectedItemOnInventory.SubItems[4].Text); int numbertoRemove = Convert.ToInt32(POSForm_Quantity_txtBox.Text);
                int toDatabaseValue = numberofItemsonList - numbertoRemove;
                    conn.Close();
                    conn.Open();
                    MySqlCommand command2 = conn.CreateCommand();
                    MySqlCommand command68 = new MySqlCommand("insert into cart_table (cart_transactionID,cart_transactionItemNumber,cart_stockIDNumber,cart_productName,cart_itemPrice,cart_numberofItems,cart_subtotal,cart_total,cart_category,cart_addedBy,cart_DateTime,cart_status,cart_profit,cart_additionalDetails) values" +
                        " ( '" + POSForm_shoppingCart_transactionNumber_lbl.Text + "','" + POSForm_shoppingCart_totalitems_lbl.Text + "','" + selectedItemOnInventory.SubItems[0].Text + "','" + selectedItemOnInventory.SubItems[1].Text + "','" + selectedItemOnInventory.SubItems[3].Text + "','" + POSForm_Quantity_txtBox.Text + "','" + total + "','" + POSForm_shoppingCart_totalValue_lbl.Text + "','" + selectedItemOnInventory.SubItems[5].Text + "','" + POS_user_Firstname.Text + "','" + DateTime.Now.ToString() + "','" + "ACTIVE" + "','" + profit + "','" + selectedItemOnInventory.SubItems[2].Text + "')", conn);
                    command68.ExecuteNonQuery();
                MessageBox.Show("Item inserted to cart!", "Inventory updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();

                MySqlConnection con = new MySqlConnection(myConnection);
                con.Open();
                MySqlCommand save = con.CreateCommand();
                save.Connection = con;
                save.CommandText = "update inventory_table set stock_number_of_items=@newInventoryValue where stock_id_number  = '" + containsThis + "' ";
                save.Parameters.Add("@newInventoryValue", toDatabaseValue);
                save.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Inventory database updated");
                populateCart(); fill_POSwithProducts();
                decimal inventoryTotalWorth = 0;
                for (int v = 0; i < POSForm_shoppingCart_lstView.Items.Count; i++)
                {
                    inventoryTotalWorth += decimal.Parse(POSForm_shoppingCart_lstView.Items[i].SubItems[4].Text);
                }
                int numberofItemsinCart = Convert.ToInt32(POSForm_shoppingCart_totalitems_lbl.Text); int addItem = numberofItemsinCart + 1;
                POSForm_shoppingCart_totalitems_lbl.Text = addItem.ToString();
                POSForm_shoppingCart_totalValue_lbl.Text = "₱" + inventoryTotalWorth.ToString("N0");
                POSForm_items_list_lstView.Enabled = true; POSForm_minus_btn.Enabled = false; POSForm_plus_btn.Enabled = false;
                POSForm_Quantity_txtBox.Enabled = false; POSForm_addtoCart_btn.Enabled = false; POSForm_ChangeItem_btn.Enabled = false;
                POSForm_shoppingCart_lstView.Enabled = true; POSForm_shoppingCart_cash_txtBox.Enabled = true;
                selectedItemOnInventory.Focused = false; selectedItemOnInventory.Selected = false;
            }
        }
        private void POSForm_addtoCart_btn_Click(object sender, EventArgs e)
        {
           
            decimal a = Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
            int i = 0;
            ListViewItem selectedItemOnInventory = POSForm_items_list_lstView.SelectedItems[i];
            decimal z = Convert.ToDecimal(selectedItemOnInventory.SubItems[4].Text);
            if (a > z)
            {
                MessageBox.Show("Selected items stocks are not enough! " + z + " pieces are available");
                POSForm_Quantity_txtBox.Text = z.ToString();

            }
            else
            {
                 if (z <= 0)
                {
                    MessageBox.Show("Selected item is out of stocks. Please select another product");
                    POSForm_Quantity_txtBox.Text = "1";
                }
                else
                {
                    addtoCart();
                }
            }
                    //        string containsThis = selectedItemOnInventory.SubItems[0].Text;
                    //        decimal profit = Convert.ToDecimal(selectedItemOnInventory.SubItems[2].Text) - Convert.ToDecimal(selectedItemOnInventory.SubItems[5].Text) * Convert.ToDecimal(POSForm_Quantity_txtBox.Text);

                    //            ListViewItem inventoryItem = new ListViewItem(selectedItemOnInventory.SubItems[0].Text);
                    //            inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[1].Text);

                    //            inventoryItem.SubItems.Add(POSForm_Quantity_txtBox.Text);
                    //            inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[2].Text);
                    //            decimal total = Convert.ToDecimal(selectedItemOnInventory.SubItems[2].Text) * Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                    //            inventoryItem.SubItems.Add(total.ToString());
                    //            inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[4].Text);
                    //            inventoryItem.SubItems.Add(profit.ToString());

                    //            POSForm_shoppingCart_lstView.Items.Add(inventoryItem);


                    //            decimal y = Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                    //            decimal x = Convert.ToDecimal(selectedItemOnInventory.SubItems[3].Text);

                    //            z = z - y;
                    //            selectedItemOnInventory.SubItems[3].Text = z.ToString();

                    //            decimal inventoryTotalWorth = 0;
                    //            for (int v = 0; i < POSForm_shoppingCart_lstView.Items.Count; i++)
                    //            {
                    //                inventoryTotalWorth += decimal.Parse(POSForm_shoppingCart_lstView.Items[i].SubItems[4].Text);
                    //            }
                    //            int numberofItemsinCart = Convert.ToInt32(POSForm_shoppingCart_totalitems_lbl.Text); int addItem = numberofItemsinCart + 1;
                    //            POSForm_shoppingCart_totalitems_lbl.Text = addItem.ToString();
                    //            POSForm_shoppingCart_totalValue_lbl.Text = "₱" + inventoryTotalWorth.ToString("N0");
                    //            POSForm_items_list_lstView.Enabled = true; POSForm_minus_btn.Enabled = false; POSForm_plus_btn.Enabled = false;
                    //            POSForm_Quantity_txtBox.Enabled = false; POSForm_addtoCart_btn.Enabled = false; POSForm_ChangeItem_btn.Enabled = false;
                    //            POSForm_shoppingCart_lstView.Enabled = true; POSForm_shoppingCart_cash_txtBox.Enabled = true;
                    //            inventoryItem.Focused = false; inventoryItem.Selected = false;

                    //    }
                    //}
                }

        private void POSForm_processCartitems_btn_Click(object sender, EventArgs e)
        {
            try
            {
                switch (POSForm_processCartitems_btn.Text)
                {
                    case "Process":
                        POSForm_processCartitems_btn.Text = "Save";
                        groupBox2.Enabled = false; POSForm_shoppingCart_cash_txtBox.Enabled = false;
                        string ShoppingCartPeso = POSForm_shoppingCart_totalValue_lbl.Text; string removePesoSign = ShoppingCartPeso.Remove(0, 1);
                        decimal ShoppingCart_TotalValue = Convert.ToDecimal(removePesoSign); decimal ShoppingCart_CashValue = Convert.ToDecimal(POSForm_shoppingCart_cash_txtBox.Text);
                        decimal ShoppingCart_ChangeValue = ShoppingCart_CashValue - ShoppingCart_TotalValue;
                        if (ShoppingCart_TotalValue > ShoppingCart_CashValue)
                        {
                            MessageBox.Show("Money is insufficient!");
                            POSForm_Quantity_txtBox.Clear();
                        }
                        else
                        {
                            POSForm_shoppingCart_change_lbl.Text = "₱" + ShoppingCart_ChangeValue.ToString("N0");
                            POSForm_items_list_lstView.Enabled = false; POSForm_minus_btn.Enabled = false; POSForm_plus_btn.Enabled = false;
                            POSForm_Quantity_txtBox.Enabled = false; POSForm_addtoCart_btn.Enabled = false; POSForm_ChangeItem_btn.Enabled = false; POSForm_itemSearch_txtBox.Enabled = false;
                            POSForm_Category_cmboBox.Enabled = false; inventoryForm_sortGo_btn.Enabled = false;
                        }
                        POSForm_shoppingCart_lstView.Enabled=false; POSForm_Cart_removeItem_btn.Visible = false;
                        break;



                    //============ 2ND CASE ================//
                    case "Save":
                            var cashPaymentType = "Cash";
                        if(POSForm_transactiontype_Credit_rdBtn.Checked == true)
                        {
                            cashPaymentType = "Credit";
                        }

                            var transactionID = ""; var transaction_item_ID = ""; var transaction_item_name = ""; var transaction_item_pieces = "";
                            var transaction_item_price = ""; var transaction_item_subtotal = ""; var transaction_item_category = "";
                            var item_Product_line = ""; var item_supplier = ""; var item_additional_details = ""; var item_expiration_date = "";
                             var transac_change = POSForm_shoppingCart_change_lbl.Text; string removePesoSign2 = transac_change.Remove(0, 1); var transac_total_price = POSForm_shoppingCart_totalValue_lbl.Text; string removePesoSign3 = transac_total_price.Remove(0, 1);
                            decimal transac_change_value = Convert.ToDecimal(removePesoSign2); decimal transac_total_value = Convert.ToDecimal(removePesoSign3); var inventoryCount = "";


                        for (int i = 0; i < POSForm_shoppingCart_lstView.Items.Count; i++)
                        {
                            string customerNamehere = "N/A";
                            if(POSForm_customerinfo_id_lbl.Text != "")
                            {
                                customerNamehere = POSForm_customerinfo_id_lbl.Text;
                            }
                            transactionID = POSForm_shoppingCart_transactionNumber_lbl.Text;
                            transaction_item_ID = POSForm_shoppingCart_lstView.Items[i].SubItems[0].Text;

                            MySqlConnection connection = new MySqlConnection(myConnection);
                            connection.Close();
                            connection.Open();
                            MySqlCommand command = connection.CreateCommand();
                            string query = "select * from inventory_table where stock_id_number = '" + transaction_item_ID + "'";
                            command.CommandText = query;
                            MySqlDataReader read = command.ExecuteReader();

                            while (read.Read())
                            {
                                item_Product_line = read["stock_product_line"].ToString();
                                item_supplier = read["stock_supplier"].ToString();
                                item_additional_details = read["stock_additional_details"].ToString();
                                item_expiration_date = read["stock_expiration_date"].ToString();
                                inventoryCount = read["stock_number_of_items"].ToString();
                            }
                            connection.Close();


                            transaction_item_name = POSForm_shoppingCart_lstView.Items[i].SubItems[1].Text;
                            transaction_item_pieces = POSForm_shoppingCart_lstView.Items[i].SubItems[2].Text;
                            transaction_item_price = POSForm_shoppingCart_lstView.Items[i].SubItems[3].Text;
                            transaction_item_subtotal = POSForm_shoppingCart_lstView.Items[i].SubItems[4].Text;
                            transaction_item_category = POSForm_shoppingCart_lstView.Items[i].SubItems[5].Text;

                            connection.Open();

                                MySqlCommand command2 = new MySqlCommand("insert into transaction_table (transaction_id,transaction_item_ID,transaction_item_name" +
                                     ",transaction_item_count,transaction_item_category," +
                                     "transaction_item_price,transaction_item_product_line,transaction_item_supplier,transaction_item_additional_details,transaction_item_expiration_date,transaction_item_item_subtotal,transaction_total_price,transaction_cash_rendered,transaction_change_rendered" +
                                     ",transaction_user,transaction_date_made,transaction_status,transaction_customer_name,transaction_type) values " +
                                     "('" + transactionID + "','" + transaction_item_ID + "','" + transaction_item_name + "'" +
                                     ",'" + transaction_item_pieces + "'," +
                                     "'" + transaction_item_category + "','" + transaction_item_price + "','" + item_Product_line + "'" +
                                     ",'" + item_supplier +
                                     "','" + item_additional_details + "','" + item_expiration_date + "','" + transaction_item_subtotal + "','" + transac_total_value + "','" + POSForm_shoppingCart_cash_txtBox.Text + "','" + transac_change_value + "','" + POS_user_Firstname.Text + "','" + DateTime.Now + "','" + "Unread" + "','" + customerNamehere + "','" + cashPaymentType + "')", connection);

                                command2.ExecuteNonQuery();

                                int newInventoryCount_value = Convert.ToInt32(inventoryCount) - Convert.ToInt32(transaction_item_pieces);
                                MySqlConnection con = new MySqlConnection(myConnection);
                                con.Open();
                                MySqlCommand save = con.CreateCommand();
                                save.Connection = con;
                                save.CommandText = "update inventory_table set stock_number_of_items=@newInventoryValue where stock_id_number  = '" + transaction_item_ID + "' ";
                                save.Parameters.Add("@newInventoryValue", newInventoryCount_value);
                                save.ExecuteNonQuery();
                                con.Close();
                        }

                        for (int i = 0; i < POSForm_shoppingCart_lstView.Items.Count; i++)
                        {
                            MySqlConnection connection = new MySqlConnection(myConnection);
                            connection.Close();
                            connection.Open();
                            decimal CartCashValue = 0;
                            decimal CartCreditValue = 0;
                            if (POSForm_transactiontype_cash_RdBtn.Checked == true)
                            {
                                CartCashValue = transac_total_value;
                            }
                            else if(POSForm_transactiontype_Credit_rdBtn.Checked == true)
                            {
                                CartCreditValue = transac_total_value;
                            }
                            decimal CartTotalValue = transac_total_value;
                            decimal fetchedCash = 0; decimal fetchedCredit = 0; decimal fetchedTotal = 0; var fetchedGross = ""; var fetchedNet = ""; decimal fetchTotalSales = 0;

                            MySqlCommand commandx = connection.CreateCommand();
                            string queryx = "select * from sales_table where sales_date = '" + datenow_value.Text + "'";
                            commandx.CommandText = queryx;
                            MySqlDataReader readx = commandx.ExecuteReader();
                            int count1 = 0;
                            while (readx.Read())
                            {
                                fetchedCash = Convert.ToDecimal(readx["sales_cash"].ToString());
                                fetchedCredit = Convert.ToDecimal(readx["sales_credit"].ToString());
                                fetchedTotal = Convert.ToDecimal(readx["sales_total"].ToString());
                                fetchTotalSales = Convert.ToDecimal(readx["sales_transactotal"].ToString());
                                count1++;
                            }
                            if (fetchedCash.ToString() == "") { fetchedCash = 0; }
                            if (fetchedCredit.ToString() == "") { fetchedCredit = 0; }
                            if (fetchedTotal.ToString() == "") { fetchedTotal = 0; } 
                            if (fetchTotalSales.ToString() == "") { fetchTotalSales = 0; }
                                 
                            connection.Close();
                            decimal newCartCashValue = Convert.ToDecimal(fetchedCash) + CartCashValue; 
                            decimal newCartCreditValue = Convert.ToDecimal(fetchedCredit) + CartCreditValue; 
                            decimal newCartTotalValue = Convert.ToDecimal(fetchedTotal) + transac_total_value;
                            int cartNumberofItems = 0;
                            for (int b = 0; b < POSForm_shoppingCart_lstView.Items.Count; b++)
                            {
                                cartNumberofItems++;
                            }
                            decimal inventoryProfit = 0;
                            for (int v = 0; i < POSForm_shoppingCart_lstView.Items.Count; i++)
                            {
                                inventoryProfit += decimal.Parse(POSForm_shoppingCart_lstView.Items[i].SubItems[6].Text);
                            }
                            int newNumberofItems = Convert.ToInt32(fetchTotalSales) + cartNumberofItems;
                            var transacMethod = ""; decimal ifCashAmount = 0; decimal ifCreditAmount = 0; decimal totalAmount = transac_total_value;
                            if (POSForm_transactiontype_cash_RdBtn.Checked == true)
                            {
                                transacMethod = "CASH"; ifCashAmount = transac_total_value;
                            }
                            else if (POSForm_transactiontype_Credit_rdBtn.Checked == true)
                            {
                                transacMethod = "CREDIT"; ifCreditAmount = transac_total_value;
                            }
                            if (count1 <= 0)
                            {
                                connection.Close();
                                connection.Open();
                                MySqlCommand command3 = new MySqlCommand("insert into sales_table (sales_date,sales_cash,sales_credit" +
                                    ",sales_total,sales_transactotal,sales_cashier,sales_type,sales_profit) values " +
                                    "('" + datenow_value.Text + "','" + ifCashAmount + "','" + ifCreditAmount + "'" +
                                    ",'" + totalAmount + "'," +
                                    "'" + cartNumberofItems + "','" + POS_user_Firstname.Text + "','" + transacMethod + "','" + inventoryProfit + "')", connection);

                                command3.ExecuteNonQuery();
                                //MessageBox.Show("Added Sales successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                connection.Close();
                                connection.Open();
                                MySqlCommand command5 = connection.CreateCommand();
                                string queryc = "update sales_table set sales_cash = '" + newCartCashValue + "' , sales_credit = '" + newCartCreditValue + "' , sales_total = '" + newCartTotalValue + "' , sales_transactotal = '" + newNumberofItems + "' , sales_cashier = '" + POS_user_Firstname.Text + "', sales_type = '" + transacMethod + "' where sales_date  = '" + datenow_value.Text + "' ";
                                command5.CommandText = queryc;
                                command5.ExecuteNonQuery();
                                //MessageBox.Show("Update Sales successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        if (POSForm_transactiontype_Credit_rdBtn.Checked == true)
                        {
                            MySqlConnection connection = new MySqlConnection(myConnection);
                            decimal CustomerBalance = 0; decimal balanceToAdd = transac_total_value;
                            connection.Close();
                            connection.Open();
                            MySqlCommand commandcz = connection.CreateCommand();
                            string querycz = "select * from customer_table where customer_ID  = '" + POSForm_customerinfo_name_lbl.Text + "'";
                            commandcz.CommandText = querycz;
                            MySqlDataReader readcz = commandcz.ExecuteReader();

                            while (readcz.Read())
                            {
                                CustomerBalance = Convert.ToDecimal(readcz["customer_balance"].ToString());
                            }
                            decimal ValuetoDatabase = CustomerBalance + balanceToAdd;
                            connection.Close();
                            MySqlConnection conv = new MySqlConnection(myConnection);
                            conv.Open();
                            MySqlCommand savev = conv.CreateCommand();
                            savev.Connection = conv;
                            savev.CommandText = "update customer_table set customer_balance=@newCustomerBalance where customer_ID   = '" + POSForm_customerinfo_name_lbl.Text + "' ";
                            savev.Parameters.Add("@newCustomerBalance", ValuetoDatabase);
                            savev.ExecuteNonQuery();
                            conv.Close();

                            MySqlConnection convz = new MySqlConnection(myConnection);
                            convz.Open();
                            MySqlCommand savevz = convz.CreateCommand();
                            savevz.Connection = convz;
                            savevz.CommandText = "update cart_table set cart_status=@newCartStatus where cart_transactionID   = '" + POSForm_shoppingCart_transactionNumber_lbl.Text + "' ";
                            savevz.Parameters.Add("@newCartStatus", "DONE");
                            savevz.ExecuteNonQuery();
                            convz.Close();
                            //MessageBox.Show("Stock updated and customer balance updated!", "Transaction sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        MessageBox.Show("Stock updated!", "Transaction sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("Transaction successfull!");
                        POSForm_shoppingCart_transactionNumber_lbl.Text = ""; Random rand = new Random(); NumberRandom = rand.Next(111111111, 999999999);
                        POSForm_processCartitems_btn.Text = "Process"; POSForm_shoppingCart_lstView.Items.Clear(); POSForm_shoppingCart_totalValue_lbl.Text = "00.00";
                        POSForm_shoppingCart_change_lbl.Text = "-"; POSForm_shoppingCart_cash_txtBox.Text = ""; POSForm_processCartitems_btn.Visible = false; POSForm_Cart_Back_btn.Visible = false;
                        label5.Visible = false; POSForm_customerinfo_name_lbl.Items.Clear(); label7.Visible = false; POSForm_customerinfo_id_lbl.Text = ""; POSForm_customerinfo_name_lbl.Visible = false; POSForm_customerinfo_id_lbl.Visible = false;
                        POSForm_shoppingCart_totalitems_lbl.Text = "0"; POSForm_shoppingCart_transactionNumber_lbl.Text = NumberRandom.ToString();
                        POSForm_items_list_lstView.Enabled = true; fill_POSwithProducts(); POSForm_Category_cmboBox.Enabled = true; inventoryForm_sortGo_btn.Enabled = true;
                        POSForm_itemSearch_txtBox.Enabled = true; groupBox2.Enabled = true; POSForm_transactiontype_cash_RdBtn.Checked = false; POSForm_transactiontype_Credit_rdBtn.Checked = false;
                        break;
                }
            }
            catch (Exception ez)
            {
                MessageBox.Show("ERROR: " + ez);
            }
        }

        private void POSForm_items_list_lstView_DoubleClick(object sender, EventArgs e)
        {
            POSForm_minus_btn.Enabled = true; POSForm_plus_btn.Enabled = true;
            POSForm_Quantity_txtBox.Enabled = true; POSForm_items_list_lstView.Enabled = false; POSForm_addtoCart_btn.Enabled = true; POSForm_ChangeItem_btn.Enabled = true;
            POSForm_itemSearch_txtBox.Enabled = false; POSForm_Category_cmboBox.Enabled = false; inventoryForm_sortGo_btn.Enabled = false; POSForm_shoppingCart_lstView.Enabled = false;
            POSForm_shoppingCart_cash_txtBox.Enabled = false; POSForm_processCartitems_btn.Enabled = false;
        }

        private void POSForm_ChangeItem_btn_Click(object sender, EventArgs e)
        {
            POSForm_items_list_lstView.Enabled = true; POSForm_minus_btn.Enabled = false; POSForm_plus_btn.Enabled = false;
            POSForm_Quantity_txtBox.Enabled = false; POSForm_addtoCart_btn.Enabled = false; POSForm_ChangeItem_btn.Enabled = false;
            POSForm_shoppingCart_lstView.Enabled = true;
        }

        private void POSForm_shoppingCart_cash_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);

            if (POSForm_shoppingCart_cash_txtBox.Text != "")
            {
                POSForm_processCartitems_btn.Enabled = true;
            }
        }

        private void POSForm_shoppingCart_cash_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (POSForm_shoppingCart_cash_txtBox.Text != "")
            {
                POSForm_processCartitems_btn.Enabled = true;
            }
        }

        private void POSForm_Cart_Back_btn_Click(object sender, EventArgs e)
        {
            POSForm_processCartitems_btn.Text = "Process"; POSForm_items_list_lstView.Enabled = true; POSForm_minus_btn.Enabled = true; POSForm_plus_btn.Enabled = true;
            POSForm_Quantity_txtBox.Enabled = true; POSForm_addtoCart_btn.Enabled = true; POSForm_ChangeItem_btn.Enabled = true; POSForm_itemSearch_txtBox.Enabled = true;
            POSForm_Category_cmboBox.Enabled = true; inventoryForm_sortGo_btn.Enabled = true;
        }

        private void POSForm_toTransactions_btn_Click(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = POS_user_Firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = POS_user_idnumber.Text;
            toTransactionForm.account_level.Text = account_level.Text;
            this.Close();
            toTransactionForm.Show();
        }

        private void POSForm_plus_btn_Click(object sender, EventArgs e)
        {
            int addCart_Value = Convert.ToInt32(POSForm_Quantity_txtBox.Text); addCart_Value++;
            POSForm_Quantity_txtBox.Text = addCart_Value.ToString();
        }

        private void POSForm_minus_btn_Click(object sender, EventArgs e)
        {
            int addCart_Value = Convert.ToInt32(POSForm_Quantity_txtBox.Text); addCart_Value--;
            POSForm_Quantity_txtBox.Text = addCart_Value.ToString();
        }

        private void POSForm_Quantity_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (POSForm_Quantity_txtBox.Text == "") { POSForm_Quantity_txtBox.Text = "1"; }
            else { int addCart_Value = Convert.ToInt32(POSForm_Quantity_txtBox.Text); if (addCart_Value <= 0) { POSForm_Quantity_txtBox.Text = "1"; } }
            
        }

        private void POSForm_transactiontype_cash_RdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(POSForm_transactiontype_cash_RdBtn.Checked == true || POSForm_transactiontype_Credit_rdBtn.Checked == true)
            {
                POSForm_transactiontype_confirm_btn.Enabled = true;
            }
            else
            {
                POSForm_transactiontype_confirm_btn.Enabled = false;
            }
            if (POSForm_transactiontype_Credit_rdBtn.Checked == true && POSForm_customerinfo_name_lbl.Text == "N/A")
            {
                POSForm_processCartitems_btn.Enabled = false;
            }
            else
            {
                POSForm_processCartitems_btn.Enabled = false;
            }
        }

        private void POSForm_transactiontype_Credit_rdBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (POSForm_transactiontype_Credit_rdBtn.Checked == true && POSForm_customerinfo_name_lbl.Text == "N/A")
            {
                POSForm_processCartitems_btn.Enabled = false;
            }
            else
            {
                POSForm_processCartitems_btn.Enabled = false;
            }
            if (POSForm_transactiontype_cash_RdBtn.Checked == true || POSForm_transactiontype_Credit_rdBtn.Checked == true)
            {
                POSForm_transactiontype_confirm_btn.Enabled = true;
            }
            else
            {
                POSForm_transactiontype_confirm_btn.Enabled = false;
            }
        }

        private void POSForm_transactiontype_confirm_btn_Click(object sender, EventArgs e)
        {
            
            if (POSForm_transactiontype_Credit_rdBtn.Checked == true)
            {
                POSForm_shoppingCart_cash_txtBox.Enabled = true; POSForm_customerinfo_name_lbl.Visible = true; label5.Visible = true; POSForm_processCartitems_btn.Enabled = true; label7.Visible = true; POSForm_customerinfo_id_lbl.Visible = true;
            }
            else if(POSForm_transactiontype_cash_RdBtn.Checked == true)
            {
                POSForm_processCartitems_btn.Enabled = true; POSForm_shoppingCart_cash_txtBox.Enabled = true;
            }
            else
            {
                POSForm_shoppingCart_cash_txtBox.Enabled = false; POSForm_customerinfo_name_lbl.Visible = false; label5.Visible = false; POSForm_processCartitems_btn.Enabled = true; label7.Visible = false; POSForm_customerinfo_id_lbl.Visible = false;
            }
        
        }

        private void POSForm_toCustomer_btn_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = POS_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = POS_user_idnumber.Text;
            toCustomer.account_level.Text = account_level.Text;
            this.Close();
            toCustomer.Show();
        }

        private void POSForm_toSales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = POS_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = POS_user_idnumber.Text;
            toSalesForm.account_level.Text = account_level.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void POSForm_toSuppliers_btn_Click(object sender, EventArgs e)
        {
            supplier_Form toSupplier = new supplier_Form();
            toSupplier.Supplier_user_Firstname.Text = POS_user_Firstname.Text;
            toSupplier.Supplier_user_idnumber.Text = POS_user_idnumber.Text;
            toSupplier.account_level.Text = account_level.Text;
            this.Close();
            toSupplier.Show();
        }

        private void POSForm_Quantity_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void POSForm_itemSearch_txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (POSForm_itemSearch_txtBox.Text == "")
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(myConnection);
                        POSForm_items_list_lstView.Items.Clear();
                        connection.Close();

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query = "select * from inventory_table WHERE stock_forReplacement = '" + "false" + "' AND stock_forScrap = '" + "false" + "' AND stock_status = '" + 1 + "' ";
                        command.CommandText = query;
                        MySqlDataReader read = command.ExecuteReader();

                        while (read.Read())
                        {
                            ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                            item.SubItems.Add(read["stock_product_name"].ToString());
                            item.SubItems.Add(read["stock_additional_details"].ToString());
                            item.SubItems.Add(read["stock_selling_price"].ToString());
                            item.SubItems.Add(read["stock_number_of_items"].ToString());
                            item.SubItems.Add(read["stock_category"].ToString());
                            item.SubItems.Add(read["stock_purchase_price"].ToString());

                            POSForm_items_list_lstView.Items.Add(item);
                            POSForm_items_list_lstView.FullRowSelect = true;
                        }
                        connection.Close();
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR: " + x);
                    }
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    POSForm_items_list_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM inventory_table WHERE stock_forReplacement = '" + "false" + "' AND stock_forScrap = '" + "false" + "' AND stock_status = '" + 1 + "' && stock_product_name LIKE '%" + POSForm_itemSearch_txtBox.Text + "%'";
                    command.CommandText = query;
                    MySqlDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {


                        ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                        item.SubItems.Add(read["stock_product_name"].ToString());
                        item.SubItems.Add(read["stock_additional_details"].ToString());
                        item.SubItems.Add(read["stock_selling_price"].ToString());
                        item.SubItems.Add(read["stock_number_of_items"].ToString());
                        item.SubItems.Add(read["stock_category"].ToString());
                        item.SubItems.Add(read["stock_purchase_price"].ToString());

                        POSForm_items_list_lstView.Items.Add(item);
                        POSForm_items_list_lstView.FullRowSelect = true;
                    }
                    connection.Close();
                }
            }

            catch (Exception x)
            {
                MessageBox.Show("ERROR: " + x);
            }
        }

        private void POSForm_customerinfo_name_lbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                POSForm_customerinfo_id_lbl.Text = "";
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from customer_table WHERE customer_ID  = '" + POSForm_customerinfo_name_lbl.Text + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    POSForm_customerinfo_id_lbl.Text = (read["customer_firstname"].ToString() + " " + read["customer_lastname"].ToString());
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
        }

        private void POSForm_Cart_removeItem_btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to remove this item on the cart?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string ShoppingCartPeso = POSForm_shoppingCart_totalValue_lbl.Text;
                string removePesoSign = ShoppingCartPeso.Remove(0, 1);
                decimal ShoppingCart_TotalValue = Convert.ToDecimal(removePesoSign);
                int i = 0; int inventoryValue = 0;
                ListViewItem selectedItemOnCart = POSForm_shoppingCart_lstView.SelectedItems[i];
                string containsThis = selectedItemOnCart.SubItems[0].Text; int cartItemValue = Convert.ToInt32(selectedItemOnCart.SubItems[2].Text);
                decimal cartPrice = Convert.ToDecimal(selectedItemOnCart.SubItems[4].Text);
                decimal newTotalPrice = ShoppingCart_TotalValue - cartPrice; POSForm_shoppingCart_totalValue_lbl.Text = newTotalPrice.ToString();
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table WHERE stock_id_number  = '" + containsThis + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    inventoryValue = Convert.ToInt32((read["stock_number_of_items"].ToString()));
                }
                connection.Close();

                int newItemValue = inventoryValue + cartItemValue;

                MySqlConnection con = new MySqlConnection(myConnection);
                con.Open();
                MySqlCommand save = con.CreateCommand();
                save.Connection = con;
                save.CommandText = "update inventory_table set stock_number_of_items = @newInventoryValue where stock_id_number  = '" + containsThis + "' ";
                save.Parameters.Add("@newInventoryValue", newItemValue);
                save.ExecuteNonQuery();
                con.Close();

                MySqlConnection con2 = new MySqlConnection(myConnection);
                con2.Open();
                MySqlCommand save2 = con2.CreateCommand();
                save2.Connection = con2;
                save2.CommandText = "update cart_table set cart_status = @cartUpdatedStatus where cart_transactionID  = '" + POSForm_shoppingCart_transactionNumber_lbl.Text + "' && cart_stockIDNumber  = '" + containsThis + "'";
                save2.Parameters.Add("@cartUpdatedStatus", "REMOVED");
                save2.ExecuteNonQuery();
                con2.Close();
                populateCart(); fill_POSwithProducts();
                MessageBox.Show("Item removed!", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                POSForm_Cart_removeItem_btn.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void POSForm_shoppingCart_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void POSForm_shoppingCart_lstView_MouseClick(object sender, MouseEventArgs e)
        {
            if (POSForm_shoppingCart_lstView.SelectedItems.Count > 0)
            {
                POSForm_Cart_removeItem_btn.Visible = true; POSForm_Cart_removeItem_btn.Enabled = true;
            }
            else
            {
                POSForm_Cart_removeItem_btn.Visible = false; POSForm_Cart_removeItem_btn.Enabled = false;
            }
        }
    }

}

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
    public partial class POS_Form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public POS_Form()
        {
            InitializeComponent();
            fill_POSwithProducts();
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
                string query = "select * from inventory_table";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                    item.SubItems.Add(read["stock_product_name"].ToString());
                    item.SubItems.Add(read["stock_selling_price"].ToString());
                    item.SubItems.Add(read["stock_number_of_items"].ToString());
                    item.SubItems.Add(read["stock_category"].ToString());

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
        public void populate_Customer()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                POSForm_customerinfo_name_lbl.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                    item.SubItems.Add(read["stock_product_name"].ToString());
                    item.SubItems.Add(read["stock_selling_price"].ToString());
                    item.SubItems.Add(read["stock_number_of_items"].ToString());
                    item.SubItems.Add(read["stock_category"].ToString());

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
            this.Close();
            toInventory.Show();
            
        }

        private void POS_toUsers_btn_Click(object sender, EventArgs e)
        {
            employees_Form toEmployeeForm = new employees_Form();
            toEmployeeForm.employeeForm_employeeCurrentPicture_pctrBox.Image = POS_employeeCurrentPhoto_pctrBox.Image;
            toEmployeeForm.employeeForm_user_firstname.Text = POS_user_Firstname.Text;
            toEmployeeForm.employeeForm_user_idnumber.Text = POS_user_idnumber.Text;
            this.Close();
            toEmployeeForm.Show();
        }

        private void POSForm_logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("You are now logged out!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
                //Environment.Exit(0);
            }
        }

        private void POSForm_addtoCart_btn_Click(object sender, EventArgs e)
        {
            decimal a = Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
            int i = 0;
            ListViewItem selectedItemOnInventory = POSForm_items_list_lstView.SelectedItems[i];
            decimal z = Convert.ToDecimal(selectedItemOnInventory.SubItems[3].Text);
            if (a > z)
            {
                MessageBox.Show("Selected items stocks are not enough! " + z + " pieces are available");
                POSForm_Quantity_txtBox.Text = z.ToString();

            }
            else
            {
                if (z == 1)
                {
                    MessageBox.Show("Selected item stock is not enough! " + z + " piece is available");
                    POSForm_Quantity_txtBox.Text = z.ToString();
                }
                else if (z == 0)
                {
                    MessageBox.Show("Selected item is out of stocks.");
                    POSForm_Quantity_txtBox.Text = z.ToString();
                }
                else
                {
                    ListViewItem inventoryItem = new ListViewItem(selectedItemOnInventory.SubItems[0].Text);
                    inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[1].Text);
                    inventoryItem.SubItems.Add(POSForm_Quantity_txtBox.Text);
                    inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[2].Text);
                    decimal total = Convert.ToDecimal(selectedItemOnInventory.SubItems[2].Text) * Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                    inventoryItem.SubItems.Add(total.ToString());
                    inventoryItem.SubItems.Add(selectedItemOnInventory.SubItems[4].Text);

                    POSForm_shoppingCart_lstView.Items.Add(inventoryItem);

                    decimal y = Convert.ToDecimal(POSForm_Quantity_txtBox.Text);
                    decimal x = Convert.ToDecimal(selectedItemOnInventory.SubItems[3].Text);

                    z = z - y;
                    selectedItemOnInventory.SubItems[3].Text = z.ToString();

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
                    POSForm_shoppingCart_lstView.Enabled = true;
                    POSForm_shoppingCart_cash_txtBox.Enabled = true; POSForm_processCartitems_btn.Enabled = true;
                    inventoryItem.Focused = false; inventoryItem.Selected = false;
                }
            }
        }

        private void POSForm_processCartitems_btn_Click(object sender, EventArgs e)
        {
            try
            {
                switch (POSForm_processCartitems_btn.Text)
                {
                    case "Process":
                        POSForm_processCartitems_btn.Text = "Save";
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
                        if(POSForm_transactiontype_Credit_rdBtn.Checked = true)
                        {
                            POSForm_Customerinfo_grpBox.Visible = true;
                        }
                        if (POSForm_transactiontype_cash_RdBtn.Checked = true)
                        {
                            POSForm_Customerinfo_grpBox.Visible = false;
                        }
                        break;
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
                                ",transaction_user,transaction_date_made,transaction_status,transaction_customer_name) values " +
                                "('" + transactionID + "','" + transaction_item_ID + "','" + transaction_item_name + "'" +
                                ",'" + transaction_item_pieces + "'," +
                                "'" + transaction_item_category + "','" + transaction_item_price + "','" + item_Product_line + "'" +
                                ",'" + item_supplier +
                                "','" + item_additional_details + "','" + item_expiration_date + "','" + transaction_item_subtotal + "','" + transac_total_value + "','" + POSForm_shoppingCart_cash_txtBox.Text + "','" + transac_change_value + "','" + POS_user_Firstname.Text + "','" + DateTime.Now + "','" + "Unread" + "','" + "N/A" +"')", connection);

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
                            MessageBox.Show("Stock updated!");
                        }
                        MessageBox.Show("Transaction successfull!");
                        POSForm_processCartitems_btn.Text = "Process";
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
        }

        private void POSForm_shoppingCart_cash_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(POSForm_shoppingCart_cash_txtBox.Text != "")
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
            int addCart_Value = Convert.ToInt32(POSForm_Quantity_txtBox.Text);
            if(addCart_Value <= -1) { POSForm_Quantity_txtBox.Text = "0"; }
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
        }

        private void POSForm_transactiontype_Credit_rdBtn_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void POSForm_transactiontype_confirm_btn_Click(object sender, EventArgs e)
        {
            if (POSForm_transactiontype_Credit_rdBtn.Checked == true)
            {
                POSForm_Customerinfo_grpBox.Visible = true;
            }
            else
            {
                POSForm_Customerinfo_grpBox.Visible = false;
            }
            if (POSForm_transactiontype_cash_RdBtn.Checked == true || POSForm_transactiontype_Credit_rdBtn.Checked == true)
            {
                POSForm_shoppingCart_cash_txtBox.Enabled = true;
            }
            else
            {
                POSForm_shoppingCart_cash_txtBox.Enabled = false;
            }
        }

        private void POSForm_toCustomer_btn_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = POS_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = POS_user_idnumber.Text;
            this.Close();
            toCustomer.Show();
        }

        private void POSForm_toSales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = POS_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = POS_user_idnumber.Text;
            this.Close();
            toSalesForm.Show();
        }
    }

}

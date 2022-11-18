using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Security.Cryptography;
using System.IO;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace YvonnieStore_Beta_2_
{
    public partial class inventory_Form : Form
    {
        string myConnection = "Server=localhost;database=chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public inventory_Form()
        {
            InitializeComponent();
            fill_InventoryList();
            checkLowStocks();
            checkProductforReplacement();
            inventoryWorth();
            nonInventorySales();
            inventoryForm_newitemexpiration_DTpicker.MinDate = DateTime.Now;
            //employeeForm_new_employeeDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            //employeeForm_employeeDOB_DTpicker.MinDate = DateTime.Now.AddYears(-60);
            //employeeForm_employeeDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            //employeForm_employeedatehired_DTpicker.MinDate = DateTime.Now.AddYears(-25);
            //employeForm_employeedatehired_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            //employeeForm_new_employeeDOB_DTpicker.Value = DateTime.Now.AddYears(-16);
        }
        private void t_Tick(object sender, EventArgs e)
        {
            inventoryForm_timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        //Public variables//
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(111111111, 999999999);
        //Public variables//

        //FILL INVENTORY METHOD START HERE//
        public void fill_InventoryList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_inventorylist_listview.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    string purchasePrice = read["stock_selling_price"].ToString(); string numberofItems = read["stock_number_of_items"].ToString();
                    decimal TotalWorth = Convert.ToDecimal(purchasePrice) * Convert.ToDecimal(numberofItems);
                    string numberofStocks = read["stock_number_of_items"].ToString(); string lowStockValue = read["stock_low_stock_alert"].ToString();
                    string isLowStock = "";
                    if (Convert.ToInt32(numberofStocks) <= Convert.ToInt32(lowStockValue))
                    {
                        isLowStock = "true";
                    }
                    else
                    {
                        isLowStock = "false";
                    }
                    ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                    //item.SubItems.Add(read["stock_product_name"].ToString()+ " " + read["stock_additional_details"].ToString());
                    item.SubItems.Add(read["stock_product_name"].ToString());
                    item.SubItems.Add(read["stock_number_of_items"].ToString());
                    item.SubItems.Add(read["stock_purchase_price"].ToString());
                    item.SubItems.Add(read["stock_selling_price"].ToString());
                    item.SubItems.Add(read["stock_product_line"].ToString());
                    item.SubItems.Add(read["stock_supplier"].ToString());
                    item.SubItems.Add(read["stock_additional_details"].ToString());
                    item.SubItems.Add(read["stock_expiration_date"].ToString());
                    item.SubItems.Add(read["stock_category"].ToString());
                    item.SubItems.Add(read["stock_date_added"].ToString());
                    item.SubItems.Add(read["stock_date_updated"].ToString());
                    item.SubItems.Add(read["stock_date_archived"].ToString());
                    item.SubItems.Add(read["stock_status"].ToString());
                    item.SubItems.Add(read["stock_addedbyUser_name"].ToString());
                    item.SubItems.Add(read["stock_lastupdatedby"].ToString());
                    item.SubItems.Add(read["stock_low_stock_alert"].ToString());
                    item.SubItems.Add(read["stock_forReplacement"].ToString());
                    item.SubItems.Add(read["stock_forScrap"].ToString());
                    item.SubItems.Add(isLowStock.ToString());
                    item.SubItems.Add(TotalWorth.ToString());
                    if (isLowStock == "true")
                    {
                        item.BackColor = Color.IndianRed;
                    }
                    inventoryForm_inventorylist_listview.Items.Add(item);
                    inventoryForm_inventorylist_listview.FullRowSelect = true;




                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        //FILL INVENTORY METHOD END HERE//

        //ADD ITEM TO INVENTORY METHOD START HERE//
        public void add_product_to_inventory()
        {

            MySqlConnection conn = new MySqlConnection(myConnection);

            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            string query0 = "select * from inventory_table where stock_id_number  = '" + inventoryForm_newproductid_txtBox.Text + "'";
            command.CommandText = query0;
            MySqlDataReader read = command.ExecuteReader();

            int count = 0;
            while (read.Read())
            {
                count++;
            }

            if (count >= 1)
            {
                inventoryForm_newproductid_txtBox.Clear();
                Random rand = new Random();
                NumberRandom = rand.Next(111111111, 999999999);
                inventoryForm_newproductid_txtBox.Text = NumberRandom.ToString();
                MessageBox.Show("Duplicate item found with the same ID, Creating nother new ID number. Please click add again. Thank you.", "Duplicate ID found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (count == 0)
            {
                int stockValue = Convert.ToInt32(inventoryForm_newitemamount_txtBox.Text); int lowStockValue = Convert.ToInt32(inventoryForm_lowStockAlert_txtBox.Text);
                string isLowValue = "false";
                if (stockValue < lowStockValue)
                {
                    isLowValue = "true";
                }
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();

                //MySqlCommand command = new MySqlCommand("insert into data (ID,firstname,lastname,username,password,gender,email,birthdate,useradd,userupdate,userview,userdelete,pic) values ('" + userid.Text + "', '" + firstnametxtbox.Text + "','" + lastnametxtbox.Text + "','" + usernametxtbox.Text + "','" + qwe.encryptpassword(passwordtxtbox.Text) + "','" + truegender + "','" + emailtxtbox.Text + "','" + datetimepicker.Text + "','" + addchecker.Text + "','" + updatechecker.Text + "','" + viewchecker.Text + "','" + deletechecker.Text + "','" + ImageSave + "')", connection);
                MySqlCommand commandz = new MySqlCommand("insert into inventory_table (stock_id_number,stock_product_name,stock_number_of_items" +
                    ",stock_purchase_price,stock_selling_price," +
                    "stock_product_line,stock_supplier,stock_additional_details,stock_category,stock_date_added,stock_date_updated,stock_date_archived,stock_status,stock_expiration_date" +
                    ",stock_addedbyUser_name,stock_lastupdatedby,stock_low_stock_alert,stock_isLow,stock_forReplacement,stock_forScrap) values " +
                    "('" + inventoryForm_newproductid_txtBox.Text + "','" + inventoryForm_newproductname_txtBox.Text + "','" + inventoryForm_newitemamount_txtBox.Text + "'" +
                    ",'" + inventoryForm_newitempurchaseprice_txtBox.Text + "'," +
                    "'" + inventoryForm_newitemsellingprice_txtBox.Text + "','" + inventoryForm_newproductline_txtBox.Text + "','" + inventoryForm_newsupplier_CBbox.Text + "'" +
                    ",'" + inventoryForm_newitemadditionaldetails_txtBox.Text +
                    "','" + inventoryForm_newitemcategory_cmBox.Text + "','" + DateTime.Now + "','" + "N/A" + "','" + "N/A" + "','" + "1" + "','" + inventoryForm_newitemexpiration_DTpicker.Text + "','" + inventoryForm_user_Firstname.Text + "','" + "N/A" + "','" + inventoryForm_lowStockAlert_txtBox.Text + "','" + isLowValue + "','" + "false" + "','" + "false" + "')", connection);

                commandz.ExecuteNonQuery();
                fill_InventoryList();
                inventoryForm_newproductid_txtBox.Clear();
                inventoryForm_newproductname_txtBox.Clear();
                inventoryForm_newitemamount_txtBox.Text = "";
                inventoryForm_newitempurchaseprice_txtBox.Clear();
                inventoryForm_newitemsellingprice_txtBox.Text = "";
                inventoryForm_newproductline_txtBox.Clear();
                inventoryForm_newsupplier_CBbox.Items.Clear();
                inventoryForm_newitemadditionaldetails_txtBox.Clear();
                inventoryForm_lowStockAlert_txtBox.Clear();
                MessageBox.Show("New item successfully inserted into the inventory list.", "SUCCESS!");
                panel5.Visible = true;
                panel1.Visible = true;
                inventoryForm_addnewitem_btn.Visible = true;
                inventoryForm_inventorylist_listview.Visible = true;
                inventoryForm_addnewItem_panel.Visible = false;
                inventoryForm_newitemcategory_cmBox.Items.Clear();
                inventoryForm_newsupplier_CBbox.Items.Clear();
                inventoryForm_newitemexpiration_DTpicker.Text = DateTime.Now.ToString();
                inventoryWorth();
                checkLowStocks();

            }

            conn.Close();
            
        }
        //ADD ITEM TO INVENTORY METHOD ENDS HERE//

        public void populateSupplierComboBox()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_newsupplier_CBbox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from supplier_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    //for (int i = 0; i < read.; i++)
                    //{
                    inventoryForm_newsupplier_CBbox.Items.Add(read["supplier_company"].ToString());
                    // }
                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void populateSupplierComboBoxUpdate()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_update_supplier_cBox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from supplier_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    inventoryForm_update_supplier_cBox.Items.Add(read["supplier_company"].ToString());
                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void populateCategoryNew()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_newitemcategory_cmBox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    inventoryForm_newitemcategory_cmBox.Items.Add(read["stock_category"].ToString());
                }
                List<object> list = new List<object>();
                foreach (object o in inventoryForm_newitemcategory_cmBox.Items)
                {
                    if (!list.Contains(o))
                    {
                        list.Add(o);
                    }
                }
                inventoryForm_newitemcategory_cmBox.Items.Clear();
                inventoryForm_newitemcategory_cmBox.Items.AddRange(list.ToArray());
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void populateCategoryUpdate()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_update_category_txtBox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    inventoryForm_update_category_txtBox.Items.Add(read["stock_category"].ToString());
                }
                List<object> list = new List<object>();
                foreach (object o in inventoryForm_update_category_txtBox.Items)
                {
                    if (!list.Contains(o))
                    {
                        list.Add(o);
                    }
                }
                inventoryForm_update_category_txtBox.Items.Clear();
                inventoryForm_update_category_txtBox.Items.AddRange(list.ToArray());
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void inventoryWorth()
        {
            if (inventoryForm_inventorylist_listview.Items.Count == 0)
            {
                inventoryForm_inventoryworth_lbl.Text = "Inventory empty";
                label19.Visible = false;
            }
            else
            {
                decimal inventoryTotalWorth = 0;
                for (int i = 0; i < inventoryForm_inventorylist_listview.Items.Count; i++)
                {
                    inventoryTotalWorth += decimal.Parse(inventoryForm_inventorylist_listview.Items[i].SubItems[20].Text);
                }
                inventoryForm_inventoryworth_lbl.Text = "₱" + inventoryTotalWorth.ToString("N0");
            }
        }
        public void nonInventorySales()
        {
            MySqlConnection connection = new MySqlConnection(myConnection);

            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string query0 = "select SUM(sales_noninventorytotal) FROM sales_table where sales_type = '" + "NONINVENTORY" + "'";
            command.CommandText = query0;
            //MySqlDataReader read = command.ExecuteReader();

            inventoryForm_noninventorysales_lbl.Text = "₱" + command.ExecuteScalar().ToString();
            // var salesValue = read["stock_selling_price"].ToString();
            //while (read.Read())
            //{
            // inventoryForm_noninventorysales_lbl.Text = salesValue;
            //}
            connection.Close();

        }
        public void checkLowStocks()
        {
            int numberOfLowStocks = 0;

            for (int i = 0; i < inventoryForm_inventorylist_listview.Items.Count; i++)
            {
                string isLowStock = inventoryForm_inventorylist_listview.Items[i].SubItems[19].Text;
                string stockID = inventoryForm_inventorylist_listview.Items[i].SubItems[0].Text;
                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from inventory_table where stock_id_number = '" + stockID + "'";
                command.CommandText = query0;
                MySqlDataReader read = command.ExecuteReader();

                int count = 0;
                while (read.Read())
                {
                    count++;
                }

                if (isLowStock == "true")
                {


                    if (count == 1)
                    {
                        connection.Close();
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        string query1 = "update inventory_table set stock_isLow = '" + "true" + "' where stock_id_number  = '" + stockID + "' ";
                        command2.CommandText = query1;
                        command2.ExecuteNonQuery();
                    }
                    numberOfLowStocks++;
                }
                else if (isLowStock == "false")
                {
                    if (count == 1)
                    {
                        connection.Close();
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        string query1 = "update inventory_table set stock_isLow = '" + "false" + "' where stock_id_number  = '" + stockID + "' ";
                        command2.CommandText = query1;
                        command2.ExecuteNonQuery();
                    }
                }

            }
            inventoryForm_lowstacksvalue_lbl.Text = numberOfLowStocks.ToString();
        }
        private void ResetForm()
        {
            inventoryForm_newproductid_txtBox.Clear();
            Random rand = new Random();
            NumberRandom = rand.Next(111111111, 999999999);
            inventoryForm_newproductname_txtBox.Clear();
            inventoryForm_newitemamount_txtBox.Clear();
            inventoryForm_newitemsellingprice_txtBox.Clear();
            inventoryForm_newitempurchaseprice_txtBox.Clear();
            inventoryForm_newproductline_txtBox.Clear();
            inventoryForm_newitemadditionaldetails_txtBox.Clear();
        }
        public void checkProductforReplacement()
        {
            int itemsForReplacement = 0; int itemsForScrap = 0;
            for (int i = 0; i < inventoryForm_inventorylist_listview.Items.Count; i++)
            {
                string forReplacement = inventoryForm_inventorylist_listview.Items[i].SubItems[17].Text;
                string forScrap = inventoryForm_inventorylist_listview.Items[i].SubItems[18].Text;
                string stockID = inventoryForm_inventorylist_listview.Items[i].SubItems[0].Text;
                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from inventory_table";
                command.CommandText = query0;
                MySqlDataReader read = command.ExecuteReader();

                int count = 0;
                while (read.Read())
                {
                    count++;
                }

                if (forReplacement == "true")
                {
                    itemsForReplacement++;
                }
                if (forScrap == "true")
                {
                    itemsForScrap++;
                }

            }
            inventoryForm_Replacement_lbl.Text = itemsForReplacement.ToString(); inventoryForm_Scral_lbl.Text = itemsForScrap.ToString();
        }
        public void closeUpdateForm()
        {
            inventoryForm_updateForm_panel.Visible = false;
            inventoryForm_inventorylist_listview.Visible = true;
            inventoryForm_addnewitem_btn.Visible = true;
            panel5.Visible = true;
            panel1.Visible = true;
            inventoryForm_addnewitem_btn.Text = "ADD NEW ITEM";
        }
        public void updatemethod()
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int statusValue = 1;
                if (inventoryForm_update_statusActive_rBtn.Checked == true)
                {
                    statusValue = 1;
                }
                else if (inventoryForm_update_statusHidden_rBtn.Checked == true)
                {
                    statusValue = 0;
                }
                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from inventory_table where stock_id_number = '" + inventoryForm_update_productID_txtBox.Text + "'";
                command.CommandText = query0;
                MySqlDataReader read = command.ExecuteReader();

                string forReplacementValue = "false"; string forScrapValue = "false";
                int count = 0;
                while (read.Read())
                {
                    //forReplacementValue = read["stock_forReplacement"].ToString();
                    //forScrapValue = read["stock_forScrap"].ToString();
                    count++;
                }

                if (count >= 1)
                {
                    inventoryForm_update_forReplacement_lbl.Text = inventorForm_update_forreplacement_txtBox.Text;
                    inventorForm_update_forsrap_lbl.Text = inventorForm_update_forscrap_txtBox.Text;
                    if (inventoryForm_update_forReplacement_lbl.Text != "0")
                    {
                        forReplacementValue = "true";
                    }
                    if (inventorForm_update_forsrap_lbl.Text != "0")
                    {
                        forScrapValue = "true";
                    }
                    connection.Close();
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    string query1 = "update inventory_table set stock_product_name = '" + inventoryForm_update_productName_txtBox.Text + "' , stock_number_of_items = '" + inventoryForm_update_stocks_txtBox.Text + "' , stock_purchase_price = '" + inventoryForm_update_purchaseprice_txtBox.Text + "' , stock_selling_price = '" + inventoryForm_update_sellingPrice_txtBox.Text + "' , stock_product_line = '" + inventoryForm_update_productline_txtBox.Text + "' , stock_supplier = '" + inventoryForm_update_supplier_cBox.Text + "' , stock_additional_details = '" + inventoryForm_update_additionaldetails_txtBox.Text + "', stock_expiration_date = '" + inventoryForm_update_expirationDate_DTpicker.Text + "' , stock_category = '" + inventoryForm_update_category_txtBox.Text + "' , stock_date_updated = '" + DateTime.Now + "' , stock_status = '" + statusValue + "', stock_lastupdatedby = '" + inventoryForm_user_Firstname.Text + "', stock_forReplacement = '" + forReplacementValue + "', stock_forScrap = '" + forScrapValue + "' where stock_id_number  = '" + inventoryForm_update_productID_txtBox.Text + "' ";
                    command2.CommandText = query1;
                    command2.ExecuteNonQuery();
                    fill_InventoryList(); checkLowStocks(); checkProductforReplacement();
                    MessageBox.Show("For replacement results: " + forReplacementValue + " / " + forScrapValue, "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inventorForm_update_forreplacement_txtBox.Text = ""; inventorForm_update_forscrap_txtBox.Text = "";
                }
                connection.Close();
                connection.Open();
                MySqlCommand commandInventory2 = connection.CreateCommand();
                string query2 = "select * from forscrapandreplacment_table where product_ID = '" + inventoryForm_update_productID_txtBox.Text + "'";
                commandInventory2.CommandText = query2;
                MySqlDataReader read1 = commandInventory2.ExecuteReader();

                int count2 = 0;
                while (read1.Read())
                {
                    count2++;
                }
                if (count2 >= 1)
                {
                    connection.Close();
                    connection.Open();
                    MySqlCommand command3 = connection.CreateCommand();
                    string query1 = "update forscrapandreplacment_table set product_name = '" + inventoryForm_update_productName_txtBox.Text + "' , product_purchasePrice = '" + inventoryForm_update_purchaseprice_txtBox.Text + "' , product_sellingPrice = '" + inventoryForm_update_sellingPrice_txtBox.Text + "' , product_productLine = '" + inventoryForm_update_productline_txtBox.Text + "' , product_supplier = '" + inventoryForm_update_supplier_cBox.Text + "' , product_additionalDetails = '" + inventoryForm_update_additionaldetails_txtBox.Text + "', product_expirationDate = '" + inventoryForm_update_expirationDate_DTpicker.Text + "' , product_category = '" + inventoryForm_update_category_txtBox.Text + "' , product_addedDate = '" + DateTime.Now + "' , product_addedBy = '" + inventoryForm_user_Firstname.Text + "', product_numberofScrap = '" + inventorForm_update_forscrap_txtBox.Text + "', product_numberofReplacement = '" + inventorForm_update_forreplacement_txtBox.Text + "', product_totalValue = '" + inventoryForm_update_totalValue_lbl.Text + "' where product_ID  = '" + inventoryForm_update_productID_txtBox.Text + "' ";
                    command3.CommandText = query1;
                    command3.ExecuteNonQuery();
                    fill_InventoryList(); checkLowStocks();
                    MessageBox.Show("For replacement Update successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    closeUpdateForm();
                }
                else
                {
                    int forReplacementTotalCount = Convert.ToInt32(inventoryForm_update_forReplacement_lbl.Text); int forScrapTotalCount = Convert.ToInt32(inventorForm_update_forsrap_lbl.Text); int ProductLiveCount = Convert.ToInt32(inventoryForm_update_stocks_txtBox.Text);
                    int forReplacementToAdd = Convert.ToInt32(inventorForm_update_forreplacement_txtBox.Text); int forScrapToAdd = Convert.ToInt32(inventorForm_update_forscrap_txtBox.Text); decimal SellingPrice = Convert.ToDecimal(inventoryForm_update_sellingPrice_txtBox.Text);
                    decimal ForReplacementValue = SellingPrice * Convert.ToDecimal(inventoryForm_update_forReplacement_lbl.Text); decimal ForScrapValue = SellingPrice * Convert.ToDecimal(inventorForm_update_forsrap_lbl.Text); decimal ForNewReplacementValue = SellingPrice * Convert.ToDecimal(inventorForm_update_forreplacement_txtBox.Text); decimal ForNewScrapValue = SellingPrice * Convert.ToDecimal(inventorForm_update_forscrap_txtBox.Text);
                    inventoryForm_update_totalValue_lbl.Text = Convert.ToString(ForReplacementValue + ForScrapValue);
                    connection.Close();
                    connection.Open();
                    MySqlCommand command4 = new MySqlCommand("insert into forscrapandreplacment_table (product_ID,product_name,product_expirationDate" +
                                 ",product_category,product_purchasePrice," +
                                 "product_sellingPrice,product_productLine,product_supplier,product_additionalDetails,product_addedBy,product_addedDate,product_numberofScrap,product_numberofReplacement,product_totalValue) values " +
                                 "('" + inventoryForm_update_productID_txtBox.Text + "','" + inventoryForm_update_productName_txtBox.Text + "','" + inventoryForm_update_expirationDate_DTpicker.Text + "'" +
                                 ",'" + inventoryForm_update_category_txtBox.Text + "'," +
                                 "'" + inventoryForm_update_purchaseprice_txtBox.Text + "','" + inventoryForm_update_sellingPrice_txtBox.Text + "','" + inventoryForm_update_productline_txtBox.Text + "'" +
                                 ",'" + inventoryForm_update_supplier_cBox.Text +
                                 "','" + inventoryForm_update_additionaldetails_txtBox.Text + "','" + inventoryForm_user_Firstname.Text + "','" + DateTime.Now.ToString() + "','" + inventorForm_update_forscrap_txtBox.Text + "','" + inventorForm_update_forreplacement_txtBox.Text + "','" + inventoryForm_update_totalValue_lbl.Text + "')", connection);
                        command4.ExecuteNonQuery();
                    MessageBox.Show("For replacement added successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    closeUpdateForm();
                }
            }

            else
            {
                return;
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toPOSForm.POS_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toPOSForm.account_level.Text = account_level.Text;
            this.Close();
            toPOSForm.Show();
        }
        private void inventoryForm_addnewitem_btn_Click(object sender, EventArgs e)
        {
            switch (inventoryForm_addnewitem_btn.Text)
            {
                case "ADD NEW ITEM":
                    if (inventoryForm_newproductid_txtBox.Text == "")
                    {
                        inventoryForm_newproductid_txtBox.Text = NumberRandom.ToString();
                    }
                    panel5.Visible = false;
                    panel1.Visible = false;
                    inventoryForm_addnewitem_btn.Visible = false;
                    inventoryForm_inventorylist_listview.Visible = false;
                    inventoryForm_addnewItem_panel.Visible = true;
                    inventoryForm_newproductid_txtBox.Clear();
                    Random rand = new Random();
                    NumberRandom = rand.Next(111111111, 999999999);
                    inventoryForm_newproductid_txtBox.Text = NumberRandom.ToString();
                    populateSupplierComboBox(); populateCategoryNew();
                    break;
                case "Update Item":
                    inventoryForm_updateForm_panel.Visible = true;
                    inventoryForm_inventorylist_listview.Visible = false;
                    inventoryForm_addnewitem_btn.Visible = false;
                    panel5.Visible = false;
                    panel1.Visible = false;
                    populateSupplierComboBoxUpdate(); populateCategoryUpdate();
                    break;
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            //inventory_Form inventoryForm = new inventory_Form();
            //inventoryForm.inventoryForm_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            //this.Close();
            inventoryForm_addnewItem_panel.Visible = false;
            //inventoryForm.Show();
            panel5.Visible = true;
            panel1.Visible = true;
            inventoryForm_addnewitem_btn.Visible = true;
            inventoryForm_inventorylist_listview.Visible = true;
            ResetForm();
        }

        public void fillLowStocks()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_lowStock_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table where stock_isLow = '" + "true" + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                    item.SubItems.Add(read["stock_product_name"].ToString());
                    item.SubItems.Add(read["stock_number_of_items"].ToString());
                    item.SubItems.Add(read["stock_purchase_price"].ToString());
                    item.SubItems.Add(read["stock_selling_price"].ToString());
                    item.SubItems.Add(read["stock_product_line"].ToString());
                    item.SubItems.Add(read["stock_supplier"].ToString());
                    item.SubItems.Add(read["stock_additional_details"].ToString());
                    item.SubItems.Add(read["stock_expiration_date"].ToString());
                    item.SubItems.Add(read["stock_category"].ToString());
                    item.SubItems.Add(read["stock_date_added"].ToString());
                    item.SubItems.Add(read["stock_date_updated"].ToString());
                    item.SubItems.Add(read["stock_date_archived"].ToString());
                    item.SubItems.Add(read["stock_status"].ToString());
                    item.SubItems.Add(read["stock_addedbyUser_name"].ToString());
                    item.SubItems.Add(read["stock_lastupdatedby"].ToString());
                    item.SubItems.Add(read["stock_low_stock_alert"].ToString());
                    item.BackColor = Color.IndianRed;
                    inventoryForm_lowStock_lstView.Items.Add(item);
                    inventoryForm_lowStock_lstView.FullRowSelect = true;


                }
                connection.Close();
            }

            catch (Exception z)
            {
                MessageBox.Show("ERROR: " + z);
            }
        }
        private void inventoryForm_lowStocks_btn_Click(object sender, EventArgs e)
        {
            switch (inventoryForm_lowStocks_btn.Text)
            {
                case "View":
                    inventoryForm_lowStock_lstView.Visible = true; inventoryForm_inventorylist_listview.Visible = false; inventoryForm_addnewitem_btn.Visible = false; inventoryForm_lowStocks_btn.Text = "Done";
                    fillLowStocks(); inventoryForm_BOstocks_btn.Enabled = false;
                    break;
                case "Done":
                    inventoryForm_lowStocks_btn.Text = "View";
                    fill_InventoryList(); inventoryForm_lowStock_lstView.Visible = false; inventoryForm_inventorylist_listview.Visible = true; inventoryForm_addnewitem_btn.Visible = true;
                    inventoryForm_BOstocks_btn.Enabled = true;
                    break;
            }

        }

        private void inventoryForm_refreshList_btn_Click(object sender, EventArgs e)
        {
            inventoryForm_sort_comboBox.SelectedItem = "None";
            inventoryForm_inventorylist_listview.Items.Clear();
            fill_InventoryList();

        }

        private void inventoryForm_addnewproduct_btn_Click(object sender, EventArgs e)
        {
            string purchaseValue = inventoryForm_newitempurchaseprice_txtBox.Text; string sellingValue = inventoryForm_newitemsellingprice_txtBox.Text;

            if (inventoryForm_newproductid_txtBox.Text == "" || inventoryForm_newproductname_txtBox.Text == "" || inventoryForm_newitemamount_txtBox.Text == "" ||
                inventoryForm_newitemcategory_cmBox.Text == "" || inventoryForm_newitempurchaseprice_txtBox.Text == "" || inventoryForm_newitemsellingprice_txtBox.Text == "" ||
                inventoryForm_newproductline_txtBox.Text == "" || inventoryForm_newsupplier_CBbox.Text == "" || inventoryForm_lowStockAlert_txtBox.Text == "")
            {
                MessageBox.Show("Please do not leave a textbox empty. Thank you", "Value missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Convert.ToDecimal(purchaseValue) > Convert.ToDecimal(sellingValue))
                {
                    MessageBox.Show("Purchase price is greater than Selling price! You will not earn profit", "No Profit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    add_product_to_inventory();
                }
            }
        }

        private void inventory_Form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            inventoryForm_timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            inventoryForm_datenow_value.Text = DateTime.Now.ToShortDateString();
        }

        private void inventoryForm_inventorylist_listview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            int statusValue;
            ListViewItem inventoryList = inventoryForm_inventorylist_listview.SelectedItems[i];
            inventoryForm_update_productID_txtBox.Text = inventoryList.SubItems[0].Text;
            inventoryForm_update_productName_txtBox.Text = inventoryList.SubItems[1].Text;
            inventoryForm_update_stocks_txtBox.Text = inventoryList.SubItems[2].Text;
            inventoryForm_update_purchaseprice_txtBox.Text = inventoryList.SubItems[3].Text;
            inventoryForm_update_sellingPrice_txtBox.Text = inventoryList.SubItems[4].Text;
            inventoryForm_update_productline_txtBox.Text = inventoryList.SubItems[5].Text;
            inventoryForm_update_supplier_cBox.Text = inventoryList.SubItems[6].Text;
            inventoryForm_update_additionaldetails_txtBox.Text = inventoryList.SubItems[7].Text;


            inventoryForm_update_expirationDate_DTpicker.Text = inventoryList.SubItems[8].Text;
            inventoryForm_update_category_txtBox.Text = inventoryList.SubItems[9].Text;
            statusValue = Convert.ToInt32(inventoryList.SubItems[13].Text);
            if (statusValue == 1)
            {
                inventoryForm_update_statusActive_rBtn.Checked = true;
            }
            else if (statusValue == 0)
            {
                inventoryForm_update_statusHidden_rBtn.Checked = true;
            }
            inventoryForm_updateLowStockalert_txtBox.Text = inventoryList.SubItems[16].Text;
            inventoryForm_addnewitem_btn.Text = "Update Item";

            MySqlConnection connection = new MySqlConnection(myConnection);
            inventoryForm_lowStock_lstView.Items.Clear();
            connection.Close();

            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string query = "select * from forscrapandreplacment_table where product_ID = '" + inventoryForm_update_productID_txtBox.Text + "'";
            command.CommandText = query;
            MySqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                inventoryForm_update_forReplacement_lbl.Text = read["product_numberofReplacement"].ToString();
                inventorForm_update_forsrap_lbl.Text = read["product_numberofScrap"].ToString();
            }
            inventorForm_update_forreplacement_txtBox.Text = inventoryForm_update_forReplacement_lbl.Text; inventorForm_update_forscrap_txtBox.Text = inventorForm_update_forsrap_lbl.Text;

            connection.Close();
            //decimal forReplacementTotalCount = Convert.ToDecimal(inventoryForm_update_forReplacement_lbl.Text); decimal forScrapTotalCount = Convert.ToDecimal(inventorForm_update_forsrap_lbl.Text);
            //int forReplacementToAdd = Convert.ToInt32(inventorForm_update_forreplacement_txtBox.Text); int forScrapToAdd = Convert.ToInt32(inventorForm_update_forscrap_txtBox.Text); decimal SellingPrice = Convert.ToDecimal(inventoryForm_update_sellingPrice_txtBox.Text);
            //decimal ForReplacementValue = SellingPrice * Convert.ToDecimal(forReplacementTotalCount); decimal ForScrapValue = SellingPrice * Convert.ToDecimal(forScrapTotalCount); decimal ForNewReplacementValue = SellingPrice * Convert.ToDecimal(forReplacementToAdd); decimal ForNewScrapValue = SellingPrice * Convert.ToDecimal(forScrapToAdd);
            //inventoryForm_update_totalValue_lbl.Text = Convert.ToString(ForReplacementValue + ForScrapValue);

        }

        private void inventoryForm_update_back_btn_Click(object sender, EventArgs e)
        {
            closeUpdateForm();
        }

        private void inventoryForm_sortGo_btn_Click(object sender, EventArgs e)
        {
            if (inventoryForm_sort_comboBox.Text == "None")
            {
                fill_InventoryList();
            }
            else
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    inventoryForm_inventorylist_listview.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "select * from inventory_table where stock_category = '" + inventoryForm_sort_comboBox.Text + "'";
                    command.CommandText = query;
                    MySqlDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {
                        string purchasePrice = read["stock_selling_price"].ToString(); string numberofItems = read["stock_number_of_items"].ToString();
                        decimal TotalWorth = Convert.ToInt32(purchasePrice) * Convert.ToInt32(numberofItems);
                        string numberofStocks = read["stock_number_of_items"].ToString(); string lowStockValue = read["stock_low_stock_alert"].ToString();
                        string isLowStock = "";
                        if (Convert.ToInt32(numberofStocks) <= Convert.ToInt32(lowStockValue))
                        {
                            isLowStock = "true";
                        }
                        else
                        {
                            isLowStock = "false";
                        }
                        ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                        //item.SubItems.Add(read["stock_product_name"].ToString()+ " " + read["stock_additional_details"].ToString());
                        item.SubItems.Add(read["stock_product_name"].ToString());
                        item.SubItems.Add(read["stock_number_of_items"].ToString());
                        item.SubItems.Add(read["stock_purchase_price"].ToString());
                        item.SubItems.Add(read["stock_selling_price"].ToString());
                        item.SubItems.Add(read["stock_product_line"].ToString());
                        item.SubItems.Add(read["stock_supplier"].ToString());
                        item.SubItems.Add(read["stock_additional_details"].ToString());
                        item.SubItems.Add(read["stock_expiration_date"].ToString());
                        item.SubItems.Add(read["stock_category"].ToString());
                        item.SubItems.Add(read["stock_date_added"].ToString());
                        item.SubItems.Add(read["stock_date_updated"].ToString());
                        item.SubItems.Add(read["stock_date_archived"].ToString());
                        item.SubItems.Add(read["stock_status"].ToString());
                        item.SubItems.Add(read["stock_addedbyUser_name"].ToString());
                        item.SubItems.Add(read["stock_lastupdatedby"].ToString());
                        item.SubItems.Add(read["stock_low_stock_alert"].ToString());
                        item.SubItems.Add(isLowStock.ToString());
                        item.SubItems.Add(TotalWorth.ToString());
                        if (isLowStock == "true")
                        {
                            item.BackColor = Color.IndianRed;
                        }
                        inventoryForm_inventorylist_listview.Items.Add(item);
                        inventoryForm_inventorylist_listview.FullRowSelect = true;




                    }
                    connection.Close();
                }

                catch (Exception x)
                {
                    MessageBox.Show("ERROR: " + x);
                }
            }
        }

        private void inventoryForm_inventorylistsearch_txtbox_TextChanged(object sender, EventArgs e)
        {
            switch (inventoryForm_lowStocks_btn.Text)
            {
                case "View":
                    try
                    {
                        if (inventoryForm_inventorylistsearch_txtbox.Text == "")
                        {
                            fill_InventoryList();
                        }
                        else
                        {
                            MySqlConnection connection = new MySqlConnection(myConnection);
                            inventoryForm_inventorylist_listview.Items.Clear();
                            connection.Close();

                            connection.Open();
                            MySqlCommand command = connection.CreateCommand();
                            string query = "SELECT * FROM inventory_table WHERE stock_product_name LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR stock_addedbyUser_name LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR stock_id_number LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'";
                            //"select * from inventory_table where stock_id_number  = '" + inventoryForm_inventorylistsearch_txtbox.Text + "' OR [stock_product_name] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR [stock_addedbyUser_name] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'OR [stock_additional_details] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'";
                            command.CommandText = query;
                            MySqlDataReader read = command.ExecuteReader();

                            while (read.Read())
                            {
                                string purchasePrice = read["stock_selling_price"].ToString(); string numberofItems = read["stock_number_of_items"].ToString();
                                decimal TotalWorth = Convert.ToDecimal(purchasePrice) * Convert.ToDecimal(numberofItems);
                                string numberofStocks = read["stock_number_of_items"].ToString(); string lowStockValue = read["stock_low_stock_alert"].ToString();
                                string isLowStock = "";
                                if (Convert.ToInt32(numberofStocks) <= Convert.ToInt32(lowStockValue))
                                {
                                    isLowStock = "true";
                                }
                                else
                                {
                                    isLowStock = "false";
                                }
                                ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                                item.SubItems.Add(read["stock_product_name"].ToString());
                                item.SubItems.Add(read["stock_number_of_items"].ToString());
                                item.SubItems.Add(read["stock_purchase_price"].ToString());
                                item.SubItems.Add(read["stock_selling_price"].ToString());
                                item.SubItems.Add(read["stock_product_line"].ToString());
                                item.SubItems.Add(read["stock_supplier"].ToString());
                                item.SubItems.Add(read["stock_additional_details"].ToString());
                                item.SubItems.Add(read["stock_expiration_date"].ToString());
                                item.SubItems.Add(read["stock_category"].ToString());
                                item.SubItems.Add(read["stock_date_added"].ToString());
                                item.SubItems.Add(read["stock_date_updated"].ToString());
                                item.SubItems.Add(read["stock_date_archived"].ToString());
                                item.SubItems.Add(read["stock_status"].ToString());
                                item.SubItems.Add(read["stock_addedbyUser_name"].ToString());
                                item.SubItems.Add(read["stock_lastupdatedby"].ToString());
                                item.SubItems.Add(read["stock_low_stock_alert"].ToString());
                                if (isLowStock == "true")
                                {
                                    item.BackColor = Color.IndianRed;
                                }
                                inventoryForm_inventorylist_listview.Items.Add(item);
                                inventoryForm_inventorylist_listview.FullRowSelect = true;
                            }
                            connection.Close();
                        }
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR: " + x);
                    }
                    break;
                case "Done":
                    try
                    {
                        if (inventoryForm_inventorylistsearch_txtbox.Text == "")
                        {
                            fillLowStocks();
                        }
                        else
                        {
                            MySqlConnection connection = new MySqlConnection(myConnection);
                            inventoryForm_lowStock_lstView.Items.Clear();
                            connection.Close();

                            connection.Open();
                            MySqlCommand command = connection.CreateCommand();
                            string query = "SELECT * FROM inventory_table WHERE stock_isLow = '" + "true" + "' AND stock_product_name LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR stock_addedbyUser_name LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR stock_id_number LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'";
                            //"select * from inventory_table where stock_id_number  = '" + inventoryForm_inventorylistsearch_txtbox.Text + "' OR [stock_product_name] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%' OR [stock_addedbyUser_name] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'OR [stock_additional_details] LIKE '%" + inventoryForm_inventorylistsearch_txtbox.Text + "%'";
                            command.CommandText = query;
                            MySqlDataReader read = command.ExecuteReader();

                            while (read.Read())
                            {
                                string purchasePrice = read["stock_selling_price"].ToString(); string numberofItems = read["stock_number_of_items"].ToString();
                                decimal TotalWorth = Convert.ToInt32(purchasePrice) * Convert.ToInt32(numberofItems);
                                string numberofStocks = read["stock_number_of_items"].ToString(); string lowStockValue = read["stock_low_stock_alert"].ToString();
                                string isLowStock = "";
                                if (Convert.ToInt32(numberofStocks) <= Convert.ToInt32(lowStockValue))
                                {
                                    isLowStock = "true";
                                }
                                else
                                {
                                    isLowStock = "false";
                                }
                                ListViewItem item = new ListViewItem(read["stock_id_number"].ToString());
                                //item.SubItems.Add(read["stock_product_name"].ToString()+ " " + read["stock_additional_details"].ToString());
                                item.SubItems.Add(read["stock_product_name"].ToString());
                                item.SubItems.Add(read["stock_number_of_items"].ToString());
                                item.SubItems.Add(read["stock_purchase_price"].ToString());
                                item.SubItems.Add(read["stock_selling_price"].ToString());
                                item.SubItems.Add(read["stock_product_line"].ToString());
                                item.SubItems.Add(read["stock_supplier"].ToString());
                                item.SubItems.Add(read["stock_additional_details"].ToString());
                                item.SubItems.Add(read["stock_expiration_date"].ToString());
                                item.SubItems.Add(read["stock_category"].ToString());
                                item.SubItems.Add(read["stock_date_added"].ToString());
                                item.SubItems.Add(read["stock_date_updated"].ToString());
                                item.SubItems.Add(read["stock_date_archived"].ToString());
                                item.SubItems.Add(read["stock_status"].ToString());
                                item.SubItems.Add(read["stock_addedbyUser_name"].ToString());
                                item.SubItems.Add(read["stock_lastupdatedby"].ToString());
                                item.SubItems.Add(read["stock_low_stock_alert"].ToString());
                                item.SubItems.Add(isLowStock.ToString());
                                item.SubItems.Add(TotalWorth.ToString());
                                if (isLowStock == "true")
                                {
                                    item.BackColor = Color.IndianRed;
                                }
                                inventoryForm_lowStock_lstView.Items.Add(item);
                                inventoryForm_lowStock_lstView.FullRowSelect = true;
                            }
                            connection.Close();
                        }
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR: " + x);
                    }
                    break;
            }
            
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            try
            {
                decimal numberofItems = Convert.ToDecimal(inventoryForm_newitemamount_txtBox.Text); decimal purchasePriceValue = Convert.ToDecimal(inventoryForm_newitempurchaseprice_txtBox.Text);
                decimal sellingPriceValue = Convert.ToDecimal(inventoryForm_newitemsellingprice_txtBox.Text);

                decimal purchaseXnumberofItems = purchasePriceValue * numberofItems; decimal sellingXnumberofItems = sellingPriceValue * numberofItems;
                decimal profitValue = sellingXnumberofItems - purchaseXnumberofItems;
                inventoryForm_add_profit_lbl.Text = profitValue.ToString();
            }
            
            catch (Exception x)
            {
                MessageBox.Show("ERROR: " + x);
            }
        }

        private void update_test_Btn_Click(object sender, EventArgs e)
        {
            decimal numberofItems = Convert.ToDecimal(inventoryForm_update_stocks_txtBox.Text); decimal purchasePriceValue = Convert.ToDecimal(inventoryForm_update_purchaseprice_txtBox.Text);
            decimal sellingPriceValue = Convert.ToDecimal(inventoryForm_update_sellingPrice_txtBox.Text);

            decimal purchaseXnumberofItems = purchasePriceValue * numberofItems; decimal sellingXnumberofItems = sellingPriceValue * numberofItems;
            decimal profitValue = sellingXnumberofItems - purchaseXnumberofItems;
            inventoryForm_update_profit_lbl.Text = profitValue.ToString();
        }

        private void inventoryForm_update_update_btn_Click(object sender, EventArgs e)
        {
            string purchaseValue = inventoryForm_update_purchaseprice_txtBox.Text; string sellingValue = inventoryForm_update_sellingPrice_txtBox.Text;
            if (Convert.ToDecimal(purchaseValue) > Convert.ToDecimal(sellingValue))
            {
                MessageBox.Show("Purchase price is greater than Selling price! You will not earn profit", "No Profit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Convert.ToInt32(inventorForm_update_forreplacement_txtBox.Text) > Convert.ToInt32(inventoryForm_update_stocks_txtBox.Text))
            {
                MessageBox.Show("For replacement value is greater than current stock count, Please double check and try again.", "For replacment greater than product count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                inventorForm_update_forreplacement_txtBox.Text = "0";
            }
            else if (Convert.ToInt32(inventorForm_update_forscrap_txtBox.Text) > Convert.ToInt32(inventoryForm_update_stocks_txtBox.Text))
            {
                MessageBox.Show("For scrap value is greater than current stock count, Please double check and try again.", "For scrap greater than product count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                inventorForm_update_forscrap_txtBox.Text = "0";
            }
            else
            {
                updatemethod();
            }
        }

        public void populateReplacement()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_forReplacement_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from forscrapandreplacment_table where product_numberofReplacement != '" + "0" + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                    item.SubItems.Add(read["product_name"].ToString());
                    item.SubItems.Add(read["product_expirationDate"].ToString());
                    item.SubItems.Add(read["product_category"].ToString());
                    item.SubItems.Add(read["product_purchasePrice"].ToString());
                    item.SubItems.Add(read["product_sellingPrice"].ToString());
                    item.SubItems.Add(read["product_productLine"].ToString());
                    item.SubItems.Add(read["product_supplier"].ToString());
                    item.SubItems.Add(read["product_additionalDetails"].ToString());
                    item.SubItems.Add(read["product_addedBy"].ToString());
                    item.SubItems.Add(read["product_addedDate"].ToString());
                    item.SubItems.Add(read["product_numberofReplacement"].ToString());
                    item.SubItems.Add(read["product_totalValue"].ToString());
                    item.BackColor = Color.SlateBlue;
                    inventoryForm_forReplacement_lstView.Items.Add(item);
                    inventoryForm_forReplacement_lstView.FullRowSelect = true;
                }
                connection.Close();
            }

            catch (Exception x)
            {
                MessageBox.Show("ERROR SA FOR SCRAP POPULATE LISTVIEW");
            }
        }
        public void populateScrap()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventoryForm_forScrap_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from forscrapandreplacment_table where product_numberofScrap != '" + "0" + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                    item.SubItems.Add(read["product_name"].ToString());
                    item.SubItems.Add(read["product_expirationDate"].ToString());
                    item.SubItems.Add(read["product_category"].ToString());
                    item.SubItems.Add(read["product_purchasePrice"].ToString());
                    item.SubItems.Add(read["product_sellingPrice"].ToString());
                    item.SubItems.Add(read["product_productLine"].ToString());
                    item.SubItems.Add(read["product_supplier"].ToString());
                    item.SubItems.Add(read["product_additionalDetails"].ToString());
                    item.SubItems.Add(read["product_addedBy"].ToString());
                    item.SubItems.Add(read["product_addedDate"].ToString());
                    item.SubItems.Add(read["product_numberofScrap"].ToString());
                    item.SubItems.Add(read["product_totalValue"].ToString());
                    item.BackColor = Color.MediumVioletRed;
                    inventoryForm_forScrap_lstView.Items.Add(item);
                    inventoryForm_forScrap_lstView.FullRowSelect = true;
                }
                connection.Close();
            }

            catch (Exception x)
            {
                MessageBox.Show("ERROR SA FOR SCRAP POPULATE LISTVIEW");
            }
        }
        private void inventoryForm_BOstocks_btn_Click(object sender, EventArgs e)
        {
            switch (inventoryForm_BOstocks_btn.Text)
            {
                case "View":
                    inventoryForm_inventorylist_listview.Visible = false; inventoryForm_lowStock_lstView.Visible = false; inventoryForm_addnewitem_btn.Visible = false;
                    inventoryForm_forReplacement_lbl.Visible = true; inventoryForm_forReplacement_lstView.Visible = true; inventoryForm_forScrap_lbl.Visible = true; inventoryForm_forScrap_lstView.Visible = true;
                    inventoryForm_BOstocks_btn.Text = "Done"; inventoryForm_sort_comboBox.Enabled = false; inventoryForm_sortGo_btn.Enabled = false; inventoryForm_refreshList_btn.Enabled = false;
                    inventoryForm_nonInventory_Sales_btn.Enabled = false; inventoryForm_lowStocks_btn.Enabled = false; inventoryForm_inventorylistsearch_txtbox.Enabled = false;
                    label6.Visible = true; inventoryForm_forScrapSearch_txtBox.Visible = true; label7.Visible = true; inventoryForm_forReplacementSearch_txtBox.Visible = true;
                    populateReplacement();
                    populateScrap();
                    break;
                case "Done":
                    inventoryForm_inventorylist_listview.Visible = true; inventoryForm_lowStock_lstView.Visible = false; inventoryForm_addnewitem_btn.Visible = true;
                    inventoryForm_forReplacement_lbl.Visible = false; inventoryForm_forReplacement_lstView.Visible = false; inventoryForm_forScrap_lbl.Visible = false; inventoryForm_forScrap_lstView.Visible = false;
                    inventoryForm_BOstocks_btn.Text = "View"; fill_InventoryList(); inventoryForm_sort_comboBox.Enabled = true; inventoryForm_sortGo_btn.Enabled = true; inventoryForm_refreshList_btn.Enabled = true;
                    inventoryForm_nonInventory_Sales_btn.Enabled = true; inventoryForm_lowStocks_btn.Enabled = true; inventoryForm_inventorylistsearch_txtbox.Enabled = true;
                    label6.Visible = false; inventoryForm_forScrapSearch_txtBox.Visible = false; label7.Visible = false; inventoryForm_forReplacementSearch_txtBox.Visible = false;
                    break;
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toTransactionForm.account_level.Text = account_level.Text;
            this.Close();
            toTransactionForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toCustomer.account_level.Text = account_level.Text;
            this.Close();
            toCustomer.Show();
        }

        private void inventoryForm_newitemamount_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void inventoryForm_newitempurchaseprice_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && inventoryForm_newitempurchaseprice_txtBox.Text.IndexOf('.') >= 1)
            {
                e.Handled = true;
            }
        }
        private void inventoryForm_newitemsellingprice_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && inventoryForm_newitemsellingprice_txtBox.Text.IndexOf('.') >= 1)
            {
                e.Handled = true;
            }
        }

        private void inventoryForm_lowStockAlert_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void inventoryForm_update_stocks_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void inventoryForm_updateLowStockalert_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

            private void inventoryForm_update_purchaseprice_txtBox_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && inventoryForm_newitemsellingprice_txtBox.Text.IndexOf('.') >= 1)
                {
                    e.Handled = true;
                }

            }

            private void inventoryForm_update_sellingPrice_txtBox_KeyPress(object sender, KeyPressEventArgs e)
            {

                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && inventoryForm_newitemsellingprice_txtBox.Text.IndexOf('.') >= 1)
                {
                    e.Handled = true;
                }
            }

        private void inventorForm_update_forreplacement_chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(inventorForm_update_forreplacement_chkBox.Checked == true) { inventorForm_update_forreplacement_txtBox.Enabled = true; } else { inventorForm_update_forreplacement_txtBox.Enabled = false; }
        }

        private void inventorForm_update_forsrap_chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(inventorForm_update_forsrap_chkBox.Checked == true) { inventorForm_update_forscrap_txtBox.Enabled = true; } else { inventorForm_update_forscrap_txtBox.Enabled = false; }
        }

        private void inventorForm_update_forreplacement_txtBox_TextChanged(object sender, EventArgs e)
        {

            if (inventorForm_update_forreplacement_txtBox.Text == "") inventorForm_update_forreplacement_txtBox.Text = inventoryForm_update_forReplacement_lbl.Text;
            decimal productPrice = Convert.ToDecimal(inventoryForm_update_sellingPrice_txtBox.Text); int forReplacementCount = Convert.ToInt32(inventorForm_update_forreplacement_txtBox.Text);
            decimal TotalValue = Convert.ToDecimal(inventoryForm_update_totalValue_lbl.Text);
            decimal changeTotalValue = productPrice * Convert.ToDecimal(forReplacementCount) + TotalValue;
            inventoryForm_update_totalValue_lbl.Text = changeTotalValue.ToString();
        }

        private void inventorForm_update_forscrap_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (inventorForm_update_forscrap_txtBox.Text == "") inventorForm_update_forscrap_txtBox.Text = inventorForm_update_forsrap_lbl.Text;
            decimal productPrice = Convert.ToDecimal(inventoryForm_update_sellingPrice_txtBox.Text); int forScrapCount = Convert.ToInt32(inventorForm_update_forscrap_txtBox.Text);
            decimal TotalValue = Convert.ToDecimal(inventoryForm_update_totalValue_lbl.Text);
            decimal changeTotalValue = productPrice * Convert.ToDecimal(forScrapCount) + TotalValue;
            inventoryForm_update_totalValue_lbl.Text = changeTotalValue.ToString();
        }

        private void inventoryForm_Users_Btn_Click(object sender, EventArgs e)
        {
            employees_Form toUserForm = new employees_Form();
            toUserForm.employeeForm_user_firstname.Text = inventoryForm_user_Firstname.Text;
            toUserForm.employeeForm_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toUserForm.account_level.Text = account_level.Text;
            this.Close();
            toUserForm.Show();
        }

        private void inventoryForm_Supplier_Btn_Click(object sender, EventArgs e)
        {
            supplier_Form toSupplier = new supplier_Form();
            toSupplier.Supplier_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toSupplier.Supplier_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toSupplier.account_level.Text = account_level.Text;
            this.Close();
            toSupplier.Show();
        }

        private void inventoryForm_Sales_Btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            toSalesForm.account_level.Text = account_level.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void inventoryForm_forReplacementSearch_txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (inventoryForm_forReplacementSearch_txtBox.Text == "")
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(myConnection);
                        inventoryForm_forReplacement_lstView.Items.Clear();
                        connection.Close();

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query = "select * from forscrapandreplacment_table where product_numberofReplacement != '" + "0" + "'";
                        command.CommandText = query;
                        MySqlDataReader read = command.ExecuteReader();

                        while (read.Read())
                        {
                            ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                        item.SubItems.Add(read["product_name"].ToString());
                        item.SubItems.Add(read["product_expirationDate"].ToString());
                        item.SubItems.Add(read["product_category"].ToString());
                        item.SubItems.Add(read["product_purchasePrice"].ToString());
                        item.SubItems.Add(read["product_sellingPrice"].ToString());
                        item.SubItems.Add(read["product_productLine"].ToString());
                        item.SubItems.Add(read["product_supplier"].ToString());
                        item.SubItems.Add(read["product_additionalDetails"].ToString());
                        item.SubItems.Add(read["product_addedBy"].ToString());
                        item.SubItems.Add(read["product_addedDate"].ToString());
                        item.SubItems.Add(read["product_numberofReplacement"].ToString());
                        item.SubItems.Add(read["product_totalValue"].ToString());
                        item.BackColor = Color.SlateBlue;
                        inventoryForm_forReplacement_lstView.Items.Add(item);
                        inventoryForm_forReplacement_lstView.FullRowSelect = true;
                        }
                        connection.Close();
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR SA FOR SCRAP POPULATE LISTVIEW");
                    }
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    inventoryForm_forReplacement_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM forscrapandreplacment_table WHERE product_numberofReplacement != '" + "0" + "' && product_ID LIKE '%" + inventoryForm_forReplacementSearch_txtBox.Text + "%' OR product_supplier LIKE '%" + inventoryForm_forReplacementSearch_txtBox.Text + "%' OR product_expirationDate LIKE '%" + inventoryForm_forReplacementSearch_txtBox.Text + "%' OR product_name LIKE '%" + inventoryForm_forReplacementSearch_txtBox.Text + "%'";
                    command.CommandText = query;
                    MySqlDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {

                        ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                        item.SubItems.Add(read["product_name"].ToString());
                        item.SubItems.Add(read["product_expirationDate"].ToString());
                        item.SubItems.Add(read["product_category"].ToString());
                        item.SubItems.Add(read["product_purchasePrice"].ToString());
                        item.SubItems.Add(read["product_sellingPrice"].ToString());
                        item.SubItems.Add(read["product_productLine"].ToString());
                        item.SubItems.Add(read["product_supplier"].ToString());
                        item.SubItems.Add(read["product_additionalDetails"].ToString());
                        item.SubItems.Add(read["product_addedBy"].ToString());
                        item.SubItems.Add(read["product_addedDate"].ToString());
                        item.SubItems.Add(read["product_numberofReplacement"].ToString());
                        item.SubItems.Add(read["product_totalValue"].ToString());
                        item.BackColor = Color.SlateBlue;
                        inventoryForm_forReplacement_lstView.Items.Add(item);
                        inventoryForm_forReplacement_lstView.FullRowSelect = true;

                    }
                    connection.Close();
                }
            }

            catch (Exception x)
            {
                MessageBox.Show("ERROR: " + x);
            }
        }

        private void inventoryForm_forScrapSearch_txtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (inventoryForm_forScrapSearch_txtBox.Text == "")
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(myConnection);
                        inventoryForm_forScrap_lstView.Items.Clear();
                        connection.Close();

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query = "select * from forscrapandreplacment_table where product_numberofScrap != '" + "0" + "'";
                        command.CommandText = query;
                        MySqlDataReader read = command.ExecuteReader();

                        while (read.Read())
                        {
                            ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                            item.SubItems.Add(read["product_name"].ToString());
                            item.SubItems.Add(read["product_expirationDate"].ToString());
                            item.SubItems.Add(read["product_category"].ToString());
                            item.SubItems.Add(read["product_purchasePrice"].ToString());
                            item.SubItems.Add(read["product_sellingPrice"].ToString());
                            item.SubItems.Add(read["product_productLine"].ToString());
                            item.SubItems.Add(read["product_supplier"].ToString());
                            item.SubItems.Add(read["product_additionalDetails"].ToString());
                            item.SubItems.Add(read["product_addedBy"].ToString());
                            item.SubItems.Add(read["product_addedDate"].ToString());
                            item.SubItems.Add(read["product_numberofScrap"].ToString());
                            item.SubItems.Add(read["product_totalValue"].ToString());
                            item.BackColor = Color.MediumVioletRed;
                            inventoryForm_forScrap_lstView.Items.Add(item);
                            inventoryForm_forScrap_lstView.FullRowSelect = true;
                        }
                        connection.Close();
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR SA FOR SCRAP POPULATE LISTVIEW");
                    }
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);
                    inventoryForm_forScrap_lstView.Items.Clear();
                    connection.Close();

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query = "SELECT * FROM forscrapandreplacment_table WHERE product_numberofScrap != '" + "0" + "' && product_ID LIKE '%" + inventoryForm_forScrapSearch_txtBox.Text + "%' OR product_supplier LIKE '%" + inventoryForm_forScrapSearch_txtBox.Text + "%' OR product_expirationDate LIKE '%" + inventoryForm_forScrapSearch_txtBox.Text + "%' OR product_name LIKE '%" + inventoryForm_forScrapSearch_txtBox.Text + "%'";
                    command.CommandText = query;
                    MySqlDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {


                        ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                        item.SubItems.Add(read["product_name"].ToString());
                        item.SubItems.Add(read["product_expirationDate"].ToString());
                        item.SubItems.Add(read["product_category"].ToString());
                        item.SubItems.Add(read["product_purchasePrice"].ToString());
                        item.SubItems.Add(read["product_sellingPrice"].ToString());
                        item.SubItems.Add(read["product_productLine"].ToString());
                        item.SubItems.Add(read["product_supplier"].ToString());
                        item.SubItems.Add(read["product_additionalDetails"].ToString());
                        item.SubItems.Add(read["product_addedBy"].ToString());
                        item.SubItems.Add(read["product_addedDate"].ToString());
                        item.SubItems.Add(read["product_numberofScrap"].ToString());
                        item.SubItems.Add(read["product_totalValue"].ToString());
                        item.BackColor = Color.MediumVioletRed;
                        inventoryForm_forScrap_lstView.Items.Add(item);
                        inventoryForm_forScrap_lstView.FullRowSelect = true;

                    }
                    connection.Close();
                }
            }

            catch (Exception x)
            {
                MessageBox.Show("ERROR: " + x);
            }
        }

        private void inventoryForm_newitempurchaseprice_txtBox_TextChanged(object sender, EventArgs e)
        {
            if(inventoryForm_newitempurchaseprice_txtBox.Text == "" || inventoryForm_newitemsellingprice_txtBox.Text == "" || inventoryForm_newitemamount_txtBox.Text == "")
            {
                testButton.Enabled = false;
            }
            else
            {
                testButton.Enabled = true;
            }
            
        }

        private void inventoryForm_newitemsellingprice_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (inventoryForm_newitempurchaseprice_txtBox.Text == "" || inventoryForm_newitemsellingprice_txtBox.Text == "" || inventoryForm_newitemamount_txtBox.Text == "")
            {
                testButton.Enabled = false;
            }
            else
            {
                testButton.Enabled = true;
            }
        }

        private void inventoryForm_addnewitemclear_btn_Click(object sender, EventArgs e)
        {
            inventoryForm_newproductname_txtBox.Clear();
            inventoryForm_newitemamount_txtBox.Clear();
            inventoryForm_newitemsellingprice_txtBox.Clear();
            inventoryForm_newitempurchaseprice_txtBox.Clear();
            inventoryForm_newproductline_txtBox.Clear();
            inventoryForm_newitemadditionaldetails_txtBox.Clear();
        }

        private void inventorForm_update_forreplacement_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void inventorForm_update_forscrap_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
 }
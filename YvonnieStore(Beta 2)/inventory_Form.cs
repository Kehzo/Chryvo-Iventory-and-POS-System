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
                    if(Convert.ToInt32(numberofStocks)<=Convert.ToInt32(lowStockValue))
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
            int stockValue = Convert.ToInt32(inventoryForm_newitemamount_txtBox.Text); int lowStockValue = Convert.ToInt32(inventoryForm_lowStockAlert_txtBox.Text);
            string isLowValue = "false";
            if(stockValue < lowStockValue)
            {
                isLowValue = "true";
            }
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();

            //MySqlCommand command = new MySqlCommand("insert into data (ID,firstname,lastname,username,password,gender,email,birthdate,useradd,userupdate,userview,userdelete,pic) values ('" + userid.Text + "', '" + firstnametxtbox.Text + "','" + lastnametxtbox.Text + "','" + usernametxtbox.Text + "','" + qwe.encryptpassword(passwordtxtbox.Text) + "','" + truegender + "','" + emailtxtbox.Text + "','" + datetimepicker.Text + "','" + addchecker.Text + "','" + updatechecker.Text + "','" + viewchecker.Text + "','" + deletechecker.Text + "','" + ImageSave + "')", connection);
            MySqlCommand command = new MySqlCommand("insert into inventory_table (stock_id_number,stock_product_name,stock_number_of_items" +
                ",stock_purchase_price,stock_selling_price," +
                "stock_product_line,stock_supplier,stock_additional_details,stock_category,stock_date_added,stock_date_updated,stock_date_archived,stock_status,stock_expiration_date" +
                ",stock_addedbyUser_name,stock_lastupdatedby,stock_low_stock_alert,stock_isLow,stock_forReplacement,stock_forScrap) values " +
                "('" + inventoryForm_newproductid_txtBox.Text + "','" + inventoryForm_newproductname_txtBox.Text + "','" + inventoryForm_newitemamount_txtBox.Text + "'" +
                ",'" + inventoryForm_newitempurchaseprice_txtBox.Text + "'," +
                "'" + inventoryForm_newitemsellingprice_txtBox.Text + "','" + inventoryForm_newproductline_txtBox.Text + "','" + inventoryForm_newsupplier_txtBox.Text + "'" +
                ",'" + inventoryForm_newitemadditionaldetails_txtBox.Text +
                "','" + inventoryForm_newitemcategory_cmBox.Text + "','" + DateTime.Now + "','" + "N/A" + "','" + "N/A" + "','" + "1" + "','" + inventoryForm_newitemexpiration_DTpicker.Text + "','" + inventoryForm_user_Firstname.Text + "','" + "N/A" + "','" + inventoryForm_lowStockAlert_txtBox.Text + "','" + isLowValue + "','" + "false" + "','" + "false" + "')", connection);

            command.ExecuteNonQuery();
            fill_InventoryList();
            inventoryForm_newproductid_txtBox.Clear();
            inventoryForm_newproductname_txtBox.Clear();
            inventoryForm_newitemamount_txtBox.Text = "";
            inventoryForm_newitempurchaseprice_txtBox.Clear();
            inventoryForm_newitemsellingprice_txtBox.Text = "";
            inventoryForm_newproductline_txtBox.Clear();
            inventoryForm_newsupplier_txtBox.Text = "";
            inventoryForm_newitemadditionaldetails_txtBox.Clear();
            inventoryForm_lowStockAlert_txtBox.Clear();
            MessageBox.Show("New item successfully inserted into the inventory list.", "SUCCESS!");
            panel5.Visible = true;
            panel1.Visible = true;
            inventoryForm_addnewitem_btn.Visible = true;
            inventoryForm_inventorylist_listview.Visible = true;
            inventoryForm_addnewItem_panel.Visible = false;
            inventoryWorth();
            checkLowStocks();
        }
        //ADD ITEM TO INVENTORY METHOD ENDS HERE//

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
                string query0 = "select * from inventory_table where stock_id_number = '" + stockID + "'";
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

                    int count = 0;
                    while (read.Read())
                    {
                        count++;
                    }

                    if (count == 1)
                    {
                        connection.Close();
                        connection.Open();
                        MySqlCommand command2 = connection.CreateCommand();
                        string query1 = "update inventory_table set  stock_product_name = '" + inventoryForm_update_productName_txtBox.Text + "' , stock_number_of_items = '" + inventoryForm_update_stocks_txtBox.Text + "' , stock_purchase_price = '" + inventoryForm_update_purchaseprice_txtBox.Text + "' , stock_selling_price = '" + inventoryForm_update_sellingPrice_txtBox.Text + "' , stock_product_line = '" + inventoryForm_update_productline_txtBox.Text + "' , stock_supplier = '" + inventoryForm_update_supplier_cBox.Text + "' , stock_additional_details = '" + inventoryForm_update_additionaldetails_txtBox.Text + "', stock_expiration_date = '" + inventoryForm_update_expirationDate_DTpicker.Text + "' , stock_category = '" + inventoryForm_update_category_txtBox.Text + "' , stock_date_updated = '" + DateTime.Now + "' , stock_status = '" + statusValue + "', stock_lastupdatedby = '" + inventoryForm_user_Firstname.Text + "' where stock_id_number  = '" + inventoryForm_update_productID_txtBox.Text + "' ";
                        command2.CommandText = query1;
                        command2.ExecuteNonQuery();
                        fill_InventoryList();
                        MessageBox.Show("Update successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    //else if (count > 1)
                    //{
                    //    MessageBox.Show("Please make sure there is no duplicate", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
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
                    break;
                case "Update Item":
                    inventoryForm_updateForm_panel.Visible = true;
                    inventoryForm_inventorylist_listview.Visible = false;
                    inventoryForm_addnewitem_btn.Visible = false;
                    panel5.Visible = false;
                    panel1.Visible = false;
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

        private void inventoryForm_lowStocks_btn_Click(object sender, EventArgs e)
        {
            switch (inventoryForm_lowStocks_btn.Text)
            {
                case "View":
                    inventoryForm_lowStock_lstView.Visible = true; inventoryForm_inventorylist_listview.Visible = false; inventoryForm_addnewitem_btn.Visible = false; inventoryForm_lowStocks_btn.Text = "Done";
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
                    break;
                case "Done":
                    inventoryForm_lowStocks_btn.Text = "View";
                    fill_InventoryList(); inventoryForm_lowStock_lstView.Visible = false; inventoryForm_inventorylist_listview.Visible = true; inventoryForm_addnewitem_btn.Visible = true;
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
            if (Convert.ToDecimal(purchaseValue) > Convert.ToDecimal(sellingValue))
            {
                MessageBox.Show("Purchase price is greater than Selling price! You will not earn profit", "No Profit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                add_product_to_inventory();
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
            if(statusValue == 1)
            {
                inventoryForm_update_statusActive_rBtn.Checked = true;
            }
            else if (statusValue == 0)
            {
                inventoryForm_update_statusHidden_rBtn.Checked = true;
            }

            inventoryForm_addnewitem_btn.Text = "Update Item";

        }

        private void inventoryForm_update_back_btn_Click(object sender, EventArgs e)
        {
            inventoryForm_updateForm_panel.Visible = false;
            inventoryForm_inventorylist_listview.Visible = true;
            inventoryForm_addnewitem_btn.Visible = true;
            panel5.Visible = true;
            panel1.Visible = true;
            inventoryForm_addnewitem_btn.Text = "ADD NEW ITEM";
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
            try
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

        private void testButton_Click(object sender, EventArgs e)
        {
            decimal numberofItems = Convert.ToDecimal(inventoryForm_newitemamount_txtBox.Text); decimal purchasePriceValue = Convert.ToDecimal(inventoryForm_newitempurchaseprice_txtBox.Text);
            decimal sellingPriceValue = Convert.ToDecimal(inventoryForm_newitemsellingprice_txtBox.Text);

            decimal purchaseXnumberofItems = purchasePriceValue * numberofItems; decimal sellingXnumberofItems = sellingPriceValue * numberofItems;
            decimal profitValue = sellingXnumberofItems - purchaseXnumberofItems;
            inventoryForm_add_profit_lbl.Text = profitValue.ToString();
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
            else
            {
                updatemethod();
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
                    inventoryForm_nonInventory_Sales_btn.Enabled=false; inventoryForm_lowStocks_btn.Enabled = false; inventoryForm_inventorylistsearch_txtbox.Enabled = false;
                    label6.Visible = true; inventoryForm_forScrapSearch_txtBox.Visible = true; label7.Visible = true; inventoryForm_forReplacementSearch_txtBox.Visible = true;
                    //POPULATE LISTVIEW FOR REPLACEMENT ITEMS START HERE//
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(myConnection);
                        inventoryForm_forReplacement_lstView.Items.Clear();
                        connection.Close();

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query = "select * from inventory_table where stock_forReplacement = '" + "true" + "'";
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
                            item.BackColor = Color.SlateBlue;
                            inventoryForm_forReplacement_lstView.Items.Add(item);
                            inventoryForm_forReplacement_lstView.FullRowSelect = true;
                        }
                        connection.Close();
                    }

                    catch (Exception x)
                    {
                        MessageBox.Show("ERROR SA FOR REPLACEMENT POPULATE LISTVIEW");
                    }
                    //POPULATE LISTVIEW FOR REPLACEMENT ITEMS END HERE//

                    //POPULATE LISTVIEW FOR SCRAP ITEMS START HERE//
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(myConnection);
                        inventoryForm_forScrap_lstView.Items.Clear();
                        connection.Close();

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query = "select * from inventory_table where stock_forScrap = '" + "true" + "'";
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
                    //POPULATE LISTVIEW FOR SCRAP ITEMS END HERE//
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
            this.Close();
            toTransactionForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = inventoryForm_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = inventoryForm_user_idnumber.Text;
            this.Close();
            toCustomer.Show();
        }
    }
}


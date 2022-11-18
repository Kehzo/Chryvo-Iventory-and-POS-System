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
    public partial class supplier_Form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(1111111, 9999999);
        public supplier_Form()
        {
            InitializeComponent();
            supplierForm_supplierlastvisit_DTpicker.MinDate = DateTime.Now.AddYears(-25);
            supplierForm_supplierlastvisit_DTpicker.MaxDate = DateTime.Now;
            fillSupplierlist();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        private void supplier_Form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
        }
        private void ResetForm()
        {
            supplierForm_new_supplierID_lbl.Text = "";
            Random rand = new Random();
            NumberRandom = rand.Next(1111111, 9999999);
        }
        public void fillSupplierlist()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                supplierForm_supplierlist_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from supplier_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["supplier_ID"].ToString());
                    item.SubItems.Add(read["supplier_firstname"].ToString() + " " + read["supplier_middlename"].ToString() + " " + read["supplier_lastname"].ToString());
                    item.SubItems.Add(read["supplier_gender"].ToString());
                    item.SubItems.Add(read["supplier_company"].ToString());
                    item.SubItems.Add(read["supplier_contactno"].ToString());
                    item.SubItems.Add(read["supplier_dateadded"].ToString());
                    item.SubItems.Add(read["supplier_lastvisit"].ToString());
                    item.SubItems.Add(read["supplier_balance"].ToString());
                    item.SubItems.Add(read["supplier_insertedby"].ToString());
                    item.SubItems.Add(read["supplier_status"].ToString());
                    item.SubItems.Add(read["supplier_firstname"].ToString());
                    item.SubItems.Add(read["supplier_middlename"].ToString());
                    item.SubItems.Add(read["supplier_lastname"].ToString());

                    supplierForm_supplierlist_lstView.Items.Add(item);
                    supplierForm_supplierlist_lstView.FullRowSelect = true;

                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void fillsupplierProducts()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                supplierForm_supplierProducts_txtBox.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory_table where stock_supplier = '" + supplierForm_updatecompanyName_txtBox.Text + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["stock_product_name"].ToString() + " " + read["stock_additional_details"].ToString());

                    supplierForm_supplierProducts_txtBox.Items.Add(item);
                    supplierForm_supplierProducts_txtBox.FullRowSelect = true;


                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }

        private void stopdebug_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void customerForm_addnewuser_btn_Click(object sender, EventArgs e)
        {
            switch (supplierForm_addnewuser_btn.Text)
            {
                case "Add new supplier":
                    supplierForm_new_supplierID_lbl.Text = NumberRandom.ToString();
                    supplierForm_supplierdetails_grpBox.Visible = false;
                    supplierForm_new_supplier_grpBox.Visible = true;
                    supplierForm_addnewuser_btn.Text = "Cancel";
                    customerForm_inventory_btn.Enabled = false; customerForm_supplier_btn.Enabled = false; customerForm_sales_btn.Enabled = false; customerForm_POS_btn.Enabled = false;
                    supplierForm_supplierlist_lstView.Enabled = false; supplierForm_refreshlist_btn.Enabled = false;
                    break;
                case "Remove user":
                    //archiveEmployee();
                    //employeeForm_edit_btn.Text = "Edit record";
                    //employeeForm_edit_btn.Enabled = false;
                    //employeeForm_employeefullname_txtBox.Enabled = false; employeeForm_employeefullname_txtBox.Clear();
                    //employeeForm_employeeaddress_txtBox.Enabled = false; employeeForm_employeeaddress_txtBox.Clear();
                    //employeeForm_employeeDOB_DTpicker.Enabled = false;
                    //employeForm_employeedatehired_DTpicker.Enabled = false;
                    //employeeForm_employeebalance_txtBox.Enabled = false; employeeForm_employeebalance_txtBox.Clear();
                    //employeeForm_employeestatusActive_chkBox.Enabled = false; employeeForm_employeestatusActive_chkBox.Checked = false;
                    //employeeForm_employeeaccstatusInactive_chkBox.Enabled = false; employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
                    //employeeForm_update_contactno_txtBox.Enabled = false; employeeForm_update_contactno_txtBox.Clear();
                    //employeeForm_update_password_txtBox.Enabled = false; employeeForm_update_password_txtBox.Clear();
                    //employeeForm_employeelastin_txtBox.Enabled = false; employeeForm_employeelastin_txtBox.Clear();
                    //employeeForm_employeelastout_txtBox.Enabled = false; employeeForm_employeelastout_txtBox.Clear();
                    //employeeForm_edit_signiture_grpBox.Enabled = false; employeeForm_update_signiture_pctrBox.Image = null; employeeForm_updateemployeeSigniture_path_txtBox.Clear();
                    //employeeForm_edit_employeePhoto_grpBox.Enabled = false; employeeForm_update_employeePhoto_pctrBox.Image = null; employeeForm_updateemployeePhoto_path_txtBox.Clear();
                    //employeeForm_updatefname_lbl.Visible = false; employeeForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
                    //employeeForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; employeeForm_update_lname_txtBox.Visible = false;
                    //employeeForm_update_fullname_lbl.Visible = true; employeeForm_employeefullname_txtBox.Visible = true; employeeForm_employeelist_lstView.Enabled = true;
                    break;
                case "Cancel":
                    supplierForm_supplierdetails_grpBox.Visible = true;
                    supplierForm_new_supplier_grpBox.Visible = false;
                    supplierForm_addnewuser_btn.Text = "Add new supplier";
                    supplierForm_new_firstname_txtBox.Clear(); supplierForm_new_middlename_txtBox.Clear(); supplierForm_new_lastname_txtBox.Clear();
                    supplierForm_new_companyName_txtBox.Clear(); supplierForm_new_contactno_txtBox.Clear();
                    supplierForm_new_gendermale_rdBtn.Checked = true;
                    customerForm_inventory_btn.Enabled = true; customerForm_supplier_btn.Enabled = true; customerForm_sales_btn.Enabled = true; customerForm_POS_btn.Enabled = true;
                    supplierForm_supplierlist_lstView.Enabled = true; supplierForm_refreshlist_btn.Enabled = true; ResetForm();
                    break;
            }
        }

        private void customerForm_customerlist_lstView_DoubleClick(object sender, EventArgs e)
        {
            int i = 0;
            ListViewItem supplierLists = supplierForm_supplierlist_lstView.SelectedItems[i];
            supplierForm_customerID_lbl.Text = supplierLists.SubItems[0].Text;
            supplierForm_supplierfullname_txtBox.Text = supplierLists.SubItems[1].Text;
            if (supplierLists.SubItems[2].Text == "Male")
            {
                supplierForm_update_supplierMaleGender_rdBtn.Checked = true;
            }
            else if (supplierLists.SubItems[2].Text == "Female")
            {
                supplierForm_update_supplierFemaleGender_rdBtn.Checked = true;
            }
            supplierForm_updatecompanyName_txtBox.Text = supplierLists.SubItems[3].Text;
            supplierForm_update_contactno_txtBox.Text = supplierLists.SubItems[4].Text;
            if(supplierLists.SubItems[6].Text == "N/A")
            {
                supplierForm_dateLastvisit_lbl.Visible = true; supplierForm_supplierlastvisit_DTpicker.Visible = false;
            }
            else 
            {
                supplierForm_dateLastvisit_lbl.Visible = false; supplierForm_supplierlastvisit_DTpicker.Visible = true; supplierForm_supplierlastvisit_DTpicker.Text = supplierLists.SubItems[6].Text;
            }
            supplierForm_dateAdded_lbl.Text = supplierLists.SubItems[5].Text;
            supplierForm_supplierbalance_txtBox.Text = supplierLists.SubItems[7].Text;
            supplierForm_insertedby_lbl.Text = supplierLists.SubItems[8].Text;
            if (supplierLists.SubItems[9].Text == "ACTIVE")
            {
                fillsupplierProducts();
                supplierForm_customerstatusActive_chkBox.Visible = true;
                supplierForm_customerstatusActive_chkBox.Checked = true;
                supplierForm_customeraccstatusInactive_chkBox.Visible = true;
                supplierForm_customeraccstatusInactive_chkBox.Checked = false;
                supplierForm_customerstatusarchived_lbl.Visible = false;
                supplierForm_edit_btn.Text = "Edit record"; supplierForm_addnewuser_btn.Visible = false;
                //supplierForm_customerstatusarchived_lbl.ForeColor = Color.Green; supplierForm_customerstatusarchived_lbl.Visible = true; supplierForm_customerstatusarchived_lbl.Text = "Active";
            }
            else if (supplierLists.SubItems[9].Text == "INACTIVE")
            {
                supplierForm_customerstatusarchived_lbl.Visible = false;
                supplierForm_customerstatusActive_chkBox.Visible = true;
                supplierForm_customerstatusActive_chkBox.Checked = false;
                supplierForm_customeraccstatusInactive_chkBox.Visible = true;
                supplierForm_customeraccstatusInactive_chkBox.Checked = true;
                supplierForm_edit_btn.Enabled = false;
                supplierForm_edit_btn.Text = "User records unavailable to edit";
                // supplierForm_customerstatusarchived_lbl.ForeColor = Color.Red; supplierForm_customerstatusarchived_lbl.Visible = true; supplierForm_customerstatusarchived_lbl.Text = "Inactive";
            }
            else if (supplierLists.SubItems[9].Text == "ARCHIVED")
            {
                supplierForm_customerstatusarchived_lbl.Text = "User details archived only";
                supplierForm_customerstatusarchived_lbl.ForeColor = Color.Red;
                supplierForm_customerstatusActive_chkBox.Visible = false;
                supplierForm_customerstatusActive_chkBox.Checked = false;
                supplierForm_customeraccstatusInactive_chkBox.Visible = false;
                supplierForm_customeraccstatusInactive_chkBox.Checked = false;
                // supplierForm_customerstatusarchived_lbl.ForeColor = Color.Red; supplierForm_customerstatusarchived_lbl.Visible = true; supplierForm_customerstatusarchived_lbl.Text = "Archived"; ;
            }

            //MySqlConnection conn = new MySqlConnection(myConnection);

            //MySqlCommand copro3 = new MySqlCommand();
            //conn.Open();
            //copro3.Connection = conn;
            //copro3.CommandText = "select * from supplier_table where supplier_ID   = '" + supplierLists.SubItems[0].Text + "'";
            //MySqlDataReader copro = copro3.ExecuteReader();
            //while (copro.Read())
            //{

            //}
            //conn.Close();

            //supplierForm_edit_btn.Visible = true;
            //supplierForm_edit_btn.Enabled = true;
            //if (supplierLists.SubItems[9].Text == "INACTIVE")
            //{
            //    supplierForm_customerstatusarchived_lbl.Visible = false;
            //    supplierForm_customerstatusActive_chkBox.Visible = true;
            //    supplierForm_customerstatusActive_chkBox.Checked = false;
            //    supplierForm_customeraccstatusInactive_chkBox.Visible = true;
            //    supplierForm_customeraccstatusInactive_chkBox.Checked = true;
            //    supplierForm_edit_btn.Enabled = false;
            //    supplierForm_edit_btn.Text = "User records unavailable to edit";
            //}
            //else if (supplierLists.SubItems[9].Text == "ARCHIVED")
            //{
            //    supplierForm_customerstatusarchived_lbl.Text = "User details archived only";
            //    supplierForm_customerstatusarchived_lbl.ForeColor = Color.Red;
            //    supplierForm_customerstatusActive_chkBox.Visible = false;
            //    supplierForm_customerstatusActive_chkBox.Checked = false;
            //    supplierForm_customeraccstatusInactive_chkBox.Visible = false;
            //    supplierForm_customeraccstatusInactive_chkBox.Checked = false;
            //    //employeeForm_employeestatusActive_chkBox.Checked = false;
            //    //employeeForm_employeeaccstatusInactive_chkBox.Checked = true;
            //    //employeeForm_employeedateofexit_lbl.ForeColor = Color.Red;
            //}
            //else
            //{
            //    fillsupplierProducts();
            //    supplierForm_customerstatusActive_chkBox.Visible = true;
            //    supplierForm_customerstatusActive_chkBox.Checked = true;
            //    supplierForm_customeraccstatusInactive_chkBox.Visible = true;
            //    supplierForm_customeraccstatusInactive_chkBox.Checked = false;
            //    supplierForm_customerstatusarchived_lbl.Visible = false;
            //    supplierForm_edit_btn.Text = "Edit record"; supplierForm_addnewuser_btn.Visible = false;
            //}
        }
        public static Boolean Validates(String Input)
        {
            if (Input.Length == 0)
            {
                return true;
            }
            return false;
        }
        public void add_new_supplier()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(myConnection);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                string query0 = "select * from supplier_table where supplier_ID   = '" + supplierForm_new_supplierID_lbl.Text + "'";
                command.CommandText = query0;
                MySqlDataReader read = command.ExecuteReader();

                int count = 0;
                while (read.Read())
                {
                    count++;
                }

                if (count >= 1)
                {
                    supplierForm_new_supplierID_lbl.Text = "";
                    Random rand = new Random();
                    NumberRandom = rand.Next(111111, 999999);
                    supplierForm_new_supplierID_lbl.Text = NumberRandom.ToString();
                    MessageBox.Show("Duplicate supplier found with the same ID, Creating nother new ID number. Please click add again. Thank you.", "Duplicate ID found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (count == 0)
                {
                    String missing_Firstname = Validates(supplierForm_new_firstname_txtBox.Text) ? "First name" : "";
                    String missing_Middlename = Validates(supplierForm_new_middlename_txtBox.Text) ? "Middle name" : "";
                    String missing_Lastname = Validates(supplierForm_new_lastname_txtBox.Text) ? "Last name" : "";
                    String missing_Company = Validates(supplierForm_new_companyName_txtBox.Text) ? "Company" : "";
                    String missing_ContactNo = Validates(supplierForm_new_contactno_txtBox.Text) ? "Contact no." : "";

                    if (supplierForm_new_firstname_txtBox.Text == "" || supplierForm_new_middlename_txtBox.Text == "" || supplierForm_new_lastname_txtBox.Text == "" || supplierForm_new_companyName_txtBox.Text == "" || supplierForm_new_contactno_txtBox.Text == "")
                    {
                        MessageBox.Show("Please fill up the following blank spaces:" + "\n" + missing_Firstname +
                            "\n" + missing_Middlename + "\n" + missing_Lastname + "\n" + missing_Company + "\n" + missing_ContactNo, "Following fields are empty!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        var gendervalue = "Male";
                        if (supplierForm_new_genderfemale_rdBtn.Checked == true)
                        {
                            gendervalue = "Female";
                        }

                        MySqlConnection con = new MySqlConnection(myConnection);
                        con.Open();
                        MySqlCommand save = con.CreateCommand();
                        save.Connection = con;
                        save.CommandText = ("insert into supplier_table (supplier_ID,supplier_firstname,supplier_middlename" +
                            ",supplier_lastname,supplier_gender," +
                           "supplier_company,supplier_contactno,supplier_dateadded,supplier_lastvisit,supplier_balance,supplier_insertedby,supplier_status) values(@supplier_ID ,@supplier_firstname, @supplier_middlename,@supplier_lastname,@supplier_gender,@supplier_company,@supplier_contactno,@supplier_dateadded,@supplier_lastvisit,@supplier_balance,@supplier_insertedby,@supplier_status)");
                        save.Parameters.AddWithValue("@supplier_ID", supplierForm_new_supplierID_lbl.Text);
                        save.Parameters.AddWithValue("@supplier_firstname", supplierForm_new_firstname_txtBox.Text);
                        save.Parameters.AddWithValue("@supplier_middlename", supplierForm_new_middlename_txtBox.Text);
                        save.Parameters.AddWithValue("@supplier_lastname", supplierForm_new_lastname_txtBox.Text);
                        save.Parameters.AddWithValue("@supplier_gender", gendervalue);
                        save.Parameters.AddWithValue("@supplier_company", supplierForm_new_companyName_txtBox.Text);
                        save.Parameters.AddWithValue("@supplier_contactno", supplierForm_new_contactno_txtBox.Text);
                        save.Parameters.AddWithValue("@supplier_dateadded", DateTime.Now.ToString());
                        save.Parameters.AddWithValue("@supplier_lastvisit", "N/A");
                        save.Parameters.AddWithValue("@supplier_balance", 0);
                        save.Parameters.AddWithValue("@supplier_insertedby", Supplier_user_Firstname.Text);
                        save.Parameters.AddWithValue("@supplier_status", "ACTIVE");
                        save.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("New Supplier added!" + "Supplier ID is " + supplierForm_new_supplierID_lbl.Text);
                        fillSupplierlist();
                        supplierForm_new_firstname_txtBox.Clear(); supplierForm_new_middlename_txtBox.Clear(); supplierForm_new_lastname_txtBox.Clear();
                        supplierForm_new_companyName_txtBox.Clear(); supplierForm_new_contactno_txtBox.Clear(); ; supplierForm_new_gendermale_rdBtn.Checked = true;
                        supplierForm_new_companyName_txtBox.Clear(); supplierForm_supplierdetails_grpBox.Visible = true;
                        supplierForm_new_supplier_grpBox.Visible = false; supplierForm_addnewuser_btn.Text = "Add new supplier "; supplierForm_supplierlist_lstView.Enabled = true;
                        supplierForm_refreshlist_btn.Enabled = true; customerForm_POS_btn.Enabled = true; customerForm_inventory_btn.Enabled = true; customerForm_sales_btn.Enabled = true; customerForm_supplier_btn.Enabled = true;
                    }
                }

                conn.Close();
            }
            catch (Exception tangina)
            {
                MessageBox.Show("Error:" + tangina);
            }
        }
        public void updateSupplier()
        {

            DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from supplier_table where supplier_ID   = '" + supplierForm_customerID_lbl.Text + "'";
                command.CommandText = query0;
                MySqlDataReader read = command.ExecuteReader();

                int count = 0;
                while (read.Read())
                {
                    count++;
                }

                if (count == 1)
                {
                    var accountStatusValue = "ACTIVE";
                    var genderStatusValue = "Male";
                    if (supplierForm_customeraccstatusInactive_chkBox.Checked == true)
                    {
                        accountStatusValue = "INACTIVE";
                    }
                    if (supplierForm_update_supplierFemaleGender_rdBtn.Checked == true)
                    {
                        genderStatusValue = "Female";
                    }
                    connection.Close();
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    string query1 = "update supplier_table set  supplier_firstname = '" + supplierForm_update_fname_txtBox.Text + "' , supplier_middlename = '" + supplierForm_update_mname_txtBox.Text + "', supplier_lastname = '" + supplierForm_update_lname_txtBox.Text + "' , supplier_gender = '" + genderStatusValue + "' , supplier_company = '" + supplierForm_updatecompanyName_txtBox.Text + "', supplier_contactno = '" + supplierForm_update_contactno_txtBox.Text + "', supplier_lastvisit = '" + supplierForm_supplierlastvisit_DTpicker.Text + "', supplier_balance = '" + supplierForm_supplierbalance_txtBox + "', supplier_insertedby = '" + Supplier_user_Firstname.Text + "',  supplier_status  = '" + accountStatusValue + "' where supplier_ID   = '" + supplierForm_customerID_lbl.Text + "' ";
                    command2.CommandText = query1;
                    command2.ExecuteNonQuery();
                    fillSupplierlist();
                    MessageBox.Show("Update successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (count > 1)
                {
                    MessageBox.Show("Please Make Sure That You Don't Have A Duplicate Reservation", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    return;
                }

            }
        }

        private void customerForm_edit_btn_Click(object sender, EventArgs e)
        {
            switch (supplierForm_edit_btn.Text)
            {
                case "Edit record":
                    customerForm_inventory_btn.Enabled = false; customerForm_supplier_btn.Enabled = false; customerForm_sales_btn.Enabled = false; customerForm_POS_btn.Enabled = false; supplierForm_refreshlist_btn.Enabled = false;
                    supplierForm_update_supplierMaleGender_rdBtn.Enabled = true;
                    supplierForm_update_supplierFemaleGender_rdBtn.Enabled = true;
                    supplierForm_addnewuser_btn.Text = "Remove user";
                    int i = 0;
                    ListViewItem customerLists = supplierForm_supplierlist_lstView.SelectedItems[i];
                    supplierForm_update_fname_txtBox.Text = customerLists.SubItems[10].Text;
                    supplierForm_update_mname_txtBox.Text = customerLists.SubItems[11].Text;
                    supplierForm_update_lname_txtBox.Text = customerLists.SubItems[12].Text;

                    supplierForm_supplierlist_lstView.Enabled = false;
                    supplierForm_supplierlist_lstView.SelectedItems.Clear();
                    supplierForm_edit_btn.Text = "Apply Changes";
                    supplierForm_update_cancel_btn.Visible = true;
                    supplierForm_update_fname_txtBox.Enabled = true; supplierForm_updatefname_lbl.Visible = true;
                    supplierForm_updatecompanyName_txtBox.Enabled = true; supplierForm_update_mname_txtBox.Visible = true;
                    supplierForm_supplierlastvisit_DTpicker.Enabled = true;
                    supplierForm_supplierbalance_txtBox.Enabled = true; supplierForm_updatelname_lbl.Visible = true;
                    supplierForm_customerstatusActive_chkBox.Enabled = true;
                    supplierForm_customeraccstatusInactive_chkBox.Enabled = true; supplierForm_update_fullname_lbl.Visible = false;
                    supplierForm_update_contactno_txtBox.Enabled = true; supplierForm_supplierfullname_txtBox.Visible = false;

                    if (supplierForm_customerstatusarchived_lbl.Text == "ARCHIVED")
                    {
                        supplierForm_addnewuser_btn.Enabled = false;
                        supplierForm_customerstatusActive_chkBox.Enabled = false;
                        supplierForm_customeraccstatusInactive_chkBox.Enabled = false;
                        supplierForm_customerstatusarchived_lbl.Visible = true;
                    }
                    break;
                case "Apply Changes":
                    updateSupplier();
                    supplierForm_edit_btn.Text = "Edit record";
                    supplierForm_edit_btn.Enabled = false;
                    supplierForm_supplierfullname_txtBox.Enabled = false; supplierForm_supplierfullname_txtBox.Clear();
                    supplierForm_updatecompanyName_txtBox.Enabled = false; supplierForm_updatecompanyName_txtBox.Clear();
                    supplierForm_supplierlastvisit_DTpicker.Enabled = false;
                    supplierForm_supplierbalance_txtBox.Enabled = false; supplierForm_supplierbalance_txtBox.Clear();
                    supplierForm_customerstatusActive_chkBox.Enabled = false; supplierForm_customerstatusActive_chkBox.Checked = false;
                    supplierForm_customeraccstatusInactive_chkBox.Enabled = false; supplierForm_customeraccstatusInactive_chkBox.Checked = false;
                    supplierForm_update_contactno_txtBox.Enabled = false; supplierForm_update_contactno_txtBox.Clear();
                    supplierForm_updatefname_lbl.Visible = false; supplierForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
                    supplierForm_update_mname_txtBox.Visible = false; supplierForm_updatelname_lbl.Visible = false; supplierForm_update_lname_txtBox.Visible = false;
                    supplierForm_update_fullname_lbl.Visible = true; supplierForm_supplierfullname_txtBox.Visible = true; supplierForm_supplierlist_lstView.Enabled = true;
                    supplierForm_update_supplierMaleGender_rdBtn.Enabled = false; supplierForm_insertedby_lbl.Text = "";
                    supplierForm_update_supplierFemaleGender_rdBtn.Enabled = false;
                    break;

            }
        }

        private void customerForm_update_cancel_btn_Click(object sender, EventArgs e)
        {
            customerForm_inventory_btn.Enabled = true; customerForm_supplier_btn.Enabled = true; customerForm_sales_btn.Enabled = true; customerForm_POS_btn.Enabled = true; supplierForm_refreshlist_btn.Enabled = true;
            supplierForm_update_cancel_btn.Visible = false;
            supplierForm_addnewuser_btn.Text = "Add new supplier";
            supplierForm_customerID_lbl.Text = "-";
            supplierForm_supplierlist_lstView.Enabled = true;
            supplierForm_edit_btn.Text = "Edit record";
            supplierForm_edit_btn.Enabled = false;
            supplierForm_supplierfullname_txtBox.Enabled = false; supplierForm_supplierfullname_txtBox.Clear();
            supplierForm_updatecompanyName_txtBox.Enabled = false; supplierForm_updatecompanyName_txtBox.Clear();
            supplierForm_supplierlastvisit_DTpicker.Enabled = false;
            supplierForm_supplierbalance_txtBox.Enabled = false; supplierForm_supplierbalance_txtBox.Clear();
            supplierForm_customerstatusActive_chkBox.Enabled = false; supplierForm_customerstatusActive_chkBox.Checked = false;
            supplierForm_customeraccstatusInactive_chkBox.Enabled = false; supplierForm_customeraccstatusInactive_chkBox.Checked = false;
            supplierForm_update_contactno_txtBox.Enabled = false; supplierForm_update_contactno_txtBox.Clear();
            supplierForm_updatefname_lbl.Visible = false; supplierForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
            supplierForm_update_mname_txtBox.Visible = false; supplierForm_updatelname_lbl.Visible = false; supplierForm_update_lname_txtBox.Visible = false;
            supplierForm_update_fullname_lbl.Visible = true; supplierForm_supplierfullname_txtBox.Visible = true; supplierForm_supplierlist_lstView.Enabled = true;
            supplierForm_update_supplierMaleGender_rdBtn.Enabled = false; supplierForm_insertedby_lbl.Text = "";
            supplierForm_update_supplierFemaleGender_rdBtn.Enabled = false;
        }

        private void supplierForm_addnewusertoDB_btn_Click(object sender, EventArgs e)
        {
            add_new_supplier();
        }

        private void customerForm_POS_btn_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = Supplier_user_Firstname.Text;
            toPOSForm.POS_user_idnumber.Text = Supplier_user_idnumber.Text;
            toPOSForm.account_level.Text = account_level.Text;
            this.Close();
            toPOSForm.Show();
        }

        private void customerForm_inventory_btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_Firstname.Text = Supplier_user_Firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = Supplier_user_idnumber.Text;
            toInventory.account_level.Text = account_level.Text;
            this.Close();
            toInventory.Show();
        }

        private void customerForm_supplier_btn_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = Supplier_user_Firstname.Text;
            toCustomer.Customer_user_idnumber.Text = Supplier_user_idnumber.Text;
            toCustomer.account_level.Text = account_level.Text;
            this.Close();
            toCustomer.Show();
        }

        private void customerForm_sales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = Supplier_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = Supplier_user_idnumber.Text;
            toSalesForm.account_level.Text = account_level.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void customerForm_users_btn_Click(object sender, EventArgs e)
        {
            employees_Form toUserForm = new employees_Form();
            toUserForm.employeeForm_user_firstname.Text = Supplier_user_Firstname.Text;
            toUserForm.employeeForm_user_idnumber.Text = Supplier_user_idnumber.Text;
            toUserForm.account_level.Text = account_level.Text;
            this.Close();
            toUserForm.Show();
        }

        private void customerForm_transactions_btn_Click(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = Supplier_user_Firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = Supplier_user_idnumber.Text;
            toTransactionForm.account_level.Text = account_level.Text;
            this.Close();
            toTransactionForm.Show();
        }
    }
}

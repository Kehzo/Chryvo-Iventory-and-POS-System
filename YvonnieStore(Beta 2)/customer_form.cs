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
    public partial class customer_form : Form
    {
        string myConnection = "Server=localhost;Database= chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(1111111, 9999999);
        public static String CustomerNewDefaultPic = "F:\\Programing Projects\\Chryvo Store POS and Inventory II\\Photos\\defaultEmployeeImage.Jpg";
        public static String CustomerNewDefaultSigniture = "F:\\Programing Projects\\Chryvo Store POS and Inventory II\\Photos\\defaultSigniturePhoto.Jpg";
        public static Boolean imagechecker = false;
        public customer_form()
        {
            InitializeComponent();
            customerForm_new_employeeDOB_DTpicker.MinDate = DateTime.Now.AddYears(-70);
            customerForm_new_employeeDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            customerForm_customerDOB_DTpicker.MinDate = DateTime.Now.AddYears(-60);
            customerForm_customerDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            customerForm_customerdatehired_DTpicker.MinDate = DateTime.Now.AddYears(-25);
            customerForm_customerdatehired_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            customerForm_new_employeeDOB_DTpicker.Value = DateTime.Now.AddYears(-16);
            fillCustomerlist();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        private void customer_form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
        }
        private void ResetForm()
        {
            customerForm_new_customerID_lbl.Text = "";
            Random rand = new Random();
            NumberRandom = rand.Next(1111111, 9999999);
        }

        public void fillCustomerlist()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                customerForm_customerlist_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from customer_table ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["customer_ID"].ToString());
                    item.SubItems.Add(read["customer_firstname"].ToString() + " " + read["customer_middlename"].ToString() + " " + read["customer_lastname"].ToString());
                    item.SubItems.Add(read["customer_birthdate"].ToString());
                    item.SubItems.Add(read["customer_gender"].ToString());
                    item.SubItems.Add(read["customer_address"].ToString());
                    item.SubItems.Add(read["customer_contactno"].ToString());
                    item.SubItems.Add(read["customer_registered"].ToString());
                    item.SubItems.Add(read["customer_lastpurchased"].ToString());
                    item.SubItems.Add(read["customer_balance"].ToString());
                    item.SubItems.Add(read["customer_insertedby"].ToString());
                    item.SubItems.Add(read["customer_firstname"].ToString());
                    item.SubItems.Add(read["customer_middlename"].ToString());
                    item.SubItems.Add(read["customer_lastname"].ToString());
                    item.SubItems.Add(read["customer_status"].ToString());

                    customerForm_customerlist_lstView.Items.Add(item);
                    customerForm_customerlist_lstView.FullRowSelect = true;


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
            switch (customerForm_addnewuser_btn.Text)
            {
                case "Add new customer":
                    customerForm_new_customerID_lbl.Text = NumberRandom.ToString();
                    customerForm_customerdetails_grpBox.Visible = false;
                    employeeForm_new_employee_grpBox.Visible = true;
                    customerForm_addnewuser_btn.Text = "Cancel";
                    customerForm_inventory_btn.Enabled = false; customerForm_supplier_btn.Enabled = false; customerForm_sales_btn.Enabled = false; customerForm_POS_btn.Enabled = false;
                    customerForm_customerlist_lstView.Enabled = false; customerForm_refreshlist_btn.Enabled = false;
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
                    customerForm_customerdetails_grpBox.Visible = true;
                    employeeForm_new_employee_grpBox.Visible = false;
                    customerForm_addnewuser_btn.Text = "Add new user";
                    customerForm_new_firstname_txtBox.Clear(); customerForm_new_middlename_txtBox.Clear(); customerForm_new_lastname_txtBox.Clear();
                    customerForm_new_address_txtBox.Clear(); customerForm_new_contactno_txtBox.Clear();
                    customerForm_new_gendermale_rdBtn.Checked = true; customerForm_new_signiture_pctrBox.Image = null;
                    customerForm_new_employeePhoto_pctrBox.Image = null; customerForm_customerPhoto_path_txtBox.Clear(); customerForm_customerSigniture_path_txtBox.Clear();
                    customerForm_inventory_btn.Enabled = true; customerForm_supplier_btn.Enabled = true; customerForm_sales_btn.Enabled = true; customerForm_POS_btn.Enabled = true;
                    customerForm_customerlist_lstView.Enabled = true; customerForm_refreshlist_btn.Enabled = true; ResetForm(); customerForm_addnewuser_btn.Text = "";
                    break;
            }
        }

        private void customerForm_customerlist_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            ListViewItem customerLists = customerForm_customerlist_lstView.SelectedItems[i];
            customerForm_customerID_lbl.Text = customerLists.SubItems[0].Text;
            customerForm_customerfullname_txtBox.Text = customerLists.SubItems[1].Text;
            customerForm_customerDOB_DTpicker.Text = customerLists.SubItems[2].Text;
            if (customerLists.SubItems[3].Text == "Male")
            {
                customerForm_update_employeeMaleGender_rdBtn.Checked = true;
            }
            else if (customerLists.SubItems[3].Text == "Female")
            {
                customerForm_update_employeeMaleGender_rdBtn.Checked = true;
            }
            customerForm_new_address_txtBox.Text = customerLists.SubItems[4].Text;
            customerForm_update_contactno_txtBox.Text = customerLists.SubItems[5].Text;
            customerForm_customerdatehired_DTpicker.Text = customerLists.SubItems[6].Text;
            customerForm_lastpurchasedate_lbl.Text = customerLists.SubItems[7].Text;
            customerForm_customerbalance_txtBox.Text = customerLists.SubItems[8].Text;
            customerForm_insertedby_lbl.Text = customerLists.SubItems[9].Text;
            if (customerLists.SubItems[13].Text == "ACTIVE")
            {
                customerForm_customerstatusarchived_lbl.ForeColor = Color.Green; customerForm_customerstatusarchived_lbl.Visible = true; customerForm_customerstatusarchived_lbl.Text = "Active";
            }
            else if (customerLists.SubItems[13].Text == "INACTIVE")
            {
                customerForm_customerstatusarchived_lbl.ForeColor = Color.Red; customerForm_customerstatusarchived_lbl.Visible = true; customerForm_customerstatusarchived_lbl.Text = "Inactive";
            }
            else if (customerLists.SubItems[13].Text == "ARCHIVED")
            {
                customerForm_customerstatusarchived_lbl.ForeColor = Color.Red; customerForm_customerstatusarchived_lbl.Visible = true; customerForm_customerstatusarchived_lbl.Text = "Archived"; ;
            }

            MySqlConnection conn = new MySqlConnection(myConnection);

            MySqlCommand copro3 = new MySqlCommand();
            conn.Open();
            copro3.Connection = conn;
            copro3.CommandText = "select * from customer_table where customer_ID  = '" + customerLists.SubItems[0].Text + "'";
            MySqlDataReader copro = copro3.ExecuteReader();
            while (copro.Read())
            {

            }
            conn.Close();

            MySqlDataAdapter Adapter = new MySqlDataAdapter(copro3);
            DataSet SetDatas = new DataSet();
            Adapter.Fill(SetDatas);

            if (SetDatas.Tables[0].Rows.Count > 0)
            {
                MemoryStream getCustomerPhoto = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["customer_photo"]);
                customerForm_update_customerPhoto_pctrBox.Image = Image.FromStream(getCustomerPhoto);

                MemoryStream getCustomerSigniture = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["customer_signiturephoto"]);
                customerForm_update_signiture_pctrBox.Image = Image.FromStream(getCustomerSigniture);


            }
            customerForm_edit_btn.Visible = true;
            customerForm_edit_btn.Enabled = true;
            if (customerLists.SubItems[13].Text == "INACTIVE")
            {
                customerForm_customerstatusarchived_lbl.Visible = false;
                customerForm_customerstatusActive_chkBox.Visible = true;
                customerForm_customerstatusActive_chkBox.Checked = false;
                customerForm_customeraccstatusInactive_chkBox.Visible = true;
                customerForm_customeraccstatusInactive_chkBox.Checked = true;
                customerForm_edit_btn.Enabled = false;
                customerForm_edit_btn.Text = "User records unavailable to edit";
            }
            else if (customerLists.SubItems[13].Text == "ARCHIVED")
            {
                customerForm_customerstatusarchived_lbl.Text = "User details archived only";
                customerForm_customerstatusarchived_lbl.ForeColor = Color.Red;
                customerForm_customerstatusActive_chkBox.Visible = false;
                customerForm_customerstatusActive_chkBox.Checked = false;
                customerForm_customeraccstatusInactive_chkBox.Visible = false;
                customerForm_customeraccstatusInactive_chkBox.Checked = false;
                //employeeForm_employeestatusActive_chkBox.Checked = false;
                //employeeForm_employeeaccstatusInactive_chkBox.Checked = true;
                //employeeForm_employeedateofexit_lbl.ForeColor = Color.Red;
            }
            else
            {
                customerForm_customerstatusActive_chkBox.Visible = true;
                customerForm_customerstatusActive_chkBox.Checked = true;
                customerForm_customeraccstatusInactive_chkBox.Visible = true;
                customerForm_customeraccstatusInactive_chkBox.Checked = false;
                customerForm_customerstatusarchived_lbl.Visible = false;
                customerForm_edit_btn.Text = "Edit record"; customerForm_addnewuser_btn.Visible = false;
            }
        }
        public static Boolean Validates(String Input)
        {
            if (Input.Length == 0)
            {
                return true;
            }
            return false;
        }
        public void add_new_customer()
        {
            String missing_Firstname = Validates(customerForm_new_firstname_txtBox.Text) ? "First name" : "";
            String missing_Middlename = Validates(customerForm_new_middlename_txtBox.Text) ? "Middle name" : "";
            String missing_Lastname = Validates(customerForm_new_lastname_txtBox.Text) ? "Last name" : "";
            String missing_Address = Validates(customerForm_new_address_txtBox.Text) ? "Address" : "";
            String missing_ContactNo = Validates(customerForm_new_contactno_txtBox.Text) ? "Contact no." : "";

            if (customerForm_new_firstname_txtBox.Text == "" || customerForm_new_middlename_txtBox.Text == "" || customerForm_new_lastname_txtBox.Text == "" || customerForm_new_address_txtBox.Text == "" || customerForm_new_contactno_txtBox.Text == "")
            {
                MessageBox.Show("Please fill up the following blank spaces:" + "\n" + missing_Firstname +
                    "\n" + missing_Middlename + "\n" + missing_Lastname + "\n" + missing_Address + "\n" + missing_ContactNo, "Following fields are empty!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (customerForm_new_signiture_pctrBox.Image == null)
            {
                MessageBox.Show("Signiture field is empty. Please attach image of new employees' signiture to proceed.");
            }
            else if (customerForm_new_employeePhoto_pctrBox.Image == null)
            {
                MessageBox.Show("Employee photo field is empty. Please attach image of new employees' photo to proceed.");
            }
            else
            {
                var gendervalue = "Male";
                if (customerForm_new_genderfemale_rdBtn.Checked == true)
                {
                    gendervalue = "Female";
                }

                byte[] ImageSaveDefaultPicture = null;
                byte[] ImageSaveDefaultSigniture = null;
                string photoCustomer = customerForm_customerPhoto_path_txtBox.Text;
                string photoSigniturePhoto = customerForm_customerSigniture_path_txtBox.Text;
                byte[] Employeeimage;
                byte[] Signitureimage;

                FileStream readEmployeePhoto = new FileStream(photoCustomer, FileMode.Open, FileAccess.Read);
                FileStream readEmployeeSigniture = new FileStream(photoSigniturePhoto, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(readEmployeePhoto);
                Employeeimage = br.ReadBytes((int)readEmployeePhoto.Length);
                BinaryReader br2 = new BinaryReader(readEmployeeSigniture);
                Signitureimage = br2.ReadBytes((int)readEmployeeSigniture.Length);
                br.Close();
                readEmployeePhoto.Close();
                br2.Close();
                readEmployeeSigniture.Close();
                if (imagechecker == false)
                {
                    FileStream FileSt = new FileStream(CustomerNewDefaultPic, FileMode.Open, FileAccess.Read);
                    BinaryReader ReaderBinary = new BinaryReader(FileSt);
                    ImageSaveDefaultPicture = ReaderBinary.ReadBytes((int)FileSt.Length);
                    FileStream FileSt2 = new FileStream(CustomerNewDefaultSigniture, FileMode.Open, FileAccess.Read);
                    BinaryReader ReaderBinary2 = new BinaryReader(FileSt);
                    ImageSaveDefaultSigniture = ReaderBinary.ReadBytes((int)FileSt.Length);
                    ReaderBinary.Close();
                    FileSt.Close();
                    FileSt2.Close();
                }

                MySqlConnection con = new MySqlConnection(myConnection);
                con.Open();
                MySqlCommand save = con.CreateCommand();
                save.Connection = con;
                save.CommandText = ("insert into customer_table (customer_ID ,customer_firstname,customer_middlename" +
                    ",customer_lastname,customer_birthdate," +
                   "customer_gender,customer_address,customer_contactno,customer_registered,customer_lastpurchased,customer_balance,customer_insertedby" +
                   ",customer_status,customer_photo,customer_signiturephoto,customer_updatedby) values(@customer_ID,@customer_firstname, @customer_middlename,@customer_lastname,@customer_birthdate,@customer_gender,@customer_address,@customer_contactno,@customer_registered,@customer_lastpurchased,@customer_balance,@customer_insertedby,@customer_status,@customer_photo,@customer_signiturephoto,@customer_updatedby)");
                save.Parameters.AddWithValue("@customer_ID", customerForm_new_customerID_lbl.Text);
                save.Parameters.AddWithValue("@customer_firstname", customerForm_new_firstname_txtBox.Text);
                save.Parameters.AddWithValue("@customer_middlename", customerForm_new_middlename_txtBox.Text);
                save.Parameters.AddWithValue("@customer_lastname", customerForm_new_lastname_txtBox.Text);
                save.Parameters.AddWithValue("@customer_birthdate", customerForm_new_employeeDOB_DTpicker.Text);
                save.Parameters.AddWithValue("@customer_gender", gendervalue);
                save.Parameters.AddWithValue("@customer_address", customerForm_new_address_txtBox.Text);
                save.Parameters.AddWithValue("@customer_contactno", customerForm_new_contactno_txtBox.Text);
                save.Parameters.AddWithValue("@customer_registered", DateTime.Now.ToString());
                save.Parameters.AddWithValue("@customer_lastpurchased", "N/A");
                save.Parameters.AddWithValue("@customer_balance", "N/A");
                save.Parameters.AddWithValue("@customer_insertedby", Customer_user_Firstname.Text);
                save.Parameters.AddWithValue("@customer_status", "ACTIVE");
                save.Parameters.Add("@customer_photo", MySqlDbType.Blob);
                save.Parameters["@customer_photo"].Value = Employeeimage;
                save.Parameters.Add("@customer_signiturephoto", MySqlDbType.Blob);
                save.Parameters["@customer_signiturephoto"].Value = Signitureimage;
                save.Parameters.AddWithValue("@customer_updatedby", "N/A");
                save.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New customer added!" + "Customer ID is " + customerForm_new_customerID_lbl.Text);
                fillCustomerlist();
                customerForm_new_firstname_txtBox.Clear(); customerForm_new_middlename_txtBox.Clear(); customerForm_new_lastname_txtBox.Clear();
                customerForm_new_address_txtBox.Clear(); customerForm_new_contactno_txtBox.Clear();; customerForm_new_gendermale_rdBtn.Checked = true;
                customerForm_customerPhoto_path_txtBox.Clear(); customerForm_customerSigniture_path_txtBox.Clear(); customerForm_customerdetails_grpBox.Visible = true;
                employeeForm_new_employee_grpBox.Visible = false; customerForm_addnewuser_btn.Text = "Add new customer "; customerForm_customerlist_lstView.Enabled = true;
                customerForm_refreshlist_btn.Enabled = true; 
            }


        }
        public void updateCustomer()
        {

            DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                if (customerForm_update_customerPhoto_pctrBox.Text != "" && customerForm_update_signiture_pctrBox.Text != "")
                {
                    string CustomerUpdateDefaultPicture = customerForm_updatcustomerPhoto_path_txtBox.Text;
                    string CustomerUpdateDefaultSigniture = customerForm_updatecustomerSigniture_path_txtBox.Text;
                    byte[] CustomerPicture;
                    byte[] CustomerSigniturePhoto;

                    FileStream readCustomerPhoto = new FileStream(CustomerUpdateDefaultPicture, FileMode.Open, FileAccess.Read);
                    FileStream readCustomerSigniture = new FileStream(CustomerUpdateDefaultSigniture, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(readCustomerPhoto);
                    CustomerPicture = br.ReadBytes((int)readCustomerPhoto.Length);
                    BinaryReader br2 = new BinaryReader(readCustomerSigniture);
                    CustomerSigniturePhoto = br2.ReadBytes((int)readCustomerSigniture.Length);
                    br.Close();
                    readCustomerPhoto.Close();
                    br2.Close();
                    readCustomerSigniture.Close();



                    MySqlConnection con = new MySqlConnection(myConnection);
                    con.Open();
                    MySqlCommand save = con.CreateCommand();
                    save.Connection = con;
                    save.CommandText = "update customer_table set customer_photo=@customer_photo,customer_signiturephoto=@customer_signiturephoto where customer_ID  = '" + customerForm_customerID_lbl.Text + "' ";
                    save.Parameters.Add("@customer_photo", MySqlDbType.Blob);
                    save.Parameters["@customer_photo"].Value = CustomerPicture;
                    save.Parameters.Add("@customer_signiturephoto", MySqlDbType.Blob);
                    save.Parameters["@customer_signiturephoto"].Value = CustomerSigniturePhoto;
                    save.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Photo updated!");
                }

                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from customer_table where customer_ID  = '" + customerForm_customerID_lbl.Text + "'";
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
                    if (customerForm_customeraccstatusInactive_chkBox.Checked == true)
                    {
                        accountStatusValue = "INACTIVE";
                    }
                    if (customerForm_update_employeeFemaleGender_rdBtn.Checked == true)
                    {
                        genderStatusValue = "Female";
                    }
                    connection.Close();
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    string query1 = "update customer_table set  customer_firstname = '" + customerForm_update_fname_txtBox.Text + "' , customer_middlename = '" + customerForm_update_mname_txtBox.Text + "', customer_gender = '" + genderStatusValue + "' , customer_lastname = '" + customerForm_update_lname_txtBox.Text + "' , customer_birthdate = '" + customerForm_customerDOB_DTpicker.Text + "', customer_address = '" + customerForm_customeraddress_txtBox.Text + "', customer_contactno = '" + customerForm_update_contactno_txtBox.Text + "', customer_registered = '" + DateTime.Now + "', customer_lastpurchased = '" + "N/A" + "', customer_balance = '" + 0 + "',  customer_updatedby  = '" + Customer_user_Firstname.Text + "',  customer_status  = '" + accountStatusValue + "' where customer_ID  = '" + customerForm_customerID_lbl.Text + "' ";
                    command2.CommandText = query1;
                    command2.ExecuteNonQuery();
                    fillCustomerlist();
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
        private void customerForm_addnewusertoDB_btn_Click(object sender, EventArgs e)
        {
            add_new_customer();
        }

        private void customerForm_new_uploadSigniture_btn_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                customerForm_new_signiture_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                customerForm_customerSigniture_path_txtBox.Text = open.FileName;
            }
        }

        private void customerForm_new_uploadPhoto_lnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                customerForm_new_employeePhoto_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                customerForm_customerPhoto_path_txtBox.Text = open.FileName;
            }
        }

        private void customerForm_update_uploadsigniture_btn_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                customerForm_update_signiture_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                customerForm_updatecustomerSigniture_path_txtBox.Text = open.FileName;
            }
        }

        private void customerForm_update_uploadPhoto_lnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                customerForm_update_customerPhoto_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                customerForm_updatcustomerPhoto_path_txtBox.Text = open.FileName;
            }
        }

        private void customerForm_update_signiture_pctrBox_DoubleClick(object sender, EventArgs e)
        {
            customerForm_enlarge_EmployeePhoto_pctrBox.Image = customerForm_update_signiture_pctrBox.Image;
            panel5.Visible = false; panel3.Visible = false; customerForm_customerdetails_grpBox.Visible = false; panel2.Visible = false;
            customerForm_addnewuser_btn.Visible = false; customerForm_enlarge_EmployeePhoto_pctrBox.Visible = true; customerForm_CancelCustomerPicturePreview_btn.Visible = true;
        }

        private void customerForm_update_customerPhoto_pctrBox_DoubleClick(object sender, EventArgs e)
        {
            customerForm_enlarge_EmployeePhoto_pctrBox.Image = customerForm_update_customerPhoto_pctrBox.Image;
            panel5.Visible = false; panel3.Visible = false; customerForm_customerdetails_grpBox.Visible = false; panel2.Visible = false;
            customerForm_addnewuser_btn.Visible = false; customerForm_enlarge_EmployeePhoto_pctrBox.Visible = true; customerForm_CancelCustomerPicturePreview_btn.Visible = true;
        }

        private void customerForm_CancelCustomerPicturePreview_btn_Click(object sender, EventArgs e)
        {
            customerForm_CancelCustomerPicturePreview_btn.Visible = false; customerForm_enlarge_EmployeePhoto_pctrBox.Visible = false; customerForm_enlarge_EmployeePhoto_pctrBox.Image = null;
            panel5.Visible = true; panel3.Visible = true; customerForm_customerdetails_grpBox.Visible = true;
            customerForm_addnewuser_btn.Visible = true; panel2.Visible = true;
        }

        private void customerForm_edit_btn_Click(object sender, EventArgs e)
        {
            switch (customerForm_edit_btn.Text)
            {
                case "Edit record":
                    customerForm_inventory_btn.Enabled = false; customerForm_supplier_btn.Enabled = false; customerForm_sales_btn.Enabled = false; customerForm_POS_btn.Enabled = false; customerForm_refreshlist_btn.Enabled = false;
                    customerForm_doubleClickpicture_lbl.Visible = true;
                    customerForm_update_employeeMaleGender_rdBtn.Enabled = true;
                    customerForm_update_employeeFemaleGender_rdBtn.Enabled = true;
                    customerForm_addnewuser_btn.Text = "Remove user";
                    int i = 0;
                    ListViewItem customerLists = customerForm_customerlist_lstView.SelectedItems[i];
                    customerForm_update_fname_txtBox.Text = customerLists.SubItems[10].Text;
                    customerForm_update_mname_txtBox.Text = customerLists.SubItems[11].Text;
                    customerForm_update_lname_txtBox.Text = customerLists.SubItems[12].Text;

                    customerForm_customerlist_lstView.Enabled = false;
                    customerForm_customerlist_lstView.SelectedItems.Clear();
                    customerForm_edit_btn.Text = "Apply Changes";
                    customerForm_update_cancel_btn.Visible = true;
                    customerForm_update_fname_txtBox.Enabled = true; employeeForm_updatefname_lbl.Visible = true;
                    customerForm_customeraddress_txtBox.Enabled = true; customerForm_update_mname_txtBox.Visible = true;
                    customerForm_customerDOB_DTpicker.Enabled = true; employeeForm_updatemname_lbl.Visible = true;
                    customerForm_customerdatehired_DTpicker.Enabled = true;
                    customerForm_customerbalance_txtBox.Enabled = true; employeeForm_updatelname_lbl.Visible = true;
                    customerForm_customerstatusActive_chkBox.Enabled = true;
                    customerForm_customeraccstatusInactive_chkBox.Enabled = true; employeeForm_update_fullname_lbl.Visible = false;
                    customerForm_update_contactno_txtBox.Enabled = true; customerForm_customerfullname_txtBox.Visible = false;
                    customerForm_edit_signiture_grpBox.Enabled = true;
                    customerForm_edit_employeePhoto_grpBox.Enabled = true;

                    if (customerForm_customerstatusarchived_lbl.Text == "ARCHIVED")
                    {
                        customerForm_addnewuser_btn.Enabled = false;
                        customerForm_customerstatusActive_chkBox.Enabled = false;
                        customerForm_customeraccstatusInactive_chkBox.Enabled = false;
                        customerForm_customerstatusarchived_lbl.Visible = true;
                    }
                    break;
                case "Apply Changes":
                    updateCustomer();
                    customerForm_edit_btn.Text = "Edit record";
                    customerForm_edit_btn.Enabled = false;
                    customerForm_customerfullname_txtBox.Enabled = false; customerForm_customerfullname_txtBox.Clear();
                    customerForm_customeraddress_txtBox.Enabled = false; customerForm_customeraddress_txtBox.Clear();
                    customerForm_customerDOB_DTpicker.Enabled = false;
                    customerForm_customerdatehired_DTpicker.Enabled = false;
                    customerForm_customerbalance_txtBox.Enabled = false; customerForm_customerbalance_txtBox.Clear();
                    customerForm_customerstatusActive_chkBox.Enabled = false; customerForm_customerstatusActive_chkBox.Checked = false;
                    customerForm_customeraccstatusInactive_chkBox.Enabled = false; customerForm_customeraccstatusInactive_chkBox.Checked = false;
                    customerForm_update_contactno_txtBox.Enabled = false; customerForm_update_contactno_txtBox.Clear();
                    customerForm_edit_signiture_grpBox.Enabled = false; customerForm_update_signiture_pctrBox.Image = null; customerForm_updatecustomerSigniture_path_txtBox.Clear();
                    customerForm_edit_employeePhoto_grpBox.Enabled = false; customerForm_update_customerPhoto_pctrBox.Image = null; customerForm_updatcustomerPhoto_path_txtBox.Clear();
                    employeeForm_updatefname_lbl.Visible = false; customerForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
                    customerForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; customerForm_update_lname_txtBox.Visible = false;
                    employeeForm_update_fullname_lbl.Visible = true; customerForm_customerfullname_txtBox.Visible = true; customerForm_customerlist_lstView.Enabled = true;
                    customerForm_update_employeeMaleGender_rdBtn.Enabled = false; customerForm_insertedby_lbl.Text = "";
                    customerForm_update_employeeFemaleGender_rdBtn.Enabled = false;
                    break;

            }
        }

        private void customerForm_update_cancel_btn_Click(object sender, EventArgs e)
        {
            customerForm_inventory_btn.Enabled = true; customerForm_supplier_btn.Enabled = true; customerForm_sales_btn.Enabled = true; customerForm_POS_btn.Enabled = true; customerForm_refreshlist_btn.Enabled = true;
            customerForm_doubleClickpicture_lbl.Visible = false;
            customerForm_update_cancel_btn.Visible = false;
            customerForm_addnewuser_btn.Text = "Add new customer";
            customerForm_customerID_lbl.Text = "-";
            customerForm_customerlist_lstView.Enabled = true;
            customerForm_edit_btn.Text = "Edit record";
            customerForm_edit_btn.Enabled = false;
            customerForm_customerfullname_txtBox.Enabled = false; customerForm_customerfullname_txtBox.Clear();
            customerForm_customeraddress_txtBox.Enabled = false; customerForm_customeraddress_txtBox.Clear();
            customerForm_customerDOB_DTpicker.Enabled = false;
            customerForm_customerdatehired_DTpicker.Enabled = false;
            customerForm_customerbalance_txtBox.Enabled = false; customerForm_customerbalance_txtBox.Clear();
            customerForm_customerstatusActive_chkBox.Enabled = false; customerForm_customerstatusActive_chkBox.Checked = false;
            customerForm_customeraccstatusInactive_chkBox.Enabled = false; customerForm_customeraccstatusInactive_chkBox.Checked = false;
            customerForm_update_contactno_txtBox.Enabled = false; customerForm_update_contactno_txtBox.Clear();
            customerForm_edit_signiture_grpBox.Enabled = false; customerForm_update_signiture_pctrBox.Image = null; customerForm_updatecustomerSigniture_path_txtBox.Clear();
            customerForm_edit_employeePhoto_grpBox.Enabled = false; customerForm_update_customerPhoto_pctrBox.Image = null; customerForm_updatcustomerPhoto_path_txtBox.Clear();
            employeeForm_updatefname_lbl.Visible = false; customerForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
            customerForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; customerForm_update_lname_txtBox.Visible = false;
            employeeForm_update_fullname_lbl.Visible = true; customerForm_customerfullname_txtBox.Visible = true; customerForm_customerlist_lstView.Enabled = true;
            customerForm_update_employeeMaleGender_rdBtn.Enabled = false; customerForm_insertedby_lbl.Text = "";
            customerForm_update_employeeFemaleGender_rdBtn.Enabled = false;
        }

        private void customerForm_POS_btn_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = Customer_user_Firstname.Text;
            toPOSForm.POS_user_idnumber.Text = Customer_user_idnumber.Text;
            this.Close();
            toPOSForm.Show();
        }

        private void customerForm_inventory_btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_Firstname.Text = Customer_user_Firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = Customer_user_idnumber.Text;
            this.Close();
            toInventory.Show();
        }

        private void customerForm_sales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = Customer_user_Firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = Customer_user_idnumber.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void POS_toUsers_btn_Click(object sender, EventArgs e)
        {
            employees_Form toEmployeeForm = new employees_Form();
            toEmployeeForm.employeeForm_user_firstname.Text = Customer_user_Firstname.Text;
            toEmployeeForm.employeeForm_user_idnumber.Text = Customer_user_idnumber.Text;
            this.Close();
            toEmployeeForm.Show();
        }

        private void POSForm_toTransactions_btn_Click(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = Customer_user_Firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = Customer_user_idnumber.Text;
            this.Close();
            toTransactionForm.Show();
        }

        private void customerForm_supplier_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

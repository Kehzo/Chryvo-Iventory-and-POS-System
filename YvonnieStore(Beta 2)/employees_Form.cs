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
using System.Runtime.InteropServices.ComTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using TextBox = System.Windows.Forms.TextBox;

namespace YvonnieStore_Beta_2_
{
    public partial class employees_Form : Form
    {
        string myConnection = "Server=localhost;database=chryvo_store_database ;Uid=root;Password=";
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(1111111, 9999999);
       // public static Random NumberRandom = new Random();
        public static String EmployeeNewDefaultPic = "F:\\Programing Projects\\Chryvo Store POS and Inventory II\\Photos\\defaultEmployeeImage.Jpg";
        public static String EmployeeNewDefaultSigniture = "F:\\Programing Projects\\Chryvo Store POS and Inventory II\\Photos\\defaultSigniturePhoto.Jpg";
        public static Boolean imagechecker = false;

        public employees_Form()
        {
            InitializeComponent();
            employeeForm_new_employeeDOB_DTpicker.MinDate = DateTime.Now.AddYears(-70);
            employeeForm_new_employeeDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            employeeForm_employeeDOB_DTpicker.MinDate = DateTime.Now.AddYears(-60);
            employeeForm_employeeDOB_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            employeForm_employeedatehired_DTpicker.MinDate = DateTime.Now.AddYears(-25);
            employeForm_employeedatehired_DTpicker.MaxDate = DateTime.Now.AddDays(1);
            employeeForm_new_employeeDOB_DTpicker.Value = DateTime.Now.AddYears(-16);
            fillEmployeelist();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            employeeForm_timenow_value.Text = DateTime.Now.ToLongTimeString();
        }
        private void employees_Form_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            employeeForm_timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            employeeForm_datenow_value.Text = DateTime.Now.ToShortDateString();
        }
        private void ResetForm()
        {
            employeeForm_new_employeeID_lbl.Text = "";
            Random rand = new Random();
            NumberRandom = rand.Next(1111111, 9999999);
        }

        //Public variables//

        //Public variables//
        public void fillEmployeelist()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                employeeForm_employeelist_lstView.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from useraccounts ";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["id_number"].ToString());
                    item.SubItems.Add(read["firstname"].ToString() + " " + read["middlename"].ToString() + " " + read["lastname"].ToString());
                    item.SubItems.Add(read["address"].ToString());
                    item.SubItems.Add(read["birthdate"].ToString());
                    item.SubItems.Add(read["date_employed"].ToString());
                    item.SubItems.Add(read["inserted_by"].ToString());
                    item.SubItems.Add(read["last_in"].ToString());
                    item.SubItems.Add(read["last_out"].ToString());
                    item.SubItems.Add(read["balance"].ToString());
                    item.SubItems.Add(read["account_status"].ToString());
                    item.SubItems.Add(read["date_exited"].ToString());
                    item.SubItems.Add(read["contact_no"].ToString());
                    item.SubItems.Add(read["password"].ToString());
                    item.SubItems.Add(read["firstname"].ToString());
                    item.SubItems.Add(read["middlename"].ToString());
                    item.SubItems.Add(read["lastname"].ToString());
                    item.SubItems.Add(read["gender"].ToString());

                    employeeForm_employeelist_lstView.Items.Add(item);
                    employeeForm_employeelist_lstView.FullRowSelect = true;


                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
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
        //ADD ITEM TO INVENTORY METHOD START HERE//
        public void add_new_user()
        {
            String missing_Firstname = Validates(employeeForm_new_firstname_txtBox.Text) ? "First name" : "";
            String missing_Middlename = Validates(employeeForm_new_middlename_txtBox.Text) ? "Middle name" : "";
            String missing_Lastname = Validates(employeeForm_new_lastname_txtBox.Text) ? "Last name" : "";
            String missing_Address = Validates(employeeForm_new_address_txtBox.Text) ? "Address" : "";
            String missing_ContactNo = Validates(employeeForm_new_contactno_txtBox.Text) ? "Contact no." : "";
            String missing_Password = Validates(employeeForm_new_password_txtBox.Text) ? "Password" : "";

            if (employeeForm_new_firstname_txtBox.Text == "" || employeeForm_new_middlename_txtBox.Text == "" || employeeForm_new_lastname_txtBox.Text == "" || employeeForm_new_address_txtBox.Text == "" || employeeForm_new_contactno_txtBox.Text == "" || employeeForm_new_password_txtBox.Text == "")
            {
                MessageBox.Show("Please fill up the following blank spaces:" + "\n" + missing_Firstname +
                    "\n" + missing_Middlename + "\n" + missing_Lastname + "\n" + missing_Address + "\n" + missing_ContactNo + "\n" + missing_Password , "Following fields are empty!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (employeeForm_new_signiture_pctrBox.Image == null)
            {
                MessageBox.Show("Signiture field is empty. Please attach image of new employees' signiture to proceed.");
            }
            else if (employeeForm_new_employeePhoto_pctrBox.Image == null)
            {
                MessageBox.Show("Employee photo field is empty. Please attach image of new employees' photo to proceed.");
            }
            else
            {
                int account_level_value = 1;
                int gendervalue = 0;
                if (employeeForm_new_acctypeAdmin_rdBtn.Checked == true)
                {
                    account_level_value = 0;
                }
                if (employeeForm_new_genderfemale_rdBtn.Checked == true)
                {
                    gendervalue = 1;
                }

                byte[] ImageSaveDefaultPicture = null;
                byte[] ImageSaveDefaultSigniture = null;
                string photoEmployeePhoto = employeeForm_employeePhoto_path_txtBox.Text;
                string photoSigniturePhoto = employeeForm_employeeSigniture_path_txtBox.Text;
                byte[] Employeeimage;
                byte[] Signitureimage;

                FileStream readEmployeePhoto = new FileStream(photoEmployeePhoto, FileMode.Open, FileAccess.Read);
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
                    FileStream FileSt = new FileStream(EmployeeNewDefaultPic, FileMode.Open, FileAccess.Read);
                    BinaryReader ReaderBinary = new BinaryReader(FileSt);
                    ImageSaveDefaultPicture = ReaderBinary.ReadBytes((int)FileSt.Length);
                    FileStream FileSt2 = new FileStream(EmployeeNewDefaultSigniture, FileMode.Open, FileAccess.Read);
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
                save.CommandText = ("insert into useraccounts (id_number,password,account_level" +
                    ",account_status,firstname," +
                   "middlename,lastname,birthdate,address,gender,contact_no,date_employed" +
                   ",last_in,last_out,balance,inserted_by,date_exited,employee_picture,employee_signiture,date_archived) values(@ID,@password, @accountlevel,@accountstatus,@firstname,@middlename,@lastname,@birthdate,@address,@gender,@contactno,@dateEmployed,@last_In,@last_Out,@balance,@inserted_by,@date_exited,@employeePicture,@employeeSigniture,@date_archived)");
                save.Parameters.AddWithValue("@ID", employeeForm_new_employeeID_lbl.Text);
                save.Parameters.AddWithValue("@password", employeeForm_new_password_txtBox.Text);
                save.Parameters.AddWithValue("@accountlevel", account_level_value);
                save.Parameters.AddWithValue("@accountstatus", 1);
                save.Parameters.AddWithValue("@firstname", employeeForm_new_firstname_txtBox.Text);
                save.Parameters.AddWithValue("@middlename", employeeForm_new_middlename_txtBox.Text);
                save.Parameters.AddWithValue("@lastname", employeeForm_new_lastname_txtBox.Text);
                save.Parameters.AddWithValue("@birthdate", employeeForm_new_employeeDOB_DTpicker.Text);
                save.Parameters.AddWithValue("@address", employeeForm_new_address_txtBox.Text);
                save.Parameters.AddWithValue("@gender", gendervalue);
                save.Parameters.AddWithValue("@contactno", employeeForm_new_contactno_txtBox.Text);
                save.Parameters.AddWithValue("@dateEmployed", datenow_DTpicker.Text);
                save.Parameters.AddWithValue("@last_In", "N/A");
                save.Parameters.AddWithValue("@last_Out", "N/A");
                save.Parameters.AddWithValue("@balance", "0");
                save.Parameters.AddWithValue("@inserted_by", employeeForm_user_firstname.Text);
                save.Parameters.AddWithValue("@date_exited", "ACTIVE");
                save.Parameters.AddWithValue("@date_archived", "ACTIVE");
                save.Parameters.Add("@employeePicture", MySqlDbType.Blob);
                save.Parameters["@employeePicture"].Value = Employeeimage;
                save.Parameters.Add("@employeeSigniture", MySqlDbType.Blob);
                save.Parameters["@employeeSigniture"].Value = Signitureimage;
                save.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New user added!" + "Your ID is " + employeeForm_new_employeeID_lbl.Text);
                fillEmployeelist();
                employeeForm_new_firstname_txtBox.Clear(); employeeForm_new_middlename_txtBox.Clear(); employeeForm_new_middlename_txtBox.Clear();
                employeeForm_new_lastname_txtBox.Clear(); employeeForm_new_address_txtBox.Clear(); employeeForm_new_contactno_txtBox.Clear();
                employeeForm_new_password_txtBox.Clear(); employeeForm_new_gendermale_rdBtn.Checked = true; employeeForm_new_acctypeEmployee_rdBtn.Checked = true;
                employeeForm_employeePhoto_path_txtBox.Clear(); employeeForm_employeeSigniture_path_txtBox.Clear(); employeeForm_employeedetails_grpBox.Visible = true;
                employeeForm_new_employee_grpBox.Visible = false; employeeForm_addnewuser_btn.Text = "Add new user";
            }
            

}
        //ADD ITEM TO INVENTORY METHOD ENDS HERE//

        public void updateEmployee()
        {
                
            DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {

                if (employeeForm_updateemployeePhoto_path_txtBox.Text != "" && employeeForm_updateemployeeSigniture_path_txtBox.Text != "")
                {
                    string EmployeeUpdateDefaultPicture = employeeForm_updateemployeePhoto_path_txtBox.Text;
                    string EmployeeUpdateDefaultSigniture = employeeForm_updateemployeeSigniture_path_txtBox.Text;
                    byte[] EmployeePicture;
                    byte[] EmployeeSigniturePhoto;

                    FileStream readEmployeePhoto = new FileStream(EmployeeUpdateDefaultPicture, FileMode.Open, FileAccess.Read);
                    FileStream readEmployeeSigniture = new FileStream(EmployeeUpdateDefaultSigniture, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(readEmployeePhoto);
                    EmployeePicture = br.ReadBytes((int)readEmployeePhoto.Length);
                    BinaryReader br2 = new BinaryReader(readEmployeeSigniture);
                    EmployeeSigniturePhoto = br2.ReadBytes((int)readEmployeeSigniture.Length);
                    br.Close();
                    readEmployeePhoto.Close();
                    br2.Close();
                    readEmployeeSigniture.Close();

                    

                    MySqlConnection con = new MySqlConnection(myConnection);
                    con.Open();
                    MySqlCommand save = con.CreateCommand();
                    save.Connection = con;
                    save.CommandText = "update useraccounts set employee_picture=@employeePicture,employee_signiture=@employeeSigniture where id_number = '" + employeeForm_employeeID_lbl.Text + "' ";
                    save.Parameters.Add("@employeePicture", MySqlDbType.Blob);
                    save.Parameters["@employeePicture"].Value = EmployeePicture;
                    save.Parameters.Add("@employeeSigniture", MySqlDbType.Blob);
                    save.Parameters["@employeeSigniture"].Value = EmployeeSigniturePhoto;
                    save.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Photo updated!");
                }

                    MySqlConnection connection = new MySqlConnection(myConnection);

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query0 = "select * from useraccounts where id_number = '" + employeeForm_employeeID_lbl.Text + "'";
                    command.CommandText = query0;
                    MySqlDataReader read = command.ExecuteReader();

                    int count = 0;
                    while (read.Read())
                    {
                        count++;
                    }

                    if (count == 1)
                    {
                            int accountStatusValue = 0;
                            int genderStatusValue = 0;
                        if (employeeForm_employeestatusActive_chkBox.Checked == true)
                        {
                            accountStatusValue = 1;
                        }
                        if (employeeForm_update_employeeFemaleGender_rdBtn.Checked == true)
                        {
                            genderStatusValue = 1;
                        }
                            connection.Close();
                            connection.Open();
                            MySqlCommand command2 = connection.CreateCommand();
                            string query1 = "update useraccounts set  firstname = '" + employeeForm_update_fname_txtBox.Text + "' , middlename = '" + employeeForm_update_mname_txtBox.Text + "', gender = '" + genderStatusValue + "' , lastname = '" + employeeForm_update_lname_txtBox.Text + "' , password = '" + employeeForm_update_password_txtBox.Text + "', account_level = '" + 1 + "', birthdate = '" + employeeForm_employeeDOB_DTpicker.Text + "', date_employed = '" + employeForm_employeedatehired_DTpicker.Text + "', balance = '" + employeeForm_employeebalance_txtBox.Text + "', contact_no = '" + employeeForm_update_contactno_txtBox.Text + "',  account_status  = '" + accountStatusValue + "',  lastupdatedby  = '" + employeeForm_user_firstname.Text + "',  address  = '" + employeeForm_employeeaddress_txtBox.Text + "' where id_number = '" + employeeForm_employeeID_lbl.Text + "' ";
                            command2.CommandText = query1;
                            command2.ExecuteNonQuery();
                            fillEmployeelist();
                           // debugLbl.Text = genderStatusValue.ToString();
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

        public void archiveEmployee()
        {

            DialogResult dr = MessageBox.Show("Are you sure you want to remove user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                MySqlConnection connection = new MySqlConnection(myConnection);

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query0 = "select * from useraccounts where id_number = '" + employeeForm_employeeID_lbl.Text + "'";
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
                    string query1 = "update useraccounts set account_status = '" + 2 + "',date_archived = '" + datenow_DTpicker.Text + "',date_exited = '" + "ARCHIVED" + "' where id_number = '" + employeeForm_employeeID_lbl.Text + "' ";
                    command2.CommandText = query1;
                    command2.ExecuteNonQuery();
                    fillEmployeelist();

                    MessageBox.Show("User records has been archived.", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (count > 1)
                {
                    MessageBox.Show("Archive error", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    return;
                }

            }
        }

        private void employeeForm_edit_btn_Click(object sender, EventArgs e)
        {

        }

        private void stopdebug_btn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void employeeForm_addnewusertoDB_btn_Click(object sender, EventArgs e)
        {
            add_new_user();
        }

        private void employeeForm_addnewuser_btn_Click(object sender, EventArgs e)
        {
            switch (employeeForm_addnewuser_btn.Text)
            {
                case "Add new user":
                    employeeForm_new_employeeID_lbl.Text = NumberRandom.ToString(); 
                    employeeForm_employeedetails_grpBox.Visible = false;
                    employeeForm_new_employee_grpBox.Visible = true;
                    employeeForm_addnewuser_btn.Text = "Cancel";
                    employeeForm_inventory_btn.Enabled = false; employeeForm_suppliers_btn.Enabled = false; employeeForm_sales_btn.Enabled=false; employeeForm_home_btn.Enabled = false;
                    employeeForm_employeelist_lstView.Enabled = false; employeeForm_refreshlist_btn.Enabled = false;
                    break;
                case "Remove user":
                    archiveEmployee();
                    employeeForm_edit_btn.Text = "Edit record";
                    employeeForm_edit_btn.Enabled = false;
                    employeeForm_employeefullname_txtBox.Enabled = false; employeeForm_employeefullname_txtBox.Clear();
                    employeeForm_employeeaddress_txtBox.Enabled = false; employeeForm_employeeaddress_txtBox.Clear();
                    employeeForm_employeeDOB_DTpicker.Enabled = false;
                    employeForm_employeedatehired_DTpicker.Enabled = false;
                    employeeForm_employeebalance_txtBox.Enabled = false; employeeForm_employeebalance_txtBox.Clear();
                    employeeForm_employeestatusActive_chkBox.Enabled = false; employeeForm_employeestatusActive_chkBox.Checked = false;
                    employeeForm_employeeaccstatusInactive_chkBox.Enabled = false; employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
                    employeeForm_update_contactno_txtBox.Enabled = false; employeeForm_update_contactno_txtBox.Clear();
                    employeeForm_update_password_txtBox.Enabled = false; employeeForm_update_password_txtBox.Clear();
                    employeeForm_employeelastin_txtBox.Enabled = false; employeeForm_employeelastin_txtBox.Clear();
                    employeeForm_employeelastout_txtBox.Enabled = false; employeeForm_employeelastout_txtBox.Clear();
                    employeeForm_edit_signiture_grpBox.Enabled = false; employeeForm_update_signiture_pctrBox.Image = null; employeeForm_updateemployeeSigniture_path_txtBox.Clear();
                    employeeForm_edit_employeePhoto_grpBox.Enabled = false; employeeForm_update_employeePhoto_pctrBox.Image = null; employeeForm_updateemployeePhoto_path_txtBox.Clear();
                    employeeForm_updatefname_lbl.Visible = false; employeeForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
                    employeeForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; employeeForm_update_lname_txtBox.Visible = false;
                    employeeForm_update_fullname_lbl.Visible = true; employeeForm_employeefullname_txtBox.Visible = true; employeeForm_employeelist_lstView.Enabled = true;
                    break;
                case "Cancel":
                    employeeForm_employeedetails_grpBox.Visible = true;
                    employeeForm_new_employee_grpBox.Visible = false;
                    employeeForm_addnewuser_btn.Text = "Add new user";
                    employeeForm_new_firstname_txtBox.Clear(); employeeForm_new_middlename_txtBox.Clear(); employeeForm_new_lastname_txtBox.Clear();
                    employeeForm_new_address_txtBox.Clear(); employeeForm_new_contactno_txtBox.Clear(); employeeForm_new_password_txtBox.Clear();
                    employeeForm_new_gendermale_rdBtn.Checked = true; employeeForm_new_acctypeAdmin_rdBtn.Checked = true; employeeForm_new_signiture_pctrBox.Image = null;
                    employeeForm_new_employeePhoto_pctrBox.Image = null; employeeForm_employeePhoto_path_txtBox.Clear(); employeeForm_employeeSigniture_path_txtBox.Clear();
                    employeeForm_inventory_btn.Enabled = true; employeeForm_suppliers_btn.Enabled = true; employeeForm_sales_btn.Enabled = true; employeeForm_home_btn.Enabled = true;
                    employeeForm_employeelist_lstView.Enabled = true; employeeForm_refreshlist_btn.Enabled = true; ResetForm();
                    break;
            }
        }

        private void employeeForm_employeelist_lstView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int i = 0;
            ListViewItem employeeLists = employeeForm_employeelist_lstView.SelectedItems[i];
            employeeForm_employeeID_lbl.Text = employeeLists.SubItems[0].Text;
            employeeForm_employeefullname_txtBox.Text = employeeLists.SubItems[1].Text;
            employeeForm_employeeaddress_txtBox.Text = employeeLists.SubItems[2].Text;
            employeeForm_employeeDOB_DTpicker.Text = employeeLists.SubItems[3].Text;
            employeForm_employeedatehired_DTpicker.Text = employeeLists.SubItems[4].Text;
            employeeForm_employeelastin_txtBox.Text = employeeLists.SubItems[6].Text;
            employeeForm_employeelastout_txtBox.Text = employeeLists.SubItems[7].Text;
            employeeForm_employeebalance_txtBox.Text = employeeLists.SubItems[8].Text;

            
            employeeForm_update_contactno_txtBox.Text = employeeLists.SubItems[11].Text;
            employeeForm_update_password_txtBox.Text = employeeLists.SubItems[12].Text;
            employeeForm_employeedateofexit_lbl.Text = employeeLists.SubItems[10].Text;
            if (employeeLists.SubItems[9].Text == "0")
            { 
                employeeForm_employeedateofexit_lbl.ForeColor = Color.Red ;
                employeeForm_employeedateofexit_lbl.Text = employeeLists.SubItems[10].Text;
                employeeForm_employeestatusActive_chkBox.Checked = false;
                employeeForm_employeeaccstatusInactive_chkBox.Checked = true;
                employeeForm_employeestatusActive_chkBox.Visible = true;
                employeeForm_employeeaccstatusInactive_chkBox.Visible = true;
                employeeForm_employeestatusarchived_lbl.Visible = false;
            }
            else if (employeeLists.SubItems[9].Text == "1")
            {
                employeeForm_employeedateofexit_lbl.ForeColor = Color.Green;
                employeeForm_employeestatusActive_chkBox.Checked = true;
                employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
                employeeForm_employeedateofexit_lbl.Text = employeeLists.SubItems[10].Text;
                employeeForm_employeestatusActive_chkBox.Visible = true;
                employeeForm_employeeaccstatusInactive_chkBox.Visible = true;
                employeeForm_employeestatusarchived_lbl.Visible = false;
            }

            //if(employeeLists.SubItems[10].Text != "N/A" || employeeLists.SubItems[10].Text != "ACTIVE")
            //{
            //    employeeForm_employeedateofexit_lbl.Text = employeeLists.SubItems[10].Text;
            //} 

            if (employeeLists.SubItems[16].Text == "0")
            {
                employeeForm_update_employeeMaleGender_rdBtn.Checked = true;
            }
            else if (employeeLists.SubItems[16].Text == "1")
            {
                employeeForm_update_employeeFemaleGender_rdBtn.Checked = true;
            }

            debugLbl.Text = employeeLists.SubItems[0].Text;

            MySqlConnection conn = new MySqlConnection(myConnection);

            MySqlCommand copro3 = new MySqlCommand();
            conn.Open();
            copro3.Connection = conn;
            copro3.CommandText = "select * from useraccounts where id_number = '" + employeeLists.SubItems[0].Text + "'";
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
                MemoryStream getEmployeePhoto = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["employee_picture"]);
                employeeForm_update_employeePhoto_pctrBox.Image = Image.FromStream(getEmployeePhoto);

                MemoryStream getEmployeeSigniture = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["employee_signiture"]);
                employeeForm_update_signiture_pctrBox.Image = Image.FromStream(getEmployeeSigniture);


            }
            employeeForm_edit_btn.Visible = true;
            employeeForm_edit_btn.Enabled = true;
            if (employeeLists.SubItems[9].Text == "2")
            {
                employeeForm_employeestatusarchived_lbl.Visible = true;
                employeeForm_employeestatusActive_chkBox.Visible = false;
                employeeForm_employeeaccstatusInactive_chkBox.Visible = false;
                employeeForm_edit_btn.Enabled = false;
                employeeForm_edit_btn.Text = "User records unavailable to edit";
                employeeForm_employeedateofexit_lbl.Text = employeeLists.SubItems[10].Text;
            }
            if (employeeLists.SubItems[10].Text == "ARCHIVED")
            {
                employeeForm_employeedateofexit_lbl.Text = "User details archived only";
                employeeForm_employeedateofexit_lbl.ForeColor = Color.Red;
                //employeeForm_employeestatusActive_chkBox.Checked = false;
                //employeeForm_employeeaccstatusInactive_chkBox.Checked = true;
                //employeeForm_employeedateofexit_lbl.ForeColor = Color.Red;
            }
            else
            employeeForm_edit_btn.Text = "Edit record";
        }

        private void employeeForm_new_uploadPhoto_lnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                employeeForm_new_employeePhoto_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                employeeForm_employeePhoto_path_txtBox.Text = open.FileName;
            }
        }

        private void employeeForm_new_uploadSigniture_btn_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                employeeForm_new_signiture_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                employeeForm_employeeSigniture_path_txtBox.Text = open.FileName;
            }
        }

        private void employeeForm_update_uploadsigniture_btn_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                employeeForm_update_signiture_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                employeeForm_updateemployeeSigniture_path_txtBox.Text = open.FileName;
            }
            
        }

        private void employeeForm_update_uploadPhoto_lnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                employeeForm_update_employeePhoto_pctrBox.Image = new Bitmap(open.FileName);
                // image file path
                employeeForm_updateemployeePhoto_path_txtBox.Text = open.FileName;
            }
        }

        private void employeeForm_edit_btn_Click_1(object sender, EventArgs e)
        {
            switch (employeeForm_edit_btn.Text)
            {
                case "Edit record":
                    employeeForm_inventory_btn.Enabled = false; employeeForm_suppliers_btn.Enabled = false; employeeForm_sales_btn.Enabled = false; employeeForm_home_btn.Enabled = false; employeeForm_refreshlist_btn.Enabled=false;
                    employeeForm_doubleClickpicture_lbl.Visible = true;
                    employeeForm_update_employeeMaleGender_rdBtn.Enabled = true;
                    employeeForm_update_employeeFemaleGender_rdBtn.Enabled = true;
                    employeeForm_showhidepass_btn.Enabled = true;
                    employeeForm_addnewuser_btn.Text = "Remove user";
                    int i = 0;
                    ListViewItem employeeLists = employeeForm_employeelist_lstView.SelectedItems[i];
                    employeeForm_update_fname_txtBox.Text = employeeLists.SubItems[13].Text;
                    employeeForm_update_mname_txtBox.Text = employeeLists.SubItems[14].Text;
                    employeeForm_update_lname_txtBox.Text = employeeLists.SubItems[15].Text;

                    employeeForm_employeelist_lstView.Enabled = false;
                    employeeForm_employeelist_lstView.SelectedItems.Clear();
                    employeeForm_edit_btn.Text = "Apply Changes";
                    employeeForm_update_cancel_btn.Visible = true;
                    employeeForm_employeefullname_txtBox.Enabled = true; employeeForm_updatefname_lbl.Visible = true;
                    employeeForm_employeeaddress_txtBox.Enabled = true; employeeForm_update_fname_txtBox.Visible = true;
                    employeeForm_employeeDOB_DTpicker.Enabled = true; employeeForm_updatemname_lbl.Visible = true;
                    employeForm_employeedatehired_DTpicker.Enabled = true; employeeForm_update_mname_txtBox.Visible = true;
                    employeeForm_employeebalance_txtBox.Enabled = true; employeeForm_updatelname_lbl.Visible = true;
                    employeeForm_employeestatusActive_chkBox.Enabled = true; employeeForm_update_lname_txtBox.Visible = true;
                    employeeForm_employeeaccstatusInactive_chkBox.Enabled = true; employeeForm_update_fullname_lbl.Visible = false;
                    employeeForm_update_contactno_txtBox.Enabled = true; employeeForm_employeefullname_txtBox.Visible = false;
                    employeeForm_update_password_txtBox.Enabled = true;
                    employeeForm_employeelastin_txtBox.Enabled = true;
                    employeeForm_employeelastout_txtBox.Enabled = true;
                    employeeForm_edit_signiture_grpBox.Enabled = true;
                    employeeForm_edit_employeePhoto_grpBox.Enabled = true;

                    if (employeeForm_employeedateofexit_lbl.Text == "ARCHIVED")
                    {
                        employeeForm_addnewuser_btn.Enabled = false;
                        employeeForm_employeestatusActive_chkBox.Enabled = false;
                        employeeForm_employeeaccstatusInactive_chkBox.Enabled = false;
                    }
                    break;
                case "Apply Changes":
                    updateEmployee();
                    employeeForm_showhidepass_btn.Text = "show";
                    employeeForm_showhidepass_btn.Enabled = false;
                    employeeForm_edit_btn.Text = "Edit record";
                    employeeForm_edit_btn.Enabled = false;
                    employeeForm_employeefullname_txtBox.Enabled = false; employeeForm_employeefullname_txtBox.Clear();
                    employeeForm_employeeaddress_txtBox.Enabled = false; employeeForm_employeeaddress_txtBox.Clear();
                    employeeForm_employeeDOB_DTpicker.Enabled = false;
                    employeForm_employeedatehired_DTpicker.Enabled = false;
                    employeeForm_employeebalance_txtBox.Enabled = false; employeeForm_employeebalance_txtBox.Clear();
                    employeeForm_employeestatusActive_chkBox.Enabled = false; employeeForm_employeestatusActive_chkBox.Checked = false;
                    employeeForm_employeeaccstatusInactive_chkBox.Enabled = false; employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
                    employeeForm_update_contactno_txtBox.Enabled = false; employeeForm_update_contactno_txtBox.Clear();
                    employeeForm_update_password_txtBox.Enabled = false; employeeForm_update_password_txtBox.Clear();
                    employeeForm_employeelastin_txtBox.Enabled = false; employeeForm_employeelastin_txtBox.Clear();
                    employeeForm_employeelastout_txtBox.Enabled = false; employeeForm_employeelastout_txtBox.Clear();
                    employeeForm_edit_signiture_grpBox.Enabled = false; employeeForm_update_signiture_pctrBox.Image = null; employeeForm_updateemployeeSigniture_path_txtBox.Clear();
                    employeeForm_edit_employeePhoto_grpBox.Enabled = false; employeeForm_update_employeePhoto_pctrBox.Image = null; employeeForm_updateemployeePhoto_path_txtBox.Clear();
                    employeeForm_updatefname_lbl.Visible = false; employeeForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
                    employeeForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; employeeForm_update_lname_txtBox.Visible = false;
                    employeeForm_update_fullname_lbl.Visible = true; employeeForm_employeefullname_txtBox.Visible = true; employeeForm_employeelist_lstView.Enabled = true;
                    employeeForm_update_password_txtBox.PasswordChar = '*'; employeeForm_update_employeeMaleGender_rdBtn.Enabled = false;
                    employeeForm_update_employeeFemaleGender_rdBtn.Enabled = false;
                    break;

            }
        }

        private void employeeForm_employeestatusActive_chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(employeeForm_employeestatusActive_chkBox.Checked == true)
            {
                employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
            }
            else if (employeeForm_employeestatusActive_chkBox.Checked == false && employeeForm_employeeaccstatusInactive_chkBox.Checked == false)
            {
                employeeForm_employeestatusActive_chkBox.Checked = true;
            }
        }

        private void employeeForm_employeeaccstatusInactive_chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (employeeForm_employeeaccstatusInactive_chkBox.Checked == true)
            {
                employeeForm_employeestatusActive_chkBox.Checked = false;
            }
            else if (employeeForm_employeestatusActive_chkBox.Checked == false && employeeForm_employeeaccstatusInactive_chkBox.Checked == false)
            {
                employeeForm_employeeaccstatusInactive_chkBox.Checked = true;
            }
        }

        private void employeeForm_update_cancel_btn_Click(object sender, EventArgs e)
        {
            employeeForm_inventory_btn.Enabled = true; employeeForm_suppliers_btn.Enabled = true; employeeForm_sales_btn.Enabled = true; employeeForm_home_btn.Enabled = true; employeeForm_refreshlist_btn.Enabled = true;
            employeeForm_doubleClickpicture_lbl.Visible = false;
            employeeForm_update_cancel_btn.Visible = false;
            employeeForm_showhidepass_btn.Text = "show";
            employeeForm_showhidepass_btn.Enabled = false;
            employeeForm_addnewuser_btn.Text = "Add new user";
            employeeForm_employeedateofexit_lbl.Text = "";
            employeeForm_employeeID_lbl.Text = "-";
            employeeForm_employeelist_lstView.Enabled = true;
            employeeForm_edit_btn.Text = "Edit record";
            employeeForm_edit_btn.Enabled = false;
            employeeForm_employeefullname_txtBox.Enabled = false; employeeForm_employeefullname_txtBox.Clear();
            employeeForm_employeeaddress_txtBox.Enabled = false; employeeForm_employeeaddress_txtBox.Clear();
            employeeForm_employeeDOB_DTpicker.Enabled = false;
            employeForm_employeedatehired_DTpicker.Enabled = false;
            employeeForm_employeebalance_txtBox.Enabled = false; employeeForm_employeebalance_txtBox.Clear();
            employeeForm_employeestatusActive_chkBox.Enabled = false; employeeForm_employeestatusActive_chkBox.Checked = false;
            employeeForm_employeeaccstatusInactive_chkBox.Enabled = false; employeeForm_employeeaccstatusInactive_chkBox.Checked = false;
            employeeForm_update_contactno_txtBox.Enabled = false; employeeForm_update_contactno_txtBox.Clear();
            employeeForm_update_password_txtBox.Enabled = false; employeeForm_update_password_txtBox.Clear();
            employeeForm_employeelastin_txtBox.Enabled = false; employeeForm_employeelastin_txtBox.Clear();
            employeeForm_employeelastout_txtBox.Enabled = false; employeeForm_employeelastout_txtBox.Clear();
            employeeForm_edit_signiture_grpBox.Enabled = false; employeeForm_update_signiture_pctrBox.Image = null; employeeForm_updateemployeeSigniture_path_txtBox.Clear();
            employeeForm_edit_employeePhoto_grpBox.Enabled = false; employeeForm_update_employeePhoto_pctrBox.Image = null; employeeForm_updateemployeePhoto_path_txtBox.Clear();
            employeeForm_updatefname_lbl.Visible = false; employeeForm_update_fname_txtBox.Visible = false; employeeForm_updatemname_lbl.Visible = false;
            employeeForm_update_mname_txtBox.Visible = false; employeeForm_updatelname_lbl.Visible = false; employeeForm_update_lname_txtBox.Visible = false;
            employeeForm_update_fullname_lbl.Visible = true; employeeForm_employeefullname_txtBox.Visible = true;
            employeeForm_update_password_txtBox.PasswordChar = '*';
        }

        private void employeeForm_home_btn_Click(object sender, EventArgs e)
        {
            POS_Form toPOSForm = new POS_Form();
            toPOSForm.POS_user_Firstname.Text = employeeForm_user_firstname.Text;
            toPOSForm.POS_user_idnumber.Text = employeeForm_user_idnumber.Text;
            this.Close();
            toPOSForm.Show();
        }

        private void employeeForm_inventory_btn_Click(object sender, EventArgs e)
        {
            inventory_Form toInventory = new inventory_Form();
            toInventory.inventoryForm_user_picture.Image = employeeForm_employeeCurrentPicture_pctrBox.Image;
            toInventory.inventoryForm_user_Firstname.Text = employeeForm_user_firstname.Text;
            toInventory.inventoryForm_user_idnumber.Text = employeeForm_user_idnumber.Text;
            this.Close();
            toInventory.Show();
        }

        private void employeeForm_update_fname_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void employeeForm_update_mname_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void employeeForm_update_lname_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void employeeForm_update_contactno_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void employeeForm_employeebalance_txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void employeeForm_employeefullname_txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) employeeForm_employeefullname_txtBox.SelectAll();
        }

        private void employeeForm_update_fname_txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) employeeForm_update_fname_txtBox.SelectAll();
        }

        private void employeeForm_update_mname_txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) employeeForm_update_mname_txtBox.SelectAll();
        }

        private void employeeForm_update_lname_txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) employeeForm_update_lname_txtBox.SelectAll();
        }

        private void employeeForm_employeeaddress_txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) employeeForm_employeeaddress_txtBox.SelectAll();
        }

        private void employeeForm_showhidepass_btn_Click(object sender, EventArgs e)
        {
            switch (employeeForm_showhidepass_btn.Text)
            {
                case "show":
                    employeeForm_update_password_txtBox.PasswordChar = '\0';
                    employeeForm_showhidepass_btn.Text = "hide";
                    break;
                case "hide":
                    employeeForm_update_password_txtBox.PasswordChar = '*';
                    employeeForm_showhidepass_btn.Text = "show";
                    break;

            }
        }

        private void employeeForm_refreshlist_btn_Click(object sender, EventArgs e)
        {
            fillEmployeelist();
        }

        private void employeeForm_update_employeePhoto_pctrBox_DoubleClick(object sender, EventArgs e)
        {
            employeeForm_enlarge_EmployeePhoto_pctrBox.Image = employeeForm_update_employeePhoto_pctrBox.Image; panel2.Visible = false;
            panel5.Visible = false; panel3.Visible = false; employeeForm_employeedetails_grpBox.Visible = false;
            employeeForm_addnewuser_btn.Visible = false; employeeForm_enlarge_EmployeePhoto_pctrBox.Visible = true; employeeForm_CancelEmployeePicturePreview_btn.Visible = true;
        }

        private void employeeForm_CancelEmployeePicturePreview_btn_Click(object sender, EventArgs e)
        {
            employeeForm_CancelEmployeePicturePreview_btn.Visible = false; employeeForm_enlarge_EmployeePhoto_pctrBox.Visible = false; employeeForm_enlarge_EmployeePhoto_pctrBox.Image = null;
            panel5.Visible = true; panel3.Visible = true; employeeForm_employeedetails_grpBox.Visible = true;
            employeeForm_addnewuser_btn.Visible = true; panel2.Visible = true;
        }

        private void employeeForm_update_signiture_pctrBox_DoubleClick(object sender, EventArgs e)
        {
            employeeForm_enlarge_EmployeePhoto_pctrBox.Image = employeeForm_update_signiture_pctrBox.Image;
            panel5.Visible = false; panel3.Visible = false; employeeForm_employeedetails_grpBox.Visible = false; panel2.Visible = false;
            employeeForm_addnewuser_btn.Visible = false; employeeForm_enlarge_EmployeePhoto_pctrBox.Visible = true; employeeForm_CancelEmployeePicturePreview_btn.Visible = true;
        }

        private void employeeForm_suppliers_btn_Click(object sender, EventArgs e)
        {

        }

        private void employeeForm_sales_btn_Click(object sender, EventArgs e)
        {
            sales_form toSalesForm = new sales_form();
            toSalesForm.salesForm_user_Firstname.Text = employeeForm_user_firstname.Text;
            toSalesForm.salesForm_user_idnumber.Text = employeeForm_user_idnumber.Text;
            this.Close();
            toSalesForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer_form toCustomer = new customer_form();
            toCustomer.Customer_user_Firstname.Text = employeeForm_user_firstname.Text;
            toCustomer.Customer_user_idnumber.Text = employeeForm_user_idnumber.Text;
            this.Close();
            toCustomer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transaction_Form toTransactionForm = new Transaction_Form();
            toTransactionForm.Transaction_user_Firstname.Text = employeeForm_user_firstname.Text;
            toTransactionForm.Transaction_user_idnumber.Text = employeeForm_user_idnumber.Text;
            this.Close();
            toTransactionForm.Show();
        }
    }
    
}

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

namespace YvonnieStore_Beta_2_
{
    public partial class inventory : Form
    {
        string myConnection = "Server=localhost;Database= yvonniestores ;Uid=root;Password=";
        public string sendmemes;
        public inventory()
        {
            InitializeComponent();
            viewall();
            inventory_id.Text = NumberRandom.ToString();
            inventory_expire.Value = DateTime.Now.AddDays(+30).Date;
            inventory_expire.MinDate = DateTime.Now.AddDays(+30).Date;
            
        }
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(0, 69) + 14624;
        public static String UserPicture = "";
        public static String DefaultPic = "C:\\Users\\eric agino valdez\\Desktop\\wew\\YvonnieStorePOS(Copro III 2k17 finals)\\Product pics\\unknown.Jpg";
        public static Boolean imagechecker = false;
        public void itemnumber()
        {
            ListViewItem zxc = new ListViewItem();
            inventory_id.Text = zxc.SubItems[0].Text;
            int currentidnumber = Convert.ToInt32(inventory_id.Text);
            int plus1 = currentidnumber + 1;
            inventory_id.Text = plus1.ToString();
        }
        public void viewall()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                inventory_list.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory where category = '" + category.Text + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                    item.SubItems.Add(read["product_name"].ToString());
                    item.SubItems.Add(read["product_flavor"].ToString());
                    item.SubItems.Add(read["product_weight"].ToString());
                    item.SubItems.Add(read["product_stocks"].ToString());
                    item.SubItems.Add(read["product_price"].ToString());
                    item.SubItems.Add(read["product_purchase"].ToString());
                    item.SubItems.Add(read["product_unit"].ToString());
                    item.SubItems.Add(read["product_line"].ToString());
                    item.SubItems.Add(read["product_supplier"].ToString());
                    item.SubItems.Add(read["product_expiry"].ToString());
                    item.SubItems.Add(read["time_added"].ToString());


                    inventory_list.Items.Add(item);
                    inventory_list.FullRowSelect = true;


                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void morphchecker()
        {

            switch (inventory_morph.Text)
            {
                case "Update":
                    morphupdate();
                    break;
                case "Add":
                    morphadd();
                    break;

            }
        }
        public void morphadd()
        {
            if (canadd.Checked == true||adderxcheck.Text == "OK")
            {
                if (adderxcheck.Text == "OK")
                {
                    canadd.Checked = true;
                }
                addmethod();
            }

            else
            {
               
                DialogResult dr = MessageBox.Show("You are not allowed to add a inventory item. Do you wish to enter another account who has the authority to add the ff. items?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    bool adder = false;
                    bool updater = false;
                    bool viewer = false;
                    bool deleter = false;
                    if (canadd.Checked == true)
                    {
                        adder = true;
                    }
                    else if (canadd.Checked == false)
                    {
                        adder = false;
                    }
                    if (canupdate.Checked == true)
                    {
                        updater = true;
                    }
                    else if (canupdate.Checked == false)
                    {
                        updater = false;
                    }
                    if (canview.Checked == true)
                    {
                        viewer = true;
                    }
                    else if (canview.Checked == false)
                    {
                        viewer = false;
                    }
                    if (candelete.Checked == true)
                    {
                        deleter = true;
                    }
                    else if (candelete.Checked == false)
                    {
                        deleter = false;
                    }
                    authenticator x = new authenticator();
                    x.Show();
                    x.accessoptions.Text = "add";
                    x.usersinfo_username.Text = usersinfo_username.Text;
                    x.usersinfo_firstname.Text = usersinfo_firstname.Text;
                    x.usersinfo_lastname.Text = usersinfo_lastname.Text;
                    x.canadd.Checked = adder;
                    x.canupdate.Checked = updater;
                    x.canview.Checked = viewer;
                    x.candelete.Checked = deleter;
                    x.inventory_id.Text = inventory_id.Text;
                    x.inventory_name.Text = inventory_name.Text;
                    x.inventory_flavor.Text = inventory_flavor.Text;
                    x.inventory_unit.Text = inventory_unit.Text;
                    x.subweight.Text = subweight.Text;
                    x.inventory_stocks.Text = inventory_stocks.Text;
                    x.inventory_price.Text = inventory_price.Text;
                    x.inventory_purchase.Text = inventory_purchase.Text;
                    x.inventory_measurement.Text = inventory_measurement.Text;
                    x.inventory_category.Text = inventory_category.Text;
                    x.inventory_line.Text = inventory_line.Text;
                    x.inventory_supplier.Text = inventory_supplier.Text;
                    x.inventory_category.Text = inventory_category.Text;
                    x.product_picture.Image = product_picture.Image;
                    x.inventory_expire.Text = inventory_expire.Text;
                    x.timenow_value.Text = timenow_value.Text;
                    this.Hide();
                    //x.usersinfo_accounttype.Text = usersinfo_accounttype.Text;
                }

            }
        }
        public void addmethod()
        {
            //try
            //{

            int a = Convert.ToInt32(inventory_id.Text);

            int b = a + 3;
            
            //if (inventory_food.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");
            //}
            //else if (inventory_beverage.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");
            //}
            //else if (inventory_other.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");
            //}
            //else if (inventory_liquids.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");

            //}
            //else if (inventory_bar.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");
            //}
            //else if (inventory_other2.Checked == false)
            //{
            //    MessageBox.Show("Oops you forgot to choose a category.");
            //}

            string sendnudes = sendmemes;
            byte[] ImageSave = null;

            if (imagechecker != false)
            {
                FileStream FileSt = new FileStream(UserPicture, FileMode.Open, FileAccess.Read);
                BinaryReader ReaderBinary = new BinaryReader(FileSt);
                ImageSave = ReaderBinary.ReadBytes((int)FileSt.Length);
            }
            else if (imagechecker == false)
            {
                FileStream FileSt = new FileStream(DefaultPic, FileMode.Open, FileAccess.Read);
                BinaryReader ReaderBinary = new BinaryReader(FileSt);
                ImageSave = ReaderBinary.ReadBytes((int)FileSt.Length);
            }
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();

            //MySqlCommand command = new MySqlCommand("insert into data (ID,firstname,lastname,username,password,gender,email,birthdate,useradd,userupdate,userview,userdelete,pic) values ('" + userid.Text + "', '" + firstnametxtbox.Text + "','" + lastnametxtbox.Text + "','" + usernametxtbox.Text + "','" + qwe.encryptpassword(passwordtxtbox.Text) + "','" + truegender + "','" + emailtxtbox.Text + "','" + datetimepicker.Text + "','" + addchecker.Text + "','" + updatechecker.Text + "','" + viewchecker.Text + "','" + deletechecker.Text + "','" + ImageSave + "')", connection);
            MySqlCommand command = new MySqlCommand("insert into inventory (product_ID,product_name,product_flavor,product_line,product_unit,product_stocks,product_price,product_supplier,product_purchase,category,product_expiry,time_added,product_weight) values ('" + inventory_id.Text + "','" + inventory_name.Text + "','" + inventory_flavor.Text + "','" + inventory_line.Text + "','" + inventory_measurement.Text + "','" + inventory_stocks.Text + "','" + inventory_price.Text + "','" + inventory_supplier.Text + "','" + inventory_purchase.Text + "','" + inventory_category.Text + "','" + inventory_expire.Text + "','" + datenow_value.Text + " " + timenow_value.Text + "','" + inventory_unit.Text + " " + subweight.Text  + "')", connection);
            command.ExecuteNonQuery();
            inventory_id.Text = b.ToString();
            viewall();
            inventory_name.Clear();
            inventory_flavor.Clear();
            inventory_measurement.Text = "";
            inventory_stocks.Clear();
            inventory_line.Text = "";
            inventory_price.Clear();
            inventory_supplier.Text = "";
            inventory_purchase.Clear();
            inventory_category.Text = "";
            int c = Convert.ToInt32(inventory_id.Text);

            int asd = c + 3;
            inventory_id.Text = asd.ToString();
            //addbacktrack();
            //checker();
            // crazy();
            MessageBox.Show("New item successfully inserted into the inventory list.", "SUCCESS!");

        }

        //catch (Exception e)
        //{
        //    MessageBox.Show("ERROR: " + e);
        //}
        // }
        public void updatemethod()
        {
            if (canupdate.Checked == true)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    MySqlConnection connection = new MySqlConnection(myConnection);

                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    string query0 = "select * from inventory where product_ID = '" + inventory_id.Text + "'";
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
                        string query1 = "update inventory set  product_name = '" + inventory_name.Text + "' , product_flavor = '" + inventory_flavor.Text + "' , product_line = '" + inventory_line.Text + "' , product_unit = '" + inventory_measurement.Text + "' , product_stocks = '" + inventory_stocks.Text + "' , product_price = '" + inventory_price.Text + "' , product_supplier = '" + inventory_supplier.Text + "', product_purchase = '" + inventory_purchase.Text + "' , time_updated = '" + DateTime.Now + "'where product_ID = '" + inventory_id.Text + "' ";
                        command2.CommandText = query1;
                        command2.ExecuteNonQuery();
                        viewall();
                        //updatebacktrack();
                        //checker();
                        // crazy();
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
        }

        public void morphupdate()
        {

            if (canupdate.Checked == true)
            {
                updatemethod();
            }
            else
            {
                DialogResult dr = MessageBox.Show("You are not allowed to update a inventory item. Do you wish to enter another account who has the authority to update the ff. items?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    bool adder = false;
                    bool updater = false;
                    bool viewer = false;
                    bool deleter = false;
                    if (canadd.Checked == true)
                    {
                        adder = true;
                    }
                    else if (canadd.Checked == false)
                    {
                        adder = false;
                    }
                    if (canupdate.Checked == true)
                    {
                        updater = true;
                    }
                    else if (canupdate.Checked == false)
                    {
                        updater = false;
                    }
                    if (canview.Checked == true)
                    {
                        viewer = true;
                    }
                    else if (canview.Checked == false)
                    {
                        viewer = false;
                    }
                    if (candelete.Checked == true)
                    {
                        deleter = true;
                    }
                    else if (candelete.Checked == false)
                    {
                        deleter = false;
                    }
                    authenticator x = new authenticator();
                    x.Show();
                    x.accessoptions.Text = "update";
                    x.usersinfo_username.Text = usersinfo_username.Text;
                    x.usersinfo_firstname.Text = usersinfo_firstname.Text;
                    x.usersinfo_lastname.Text = usersinfo_lastname.Text;
                    x.canadd.Checked = adder;
                    x.canupdate.Checked = updater;
                    x.canview.Checked = viewer;
                    x.candelete.Checked = deleter;
                }
                else
                {
                    return;
                }
            }
        }

     
        private void inventory_Load(object sender, EventArgs e)
        {
            panel4.AutoScroll = false;
            panel4.HorizontalScroll.Enabled = true;
            panel4.HorizontalScroll.Visible = true;
            panel4.HorizontalScroll.Maximum = 100;
            panel4.AutoScroll = true;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timenow_value.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datenow_value.Text = DateTime.Now.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            morphchecker();
        }

        private void inventory_picture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void user_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png";

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                UserPicture = OpenFile.FileName.ToString();
                product_picture.ImageLocation = UserPicture;
                imagechecker = true;

            }
        }

        private void inventory_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            product_picture.Image = null;
            inventory_morph.Text = "Update";
            inventory_morph2.Text = "Cancel";
            int i = 0;

            ListViewItem qwe = inventory_list.SelectedItems[i];
            inventory_id.Text = qwe.SubItems[0].Text;
            inventory_name.Text = qwe.SubItems[1].Text;
            inventory_flavor.Text = qwe.SubItems[2].Text;
            inventory_unit.Text = qwe.SubItems[3].Text;
            inventory_stocks.Text = qwe.SubItems[4].Text;
            // repasswordtxtbox.Text = qwe.SubItems[5].Text;
            inventory_price.Text = qwe.SubItems[5].Text;
            inventory_purchase.Text = qwe.SubItems[6].Text;
            inventory_measurement.Text = qwe.SubItems[7].Text;
            inventory_line.Text = qwe.SubItems[8].Text;
            inventory_supplier.Text = qwe.SubItems[9].Text;
            inventory_expire.Text = qwe.SubItems[10].Text;
            inventory_category.Text = category.Text;

            inventory_category.Enabled = false;
            MySqlConnection conn = new MySqlConnection(myConnection);

            MySqlCommand copro3 = new MySqlCommand();
            conn.Open();
            copro3.Connection = conn;
            copro3.CommandText = "select * from inventory where product_id = '" + qwe.SubItems[0].Text + "'";
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
                MemoryStream MemeStream = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["product_picture"]);

                product_picture.Image = Image.FromStream(MemeStream);
            }
        }

        private void junkfoodsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Junkfoods";
            viewall();
        }

        private void biscuitsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Biscuits";
            viewall();
        }

        private void candiesbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Candies";
            viewall();
        }

        private void chocolatesbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Chocolates";
            viewall();
        }

        private void homemadeproductsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Home-Made-Products";
            viewall();
        }

        private void readytodrinkbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Ready-To-Drink";
            viewall();
        }

        private void liquors_Click(object sender, EventArgs e)
        {
            category.Text = "Liquors";
            viewall();
        }

        private void pastabtn_Click(object sender, EventArgs e)
        {
            category.Text = "Pasta";
            viewall();
        }

        private void saucebtn_Click(object sender, EventArgs e)
        {
            category.Text = "Sauce";
            viewall();
        }

        private void spreadsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Spreads";
            viewall();
        }

        private void cannedgoodsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Canned Goods";
            viewall();
        }

        private void noodlesbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Noodles";
            viewall();
        }

        private void seasoningbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Seasoning";
            viewall();
        }

        private void cerealsbtn_Click(object sender, EventArgs e)
        {
            category.Text = "Cereals";
            viewall();
        }

        private void proceedto_transactions_Click(object sender, EventArgs e)
        {
            bool adder = false;
            bool updater = false;
            bool viewer = false;
            bool deleter = false;
            if (canadd.Checked == true)
            {
                adder = true;
            }
            else if (canadd.Checked == false)
            {
                adder = false;
            }
            if (canupdate.Checked == true)
            {
                updater = true;
            }
            else if (canupdate.Checked == false)
            {
                updater = false;
            }
            if (canview.Checked == true)
            {
                viewer = true;
            }
            else if (canview.Checked == false)
            {
                viewer = false;
            }
            if (candelete.Checked == true)
            {
                deleter = true;
            }
            else if (candelete.Checked == false)
            {
                deleter = false;
            }

            Transactions a = new Transactions();
            this.Hide();
            a.usersinfo_username.Text = usersinfo_username.Text;
            a.usersinfo_firstname.Text = usersinfo_firstname.Text;
            a.usersinfo_lastname.Text = usersinfo_lastname.Text;
            // a.usersinfo_accounttype.Text = usersinfo_accessibility.Text;
            a.canadd.Checked = adder;
            a.canupdate.Checked = updater;
            a.canview.Checked = viewer;
            a.candelete.Checked = deleter;
            a.Show();
        }

        private void proceedto_employees_Click(object sender, EventArgs e)
        {
            bool adder = false;
            bool updater = false;
            bool viewer = false;
            bool deleter = false;
            if (canadd.Checked == true)
            {
                adder = true;
            }
            else if (canadd.Checked == false)
            {
                adder = false;
            }
            if (canupdate.Checked == true)
            {
                updater = true;
            }
            else if (canupdate.Checked == false)
            {
                updater = false;
            }
            if (canview.Checked == true)
            {
                viewer = true;
            }
            else if (canview.Checked == false)
            {
                viewer = false;
            }
            if (candelete.Checked == true)
            {
                deleter = true;
            }
            else if (candelete.Checked == false)
            {
                deleter = false;
            }

            employees c = new employees();
            this.Hide();
            c.Show();
            c.usersinfo_username.Text = usersinfo_username.Text;
            c.usersinfo_firstname.Text = usersinfo_firstname.Text;
            c.usersinfo_lastname.Text = usersinfo_lastname.Text;
            //c.usersinfo_accounttype.Text = usersinfo_accessibility.Text;
            c.canadd.Checked = adder;
            c.canupdate.Checked = updater;
            c.canview.Checked = viewer;
            c.candelete.Checked = deleter;
        }

        private void proceedto_inventory_Click(object sender, EventArgs e)
        {
            bool adder = false;
            bool updater = false;
            bool viewer = false;
            bool deleter = false;
            if (canadd.Checked == true)
            {
                adder = true;
            }
            else if (canadd.Checked == false)
            {
                adder = false;
            }
            if (canupdate.Checked == true)
            {
                updater = true;
            }
            else if (canupdate.Checked == false)
            {
                updater = false;
            }
            if (canview.Checked == true)
            {
                viewer = true;
            }
            else if (canview.Checked == false)
            {
                viewer = false;
            }
            if (candelete.Checked == true)
            {
                deleter = true;
            }
            else if (candelete.Checked == false)
            {
                deleter = false;
            }

            Form1 c = new Form1();
            this.Hide();
            c.Show();
            c.usersinfo_username.Text = usersinfo_username.Text;
            c.usersinfo_firstname.Text = usersinfo_firstname.Text;
            c.usersinfo_lastname.Text = usersinfo_lastname.Text;
            //c.usersinfo_accounttype.Text = usersinfo_accessibility.Text;
            c.canadd.Checked = adder;
            c.canupdate.Checked = updater;
            c.canview.Checked = viewer;
            c.candelete.Checked = deleter;
        }

        private void inventory_morph2_Click(object sender, EventArgs e)
        {
            if (inventory_morph2.Text == "Clear")
            {
                //inventory_id.Clear();
                inventory_name.Clear();
                inventory_flavor.Clear();
                inventory_measurement.Text = "";
                inventory_stocks.Clear();
                inventory_line.Text = "";
                inventory_price.Clear();
                inventory_supplier.Text = "";
                inventory_purchase.Clear();
                inventory_unit.Clear();
                inventory_category.Text = "";
                inventory_expire.Value = DateTime.Now.AddDays(+30).Date;
                int c = Convert.ToInt32(inventory_id.Text);

                int asd = c + 3;
                inventory_id.Text = asd.ToString();
            }
            if (inventory_morph2.Text == "Cancel")
            {
                inventory_morph2.Text = "Clear";
                int c = Convert.ToInt32(inventory_id.Text);

                int asd = c + 3;
                inventory_id.Text = NumberRandom.ToString();
                //inventory_id.Text = c.ToString() ;
                inventory_morph.Text = "Add";
                inventory_name.Clear();
                inventory_flavor.Clear();
                inventory_measurement.Text = "";
                inventory_stocks.Clear();
                inventory_line.Text = "";
                inventory_price.Clear();
                inventory_supplier.Text = "";
                inventory_purchase.Clear();
                inventory_category.Text = "";
                inventory_unit.Clear();
                inventory_expire.Value = DateTime.Now.AddDays(+30).Date;
                product_picture.Image = null;
                inventory_category.Enabled = true;
            }
        }

        private void inventory_Activated(object sender, EventArgs e)
        {
            

            //user_picture.Image = System.Drawing.Image.FromStream(login.ImageStream);
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string query0 = "select * from employees where username = '" + usersinfo_username.Text + "'";
            command.CommandText = query0;
            MySqlDataReader copro = command.ExecuteReader();

            int count = 0;
            while (copro.Read())
            {
                count++;
            }

            if (count == 1)
            {
                string firstname = (copro["firstname"].ToString());

                connection.Close();
                connection.Open();
                MySqlCommand command2 = connection.CreateCommand();
                string query1 = "update employees set status = '" + "Offline" + "' where username = '" + usersinfo_username.Text + "' ";
                command2.CommandText = query1;
                command2.ExecuteNonQuery();

                MessageBox.Show("You are now logged out! " + firstname, "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                login qwe = new login();
                this.Hide();
                qwe.Show();

            }
        }

        private void inventory_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void inventory_flavor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void inventory_supplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                   && e.KeyChar != Convert.ToInt16(Keys.Back));   
        }

        private void inventory_line_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                   && e.KeyChar != Convert.ToInt16(Keys.Back));   
        }

        private void inventory_unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
               && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void inventory_stocks_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void inventory_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
               && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            
        }

        private void inventory_purchase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
               && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        
    }
}

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
    public partial class authenticator : Form
    {
        string myConnection = "Server=localhost;Database= yvonniestores ;Uid=root;Password=";
        public authenticator()
        {
            InitializeComponent();
        }
        public static bool adderx = false;
        public static bool updater = false;
        public static bool viewer = false;
        public static bool deleter = false;
        private void loginbutton_Click(object sender, EventArgs e)
        {
            if (accessoptions.Text == "update")
            {
                if (usernametxtbox.Text == "")
                {
                    //String Error = ValidateInput.ValidateLenght(username.Text.Length) ? "" : "adjashd";
                    MessageBox.Show(this, "Username field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (passwordtxtbox.Text == "")
                {
                    MessageBox.Show(this, "Password field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand Command = connection.CreateCommand();

                Command.Connection = connection;
                Command.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";

                MySqlDataReader read = Command.ExecuteReader();
                int count1 = 0;
                while (read.Read())
                {
                    count1++;

                }
                connection.Close();
                if (count1 == 1)
                {
                    MySqlCommand copro3 = new MySqlCommand();
                    connection.Open();
                    copro3.Connection = connection;
                    copro3.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";
                    MySqlDataReader copro = copro3.ExecuteReader();
                    while (copro.Read())
                    {
                        string firstname = (copro["firstname"].ToString());
                        string lastname = (copro["lastname"].ToString());
                        string user = (copro["ID"].ToString());
                        string pass = (copro["password"].ToString());

                        string ups = (copro["can_update"].ToString());

                        bool updater = true;
                        if (ups == "Allowed")
                        {
                            canupdate.Checked = true;
                            updater = true;
                        }
                        if (user == usernametxtbox.Text && pass == passwordtxtbox.Text && canupdate.Checked == true)
                        {
                            canupdate.Checked = true;
                            ////bool adderx = false;
                            ////bool updaterx = false;
                            ////bool viewer = false;
                            ////bool deleter = false;
                            if (canadd.Checked == true)
                            {
                                adderx = true;
                            }
                            else if (canadd.Checked == false)
                            {
                                adderx = false;
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


                            inventory x = new inventory();
                            x.Show();
                            x.usersinfo_username.Text = usersinfo_username.Text;
                            x.usersinfo_firstname.Text = usersinfo_firstname.Text;
                            x.usersinfo_lastname.Text = usersinfo_lastname.Text;
                            x.canadd.Checked = adderx;
                            x.canupdate.Checked = updater;
                            x.candelete.Checked = deleter;
                            x.canview.Checked = viewer;
                            
                            //new inventory().ShowDialog();
                            this.Hide();
                        }
                    }
                }
            }
            else if (accessoptions.Text == "add")
            {
                //canadd.Checked = true;
                //bool addersu = false;
                //if (canadd.Checked == true)
                //{
                //    addersu = true;
                //}
                //else if (canadd.Checked == false)
                //{
                //    addersu = false;
                //}

                if (usernametxtbox.Text == "")
                {
                    //String Error = ValidateInput.ValidateLenght(username.Text.Length) ? "" : "adjashd";
                    MessageBox.Show(this, "Username field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (passwordtxtbox.Text == "")
                {
                    MessageBox.Show(this, "Password field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand Command = connection.CreateCommand();

                Command.Connection = connection;
                Command.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";

                MySqlDataReader read = Command.ExecuteReader();
                int count1 = 0;
                while (read.Read())
                {
                    count1++;

                }
                connection.Close();
                if (count1 == 1)
                {
                    MySqlCommand copro3 = new MySqlCommand();
                    connection.Open();
                    copro3.Connection = connection;
                    copro3.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";
                    MySqlDataReader copro = copro3.ExecuteReader();
                    while (copro.Read())
                    {
                        string firstname = (copro["firstname"].ToString());
                        string lastname = (copro["lastname"].ToString());
                        string user = (copro["ID"].ToString());
                        string pass = (copro["password"].ToString());
                        string adds = (copro["can_add"].ToString());

                        bool adder = true;
                        //int a = 0;
                        if (adds == "Allowed")
                        {
                            canadd.Checked = true;
                            adder = true;
                            //a = 1;



                            if (user == usernametxtbox.Text && pass == passwordtxtbox.Text && canadd.Checked == true)
                            {
                                canadd.Checked = true;
                                
                                if (canadd.Checked == true)
                                {
                                    adderx = true;
                                }
                                else if (canadd.Checked == false)
                                {
                                    adderx = false;
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
                                

                                inventory x = new inventory();
                                x.usersinfo_username.Text = usersinfo_username.Text;
                                x.usersinfo_firstname.Text = usersinfo_firstname.Text;
                                x.usersinfo_lastname.Text = usersinfo_lastname.Text;
                                
                                x.canadd.Checked = adderx;
                                x.canupdate.Checked = updater;
                                x.candelete.Checked = deleter;
                                x.canview.Checked = viewer;
                                x.adderxcheck.Text = "OK";
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
                                x.addmethod();
                                x.Show();
                                this.Hide();


                            }
                        }
                    }
                }
            }
            else if (accessoptions.Text == "delete")
            {
                if (usernametxtbox.Text == "")
                {
                    //String Error = ValidateInput.ValidateLenght(username.Text.Length) ? "" : "adjashd";
                    MessageBox.Show(this, "Username field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (passwordtxtbox.Text == "")
                {
                    MessageBox.Show(this, "Password field is empty.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand Command = connection.CreateCommand();

                Command.Connection = connection;
                Command.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";

                MySqlDataReader read = Command.ExecuteReader();
                int count1 = 0;
                while (read.Read())
                {
                    count1++;

                }
                connection.Close();
                if (count1 == 1)
                {
                    MySqlCommand copro3 = new MySqlCommand();
                    connection.Open();
                    copro3.Connection = connection;
                    copro3.CommandText = "select * from employees where ID = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";
                    MySqlDataReader copro = copro3.ExecuteReader();
                    while (copro.Read())
                    {
                        string firstname = (copro["firstname"].ToString());
                        string lastname = (copro["lastname"].ToString());
                        string user = (copro["ID"].ToString());
                        string pass = (copro["password"].ToString());
                        string dels = (copro["can_delete"].ToString());

                        bool deleter = true;
                        if (dels == "Allowed")
                        {
                            canadd.Checked = true;
                            deleter = true;


                            if (user == usernametxtbox.Text && pass == passwordtxtbox.Text && candelete.Checked == true)
                            {
                                inventory x = new inventory();
                                x.candelete.Checked = deleter;
                                x.Show();
                                this.Hide();
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

            inventory a = new inventory();
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
    }
}

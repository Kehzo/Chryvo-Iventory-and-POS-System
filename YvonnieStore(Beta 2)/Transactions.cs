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
    public partial class Transactions : Form
    {
        string myConnection = "Server=localhost;Database= yvonniestores ;Uid=root;Password=";
        public Transactions()
        {
            InitializeComponent();
            view();
        }
        public void view()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                transaction_list.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from transaction";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["transaction_number"].ToString());
                    item.SubItems.Add(read["transaction_items"].ToString());
                    item.SubItems.Add(read["cash_paid"].ToString());
                    item.SubItems.Add(read["customer_change"].ToString());
                    item.SubItems.Add(read["transaction_total"].ToString());
                    item.SubItems.Add(read["transaction_date"].ToString());
                    item.SubItems.Add(read["cashier"].ToString());
                    item.SubItems.Add(read["customers_name"].ToString());
                    


                    transaction_list.Items.Add(item);
                    transaction_list.FullRowSelect = true;


                }
                connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
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

            Form1 c = new Form1();
            c.Show();
            
            c.usersinfo_username.Text = usersinfo_username.Text;
            c.usersinfo_firstname.Text = usersinfo_firstname.Text;
            c.usersinfo_lastname.Text = usersinfo_lastname.Text;
            //c.usersinfo_accounttype.Text = usersinfo_accessibility.Text;
            c.canadd.Checked = adder;
            c.canupdate.Checked = updater;
            c.canview.Checked = viewer;
            c.candelete.Checked = deleter;
            this.Hide();
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

        private void Transactions_Activated(object sender, EventArgs e)
        {
            //user_picture.Image = System.Drawing.Image.FromStream(login.ImageStream);
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
    }
}

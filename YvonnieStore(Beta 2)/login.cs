using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace YvonnieStore_Beta_2_
{
    public partial class login : Form
    {
        string myConnection = "Server=localhost;database=chryvo_store_database ;Uid=root;Password=";
        //public static byte[] Image = null;
        //public static MemoryStream ImageStream;
        public login()
        {
            InitializeComponent();
            //viewall();
            //zzz();
            //totalsalestoday();
            //usernametxtbox.ShortcutsEnabled = true;
        }
        public void checkin()
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
            else
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                connection.Open();
                MySqlCommand Command = connection.CreateCommand();

                Command.Connection = connection;
                Command.CommandText = "select * from useraccounts where id_number = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";

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
                    copro3.CommandText = "select * from useraccounts where id_number = '" + usernametxtbox.Text + "' and password = '" + passwordtxtbox.Text + "'";
                    MySqlDataReader copro = copro3.ExecuteReader();
                    while (copro.Read())
                    {
                        string firstname = (copro["firstname"].ToString());
                        string lastname = (copro["lastname"].ToString());
                        string ID = (copro["id_number"].ToString());
                        string pass = (copro["password"].ToString());
                            if (ID == usernametxtbox.Text && pass == passwordtxtbox.Text)
                            {
                                MessageBox.Show("Welcome! " + firstname + " " + lastname, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                POS_Form POS_Form = new POS_Form();
                                POS_Form.Show();
                                POS_Form.POS_user_Firstname.Text = firstname;
                                POS_Form.POS_user_idnumber.Text = ID;
                                this.Hide();

                            }
                        
                    }


                }
                else
                {
                    usernametxtbox.Text = "";
                    passwordtxtbox.Text = "";
                    MessageBox.Show(this, "Login Failed, No account found", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernametxtbox.Focus();

                }
            }
        }
        private void loginbutton_Click(object sender, EventArgs e)
        {
            checkin();    
        }
       
        private void usernametxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                 && e.KeyChar != Convert.ToInt16(Keys.Back));

            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);

            if (usernametxtbox.Text.Length >= 8)
            {
                MessageBox.Show("Maximum Username Length");
                usernametxtbox.Clear();
            }
            if (e.KeyChar == Convert.ToInt16(Keys.Enter))
            {

                switch (loginbutton.Enabled)
                {
                    case true:
                        checkin();
                        break;
                    case false:
                        MessageBox.Show("Username is incorrect! Please try again");
                        break;
                }

            }
        }

        private void passwordtxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsLetter(e.KeyChar)
              && !Char.IsDigit(e.KeyChar)
                   && e.KeyChar != Convert.ToInt16(Keys.Back));

            if (passwordtxtbox.Text.Length == 23)
            {
                MessageBox.Show("Maximum Password Length");
            }

            if (e.KeyChar == Convert.ToInt16(Keys.Enter))
            {
                checkin();

            }
        }

        private void usernametxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) usernametxtbox.SelectAll();
        }

        private void passwordtxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) passwordtxtbox.SelectAll();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }

}

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

namespace YvonnieStore_Beta_2_
{
    public partial class employees : Form
    {
        string myConnection = "Server=localhost;Database= yvonniestores ;Uid=root;Password=";
        public employees()
        {
            InitializeComponent();
            view();
        }

        //public static class StringCipher
        //{hh
        //    // This constant is used to determine the keysize of the encryption algorithm in bits.
        //    // We divide this by 8 within the code below to get the equivalent number of bytes.
        //    private const int Keysize = 256;

        //    // This constant determines the number of iterations for the password bytes generation function.
        //    private const int DerivationIterations = 1000;

        //    public static string Encrypt(string plainText, string passPhrase)
        //    {
        //        // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
        //        // so that the same Salt and IV values can be used when decrypting.  
        //        var saltStringBytes = Generate256BitsOfRandomEntropy();
        //        var ivStringBytes = Generate256BitsOfRandomEntropy();
        //        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //        using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        //        {
        //            var keyBytes = password.GetBytes(Keysize / 8);
        //            using (var symmetricKey = new RijndaelManaged())
        //            {
        //                symmetricKey.BlockSize = 256;
        //                symmetricKey.Mode = CipherMode.CBC;
        //                symmetricKey.Padding = PaddingMode.PKCS7;
        //                using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
        //                {
        //                    using (var memoryStream = new MemoryStream())
        //                    {
        //                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //                        {
        //                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //                            cryptoStream.FlushFinalBlock();
        //                            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
        //                            var cipherTextBytes = saltStringBytes;
        //                            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
        //                            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
        //                            memoryStream.Close();
        //                            cryptoStream.Close();
        //                            return Convert.ToBase64String(cipherTextBytes);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    public static string Decrypt(string cipherText, string passPhrase)
        //    {
        //        // Get the complete stream of bytes that represent:
        //        // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        //        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
        //        // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        //        var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
        //        // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        //        var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
        //        // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        //        var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

        //        using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
        //        {
        //            var keyBytes = password.GetBytes(Keysize / 8);
        //            using (var symmetricKey = new RijndaelManaged())
        //            {
        //                symmetricKey.BlockSize = 256;
        //                symmetricKey.Mode = CipherMode.CBC;
        //                symmetricKey.Padding = PaddingMode.PKCS7;
        //                using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
        //                {
        //                    using (var memoryStream = new MemoryStream(cipherTextBytes))
        //                    {
        //                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        //                        {
        //                            var plainTextBytes = new byte[cipherTextBytes.Length];
        //                            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //                            memoryStream.Close();
        //                            cryptoStream.Close();
        //                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    private static byte[] Generate256BitsOfRandomEntropy()
        //    {
        //        var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
        //        using (var rngCsp = new RNGCryptoServiceProvider())
        //        {
        //            // Fill the array with cryptographically secure random bytes.
        //            rngCsp.GetBytes(randomBytes);
        //        }
        //        return randomBytes;
        //    }
        //}
         
        Timer t = new Timer();
        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(0, 69) + 14624;
        public static String UserPicture = "";
        public static String DefaultPic = "C:\\Users\\eric agino valdez\\Desktop\\wew\\YvonnieStorePOS(Copro III 2k17 finals)\\Product pics\\unknown.Jpg";
        public static Boolean imagechecker = false;

        public void view()
        {
            MySqlConnection connection = new MySqlConnection(myConnection);
            accounts_list.Items.Clear();
            connection.Close();

            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            string query = "select * from employees ";
            command.CommandText = query;
            MySqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {

                ListViewItem item = new ListViewItem(read["ID"].ToString());
                item.SubItems.Add(read["firstname"].ToString());
                item.SubItems.Add(read["lastname"].ToString());
                item.SubItems.Add(read["username"].ToString());
                item.SubItems.Add(read["password"].ToString());
                item.SubItems.Add(read["gender"].ToString());
                item.SubItems.Add(read["status"].ToString());
                item.SubItems.Add(read["lastlog"].ToString());
                



                accounts_list.Items.Add(item);
                accounts_list.FullRowSelect = true;


            }
            connection.Close();
        }
        public void adduser()
        {
            try
            {

                if (user_password.Text == user_repass.Text)
                {
                    string truegender = "";
                    string yes = "Allowed";
                    string no = "Denied";

                    if (malebutton.Checked == true)
                    {
                        truegender = "Male";
                    }
                    else if (femalebutton.Checked == true)
                    {
                        truegender = "Female";
                    }
                    if (add_check.Checked == true)
                    {
                        addchecker.Text = yes;
                    }
                    else if (add_check.Checked == false)
                    {
                        addchecker.Text = no;
                    }
                    if (update_check.Checked == true)
                    {
                        updatechecker.Text = yes;
                    }
                    else if (update_check.Checked == false)
                    {
                        updatechecker.Text = no;
                    }
                    if (view_check.Checked == true)
                    {
                        viewchecker.Text = yes;
                    }
                    else if (view_check.Checked == false)
                    {
                        viewchecker.Text = no;
                    }
                    if (delete_check.Checked == true)
                    {
                        deletechecker.Text = yes;
                    }
                    else if (delete_check.Checked == false)
                    {
                        deletechecker.Text = no;
                    }
                    byte[] ImageSave = null;

                    if (imagechecker != false)
                    {
                         byte[] imageBT;

                        FileStream fstream = new FileStream(this.path.Text, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fstream);
                        imageBT = br.ReadBytes((int)fstream.Length);

                        //FileStream FileSt = new FileStream(UserPicture, FileMode.Open, FileAccess.Read);
                        //BinaryReader ReaderBinary = new BinaryReader(FileSt);
                        //ImageSave = ReaderBinary.ReadBytes((int)FileSt.Length);
                    }
                    else if (imagechecker == false)
                    {
                        FileStream FileSt = new FileStream(DefaultPic, FileMode.Open, FileAccess.Read);
                        BinaryReader ReaderBinary = new BinaryReader(FileSt);
                        ImageSave = ReaderBinary.ReadBytes((int)FileSt.Length);
                    }
                    idtxt.Text = NumberRandom.ToString();
                    
                    MySqlConnection con = new MySqlConnection(myConnection);
                    con.Open();
                    MySqlCommand save = con.CreateCommand();
                    save.Connection = con;
                    save.CommandText = ("insert into employees (ID,firstname,lastname,username,password,gender,can_add,can_update,can_view,can_delete,status) values(@ID,@firstname, @lastname,@username,@password,@gender,@can_add,@can_update,@can_view,@can_delete,@status)");
                    save.Parameters.AddWithValue("@ID", NumberRandom);
                    save.Parameters.AddWithValue("@firstname", user_firstname.Text);
                    save.Parameters.AddWithValue("@lastname", user_lastname.Text);
                    save.Parameters.AddWithValue("@username", user_username.Text);
                    save.Parameters.AddWithValue("@password", user_password.Text);
                    save.Parameters.AddWithValue("@gender", truegender);
                    save.Parameters.AddWithValue("@can_add", addchecker.Text);
                    save.Parameters.AddWithValue("@can_update", updatechecker.Text);
                    save.Parameters.AddWithValue("@can_view", viewchecker.Text);
                    save.Parameters.AddWithValue("@can_delete", deletechecker.Text);
                    save.Parameters.AddWithValue("@status", "Offline");
                   // save.Parameters.Add(new MySqlParameter("@employee_image", user_picture.Image));
                    save.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New user added!");
                    MessageBox.Show("Your ID is " + idtxt.Text);
                    view();
                }
                else
                {
                    MessageBox.Show("Password does not match");
                    return;
                }
            }


            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }
        }
        public void updateuser()
        {
            string yes = "Allowed";
            string no = "Denied";

            if (user_repass.Text == user_password.Text)
            {
                if (add_check.Checked == true)
                {
                    addchecker.Text = yes;
                }
                else if (add_check.Checked == false)
                {
                    addchecker.Text = no;
                }
                if (update_check.Checked == true)
                {
                    updatechecker.Text = yes;
                }
                else if (update_check.Checked == false)
                {
                    updatechecker.Text = no;
                }
                if (view_check.Checked == true)
                {
                    viewchecker.Text = yes;
                }
                else if (view_check.Checked == false)
                {
                    viewchecker.Text = no;
                }
                if (delete_check.Checked == true)
                {
                    deletechecker.Text = yes;
                }
                else if (delete_check.Checked == false)
                {
                    deletechecker.Text = no;
                }

                byte[] ImageSave = null;

                FileStream FileSt = new FileStream(UserPicture, FileMode.Open, FileAccess.Read);
                BinaryReader ReaderBinary = new BinaryReader(FileSt);
                ImageSave = ReaderBinary.ReadBytes((int)FileSt.Length);

                DialogResult dr = MessageBox.Show("Are you sure you want to apply the new changes to this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (user_password.Text == user_repass.Text)
                    {

                        MySqlConnection connection = new MySqlConnection(myConnection);

                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        string query0 = "select * from employees where ID = '" + idtxt.Text + "'";
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
                            string query1 = "update employees set  firstname = '" + user_firstname.Text + "' , lastname = '" + user_lastname.Text + "' , username = '" + user_username.Text + "' , password = '" + user_password.Text + "' ,  status  = '" + "Offline" + "' where ID = '" + idtxt.Text + "' ";
                            command2.CommandText = query1;
                            command2.ExecuteNonQuery();
                            view();

                            MessageBox.Show("Update successfull!", "EDITION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (count > 1)
                        {
                            MessageBox.Show("Please Make Sure That You Don't Have A Duplicate Reservation", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Password does not match.");
                    return;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            switch (user_morph.Text)
            {
                case "Add":
                    adduser();
                    break;
                case "Update":
                    updateuser();
                    break;
                case "Done":
                    //userselectdelete();
                    break;

            }
        }

        private void user_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *gif; *bm";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                usermainpicture.Image = new Bitmap(open.FileName);
                // image file path
                path.Text = open.FileName;
            }





            //OpenFileDialog OpenFile = new OpenFileDialog();
            //OpenFile.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png";

            //if (OpenFile.ShowDialog() == DialogResult.OK)
            //{
            //    UserPicture = OpenFile.FileName.ToString();
            //    usermainpicture.ImageLocation = UserPicture;
            //    imagechecker = true;

            //}
        }

        private void accounts_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            user_morph.Text = "Update";
            int i = 0;

            ListViewItem qwe = accounts_list.SelectedItems[i];
            idtxt.Text = qwe.SubItems[0].Text;
            user_firstname.Text = qwe.SubItems[1].Text;
            user_lastname.Text = qwe.SubItems[2].Text;
            user_username.Text = qwe.SubItems[3].Text;
            user_password.Text = qwe.SubItems[4].Text;
            user_repass.Text = qwe.SubItems[4].Text;



            MySqlConnection conn = new MySqlConnection(myConnection);

            MySqlCommand copro3 = new MySqlCommand();
            conn.Open();
            copro3.Connection = conn;
            copro3.CommandText = "select * from employees where firstname = '" + qwe.SubItems[1].Text + "'";
            MySqlDataReader copro = copro3.ExecuteReader();

            while (copro.Read())
            {

                string repass = (copro["password"].ToString());
                string gender = (copro["gender"].ToString());
                string adder = (copro["can_add"].ToString());
                string updater = (copro["can_update"].ToString());
                string remover = (copro["can_delete"].ToString());
                string viewer = (copro["can_view"].ToString());

                addchecker.Text = adder;
                updatechecker.Text = updater;
                viewchecker.Text = viewer;
                deletechecker.Text = remover;


                if (gender == "Male")
                {
                    malebutton.Checked = true;
                }
                else if (gender == "Female")
                {
                    femalebutton.Checked = true;
                }



            }
            conn.Close();

            MySqlDataAdapter Adapter = new MySqlDataAdapter(copro3);
            DataSet SetDatas = new DataSet();
            Adapter.Fill(SetDatas);

            if (SetDatas.Tables[0].Rows.Count > 0)
            {
                MemoryStream MemeStream = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["employee_image"]);

                usermainpicture.Image = Image.FromStream(MemeStream);
            }
            if (addchecker.Text == "Allowed")
            {
                add_check.Checked = true;
            }
            else if (addchecker.Text == "Denied")
            {
                add_check.Checked = false;
            }
            if (updatechecker.Text == "Allowed")
            {
                update_check.Checked = true;
            }
            else if (updatechecker.Text == "Denied")
            {
                update_check.Checked = false;
            }
            if (viewchecker.Text == "Allowed")
            {
                view_check.Checked = true;
            }
            else if (viewchecker.Text == "Denied")
            {
                view_check.Checked = false;
            }
            if (deletechecker.Text == "Allowed")
            {
                delete_check.Checked = true;
            }
            else if (deletechecker.Text == "Denied")
            {
                delete_check.Checked = false;
            }
        }

        private void employees_Activated(object sender, EventArgs e)
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

        private void user_firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void user_lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

            // only allow one decimal point
            if (e.KeyChar == ' '
                && (sender as TextBox).Text.IndexOf(' ') > -1)
            {
                e.Handled = true;
            }
        }
    }
}

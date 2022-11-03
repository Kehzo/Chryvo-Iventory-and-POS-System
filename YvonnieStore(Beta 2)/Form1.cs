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
    public partial class Form1 : Form
    {
        string myConnection = "Server=localhost;Database= yvonniestores ;Uid=root;Password=";
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
            newcurrent_transactionnumber();
            //totalsalestoday();
            //loaduserpicture();
            zero();
            viewall();
            
            
        }


        public static Random rand = new Random();
        public static int NumberRandom = rand.Next(0, 69) + 80834;
        public void viewall()
        {
            product_picture.Image = null;
            itemnametxt.Text = "";
            quantitytxtbox.Text = "0";
            try
            {
                MySqlConnection connection = new MySqlConnection(myConnection);
                items_list.Items.Clear();
                connection.Close();

                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                string query = "select * from inventory where category = '" + category.Text + "' and product_unit = '" + unitz.Text + "'";
                command.CommandText = query;
                MySqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {

                    ListViewItem item = new ListViewItem(read["product_ID"].ToString());
                    item.SubItems.Add(read["product_name"].ToString());
                    item.SubItems.Add(read["product_flavor"].ToString());
                    item.SubItems.Add(read["product_price"].ToString());
                    item.SubItems.Add(read["product_stocks"].ToString());
                    item.SubItems.Add(read["product_unit"].ToString());


                    items_list.Items.Add(item);
                    items_list.FullRowSelect = true;


                }
                connection.Close();
               
            }

            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e);
            }

        }
        public void zero()
        {
            cashtxtbox.Text = "0.00";
            totaltxtbox.Text = "0.00";
            changetxtbox.Text = "0.00";
        }
        public void newcurrent_transactionnumber()
        {
            transaction_number.Text = NumberRandom.ToString();
        }
        private void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            
        }
        private void t_Tick(object sender, EventArgs e)
        {
            timenow_value.Text = DateTime.Now.ToLongTimeString();
        }

        private void Form1_Load(object sender, EventArgs e)
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
            usersinfo_timein.Text = timenow_value.Text;

            
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

            inventory b = new inventory();
            this.Hide();
            b.Show();
            b.usersinfo_username.Text = usersinfo_username.Text;
            b.usersinfo_firstname.Text = usersinfo_firstname.Text;
            b.usersinfo_lastname.Text = usersinfo_lastname.Text;
            //b.user_accounttype.Text = usersinfo_accessibility.Text;
            b.canadd.Checked = adder;
            b.canupdate.Checked = updater;
            b.canview.Checked = viewer;
            b.candelete.Checked = deleter;
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
        public void viewselected()
        {
            //if (items_food.Checked == true && items_combo.Text == "Junkfoods/Snax")
            //{
            MySqlConnection connection = new MySqlConnection(myConnection);
            items_list.Items.Clear();
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
                item.SubItems.Add(read["product_stocks"].ToString());
                



                items_list.Items.Add(item);
                items_list.FullRowSelect = true;


            }
            connection.Close();
        }

        private void items_list_DoubleClick(object sender, EventArgs e)
        {
            quantitytxtbox.Enabled = true;
            int i = 0;
            ListViewItem qwe = items_list.SelectedItems[i];
            itemnametxt.Text = qwe.SubItems[1].Text;

            addtocartbtn.Enabled = true;
            proccessbtn.Enabled = true;
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

            //MySqlDataAdapter Adapter = new MySqlDataAdapter(copro3);
            //DataSet SetDatas = new DataSet();
            //Adapter.Fill(SetDatas);

            //if (SetDatas.Tables[0].Rows.Count > 0)
            //{
            //    MemoryStream MemeStream = new MemoryStream((byte[])SetDatas.Tables[0].Rows[0]["product_picture"]);

            //    product_picture.Image = Image.FromStream(MemeStream);
            //}
        }

        private void proccessbtn_Click(object sender, EventArgs e)
        {
            quantitytxtbox.Text = "";
            quantitytxtbox.Enabled = false;
            cashtxtbox.Enabled = true;
            
           // cashtxtbox.Enabled = true;
            double total = 0;
            decimal valorAcumulado = 0;
            for (int i = 0; i < cart_list.Items.Count; i++)
            {
                total += double.Parse(cart_list.Items[i].SubItems[1].Text) * double.Parse(cart_list.Items[i].SubItems[3].Text);
            }
            Console.WriteLine(valorAcumulado); // 83
            //foreach (ListViewItem item in cart_list.Items)
            //{
            //    total = Convert.ToInt32(item.SubItems[1].Text) * Convert.ToInt32(item.SubItems[3].Text);
            //    for (int z = 1; z < item.SubItems.Count; z++)
            //    {
            //        string qwe = item.SubItems[z].Text;
            //        total = Convert.ToDouble(item.SubItems[1].Text) * Convert.ToDouble(item.SubItems[3].Text);

            //    }
            //}
            totaltxtbox.Text = total.ToString();
            verifybtn.Enabled = true;
        }

        private void plusbtn_Click(object sender, EventArgs e)
        {
            double shitint = Convert.ToDouble(quantitytxtbox.Text);

            shitint = shitint + 1;
            String madafaka = Convert.ToString(shitint);
            quantitytxtbox.Text = madafaka;

            string a = quantitytxtbox.Text;

            double z = Convert.ToDouble(a.ToString());

            if (z >= 100)
            {
                quantitytxtbox.Text = "99";
            }
            if (z <= 0)
            {
                quantitytxtbox.Text = "0";
            }
        }

        private void quantitytxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar)
                        && e.KeyChar != Convert.ToInt16(Keys.Back));
        }

        private void minusbtn_Click(object sender, EventArgs e)
        {
            double shitint = Convert.ToDouble(quantitytxtbox.Text);

            shitint = shitint - 1;
            String madafaka = Convert.ToString(shitint);
            quantitytxtbox.Text = madafaka;

            string a = quantitytxtbox.Text;

            double z = Convert.ToDouble(a.ToString());

            if (z >= 100)
            {
                quantitytxtbox.Text = "99";
            }
            if (z <= 0)
            {
                quantitytxtbox.Text = "0";
            }
        }

        private void addtocartbtn_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(quantitytxtbox.Text);


            if (a == 0)
            {
                MessageBox.Show("Enter how much quantity you want.");
            }

            else
            {
                int i = 0;
                ListViewItem qwe = items_list.SelectedItems[i];
                int z = Convert.ToInt32(qwe.SubItems[4].Text);
                if (a > z)
                {
                    if (z == 1)
                    {
                        MessageBox.Show("Selected item stock is not enough! " + z + " piece is available");
                        quantitytxtbox.Text = z.ToString();
                    }
                    else if (z == 0)
                    {
                        MessageBox.Show("Selected item is out of stocks.");
                        quantitytxtbox.Text = z.ToString();
                    }
                    else
                    {

                        MessageBox.Show("Selected items stocks are not enough! " + z + " pieces are available");
                        quantitytxtbox.Text = z.ToString();
                    }

                }
                else
                {
                    

                    ListViewItem asd = new ListViewItem(qwe.SubItems[1].Text);
                    asd.SubItems.Add(qwe.SubItems[3].Text);
                    asd.SubItems.Add(qwe.SubItems[5].Text);
                    asd.SubItems.Add(quantitytxtbox.Text);

                    cart_list.Items.Add(asd);

                    int y = Convert.ToInt32(quantitytxtbox.Text);
                    int x = Convert.ToInt32(qwe.SubItems[4].Text);

                    z = z - y;
                    qwe.SubItems[4].Text = z.ToString();


                   
                    
                }
            }
        }

        private void cashtxtbox_TextChanged(object sender, EventArgs e)
        {
            double q = 0;
            double w = 0;
            if (Double.TryParse(cashtxtbox.Text, out w) && Double.TryParse(totaltxtbox.Text, out q))
                changetxtbox.Text = (w - q).ToString();
        }

        private void cashtxtbox_KeyPress(object sender, KeyPressEventArgs e)
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
        public void change()
        {
            double a = 0;double b = 0;

            bool isAValid = Double.TryParse(cashtxtbox.Text, out a);
            bool isBValid = Double.TryParse(totaltxtbox.Text, out b);

            if (isAValid && isBValid)
                changetxtbox.Text = (a - b).ToString();
        }

        private void verifybtn_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(cashtxtbox.Text);
                double b = Convert.ToDouble(totaltxtbox.Text);
                if (string.IsNullOrEmpty(cashtxtbox.Text))
                {
                    MessageBox.Show("Customer has not yet paid their items");
                }
                else
                {
                    if (a < b) { MessageBox.Show("Customer has insufficient cash. Please pay the full total amount."); }
                    else
                    {
                        purchasebtn.Enabled = true;
                        change();
                        MessageBox.Show("Items and customers cash verified. Purchase now");
                        verifybtn.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cartitems()
        {
            
            foreach (ListViewItem ee in cart_list.Items)
            {
                
                for (int z = 1; z < ee.SubItems.Count; z++)
                {
                    itemss.Text = ee.SubItems[z].Text;
                }
            }
        }
        //public static void PrintReceiptForTransaction()
        //{
        //    PrintDocument recordDoc = new PrintDocument();

        //    recordDoc.DocumentName = "Customer Receipt";
        //    recordDoc.PrintPage += new PrintPageEventHandler(Form1.PrintReceiptPage); // function below
        //    recordDoc.PrintController = new StandardPrintController(); // hides status dialog popup
        //    // Comment if debugging 
        //    PrinterSettings ps = new PrinterSettings();
        //    ps.PrinterName = "EPSON TM-T20II Receipt";
        //    recordDoc.PrinterSettings = ps;
        //    recordDoc.Print();
        //    // --------------------------------------

        //    // Uncomment if debugging - shows dialog instead
        //    //PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
        //    //printPrvDlg.Document = recordDoc;
        //    //printPrvDlg.Width = 1200;
        //    //printPrvDlg.Height = 800;
        //    //printPrvDlg.ShowDialog();
        //    // --------------------------------------

        //    recordDoc.Dispose();
        //}
        //private static void PrintReceiptPage(object sender, PrintPageEventArgs e)
        //{
        //    float x = 10;
        //    float y = 5;
        //    float width = 270.0F; // max width I found through trial and error
        //    float height = 0F;

        //    Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
        //    Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
        //    SolidBrush drawBrush = new SolidBrush(Color.Black);

        //    // Set format of string.
        //    StringFormat drawFormatCenter = new StringFormat();
        //    drawFormatCenter.Alignment = StringAlignment.Center;
        //    StringFormat drawFormatLeft = new StringFormat();
        //    drawFormatLeft.Alignment = StringAlignment.Near;
        //    StringFormat drawFormatRight = new StringFormat();
        //    drawFormatRight.Alignment = StringAlignment.Far;

        //    // Draw string to screen.
        //    string text = "Yvonnie Store";
        //    e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

        //    text = "Items:";
        //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                 

        //    // ... and so on
        //}
        //public void itemsus()
        //{
        //     foreach (ListViewItem ee in cart_list.Items)
        //     {
        //        //product name
        //         itemsss.Text += ee.ToString() + " ";
        //         for (int z = 1; z < ee.SubItems.Count; z++)
        //         {
        //             itemsss.Text += ee.SubItems[z].Text + " ";
        //             //quantity
        //         }
        //     }
        //}
        private void purchasebtn_Click(object sender, EventArgs e)
        {
           // itemsus();

            string ded;
            //foreach (ListViewItem item in cart_list.Items)
            //{
            //    ded = item.SubItems[0].Text;
            //}
            //using (StreamWriter sw = new StreamWriter(@"C:\Users\eric agino valdez\Desktop\wew\Copro III Finals\YvonniesStorePOS\sales\items\items.txt"))
            //{
            //    sw.WriteLine(" ---------------------------------");
            //    sw.WriteLine("         ITEMS PURCHASED          ");
            //    sw.WriteLine("----------------------------------");
            //    sw.WriteLine("Items in this receipt is arranged by:");
            //    sw.WriteLine(">Item name");
            //    sw.WriteLine(">Item Price");
            //    sw.WriteLine(">Item Quantity");
            //    sw.WriteLine("Time purchased: " + DateTime.Now.ToString("hh:mm:ss tt"));
            //    foreach (ListViewItem ee in cart_list.Items)
            //    {
            //        sw.WriteLine("________________");
            //        sw.WriteLine(ee.Text);


            //        for (int z = 1; z < ee.SubItems.Count; z++)
            //        {
            //            sw.WriteLine(ee.SubItems[z].Text);

            //        }
            //    }
            //}
            // sales_total.Text = dd.ToString() + ".00";

            int i = 0;
            int transnumber = Convert.ToInt32(transaction_number.Text);
            string itemz;

            string quantity;
            double cashpaid = Convert.ToDouble(cashtxtbox.Text);
            double change = Convert.ToDouble(changetxtbox.Text);
            double total = Convert.ToDouble(totaltxtbox.Text);
            double lolol = Convert.ToDouble(total_sales.Text);
            double yas = lolol + cashpaid;

            string date = datenow_value.Text;
            string time = timenow_value.Text;


            string updatestocks;

            ListViewItem cart = cart_list.Items[i];
            itemz = cart.SubItems[0].Text;
            quantity = cart.SubItems[2].Text;

            MySqlConnection connection = new MySqlConnection(myConnection);
            connection.Open();

            MySqlCommand command = new MySqlCommand("insert into transaction (transaction_number,transaction_items,transaction_total,cash_paid,customer_change,transaction_date,customers_name,cashier) values ('" + transnumber + "', '" + itemz + "','" + total + "','" + cashpaid + "','" + change + "','" + date + "','" + "customer" + "','" + usersinfo_firstname.Text + "')", connection);
            command.ExecuteNonQuery();
            MySqlCommand command3 = connection.CreateCommand();
            total_sales.Text = yas.ToString();
            MessageBox.Show("Purchase success! Thank you for shopping", "SUCCESS!");
            ListViewItem qwe = items_list.SelectedItems[i];


            string query0 = "select * from inventory where product_id = '" + qwe.SubItems[0].Text + "'";
            command3.CommandText = query0;
            MySqlDataReader reader = command3.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count++;
            }

            foreach (ListViewItem item in items_list.Items)
            {


                if (count == 1)
                {
                    connection.Close();
                    connection.Open();
                    MySqlCommand command2 = connection.CreateCommand();
                    string query1 = "update inventory set product_stocks = '" + qwe.SubItems[4].Text + "' where product_ID = '" + qwe.SubItems[0].Text + "' ";
                    command2.CommandText = query1;
                    command2.ExecuteNonQuery();

                    viewall();
                    cart_list.Items.Clear();
                    totaltxtbox.Text = "0";
                    cashtxtbox.Text = "0";
                    changetxtbox.Text = "0";
                    purchasebtn.Enabled = false;
                }

            }
            MessageBox.Show("Stocks have been updated");
            int a = Convert.ToInt32(transaction_number.Text) + 1;
            transaction_number.Text = a.ToString();
            cashtxtbox.Enabled = false;
            addtocartbtn.Enabled = false;

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //user_picture.Image = Image.FromStream(login.ImageStream);
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

        private void perpieceradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per piece";
            viewall();
        }

        private void perkiloradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per kilo";
            viewall();
        }

        private void percansradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per cans";
            viewall();
        }

        private void perlinersradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per liners";
            viewall();
        }

        private void persachetradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per sachet";
            viewall();
        }

        private void perpairradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per pair";
            viewall();
        }

        private void perrimradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per rim";
            viewall();
        }

        private void perbagradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per bag";
            viewall();
        }

        private void perbarradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per bar";
            viewall();
        }

        private void perdozenradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per dozen";
            viewall();
        }

        private void percaseradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per case";
            viewall();
        }

        private void percutsradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per cuts";
            viewall();
        }

        private void perlongbarsradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per long bars";
            viewall();
        }

        private void perpacksradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per packs";
            viewall();
        }

        private void perrollsradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per rolls";
            viewall();
        }

        private void perpadsradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per pads";
            viewall();
        }

        private void pertieradio_CheckedChanged(object sender, EventArgs e)
        {
            unitz.Text = "per tie";
            viewall();
        }

        private void items_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void unitz_Click(object sender, EventArgs e)
        {

        }

    }
}

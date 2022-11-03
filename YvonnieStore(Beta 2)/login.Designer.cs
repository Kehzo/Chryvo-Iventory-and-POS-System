namespace YvonnieStore_Beta_2_
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.loginbutton = new System.Windows.Forms.Button();
            this.passwordtxtbox = new System.Windows.Forms.TextBox();
            this.usernametxtbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginbutton
            // 
            this.loginbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginbutton.Location = new System.Drawing.Point(193, 303);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(86, 56);
            this.loginbutton.TabIndex = 21;
            this.loginbutton.Text = "🔓";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // passwordtxtbox
            // 
            this.passwordtxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordtxtbox.Location = new System.Drawing.Point(176, 248);
            this.passwordtxtbox.MaxLength = 20;
            this.passwordtxtbox.Name = "passwordtxtbox";
            this.passwordtxtbox.PasswordChar = '*';
            this.passwordtxtbox.Size = new System.Drawing.Size(124, 29);
            this.passwordtxtbox.TabIndex = 20;
            this.passwordtxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordtxtbox_KeyDown);
            this.passwordtxtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordtxtbox_KeyPress);
            // 
            // usernametxtbox
            // 
            this.usernametxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernametxtbox.Location = new System.Drawing.Point(176, 204);
            this.usernametxtbox.Name = "usernametxtbox";
            this.usernametxtbox.Size = new System.Drawing.Size(124, 29);
            this.usernametxtbox.TabIndex = 18;
            this.usernametxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usernametxtbox_KeyDown);
            this.usernametxtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.usernametxtbox_KeyPress);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(420, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 44);
            this.button1.TabIndex = 22;
            this.button1.Text = "❌";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(469, 412);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.passwordtxtbox);
            this.Controls.Add(this.usernametxtbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loginbutton;
        private System.Windows.Forms.TextBox passwordtxtbox;
        private System.Windows.Forms.TextBox usernametxtbox;
        private System.Windows.Forms.Button button1;
    }
}
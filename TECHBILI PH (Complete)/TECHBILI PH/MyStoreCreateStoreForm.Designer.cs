namespace TECHBILI_PH
{
    partial class MyStoreCreateStoreForm
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
            this.cbxShowpassword = new System.Windows.Forms.CheckBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCreateStore = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStorename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContactno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSellername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxShowpassword
            // 
            this.cbxShowpassword.AutoSize = true;
            this.cbxShowpassword.ForeColor = System.Drawing.Color.White;
            this.cbxShowpassword.Location = new System.Drawing.Point(242, 246);
            this.cbxShowpassword.Name = "cbxShowpassword";
            this.cbxShowpassword.Size = new System.Drawing.Size(108, 19);
            this.cbxShowpassword.TabIndex = 23;
            this.cbxShowpassword.Text = "Show password";
            this.cbxShowpassword.UseVisualStyleBackColor = true;
            this.cbxShowpassword.CheckedChanged += new System.EventHandler(this.cbxShowpassword_CheckedChanged);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(197, 307);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 21;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCreateStore
            // 
            this.btnCreateStore.AutoSize = true;
            this.btnCreateStore.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCreateStore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateStore.ForeColor = System.Drawing.Color.White;
            this.btnCreateStore.Location = new System.Drawing.Point(195, 276);
            this.btnCreateStore.Name = "btnCreateStore";
            this.btnCreateStore.Size = new System.Drawing.Size(81, 25);
            this.btnCreateStore.TabIndex = 20;
            this.btnCreateStore.Text = "Create Store";
            this.btnCreateStore.UseVisualStyleBackColor = false;
            this.btnCreateStore.Click += new System.EventHandler(this.btnCreateStore_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(184, 212);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(166, 23);
            this.txtPassword.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(174, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Create Store";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(111, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Password";
            // 
            // txtStorename
            // 
            this.txtStorename.Location = new System.Drawing.Point(184, 183);
            this.txtStorename.Name = "txtStorename";
            this.txtStorename.Size = new System.Drawing.Size(166, 23);
            this.txtStorename.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(111, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Store name";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(184, 153);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(166, 23);
            this.txtAddress.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(111, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "Address";
            // 
            // txtContactno
            // 
            this.txtContactno.Location = new System.Drawing.Point(184, 124);
            this.txtContactno.Name = "txtContactno";
            this.txtContactno.ReadOnly = true;
            this.txtContactno.Size = new System.Drawing.Size(166, 23);
            this.txtContactno.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(111, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "Contact no.";
            // 
            // txtSellername
            // 
            this.txtSellername.Location = new System.Drawing.Point(184, 95);
            this.txtSellername.Name = "txtSellername";
            this.txtSellername.ReadOnly = true;
            this.txtSellername.Size = new System.Drawing.Size(166, 23);
            this.txtSellername.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(111, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 28;
            this.label6.Text = "Seller name";
            // 
            // MyStoreCreateStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(458, 379);
            this.Controls.Add(this.txtSellername);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContactno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxShowpassword);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCreateStore);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStorename);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MyStoreCreateStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyStoreCreateStoreForm";
            this.Load += new System.EventHandler(this.MyStoreCreateStoreForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cbxShowpassword;
        private Button btnBack;
        private Button btnCreateStore;
        private TextBox txtPassword;
        private Label label3;
        private Label label2;
        private TextBox txtStorename;
        private Label label1;
        private TextBox txtAddress;
        private Label label4;
        private TextBox txtContactno;
        private Label label5;
        private TextBox txtSellername;
        private Label label6;
    }
}
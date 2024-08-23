namespace TECHBILI_PH
{
    partial class MyStoreMainScreenForm
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
            this.lblStorename = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flpInventory = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProductSold = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnSearchItem = new System.Windows.Forms.Button();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.pboxItemPricture = new System.Windows.Forms.PictureBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtItemdescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtItemquantity = new System.Windows.Forms.TextBox();
            this.txtItemprice = new System.Windows.Forms.TextBox();
            this.txtItemname = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearTxt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pboxItemPricture)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStorename
            // 
            this.lblStorename.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStorename.ForeColor = System.Drawing.Color.White;
            this.lblStorename.Location = new System.Drawing.Point(12, 9);
            this.lblStorename.Name = "lblStorename";
            this.lblStorename.Size = new System.Drawing.Size(886, 32);
            this.lblStorename.TabIndex = 0;
            this.lblStorename.Text = "Store name";
            this.lblStorename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.LimeGreen;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(823, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Product sold";
            // 
            // flpInventory
            // 
            this.flpInventory.AutoScroll = true;
            this.flpInventory.Location = new System.Drawing.Point(12, 67);
            this.flpInventory.Name = "flpInventory";
            this.flpInventory.Size = new System.Drawing.Size(440, 254);
            this.flpInventory.TabIndex = 7;
            // 
            // flpProductSold
            // 
            this.flpProductSold.AutoScroll = true;
            this.flpProductSold.Location = new System.Drawing.Point(12, 375);
            this.flpProductSold.Name = "flpProductSold";
            this.flpProductSold.Size = new System.Drawing.Size(886, 266);
            this.flpProductSold.TabIndex = 8;
            // 
            // btnAddItem
            // 
            this.btnAddItem.AutoSize = true;
            this.btnAddItem.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(503, 238);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(87, 25);
            this.btnAddItem.TabIndex = 10;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSearchItem
            // 
            this.btnSearchItem.AutoSize = true;
            this.btnSearchItem.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchItem.ForeColor = System.Drawing.Color.White;
            this.btnSearchItem.Location = new System.Drawing.Point(596, 238);
            this.btnSearchItem.Name = "btnSearchItem";
            this.btnSearchItem.Size = new System.Drawing.Size(87, 25);
            this.btnSearchItem.TabIndex = 11;
            this.btnSearchItem.Text = "Search Item";
            this.btnSearchItem.UseVisualStyleBackColor = false;
            this.btnSearchItem.Click += new System.EventHandler(this.btnSearchItem_Click);
            // 
            // btnUpdateItem
            // 
            this.btnUpdateItem.AutoSize = true;
            this.btnUpdateItem.BackColor = System.Drawing.Color.LimeGreen;
            this.btnUpdateItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateItem.ForeColor = System.Drawing.Color.White;
            this.btnUpdateItem.Location = new System.Drawing.Point(503, 269);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(87, 25);
            this.btnUpdateItem.TabIndex = 12;
            this.btnUpdateItem.Text = "Update Item";
            this.btnUpdateItem.UseVisualStyleBackColor = false;
            this.btnUpdateItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.AutoSize = true;
            this.btnRemove.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(596, 269);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(87, 25);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "Remove Item";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // pboxItemPricture
            // 
            this.pboxItemPricture.BackColor = System.Drawing.Color.LimeGreen;
            this.pboxItemPricture.Location = new System.Drawing.Point(461, 67);
            this.pboxItemPricture.Name = "pboxItemPricture";
            this.pboxItemPricture.Size = new System.Drawing.Size(166, 137);
            this.pboxItemPricture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxItemPricture.TabIndex = 14;
            this.pboxItemPricture.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.AutoSize = true;
            this.btnUploadImage.BackColor = System.Drawing.Color.LimeGreen;
            this.btnUploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUploadImage.ForeColor = System.Drawing.Color.White;
            this.btnUploadImage.Location = new System.Drawing.Point(633, 67);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(93, 25);
            this.btnUploadImage.TabIndex = 15;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.UseVisualStyleBackColor = false;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(633, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Item name";
            // 
            // txtItemdescription
            // 
            this.txtItemdescription.Location = new System.Drawing.Point(732, 188);
            this.txtItemdescription.Multiline = true;
            this.txtItemdescription.Name = "txtItemdescription";
            this.txtItemdescription.Size = new System.Drawing.Size(166, 133);
            this.txtItemdescription.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(633, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Item price";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(633, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Item quantity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(633, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "Item description";
            // 
            // txtItemquantity
            // 
            this.txtItemquantity.Location = new System.Drawing.Point(732, 159);
            this.txtItemquantity.Name = "txtItemquantity";
            this.txtItemquantity.Size = new System.Drawing.Size(166, 23);
            this.txtItemquantity.TabIndex = 21;
            // 
            // txtItemprice
            // 
            this.txtItemprice.Location = new System.Drawing.Point(732, 130);
            this.txtItemprice.Name = "txtItemprice";
            this.txtItemprice.Size = new System.Drawing.Size(166, 23);
            this.txtItemprice.TabIndex = 22;
            // 
            // txtItemname
            // 
            this.txtItemname.Location = new System.Drawing.Point(732, 101);
            this.txtItemname.Name = "txtItemname";
            this.txtItemname.Size = new System.Drawing.Size(166, 23);
            this.txtItemname.TabIndex = 23;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Inventory";
            // 
            // btnClearTxt
            // 
            this.btnClearTxt.AutoSize = true;
            this.btnClearTxt.BackColor = System.Drawing.Color.LimeGreen;
            this.btnClearTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearTxt.ForeColor = System.Drawing.Color.White;
            this.btnClearTxt.Location = new System.Drawing.Point(596, 300);
            this.btnClearTxt.Name = "btnClearTxt";
            this.btnClearTxt.Size = new System.Drawing.Size(87, 25);
            this.btnClearTxt.TabIndex = 24;
            this.btnClearTxt.Text = "Clear ";
            this.btnClearTxt.UseVisualStyleBackColor = false;
            this.btnClearTxt.Click += new System.EventHandler(this.btnClearTxt_Click);
            // 
            // MyStoreMainScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(910, 653);
            this.Controls.Add(this.btnClearTxt);
            this.Controls.Add(this.txtItemname);
            this.Controls.Add(this.txtItemprice);
            this.Controls.Add(this.txtItemquantity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtItemdescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.pboxItemPricture);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpdateItem);
            this.Controls.Add(this.btnSearchItem);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.flpProductSold);
            this.Controls.Add(this.flpInventory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblStorename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MyStoreMainScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyStoreLogin";
            this.Load += new System.EventHandler(this.MyStoreMainScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxItemPricture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblStorename;
        private Button btnLogout;
        private Label label3;
        private FlowLayoutPanel flpInventory;
        private FlowLayoutPanel flpProductSold;
        private Button btnAddItem;
        private Button btnSearchItem;
        private Button btnUpdateItem;
        private Button btnRemove;
        private PictureBox pboxItemPricture;
        private Button btnUploadImage;
        private Label label1;
        private TextBox txtItemdescription;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtItemquantity;
        private TextBox txtItemprice;
        private TextBox txtItemname;
        private OpenFileDialog ofd;
        private Label label2;
        private Button btnClearTxt;
    }
}
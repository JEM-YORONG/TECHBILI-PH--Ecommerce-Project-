namespace TECHBILI_PH
{
    partial class ProductInfoForm
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
            this.pboxPicture = new System.Windows.Forms.PictureBox();
            this.btnBAck = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStorename = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxPicture
            // 
            this.pboxPicture.BackColor = System.Drawing.Color.LimeGreen;
            this.pboxPicture.Location = new System.Drawing.Point(14, 57);
            this.pboxPicture.Name = "pboxPicture";
            this.pboxPicture.Size = new System.Drawing.Size(264, 235);
            this.pboxPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxPicture.TabIndex = 0;
            this.pboxPicture.TabStop = false;
            // 
            // btnBAck
            // 
            this.btnBAck.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBAck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBAck.ForeColor = System.Drawing.Color.White;
            this.btnBAck.Location = new System.Drawing.Point(412, 374);
            this.btnBAck.Name = "btnBAck";
            this.btnBAck.Size = new System.Drawing.Size(75, 23);
            this.btnBAck.TabIndex = 1;
            this.btnBAck.Text = "Back";
            this.btnBAck.UseVisualStyleBackColor = false;
            this.btnBAck.Click += new System.EventHandler(this.btnBAck_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(314, 374);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(75, 23);
            this.btnAddToCart.TabIndex = 2;
            this.btnAddToCart.Text = "Add to cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(284, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(179, 32);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Product Name";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrice.ForeColor = System.Drawing.Color.White;
            this.lblPrice.Location = new System.Drawing.Point(329, 97);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(48, 25);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "PHP";
            // 
            // lblDescription
            // 
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(284, 143);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(504, 149);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Product Info";
            // 
            // lblStorename
            // 
            this.lblStorename.AutoSize = true;
            this.lblStorename.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStorename.ForeColor = System.Drawing.Color.White;
            this.lblStorename.Location = new System.Drawing.Point(14, 15);
            this.lblStorename.Name = "lblStorename";
            this.lblStorename.Size = new System.Drawing.Size(118, 30);
            this.lblStorename.TabIndex = 6;
            this.lblStorename.Text = "Storename";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(331, 336);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.ReadOnly = true;
            this.txtQuantity.Size = new System.Drawing.Size(36, 23);
            this.txtQuantity.TabIndex = 7;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(324, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Quantity";
            // 
            // btnMinus
            // 
            this.btnMinus.AutoSize = true;
            this.btnMinus.BackColor = System.Drawing.Color.LimeGreen;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMinus.Location = new System.Drawing.Point(301, 335);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(24, 25);
            this.btnMinus.TabIndex = 9;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.AutoSize = true;
            this.btnPlus.BackColor = System.Drawing.Color.LimeGreen;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlus.Location = new System.Drawing.Point(373, 335);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(25, 25);
            this.btnPlus.TabIndex = 10;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(287, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "PHP";
            // 
            // ProductInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(800, 421);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblStorename);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.btnBAck);
            this.Controls.Add(this.pboxPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductInfoForm";
            this.Load += new System.EventHandler(this.ProductInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pboxPicture;
        private Button btnBAck;
        private Button btnAddToCart;
        private Label lblName;
        private Label lblPrice;
        private Label lblDescription;
        private Label lblStorename;
        private TextBox txtQuantity;
        private Label label1;
        private Button btnMinus;
        private Button btnPlus;
        private Label label2;
    }
}
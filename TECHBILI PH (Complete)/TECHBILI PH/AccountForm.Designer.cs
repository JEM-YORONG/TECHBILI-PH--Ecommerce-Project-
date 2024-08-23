namespace TECHBILI_PH
{
    partial class AccountForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.pboxUserpicture = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flpHistory = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMyStore = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pboxUserpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(713, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pboxUserpicture
            // 
            this.pboxUserpicture.BackColor = System.Drawing.Color.LimeGreen;
            this.pboxUserpicture.Location = new System.Drawing.Point(12, 12);
            this.pboxUserpicture.Name = "pboxUserpicture";
            this.pboxUserpicture.Size = new System.Drawing.Size(124, 102);
            this.pboxUserpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxUserpicture.TabIndex = 4;
            this.pboxUserpicture.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(142, 21);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(101, 25);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Purchased History";
            // 
            // flpHistory
            // 
            this.flpHistory.AutoScroll = true;
            this.flpHistory.Location = new System.Drawing.Point(12, 156);
            this.flpHistory.Name = "flpHistory";
            this.flpHistory.Size = new System.Drawing.Size(776, 356);
            this.flpHistory.TabIndex = 11;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.LimeGreen;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(713, 41);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMyStore
            // 
            this.btnMyStore.BackColor = System.Drawing.Color.LimeGreen;
            this.btnMyStore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMyStore.ForeColor = System.Drawing.Color.White;
            this.btnMyStore.Location = new System.Drawing.Point(142, 62);
            this.btnMyStore.Name = "btnMyStore";
            this.btnMyStore.Size = new System.Drawing.Size(75, 23);
            this.btnMyStore.TabIndex = 15;
            this.btnMyStore.Text = "My Store";
            this.btnMyStore.UseVisualStyleBackColor = false;
            this.btnMyStore.Click += new System.EventHandler(this.btnMyStore_Click);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(800, 524);
            this.Controls.Add(this.btnMyStore);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.flpHistory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pboxUserpicture);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AccountForm";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxUserpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnBack;
        private PictureBox pboxUserpicture;
        private Label lblUsername;
        private Label label1;
        private FlowLayoutPanel flpHistory;
        private Button btnLogout;
        private Button btnMyStore;
    }
}
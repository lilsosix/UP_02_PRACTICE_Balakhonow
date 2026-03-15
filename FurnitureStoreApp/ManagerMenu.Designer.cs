namespace FurnitureStoreApp
{
    partial class ManagerMenu
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnOrdersList;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCurrentUser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnClients = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.btnOrdersList = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnClients.ForeColor = System.Drawing.Color.White;
            this.btnClients.Location = new System.Drawing.Point(55, 100);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(200, 40);
            this.btnClients.TabIndex = 0;
            this.btnClients.Text = "Клиенты";
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNewOrder.ForeColor = System.Drawing.Color.White;
            this.btnNewOrder.Location = new System.Drawing.Point(55, 150);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(200, 40);
            this.btnNewOrder.TabIndex = 1;
            this.btnNewOrder.Text = "Новый заказ";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);
            // 
            // btnOrdersList
            // 
            this.btnOrdersList.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOrdersList.ForeColor = System.Drawing.Color.White;
            this.btnOrdersList.Location = new System.Drawing.Point(55, 200);
            this.btnOrdersList.Name = "btnOrdersList";
            this.btnOrdersList.Size = new System.Drawing.Size(200, 40);
            this.btnOrdersList.TabIndex = 2;
            this.btnOrdersList.Text = "Учёт заказов";
            this.btnOrdersList.UseVisualStyleBackColor = false;
            this.btnOrdersList.Click += new System.EventHandler(this.btnOrdersList_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(55, 255);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 40);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход из учётной записи";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(310, 58);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCurrentUser.Location = new System.Drawing.Point(12, 62);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(280, 18);
            this.lblCurrentUser.TabIndex = 4;
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 315);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOrdersList);
            this.Controls.Add(this.btnNewOrder);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ManagerMenu";
            this.Text = "Меню менеджера";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagerMenu_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

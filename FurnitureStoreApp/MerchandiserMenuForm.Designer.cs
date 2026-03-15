namespace FurnitureStoreApp
{
    partial class MerchandiserMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox1 - оранжевая шапка
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(310, 60);
            this.pictureBox1.TabStop = false;

            // Логотип добавляем как дочерний элемент на шапку
            var pbLogo = new System.Windows.Forms.PictureBox();
            pbLogo.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            pbLogo.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pbLogo.Location = new System.Drawing.Point(-3, -2);
            pbLogo.Size = new System.Drawing.Size(75, 75);
            pbLogo.TabStop = false;
            this.pictureBox1.Controls.Add(pbLogo);

            // lblCurrentUser
            this.lblCurrentUser.AutoSize = false;
            this.lblCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCurrentUser.Location = new System.Drawing.Point(12, 65);
            this.lblCurrentUser.Size = new System.Drawing.Size(280, 18);
            this.lblCurrentUser.Text = "";
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnProducts
            this.btnProducts.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Location = new System.Drawing.Point(55, 100);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(200, 40);
            this.btnProducts.TabIndex = 0;
            this.btnProducts.Text = "Учёт товаров";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);

            // btnOrders
            this.btnOrders.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Location = new System.Drawing.Point(55, 150);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(200, 40);
            this.btnOrders.TabIndex = 1;
            this.btnOrders.Text = "Учёт заказов";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);

            // btnExit
            this.btnExit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(55, 210);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 40);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход из учётной записи";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // MerchandiserMenuForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 275);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MerchandiserMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню товароведа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MerchandiserMenuForm_FormClosing);
            this.Load += new System.EventHandler(this.MerchandiserMenuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

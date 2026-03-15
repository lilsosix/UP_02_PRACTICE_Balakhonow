namespace FurnitureStoreApp
{
    partial class AdminRestoreForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblInfo;
        private System.Windows.Forms.TextBox    txtLog;
        private System.Windows.Forms.Button     btnRestore;
        private System.Windows.Forms.Button     btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle    = new System.Windows.Forms.Label();
            this.lblInfo     = new System.Windows.Forms.Label();
            this.txtLog      = new System.Windows.Forms.TextBox();
            this.btnRestore  = new System.Windows.Forms.Button();
            this.btnBack     = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox2 — оранжевая шапка
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox2.Location  = new System.Drawing.Point(0, 0);
            this.pictureBox2.Size      = new System.Drawing.Size(520, 58);
            this.pictureBox2.TabStop   = false;

            // pictureBox1 — логотип
            this.pictureBox1.BackColor            = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage      = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location  = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Size      = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabStop   = false;

            // lblTitle
            this.lblTitle.BackColor  = System.Drawing.Color.Transparent;
            this.lblTitle.Font       = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor  = System.Drawing.Color.White;
            this.lblTitle.Location   = new System.Drawing.Point(85, 10);
            this.lblTitle.Size       = new System.Drawing.Size(420, 36);
            this.lblTitle.Text       = "Восстановление структуры БД";
            this.lblTitle.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;

            // lblInfo
            this.lblInfo.Location  = new System.Drawing.Point(15, 70);
            this.lblInfo.Size      = new System.Drawing.Size(490, 45);
            this.lblInfo.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblInfo.Text      = "Будут удалены все таблицы и созданы заново.\r\n" +
                                     "Данные пользователей, товаров и заказов будут потеряны.\r\n" +
                                     "Справочники (роли, статусы, доставка, категории) — заполнены заново.";
            this.lblInfo.ForeColor = System.Drawing.Color.DarkRed;

            // txtLog
            this.txtLog.Location   = new System.Drawing.Point(15, 125);
            this.txtLog.Size       = new System.Drawing.Size(490, 220);
            this.txtLog.Multiline  = true;
            this.txtLog.ReadOnly   = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Font       = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.BackColor  = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLog.ForeColor  = System.Drawing.Color.LightGreen;

            // btnRestore
            this.btnRestore.Location             = new System.Drawing.Point(15, 358);
            this.btnRestore.Size                 = new System.Drawing.Size(200, 32);
            this.btnRestore.Text                 = "Восстановить структуру БД";
            this.btnRestore.BackColor            = System.Drawing.Color.FromArgb(192, 64, 64);
            this.btnRestore.ForeColor            = System.Drawing.Color.White;
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click               += new System.EventHandler(this.btnRestore_Click);

            // btnBack
            this.btnBack.Location  = new System.Drawing.Point(390, 358);
            this.btnBack.Size      = new System.Drawing.Size(115, 32);
            this.btnBack.Text      = "\u2190 Назад";
            this.btnBack.Click    += new System.EventHandler(this.btnBack_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(520, 406);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Восстановление структуры БД";

            this.pictureBox2.Controls.Add(this.pictureBox1);
            this.pictureBox2.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBack);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

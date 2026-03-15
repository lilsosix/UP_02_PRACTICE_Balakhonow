using System;
namespace FurnitureStoreApp
{
    partial class AdminImportMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblUser;
        private System.Windows.Forms.Button     btnImport;
        private System.Windows.Forms.Button     btnRestore;
        private System.Windows.Forms.Button     btnExit;

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
            this.lblUser     = new System.Windows.Forms.Label();
            this.btnImport   = new System.Windows.Forms.Button();
            this.btnRestore  = new System.Windows.Forms.Button();
            this.btnExit     = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox2 — оранжевая шапка
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox2.Location  = new System.Drawing.Point(0, 0);
            this.pictureBox2.Size      = new System.Drawing.Size(400, 58);
            this.pictureBox2.TabStop   = false;

            // pictureBox1 — логотип
            this.pictureBox1.BackColor            = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage      = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location  = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Size      = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabStop   = false;

            // lblTitle
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font      = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(85, 10);
            this.lblTitle.Size      = new System.Drawing.Size(300, 36);
            this.lblTitle.Text      = "Администрирование БД";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblUser
            this.lblUser.Location  = new System.Drawing.Point(15, 68);
            this.lblUser.Size      = new System.Drawing.Size(370, 22);
            this.lblUser.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblUser.Text      = "Вы вошли как: Администратор БД (учётная запись по умолчанию)";
            this.lblUser.ForeColor = System.Drawing.Color.Gray;

            // btnImport
            this.btnImport.Location             = new System.Drawing.Point(40, 105);
            this.btnImport.Size                 = new System.Drawing.Size(320, 45);
            this.btnImport.Text                 = "📥  Импорт данных из CSV";
            this.btnImport.Font                 = new System.Drawing.Font("Arial", 11F);
            this.btnImport.BackColor            = System.Drawing.SystemColors.Highlight;
            this.btnImport.ForeColor            = System.Drawing.Color.White;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click               += new System.EventHandler(this.btnImport_Click);

            // btnRestore
            this.btnRestore.Location             = new System.Drawing.Point(40, 165);
            this.btnRestore.Size                 = new System.Drawing.Size(320, 45);
            this.btnRestore.Text                 = "🔧  Восстановление структуры БД";
            this.btnRestore.Font                 = new System.Drawing.Font("Arial", 11F);
            this.btnRestore.BackColor            = System.Drawing.Color.FromArgb(192, 100, 40);
            this.btnRestore.ForeColor            = System.Drawing.Color.White;
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click               += new System.EventHandler(this.btnRestore_Click);

            // btnExit
            this.btnExit.Location  = new System.Drawing.Point(40, 228);
            this.btnExit.Size      = new System.Drawing.Size(320, 32);
            this.btnExit.Text      = "Выйти из учётной записи";
            this.btnExit.Click    += new System.EventHandler(this.btnExit_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(400, 276);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Администрирование БД";

            this.pictureBox2.Controls.Add(this.pictureBox1);
            this.pictureBox2.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnExit);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

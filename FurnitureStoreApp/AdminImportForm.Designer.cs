namespace FurnitureStoreApp
{
    partial class AdminImportForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblTableTitle;
        private System.Windows.Forms.ComboBox   cmbTable;
        private System.Windows.Forms.Label      lblColumns;
        private System.Windows.Forms.Label      lblFileTitle;
        private System.Windows.Forms.Label      lblFile;
        private System.Windows.Forms.Button     btnChooseFile;
        private System.Windows.Forms.CheckBox   chkSkipHeader;
        private System.Windows.Forms.TextBox    txtLog;
        private System.Windows.Forms.Button     btnImport;
        private System.Windows.Forms.Button     btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox2    = new System.Windows.Forms.PictureBox();
            this.pictureBox1    = new System.Windows.Forms.PictureBox();
            this.lblTitle       = new System.Windows.Forms.Label();
            this.lblTableTitle  = new System.Windows.Forms.Label();
            this.cmbTable       = new System.Windows.Forms.ComboBox();
            this.lblColumns     = new System.Windows.Forms.Label();
            this.lblFileTitle   = new System.Windows.Forms.Label();
            this.lblFile        = new System.Windows.Forms.Label();
            this.btnChooseFile  = new System.Windows.Forms.Button();
            this.chkSkipHeader  = new System.Windows.Forms.CheckBox();
            this.txtLog         = new System.Windows.Forms.TextBox();
            this.btnImport      = new System.Windows.Forms.Button();
            this.btnBack        = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox2 — оранжевая шапка
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox2.Location  = new System.Drawing.Point(0, 0);
            this.pictureBox2.Size      = new System.Drawing.Size(560, 58);
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
            this.lblTitle.Font      = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(85, 10);
            this.lblTitle.Size      = new System.Drawing.Size(460, 36);
            this.lblTitle.Text      = "Импорт данных из CSV";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblTableTitle
            this.lblTableTitle.Location = new System.Drawing.Point(15, 70);
            this.lblTableTitle.Size     = new System.Drawing.Size(130, 22);
            this.lblTableTitle.Text     = "Таблица для импорта:";
            this.lblTableTitle.Font     = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // cmbTable
            this.cmbTable.Location        = new System.Drawing.Point(155, 70);
            this.cmbTable.Size            = new System.Drawing.Size(200, 22);
            this.cmbTable.DropDownStyle   = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.SelectedIndexChanged += new System.EventHandler(this.cmbTable_SelectedIndexChanged);

            // lblColumns — подсказка об ожидаемых колонках
            this.lblColumns.Location  = new System.Drawing.Point(15, 98);
            this.lblColumns.Size      = new System.Drawing.Size(530, 32);
            this.lblColumns.Font      = new System.Drawing.Font("Consolas", 8.5F);
            this.lblColumns.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblColumns.Text      = "";

            // lblFileTitle
            this.lblFileTitle.Location = new System.Drawing.Point(15, 138);
            this.lblFileTitle.Size     = new System.Drawing.Size(90, 22);
            this.lblFileTitle.Text     = "CSV файл:";
            this.lblFileTitle.Font     = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnChooseFile
            this.btnChooseFile.Location  = new System.Drawing.Point(110, 136);
            this.btnChooseFile.Size      = new System.Drawing.Size(130, 26);
            this.btnChooseFile.Text      = "Выбрать файл...";
            this.btnChooseFile.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnChooseFile.ForeColor = System.Drawing.Color.White;
            this.btnChooseFile.UseVisualStyleBackColor = false;
            this.btnChooseFile.Click    += new System.EventHandler(this.btnChooseFile_Click);

            // lblFile — имя выбранного файла
            this.lblFile.Location  = new System.Drawing.Point(250, 140);
            this.lblFile.Size      = new System.Drawing.Size(295, 18);
            this.lblFile.Text      = "Файл не выбран";
            this.lblFile.ForeColor = System.Drawing.Color.Gray;
            this.lblFile.Font      = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);

            // chkSkipHeader
            this.chkSkipHeader.Location = new System.Drawing.Point(15, 168);
            this.chkSkipHeader.Size     = new System.Drawing.Size(300, 20);
            this.chkSkipHeader.Text     = "Первая строка — заголовок (пропустить)";
            this.chkSkipHeader.Checked  = true;

            // txtLog — лог операции / предпросмотр
            this.txtLog.Location   = new System.Drawing.Point(15, 196);
            this.txtLog.Size       = new System.Drawing.Size(530, 190);
            this.txtLog.Multiline  = true;
            this.txtLog.ReadOnly   = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Font       = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.BackColor  = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLog.ForeColor  = System.Drawing.Color.LightGreen;

            // btnImport
            this.btnImport.Location             = new System.Drawing.Point(15, 398);
            this.btnImport.Size                 = new System.Drawing.Size(160, 32);
            this.btnImport.Text                 = "Импортировать";
            this.btnImport.BackColor            = System.Drawing.SystemColors.Highlight;
            this.btnImport.ForeColor            = System.Drawing.Color.White;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Enabled              = false;
            this.btnImport.Click               += new System.EventHandler(this.btnImport_Click);

            // btnBack
            this.btnBack.Location  = new System.Drawing.Point(430, 398);
            this.btnBack.Size      = new System.Drawing.Size(115, 32);
            this.btnBack.Text      = "\u2190 Назад";
            this.btnBack.Click    += new System.EventHandler(this.btnBack_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(560, 446);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Импорт данных из CSV";

            this.pictureBox2.Controls.Add(this.pictureBox1);
            this.pictureBox2.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.cmbTable);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lblFileTitle);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.chkSkipHeader);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBack);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

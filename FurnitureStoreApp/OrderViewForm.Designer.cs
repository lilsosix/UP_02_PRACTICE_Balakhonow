namespace FurnitureStoreApp
{
    partial class OrderViewForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblOrderIdTitle;
        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblOrderDateTitle;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblCompletionDateTitle;
        private System.Windows.Forms.Label lblCompletionDate;
        private System.Windows.Forms.Label lblClientTitle;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label  lblClientPhone;
        private System.Windows.Forms.Button btnShowPersonal;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.Label lblCostTitle;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblPrepaymentTitle;
        private System.Windows.Forms.Label lblPrepayment;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDeliveryTitle;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.Label lblManagerTitle;
        private System.Windows.Forms.Label lblManager;
        private System.Windows.Forms.Label lblChangeStatus;
        private System.Windows.Forms.ComboBox cmbChangeStatus;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnPrintCheck;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.PictureBox pictureBox1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblOrderIdTitle = new System.Windows.Forms.Label();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblOrderDateTitle = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblCompletionDateTitle = new System.Windows.Forms.Label();
            this.lblCompletionDate = new System.Windows.Forms.Label();
            this.lblClientTitle = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblClientPhone    = new System.Windows.Forms.Label();
            this.btnShowPersonal   = new System.Windows.Forms.Button();
            this.lblItemsTitle = new System.Windows.Forms.Label();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.lblCostTitle = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblPrepaymentTitle = new System.Windows.Forms.Label();
            this.lblPrepayment = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDeliveryTitle = new System.Windows.Forms.Label();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.lblManagerTitle = new System.Windows.Forms.Label();
            this.lblManager = new System.Windows.Forms.Label();
            this.lblChangeStatus = new System.Windows.Forms.Label();
            this.cmbChangeStatus = new System.Windows.Forms.ComboBox();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnPrintCheck = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();

            // pictureBox2 — оранжевая шапка
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Size = new System.Drawing.Size(620, 55);
            this.pictureBox2.TabStop = false;

            // pictureBox1 — логотип
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabStop = false;

            int c1 = 15, c2 = 190, y = 68, step = 26;

            // Номер заказа
            SetLbl(lblOrderIdTitle, "Номер заказа:", c1, y, true);
            SetLbl(lblOrderId, "—", c2, y, false); y += step;

            // Дата создания
            SetLbl(lblOrderDateTitle, "Дата создания:", c1, y, true);
            SetLbl(lblOrderDate, "—", c2, y, false); y += step;

            // Дата выполнения
            SetLbl(lblCompletionDateTitle, "Дата выполнения:", c1, y, true);
            SetLbl(lblCompletionDate, "—", c2, y, false); y += step;

            // Клиент
            SetLbl(lblClientTitle, "Клиент:", c1, y, true);
            SetLbl(lblClientName, "—", c2, y, false); y += step;

            // Телефон
            var lblPhoneTitle = new System.Windows.Forms.Label();
            SetLbl(lblPhoneTitle, "Телефон:", c1, y, true);
            SetLbl(lblClientPhone, "—", c2, y, false); y += step + 4;

            // btnShowPersonal
            this.btnShowPersonal.Location             = new System.Drawing.Point(c2, y - step * 2 - 4);
            this.btnShowPersonal.Size                 = new System.Drawing.Size(130, 22);
            this.btnShowPersonal.Text                 = "Показать данные";
            this.btnShowPersonal.Font                 = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.btnShowPersonal.BackColor            = System.Drawing.SystemColors.Highlight;
            this.btnShowPersonal.ForeColor            = System.Drawing.Color.White;
            this.btnShowPersonal.UseVisualStyleBackColor = false;
            this.btnShowPersonal.TabIndex             = 30;
            this.btnShowPersonal.Click               += new System.EventHandler(this.btnShowPersonal_Click);

            // Состав заказа — заголовок
            SetLbl(lblItemsTitle, "Состав заказа:", c1, y, true); y += 22;

            // DataGridView позиций
            this.dataGridViewItems.Location = new System.Drawing.Point(c1, y);
            this.dataGridViewItems.Size = new System.Drawing.Size(590, 150);
            this.dataGridViewItems.TabStop = false;
            y += 158;

            // Итого
            SetLbl(lblCostTitle, "Итого:", c1, y, true);
            SetLbl(lblCost, "—", c2, y, false); y += step;

            // Предоплата
            SetLbl(lblPrepaymentTitle, "Предоплата:", c1, y, true);
            SetLbl(lblPrepayment, "—", c2, y, false); y += step;

            // Статус
            SetLbl(lblStatusTitle, "Статус:", c1, y, true);
            SetLbl(lblStatus, "—", c2, y, false); y += step;

            // Доставка
            SetLbl(lblDeliveryTitle, "Доставка:", c1, y, true);
            SetLbl(lblDelivery, "—", c2, y, false); y += step;

            // Менеджер
            SetLbl(lblManagerTitle, "Менеджер:", c1, y, true);
            SetLbl(lblManager, "—", c2, y, false); y += step + 8;

            // Смена статуса
            this.lblChangeStatus.AutoSize = true;
            this.lblChangeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChangeStatus.Location = new System.Drawing.Point(c1, y);
            this.lblChangeStatus.Text = "Изменить статус:";

            this.cmbChangeStatus.Location = new System.Drawing.Point(185, y - 2);
            this.cmbChangeStatus.Size = new System.Drawing.Size(190, 22);

            this.btnChangeStatus.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnChangeStatus.ForeColor = System.Drawing.Color.White;
            this.btnChangeStatus.Location = new System.Drawing.Point(385, y - 4);
            this.btnChangeStatus.Size = new System.Drawing.Size(140, 27);
            this.btnChangeStatus.Text = "Сохранить статус";
            this.btnChangeStatus.UseVisualStyleBackColor = false;
            this.btnChangeStatus.Click += new System.EventHandler(this.btnChangeStatus_Click);
            y += 40;

            // Кнопка Назад
            this.btnBack.Location = new System.Drawing.Point(c1, y);
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.Text = "← Назад";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // Кнопка Печать чека
            this.btnPrintCheck.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPrintCheck.ForeColor = System.Drawing.Color.White;
            this.btnPrintCheck.Location = new System.Drawing.Point(210, y);
            this.btnPrintCheck.Size = new System.Drawing.Size(160, 30);
            this.btnPrintCheck.Text = "Печать чека (Word)";
            this.btnPrintCheck.UseVisualStyleBackColor = false;
            this.btnPrintCheck.Click += new System.EventHandler(this.btnPrintCheck_Click);

            y += 40;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, y);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Просмотр заказа";

            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblOrderIdTitle);
            this.Controls.Add(this.lblOrderId);
            this.Controls.Add(this.lblOrderDateTitle);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.lblCompletionDateTitle);
            this.Controls.Add(this.lblCompletionDate);
            this.Controls.Add(this.lblClientTitle);
            this.Controls.Add(this.lblClientName);
            this.Controls.Add(lblPhoneTitle);
            this.Controls.Add(this.lblClientPhone);
            this.Controls.Add(this.btnShowPersonal);
            this.Controls.Add(this.lblItemsTitle);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.lblCostTitle);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.lblPrepaymentTitle);
            this.Controls.Add(this.lblPrepayment);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDeliveryTitle);
            this.Controls.Add(this.lblDelivery);
            this.Controls.Add(this.lblManagerTitle);
            this.Controls.Add(this.lblManager);
            this.Controls.Add(this.lblChangeStatus);
            this.Controls.Add(this.cmbChangeStatus);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnPrintCheck);
            this.Controls.Add(this.btnBack);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetLbl(System.Windows.Forms.Label lbl, string text, int x, int y, bool bold)
        {
            lbl.AutoSize = true;
            lbl.Font = bold
                ? new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold)
                : new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }
    }
}
using System;
using System.Windows.Forms;
using System.Drawing;
namespace FurnitureStoreApp
{
    partial class ClientEditForm
    {
        private System.ComponentModel.IContainer components = null;

        // Элементы управления
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPassportSeries;
        private System.Windows.Forms.TextBox txtPassportNumber;
        private System.Windows.Forms.DateTimePicker dtpPassportIssue;
        private System.Windows.Forms.TextBox txtDivisionCode;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPassportSeries = new System.Windows.Forms.TextBox();
            this.txtPassportNumber = new System.Windows.Forms.TextBox();
            this.dtpPassportIssue = new System.Windows.Forms.DateTimePicker();
            this.txtDivisionCode = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPassportSeries = new System.Windows.Forms.Label();
            this.lblPassportNumber = new System.Windows.Forms.Label();
            this.lblPassportIssue = new System.Windows.Forms.Label();
            this.lblDivisionCode = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtPhone = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(156, 83);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(250, 20);
            this.txtFullName.TabIndex = 0;
            // 
            // txtPassportSeries
            // 
            this.txtPassportSeries.Location = new System.Drawing.Point(156, 143);
            this.txtPassportSeries.MaxLength = 4;
            this.txtPassportSeries.Name = "txtPassportSeries";
            this.txtPassportSeries.Size = new System.Drawing.Size(80, 20);
            this.txtPassportSeries.TabIndex = 2;
            // 
            // txtPassportNumber
            // 
            this.txtPassportNumber.Location = new System.Drawing.Point(156, 173);
            this.txtPassportNumber.MaxLength = 6;
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPassportNumber.TabIndex = 3;
            // 
            // dtpPassportIssue
            // 
            this.dtpPassportIssue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPassportIssue.Location = new System.Drawing.Point(156, 203);
            this.dtpPassportIssue.Name = "dtpPassportIssue";
            this.dtpPassportIssue.Size = new System.Drawing.Size(100, 20);
            this.dtpPassportIssue.TabIndex = 4;
            // 
            // txtDivisionCode
            // 
            this.txtDivisionCode.Location = new System.Drawing.Point(156, 233);
            this.txtDivisionCode.Name = "txtDivisionCode";
            this.txtDivisionCode.Size = new System.Drawing.Size(100, 20);
            this.txtDivisionCode.TabIndex = 5;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(156, 263);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(300, 60);
            this.txtAddress.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSave.Location = new System.Drawing.Point(260, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(136, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(18, 86);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(41, 13);
            this.lblFullName.TabIndex = 15;
            this.lblFullName.Text = "ФИО:*";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(18, 116);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(59, 13);
            this.lblPhone.TabIndex = 14;
            this.lblPhone.Text = "Телефон:*";
            // 
            // lblPassportSeries
            // 
            this.lblPassportSeries.AutoSize = true;
            this.lblPassportSeries.Location = new System.Drawing.Point(18, 146);
            this.lblPassportSeries.Name = "lblPassportSeries";
            this.lblPassportSeries.Size = new System.Drawing.Size(95, 13);
            this.lblPassportSeries.TabIndex = 13;
            this.lblPassportSeries.Text = "Серия паспорта:*";
            // 
            // lblPassportNumber
            // 
            this.lblPassportNumber.AutoSize = true;
            this.lblPassportNumber.Location = new System.Drawing.Point(18, 176);
            this.lblPassportNumber.Name = "lblPassportNumber";
            this.lblPassportNumber.Size = new System.Drawing.Size(98, 13);
            this.lblPassportNumber.TabIndex = 12;
            this.lblPassportNumber.Text = "Номер паспорта:*";
            // 
            // lblPassportIssue
            // 
            this.lblPassportIssue.AutoSize = true;
            this.lblPassportIssue.Location = new System.Drawing.Point(18, 206);
            this.lblPassportIssue.Name = "lblPassportIssue";
            this.lblPassportIssue.Size = new System.Drawing.Size(80, 13);
            this.lblPassportIssue.TabIndex = 11;
            this.lblPassportIssue.Text = "Дата выдачи:*";
            // 
            // lblDivisionCode
            // 
            this.lblDivisionCode.AutoSize = true;
            this.lblDivisionCode.Location = new System.Drawing.Point(18, 236);
            this.lblDivisionCode.Name = "lblDivisionCode";
            this.lblDivisionCode.Size = new System.Drawing.Size(114, 13);
            this.lblDivisionCode.TabIndex = 10;
            this.lblDivisionCode.Text = "Код подразделения:*";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(18, 266);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(41, 13);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "Адрес:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox2.Location = new System.Drawing.Point(-3, -2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(487, 57);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;


            // pictureBox1 — логотип
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabStop = false;            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(156, 113);
            this.txtPhone.Mask = "7(000)000-00-00";
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(148, 20);
            this.txtPhone.TabIndex = 17;
            this.txtPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // ClientEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 398);
            this.ControlBox = false;
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtDivisionCode);
            this.Controls.Add(this.dtpPassportIssue);
            this.Controls.Add(this.txtPassportNumber);
            this.Controls.Add(this.txtPassportSeries);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblDivisionCode);
            this.Controls.Add(this.lblPassportIssue);
            this.Controls.Add(this.lblPassportNumber);
            this.Controls.Add(this.lblPassportSeries);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblFullName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование клиента";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblFullName;
        private Label lblPhone;
        private Label lblPassportSeries;
        private Label lblPassportNumber;
        private Label lblPassportIssue;
        private Label lblDivisionCode;
        private Label lblAddress;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private MaskedTextBox txtPhone;
    }
}
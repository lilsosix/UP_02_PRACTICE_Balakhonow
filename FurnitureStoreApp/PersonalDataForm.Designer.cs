namespace FurnitureStoreApp
{
    partial class PersonalDataForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label      lblTitle;
        private System.Windows.Forms.Label      lblNameCaption;
        private System.Windows.Forms.Label      lblNameValue;
        private System.Windows.Forms.Label      lblPhoneCaption;
        private System.Windows.Forms.Label      lblPhoneValue;
        private System.Windows.Forms.Button     btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblPhoneCaption = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pictureBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox2.Controls.Add(this.pictureBox1);
            this.pictureBox2.Controls.Add(this.lblTitle);
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 58);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(85, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 36);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Персональные данные";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNameCaption
            // 
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNameCaption.Location = new System.Drawing.Point(15, 110);
            this.lblNameCaption.Name = "lblNameCaption";
            this.lblNameCaption.Size = new System.Drawing.Size(111, 16);
            this.lblNameCaption.TabIndex = 2;
            this.lblNameCaption.Text = "ФИО клиента:";
            // 
            // lblNameValue
            // 
            this.lblNameValue.AutoSize = true;
            this.lblNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNameValue.ForeColor = System.Drawing.Color.Navy;
            this.lblNameValue.Location = new System.Drawing.Point(15, 130);
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.Size = new System.Drawing.Size(16, 17);
            this.lblNameValue.TabIndex = 3;
            this.lblNameValue.Text = "—";
            // 
            // lblPhoneCaption
            // 
            this.lblPhoneCaption.AutoSize = true;
            this.lblPhoneCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPhoneCaption.Location = new System.Drawing.Point(15, 165);
            this.lblPhoneCaption.Name = "lblPhoneCaption";
            this.lblPhoneCaption.Size = new System.Drawing.Size(169, 16);
            this.lblPhoneCaption.TabIndex = 4;
            this.lblPhoneCaption.Text = "Контактный телефон:";
            // 
            // lblPhoneValue
            // 
            this.lblPhoneValue.AutoSize = true;
            this.lblPhoneValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPhoneValue.ForeColor = System.Drawing.Color.Navy;
            this.lblPhoneValue.Location = new System.Drawing.Point(15, 185);
            this.lblPhoneValue.Name = "lblPhoneValue";
            this.lblPhoneValue.Size = new System.Drawing.Size(16, 17);
            this.lblPhoneValue.TabIndex = 5;
            this.lblPhoneValue.Text = "—";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(140, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PersonalDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 278);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblNameCaption);
            this.Controls.Add(this.lblNameValue);
            this.Controls.Add(this.lblPhoneCaption);
            this.Controls.Add(this.lblPhoneValue);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PersonalDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Персональные данные";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pictureBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

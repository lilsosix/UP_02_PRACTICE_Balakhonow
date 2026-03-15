namespace FurnitureStoreApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCaptchaPrompt = new System.Windows.Forms.Label();
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.btnRefreshCaptcha = new System.Windows.Forms.Button();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.tmrBlock = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            this.SuspendLayout();

            // txtUsername
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.txtUsername.Location = new System.Drawing.Point(100, 276);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(282, 20);
            this.txtUsername.TabIndex = 0;

            // txtPassword
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.txtPassword.Location = new System.Drawing.Point(100, 348);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(282, 20);
            this.txtPassword.TabIndex = 1;

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblUsername.Location = new System.Drawing.Point(97, 255);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Имя пользователя:";

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblPassword.Location = new System.Drawing.Point(97, 327);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Пароль:";

            // btnLogin
            this.btnLogin.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(149, 401);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(185, 38);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblTitle.Location = new System.Drawing.Point(142, 152);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Авторизация";

            // button1
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.photo;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(12, 600);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnExit_Click);

            // pictureBox2
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox2.Location = new System.Drawing.Point(84, -9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(398, 90);
            this.pictureBox2.TabStop = false;

            // pictureBox1
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(95, 90);
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);

            // lblCaptchaPrompt
            this.lblCaptchaPrompt.AutoSize = true;
            this.lblCaptchaPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptchaPrompt.ForeColor = System.Drawing.Color.DarkRed;
            this.lblCaptchaPrompt.Location = new System.Drawing.Point(100, 452);
            this.lblCaptchaPrompt.Name = "lblCaptchaPrompt";
            this.lblCaptchaPrompt.Text = "Введите символы с картинки:";
            this.lblCaptchaPrompt.Visible = false;

            // pbCaptcha
            this.pbCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCaptcha.Location = new System.Drawing.Point(100, 476);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(200, 60);
            this.pbCaptcha.TabStop = false;
            this.pbCaptcha.Visible = false;

            // btnRefreshCaptcha
            this.btnRefreshCaptcha.Location = new System.Drawing.Point(308, 476);
            this.btnRefreshCaptcha.Name = "btnRefreshCaptcha";
            this.btnRefreshCaptcha.Size = new System.Drawing.Size(80, 28);
            this.btnRefreshCaptcha.Text = "\u21bb Обновить";
            this.btnRefreshCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnRefreshCaptcha.TabIndex = 5;
            this.btnRefreshCaptcha.Visible = false;
            this.btnRefreshCaptcha.Click += new System.EventHandler(this.btnRefreshCaptcha_Click);

            // txtCaptcha
            this.txtCaptcha.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.txtCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.txtCaptcha.Location = new System.Drawing.Point(100, 544);
            this.txtCaptcha.MaxLength = 4;
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(200, 22);
            this.txtCaptcha.TabIndex = 6;
            this.txtCaptcha.Visible = false;

            // lblError
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(100, 574);
            this.lblError.Name = "lblError";
            this.lblError.Text = "";
            this.lblError.Visible = false;

            // lblBlock
            this.lblBlock.AutoSize = true;
            this.lblBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblBlock.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblBlock.Location = new System.Drawing.Point(100, 592);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Text = "";
            this.lblBlock.Visible = false;

            // tmrBlock
            this.tmrBlock.Interval = 1000;
            this.tmrBlock.Tick += new System.EventHandler(this.tmrBlock_Tick);

            // LoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 662);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.lblBlock);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtCaptcha);
            this.Controls.Add(this.btnRefreshCaptcha);
            this.Controls.Add(this.pbCaptcha);
            this.Controls.Add(this.lblCaptchaPrompt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);

            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCaptchaPrompt;
        private System.Windows.Forms.PictureBox pbCaptcha;
        private System.Windows.Forms.Button btnRefreshCaptcha;
        private System.Windows.Forms.TextBox txtCaptcha;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Timer tmrBlock;
    }
}
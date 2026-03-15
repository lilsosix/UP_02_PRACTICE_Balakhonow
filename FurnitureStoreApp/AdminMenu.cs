using System;
using System.Drawing;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Меню администратора: переход к справочникам, управлению пользователями и учёту заказов.
    /// </summary>
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
            InactivityWatcher.Start(); // Issue #14
            ApplyStyle();
        }

        /// <summary>
        /// Применяет стиль и отображает ФИО/роль текущего пользователя на оранжевой полоске.
        /// </summary>
        private void ApplyStyle()
        {
            // Заголовок формы в оранжевой полоске
            Label lblTitle = new Label();
            lblTitle.Text = "Меню администратора";
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Arial", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.AutoSize = false;
            pictureBox2.Controls.Add(lblTitle);

            // Отображение текущего пользователя
            lblCurrentUser.Text = $"{CurrentSession.FullName}  |  {CurrentSession.RoleName}";

            // Стиль кнопок
            btn_Directories.BackColor = System.Drawing.SystemColors.Highlight;
            btn_Directories.ForeColor = Color.White;
            btn_Directories.UseVisualStyleBackColor = false;
            btn_Directories.Text = "Справочники";

            btnUsers.BackColor = System.Drawing.SystemColors.Highlight;
            btnUsers.ForeColor = Color.White;
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Text = "Пользователи";
        }

        private void btn_Directories_Click(object sender, EventArgs e)
        {
            DirectoriesForm form = new DirectoriesForm();
            form.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersListForm form = new OrdersListForm();
            form.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CurrentSession.Clear();
            InactivityWatcher.Stop();
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void AdminMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && CurrentSession.UserId != 0)
            {
                if (MessageBox.Show("Выйти из системы?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                CurrentSession.Clear();
                InactivityWatcher.Stop();
            new LoginForm().Show();
            }
        }
    }
}

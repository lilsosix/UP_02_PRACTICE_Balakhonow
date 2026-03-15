using System;
using System.Drawing;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Меню менеджера: клиенты, новый заказ, учёт заказов, выход.
    /// </summary>
    public partial class ManagerMenu : Form
    {
        public ManagerMenu()
        {
            InitializeComponent();
            InactivityWatcher.Start(); // Issue #14
            ApplyStyle();
        }

        private void ApplyStyle()
        {
            Label lbl = new Label();
            lbl.Text = "Меню менеджера";
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 18, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBox2.Controls.Add(lbl);

            lblCurrentUser.Text = $"{CurrentSession.FullName}  |  {CurrentSession.RoleName}";
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ClientsForm clientsForm = new ClientsForm();
            clientsForm.ShowDialog();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            NewOrderForm orderForm = new NewOrderForm();
            orderForm.ShowDialog();
        }

        private void btnOrdersList_Click(object sender, EventArgs e)
        {
            OrdersListForm orderForm = new OrdersListForm();
            orderForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CurrentSession.Clear();
            InactivityWatcher.Stop();
            LoginForm login = new LoginForm();
            this.Close();
            login.Show();
        }

        private void ManagerMenu_FormClosing(object sender, FormClosingEventArgs e)
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
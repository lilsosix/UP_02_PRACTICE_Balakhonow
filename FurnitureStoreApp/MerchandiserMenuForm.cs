using System;
using System.Drawing;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    public partial class MerchandiserMenuForm : Form
    {
        public MerchandiserMenuForm()
        {
            InitializeComponent();
            InactivityWatcher.Start(); // Issue #14
        }

        private void MerchandiserMenuForm_Load(object sender, EventArgs e)
        {
            // Заголовок на оранжевой шапке
            Label lbl = new Label();
            lbl.Text = "Меню товароведа";
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 20, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBox1.Controls.Add(lbl);

            lblCurrentUser.Text = $"{CurrentSession.FullName}  |  {CurrentSession.RoleName}";
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            MerchandiserForm form = new MerchandiserForm();
            form.ShowDialog();
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
            new LoginForm().Show();
            this.Close();
        }

        private void MerchandiserMenuForm_FormClosing(object sender, FormClosingEventArgs e)
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

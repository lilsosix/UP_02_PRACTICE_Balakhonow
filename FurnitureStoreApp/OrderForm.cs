using System;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            LoadOrdersData();
        }

        private void LoadOrdersData()
        {
            // Заглушка для данных заказов
            var orders = new[]
            {
                new { ID = 1, Client = "Смирнов А.В.", Date = "2024-01-15", Status = "Выполнен", Amount = 45000m },
                new { ID = 2, Client = "Ковалева Т.Н.", Date = "2024-01-18", Status = "Выполнен", Amount = 89000m },
                new { ID = 3, Client = "Морозов С.П.", Date = "2024-01-20", Status = "В обработке", Amount = 15000m }
            };

            dataGridViewOrders.DataSource = orders;
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создание нового заказа", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                MessageBox.Show("Редактирование заказа", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                MessageBox.Show("Изменение статуса заказа", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения статуса", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewClients_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Просмотр клиентов", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void ManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
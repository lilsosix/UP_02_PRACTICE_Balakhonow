using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class ClientsForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = DatabaseConnection.ConnectionString;
        private DataTable clientsTable;
        private DataView clientsView;
        private bool selectMode; // режим выбора клиента

        // Свойства для возврата выбранного клиента
        public int SelectedClientId { get; private set; }
        public string SelectedClientName { get; private set; }

        public ClientsForm(bool selectMode = false)
        {
            InitializeComponent();
            this.selectMode = selectMode;
            ConnectToDatabase();
            LoadClients();
            ConfigureDataGridView();

            // Настройка интерфейса в зависимости от режима
            if (selectMode)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnAddToOrder.Visible = false;
                btnSelect.Visible = true; // показываем кнопку выбора
                dataGridViewClients.CellDoubleClick += DataGridViewClients_CellDoubleClick;
                this.Text = "Выбор клиента";
            }
            else
            {
                btnSelect.Visible = false;
            }
        }

        private void ConnectToDatabase()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClients()
        {
            try
            {
                string query = @"
                    SELECT 
                        id,
                        full_name AS 'ФИО',
                        phone AS 'Телефон',
                        passport_series AS 'Серия паспорта',
                        passport_number AS 'Номер паспорта',
                        DATE_FORMAT(passport_issue_date, '%d.%m.%Y') AS 'Дата выдачи',
                        division_code AS 'Код подразделения',
                        address AS 'Адрес'
                    FROM clients
                    ORDER BY full_name";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                clientsTable = new DataTable();
                adapter.Fill(clientsTable);
                clientsView = new DataView(clientsTable);
                dataGridViewClients.DataSource = clientsView;

                if (dataGridViewClients.Columns.Contains("id"))
                    dataGridViewClients.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.AllowUserToAddRows = false;
            dataGridViewClients.ReadOnly = true;
            dataGridViewClients.MultiSelect = false;
        }

        // Фильтрация по телефону
        private void txtSearchPhone_TextChanged(object sender, EventArgs e) => ApplyFilters();

        // Фильтрация по ФИО
        private void txtSearchName_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void ApplyFilters()
        {
            if (clientsView == null) return;

            string phone = txtSearchPhone.Text.Trim().Replace("'", "''");
            string name  = txtSearchName.Text.Trim().Replace("'", "''");

            string filter = "";
            if (!string.IsNullOrEmpty(phone))
                filter = $"[Телефон] LIKE '{phone}%'";
            if (!string.IsNullOrEmpty(name))
            {
                string nameFilter = $"[ФИО] LIKE '%{name}%'";
                filter = string.IsNullOrEmpty(filter) ? nameFilter : filter + " AND " + nameFilter;
            }

            clientsView.RowFilter = filter;
            // Всегда держим сортировку по ФИО чтобы результаты были упорядочены
            clientsView.Sort = "[ФИО] ASC";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchPhone.Clear();
            txtSearchName.Clear();
            if (clientsView != null)
            {
                clientsView.RowFilter = "";
                clientsView.Sort = "[ФИО] ASC";
            }
        }

        // CRUD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (ClientEditForm editForm = new ClientEditForm(connection))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadClients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для редактирования.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int clientId = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells["id"].Value);
            using (ClientEditForm editForm = new ClientEditForm(connection, clientId))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                    LoadClients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int clientId = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells["id"].Value);
            string fullName = dataGridViewClients.SelectedRows[0].Cells["ФИО"].Value.ToString();

            DialogResult result = MessageBox.Show($"Удалить клиента {fullName}? Все связанные заказы также будут удалены (или нужно ограничить)?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string checkOrders = "SELECT COUNT(*) FROM orders WHERE client_id = @id";
                    MySqlCommand cmdCheck = new MySqlCommand(checkOrders, connection);
                    cmdCheck.Parameters.AddWithValue("@id", clientId);
                    int orderCount = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (orderCount > 0)
                    {
                        MessageBox.Show("Невозможно удалить клиента, у которого есть заказы. Сначала удалите заказы.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query = "DELETE FROM clients WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", clientId);
                    cmd.ExecuteNonQuery();
                    LoadClients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Кнопка "Добавить в заказ" (в обычном режиме)
        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для добавления в заказ.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int clientId = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells["id"].Value);
            string clientName = dataGridViewClients.SelectedRows[0].Cells["ФИО"].Value.ToString();
            string clientPhone = dataGridViewClients.SelectedRows[0].Cells["Телефон"].Value.ToString();

            // Открываем форму нового заказа (передаём ID клиента)
            NewOrderForm orderForm = new NewOrderForm(clientId); // конструктор нужно будет доработать
            orderForm.ShowDialog();
        }

        // Кнопка "Выбрать" (в режиме выбора)
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedClientId = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells["id"].Value);
            SelectedClientName = dataGridViewClients.SelectedRows[0].Cells["ФИО"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DataGridViewClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (selectMode && e.RowIndex >= 0)
            {
                SelectedClientId = Convert.ToInt32(dataGridViewClients.Rows[e.RowIndex].Cells["id"].Value);
                SelectedClientName = dataGridViewClients.Rows[e.RowIndex].Cells["ФИО"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClientsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
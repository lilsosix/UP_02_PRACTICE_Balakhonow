using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class DirectoriesForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = DatabaseConnection.ConnectionString;

        public DirectoriesForm()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadAllData();
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
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== Загрузка данных для всех вкладок ==========
        private void LoadAllData()
        {
            LoadRoles();
            LoadCategories();
            LoadMaterials();
            LoadStatuses();
            LoadDeliveryMethods();
        }

        private void ConfigureGrid(DataGridView dgv)
        {
            dgv.ReadOnly             = true;
            dgv.AllowUserToAddRows    = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect          = false;
            dgv.AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadRoles()
        {
            try
            {
                string query = "SELECT id, name FROM roles ORDER BY id";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvRoles.DataSource = dt;
                dgvRoles.Columns["id"].HeaderText   = "ID";
                dgvRoles.Columns["name"].HeaderText = "Название роли";
                dgvRoles.Columns["id"].Visible      = false;
                ConfigureGrid(dgvRoles);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                string query = "SELECT id, name FROM product_categories ORDER BY id";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvCategories.DataSource = dt;
                dgvCategories.Columns["id"].HeaderText   = "ID";
                dgvCategories.Columns["name"].HeaderText = "Название категории";
                dgvCategories.Columns["id"].Visible      = false;
                ConfigureGrid(dgvCategories);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaterials()
        {
            try
            {
                string query = "SELECT id, name FROM materials ORDER BY id";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvMaterials.DataSource = dt;
                dgvMaterials.Columns["id"].HeaderText   = "ID";
                dgvMaterials.Columns["name"].HeaderText = "Название материала";
                dgvMaterials.Columns["id"].Visible      = false;
                ConfigureGrid(dgvMaterials);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatuses()
        {
            try
            {
                string query = "SELECT id, name FROM order_statuses ORDER BY id";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvStatuses.DataSource = dt;
                dgvStatuses.Columns["id"].HeaderText   = "ID";
                dgvStatuses.Columns["name"].HeaderText = "Название статуса";
                dgvStatuses.Columns["id"].Visible      = false;
                ConfigureGrid(dgvStatuses);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDeliveryMethods()
        {
            try
            {
                string query = "SELECT id, name FROM delivery_methods ORDER BY id";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvDeliveryMethods.DataSource = dt;
                dgvDeliveryMethods.Columns["id"].HeaderText   = "ID";
                dgvDeliveryMethods.Columns["name"].HeaderText = "Название способа доставки";
                dgvDeliveryMethods.Columns["id"].Visible      = false;
                ConfigureGrid(dgvDeliveryMethods);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки способов доставки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== Общие методы для добавления/редактирования/удаления ==========

        private void AddRecord(string tableName, string columnName)
        {
            InputDialog dlg = new InputDialog($"Введите название для {columnName}:", "Добавление записи");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string newValue = dlg.InputText.Trim();
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    MessageBox.Show("Название не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string query = $"INSERT INTO {tableName} (name) VALUES (@name)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", newValue);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EditRecord(string tableName, string columnName, DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["id"].Value);
            string currentName = dgv.SelectedRows[0].Cells["name"].Value.ToString();

            InputDialog dlg = new InputDialog($"Измените название для {columnName}:", "Редактирование записи", currentName);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string newValue = dlg.InputText.Trim();
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    MessageBox.Show("Название не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newValue == currentName)
                    return;

                try
                {
                    string query = $"UPDATE {tableName} SET name = @name WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", newValue);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteRecord(string tableName, DataGridView dgv, string checkForeignKeyQuery = null)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["id"].Value);
            string name = dgv.SelectedRows[0].Cells["name"].Value.ToString();

            DialogResult result = MessageBox.Show($"Удалить запись \"{name}\"?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            try
            {
                if (!string.IsNullOrEmpty(checkForeignKeyQuery))
                {
                    MySqlCommand checkCmd = new MySqlCommand(checkForeignKeyQuery, connection);
                    checkCmd.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Невозможно удалить запись, так как она используется в других таблицах.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string query = $"DELETE FROM {tableName} WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== Обработчики для каждой вкладки ==========

        private void btnAddRole_Click(object sender, EventArgs e) => AddRecord("roles", "роль");
        private void btnEditRole_Click(object sender, EventArgs e) => EditRecord("roles", "роль", dgvRoles);
        private void btnDeleteRole_Click(object sender, EventArgs e) => DeleteRecord("roles", dgvRoles, "SELECT COUNT(*) FROM users WHERE role_id = @id");

        private void btnAddCategory_Click(object sender, EventArgs e) => AddRecord("product_categories", "категорию");
        private void btnEditCategory_Click(object sender, EventArgs e) => EditRecord("product_categories", "категорию", dgvCategories);
        private void btnDeleteCategory_Click(object sender, EventArgs e) => DeleteRecord("product_categories", dgvCategories, "SELECT COUNT(*) FROM products WHERE category_id = @id");

        private void btnAddMaterial_Click(object sender, EventArgs e) => AddRecord("materials", "материал");
        private void btnEditMaterial_Click(object sender, EventArgs e) => EditRecord("materials", "материал", dgvMaterials);
        private void btnDeleteMaterial_Click(object sender, EventArgs e) => DeleteRecord("materials", dgvMaterials, "SELECT COUNT(*) FROM products WHERE material_id = @id");

        private void btnAddStatus_Click(object sender, EventArgs e) => AddRecord("order_statuses", "статус");
        private void btnEditStatus_Click(object sender, EventArgs e) => EditRecord("order_statuses", "статус", dgvStatuses);
        private void btnDeleteStatus_Click(object sender, EventArgs e) => DeleteRecord("order_statuses", dgvStatuses, "SELECT COUNT(*) FROM orders WHERE status_id = @id");

        private void btnAddDelivery_Click(object sender, EventArgs e) => AddRecord("delivery_methods", "способ доставки");
        private void btnEditDelivery_Click(object sender, EventArgs e) => EditRecord("delivery_methods", "способ доставки", dgvDeliveryMethods);
        private void btnDeleteDelivery_Click(object sender, EventArgs e) => DeleteRecord("delivery_methods", dgvDeliveryMethods, "SELECT COUNT(*) FROM orders WHERE delivery_id = @id");

        private void btnMenu_Click(object sender, EventArgs e) => this.Close();

        private void DirectoriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
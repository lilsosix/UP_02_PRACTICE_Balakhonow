using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class AdminForm : Form
    {
        private MySqlConnection connection;
        private readonly string connectionString = DatabaseConnection.ConnectionString;

        public AdminForm()
        {
            InitializeComponent();
            ApplyStyle();
            ConnectToDatabase();
            LoadUsers();
        }

        private void ApplyStyle()
        {
            Label lbl = new Label();
            lbl.Text = "Пользователи";
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 22, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBox2.Controls.Add(lbl);
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
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                string query = @"
                    SELECT u.id, u.full_name AS 'ФИО', r.name AS 'Роль', u.login AS 'Логин'
                    FROM users u
                    LEFT JOIN roles r ON u.role_id = r.id
                    ORDER BY u.id";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridViewUsers.DataSource = dt;
                dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewUsers.AllowUserToAddRows = false;
                dataGridViewUsers.ReadOnly = true;
                dataGridViewUsers.MultiSelect = false;

                if (dataGridViewUsers.Columns.Contains("id"))
                    dataGridViewUsers.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (UserEditForm form = new UserEditForm(connection))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadUsers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["id"].Value);
            using (UserEditForm form = new UserEditForm(connection, userId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["id"].Value);

            if (userId == CurrentSession.UserId)
            {
                MessageBox.Show("Нельзя удалить самого себя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullName = dataGridViewUsers.SelectedRows[0].Cells["ФИО"].Value.ToString();
            if (MessageBox.Show($"Удалить пользователя «{fullName}»?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                MySqlCommand check = new MySqlCommand(
                    "SELECT COUNT(*) FROM orders WHERE user_id = @id", connection);
                check.Parameters.AddWithValue("@id", userId);
                if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Нельзя удалить пользователя, у которого есть заказы.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MySqlCommand cmd = new MySqlCommand("DELETE FROM users WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь удалён.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
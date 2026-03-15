using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class UserEditForm : Form
    {
        private readonly MySqlConnection connection;
        private readonly int? editingUserId;

        public UserEditForm(MySqlConnection conn, int? userId = null)
        {
            InitializeComponent();
            connection = conn;
            editingUserId = userId;

            ApplyStyle();
            LoadRoles();
            SetupValidation();

            // Ограничения длины согласно БД
            txtFullName.MaxLength = 150;
            txtLogin.MaxLength    = 50;
            txtPassword.MaxLength = 50;

            if (editingUserId.HasValue)
            {
                this.Text = "Редактирование пользователя";
                lblPasswordHint.Visible = true;
                LoadUserData(editingUserId.Value);
            }
            else
            {
                this.Text = "Добавление пользователя";
                lblPasswordHint.Visible = false;
                txtPassword.PasswordChar = '*';
            }
        }

        private void ApplyStyle()
        {
            Label lbl = new Label();
            lbl.Text = editingUserId.HasValue ? "Редактирование" : "Добавление";
            lbl.BackColor = System.Drawing.Color.Transparent;
            lbl.Font = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.White;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBoxHeader.Controls.Add(lbl);
        }

        private void LoadRoles()
        {
            try
            {
                var adapter = new MySqlDataAdapter("SELECT id, name FROM roles ORDER BY id", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbRole.DataSource = dt;
                cmbRole.DisplayMember = "name";
                cmbRole.ValueMember = "id";
                cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserData(int userId)
        {
            var cmd = new MySqlCommand("SELECT full_name, login, role_id FROM users WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("@id", userId);
            using (MySqlDataReader r = cmd.ExecuteReader())
            {
                if (r.Read())
                {
                    txtFullName.Text = r["full_name"].ToString();
                    txtLogin.Text = r["login"].ToString();
                    int roleId = Convert.ToInt32(r["role_id"]);
                    foreach (DataRowView item in cmbRole.Items)
                    {
                        if (Convert.ToInt32(item["id"]) == roleId)
                        {
                            cmbRole.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void SetFieldState(Control field, bool valid)
        {
            field.BackColor = valid
                ? System.Drawing.Color.FromArgb(230, 255, 230)
                : System.Drawing.Color.FromArgb(255, 220, 220);
        }

        private void SetupValidation()
        {
            // ФИО: только русские буквы и пробелы
            txtFullName.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) &&
                    !Regex.IsMatch(e.KeyChar.ToString(), @"[А-Яа-яёЁ\s]"))
                    e.Handled = true;
            };
            txtFullName.TextChanged += (s, e) =>
                SetFieldState(txtFullName,
                    txtFullName.Text.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length >= 2);

            // Логин: только латиница, цифры, _  — realtime подсветка
            txtLogin.TextChanged += (s, e) =>
                SetFieldState(txtLogin,
                    txtLogin.Text.Length > 0 && Regex.IsMatch(txtLogin.Text.Trim(), @"^[a-zA-Z0-9_]+$"));
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО пользователя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus(); return false;
            }
            if (txtFullName.Text.Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
            {
                MessageBox.Show("ФИО должно содержать минимум имя и фамилию.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus(); return false;
            }
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин пользователя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus(); return false;
            }
            if (!Regex.IsMatch(txtLogin.Text.Trim(), @"^[a-zA-Z0-9_]+$"))
            {
                MessageBox.Show("Логин может содержать только латинские буквы, цифры и '_'.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus(); return false;
            }
            if (!editingUserId.HasValue && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль или сгенерируйте его.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus(); return false;
            }
            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите роль пользователя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus(); return false;
            }
            return true;
        }

        private bool IsLoginDuplicate(string login, int? excludeId = null)
        {
            string query = excludeId.HasValue
                ? "SELECT COUNT(*) FROM users WHERE login = @login AND id != @id"
                : "SELECT COUNT(*) FROM users WHERE login = @login";
            var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", login);
            if (excludeId.HasValue) cmd.Parameters.AddWithValue("@id", excludeId.Value);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            string fullName = txtFullName.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;
            int roleId = Convert.ToInt32((cmbRole.SelectedItem as DataRowView)["id"]);

            try
            {
                if (!editingUserId.HasValue)
                {
                    if (IsLoginDuplicate(login))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string hash = PasswordHelper.HashPassword(password);
                    var cmd = new MySqlCommand(
                        "INSERT INTO users (full_name, role_id, login, password) VALUES (@fn, @role, @login, @pwd)",
                        connection);
                    cmd.Parameters.AddWithValue("@fn", fullName);
                    cmd.Parameters.AddWithValue("@role", roleId);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pwd", hash);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пользователь успешно добавлен.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (IsLoginDuplicate(login, editingUserId))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MySqlCommand cmd;
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        string hash = PasswordHelper.HashPassword(password);
                        cmd = new MySqlCommand(
                            "UPDATE users SET full_name=@fn, role_id=@role, login=@login, password=@pwd WHERE id=@id",
                            connection);
                        cmd.Parameters.AddWithValue("@pwd", hash);
                    }
                    else
                    {
                        cmd = new MySqlCommand(
                            "UPDATE users SET full_name=@fn, role_id=@role, login=@login WHERE id=@id",
                            connection);
                    }
                    cmd.Parameters.AddWithValue("@fn", fullName);
                    cmd.Parameters.AddWithValue("@role", roleId);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@id", editingUserId.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные пользователя обновлены.", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            var sb = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
                sb.Append(chars[rnd.Next(chars.Length)]);
            string pwd = sb.ToString();
            txtPassword.Text = pwd;
            txtPassword.PasswordChar = '\0';
            MessageBox.Show($"Сгенерированный пароль: {pwd}\nЗапомните или скопируйте его.",
                "Пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
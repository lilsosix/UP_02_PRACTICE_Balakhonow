using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class ClientEditForm : Form
    {
        private MySqlConnection connection;
        private int? clientId;

        public ClientEditForm(MySqlConnection conn, int? id = null)
        {
            InitializeComponent();
            connection = conn;
            clientId = id;

            this.Text = clientId.HasValue ? "Редактирование клиента" : "Новый клиент";

            // Ограничения длины согласно БД
            txtFullName.MaxLength       = 150;
            txtPassportSeries.MaxLength = 4;
            txtPassportNumber.MaxLength = 6;
            txtDivisionCode.MaxLength   = 10;
            txtAddress.MaxLength        = 255;

            if (clientId.HasValue)
                LoadClientData();

            AttachValidationEvents();
            txtPhone.HidePromptOnLeave = false;
            txtPhone.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        // Подсвечивает поле красным если значение некорректно, зелёным если корректно
        private void SetFieldState(Control field, bool valid)
        {
            field.BackColor = valid
                ? System.Drawing.Color.FromArgb(230, 255, 230)   // светло-зелёный
                : System.Drawing.Color.FromArgb(255, 220, 220);  // светло-красный
        }

        private void AttachValidationEvents()
        {
            // ФИО: только русские буквы и пробелы
            txtFullName.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), @"[А-Яа-яёЁ\s]"))
                    e.Handled = true;
            };
            txtFullName.TextChanged += (s, e) =>
                SetFieldState(txtFullName, txtFullName.Text.Trim().Length >= 2);

            // Серия паспорта: только цифры, ровно 4
            txtPassportSeries.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
            txtPassportSeries.TextChanged += (s, e) =>
                SetFieldState(txtPassportSeries, txtPassportSeries.Text.Length == 4);

            // Номер паспорта: только цифры, ровно 6
            txtPassportNumber.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
            txtPassportNumber.TextChanged += (s, e) =>
                SetFieldState(txtPassportNumber, txtPassportNumber.Text.Length == 6);

            // Код подразделения: только цифры, ровно 10
            txtDivisionCode.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
            txtDivisionCode.TextChanged += (s, e) =>
                SetFieldState(txtDivisionCode, txtDivisionCode.Text.Length == 10);

            // Адрес: русские буквы, цифры, пробелы, точка, запятая, дефис (необязательное поле)
            txtAddress.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), @"[А-Яа-яёЁ0-9\s\.,\-/]"))
                    e.Handled = true;
            };
        }

        private void LoadClientData()
        {
            try
            {
                string query = "SELECT full_name, phone, passport_series, passport_number, passport_issue_date, division_code, address FROM clients WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", clientId.Value);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtFullName.Text = reader["full_name"].ToString();
                        // Загружаем телефон как есть (11 цифр) – маска сама отформатирует
                        txtPhone.Text = reader["phone"].ToString();
                        txtPassportSeries.Text = reader["passport_series"].ToString();
                        txtPassportNumber.Text = reader["passport_number"].ToString();
                        if (reader["passport_issue_date"] != DBNull.Value)
                            dtpPassportIssue.Value = Convert.ToDateTime(reader["passport_issue_date"]);
                        else
                            dtpPassportIssue.Value = DateTime.Now;
                        txtDivisionCode.Text = reader["division_code"].ToString();
                        txtAddress.Text = reader["address"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            // Извлекаем только цифры из телефона (маска может добавлять скобки, дефисы)
            string phoneDigits = Regex.Replace(txtPhone.Text, @"\D", "");
            if (phoneDigits.Length != 11)
            {
                MessageBox.Show("Телефон должен содержать 11 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassportSeries.Text) || txtPassportSeries.Text.Length != 4)
            {
                MessageBox.Show("Серия паспорта должна содержать 4 цифры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassportSeries.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassportNumber.Text) || txtPassportNumber.Text.Length != 6)
            {
                MessageBox.Show("Номер паспорта должен содержать 6 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassportNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDivisionCode.Text) || txtDivisionCode.Text.Length != 10)
            {
                MessageBox.Show("Код подразделения должен содержать 10 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDivisionCode.Focus();
                return;
            }

            try
            {
                string query;
                MySqlCommand cmd;

                if (clientId.HasValue)
                {
                    // Проверка уникальности телефона (исключая текущего клиента)
                    string checkPhone = "SELECT COUNT(*) FROM clients WHERE phone = @phone AND id != @id";
                    MySqlCommand phoneCheck = new MySqlCommand(checkPhone, connection);
                    phoneCheck.Parameters.AddWithValue("@phone", phoneDigits);
                    phoneCheck.Parameters.AddWithValue("@id", clientId.Value);
                    if (Convert.ToInt32(phoneCheck.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Клиент с таким номером телефона уже зарегистрирован.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Проверка уникальности паспорта (серия + номер)
                    string checkPassport = "SELECT COUNT(*) FROM clients WHERE passport_series = @ps AND passport_number = @pn AND id != @id";
                    MySqlCommand passportCheck = new MySqlCommand(checkPassport, connection);
                    passportCheck.Parameters.AddWithValue("@ps", txtPassportSeries.Text.Trim());
                    passportCheck.Parameters.AddWithValue("@pn", txtPassportNumber.Text.Trim());
                    passportCheck.Parameters.AddWithValue("@id", clientId.Value);
                    if (Convert.ToInt32(passportCheck.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Клиент с такими паспортными данными уже зарегистрирован.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    query = @"
                        UPDATE clients 
                        SET full_name = @full_name,
                            phone = @phone,
                            passport_series = @passport_series,
                            passport_number = @passport_number,
                            passport_issue_date = @passport_issue_date,
                            division_code = @division_code,
                            address = @address
                        WHERE id = @id";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", clientId.Value);
                }
                else
                {
                    // Проверка уникальности телефона при добавлении
                    string checkPhone2 = "SELECT COUNT(*) FROM clients WHERE phone = @phone";
                    MySqlCommand phoneCheck2 = new MySqlCommand(checkPhone2, connection);
                    phoneCheck2.Parameters.AddWithValue("@phone", phoneDigits);
                    if (Convert.ToInt32(phoneCheck2.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Клиент с таким номером телефона уже зарегистрирован.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Проверка уникальности паспорта при добавлении
                    string checkPassport2 = "SELECT COUNT(*) FROM clients WHERE passport_series = @ps AND passport_number = @pn";
                    MySqlCommand passportCheck2 = new MySqlCommand(checkPassport2, connection);
                    passportCheck2.Parameters.AddWithValue("@ps", txtPassportSeries.Text.Trim());
                    passportCheck2.Parameters.AddWithValue("@pn", txtPassportNumber.Text.Trim());
                    if (Convert.ToInt32(passportCheck2.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Клиент с такими паспортными данными уже зарегистрирован.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    query = @"
                        INSERT INTO clients (full_name, phone, passport_series, passport_number, passport_issue_date, division_code, address)
                        VALUES (@full_name, @phone, @passport_series, @passport_number, @passport_issue_date, @division_code, @address)";
                    cmd = new MySqlCommand(query, connection);
                }

                cmd.Parameters.AddWithValue("@full_name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", phoneDigits); // сохраняем только цифры
                cmd.Parameters.AddWithValue("@passport_series", txtPassportSeries.Text.Trim());
                cmd.Parameters.AddWithValue("@passport_number", txtPassportNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@passport_issue_date", dtpPassportIssue.Value.Date);
                cmd.Parameters.AddWithValue("@division_code", txtDivisionCode.Text.Trim());
                cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(txtAddress.Text) ? DBNull.Value : (object)txtAddress.Text.Trim());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Данные успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
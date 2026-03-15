using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class AdminImportForm : Form
    {
        private string connectionString = DatabaseConnection.ConnectionString;
        private string csvFilePath = "";

        // Таблица → список колонок (без AUTO_INCREMENT id)
        private static readonly Dictionary<string, string[]> TableColumns = new Dictionary<string, string[]>
        {
            ["roles"]             = new[] { "name" },
            ["product_categories"]= new[] { "name" },
            ["materials"]         = new[] { "name" },
            ["order_statuses"]    = new[] { "name" },
            ["delivery_methods"]  = new[] { "name" },
            ["users"]             = new[] { "full_name", "login", "password", "role_id" },
            ["clients"]           = new[] { "full_name", "phone", "passport_series",
                                            "passport_number", "passport_issue_date",
                                            "division_code", "address" },
            ["products"]          = new[] { "article", "name", "category_id", "material_id",
                                            "dimensions", "price", "description" },
            ["orders"]            = new[] { "order_date", "completion_date", "client_id",
                                            "status_id", "delivery_id", "user_id",
                                            "prepayment", "total_cost" },
            ["order_items"]       = new[] { "order_id", "product_id", "quantity", "price", "cost" },
        };

        public AdminImportForm()
        {
            InitializeComponent();
            LoadTableList();
        }

        private void LoadTableList()
        {
            cmbTable.Items.Clear();
            foreach (var t in TableColumns.Keys)
                cmbTable.Items.Add(t);
            cmbTable.SelectedIndex = 0;
        }

        private void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateColumnsInfo();
            ClearFileSelection();
        }

        private void UpdateColumnsInfo()
        {
            if (cmbTable.SelectedItem == null) return;
            string table = cmbTable.SelectedItem.ToString();
            string[] cols = TableColumns[table];
            lblColumns.Text = "Ожидаемые колонки (" + cols.Length + "): " + string.Join(", ", cols);
        }

        private void ClearFileSelection()
        {
            csvFilePath = "";
            lblFile.Text = "Файл не выбран";
            lblFile.ForeColor = System.Drawing.Color.Gray;
            btnImport.Enabled = false;
        }

        // ── ISSUE #4: выбор файла через OpenFileDialog ─────────────────
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title  = "Выберите CSV файл";
                dlg.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                csvFilePath = dlg.FileName;
                lblFile.Text = Path.GetFileName(csvFilePath);
                lblFile.ForeColor = System.Drawing.Color.DarkGreen;

                // Превью первых 5 строк
                PreviewFile();

                btnImport.Enabled = true;
            }
        }

        private void PreviewFile()
        {
            txtLog.Clear();
            try
            {
                var lines = File.ReadAllLines(csvFilePath, System.Text.Encoding.UTF8);
                txtLog.AppendText("── Предпросмотр файла (первые 5 строк) ──\r\n");
                foreach (var line in lines.Take(5))
                    txtLog.AppendText(line + "\r\n");
                txtLog.AppendText("── Всего строк в файле: " + lines.Length + " ──\r\n");
            }
            catch (Exception ex)
            {
                txtLog.AppendText("Ошибка чтения файла: " + ex.Message + "\r\n");
            }
        }

        // ── ISSUE #4: валидация количества колонок ─────────────────────
        private bool ValidateCsvColumns(string[] lines, string[] expectedCols, out string errorMessage)
        {
            errorMessage = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var parts = SplitCsvLine(lines[i]);
                if (parts.Length != expectedCols.Length)
                {
                    errorMessage = string.Format(
                        "Строка {0}: найдено {1} значений, ожидается {2}.\r\n" +
                        "Содержимое строки: {3}",
                        i + 1, parts.Length, expectedCols.Length, lines[i]);
                    return false;
                }
            }
            return true;
        }

        // ── ISSUE #5: импорт строк в БД ────────────────────────────────
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(csvFilePath) || cmbTable.SelectedItem == null) return;

            string   table    = cmbTable.SelectedItem.ToString();
            string[] cols     = TableColumns[table];
            string[] lines;

            try
            {
                lines = File.ReadAllLines(csvFilePath, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Пропускаем пустые строки
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

            // Если первая строка — заголовок (содержит имена колонок) — пропускаем
            bool skipHeader = chkSkipHeader.Checked;
            string[] dataLines = skipHeader ? lines.Skip(1).ToArray() : lines;

            if (dataLines.Length == 0)
            {
                MessageBox.Show("Файл не содержит данных для импорта.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ── Валидация количества колонок во всех строках ──────────
            if (!ValidateCsvColumns(dataLines, cols, out string validationError))
            {
                txtLog.AppendText("\r\n✘ ОШИБКА ВАЛИДАЦИИ:\r\n" + validationError + "\r\n");
                MessageBox.Show(
                    "Файл содержит некорректные данные:\r\n\r\n" + validationError +
                    "\r\n\r\nИмпорт отменён.",
                    "Ошибка структуры CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ── Импорт ────────────────────────────────────────────────
            txtLog.AppendText("\r\n── Начало импорта в таблицу \"" + table + "\" ──\r\n");

            int imported = 0, errors = 0;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string paramList  = string.Join(", ", cols.Select(c => "@" + c));
                            string columnList = string.Join(", ", cols);
                            string sql = "INSERT INTO " + table +
                                         " (" + columnList + ") VALUES (" + paramList + ")";

                            foreach (string line in dataLines)
                            {
                                string[] values = SplitCsvLine(line);
                                using (var cmd = new MySqlCommand(sql, conn, transaction))
                                {
                                    for (int i = 0; i < cols.Length; i++)
                                    {
                                        string val = values[i].Trim();
                                        cmd.Parameters.AddWithValue("@" + cols[i],
                                            val == "" || val.ToLower() == "null"
                                                ? (object)DBNull.Value
                                                : val);
                                    }
                                    cmd.ExecuteNonQuery();
                                    imported++;
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            errors++;
                            txtLog.AppendText("✘ Ошибка при импорте: " + ex.Message + "\r\n");
                            MessageBox.Show("Ошибка при импорте данных:\r\n" + ex.Message +
                                "\r\n\r\nВсе изменения отменены (rollback).",
                                "Ошибка импорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ── ISSUE #5: сообщение с количеством занесённых записей ──
            txtLog.AppendText("✔ Импортировано записей: " + imported + "\r\n");
            MessageBox.Show(
                "Импорт завершён успешно!\r\n\r\n" +
                "Таблица: " + table + "\r\n" +
                "Занесено записей: " + imported,
                "Импорт выполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Разбивает строку CSV с учётом значений в кавычках.
        /// </summary>
        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current   = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    { current.Append('"'); i++; }
                    else
                        inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                { result.Add(current.ToString()); current.Clear(); }
                else
                    current.Append(c);
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}

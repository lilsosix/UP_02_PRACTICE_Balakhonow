using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FurnitureStoreApp
{
    public partial class OrdersListForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = DatabaseConnection.ConnectionString;
        private DataTable ordersTable;
        private DataView ordersView;

        private const int PageSize    = 20;
        private int       _currentPage = 1;
        private int       _totalPages  = 1;
        private DataTable _filteredTable; 

        // ── Цвета статусов (Issue #10) ──────────────────────────────
        private static readonly System.Collections.Generic.Dictionary<string, Color> StatusColors =
            new System.Collections.Generic.Dictionary<string, Color>
            {
                { "Принят",    Color.FromArgb(220, 235, 255) }, // голубой
                { "В пути",    Color.FromArgb(255, 255, 200) }, // жёлтый
                { "Доставлен", Color.FromArgb(220, 255, 220) }, // зелёный
                { "Завершён",  Color.FromArgb(200, 230, 200) }, // тёмно-ёный
                { "Отменен",   Color.FromArgb(255, 210, 210) }, // красный
            };

        public OrdersListForm()
        {
            InitializeComponent();
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("FurnitureStoreApp");
            ConnectToDatabase();
            LoadStatuses();
            LoadOrders();
            ConfigureDataGridView();

            if (pictureBox2 != null)
            {
                Label lbl = new Label();
                lbl.Text = "Учёт заказов";
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Arial", 30, FontStyle.Bold);
                lbl.ForeColor = Color.White;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Dock = DockStyle.Fill;
                lbl.AutoSize = false;
                pictureBox2.Controls.Add(lbl);
            }

            this.txtSearchOrderNumber.TextChanged += (s, e) => ApplyFilters();
            this.cmbStatus.SelectedIndexChanged += (s, e) => ApplyFilters();
            this.dtpStart.ValueChanged += (s, e) => ApplyFilters();
            this.dtpEnd.ValueChanged += (s, e) => ApplyFilters();
            this.chkDateRange.CheckedChanged += (s, e) =>
            {
                dtpStart.Enabled = chkDateRange.Checked;
                dtpEnd.Enabled = chkDateRange.Checked;
                ApplyFilters();
            };
            this.cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
            var toolTip = new ToolTip();
            toolTip.SetToolTip(lblLegend,
                "Расшифровка цветов строк:\n" +
                "  Голубой  — Принят (заказ оформлен)\n" +
                "  Жёлтый  — В пути (передан в доставку)\n" +
                "  Светло-зелёный — Доставлен\n" +
                "  Зелёный — Завершён (закрыт)\n" +
                "  Розовый — Отменен");

            lblLegend.Paint += LblLegend_Paint;
        }

        private void LblLegend_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color[] colors = {
                Color.FromArgb(220, 235, 255), // Принят
                Color.FromArgb(255, 255, 200), // В пути
                Color.FromArgb(220, 255, 220), // Доставлен
                Color.FromArgb(200, 230, 200), // Завершён
                Color.FromArgb(255, 210, 210), // Отменен
            };
            int[] xPos = { 38, 110, 182, 272, 358 };
            for (int i = 0; i < colors.Length; i++)
            {
                e.Graphics.FillRectangle(new SolidBrush(colors[i]), xPos[i], 2, 14, 10);
                e.Graphics.DrawRectangle(Pens.Gray, xPos[i], 2, 14, 10);
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
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                DataRow allRow = dt.NewRow();
                allRow["id"] = -1;
                allRow["name"] = "Все статусы";
                dt.Rows.InsertAt(allRow, 0);

                cmbStatus.DataSource = dt;
                cmbStatus.DisplayMember = "name";
                cmbStatus.ValueMember = "id";
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrders()
        {
            try
            {
                string query = @"
                    SELECT
                        o.id AS 'Номер заказа',
                        DATE_FORMAT(o.order_date, '%d.%m.%Y %H:%i') AS 'Дата заказа',
                        c.full_name AS 'Клиент',
                        COUNT(oi.id) AS 'Позиций',
                        GROUP_CONCAT(p.name ORDER BY p.name SEPARATOR ', ') AS 'Товары',
                        s.name AS 'Статус',
                        o.total_cost AS 'Сумма',
                        d.name AS 'Доставка',
                        u.full_name AS 'Менеджер',
                        DATE_FORMAT(o.completion_date, '%d.%m.%Y') AS 'Дата выполнения'
                    FROM orders o
                    LEFT JOIN clients c          ON o.client_id = c.id
                    LEFT JOIN order_items oi     ON oi.order_id = o.id
                    LEFT JOIN products p         ON oi.product_id = p.id
                    LEFT JOIN order_statuses s   ON o.status_id = s.id
                    LEFT JOIN delivery_methods d ON o.delivery_id = d.id
                    LEFT JOIN users u            ON o.user_id = u.id
                    GROUP BY o.id, o.order_date, c.full_name, s.name,
                             o.total_cost, d.name, u.full_name, o.completion_date
                    ORDER BY o.id DESC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                ordersTable = new DataTable();
                adapter.Fill(ordersTable);

                ordersTable.Columns.Add("order_date_real", typeof(DateTime));
                foreach (DataRow row in ordersTable.Rows)
                {
                    string dateStr = row["Дата заказа"].ToString();
                    if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy HH:mm", null,
                        System.Globalization.DateTimeStyles.None, out DateTime dt))
                        row["order_date_real"] = dt;
                    else
                        row["order_date_real"] = DBNull.Value;
                }

                ordersView = new DataView(ordersTable);
                _currentPage = 1;
                UpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.MultiSelect = false;
        }

        private void ApplyFilters()
        {
            if (ordersView == null) return;

            string filter = "";

            if (!string.IsNullOrWhiteSpace(txtSearchOrderNumber.Text))
            {
                string num = txtSearchOrderNumber.Text.Trim().Replace("'", "''");
                filter = $"[Номер заказа] LIKE '{num}%'";
            }

            if (cmbStatus.SelectedIndex > 0)
            {
                string status = cmbStatus.Text.Replace("'", "''");
                string statusFilter = $"[Статус] = '{status}'";
                filter = string.IsNullOrEmpty(filter) ? statusFilter : $"{filter} AND {statusFilter}";
            }

            if (chkDateRange.Checked)
            {
                DateTime start = dtpStart.Value.Date;
                DateTime end   = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);
                string startStr = start.ToString("yyyy-MM-dd HH:mm:ss");
                string endStr   = end.ToString("yyyy-MM-dd HH:mm:ss");
                string dateFilter = $"order_date_real >= #{startStr}# AND order_date_real <= #{endStr}#";
                filter = string.IsNullOrEmpty(filter) ? dateFilter : $"{filter} AND {dateFilter}";
            }

            ordersView.RowFilter = filter;

            _currentPage = 1;
            UpdatePagination();
        }

        // ISSUE #8 — Пагинация
        private void UpdatePagination()
        {
            if (ordersView == null) return;

            int totalRows = ordersView.Count;
            _totalPages   = Math.Max(1, (int)Math.Ceiling(totalRows / (double)PageSize));

            if (_currentPage > _totalPages) _currentPage = _totalPages;
            if (_currentPage < 1)           _currentPage = 1;

            // Берём срез текущей страницы из DataView
            _filteredTable = ordersView.ToTable();
            int startRow   = (_currentPage - 1) * PageSize;
            int endRow     = Math.Min(startRow + PageSize, _filteredTable.Rows.Count);
            int shown      = endRow - startRow;

            // Создаём DataTable только с нужными строками
            DataTable pageTable = _filteredTable.Clone();
            for (int i = startRow; i < endRow; i++)
                pageTable.ImportRow(_filteredTable.Rows[i]);

            dataGridViewOrders.DataSource = pageTable;

            // Скрываем служебную колонку
            if (dataGridViewOrders.Columns.Contains("order_date_real"))
                dataGridViewOrders.Columns["order_date_real"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("Сумма"))
                dataGridViewOrders.Columns["Сумма"].DefaultCellStyle.Format = "N2";

            // ISSUE #10 — Условное форматирование
            ColorizeRows();

            // ISSUE #9 — Счётчик
            lblPageInfo.Text = $"Показано: {shown} из {totalRows}";

            // Кнопки ← →
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < _totalPages;

            // Номера страниц
            RebuildPageButtons();
        }

        private void RebuildPageButtons()
        {
            pnlPages.Controls.Clear();

            for (int p = 1; p <= _totalPages; p++)
            {
                int pageNum = p; 
                var btn = new Button
                {
                    Text      = p.ToString(),
                    Size      = new Size(32, 26),
                    Font      = new Font("Microsoft Sans Serif", 8.5F,
                                    p == _currentPage ? FontStyle.Bold : FontStyle.Regular),
                    BackColor = p == _currentPage
                                    ? SystemColors.Highlight
                                    : SystemColors.Control,
                    ForeColor = p == _currentPage ? Color.White : Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Margin    = new Padding(1, 0, 1, 0),
                };
                btn.Click += (s, e) => { _currentPage = pageNum; UpdatePagination(); };
                pnlPages.Controls.Add(btn);
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1) { _currentPage--; UpdatePagination(); }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages) { _currentPage++; UpdatePagination(); }
        }

        // ISSUE #10 — Условное форматирование строк по статусу
        private void ColorizeRows()
        {
            if (!dataGridViewOrders.Columns.Contains("Статус")) return;

            foreach (DataGridViewRow row in dataGridViewOrders.Rows)
            {
                string status = row.Cells["Статус"].Value?.ToString() ?? "";
                if (StatusColors.TryGetValue(status, out Color color))
                {
                    row.DefaultCellStyle.BackColor = color;
                    row.DefaultCellStyle.SelectionBackColor =
                        ControlPaint.Dark(color, 0.1f);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchOrderNumber.Clear();
            cmbStatus.SelectedIndex = 0;
            chkDateRange.Checked    = false;
            dtpStart.Value          = DateTime.Now.AddMonths(-1);
            dtpEnd.Value            = DateTime.Now;
            if (ordersView != null)
            {
                ordersView.RowFilter = "";
                ordersView.Sort      = "";
            }
            _currentPage = 1;
            UpdatePagination();
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ordersView == null) return;
            if (cmbSort.SelectedIndex == 0)
                ordersView.Sort = "[Сумма] ASC";
            else if (cmbSort.SelectedIndex == 1)
                ordersView.Sort = "[Сумма] DESC";
            _currentPage = 1;
            UpdatePagination();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (ordersView == null || ordersView.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter      = "Excel файлы (*.xlsx)|*.xlsx";
                dlg.Title       = "Сохранить отчёт";
                dlg.FileName    = $"Отчёт_по_заказам_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
                dlg.DefaultExt  = "xlsx";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {

                    string periodLabel = chkDateRange.Checked
                        ? $"{dtpStart.Value:dd.MM.yyyy} – {dtpEnd.Value:dd.MM.yyyy}"
                        : "Весь период";
                    string statusLabel = cmbStatus.SelectedIndex > 0 ? cmbStatus.Text : "Все статусы";
                    DateTime pStart = chkDateRange.Checked ? dtpStart.Value.Date : DateTime.MinValue;
                    DateTime pEnd   = chkDateRange.Checked ? dtpEnd.Value.Date   : DateTime.MaxValue;

                    using (var pkg = new ExcelPackage())
                    {
                        BuildSummarySheet(pkg, periodLabel, statusLabel, pStart, pEnd);
                        BuildDetailSheet(pkg, periodLabel);

                        pkg.SaveAs(new System.IO.FileInfo(dlg.FileName));
                    }

                    MessageBox.Show($"Отчёт сохранён!\n{dlg.FileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBox.Show("Открыть файл?", "Вопрос",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(
                            new System.Diagnostics.ProcessStartInfo
                            { FileName = dlg.FileName, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при формировании отчёта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BuildSummarySheet(ExcelPackage pkg, string periodLabel,
            string statusLabel, DateTime pStart, DateTime pEnd)
        {
            var ws = pkg.Workbook.Worksheets.Add("Сводный отчёт");
            int row = 1;

            // ── Заголовок ──────────────────────────────────────────────
            WriteCell(ws, row, 1, "ОТЧЁТ ПО ЗАКАЗАМ МАГАЗИНА МЕБЕЛИ", bold: true, size: 16);
            ws.Cells[row, 1, row, 6].Merge = true;
            row++;
            WriteCell(ws, row, 1, $"Период: {periodLabel}");
            ws.Cells[row, 1, row, 6].Merge = true;
            row++;
            WriteCell(ws, row, 1, $"Фильтр по статусу: {statusLabel}");
            ws.Cells[row, 1, row, 6].Merge = true;
            row++;
            WriteCell(ws, row, 1,
                $"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}    Сформировал: {CurrentSession.FullName}",
                italic: true, size: 10);
            ws.Cells[row, 1, row, 6].Merge = true;
            row += 2;

            string whereSql = pStart == DateTime.MinValue
                ? "WHERE 1=1"
                : "WHERE o.order_date BETWEEN @start AND @end";

            // ── 1. Ключевые показатели ─────────────────────────────────
            row = WriteSectionHeader(ws, row, "1. КЛЮЧЕВЫЕ ПОКАЗАТЕЛИ",
                System.Drawing.Color.FromArgb(68, 114, 196), 4);
            try
            {
                string sql = $@"
                    SELECT COUNT(DISTINCT o.id)                                    AS total_orders,
                           SUM(CASE WHEN s.name='Завершён' THEN 1 ELSE 0 END)      AS completed,
                           SUM(CASE WHEN s.name='Отменен'  THEN 1 ELSE 0 END)      AS canceled,
                           COALESCE(SUM(o.total_cost),0)                           AS revenue,
                           COALESCE(SUM(o.prepayment),0)                           AS prepay,
                           SUM((SELECT COUNT(*) FROM order_items oi WHERE oi.order_id=o.id)) AS items
                    FROM orders o
                    LEFT JOIN order_statuses s ON o.status_id=s.id
                    {whereSql}";
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    if (whereSql.Contains("@start"))
                    {
                        cmd.Parameters.AddWithValue("@start", pStart);
                        cmd.Parameters.AddWithValue("@end", pEnd.AddDays(1).AddSeconds(-1));
                    }
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int    total     = r["total_orders"] == DBNull.Value ? 0 : Convert.ToInt32(r["total_orders"]);
                            int    done      = r["completed"]    == DBNull.Value ? 0 : Convert.ToInt32(r["completed"]);
                            int    canceled  = r["canceled"]     == DBNull.Value ? 0 : Convert.ToInt32(r["canceled"]);
                            decimal revenue  = r["revenue"]      == DBNull.Value ? 0 : Convert.ToDecimal(r["revenue"]);
                            decimal prepay   = r["prepay"]       == DBNull.Value ? 0 : Convert.ToDecimal(r["prepay"]);
                            int    items     = r["items"]        == DBNull.Value ? 0 : Convert.ToInt32(r["items"]);
                            decimal avg      = total > 0 ? revenue / total : 0;

                            row = WriteMetric(ws, row, "Всего заказов за период",          total.ToString());
                            row = WriteMetric(ws, row, "  из них завершённых",             done.ToString());
                            row = WriteMetric(ws, row, "  из них отменённых",              canceled.ToString());
                            row = WriteMetric(ws, row, "  активных (в работе)",            (total - done - canceled).ToString());
                            row = WriteMetric(ws, row, "Всего позиций товара",             items.ToString());
                            row = WriteMetric(ws, row, "Общая выручка (сумма заказов)",    $"{revenue:N2} ₽");
                            row = WriteMetric(ws, row, "Средний чек",                      $"{avg:N2} ₽");
                            row = WriteMetric(ws, row, "Итого предоплат получено",         $"{prepay:N2} ₽");
                            row = WriteMetric(ws, row, "Остаток к оплате",                 $"{revenue - prepay:N2} ₽");
                        }
                    }
                }
            }
            catch (Exception ex) { WriteCell(ws, row++, 1, $"Ошибка: {ex.Message}"); }
            row++;

            // ── 2. По статусам ─────────────────────────────────────────
            row = BuildQueryTable(ws, row, "2. ЗАКАЗЫ ПО СТАТУСАМ",
                System.Drawing.Color.FromArgb(112, 173, 71),
                new[] { "Статус", "Кол-во заказов", "Сумма, ₽" },
                $@"SELECT s.name, COUNT(o.id), COALESCE(SUM(o.total_cost),0)
                   FROM orders o LEFT JOIN order_statuses s ON o.status_id=s.id
                   {whereSql} GROUP BY s.name ORDER BY COUNT(o.id) DESC",
                whereSql.Contains("@start"), pStart, pEnd);
            row++;

            // ── 3. По доставке ─────────────────────────────────────────
            row = BuildQueryTable(ws, row, "3. ЗАКАЗЫ ПО СПОСОБУ ДОСТАВКИ",
                System.Drawing.Color.FromArgb(112, 173, 71),
                new[] { "Способ доставки", "Кол-во заказов", "Сумма, ₽" },
                $@"SELECT d.name, COUNT(o.id), COALESCE(SUM(o.total_cost),0)
                   FROM orders o LEFT JOIN delivery_methods d ON o.delivery_id=d.id
                   {whereSql} GROUP BY d.name ORDER BY COUNT(o.id) DESC",
                whereSql.Contains("@start"), pStart, pEnd);
            row++;

            // ── 4. По менеджерам ───────────────────────────────────────
            row = BuildQueryTable(ws, row, "4. ВЫРУЧКА ПО МЕНЕДЖЕРАМ",
                System.Drawing.Color.FromArgb(112, 173, 71),
                new[] { "Менеджер", "Кол-во заказов", "Сумма заказов, ₽", "Завершено" },
                $@"SELECT u.full_name, COUNT(o.id), COALESCE(SUM(o.total_cost),0),
                          SUM(CASE WHEN s.name='Завершён' THEN 1 ELSE 0 END)
                   FROM orders o LEFT JOIN users u ON o.user_id=u.id
                   LEFT JOIN order_statuses s ON o.status_id=s.id
                   {whereSql} GROUP BY u.id,u.full_name ORDER BY SUM(o.total_cost) DESC",
                whereSql.Contains("@start"), pStart, pEnd);

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }

        private int BuildQueryTable(ExcelWorksheet ws, int startRow, string title,
            System.Drawing.Color hColor, string[] cols, string sql,
            bool useParams, DateTime pStart, DateTime pEnd)
        {
            int row = startRow;
            row = WriteSectionHeader(ws, row, title, hColor, cols.Length);

            // Шапка таблицы
            for (int c = 0; c < cols.Length; c++)
            {
                var cell = ws.Cells[row, c + 1];
                cell.Value = cols[c];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(217, 225, 242));
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
            row++;

            bool hasData = false;
            try
            {
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    if (useParams)
                    {
                        cmd.Parameters.AddWithValue("@start", pStart);
                        cmd.Parameters.AddWithValue("@end", pEnd.AddDays(1).AddSeconds(-1));
                    }
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            hasData = true;
                            for (int c = 0; c < r.FieldCount; c++)
                            {
                                var cell = ws.Cells[row, c + 1];
                                cell.Value = r[c] == DBNull.Value ? "—" :
                                    (r[c] is decimal d ? $"{d:N2}" : r[c].ToString());
                                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                            row++;
                        }
                    }
                }
            }
            catch { }

            if (!hasData)
            {
                ws.Cells[row, 1, row, cols.Length].Merge = true;
                ws.Cells[row, 1].Value = "Нет данных";
                ws.Cells[row, 1].Style.Font.Italic = true;
                row++;
            }
            return row;
        }

        private void BuildDetailSheet(ExcelPackage pkg, string periodLabel)
        {
            var ws = pkg.Workbook.Worksheets.Add("Детализация");
            int row = 1;

            WriteCell(ws, row, 1, $"ДЕТАЛИЗАЦИЯ ЗАКАЗОВ  |  Период: {periodLabel}", bold: true, size: 13);
            ws.Cells[row, 1, row, 10].Merge = true;
            row += 2;

            // Шапка
            int col = 1;
            foreach (DataColumn dc in ordersView.Table.Columns)
            {
                if (dc.ColumnName == "order_date_real") continue;
                var cell = ws.Cells[row, col];
                cell.Value = dc.ColumnName;
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(68, 114, 196));
                cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                col++;
            }
            row++;

            // Данные
            bool alt = false;
            decimal sumTotal = 0;
            int sumCol = 1;
            foreach (DataColumn dc in ordersView.Table.Columns)
            {
                if (dc.ColumnName == "order_date_real") continue;
                if (dc.ColumnName == "Сумма") break;
                sumCol++;
            }

            foreach (DataRowView rv in ordersView)
            {
                col = 1;
                var bg = alt
                    ? System.Drawing.Color.FromArgb(242, 242, 242)
                    : System.Drawing.Color.White;
                foreach (DataColumn dc in ordersView.Table.Columns)
                {
                    if (dc.ColumnName == "order_date_real") continue;
                    string val = rv.Row[dc]?.ToString() ?? "";
                    var cell = ws.Cells[row, col];
                    cell.Value = val;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(bg);
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;
                }
                if (decimal.TryParse(rv.Row["Сумма"]?.ToString(), out decimal v)) sumTotal += v;
                row++;
                alt = !alt;
            }

            // Итог
            row++;
            ws.Cells[row, 1].Value = "ИТОГО:";
            ws.Cells[row, 1].Style.Font.Bold = true;
            ws.Cells[row, 2].Value = $"{ordersView.Count} заказов";
            ws.Cells[row, 2].Style.Font.Bold = true;
            ws.Cells[row, sumCol].Value = $"{sumTotal:N2} ₽";
            ws.Cells[row, sumCol].Style.Font.Bold = true;

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
        }

        private void WriteCell(ExcelWorksheet ws, int row, int col, string value,
            bool bold = false, float size = 11, bool italic = false)
        {
            var cell = ws.Cells[row, col];
            cell.Value = value;
            cell.Style.Font.Bold   = bold;
            cell.Style.Font.Size   = size;
            cell.Style.Font.Italic = italic;
        }

        private int WriteSectionHeader(ExcelWorksheet ws, int row, string title,
            System.Drawing.Color bg, int colSpan)
        {
            ws.Cells[row, 1, row, colSpan].Merge = true;
            var cell = ws.Cells[row, 1];
            cell.Value = title;
            cell.Style.Font.Bold = true;
            cell.Style.Font.Size = 12;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(bg);
            bool darkBg = bg != System.Drawing.Color.FromArgb(255, 192, 0);
            cell.Style.Font.Color.SetColor(
                darkBg ? System.Drawing.Color.White : System.Drawing.Color.Black);
            return row + 1;
        }

        private int WriteMetric(ExcelWorksheet ws, int row, string label, string value)
        {
            ws.Cells[row, 1].Value = label;
            ws.Cells[row, 2].Value = value;
            ws.Cells[row, 2].Style.Font.Bold = true;
            return row + 1;
        }


        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrdersListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void btnViewOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для просмотра.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int orderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["Номер заказа"].Value);
            using (OrderViewForm viewForm = new OrderViewForm(connection, orderId))
                viewForm.ShowDialog();
            LoadOrders(); 
        }

        private void lblLegend_Click(object sender, EventArgs e)
        {

        }
    }
}
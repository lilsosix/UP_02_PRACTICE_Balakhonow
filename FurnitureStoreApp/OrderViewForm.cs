using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class OrderViewForm : Form
    {
        private readonly MySqlConnection connection;
        private readonly int orderId;
        private decimal totalCost;
        private int currentStatusId;
        private string _realClientName = "";
        private string _realClientPhone = "";
        private bool _personalVisible = false;

        public OrderViewForm(MySqlConnection conn, int orderId)
        {
            InitializeComponent();
            connection = conn;
            this.orderId = orderId;
            ApplyStyle();
            LoadOrderData();
            LoadOrderItems();
            LoadStatuses();
            ConfigureByRole();
        }

        private void ApplyStyle()
        {
            Label lbl = new Label();
            lbl.Text = "Просмотр заказа";
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 16, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBox2.Controls.Add(lbl);
        }

        private void LoadOrderData()
        {
            try
            {
                string query = @"
                    SELECT
                        o.id,
                        DATE_FORMAT(o.order_date,'%d.%m.%Y %H:%i') AS OrderDate,
                        DATE_FORMAT(o.completion_date,'%d.%m.%Y')  AS CompletionDate,
                        c.full_name  AS ClientName,
                        c.phone      AS ClientPhone,
                        s.name       AS StatusName,
                        o.status_id  AS StatusId,
                        d.name       AS DeliveryMethod,
                        u.full_name  AS ManagerName,
                        o.prepayment AS Prepayment,
                        o.total_cost AS TotalCost
                    FROM orders o
                    LEFT JOIN clients c          ON o.client_id = c.id
                    LEFT JOIN order_statuses s   ON o.status_id = s.id
                    LEFT JOIN delivery_methods d ON o.delivery_id = d.id
                    LEFT JOIN users u            ON o.user_id = u.id
                    WHERE o.id = @id";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", orderId);

                using (MySqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        lblOrderId.Text        = r["id"].ToString();
                        lblOrderDate.Text      = r["OrderDate"].ToString();
                        lblCompletionDate.Text = r["CompletionDate"].ToString();
                        _realClientName        = r["ClientName"].ToString();
                        _realClientPhone       = FormatPhone(r["ClientPhone"].ToString());
                        _personalVisible       = false;
                        lblClientName.Text     = MaskPersonal(_realClientName);
                        lblClientPhone.Text    = MaskPersonal(_realClientPhone);
                        lblStatus.Text         = r["StatusName"].ToString();
                        currentStatusId        = Convert.ToInt32(r["StatusId"]);
                        lblDelivery.Text       = r["DeliveryMethod"].ToString();
                        lblManager.Text        = r["ManagerName"].ToString();
                        lblPrepayment.Text     = Convert.ToDecimal(r["Prepayment"]).ToString("N2") + " руб.";
                        totalCost              = Convert.ToDecimal(r["TotalCost"]);
                        lblCost.Text           = totalCost.ToString("N2") + " руб.";
                    }
                    else
                    {
                        MessageBox.Show("Заказ не найден.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заказа: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadOrderItems()
        {
            try
            {
                string query = @"
                    SELECT
                        p.article   AS Артикул,
                        p.name      AS Наименование,
                        oi.quantity AS КолВо,
                        oi.price    AS Цена,
                        oi.cost     AS Сумма
                    FROM order_items oi
                    JOIN products p ON oi.product_id = p.id
                    WHERE oi.order_id = @id
                    ORDER BY p.name";

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", orderId);
                var adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Переименовываем колонки для отображения
                if (dt.Columns.Contains("КолВо"))
                    dt.Columns["КолВо"].ColumnName = "Кол-во";

                dataGridViewItems.DataSource = dt;
                dataGridViewItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewItems.AllowUserToAddRows = false;
                dataGridViewItems.ReadOnly = true;
                dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dataGridViewItems.Columns.Contains("Цена"))
                    dataGridViewItems.Columns["Цена"].DefaultCellStyle.Format = "N2";
                if (dataGridViewItems.Columns.Contains("Сумма"))
                    dataGridViewItems.Columns["Сумма"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки позиций: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int[] GetAllowedStatusIds(int fromStatusId)
        {
            switch (fromStatusId)
            {
                case 1: return new[] { 2, 5 };
                case 2: return new[] { 3, 5 };
                case 3: return new[] { 4 };
                default: return new int[0];
            }
        }

        private void LoadStatuses()
        {
            try
            {
                var adapter = new MySqlDataAdapter(
                    "SELECT id, name FROM order_statuses ORDER BY id", connection);
                DataTable allStatuses = new DataTable();
                adapter.Fill(allStatuses);

                DataTable filtered = allStatuses.Clone();
                if (CurrentSession.RoleId == 3)
                {
                    foreach (DataRow row in allStatuses.Rows)
                        filtered.ImportRow(row);
                }
                else
                {
                    int[] allowed = GetAllowedStatusIds(currentStatusId);
                    foreach (DataRow row in allStatuses.Rows)
                        if (Array.IndexOf(allowed, Convert.ToInt32(row["id"])) >= 0)
                            filtered.ImportRow(row);
                }

                cmbChangeStatus.DataSource    = filtered;
                cmbChangeStatus.DisplayMember = "name";
                cmbChangeStatus.ValueMember   = "id";
                cmbChangeStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmbChangeStatus.Items.Count > 0)
                    cmbChangeStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки статусов: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureByRole()
        {
            bool isAdmin   = CurrentSession.RoleId == 3;
            bool isManager = CurrentSession.RoleId == 2;
            bool isFinal   = currentStatusId == 4 || currentStatusId == 5;
            bool managerCanChange = isManager && !isFinal;

            cmbChangeStatus.Visible = isAdmin || managerCanChange;
            btnChangeStatus.Visible = isAdmin || managerCanChange;
            lblChangeStatus.Visible = isAdmin || managerCanChange;
            btnPrintCheck.Visible   = isManager || isAdmin;

            if (isManager && isFinal)
            {
                lblChangeStatus.Visible   = true;
                lblChangeStatus.Text      = "Заказ завершён — изменение статуса недоступно";
                lblChangeStatus.ForeColor = Color.Gray;
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (cmbChangeStatus.SelectedIndex == -1) return;
            int    newStatusId   = Convert.ToInt32((cmbChangeStatus.SelectedItem as DataRowView)["id"]);
            string newStatusName = (cmbChangeStatus.SelectedItem as DataRowView)["name"].ToString();

            if (MessageBox.Show(
                    "Изменить статус заказа №" + orderId + " на «" + newStatusName + "»?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var cmd = new MySqlCommand(
                    "UPDATE orders SET status_id = @s WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@s",  newStatusId);
                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.ExecuteNonQuery();
                lblStatus.Text  = newStatusName;
                currentStatusId = newStatusId;
                MessageBox.Show("Статус заказа обновлён.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStatuses();
                ConfigureByRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка смены статуса: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintCheck_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter   = "Word документ (*.docx)|*.docx";
                    dlg.FileName = "Чек_заказ_" + orderId + "_" + DateTime.Now.ToString("yyyyMMdd") + ".docx";
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    var items = new System.Collections.Generic.List<DocxLine>();
                    items.Add(new DocxLine("ЧЕК ЗАКАЗА",
                        bold: true, size: 36, center: true));
                    items.Add(new DocxLine("Дата формирования: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
                        bold: false, size: 20, center: true));
                    items.Add(new DocxLine(""));
                    items.Add(new DocxLine("Номер заказа:     " + lblOrderId.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine("Дата заказа:      " + lblOrderDate.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine("Дата выполнения:  " + lblCompletionDate.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine(""));
                    items.Add(new DocxLine("Клиент:   " + _realClientName,
                        bold: false, size: 22));
                    items.Add(new DocxLine("Телефон:  " + _realClientPhone,
                        bold: false, size: 22));
                    items.Add(new DocxLine(""));
                    items.Add(new DocxLine("Состав заказа:",
                        bold: true, size: 22));

                    foreach (DataGridViewRow row in dataGridViewItems.Rows)
                    {
                        string colName  = (row.Cells["Наименование"].Value ?? "").ToString();
                        string colArt   = (row.Cells["Артикул"].Value ?? "").ToString();
                        string colQty   = (row.Cells["Кол-во"].Value ?? "").ToString();
                        decimal colPrice = Convert.ToDecimal(row.Cells["Цена"].Value);
                        decimal colSum   = Convert.ToDecimal(row.Cells["Сумма"].Value);
                        string line = "  - " + colName
                            + "   арт. " + colArt
                            + "   " + colQty + " шт. x "
                            + colPrice.ToString("N2") + " руб. = "
                            + colSum.ToString("N2") + " руб.";
                        items.Add(new DocxLine(line, bold: false, size: 20));
                    }

                    items.Add(new DocxLine(""));
                    items.Add(new DocxLine("Способ доставки: " + lblDelivery.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine("Статус:          " + lblStatus.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine("Менеджер:        " + lblManager.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine(""));
                    items.Add(new DocxLine("Предоплата:  " + lblPrepayment.Text,
                        bold: false, size: 22));
                    items.Add(new DocxLine("ИТОГО: " + totalCost.ToString("N2") + " руб.",
                        bold: true, size: 28));

                    SaveDocx(dlg.FileName, items);
                    MessageBox.Show("Чек сохранён!\n" + dlg.FileName, "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка формирования чека: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Вспомогательный класс — одна строка документа ──────────────
        private class DocxLine
        {
            public string Text;
            public bool   Bold;
            public int    Size;
            public bool   Center;

            public DocxLine(string text, bool bold = false, int size = 22, bool center = false)
            {
                Text   = text;
                Bold   = bold;
                Size   = size;
                Center = center;
            }
        }

        // ── Генерация .docx без Word Interop (чистый Open XML + ZIP) ───
        private void SaveDocx(string path,
            System.Collections.Generic.List<DocxLine> lines)
        {
            // XML-экранирование
            System.Func<string, string> esc = s => s
                .Replace("&",  "&amp;")
                .Replace("<",  "&lt;")
                .Replace(">",  "&gt;")
                .Replace("'",  "&apos;");

            // ── document.xml ────────────────────────────────────────────
            var sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            sb.Append("<w:document");
            sb.Append(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\">");
            sb.Append("<w:body>");

            foreach (var ln in lines)
            {
                sb.Append("<w:p>");
                if (ln.Center)
                {
                    sb.Append("<w:pPr>");
                    sb.Append("<w:jc w:val=\"center\"/>");
                    sb.Append("</w:pPr>");
                }
                if (!string.IsNullOrEmpty(ln.Text))
                {
                    sb.Append("<w:r><w:rPr>");
                    if (ln.Bold) sb.Append("<w:b/>");
                    sb.Append("<w:sz w:val=\"" + ln.Size + "\"/>");
                    sb.Append("<w:szCs w:val=\"" + ln.Size + "\"/>");
                    sb.Append("</w:rPr>");
                    sb.Append("<w:t xml:space=\"preserve\">" + esc(ln.Text) + "</w:t>");
                    sb.Append("</w:r>");
                }
                sb.Append("</w:p>");
            }

            sb.Append("<w:sectPr>");
            sb.Append("<w:pgSz w:w=\"11906\" w:h=\"16838\"/>");
            sb.Append("<w:pgMar w:top=\"1134\" w:right=\"850\" w:bottom=\"1134\" w:left=\"1701\"/>");
            sb.Append("</w:sectPr>");
            sb.Append("</w:body></w:document>");

            // ── [Content_Types].xml ─────────────────────────────────────
            var ct = new System.Text.StringBuilder();
            ct.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            ct.Append("<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">");
            ct.Append("<Default Extension=\"rels\"");
            ct.Append(" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>");
            ct.Append("<Default Extension=\"xml\" ContentType=\"application/xml\"/>");
            ct.Append("<Override PartName=\"/word/document.xml\"");
            ct.Append(" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"/>");
            ct.Append("</Types>");

            // ── _rels/.rels ─────────────────────────────────────────────
            var rels = new System.Text.StringBuilder();
            rels.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            rels.Append("<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
            rels.Append("<Relationship Id=\"rId1\"");
            rels.Append(" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\"");
            rels.Append(" Target=\"word/document.xml\"/>");
            rels.Append("</Relationships>");

            // ── word/_rels/document.xml.rels ────────────────────────────
            var wordRels = new System.Text.StringBuilder();
            wordRels.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            wordRels.Append("<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
            wordRels.Append("</Relationships>");

            // ── Упаковываем в ZIP (.docx) ────────────────────────────────
            using (var fs = new System.IO.FileStream(
                       path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (var zip = new System.IO.Compression.ZipArchive(
                       fs, System.IO.Compression.ZipArchiveMode.Create))
            {
                WriteEntry(zip, "[Content_Types].xml",          ct.ToString());
                WriteEntry(zip, "_rels/.rels",                  rels.ToString());
                WriteEntry(zip, "word/document.xml",            sb.ToString());
                WriteEntry(zip, "word/_rels/document.xml.rels", wordRels.ToString());
            }
        }

        private static void WriteEntry(System.IO.Compression.ZipArchive zip,
            string entryName, string content)
        {
            var entry = zip.CreateEntry(entryName,
                System.IO.Compression.CompressionLevel.Optimal);
            using (var sw = new System.IO.StreamWriter(
                       entry.Open(), new System.Text.UTF8Encoding(false)))
                sw.Write(content);
        }

        // ── Issue #11: маскировка персональных данных ────────────────
        private static string MaskPersonal(string value)
        {
            if (string.IsNullOrEmpty(value)) return "—";
            if (value.Length <= 2) return "***";
            return value[0] + new string('*', value.Length - 2) + value[value.Length - 1];
        }

        private void btnShowPersonal_Click(object sender, EventArgs e)
        {
            // Issue #12: открываем отдельную форму с полными данными
            using (var form = new PersonalDataForm(_realClientName, _realClientPhone))
                form.ShowDialog();
        }

        private string FormatPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length != 11) return phone;
            return phone[0] + "(" + phone.Substring(1, 3) + ")"
                + phone.Substring(4, 3) + "-"
                + phone.Substring(7, 2) + "-"
                + phone.Substring(9, 2);
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}

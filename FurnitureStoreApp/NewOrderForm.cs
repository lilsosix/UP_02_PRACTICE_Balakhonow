using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class NewOrderForm : Form
    {
        private MySqlConnection connection;
        private readonly string connectionString = DatabaseConnection.ConnectionString;
        private DataTable productsTable;
        private DataTable cartTable;
        private int selectedClientId = -1;
        private string selectedClientName = "";

        public NewOrderForm()
        {
            InitializeComponent();
            dtpCompletionDate.MinDate = DateTime.Today;
            ConnectToDatabase();
            LoadCategories();
            LoadProducts();
            LoadDeliveryMethods();
            lblOrderDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
            dtpCompletionDate.Value = DateTime.Now.AddDays(7);
            InitializeCart();

            if (pictureBox2 != null)
            {
                Label lbl = new Label();
                lbl.Text = "Новый заказ";
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Arial", 36, FontStyle.Bold);
                lbl.ForeColor = Color.White;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Dock = DockStyle.Fill;
                lbl.AutoSize = false;
                pictureBox2.Controls.Add(lbl);
            }
        }

        public NewOrderForm(int clientId) : this()
        {
            LoadClient(clientId);
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

        private void LoadClient(int clientId)
        {
            try
            {
                var cmd = new MySqlCommand("SELECT full_name FROM clients WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", clientId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    selectedClientId = clientId;
                    selectedClientName = result.ToString();
                    lblSelectedClient.Text = $"Клиент: {selectedClientName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var adapter = new MySqlDataAdapter("SELECT id, name FROM product_categories ORDER BY name", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataRow allRow = dt.NewRow();
                allRow["id"] = -1;
                allRow["name"] = "Все категории";
                dt.Rows.InsertAt(allRow, 0);
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "name";
                cmbCategory.ValueMember = "id";
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                string query = @"
                    SELECT p.id, p.article AS 'Артикул', p.name AS 'Название',
                           p.price AS 'Цена', pc.name AS 'Категория', m.name AS 'Материал'
                    FROM products p
                    LEFT JOIN product_categories pc ON p.category_id = pc.id
                    LEFT JOIN materials m ON p.material_id = m.id
                    ORDER BY p.name";

                var adapter = new MySqlDataAdapter(query, connection);
                productsTable = new DataTable();
                adapter.Fill(productsTable);

                dataGridViewProducts.DataSource = productsTable;
                if (dataGridViewProducts.Columns.Contains("id"))
                    dataGridViewProducts.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDeliveryMethods()
        {
            try
            {
                var adapter = new MySqlDataAdapter("SELECT id, name FROM delivery_methods ORDER BY name", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbDelivery.DataSource = dt;
                cmbDelivery.DisplayMember = "name";
                cmbDelivery.ValueMember = "id";
                cmbDelivery.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmbDelivery.Items.Count > 0) cmbDelivery.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки доставки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCart()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("ProductId", typeof(int));
            cartTable.Columns.Add("Артикул", typeof(string));
            cartTable.Columns.Add("Наименование", typeof(string));
            cartTable.Columns.Add("Цена", typeof(decimal));
            cartTable.Columns.Add("Количество", typeof(int));
            cartTable.Columns.Add("Сумма", typeof(decimal));

            dataGridViewCart.DataSource = cartTable;
            dataGridViewCart.Columns["ProductId"].Visible = false;
            dataGridViewCart.Columns["Количество"].ReadOnly = false;
            dataGridViewCart.Columns["Артикул"].ReadOnly    = true;
            dataGridViewCart.Columns["Наименование"].ReadOnly = true;
            dataGridViewCart.Columns["Цена"].ReadOnly       = true;
            dataGridViewCart.Columns["Цена"].DefaultCellStyle.Format = "N2";
            dataGridViewCart.Columns["Сумма"].DefaultCellStyle.Format = "N2";
            dataGridViewCart.Columns["Сумма"].ReadOnly = true;
            dataGridViewCart.AllowUserToAddRows    = false;
            dataGridViewCart.AllowUserToDeleteRows = false;
            dataGridViewCart.AllowUserToResizeRows = false;
            dataGridViewCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCart.MultiSelect   = false;

            txtPrepayment.MaxLength = 12;

            dataGridViewCart.CellEndEdit += (s, e) =>
            {
                if (dataGridViewCart.Columns[e.ColumnIndex].Name == "Количество" && e.RowIndex >= 0)
                {
                    DataRow row = cartTable.Rows[e.RowIndex];
                    if (!int.TryParse(row["Количество"].ToString(), out int qty) || qty <= 0)
                    {
                        row["Количество"] = 1;
                        qty = 1;
                    }
                    row["Сумма"] = Convert.ToDecimal(row["Цена"]) * qty;
                    UpdateTotal();
                }
            };

            // Запрет ввода нецифровых символов в ячейку «Количество»
            dataGridViewCart.EditingControlShowing += (s, e) =>
            {
                if (dataGridViewCart.CurrentCell?.OwningColumn.Name == "Количество"
                    && e.Control is TextBox tb)
                {
                    tb.KeyPress -= CartQty_KeyPress;
                    tb.KeyPress += CartQty_KeyPress;
                }
            };
        }

        private void ApplyProductFilter()
        {
            if (productsTable == null) return;
            string filter = "";
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                filter = $"[Название] LIKE '%{txtSearch.Text.Trim().Replace("'", "''")}%'";
            if (cmbCategory.SelectedIndex > 0)
            {
                string cat = $"[Категория] = '{cmbCategory.Text.Replace("'", "''")}'";
                filter = string.IsNullOrEmpty(filter) ? cat : $"{filter} AND {cat}";
            }
            productsTable.DefaultView.RowFilter = filter;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyProductFilter();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyProductFilter();

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewProducts.SelectedRows[0];
            int productId = Convert.ToInt32(row.Cells["id"].Value);
            string article = row.Cells["Артикул"].Value.ToString();
            string name = row.Cells["Название"].Value.ToString();
            decimal price = Convert.ToDecimal(row.Cells["Цена"].Value);

            DataRow[] existing = cartTable.Select($"ProductId = {productId}");
            if (existing.Length > 0)
            {
                int newQty = Convert.ToInt32(existing[0]["Количество"]) + 1;
                existing[0]["Количество"] = newQty;
                existing[0]["Сумма"] = price * newQty;
            }
            else
            {
                DataRow newRow = cartTable.NewRow();
                newRow["ProductId"] = productId;
                newRow["Артикул"] = article;
                newRow["Наименование"] = name;
                newRow["Цена"] = price;
                newRow["Количество"] = 1;
                newRow["Сумма"] = price;
                cartTable.Rows.Add(newRow);
            }
            UpdateTotal();
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите позицию для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int rowIndex = dataGridViewCart.SelectedRows[0].Index;
            if (rowIndex >= 0 && rowIndex < cartTable.Rows.Count)
                cartTable.Rows.RemoveAt(rowIndex);
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
                total += Convert.ToDecimal(row["Сумма"]);

            decimal discount = total >= 10000 ? total * 0.1m : 0;
            decimal finalTotal = total - discount;

            lblTotalAmount.Text = discount > 0
                ? $"Итого: {finalTotal:N2} ₽  (скидка 10%: -{discount:N2} ₽)"
                : $"Итого: {total:N2} ₽";
        }

        private void btnSelectClient_Click(object sender, EventArgs e)
        {
            using (ClientsForm form = new ClientsForm(selectMode: true))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    selectedClientId = form.SelectedClientId;
                    selectedClientName = form.SelectedClientName;
                    lblSelectedClient.Text = $"Клиент: {selectedClientName}";
                }
            }
        }

        /// <summary>
        /// Сохраняет заказ: одна запись в orders + строки в order_items (транзакция).
        /// </summary>
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (selectedClientId == -1)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар в корзину.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbDelivery.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите способ доставки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal prepayment = 0;
            if (!string.IsNullOrWhiteSpace(txtPrepayment.Text))
            {
                if (!decimal.TryParse(txtPrepayment.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out prepayment) || prepayment < 0)
                {
                    MessageBox.Show("Введите корректную сумму предоплаты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrepayment.Focus();
                    return;
                }
            }

            int deliveryId = Convert.ToInt32((cmbDelivery.SelectedItem as DataRowView)["id"]);

            // Считаем итог со скидкой
            decimal rawTotal = 0;
            foreach (DataRow row in cartTable.Rows)
                rawTotal += Convert.ToDecimal(row["Сумма"]);
            bool hasDiscount = rawTotal >= 10000;
            decimal discountFactor = hasDiscount ? 0.9m : 1.0m;
            decimal totalCost = rawTotal * discountFactor;

            // Предоплата не может превышать итоговую сумму заказа
            if (prepayment > totalCost)
            {
                MessageBox.Show(
                    $"Предоплата ({prepayment:N2} ₽) не может превышать сумму заказа ({totalCost:N2} ₽).",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrepayment.Focus();
                return;
            }

            MySqlTransaction transaction = null;
            try
            {
                transaction = connection.BeginTransaction();

                // 1. Создаём запись заказа
                string insertOrder = @"
                    INSERT INTO orders
                        (order_date, completion_date, user_id, client_id,
                         status_id, prepayment, delivery_id, total_cost)
                    VALUES
                        (@order_date, @completion_date, @user_id, @client_id,
                         1, @prepayment, @delivery_id, @total_cost)";

                var cmdOrder = new MySqlCommand(insertOrder, connection, transaction);
                cmdOrder.Parameters.AddWithValue("@order_date", DateTime.Now);
                cmdOrder.Parameters.AddWithValue("@completion_date", dtpCompletionDate.Value);
                cmdOrder.Parameters.AddWithValue("@user_id", CurrentSession.UserId > 0 ? CurrentSession.UserId : 1);
                cmdOrder.Parameters.AddWithValue("@client_id", selectedClientId);
                cmdOrder.Parameters.AddWithValue("@prepayment", prepayment);
                cmdOrder.Parameters.AddWithValue("@delivery_id", deliveryId);
                cmdOrder.Parameters.AddWithValue("@total_cost", totalCost);
                cmdOrder.ExecuteNonQuery();

                long newOrderId = cmdOrder.LastInsertedId;

                // 2. Добавляем позиции в order_items
                string insertItem = @"
                    INSERT INTO order_items (order_id, product_id, quantity, price, cost)
                    VALUES (@order_id, @product_id, @quantity, @price, @cost)";

                foreach (DataRow item in cartTable.Rows)
                {
                    decimal itemPrice = Convert.ToDecimal(item["Цена"]);
                    int qty = Convert.ToInt32(item["Количество"]);
                    decimal itemCost = itemPrice * qty * discountFactor;

                    var cmdItem = new MySqlCommand(insertItem, connection, transaction);
                    cmdItem.Parameters.AddWithValue("@order_id", newOrderId);
                    cmdItem.Parameters.AddWithValue("@product_id", Convert.ToInt32(item["ProductId"]));
                    cmdItem.Parameters.AddWithValue("@quantity", qty);
                    cmdItem.Parameters.AddWithValue("@price", itemPrice);
                    cmdItem.Parameters.AddWithValue("@cost", itemCost);
                    cmdItem.ExecuteNonQuery();
                }

                transaction.Commit();

                string msg = $"Заказ №{newOrderId} успешно создан!\n" +
                             $"Позиций: {cartTable.Rows.Count}\n" +
                             $"Сумма: {totalCost:N2} ₽" +
                             (hasDiscount ? "\n(применена скидка 10%)" : "");
                MessageBox.Show(msg, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show($"Ошибка сохранения заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            new ClientsForm().ShowDialog();
        }

        private void CartQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class MerchandiserForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = DatabaseConnection.ConnectionString;
        private DataTable productsDataTable;

        // Константы для работы с изображениями
        private const string IMAGES_FOLDER = "ProductImages";
        private Image defaultProductImage;

        public MerchandiserForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadDefaultImage();
            ConnectToDatabase();
            LoadFilterData();      // загрузка категорий и материалов для фильтров
            LoadProducts();

            // Добавление заголовка на pictureBox2 (если есть)
            if (pictureBox2 != null)
            {
                Label lbl = new Label();
                lbl.Text = "Учет товаров";
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Arial", 36, FontStyle.Bold);
                lbl.ForeColor = Color.White;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Dock = DockStyle.Fill;
                lbl.AutoSize = false;
                pictureBox2.Controls.Add(lbl);
                cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbFilterMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            lblCurrentUser.Text = $"{CurrentSession.FullName}  |  {CurrentSession.RoleName}";
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersListForm form = new OrdersListForm();
            form.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MerchandiserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
            if (defaultProductImage != null)
                defaultProductImage.Dispose();
        }

        // ==================== НАСТРОЙКА DataGridView ====================
        private void InitializeDataGridView()
        {
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.ReadOnly = true;
            dataGridViewProducts.MultiSelect = false;
            dataGridViewProducts.RowTemplate.Height = 100;

            foreach (DataGridViewColumn column in dataGridViewProducts.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Добавляем столбец для изображений (будет первым)
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Изображение";
            imageColumn.HeaderText = "Фото";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageColumn.Width = 120;
            dataGridViewProducts.Columns.Insert(0, imageColumn);
        }

        // ==================== РАБОТА С ИЗОБРАЖЕНИЯМИ ====================
        private void LoadDefaultImage()
        {
            try
            {
                string defaultImagePath = Path.Combine(Application.StartupPath, "default_product.png");
                if (File.Exists(defaultImagePath))
                {
                    using (FileStream fs = new FileStream(defaultImagePath, FileMode.Open, FileAccess.Read))
                    {
                        defaultProductImage = Image.FromStream(fs);
                    }
                }
                else
                {
                    defaultProductImage = CreateDefaultImage(120, 120);
                }
            }
            catch
            {
                defaultProductImage = CreateDefaultImage(120, 120);
            }
        }

        private Image CreateDefaultImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.LightGray);
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    g.DrawRectangle(pen, 1, 1, width - 3, height - 3);
                }
                using (Font font = new Font("Arial", 10, FontStyle.Regular))
                using (SolidBrush brush = new SolidBrush(Color.DimGray))
                {
                    string text = "Нет\nизображения";
                    SizeF textSize = g.MeasureString(text, font, width);
                    g.DrawString(text, font, brush,
                        (width - textSize.Width) / 2,
                        (height - textSize.Height) / 2);
                }
            }
            return bitmap;
        }

        private string GetFullImagePath(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;
            if (Path.IsPathRooted(imagePath))
                return imagePath;
            return Path.Combine(Application.StartupPath, imagePath);
        }

        private Image LoadProductImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullPath = GetFullImagePath(imagePath);
                    if (File.Exists(fullPath))
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            return new Bitmap(Image.FromStream(fs), new Size(100, 100));
                        }
                    }
                }
            }
            catch { }
            return new Bitmap(defaultProductImage, new Size(100, 100));
        }

        // ==================== ПОДКЛЮЧЕНИЕ К БД ====================
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

        // ==================== ЗАГРУЗКА ДАННЫХ ДЛЯ ФИЛЬТРОВ ====================
        private void LoadFilterData()
        {
            LoadFilterCategories();
            LoadFilterMaterials();
        }

        private void LoadFilterCategories()
        {
            try
            {
                string query = "SELECT id, name FROM product_categories ORDER BY name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Добавляем пустой элемент "Все категории"
                DataRow row = dt.NewRow();
                row["id"] = -1;
                row["name"] = "Все категории";
                dt.Rows.InsertAt(row, 0);

                cmbFilterCategory.DataSource = dt;
                cmbFilterCategory.DisplayMember = "name";
                cmbFilterCategory.ValueMember = "id";
                cmbFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilterMaterials()
        {
            try
            {
                string query = "SELECT id, name FROM materials ORDER BY name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["id"] = -1;
                row["name"] = "Все материалы";
                dt.Rows.InsertAt(row, 0);

                cmbFilterMaterial.DataSource = dt;
                cmbFilterMaterial.DisplayMember = "name";
                cmbFilterMaterial.ValueMember = "id";
                cmbFilterMaterial.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ЗАГРУЗКА ТОВАРОВ ====================
        public void LoadProducts()
        {
            try
            {
                string query = @"
                    SELECT 
                        p.id,
                        p.article AS 'Артикул',
                        p.name AS 'Название',
                        pc.name AS 'Категория',
                        m.name AS 'Материал',
                        CONCAT(p.price, ' ₽') AS 'Цена',
                        p.price AS 'PriceValue',   -- для сортировки по цене
                        p.sizes AS 'Размеры',
                        p.description AS 'Описание',
                        p.image_path AS 'ImagePath'
                    FROM products p
                    LEFT JOIN product_categories pc ON p.category_id = pc.id
                    LEFT JOIN materials m ON p.material_id = m.id
                    ORDER BY p.id";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                productsDataTable = new DataTable();
                adapter.Fill(productsDataTable);
                productsDataTable.CaseSensitive = false;

                dataGridViewProducts.DataSource = productsDataTable;
                ConfigureColumns();
                dataGridViewProducts.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            if (dataGridViewProducts.Columns.Contains("id"))
                dataGridViewProducts.Columns["id"].Visible = false;
            if (dataGridViewProducts.Columns.Contains("ImagePath"))
                dataGridViewProducts.Columns["ImagePath"].Visible = false;
            if (dataGridViewProducts.Columns.Contains("Описание"))
                dataGridViewProducts.Columns["Описание"].Visible = false;
            if (dataGridViewProducts.Columns.Contains("PriceValue"))
                dataGridViewProducts.Columns["PriceValue"].Visible = false;

            if (dataGridViewProducts.Columns.Contains("Изображение"))
            {
                dataGridViewProducts.Columns["Изображение"].Width = 120;
                dataGridViewProducts.Columns["Изображение"].HeaderText = "Фото";
                dataGridViewProducts.Columns["Изображение"].DisplayIndex = 0;
            }
            if (dataGridViewProducts.Columns.Contains("Артикул"))
            {
                dataGridViewProducts.Columns["Артикул"].Width = 100;
                dataGridViewProducts.Columns["Артикул"].DisplayIndex = 1;
            }
            if (dataGridViewProducts.Columns.Contains("Название"))
            {
                dataGridViewProducts.Columns["Название"].Width = 200;
                dataGridViewProducts.Columns["Название"].DisplayIndex = 2;
            }
            if (dataGridViewProducts.Columns.Contains("Категория"))
            {
                dataGridViewProducts.Columns["Категория"].Width = 130;
                dataGridViewProducts.Columns["Категория"].DisplayIndex = 3;
            }
            if (dataGridViewProducts.Columns.Contains("Материал"))
            {
                dataGridViewProducts.Columns["Материал"].Width = 130;
                dataGridViewProducts.Columns["Материал"].DisplayIndex = 4;
            }
            if (dataGridViewProducts.Columns.Contains("Цена"))
            {
                dataGridViewProducts.Columns["Цена"].Width = 100;
                dataGridViewProducts.Columns["Цена"].DisplayIndex = 5;
            }
            if (dataGridViewProducts.Columns.Contains("Размеры"))
            {
                dataGridViewProducts.Columns["Размеры"].Width = 100;
                dataGridViewProducts.Columns["Размеры"].DisplayIndex = 6;
            }
        }

        // ==================== ФИЛЬТРАЦИЯ И СОРТИРОВКА ====================
        private void ApplyFilter()
        {
            try
            {
                if (productsDataTable == null) return;

                List<string> filters = new List<string>();

                // Текстовый поиск по названию и артикулу
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.Trim().Replace("'", "''");
                    filters.Add($"([Название] LIKE '%{searchText}%' OR [Артикул] LIKE '%{searchText}%')");
                }

                // Фильтр по категории (если выбрана не "Все категории")
                if (cmbFilterCategory.SelectedIndex > 0)
                {
                    string category = cmbFilterCategory.Text.Replace("'", "''");
                    filters.Add($"[Категория] = '{category}'");
                }

                // Фильтр по материалу
                if (cmbFilterMaterial.SelectedIndex > 0)
                {
                    string material = cmbFilterMaterial.Text.Replace("'", "''");
                    filters.Add($"[Материал] = '{material}'");
                }

                // Применяем фильтр
                productsDataTable.DefaultView.RowFilter = filters.Count > 0 ? string.Join(" AND ", filters) : "";
                dataGridViewProducts.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка применения фильтров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetFilters()
        {
            txtSearch.Clear();
            if (cmbFilterCategory.Items.Count > 0)
                cmbFilterCategory.SelectedIndex = 0;
            if (cmbFilterMaterial.Items.Count > 0)
                cmbFilterMaterial.SelectedIndex = 0;

            if (productsDataTable != null)
            {
                productsDataTable.DefaultView.RowFilter = "";
                productsDataTable.DefaultView.Sort = "";
            }
            dataGridViewProducts.Refresh();
        }

        // Сортировка по цене (возрастание)
        private void SortByPriceAscending()
        {
            if (productsDataTable != null)
            {
                productsDataTable.DefaultView.Sort = "[PriceValue] ASC";
                dataGridViewProducts.Refresh();
            }
        }

        // Сортировка по цене (убывание)
        private void SortByPriceDescending()
        {
            if (productsDataTable != null)
            {
                productsDataTable.DefaultView.Sort = "[PriceValue] DESC";
                dataGridViewProducts.Refresh();
            }
        }

        // ==================== ОБРАБОТЧИКИ СОБЫТИЙ ====================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbFilterMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnSortAsc_Click(object sender, EventArgs e)
        {
            SortByPriceAscending();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnSortDesc_Click(object sender, EventArgs e)
        {
            SortByPriceDescending();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetFilters();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
            ResetFilters();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string nextArticle = GetNextArticle();
            ProductEditForm editForm = new ProductEditForm(null, connection, nextArticle);
            editForm.FormClosed += (s, args) => LoadProducts();
            editForm.ShowDialog();
        }

        private string GetNextArticle()
        {
            try
            {
                string query = "SELECT COALESCE(MAX(CAST(article AS UNSIGNED)), 0) + 1 FROM products";
                using (var cmd = new MySqlCommand(query, connection))
                    return cmd.ExecuteScalar().ToString();
            }
            catch
            {
                return "1";
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                DataRowView rowView = dataGridViewProducts.SelectedRows[0].DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    ProductEditForm editForm = new ProductEditForm(row, connection);
                    editForm.FormClosed += (s, args) => LoadProducts();
                    editForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                DataRowView rowView = dataGridViewProducts.SelectedRows[0].DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    int productId = Convert.ToInt32(row["id"]);
                    string productName = row["Название"].ToString();
                    string article = row["Артикул"].ToString();

                    DialogResult result = MessageBox.Show(
                        $"Вы действительно хотите удалить товар:\nАртикул: {article}\nНазвание: {productName}?",
                        "Подтверждение удаления",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            string checkOrdersQuery = "SELECT COUNT(*) FROM orders WHERE product_id = @productId";
                            MySqlCommand checkCmd = new MySqlCommand(checkOrdersQuery, connection);
                            checkCmd.Parameters.AddWithValue("@productId", productId);
                            int orderCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (orderCount > 0)
                            {
                                MessageBox.Show("Нельзя удалить товар, который есть в заказах. Сначала удалите или измените заказы.",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string imagePath = row["ImagePath"]?.ToString();

                            string deleteQuery = "DELETE FROM products WHERE id = @productId";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection);
                            deleteCmd.Parameters.AddWithValue("@productId", productId);
                            deleteCmd.ExecuteNonQuery();

                            // Удаляем файл изображения, если он находится в папке IMAGES_FOLDER
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                string fullPath = GetFullImagePath(imagePath);
                                if (File.Exists(fullPath) && fullPath.Contains(IMAGES_FOLDER))
                                {
                                    try
                                    {
                                        File.Delete(fullPath);
                                    }
                                    catch { }
                                }
                            }

                            MessageBox.Show("Товар успешно удален", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProducts();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataGridViewProducts.Columns[e.ColumnIndex].Name == "Изображение" && e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];
                    string imagePath = "";
                    if (row.Cells["ImagePath"].Value != null && row.Cells["ImagePath"].Value != DBNull.Value)
                    {
                        imagePath = row.Cells["ImagePath"].Value.ToString();
                    }

                    Image productImage = LoadProductImage(imagePath);
                    e.Value = productImage;
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                e.Value = new Bitmap(defaultProductImage, new Size(100, 100));
                e.FormattingApplied = true;
            }
        }

        private void dataGridViewProducts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (dataGridViewProducts.Columns[e.ColumnIndex].Name == "Изображение")
                {
                    string imagePath = "";
                    if (dataGridViewProducts.Rows[e.RowIndex].Cells["ImagePath"].Value != null)
                    {
                        imagePath = dataGridViewProducts.Rows[e.RowIndex].Cells["ImagePath"].Value.ToString();
                    }

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string fullPath = GetFullImagePath(imagePath);
                        if (File.Exists(fullPath))
                        {
                            System.Diagnostics.Process.Start(fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Файл изображения не найден.", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Изображение отсутствует.", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dataGridViewProducts_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridViewProducts.Columns[e.ColumnIndex].Name == "Изображение")
                {
                    string imagePath = "";
                    if (dataGridViewProducts.Rows[e.RowIndex].Cells["ImagePath"].Value != null)
                    {
                        imagePath = dataGridViewProducts.Rows[e.RowIndex].Cells["ImagePath"].Value.ToString();
                    }

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string fullPath = GetFullImagePath(imagePath);
                        if (File.Exists(fullPath))
                        {
                            ToolTip toolTip = new ToolTip();
                            toolTip.SetToolTip(dataGridViewProducts, "Правый клик для просмотра");
                        }
                    }
                }
            }
        }

        private void MerchandiserForm_OldFormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}



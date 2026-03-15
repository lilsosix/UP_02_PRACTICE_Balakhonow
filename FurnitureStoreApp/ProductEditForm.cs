using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class ProductEditForm : Form
    {
        private DataRow productData;
        private MySqlConnection connection;
        private bool isEditMode;
        private DataTable categoriesTable;
        private DataTable materialsTable;

        // Константы для работы с изображениями
        private const string IMAGES_FOLDER = "ProductImages";
        private string currentImagePath; // Текущий путь к изображению в БД (относительный)

        public ProductEditForm(DataRow data, MySqlConnection conn, string autoArticle = null)
        {
            InitializeComponent();
            productData = data;
            connection = conn;
            isEditMode = (data != null);

            // Автоартикул для нового товара
            if (!isEditMode && autoArticle != null)
                txtArticle.Text = autoArticle;

            // Создаем папку для изображений, если её нет
            CreateImageFolder();

            InitializeForm();
            LoadCategoriesAndMaterials();

            // Заголовок
            Label lbl = new Label();
            lbl.Text = "Редактирование товара";
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Arial", 36, FontStyle.Bold);
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            lbl.AutoSize = false;
            pictureBox2.Controls.Add(lbl);

            // Запрещаем ввод текста в комбобоксы
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterial.DropDownStyle = ComboBoxStyle.DropDownList;

            // Ограничения длины согласно БД
            txtArticle.MaxLength     = 50;
            txtName.MaxLength        = 200;
            txtSizes.MaxLength       = 50;
            txtDescription.MaxLength = 500;

            // Подписываемся на событие изменения текста размеров
            txtSizes.TextChanged += txtSizes_TextChanged;

            // Realtime-подсветка обязательных полей
            txtArticle.TextChanged  += (s, e) => SetFieldState(txtArticle,  txtArticle.Text.Trim().Length > 0);
            txtName.TextChanged     += (s, e) => SetFieldState(txtName,     txtName.Text.Trim().Length > 0);
            txtPrice.TextChanged    += (s, e) => SetFieldState(txtPrice,
                decimal.TryParse(txtPrice.Text.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal p) && p > 0);
        }

        private void CreateImageFolder()
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, IMAGES_FOLDER);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании папки для изображений: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeForm()
        {
            if (isEditMode)
            {
                this.Text = "Редактирование товара";
                txtArticle.Text = productData["Артикул"].ToString();
                txtName.Text = productData["Название"].ToString();

                // Получаем цену: сначала пробуем из столбца PriceValue (число)
                if (productData.Table.Columns.Contains("PriceValue") && productData["PriceValue"] != DBNull.Value)
                {
                    decimal price = Convert.ToDecimal(productData["PriceValue"]);
                    txtPrice.Text = price.ToString("0.00");
                }
                else
                {
                    // Если нет числового столбца, парсим строку (запасной вариант)
                    string priceStr = productData["Цена"].ToString().Replace(" ₽", "").Trim();
                    if (decimal.TryParse(priceStr, out decimal price))
                    {
                        txtPrice.Text = price.ToString("0.00");
                    }
                    else
                    {
                        txtPrice.Text = "";
                    }
                }

                txtSizes.Text = productData["Размеры"].ToString();
                txtDescription.Text = productData["Описание"].ToString();

                // Загружаем путь к изображению из БД
                if (productData.Table.Columns.Contains("ImagePath") && productData["ImagePath"] != DBNull.Value)
                {
                    currentImagePath = productData["ImagePath"].ToString();
                    txtImagePath.Text = currentImagePath;
                    LoadImageToPictureBox(currentImagePath);
                }
                else
                {
                    currentImagePath = null;
                    txtImagePath.Text = "";
                    pictureBoxPreview.Image = null;
                }

                // Устанавливаем выбранную категорию и материал
                string categoryName = productData["Категория"].ToString();
                string materialName = productData["Материал"].ToString();

                cmbCategory.Tag = categoryName;
                cmbMaterial.Tag = materialName;
            }
            else
            {
                this.Text = "Добавление нового товара";
                currentImagePath = null;
                pictureBoxPreview.Image = null;
            }
        }

        private void LoadImageToPictureBox(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath))
                {
                    pictureBoxPreview.Image = null;
                    return;
                }

                string fullPath = GetFullImagePath(imagePath);

                if (File.Exists(fullPath))
                {
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        Image img = Image.FromStream(fs);
                        pictureBoxPreview.Image = new Bitmap(img, new Size(200, 200));
                    }
                }
                else
                {
                    pictureBoxPreview.Image = null;
                    MessageBox.Show("Файл изображения не найден. Выберите новое изображение.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                pictureBoxPreview.Image = null;
                MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetFullImagePath(string relativeOrAbsolutePath)
        {
            if (Path.IsPathRooted(relativeOrAbsolutePath))
                return relativeOrAbsolutePath;
            return Path.Combine(Application.StartupPath, relativeOrAbsolutePath);
        }

        private void LoadCategoriesAndMaterials()
        {
            try
            {
                string categoriesQuery = "SELECT id, name FROM product_categories ORDER BY name";
                MySqlDataAdapter categoriesAdapter = new MySqlDataAdapter(categoriesQuery, connection);
                categoriesTable = new DataTable();
                categoriesAdapter.Fill(categoriesTable);

                cmbCategory.DataSource = categoriesTable;
                cmbCategory.DisplayMember = "name";
                cmbCategory.ValueMember = "id";

                string materialsQuery = "SELECT id, name FROM materials ORDER BY name";
                MySqlDataAdapter materialsAdapter = new MySqlDataAdapter(materialsQuery, connection);
                materialsTable = new DataTable();
                materialsAdapter.Fill(materialsTable);

                cmbMaterial.DataSource = materialsTable;
                cmbMaterial.DisplayMember = "name";
                cmbMaterial.ValueMember = "id";

                if (isEditMode)
                {
                    string categoryName = cmbCategory.Tag as string;
                    if (!string.IsNullOrEmpty(categoryName))
                    {
                        foreach (DataRowView item in cmbCategory.Items)
                        {
                            if (item["name"].ToString() == categoryName)
                            {
                                cmbCategory.SelectedItem = item;
                                break;
                            }
                        }
                    }

                    string materialName = cmbMaterial.Tag as string;
                    if (!string.IsNullOrEmpty(materialName))
                    {
                        foreach (DataRowView item in cmbMaterial.Items)
                        {
                            if (item["name"].ToString() == materialName)
                            {
                                cmbMaterial.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFieldState(Control field, bool valid)
        {
            field.BackColor = valid
                ? System.Drawing.Color.FromArgb(230, 255, 230)
                : System.Drawing.Color.FromArgb(255, 220, 220);
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Введите артикул товара", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtArticle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название товара", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // Проверка цены: должна быть положительным числом
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену (положительное число)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию товара", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (cmbMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите материал товара", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMaterial.Focus();
                return false;
            }

            return true;
        }

        private bool CheckArticleExists(string article, int? excludeProductId = null)
        {
            try
            {
                string query;
                MySqlCommand cmd;

                if (excludeProductId.HasValue)
                {
                    query = "SELECT COUNT(*) FROM products WHERE article = @article AND id != @productId";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@productId", excludeProductId.Value);
                }
                else
                {
                    query = "SELECT COUNT(*) FROM products WHERE article = @article";
                    cmd = new MySqlCommand(query, connection);
                }

                cmd.Parameters.AddWithValue("@article", article);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки артикула: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Выберите изображение товара";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Проверка размера: не более 3 МБ
                        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                        if (fileInfo.Length > 3 * 1024 * 1024)
                        {
                            MessageBox.Show("Размер изображения не должен превышать 3 МБ.\nВыберите другой файл.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string extension = Path.GetExtension(openFileDialog.FileName);
                        string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}{extension}";
                        string relativePath = Path.Combine(IMAGES_FOLDER, fileName);
                        string fullPath = Path.Combine(Application.StartupPath, relativePath);

                        File.Copy(openFileDialog.FileName, fullPath, true);

                        txtImagePath.Text = relativePath;
                        LoadImageToPictureBox(relativePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при копировании изображения: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string fullPath = GetFullImagePath(currentImagePath);
                if (File.Exists(fullPath) && fullPath.Contains(IMAGES_FOLDER))
                {
                    try
                    {
                        File.Delete(fullPath);
                    }
                    catch { }
                }
            }

            txtImagePath.Text = "";
            pictureBoxPreview.Image = null;
            currentImagePath = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                string article = txtArticle.Text.Trim();
                string name = txtName.Text.Trim();
                decimal price = decimal.Parse(txtPrice.Text);
                string sizes = txtSizes.Text.Trim();
                string description = txtDescription.Text.Trim();
                string imagePath = txtImagePath.Text.Trim();

                int categoryId = Convert.ToInt32((cmbCategory.SelectedItem as DataRowView)["id"]);
                int materialId = Convert.ToInt32((cmbMaterial.SelectedItem as DataRowView)["id"]);

                if (isEditMode)
                {
                    int productId = Convert.ToInt32(productData["id"]);
                    if (CheckArticleExists(article, productId))
                    {
                        MessageBox.Show("Товар с таким артикулом уже существует", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (CheckArticleExists(article))
                    {
                        MessageBox.Show("Товар с таким артикулом уже существует", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (isEditMode)
                {
                    UpdateProduct(article, name, price, sizes, description, imagePath, categoryId, materialId);
                }
                else
                {
                    CreateProduct(article, name, price, sizes, description, imagePath, categoryId, materialId);
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

        private void CreateProduct(string article, string name, decimal price, string sizes,
                                  string description, string imagePath, int categoryId, int materialId)
        {
            string query = @"
                INSERT INTO products (article, category_id, material_id, name, price, description, sizes, image_path) 
                VALUES (@article, @category_id, @material_id, @name, @price, @description, @sizes, @image_path)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            AddProductParameters(cmd, article, name, price, sizes, description, imagePath, categoryId, materialId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Товар успешно добавлен", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateProduct(string article, string name, decimal price, string sizes,
                                  string description, string imagePath, int categoryId, int materialId)
        {
            if (isEditMode && currentImagePath != imagePath && !string.IsNullOrEmpty(currentImagePath))
            {
                string oldFullPath = GetFullImagePath(currentImagePath);
                if (File.Exists(oldFullPath) && oldFullPath.Contains(IMAGES_FOLDER))
                {
                    try
                    {
                        File.Delete(oldFullPath);
                    }
                    catch { }
                }
            }

            string query = @"
                UPDATE products 
                SET article = @article, 
                    category_id = @category_id, 
                    material_id = @material_id,
                    name = @name, 
                    price = @price, 
                    description = @description, 
                    sizes = @sizes, 
                    image_path = @image_path
                WHERE id = @id";

            int productId = Convert.ToInt32(productData["id"]);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            AddProductParameters(cmd, article, name, price, sizes, description, imagePath, categoryId, materialId);
            cmd.Parameters.AddWithValue("@id", productId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Данные товара успешно обновлены", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddProductParameters(MySqlCommand cmd, string article, string name, decimal price,
                                         string sizes, string description, string imagePath,
                                         int categoryId, int materialId)
        {
            cmd.Parameters.AddWithValue("@article", article);
            cmd.Parameters.AddWithValue("@category_id", categoryId);
            cmd.Parameters.AddWithValue("@material_id", materialId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@description",
                string.IsNullOrWhiteSpace(description) ? DBNull.Value : (object)description);
            cmd.Parameters.AddWithValue("@sizes",
                string.IsNullOrWhiteSpace(sizes) ? DBNull.Value : (object)sizes);
            cmd.Parameters.AddWithValue("@image_path",
                string.IsNullOrWhiteSpace(imagePath) ? DBNull.Value : (object)imagePath);
        }

        // Ограничение на ввод только цифр для артикула
        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',' || e.KeyChar == '.') && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        // Обработчик для поля размеров: заменяем пробелы на 'x'
        private void txtSizes_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            int selectionStart = tb.SelectionStart;
            string text = tb.Text;

            if (text.Contains(" "))
            {
                tb.TextChanged -= txtSizes_TextChanged;
                string newText = text.Replace(" ", "x");
                tb.Text = newText;
                tb.SelectionStart = selectionStart;
                tb.SelectionLength = 0;
                tb.TextChanged += txtSizes_TextChanged;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
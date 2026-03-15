using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class UserCatalogForm : Form
    {
        private string connectionString = "server=localhost;database=your_database_name;uid=root;pwd=your_password;";
        private DataTable productsDataTable;
        private DataView productsDataView;

        public UserCatalogForm()
        {
            InitializeComponent();
            LoadProductsFromDatabase();
            SetupRealTimeFiltering();
        }

        private void LoadProductsFromDatabase()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.id_product,
                            p.product_name,
                            c.category_name,
                            m.shortened_name as material_name,
                            p.price,
                            p.description,
                            p.size
                        FROM Product p
                        LEFT JOIN Category c ON p.id_category = c.id_category
                        LEFT JOIN Materials m ON p.id_material = m.id_material";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    productsDataTable = new DataTable();
                    adapter.Fill(productsDataTable);

                    productsDataView = new DataView(productsDataTable);
                    dataGridViewProducts.DataSource = productsDataView;

                    // Настройка отображения столбцов
                    dataGridViewProducts.Columns["id_product"].HeaderText = "ID";
                    dataGridViewProducts.Columns["product_name"].HeaderText = "Название товара";
                    dataGridViewProducts.Columns["category_name"].HeaderText = "Категория";
                    dataGridViewProducts.Columns["material_name"].HeaderText = "Материал";
                    dataGridViewProducts.Columns["price"].HeaderText = "Цена";
                    dataGridViewProducts.Columns["description"].HeaderText = "Описание";
                    dataGridViewProducts.Columns["size"].HeaderText = "Размер";

                    // Автоподбор ширины столбцов
                    dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupRealTimeFiltering()
        {
            // Обработчики событий для реального времени
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cmbCategoryFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbPriceSort.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (productsDataView == null) return;

            string searchFilter = "";
            string categoryFilter = "";
            string sortOrder = "";

            // Фильтр по поиску
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                searchFilter = $"product_name LIKE '%{txtSearch.Text}%' OR description LIKE '%{txtSearch.Text}%'";
            }

            // Фильтр по категории
            if (cmbCategoryFilter.SelectedItem != null && cmbCategoryFilter.SelectedItem.ToString() != "Все категории")
            {
                categoryFilter = $"category_name = '{cmbCategoryFilter.SelectedItem}'";
            }

            // Комбинирование фильтров
            string finalFilter = "";
            if (!string.IsNullOrEmpty(searchFilter) && !string.IsNullOrEmpty(categoryFilter))
            {
                finalFilter = $"{searchFilter} AND {categoryFilter}";
            }
            else if (!string.IsNullOrEmpty(searchFilter))
            {
                finalFilter = searchFilter;
            }
            else if (!string.IsNullOrEmpty(categoryFilter))
            {
                finalFilter = categoryFilter;
            }

            productsDataView.RowFilter = finalFilter;

            // Сортировка по цене
            if (cmbPriceSort.SelectedItem != null)
            {
                switch (cmbPriceSort.SelectedItem.ToString())
                {
                    case "По возрастанию цены":
                        productsDataView.Sort = "price ASC";
                        break;
                    case "По убыванию цены":
                        productsDataView.Sort = "price DESC";
                        break;
                    default:
                        productsDataView.Sort = "";
                        break;
                }
            }
        }

        private void LoadCategoryFilter()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT category_name FROM Category";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    cmbCategoryFilter.Items.Clear();
                    cmbCategoryFilter.Items.Add("Все категории");

                    while (reader.Read())
                    {
                        cmbCategoryFilter.Items.Add(reader["category_name"].ToString());
                    }

                    cmbCategoryFilter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserCatalogForm_Load(object sender, EventArgs e)
        {
            LoadCategoryFilter();

            // Заполнение комбобокса сортировки
            cmbPriceSort.Items.Add("Без сортировки");
            cmbPriceSort.Items.Add("По возрастанию цены");
            cmbPriceSort.Items.Add("По убыванию цены");
            cmbPriceSort.SelectedIndex = 0;
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            UserCartForm cartForm = new UserCartForm();
            cartForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbCategoryFilter.SelectedIndex = 0;
            cmbPriceSort.SelectedIndex = 0;
        }

        private void UserCatalogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
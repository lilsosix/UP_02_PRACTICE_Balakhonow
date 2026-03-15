using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class AdminRestoreForm : Form
    {
        private string connectionString = DatabaseConnection.ConnectionString;

        public AdminRestoreForm()
        {
            InitializeComponent();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Восстановление удалит все существующие данные и пересоздаст структуру БД.\n\nПродолжить?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    ExecuteRestore(conn);
                }

                txtLog.AppendText("✔ Структура БД успешно восстановлена.\r\n");
                MessageBox.Show("Структура базы данных восстановлена.\nДанные в таблицах отсутствуют.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                txtLog.AppendText("✘ Ошибка: " + ex.Message + "\r\n");
                MessageBox.Show("Ошибка при восстановлении:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteRestore(MySqlConnection conn)
        {
            // Каждый SQL выполняется отдельно — MySqlCommand не поддерживает batch напрямую
            string[] statements = BuildRestoreScript();
            foreach (string sql in statements)
            {
                if (string.IsNullOrWhiteSpace(sql)) continue;
                txtLog.AppendText("» " + sql.Trim().Split('\n')[0].Trim() + "...\r\n");
                Application.DoEvents(); // обновляем UI в процессе
                using (var cmd = new MySqlCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        private string[] BuildRestoreScript()
        {
            return new[]
            {
                // ── Отключаем проверку внешних ключей на время пересоздания ──
                "SET FOREIGN_KEY_CHECKS = 0",

                // ── Удаляем таблицы (зависимые сначала) ─────────────────────
                "DROP TABLE IF EXISTS order_items",
                "DROP TABLE IF EXISTS orders",
                "DROP TABLE IF EXISTS products",
                "DROP TABLE IF EXISTS clients",
                "DROP TABLE IF EXISTS users",
                "DROP TABLE IF EXISTS roles",
                "DROP TABLE IF EXISTS product_categories",
                "DROP TABLE IF EXISTS materials",
                "DROP TABLE IF EXISTS order_statuses",
                "DROP TABLE IF EXISTS delivery_methods",

                // ── Создаём справочные таблицы ───────────────────────────────
                @"CREATE TABLE roles (
                    id   INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE product_categories (
                    id   INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE materials (
                    id   INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE order_statuses (
                    id   INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE delivery_methods (
                    id   INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(100) NOT NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                // ── Создаём основные таблицы ─────────────────────────────────
                @"CREATE TABLE users (
                    id        INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    full_name VARCHAR(150) NOT NULL,
                    login     VARCHAR(50)  NOT NULL UNIQUE,
                    password  VARCHAR(255) NOT NULL,
                    role_id   INT          NOT NULL,
                    CONSTRAINT fk_users_role FOREIGN KEY (role_id) REFERENCES roles(id)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE clients (
                    id                  INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    full_name           VARCHAR(150) NOT NULL,
                    phone               VARCHAR(20)  NULL,
                    passport_series     VARCHAR(4)   NULL,
                    passport_number     VARCHAR(6)   NULL,
                    passport_issue_date DATE         NULL,
                    division_code       VARCHAR(10)  NULL,
                    address             VARCHAR(255) NULL
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE products (
                    id          INT            NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    article     VARCHAR(50)    NOT NULL UNIQUE,
                    name        VARCHAR(200)   NOT NULL,
                    category_id INT            NULL,
                    material_id INT            NULL,
                    dimensions  VARCHAR(50)    NULL,
                    price       DECIMAL(10,2)  NOT NULL DEFAULT 0,
                    description TEXT           NULL,
                    image       LONGBLOB       NULL,
                    CONSTRAINT fk_products_category FOREIGN KEY (category_id) REFERENCES product_categories(id),
                    CONSTRAINT fk_products_material FOREIGN KEY (material_id) REFERENCES materials(id)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE orders (
                    id              INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    order_date      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    completion_date DATE          NULL,
                    client_id       INT           NULL,
                    status_id       INT           NOT NULL,
                    delivery_id     INT           NULL,
                    user_id         INT           NULL,
                    prepayment      DECIMAL(10,2) NOT NULL DEFAULT 0,
                    total_cost      DECIMAL(10,2) NOT NULL DEFAULT 0,
                    CONSTRAINT fk_orders_client   FOREIGN KEY (client_id)   REFERENCES clients(id),
                    CONSTRAINT fk_orders_status   FOREIGN KEY (status_id)   REFERENCES order_statuses(id),
                    CONSTRAINT fk_orders_delivery FOREIGN KEY (delivery_id) REFERENCES delivery_methods(id),
                    CONSTRAINT fk_orders_user     FOREIGN KEY (user_id)     REFERENCES users(id)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                @"CREATE TABLE order_items (
                    id         INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
                    order_id   INT           NOT NULL,
                    product_id INT           NOT NULL,
                    quantity   INT           NOT NULL DEFAULT 1,
                    price      DECIMAL(10,2) NOT NULL,
                    cost       DECIMAL(10,2) NOT NULL,
                    CONSTRAINT fk_items_order   FOREIGN KEY (order_id)   REFERENCES orders(id)   ON DELETE CASCADE,
                    CONSTRAINT fk_items_product FOREIGN KEY (product_id) REFERENCES products(id)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4",

                // ── Заполняем справочники начальными значениями ──────────────
                "INSERT INTO roles (name) VALUES ('Товаровед'), ('Менеджер'), ('Администратор')",
                "INSERT INTO order_statuses (name) VALUES ('Принят'), ('Отменен'), ('В пути'), ('Доставлен'), ('Завершён')",
                "INSERT INTO delivery_methods (name) VALUES ('Самовывоз'), ('Курьером')",
                "INSERT INTO product_categories (name) VALUES ('Диваны'), ('Кресла'), ('Кровати'), ('Столы'), ('Стулья'), ('Шкафы')",
                "INSERT INTO materials (name) VALUES ('Дерево'), ('Металл'), ('Ткань'), ('Кожа'), ('Пластик'), ('Стекло')",

                // ── Включаем обратно проверку внешних ключей ────────────────
                "SET FOREIGN_KEY_CHECKS = 1",
            };
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}

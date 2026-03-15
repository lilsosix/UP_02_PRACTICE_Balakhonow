using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Централизованное управление строкой подключения к БД.
    /// Все формы берут строку подключения отсюда — менять нужно только здесь.
    /// </summary>
    public static class DatabaseConnection
    {
        public static string ConnectionString =
            "server=localhost;database=furniture_store;uid=root;pwd=;";

        /// <summary>
        /// Создаёт и открывает новое соединение. Возвращает null при ошибке.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            try
            {
                var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    $"Не удалось подключиться к базе данных:\n{ex.Message}",
                    "Ошибка подключения",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

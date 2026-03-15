using System;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Хранит данные авторизованного пользователя на время сеанса работы.
    /// </summary>
    internal static class CurrentSession
    {
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static string Login { get; set; }
        public static int RoleId { get; set; }
        public static string RoleName { get; set; }

        /// <summary>
        /// Сбрасывает данные сессии при выходе из учётной записи.
        /// </summary>
        public static void Clear()
        {
            UserId = 0;
            FullName = string.Empty;
            Login = string.Empty;
            RoleId = 0;
            RoleName = string.Empty;
        }
    }
}

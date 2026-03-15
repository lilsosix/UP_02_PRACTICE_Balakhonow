using System;
using System.Security.Cryptography;
using System.Text;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Утилита хэширования паролей (SHA-256), совместимая с хэшами в БД.
    /// </summary>
    internal static class PasswordHelper
    {
        /// <summary>
        /// Возвращает SHA-256 хэш строки в нижнем регистре.
        /// </summary>
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}

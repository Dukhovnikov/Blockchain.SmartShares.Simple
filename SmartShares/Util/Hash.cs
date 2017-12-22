using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable once CheckNamespace
namespace SmartShares
{
    /// <summary>
    /// Класс для вычисления хэша из набора данных
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Вычисление хэша из массива байтов
        /// </summary>
        public static byte[] ComputeSha256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(bytes);
        }
        /// <summary>
        /// Вычисление хэша из строки
        /// </summary>     
        public static byte[] ComputeSha256FromString(string inputString)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(inputString);
                return sha256.ComputeHash(bytes);
            }
        }
    }
}
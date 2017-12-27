using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartShares
{
    /// <summary>
    /// Конвертирование хэша
    /// </summary>
    public static class HexConvert
    {
        public static byte[] ToBytes(string s)
        {
            var result = Convert.FromBase64String(s);

            return result;
        }

        public static string FromBytes(byte[] bytes)
        {
            var result = Convert.ToBase64String(bytes);

            return result;
        }
    }
}
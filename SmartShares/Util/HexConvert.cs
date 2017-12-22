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
        ///// <summary>
        ///// Конвертирование хэша в нобор байтов
        ///// </summary>
        //public static byte[] ToBytes(string hex)
        //{
        //    var ret = new byte[hex.Length / 2];
        //    for (var i = 0; i < hex.Length; i += 2)
        //        ret[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    return ret;
        //}
        ///// <summary>
        ///// Конвертирование хэша в строку
        ///// </summary>
        //public static string FromBytes(IEnumerable<byte> bytes) =>
        //    string.Join("", bytes.Select(b => $"{b:x2}"));

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
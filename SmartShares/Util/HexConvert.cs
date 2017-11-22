using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartShares.Util
{
    public static class HexConvert
    {
        public static byte[] ToBytes(string hex)
        {
            var ret = new byte[hex.Length / 2];
            for (var i = 0; i < hex.Length; i += 2)
                ret[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return ret;
        }

        public static string FromBytes(IEnumerable<byte> bytes) =>
            string.Join("", bytes.Select(b => $"{b:x2}"));
    }
}
using System.Security.Cryptography;
using System.Text;

namespace SmartShares.Util
{
    public static class Hash
    {
        public static byte[] ComputeSha256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(bytes);
        }
        
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
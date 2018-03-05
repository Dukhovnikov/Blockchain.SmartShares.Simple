using System.Security.Cryptography;
using MessagePack;

namespace SmartShares
{
    public static class EccService
    {
        /// <summary>
        /// Получение именованной кривой nistP256.
        /// </summary>
        private static readonly ECCurve Curve = ECCurve.NamedCurves.nistP256;
        
        /// <summary>
        /// Генерация приватного и публичного ключей.
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public static void GenerateKey(out byte[] privateKey, out byte[] publicKey)
        {
            //Представляет стандартные параметры для алгоритма шифрования на основе эллиптических кривых (ECC).
            ECParameters param;
            
            //Создает новый экземпляр заданной реализации из эллиптических кривых цифровой подписи алгоритма (ECDSA).
            using (var dsa = ECDsa.Create(Curve))
                param = dsa.ExportParameters(true);
            
            privateKey = param.D;
            publicKey = ToBytes(param.Q);
        }

        /// <summary>
        /// Вычисление цифровой подписи для данных.
        /// </summary>
        public static byte[] Sign(byte[] hash, byte[] privateKey, byte[] publicKey)
        {
            var param = new ECParameters()
            {
                D = privateKey,
                Q = ToEcPoint(publicKey),
                Curve = Curve
            };

            using (var dsa = ECDsa.Create(param))
                return dsa.SignHash(hash);
        }

        /// <summary>
        /// Верификация подписанных данных. 
        /// </summary>
        public static bool Verify(byte[] hash, byte[] signature, byte[] publicKey)
        {
            var param = new ECParameters()
            {
                Q = ToEcPoint(publicKey),
                Curve = Curve
            };

            using (var dsa = ECDsa.Create(param))
                return dsa.VerifyHash(hash, signature);
        }

        /// <summary>
        /// Проверка связки ключей.
        /// </summary>
        public static bool TestKey(byte[] privateKey, byte[] publicKey)
        {
            byte[] testHash;

            using (var sha = SHA256.Create())
                testHash = sha.ComputeHash(new byte[0]);

            try
            {
                var signature = Sign(testHash, privateKey, publicKey);
                return Verify(testHash, signature, publicKey);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Конвертация публичного ключа в массив байтов.
        /// </summary>
        private static byte[] ToBytes(ECPoint point)
        {
            return MessagePackSerializer.Serialize(
                new FormattableEcPoint {X = point.X, Y = point.Y});
        }        

        /// <summary>
        /// Конвертация публичного ключа в стандартный тип (ECPoint class).
        /// </summary>
        private static ECPoint ToEcPoint(byte[] bytes)
        {
            var ecPoint = MessagePackSerializer.Deserialize<FormattableEcPoint>(bytes);
            return new ECPoint {X = ecPoint.X, Y = ecPoint.Y};
        }      
        
        [MessagePackObject]
        // ReSharper disable once MemberCanBePrivate.Global
        public sealed class FormattableEcPoint
        {
            [Key(0)]
            public byte[] X { get; set; }
            
            [Key(1)]
            public byte[] Y { get; set; }
        }
    }
}
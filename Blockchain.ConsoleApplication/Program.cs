using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input the text:");
            var hash = SHAconstuctor.GetHashSha256(Console.ReadLine());

            Console.WriteLine("Hash was generated: {0}", hash);
            Console.ReadKey();
        }
    }

    public class SHAconstuctor
    {
        public static string GetHashSha256(string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);

            return hash.Aggregate(string.Empty, (current, variable) => current + $"{variable:x2}");
        }
    }

//    Цифровые подписи основаны на асимметричном шифровании. 
//    Цифровая подпись обеспечивает приватность сообщения и доказывает принадлежность 
//    сообщения тому или иному автору. Обычно это работает следующим образом:

//    1. Алиса шифрует своё сообщение.
//    2. Алиса берёт хэш своего сообщения.
//    3. Алиса подписывает сообщение своим приватным ключом для подписывания.
//    4. Алиса посылает зашифрованные данные, хэш и подпись Бобу.
//    5. Боб вычисляет хэш зашифрованных данных.
//    6. Боб проверяет подпись, используя публичный ключ.


    /// <summary>
    /// Пример использования подписи
    /// </summary>
    public class DigitalSignature
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public byte[] SignData(byte[] hashOfDataToSign)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_publicKey);
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }
    }

    internal class Class1
    {
        private static void DigitalSignature()
        {
            //The hash value to sign. 
            byte[] HashValue =
                {59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135};
            //The value to hold the signed value. 
            byte[] SignedHashValue;
            //Generate a public/private key pair. 
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //Create an RSAPKCS1SignatureFormatter object and pass it the 
            //RSACryptoServiceProvider to transfer the private key. 
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            //Set the hash algorithm to SHA1. 
            RSAFormatter.SetHashAlgorithm("SHA1");
            //Create a signature for HashValue and assign it to 
            //SignedHashValue. 
            SignedHashValue = RSAFormatter.CreateSignature(HashValue);
        }
    }

    internal class VerifySignature
    {
        public void VerViod(byte[] HashValue, byte[] SignedHashValue)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //RSA.ImportParameters(RSAKeyInfo);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
            RSADeformatter.SetHashAlgorithm("SHA1");

            if (RSADeformatter.VerifySignature(HashValue, SignedHashValue))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }

        }
    }
}

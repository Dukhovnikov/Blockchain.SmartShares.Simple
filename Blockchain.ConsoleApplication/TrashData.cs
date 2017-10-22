using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Blockchain.SmartShares;
using Newtonsoft.Json;

namespace Blockchain.ConsoleApplication
{
    public class TrashData
    {

        public void SendMessage()
        {
            const int port = 8888; // порт для прослушивания подключений
            
            var testBlock = new Block(1, DateTime.Now, new Hash("testHash"));
            var jsonBlock = JsonConvert.SerializeObject(testBlock);
            
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
 
                // запуск слушателя
                server.Start();
 
                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");
 
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");
 
                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();
 
                    // сообщение для отправки клиенту
                    //string response = "Пшел нахуй";
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(jsonBlock);
 
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    //Console.WriteLine("Отправлено сообщение: {0}", response);
                    // закрываем подключение
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
        
        /*            var testBlock = new Block(1, DateTime.Now, new Hash("testHash"));
            string jsonBlock = JsonConvert.SerializeObject(testBlock);
            
            //Console.WriteLine("Hash generated: {0}", new Hash("testLines"));
            Console.WriteLine("input localport:");
            var localport = Console.ReadLine();
            
            Console.WriteLine("input remoteport:");
            var remoteport = Console.ReadLine();
            
            

            var receiveThread = new Thread(new ThreadStart(Connecter.ReceiveMessage));
            receiveThread.Start();
            
            //var peer = new Connecter(int.Parse(localport), int.Parse(remoteport), "172.0.0.1");
            //peer.SendMessage(Encoding.UTF8.GetBytes(jsonBlock));

            Console.ReadKey();*/
        
        
        /*
    public static class KeyGen
    {
        public static void GenerateKey(out byte[] privateKey, out byte[] publicKey)
        {
            var Curve = ECCurve.NamedCurves.nistP256;
            ECParameters param;
            using (var dsa = ECDsa.Create(Curve))
                param = dsa.ExportParameters(true);

            privateKey = param.D;
            //publicKey = ToBytes(param.Q);
        }
    }
*/

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
}
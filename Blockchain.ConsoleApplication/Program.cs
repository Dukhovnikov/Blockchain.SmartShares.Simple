using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Blockchain.SmartShares;
using Newtonsoft.Json;

namespace Blockchain.ConsoleApplication
{
    /// <summary>
    /// Server class
    /// </summary>
    internal static class Program
    {
        const int port = 8888; // порт для прослушивания подключений
        
        public static void Main(string[] args)
        {
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

        }
    }
}

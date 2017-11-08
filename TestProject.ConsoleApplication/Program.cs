﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Blockchain.ConsoleApplication;
using Blockchain.SmartShares;
using MessagePack;
using Newtonsoft.Json;

namespace TestProject.ConsoleApplication
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var pathtokeypair = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var genesisBlock = Genesis.GenerateGenesisBlock(
                FileManager.CombainPath(pathtokeypair, FileManager.FileTypeofBlockchain.KeyPairs)); 
            
            var blockchain = new Blockchain.SmartShares.Blockchain()
            {             
                blocks = new Dictionary<byte[], Block>()
                {
                    { genesisBlock.Hash, genesisBlock}
                }
            };

            var serialaizeBlockchain = JsonConvert.SerializeObject(blockchain, Formatting.Indented);
            
            var mpBlockchain = MessagePackSerializer.Serialize(blockchain);

            var block = MessagePackSerializer.Deserialize<Blockchain.SmartShares.Blockchain>(mpBlockchain);
            //var serialaizeBlockchain = MessagePackSerializer.ToJson(mpBlockchain);


/*            File.WriteAllText(
                FileManager.CombainPath(pathtokeypair, FileManager.FileTypeofBlockchain.Blockchain), 
                serialaizeBlockchain);*/
            Console.ReadKey();

        }
    }

}



#region Trash was created
/*private const int port = 8888;
private const string server = "127.0.0.1";*/

/*            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                 
                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();
                 
                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable); // пока данные есть в потоке
             
                Console.WriteLine("Полученные данные:");
                Console.WriteLine(response.ToString());

                var transiverBlock = JsonConvert.DeserializeObject<MPBlock>(response.ToString());
                // Закрываем потоки
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
 
            Console.WriteLine("Запрос завершен...");
            Console.Read();*/
            
/*            var testBlock = new Block(1, DateTime.Now, new Hash("testHash"));
            string jsonBlock = JsonConvert.SerializeObject(testBlock);
            
            //Console.WriteLine("Hash generated: {0}", new Hash("testLines"));
            Console.WriteLine("input localport:");
            var localport = Console.ReadLine();
            
            Console.WriteLine("input remoteport:");
            var remoteport = Console.ReadLine();
            
            

            //var receiveThread = new Thread(new ThreadStart(Connecter.ReceiveMessage));
            //receiveThread.Start();
            
            var peer = new Connecter(int.Parse(localport), int.Parse(remoteport), "172.0.0.1");
            peer.SendMessage(Encoding.UTF8.GetBytes(jsonBlock));

            Console.ReadKey();*/
#endregion
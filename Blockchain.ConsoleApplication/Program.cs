using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Blockchain.SmartShares;
using Blockchain.SmartShares.Network;
using Blockchain.SmartShares.OtherLogics;


namespace Blockchain.ConsoleApplication
{
    internal static class Program
    {       
        [STAThread]
        public static void Main(string[] args)
        {
            TestRunner.Run();
/*            var blockchain =
                FileManager.LoadBlockchain(
                    FileManager.CombainPath(
                        FileManager.FileTypeofBlockchain.Blockchain));

            var keyPair = KeyPair.LoadFrom(FileManager.CombainPath(FileManager.FileTypeofBlockchain.KeyPair));
                
            var coinPocket = new CoinPocket()
            {
                KeyPair = keyPair,
                Amount = CoinPocketManager.ParseFromBlockain(blockchain, keyPair.PublicKey)
            };
            
            ConsoleWorker.WriteDataCoinPocket(coinPocket);*/

            #region Работает         

/*            try
            {
                Console.Write("Введите порт для прослушивания: ");
                var localPort = int.Parse(Console.ReadLine());
                
                Console.Write("Введите удаленный порт для подключения: ");
                var remotePort = int.Parse(Console.ReadLine());
                
                ConnectionManager.TransmitterPort = remotePort;
                ConnectionManager.ReceiverPort = localPort;
                
/*                var receiveThread = new Thread(new ThreadStart(ConnectionManager.ReceiveMessage));
                receiveThread.Start();#1#

                var receiveMessage = ConnectionManager.ReceiveMessageAsync(new UdpClient(localPort));
                
                Console.Write("Введите сообщение для отправки: ");
                var testMessage = Hash.ComputeSha256FromString(Console.ReadLine());
                
                ConnectionManager.SendMessage(testMessage);
                
                Console.Write($"Принятое сообщение: {HexConvert.FromBytes(receiveMessage.Result)}");

                Console.ReadLine();
            }          
                
            catch (Exception e)
            {
                Console.WriteLine(e);
            }*/

            #endregion
        }

        public static void AddGenesisToBlockchainAndSave(string directoryData)
        {
            var genesisBlock =
                Genesis.GenerateGenesisBlock(FileManager.CombainPath(
                    FileManager.FileTypeofBlockchain.KeyPair));

            var Blockchain = new SmartShares.Blockchain()
            {
                blocks = new Dictionary<byte[], Block>()
                {
                    {genesisBlock.Hash, genesisBlock}
                }
            };

            var serializeBlockchain = BlockchainUtil.SerializeBlockchain(Blockchain);
            
            File.WriteAllBytes(FileManager.CombainPath(
                FileManager.FileTypeofBlockchain.Blockchain), 
                serializeBlockchain);
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using SmartShares;

namespace Blockchain.ConsoleApplication
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.GetEncoding(65001);

            //var pocket =
            //    JsonConvert.DeserializeObject<CoinPocket>(
            //        File.ReadAllText(DataManager.FileDictionary["Users"] + "\\Creator.txt"));
            //var keyPair = pocket.KeyPair;

            //var blockchain = Genesis.GenerateBlockchainGenesis(keyPair);

            //File.WriteAllText(DataManager.FileDictionary["Blockchain"], JsonConvert.SerializeObject(blockchain, Formatting.Indented));

            var block = new Block()
            {
                Id = 135,
                Hash = Hash.ComputeSha256FromString("blockhash"),
                PreviousHash = Hash.ComputeSha256FromString("testhash"),
                Timestamp = DateTime.Now,
                Nonce = 99
            };

            var hash = block.PreviousHash;

            var chain = new KeyValuePair<string, Block>(HexConvert.FromBytes(block.Hash), block);
            var message = BlockchainUtil.SerializeJsonByteChain(chain);

            //var dictionary = new Dictionary<string, Block>()
            //{
            //    {
            //        HexConvert.FromBytes(Hash.ComputeSha256FromString("testhash")),
            //        new Block()
            //        {
            //            Id = 135,
            //            Hash = Hash.ComputeSha256FromString("blockhash"),
            //            PreviousHash = Hash.ComputeSha256FromString("testhash"),
            //            Timestamp = DateTime.Now,
            //            Nonce = 99
            //        }
            //    }
            //};
            //var temp = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            //Console.WriteLine(JsonConvert.SerializeObject(dictionary, Formatting.Indented));

            //Console.WriteLine();

            //Console.WriteLine(HexConvert.FromBytes(Hash.ComputeSha256FromString("testhash")));
            //Console.WriteLine(Convert.ToBase64String(Hash.ComputeSha256FromString("testhash")));

            Console.ReadKey();

            ////var runner = new Runner();

            ////runner.Run();
            var senderUdpClient = new UdpClient();
            var transactionByte = Encoding.UTF8.GetBytes("sdsdadasdasdasdasd");

            try
            {

                senderUdpClient.Send(
                    message,
                    message.Length,
                    "127.0.0.1",
                    8889);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {
                senderUdpClient.Close();
            }
        }

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


        /*        public static void AddGenesisToBlockchainAndSave(string directoryData)
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
                }*/

        #endregion
    }
}

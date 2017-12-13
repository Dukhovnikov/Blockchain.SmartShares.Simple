using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace SmartShares
{
    public static class TestRunner
    {
        public static int getTrueHash()
        {          
            for (int i = 0; i < 100000; i++)
            {
                var dataHash = Hash.ComputeSha256FromString(i.ToString());
                var dataHashStr = HexConvert.FromBytes(dataHash); 

                if (dataHashStr[63].Equals('0'))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Run()
        {
            var check = true;

            var blockchain = new Dictionary<byte[], Block>();

            var task = new Task<byte[]>[1];
            while (check)
            {
                switch (Menu())
                {
                    case "1":
                        blockchain = Genesis.GenerateBlockchainGenesis();
                        break;
                            
                    case "2":
                        ChooseSomePorts();
                        task[0] = ConnectionManager.ReceiveMessageAsync();
                        break;
                        
                    case "3":
                        ConnectionManager.SendMessage(BlockchainUtil.SerializeBlockchain(blockchain));
                        break;
                        
                    case "4":
                        var receiveBlockchain = BlockchainUtil.DeserializeBlockchain(task[0].Result);
                        var receiveBlockchainJson = MessagePackSerializer.ToJson(receiveBlockchain);
                        
                        Console.WriteLine("Receive data:");
                        Console.WriteLine(receiveBlockchainJson);
                        break;

                    default:
                        check = false;
                        break;
                }
            }
        }
        
        private static string Menu()
        {
            Console.WriteLine("1. Generate blockchain");
            Console.WriteLine("2. Choose some ports fo client");
            Console.WriteLine("3. Send blockchain");
            Console.WriteLine("4. View sended data");

            return Console.ReadLine();
        }

        private static void ChooseSomePorts()
        {
            Console.WriteLine("1. User 1");
            Console.WriteLine("2. User 2");

            switch (Console.ReadLine())
            {
                case "1":
                    ConnectionManager.ReceiverPort = 8888;
                    ConnectionManager.TransmitterPort = 9999;
                    Console.WriteLine("User 1 have got contract");
                    break;
                case "2":
                    ConnectionManager.ReceiverPort = 9999;
                    ConnectionManager.TransmitterPort = 8888;
                    Console.WriteLine("User 2 have got contract");

                    break;
                default:
                    ConnectionManager.ReceiverPort = 8889;
                    ConnectionManager.TransmitterPort = 9998;
                    Console.WriteLine("User Default have got contract");
                    break;
            }

        }
        
        public static void CreateUserCreator()
        {
            EccService.GenerateKey(out var privateKey, out var publicKey);

            var coinPocket = new CoinPocket()
            {
                AmountDollars = 1000,
                ReceivePort = 8888,
                UserName = "Creator",
                KeyPair = new KeyPair()
                {
                    PrivateKey = privateKey,
                    PublicKey = publicKey
                }
            };
            
            var openFileDialog = new SaveFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new Exception("Intorrect choose data file");
            
            File.WriteAllText(openFileDialog.FileName, JsonConvert.SerializeObject(coinPocket, Formatting.Indented));
        }
        
        public static void CreateBlockchain()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new Exception("Intorrect choose data file");

            var text = File.ReadAllText(openFileDialog.FileName);

            var pocket = JsonConvert.DeserializeObject<CoinPocket>(text);

            var blockchain = Genesis.GenerateBlockchainGenesis(pocket.KeyPair);
            
            File.WriteAllBytes(FileManager.CombainPath(
                    FileManager.FileTypeofBlockchain.Blockchain),
                BlockchainUtil.SerializeBlockchain(blockchain));
        }
        
        public static void ViewBlockchain()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new Exception("Intorrect choose data file");

            var text = File.ReadAllBytes(openFileDialog.FileName);

            var blockchain = BlockchainUtil.DeserializeBlockchain(text);
            
            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));

        }
    }
}
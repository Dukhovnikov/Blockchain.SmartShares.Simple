using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Blockchain.SmartShares;
using Blockchain.SmartShares.OtherLogics;
using Newtonsoft.Json;

namespace Blockchain.ConsoleApplication
{
    public static class TestRunnerConsole
    {
        private static Dictionary<byte[], Block> Blocks { get; set; }
        private static KeyPair KeyPair { get; set; }

        public static void Run()
        {
            var check = true; 
            
            while (check)
            {
                switch (Menu())
                {
                    case "1":
                        var blockchain = UploadBlockchain();
                        Console.WriteLine("Completed:");
                        Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));
                        break;
                    case "2":
                        var keyPair = UploadKeyPair();
                        Console.WriteLine("Completed:");
                        Console.WriteLine(JsonConvert.SerializeObject(keyPair, Formatting.Indented));
                        break;
                    case "3":
                        Console.WriteLine("Completed:");
                        Console.WriteLine(GenerateKeyPair());
                        break;
                    case "4":
                        Console.WriteLine("Completed:");
                        Console.WriteLine(GenerateBlock());
                        break;
                    case "5":
                        ParseBlockchain();
                        break;
                    default:
                        check = false;
                        break;
                }
                Console.WriteLine();
            }
        }
        
        private static string Menu()
        {
            Console.WriteLine("1. Upload blockchain");
            Console.WriteLine("2. Upload pr/pub key");
            Console.WriteLine("3. Generate pr/pub key");
            Console.WriteLine("4. Generate block");
            Console.WriteLine("5. Parse blockchain");
            return Console.ReadLine();
        }
        
        private static Dictionary<byte[], Block> UploadBlockchain()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new Exception("Intorrect choose data file");

            var blocks = FileManager.LoadBlockchain(openFileDialog.FileName);
            
            Blocks = blocks.blocks;

            return blocks.blocks;
        }

        private static KeyPair UploadKeyPair()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) 
                throw new Exception("Intorrect choose data file");

            var keyPair = KeyPair.LoadFrom(openFileDialog.FileName);
            
            KeyPair = keyPair;

            return keyPair;
        }

        private static string GenerateKeyPair()
        {
            EccService.GenerateKey(out var privateKey, out var publicKey);

            var keyPair = new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };

            return JsonConvert.SerializeObject(keyPair, Formatting.Indented);
        }

        private static string GenerateBlock()
        {
            var block = Genesis.GenerateGenesisBlock();

            return JsonConvert.SerializeObject(block, Formatting.Indented);
        }

        private static void ParseBlockchain()
        {
            if (!(Blocks != null && KeyPair != null))
            {
                UploadBlockchain();
                UploadKeyPair();
            }

            var coinPocket = new CoinPocket()
            {
                KeyPair = KeyPair,
                Amount = CoinPocketManager.ParseFromBlockain(Blocks, KeyPair.PublicKey)
            };
            
            ConsoleWorker.WriteDataCoinPocket(coinPocket);
        }
          
    }
}
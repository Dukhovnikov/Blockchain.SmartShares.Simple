using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Blockchain.SmartShares;
using Newtonsoft.Json;

namespace Blockchain.ConsoleApplication
{
    internal static class Program
    {       
        [STAThread]
        public static void Main(string[] args)
        {
            
        }

        public static KeyPair CreateKeyPair()
        {
            EccService.GenerateKey(out var privateKey, out var publicKey);

            return new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };
        }

        public static void LoadKeyPairtoStringFile(KeyPair keyPair, string path)
        {
            var jsonKeyPair = JsonConvert.SerializeObject(keyPair);
            File.WriteAllText(path, jsonKeyPair);
        }

        public static void CreateBlockToFile(Block block, string path)
        {
            var jsonBlock = JsonConvert.SerializeObject(block);
                        
        }

        public static string SerialaizeBlock(Block block)
        {
            return JsonConvert.SerializeObject(block);
        }

        public static void WriteStringToFile(string str ,string path)
        {
            File.WriteAllText(path, str);
        }
    }
}
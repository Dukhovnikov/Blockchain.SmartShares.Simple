using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
            var coinPocket = DataManager.UploadUser();
            var blockchain = DataManager.UploadBlockchainDictionary();
            var tmp = DataManager.UploadBlockchain();
            //var amount = Executor.ParseFromBlockain(blockchain, coinPocket.KeyPair.PublicKey);
            var amount = Executor.GetCash(blockchain, coinPocket.KeyPair.PublicKey);
            Console.WriteLine(amount);
        }
    }
}

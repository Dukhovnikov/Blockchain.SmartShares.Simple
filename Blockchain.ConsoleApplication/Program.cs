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
    internal static class Program
    {        
        public static void Main(string[] args)
        {
            var block = new Block(1, DateTime.Now, new Hash("testhash") );

            var jsonBlock = JsonConvert.SerializeObject(block);
            Console.WriteLine("Json serialazed: {0}", jsonBlock);

            var newBlock = JsonConvert.DeserializeObject<Block>(jsonBlock);
            Console.ReadKey();

        }
    }
}
using System;
using System.Windows.Forms;

namespace SmartShares
{
    public static class ConsoleWorker
    {
        public static void WriteDataCoinPocket(CoinPocket pocket)
        {
            Console.WriteLine("______Welcome to wallet!______\n");
            Console.WriteLine("Your wallet is: {0}", 
                HexConvert.FromBytes(pocket.KeyPair.PublicKey));
            Console.WriteLine("You name is {0}", pocket.UserName);
            Console.WriteLine("You've got {0} ACT", CoinPocketManager.ParseFromBlockainFix(FileManager.LoadBlockchain(), pocket.KeyPair.PublicKey));
            //Console.WriteLine("You've got {0} $", pocket.AmountDollars);
        }

        public static CoinPocket CreateNewUser()
        {                      
            Console.WriteLine("Please, enter your name:");
            var name = Console.ReadLine();
            
            Console.WriteLine("Please, enter your receive port:");
            var receiveport = int.Parse(Console.ReadLine());

            EccService.GenerateKey(out var privateKey, out var publicKey);

            return new CoinPocket()
            {
                UserName = name,
                //AmountDollars = (ulong) new Random().Next(1000),
                KeyPair = new KeyPair()
                {
                    PrivateKey = privateKey,
                    PublicKey = publicKey
                },
                ReceivePort = receiveport
            };            
        }
    }
}
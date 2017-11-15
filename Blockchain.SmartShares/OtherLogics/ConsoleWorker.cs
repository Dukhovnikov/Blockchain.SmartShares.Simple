using System;
using Blockchain.SmartShares;
using Blockchain.SmartShares.OtherLogics;

namespace Blockchain.ConsoleApplication
{
    public static class ConsoleWorker
    {
        public static void WriteDataCoinPocket(CoinPocket pocket)
        {
            Console.WriteLine("______Welcome to wallet!______\n");
            Console.WriteLine("Your wallet is: {0}", 
                HexConvert.FromBytes(pocket.KeyPair.PublicKey));
            Console.WriteLine("On your account: {0} ACT", pocket.Amount);
        }
    }
}
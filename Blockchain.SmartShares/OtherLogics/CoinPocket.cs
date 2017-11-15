using System.Collections.Generic;
using System.Linq;

namespace Blockchain.SmartShares.OtherLogics
{
    public class CoinPocket
    {
        public KeyPair KeyPair { get; set; }
        
        public ulong Amount { get; set; }
        
        
    }

    public static class CoinPocketManager
    {
        public static ulong ParseFromBlockain(SmartShares.Blockchain data, byte[] publicKey)
        {
            foreach (var variable in data.blocks)
            {
                foreach (var outEntry in variable.Value.Transaction.OutEntries)
                {
                    if (outEntry.RecipientHash.SequenceEqual(BlockchainUtil.ToAddress(publicKey)))
                    {
                        return outEntry.Value;
                    }
                }
            }
            return 0;
        }
        
        public static ulong ParseFromBlockain(Dictionary<byte[], Block> data, byte[] publicKey)
        {
            foreach (var variable in data)
            {
                foreach (var outEntry in variable.Value.Transaction.OutEntries)
                {
                    if (outEntry.RecipientHash.SequenceEqual(BlockchainUtil.ToAddress(publicKey)))
                    {
                        return outEntry.Value;
                    }
                }
            }
            return 0;
        }
    }
}
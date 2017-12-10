using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SmartShares
{
    [JsonObject]
    public class CoinPocket
    {
        [JsonProperty("name")]
        public string UserName { get; set; }
        
        [JsonProperty("keypair")]
        public KeyPair KeyPair { get; set; }
        
        [JsonProperty("ACT")]
        public ulong Amount { get; set; }
        
        [JsonProperty("$")]
        public ulong AmountDollars { get; set; }
        
        [JsonProperty("receive")]
        public int ReceivePort { get; set; }
        
        [JsonProperty("transmit")]
        public int TransmitPort { get; set; }
    }

    public static class CoinPocketManager
    {        
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
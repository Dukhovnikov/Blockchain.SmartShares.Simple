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
        
        [JsonProperty("receive")]
        public int ReceivePort { get; set; }
    }

    public static class CoinPocketManager
    {        
        public static int ParseFromBlockain(Dictionary<byte[], Block> data, byte[] publicKey)
        {
            foreach (var variable in data)
            {
                foreach (var outEntry in variable.Value.Transaction.OutEntries)
                {
                    if (outEntry.RecipientHash.SequenceEqual(publicKey))
                    {
                        return outEntry.Value;
                    }
                }
            }
            return 0;
        }
        
        public static int ParseFromBlockainFix(Dictionary<byte[], Block> data, byte[] publicKey)
        {
            var current = data.Keys.Last();
            while (data[current].PreviousHash != null)
            {
                if (data.ContainsKey(current))
                {
                    foreach (var outEntry in data[current].Transaction.OutEntries)
                    {
                        if (outEntry.RecipientHash.SequenceEqual(publicKey))
                        {
                            return outEntry.Value;
                        }
                    }
                }
                current = data[current].PreviousHash;
            }
            return 0;
        }
    }
}
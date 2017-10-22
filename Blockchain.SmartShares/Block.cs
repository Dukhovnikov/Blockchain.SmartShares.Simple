using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [JsonObject]
    public class Block
    {
        [JsonProperty("id")]
        public readonly int Id;
        [JsonProperty("timestamp")]
        public readonly DateTime Timestamp;
        [JsonProperty("hash")]
        public readonly byte[] Hash;
        [JsonProperty("previousHash")]
        public readonly byte[] PreviuosHash;

        public Block(int previousId, DateTime currentDateTime, byte[] previuosHash)
        {
            Id = previousId;
            Timestamp = currentDateTime;
            //Hash = new Hash(previousId + currentDateTime.ToString(CultureInfo.InvariantCulture) + previuosHash);
            Hash = StaticHash.ComputeSha256FromString(previousId +
                                                      currentDateTime.ToString(CultureInfo.InvariantCulture) +
                                                      previuosHash);
            PreviuosHash = previuosHash;
        }
    }
    
     
}
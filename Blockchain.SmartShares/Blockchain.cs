using System.Collections.Generic;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [JsonObject]
    public class Blockchain
    {
        [JsonProperty("blocks")]
        public Dictionary<byte[], Block> blocks { get; set; } = new Dictionary<byte[], Block>();
    }
}
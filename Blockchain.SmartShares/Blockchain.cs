using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [MessagePackObject,JsonObject]
    public class Blockchain
    {
        [Key(0),JsonProperty("blocks")]
        public Dictionary<byte[], Block> blocks { get; set; } = new Dictionary<byte[], Block>();
    }
}
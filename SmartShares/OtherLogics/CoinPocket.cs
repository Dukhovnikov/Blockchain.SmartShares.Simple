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
}
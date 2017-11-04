using System.IO;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    public class KeyPair
    {
        [JsonProperty("pub")]
        public byte[] PublicKey { get; set; }
        
        [JsonProperty("prv")]
        public byte[] PrivateKey { get; set; }

        public static KeyPair LoadFrom(string path)
        {
            var keyContent = File.ReadAllText(path);
            var keyPair = JsonConvert.DeserializeObject<KeyPair>(keyContent);
            
            if (!EccService.TestKey(keyPair.PrivateKey, keyPair.PublicKey))
                throw new InvalidDataException("Collapsed keypair.");

            return keyPair;
        }
    }
}
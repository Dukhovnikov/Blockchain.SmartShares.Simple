using System.Collections.Generic;
using System.Text;
using MessagePack;
using Newtonsoft.Json;

namespace SmartShares
{
    public static class BlockchainUtil
    {
        public static byte[] SerializeBlockchain(Dictionary<byte[], Block> data)
        {
            return MessagePackSerializer.Serialize(data);
        }

        public static byte[] SerializeTransaction(Transaction data)
        {
            return MessagePackSerializer.Serialize(data);
        }

        public static byte[] SerializeJsonByteChain(KeyValuePair<string, Block> data)
        {
            var json = JsonConvert.SerializeObject(data);
            var byteJson = Encoding.UTF8.GetBytes(json);

            return byteJson;
        }

        public static Dictionary<byte[], Block> DeserializeBlockchain(byte[] data)
        {
            return MessagePackSerializer.Deserialize<Dictionary<byte[], Block>>(data);
        }

        public static byte[] ToAddress(byte[] publicKey)
        {
            return Hash.ComputeSha256(publicKey);
        }
    }
}
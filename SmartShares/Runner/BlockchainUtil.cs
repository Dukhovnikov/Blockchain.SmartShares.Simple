using System.Collections.Generic;
using MessagePack;

namespace SmartShares
{
    public static class BlockchainUtil
    {
        public static byte[] SerializeBlockchain(Dictionary<byte[], Block> data)
        {
            return MessagePackSerializer.Serialize(data);
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
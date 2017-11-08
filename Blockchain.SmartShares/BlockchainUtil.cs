using MessagePack;

namespace Blockchain.SmartShares
{
    public static class BlockchainUtil
    {
        public static byte[] SerializeBlockchain(Blockchain data)
        {
            return MessagePackSerializer.Serialize(data);
        }
        
        public static Blockchain DeserializeBlockchain(byte[] data)
        {
            return MessagePackSerializer.Deserialize<Blockchain>(data);
        }
        
        public static byte[] ToAddress(byte[] publicKey)
        {
            return Hash.ComputeSha256(publicKey);
        }
    }
}
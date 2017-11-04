namespace Blockchain.SmartShares
{
    public static class BlockchainUtil
    {
        public static byte[] ToAddress(byte[] publicKey)
        {
            return Hash.ComputeSha256(publicKey);
        }
    }
}
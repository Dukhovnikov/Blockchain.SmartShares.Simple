using System;

namespace Blockchain.SmartShares
{
    public class Block
    {
        public int Id { get; }
        public byte[] Hash { get; }
        public byte[] PreviousHash { get; }
        public DateTime Timestamp { get; }

        public Block(int id, byte[] previousHash)
        {
            Id = id;
            PreviousHash = previousHash;
            Timestamp = DateTime.Now;
            Hash = SmartShares.Hash.ComputeSha256FromString(id + 
                                                            previousHash.ToString() + 
                                                            Timestamp);
        }
    }
}
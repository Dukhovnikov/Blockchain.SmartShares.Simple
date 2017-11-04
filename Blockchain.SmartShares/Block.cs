using System;
using Blockchain.SmartShares.Properties;
using MessagePack;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [MessagePackObject]
    public class Block
    {
        [Key(0), JsonProperty("id")]
        public int Id { get; set; }
        
        [Key(1), JsonProperty("hash")]
        public byte[] Hash { get; set; }
        
        [Key(2), JsonProperty("prevhash")]
        public byte[] PreviousHash { get; set; }
        
        [Key(3), JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        
        [Key(4), JsonProperty("transaction")]
        public Transaction Transaction { get; set; }

        public Block Clone() =>
            new Block()
            {
                Id = Id,
                Hash = Hash,
                PreviousHash = PreviousHash,
                Timestamp = Timestamp,
                Transaction = Transaction
            };
    }
}
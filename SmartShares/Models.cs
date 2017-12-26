using System;
using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;

namespace SmartShares
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
        
        [Key(5), JsonProperty("nonce")]
        public int Nonce { get; set; }

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
    
    [MessagePackObject]
    public sealed class Transaction
    {
        [Key(0), JsonProperty("id")]
        public int Id { get; set; }            
        
        [Key(1), JsonProperty("sign")]
        public byte[] Signature { get; set; }
        
        [Key(2), JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        
        [Key(3), JsonProperty("in")]
        public List<InEntry> InEntries { get; set; }
        
        [Key(4), JsonProperty("out")]
        public List<OutEntry> OutEntries { get; set; }
    }
    
    [MessagePackObject]
    public class InEntry
    {        
        [Key(0), JsonProperty("payerId")]
        public byte[] PublicKey { get; set; }
        
        [Key(1), JsonProperty("cash")]
        public int Amount { get; set; }
    }

    [MessagePackObject]
    public class OutEntry
    {
        [Key(0), JsonProperty("to")]
        public byte[] RecipientHash { get; set; }
        
        [Key(1), JsonProperty("val")]
        public int Value { get; set; }
    }
}
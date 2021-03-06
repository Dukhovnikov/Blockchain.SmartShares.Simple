﻿using System;
using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blockchain.SmartShares.Properties
{
    [MessagePackObject]
    public class Transaction
    {
        [Key(0), JsonProperty("id")]
        public ulong Id { get; set; }
        
        [Key(1), JsonProperty("sign")]
        public byte[] Signature { get; set; }
        
        [Key(2), JsonProperty("timestamp")]
        public virtual DateTime Timestamp { get; set; }
        
        [Key(3), JsonProperty("in")]
        public IList<InEntry> InEntries { get; set; }
        
        [Key(4), JsonProperty("out")]
        public IList<OutEntry> OutEntries { get; set; }
        
        
    }

    [MessagePackObject]
    public class InEntry
    {
        [Key(0), JsonProperty("prevOut")]
        public byte[] previuosOut { get; set; }
        
        [Key(1), JsonProperty("publicKey")]
        public byte[] PublicKey { get; set; }
    }

    [MessagePackObject]
    public class OutEntry
    {
        [Key(0), JsonProperty("to")]
        public byte[] RecipientHash { get; set; }
        
        [Key(1), JsonProperty("val")]
        public ulong Value { get; set; }
    }
}
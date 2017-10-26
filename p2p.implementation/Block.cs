using System;
using System.Globalization;
using MessagePack;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [MessagePackObject]
    public class Block
    {
        [Key(0)]
        public int Id { get; }
        //public readonly int Id;
        [Key(1)]
        public DateTime Timestamp { get; }
        //public readonly DateTime Timestamp;
        //public readonly Hash Hash;
        [Key(2)]
        //public byte[] Hash { get; }
        //public readonly byte[] Hash;
        public Hash Hash{ get; }
        
        [Key(3)]
        public Hash PreviuosHash { get; }
        //public readonly byte[] PreviuosHash;

        public Block(int previousId, DateTime currentDateTime, Hash previuosHash)
//        public Block(int previousId, DateTime currentDateTime, byte[] previuosHash)
        {
            Id = previousId;
            Timestamp = currentDateTime;
            Hash = new Hash(previousId + currentDateTime.ToString(CultureInfo.InvariantCulture) + previuosHash);
//            Hash = StaticHash.ComputeSha256FromString(previousId +
//                            currentDateTime.ToString(CultureInfo.InvariantCulture) +
//                            previuosHash);
            PreviuosHash = previuosHash;
        }
    }
    
     
}
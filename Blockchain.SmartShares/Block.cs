using System;

namespace Blockchain.SmartShares
{
    public class Block
    {
        public int Id { get; private set; }
        public DateTime Timestamp { get; set; }
        public string Hash { get; private set; }
        public string PreviuosHash { get; private set; }
        
        
    }
    
     
}
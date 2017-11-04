using System.Collections.Generic;

namespace Blockchain.SmartShares
{
    public class Blockchain
    {
        public Dictionary<byte[], Block> blocks { get; set; } = new Dictionary<byte[], Block>();
        
        
    }
}
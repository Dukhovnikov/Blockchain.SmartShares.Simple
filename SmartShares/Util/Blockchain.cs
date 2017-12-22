using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShares
{
    public class Blockchain
    {

        public static int ParseFromBlockain(Dictionary<string, Block> data, byte[] publicKey)
        {
            var current = data.Keys.Last();
            while (data[current].PreviousHash != null)
            {
                if (data.ContainsKey(current))
                {
                    foreach (var outEntry in data[current].Transaction.OutEntries)
                    {
                        if (outEntry.RecipientHash.SequenceEqual(publicKey))
                        {
                            return (int) outEntry.Value;
                        }
                    }
                }
                else
                { return -1; }
                current = HexConvert.FromBytes(data[current].PreviousHash);
            }
            return 0;
        }
    }
}

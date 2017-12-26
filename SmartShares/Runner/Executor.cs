using System.Collections.Generic;
using System.Linq;

namespace SmartShares
{
    public class Executor
    {
        public static int ParseFromBlockain(Dictionary<string, Block> data, byte[] publicKey)
        {
            var current = data.Keys.Last();
            while (!data[current].PreviousHash.SequenceEqual(data[current].Hash))
            {
                if (data.ContainsKey(current))
                {
                    foreach (var outEntry in data[current].Transaction.OutEntries)
                    {
                        if (outEntry.RecipientHash.SequenceEqual(publicKey))
                        {
                            return (int)outEntry.Value;
                        }
                    }
                }
                else
                { return -1; }
                current = HexConvert.FromBytes(data[current].PreviousHash);
            }
            if (data[current].Id == 0)
            {
                foreach (var outEntry in data[current].Transaction.OutEntries)
                {
                    if (outEntry.RecipientHash.SequenceEqual(publicKey))
                    {
                        return (int) outEntry.Value;
                    }
                }
            }
            
            return 0;
        }


        public static int GetCash(Dictionary<string, Block> data, byte[] publicKey)
        {
            var currentKey = data.Last().Key;
            var cash = 0;

            while (!data[currentKey].PreviousHash.SequenceEqual(data[currentKey].Hash))
            {
                cash += data[currentKey].Transaction.OutEntries
                    .Where(outEntry => outEntry.RecipientHash.SequenceEqual(publicKey))
                    .Sum(outEntry => (int) outEntry.Value);
                
                if (EntryOutConsist(data[currentKey].Transaction.InEntries, publicKey))
                {
                    return cash;
                }
                
                if (data[currentKey].Id == 0)
                {
                    return (from outEntry in data[currentKey].Transaction.OutEntries
                        where outEntry.RecipientHash.SequenceEqual(publicKey)
                        select (int) outEntry.Value).FirstOrDefault();
                }
                
                currentKey = HexConvert.FromBytes(data[currentKey].PreviousHash);
            }
            
            return 0;

        }

        private static bool EntryOutConsist(IEnumerable<InEntry> inEntries, byte[] publicKey)
        {
            return inEntries == null || inEntries.Any(inEntry => inEntry.PublicKey.SequenceEqual(publicKey));
        }
    }
}
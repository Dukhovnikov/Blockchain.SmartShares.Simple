using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;

namespace SmartShares
{
    public class Executor
    {
       

        public static int GetCash(Dictionary<string, Block> data, byte[] publicKey)
        {
            var currentKey = data.Last().Key;
            var cash = 0;

            while (!data[currentKey].PreviousHash.SequenceEqual(data[currentKey].Hash))
            {
                cash += data[currentKey].Transaction.OutEntries
                    .Where(outEntry => outEntry.RecipientHash.SequenceEqual(publicKey))
                    .Sum(outEntry => (int) outEntry.Value);
                
                if (EntryConsistPub(data[currentKey].Transaction.InEntries, publicKey))
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

        public static int GetCashRec(Dictionary<string, Block> data, byte[] publicKey, string current)
        {
            
            if (EntryConsistPub(data[current].Transaction.InEntries, publicKey))
            {
                return GetCashOut(data[current].Transaction.OutEntries, publicKey);
            }
            if (ConsistOutWithoutEntry(data[current].Transaction, publicKey))
            {
                return GetCashOut(data[current].Transaction.OutEntries, publicKey) +
                       GetCashRec(data, publicKey, HexConvert.FromBytes(data[current].PreviousHash));
            }

            return GetCashRec(data, publicKey, HexConvert.FromBytes(data[current].PreviousHash));
        }

        private static bool ConsistOutWithoutEntry(Transaction transaction, byte[] publicKey)
        {
            return !EntryConsistPub(transaction.InEntries, publicKey) && OutConsistPub(transaction.OutEntries, publicKey);
        }

        private static bool EntryConsistPub(IEnumerable<InEntry> inEntries, byte[] publicKey)
        {
            return inEntries == null || inEntries.Any(inEntry => inEntry.PublicKey.SequenceEqual(publicKey));
        }

        private static bool OutConsistPub(IEnumerable<OutEntry> outEntries, byte[] publicKey)
        {
            return outEntries.Any(outEntry => outEntry.RecipientHash.SequenceEqual(publicKey));
        }

        private static int GetCashOut(IEnumerable<OutEntry> outEntries, byte[] publicKey)
        {
            return (from outEntry in outEntries
                where outEntry.RecipientHash.SequenceEqual(publicKey)
                select outEntry.Value).FirstOrDefault();
        }
    }
}
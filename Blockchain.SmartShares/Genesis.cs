using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;
using Blockchain.SmartShares.Properties;

namespace Blockchain.SmartShares
{
    public class Genesis
    {
        public static byte[] genesisHash =
            Hash.ComputeSha256FromString(
                "Образование — это то, что остаётся после того, как забывается всё выученное в школе");
        
        public static Block GenerateGenesisBlock(string path)
        {
            var keyPair = KeyPair.LoadFrom(path);
            var outEntry = new OutEntry()
            {
                RecipientHash = BlockchainUtil.ToAddress(keyPair.PublicKey),
                Value = 10
            };

            var genesisBlock = new Block()
            {
                Id = 0,
                Hash = genesisHash,
                PreviousHash = null,
                Timestamp = DateTime.Now,
                Transaction = new Transaction()
                {
                    Id = 0,
                    Signature = EccService.Sign(genesisHash, keyPair.PrivateKey, keyPair.PublicKey),
                    Timestamp = DateTime.Now,
                    InEntries = null,
                    OutEntries = new List<OutEntry>() {outEntry}
                }
            };

            return genesisBlock;
        }

        public static Block GenerateGenesisBlock(KeyPair keyPair)
        {
            var outEntry = new OutEntry()
            {
                RecipientHash = BlockchainUtil.ToAddress(keyPair.PublicKey),
                Value = 10
            };

            var genesisBlock = new Block()
            {
                Id = 0,
                Hash = genesisHash,
                PreviousHash = null,
                Timestamp = DateTime.Now,
                Transaction = new Transaction()
                {
                    Id = 0,
                    Signature = EccService.Sign(genesisHash, keyPair.PrivateKey, keyPair.PublicKey),
                    Timestamp = DateTime.Now,
                    InEntries = null,
                    OutEntries = new List<OutEntry>() {outEntry}
                }
            };

            return genesisBlock;
        }

        public static Block GenerateGenesisBlock()
        {
            EccService.GenerateKey(out var privateKey, out var publicKey);

            var keyPair = new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };
            
            var outEntry = new OutEntry()
            {
                RecipientHash = BlockchainUtil.ToAddress(keyPair.PublicKey),
                Value = 10
            };

            var genesisBlock = new Block()
            {
                Id = 0,
                Hash = genesisHash,
                PreviousHash = null,
                Timestamp = DateTime.Now,
                Transaction = new Transaction()
                {
                    Id = 0,
                    Signature = EccService.Sign(genesisHash, keyPair.PrivateKey, keyPair.PublicKey),
                    Timestamp = DateTime.Now,
                    InEntries = null,
                    OutEntries = new List<OutEntry>() {outEntry}
                }
            };

            return genesisBlock;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;


namespace SmartShares
{
    public class Genesis
    {
        /// <summary>
        /// Получение первого хэша из строки
        /// </summary>
        private static byte[] genesisHash =
            Hash.ComputeSha256FromString(
                "Образование — это то, что остаётся после того, как забывается всё выученное в школе");

        /// <summary>
        /// Генерация генезис блока
        /// </summary>
        public static Dictionary<string, Block> GenerateBlockchainGenesis(KeyPair keyPair)
        {
            var outEntry = new OutEntry()
            {
                RecipientHash = keyPair.PublicKey,
                Value = 100
            };

            var genesisBlock = new Block()
            {
                Id = 0,
                Hash = genesisHash,
                Nonce = 0,
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
                                   
            var blockchain = new Dictionary<string, Block>()
            {
                {HexConvert.FromBytes(genesisHash), genesisBlock}
            };

            return blockchain;
        }        

          
    }
}
using System.Collections.Generic;
using System.Linq;
using Blockchain.ConsoleApplication;
using Blockchain.SmartShares.OtherLogics;
using NUnit.Framework;

namespace Blockchain.SmartShares.UnitTests
{
    public class CoinPocketManagerTests
    {
        [Test]
        public void ParseFromBlockain_GenesisBlockchain_TrueParseAmount()
        {
            //Arrange
            EccService.GenerateKey(out var privateKey, out var publicKey);

            var keyPair = new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };
            
            var genesisBlock =
                Genesis.GenerateGenesisBlock(keyPair);
                        
            var blockchain = new SmartShares.Blockchain()
            {
                blocks = new Dictionary<byte[], Block>()
                {
                    {genesisBlock.Hash, genesisBlock}
                }
            };           
            
            //Act
            var amount = CoinPocketManager.ParseFromBlockain(blockchain, keyPair.PublicKey);
            var recipientHash = BlockchainUtil.ToAddress(keyPair.PublicKey);

            
            //Assert
            Assert.IsTrue(genesisBlock.Transaction.OutEntries[0].RecipientHash.SequenceEqual(recipientHash));
            Assert.AreEqual(amount, 10);
            
        }
    }
}
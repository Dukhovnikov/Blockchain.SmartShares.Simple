using NUnit.Framework;
using Newtonsoft.Json;

namespace Blockchain.SmartShares.UnitTests
{
    public class KeyPairTests
    {
        [Test]
        public void KeyPair_SerializeDeserializeJson_Good()
        {
            //Arrange
            EccService.GenerateKey(out var privateKey, out var publicKey);
            var keyPair = new KeyPair()
            {
                PrivateKey = privateKey,
                PublicKey = publicKey
            };
            //Act
            var jsonKeyPair = JsonConvert.SerializeObject(keyPair);
            var keyPairFromJson = JsonConvert.DeserializeObject<KeyPair>(jsonKeyPair);
            //Assert
            Assert.AreEqual(keyPair.PublicKey, keyPairFromJson.PublicKey);
        }
    }
}
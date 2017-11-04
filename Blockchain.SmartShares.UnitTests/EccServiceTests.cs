using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using Blockchain.SmartShares;

namespace Blockchain.SmartShares.UnitTests
{
    public class EccServiceTests
    {
        [Test]
        public void TestKey_GoodData_ReturnTrue()
        {
            //Arrange
            //Act
            EccService.GenerateKey(out var privateKey, out var publicKey);
            
            //Assert
            Assert.True(EccService.TestKey(privateKey, publicKey));
        }
    }

    public class HashTests
    {
        [Test]
        public void ComputeSha256_TrueHash_ReturnTrue()
        {
            //Arrange
            var bytes = new byte[0];
            var hash = SHA256.Create().ComputeHash(bytes);
            
            //Act
            var hashfortest = Hash.ComputeSha256(bytes);

            //Assert
            Assert.AreEqual(hash, hashfortest);
        }
        
        [Test]
        public void ComputeSha256FromString_TrueHash_ReturnTrue()
        {
            //Arrange
            var str = "teststring";
            var hash = SHA256.Create().
                ComputeHash(Encoding.UTF8.GetBytes(str));
            //Act
            var hashfortest = Hash.ComputeSha256FromString(str);
            
            //Assert
            Assert.AreEqual(hash, hashfortest);
        }

    }
}
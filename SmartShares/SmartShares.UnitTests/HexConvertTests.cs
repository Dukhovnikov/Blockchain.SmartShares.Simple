using SmartShares;
using NUnit.Framework;

namespace SmartShares.UnitTests
{
    public class HexConvertTests
    {
        [Test]
        public void HexConvert_FromBytes_ToBytes_GoodResult()
        {
            //Arrange
            var hash = Hash.ComputeSha256(new byte[0]);
            var hashstring = HexConvert.FromBytes(hash);
            var str = hashstring;
            //Act

            //Assert
            Assert.AreEqual(hash, HexConvert.ToBytes(str));
        }
    }
}
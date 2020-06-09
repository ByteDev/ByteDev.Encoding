using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Hex
{
    [TestFixture]
    public class HexCharExtensionsTests
    {
        [Test]
        public void WhenIsHexValue_ThenReturnTrue()
        {
            foreach (var sut in HexCharacterSet.HexChars)
            {
                var result = sut.IsHex();

                Assert.That(result, Is.True);
            }
        }

        [Test]
        public void WhenIsNotHexValue_ThenReturnFalse()
        {
            var result = '-'.IsHex();

            Assert.That(result, Is.False);
        }
    }
}
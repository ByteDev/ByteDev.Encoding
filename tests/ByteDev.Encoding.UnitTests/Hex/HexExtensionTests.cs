using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Hex
{
    [TestFixture]
    public class HexExtensionTests
    {
        [TestFixture]
        public class IsHex
        {
            [TestCase("A")]
            [TestCase("1")]
            [TestCase("F0")]
            [TestCase("F9")]
            [TestCase("F9A1")]
            [TestCase("f9a1")]
            [TestCase("f9A1")]
            public void WhenIsHex_ThenReturnTrue(string sut)
            {
                var result = sut.IsHex();

                Assert.That(result, Is.True);
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("A1G")]
            public void WhenIsNotHex_ThenReturnFalse(string sut)
            {
                var result = sut.IsHex();

                Assert.That(result, Is.False);
            }
        }
    }
}
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Hex;

[TestFixture]
public class HexStringExtensionsTests
{
    [TestFixture]
    public class IsHex
    {
        [TestCase("A")]
        [TestCase("1")]
        [TestCase("F0")]
        [TestCase("F9")]
        [TestCase("F9A1")]
        public void WhenIsHex_ThenReturnTrue(string sut)
        {
            var result = sut.IsHex();

            Assert.That(result, Is.True);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("A1G")]
        [TestCase("f9a1")]
        [TestCase("f9A1")]
        public void WhenIsNotHex_ThenReturnFalse(string sut)
        {
            var result = sut.IsHex();

            Assert.That(result, Is.False);
        }
            
        [Test]
        public void WhenDelimiterProvider_AndHasDelimiter_ThenReturnTrue()
        {
            var result = "4A=6F=68=6E".IsHex('=');

            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenDelimiterProvider_AndDoesNotHaveDelimiter_ThenReturnFalse()
        {
            var result = "4A=6F=68=6E".IsHex('-');

            Assert.That(result, Is.False);
        }
    }
}
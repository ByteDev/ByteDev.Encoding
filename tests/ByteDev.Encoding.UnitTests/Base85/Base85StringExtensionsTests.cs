using ByteDev.Encoding.Base85;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base85;

[TestFixture]
public class Base85StringExtensionsTests
{
    [TestFixture]
    public class IsBase85
    {
        [TestCase(null)]
        [TestCase("")]
        public void WhenIsNullOrEmpty_ThenReturnFalse(string sut)
        {
            var result = sut.IsBase85();

            Assert.That(result, Is.False);
        }

        [TestCase("87cURD]i,\"Ebo ")]
        public void WhenContainsNonBase32Char_ThenReturnFalse(string sut)
        {
            var result = sut.IsBase85();

            Assert.That(result, Is.False);
        }

        [TestCase(@"87cURD]i,""Ebo7")]
        public void WhenIsValidBase85Encoding_ThenReturnTrue(string sut)
        {
            var result = sut.IsBase85();

            Assert.That(result, Is.True);
        }
    }
}
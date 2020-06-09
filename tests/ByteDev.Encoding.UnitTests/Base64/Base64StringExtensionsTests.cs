using ByteDev.Encoding.Base64;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base64
{
    [TestFixture]
    public class Base64StringExtensionsTests
    {
        [TestFixture]
        public class IsBase64
        {
            [Test]
            public void WhenIsNull_ThenReturnFalse()
            {
                var result = Base64StringExtensions.IsBase64(null);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnFalse()
            {
                var result = string.Empty.IsBase64();

                Assert.That(result, Is.False);
            }

            [TestCase("Sm9obiBTbWl0aA-=")]
            [TestCase("Sm9obiBTbWl0aA$=")]
            public void WhenContainsNonBase64Char_ThenReturnFalse(string sut)
            {
                var result = sut.IsBase64();

                Assert.That(result, Is.False);
            }

            [TestCase("Sm9obiBTbWl0aA")]
            [TestCase("Sm9obiBTbWl0aA=")]
            public void WhenLengthIsNotMultipleOfFour_ThenReturnFalse(string sut)
            {
                var result = sut.IsBase64();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenContainsMoreThanTwoEqualsSuffix_ThenReturnFalse()
            {
                const string notBase64 = "Sm9obiBTbWl0a===";

                var result = notBase64.IsBase64();

                Assert.That(result, Is.False);
            }

            [TestCase("Sm9obiBTbWl0aA==")]              // "John Smith"
            [TestCase("Sm9obiBTbWl0aAA=")]
            [TestCase("Sm9obiBTbWl0aAAA")]
            public void WhenIsBase64Encoded_ThenReturnTrue(string sut)
            {
                var result = sut.IsBase64();
                
                Assert.That(result, Is.True);
            }
        }
    }
}
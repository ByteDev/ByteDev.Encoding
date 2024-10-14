using ByteDev.Encoding.Base32;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base32;

[TestFixture]
public class Base32StringExtensionsTests
{
    [TestFixture]
    public class IsBase32
    {
        [TestCase(null)]
        [TestCase("")]
        public void WhenIsNullOrEmpty_ThenReturnFalse(string sut)
        {
            var result = sut.IsBase32();

            Assert.That(result, Is.False);
        }

        [TestCase("IFBEa===")]
        [TestCase("IFBE1===")]
        [TestCase("IFBE8===")]
        [TestCase("IFBE9===")]
        [TestCase("IFBE0===")]
        [TestCase("IFBE-===")]
        public void WhenContainsNonBase32Char_ThenReturnFalse(string sut)
        {
            var result = sut.IsBase32();

            Assert.That(result, Is.False);
        }

        [TestCase("IFBE===")]
        [TestCase("IFBEGA===")]
        public void WhenLengthIsNotMultipleOfEight_ThenReturnFalse(string sut)
        {
            var result = sut.IsBase32();

            Assert.That(result, Is.False);
        }

        [Test]
        public void WhenContainsMoreThanSixEqualsSuffix_ThenReturnFalse()
        {
            const string notBase32 = "I=======";

            var result = notBase32.IsBase32();

            Assert.That(result, Is.False);
        }

        [TestCase("ORUGS4ZANFZSA43U")]
        [TestCase("ORUGS4ZA")]
        [TestCase("ORUGS4Z=")]
        [TestCase("ORUGS===")]
        [TestCase("ORUG====")]
        [TestCase("OR======")]
        public void WhenIsBase32Encoded_ThenReturnTrue(string sut)
        {
            var result = sut.IsBase32();
                
            Assert.That(result, Is.True);
        }
    }
}
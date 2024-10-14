using System;
using ByteDev.Encoding.Base64;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base64;

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

    [TestFixture]
    public class GetBase64EndPadding
    {
        [Test]
        public void WhenIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = (null as string).GetBase64EndPadding());
        }

        [Test]
        public void WhenIsEmpty_ThenReturnEmpty()
        {
            var result = string.Empty.GetBase64EndPadding();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void WhenIsLengthOne_ThenReturnEmpty()
        {
            var result = "A".GetBase64EndPadding();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void WhenStringHasNoEndPadding_ThenReturnEmpty()
        {
            var result = "QUFB".GetBase64EndPadding(); // AAA

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void WhenStringHasSingleEndPadding_ThenReturnSingleEquals()
        {
            var result = "QUE=".GetBase64EndPadding();  // AA

            Assert.That(result, Is.EqualTo("="));
        }

        [Test]
        public void WhenStringHasDoubleEndPadding_ThenReturnSingleEquals()
        {
            var result = "QQ==".GetBase64EndPadding();  // A

            Assert.That(result, Is.EqualTo("=="));
        }
    }

    [TestFixture]
    public class RemoveBase64EndPadding : Base64StringExtensionsTests
    {
        [Test]
        public void WhenIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _ = (null as string).RemoveBase64EndPadding());
        }

        [TestCase("")]
        [TestCase("A")]
        [TestCase("QUFB")]
        public void WhenIsHasNoEndPadding_ThenReturnSameString(string source)
        {
            var result = source.RemoveBase64EndPadding();

            Assert.That(result, Is.SameAs(source));
        }

        [Test]
        public void WhenStringHasSingleEndPadding_ThenReturnWithoutPadding()
        {
            var result = "QUE=".RemoveBase64EndPadding();  // AA

            Assert.That(result, Is.EqualTo("QUE"));
        }

        [Test]
        public void WhenStringHasDoubleEndPadding_ThenReturnWithoutPadding()
        {
            var result = "QQ".RemoveBase64EndPadding();  // A

            Assert.That(result, Is.EqualTo("QQ"));
        }
    }
}
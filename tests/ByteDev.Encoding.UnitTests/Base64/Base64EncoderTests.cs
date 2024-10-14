using ByteDev.Encoding.Base64;
using NUnit.Framework;
using System;

namespace ByteDev.Encoding.UnitTests.Base64;

[TestFixture]
public class Base64EncoderTests
{
    [TestFixture]
    public class CalcBase64EncodedSize
    {
        [TestCase(-2)]
        [TestCase(-1)]
        [TestCase(0)]
        public void WhenOriginalSizeIsZeroOrLess_ThenReturnZero(long originalSize)
        {
            var result = Base64Encoder.CalcBase64EncodedSize(originalSize);

            Assert.That(result, Is.Zero);
        }

        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 8)]
        [TestCase(5, 8)]
        [TestCase(6, 8)]
        [TestCase(50, 68)]
        public void WhenOriginalSizeIsGreaterThanZero_ThenReturnBase64Size(long originalSize, long expected)
        {
            var result = Base64Encoder.CalcBase64EncodedSize(originalSize);

            Assert.That(result, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class CalcOriginalSize
    {
        [TestCase(null)]
        [TestCase("")]
        public void WhenBase64StringIsNullOrEmpty_ThenReturnZero(string base64)
        {
            var result = Base64Encoder.CalcOriginalSize(base64);

            Assert.That(result, Is.Zero);
        }

        [TestCase("QQ==", 1)]       // A
        [TestCase("QUI=", 2)]       // AB
        [TestCase("QUJD", 3)]       // ABC
        [TestCase("QUJDRA==", 4)]   // ABCD
        [TestCase("QUJDREU=", 5)]   // ABCDE
        [TestCase("QUJDREVG", 6)]   // ABCDEF
        public void WhenBase64StringIsValid_ThenReturnOriginalSize(string base64, long expected)
        {
            var result = Base64Encoder.CalcOriginalSize(base64);

            Assert.That(result, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class Encode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base64Encoder().Encode((string) null));
        }

        [TestCase("", "")]
        [TestCase("John Smith", "Sm9obiBTbWl0aA==")]
        [TestCase("John Smith12345", "Sm9obiBTbWl0aDEyMzQ1")]
        public void WhenUtf8StringIsNotNull_ThenReturnEncoded(string text, string expected)
        {
            var result = new Base64Encoder().Encode(text);

            Assert.That(result, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class Decode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base64Encoder().Decode(null));
        }

        [TestCase("", "")]
        [TestCase("Sm9obiBTbWl0aA==", "John Smith")]
        [TestCase("Sm9obiBTbWl0aDEyMzQ1", "John Smith12345")]
        public void WithBase64_ThenReturnText(string base64, string expected)
        {
            var result = new Base64Encoder().Decode(base64);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
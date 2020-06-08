using System;
using ByteDev.Encoding.Base64;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base64
{
    [TestFixture]
    public class Base64EncoderTests
    {
        [TestFixture]
        public class CalcBase64EncodedSize
        {
            [TestCase(-1, 0)]
            [TestCase(0, 0)]
            [TestCase(10, 16)]
            [TestCase(15, 20)]
            [TestCase(16, 24)]
            [TestCase(50, 68)]
            public void WhenOriginalSizeProvided_ThenReturnExpectedBase64Size(long originalSize, long expected)
            {
                var result = Base64Encoder.CalcBase64EncodedSize(originalSize);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class Encode
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => new Base64Encoder().Encode((string) null));
            }

            [TestCase("", "")]
            [TestCase("John Smith", "Sm9obiBTbWl0aA==")]
            [TestCase("John Smith12345", "Sm9obiBTbWl0aDEyMzQ1")]
            public void WhenUtf8StringIsNotNull_ThenReturnBased64(string text, string expected)
            {
                var result = new Base64Encoder().Encode(text);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class Decode
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
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
}

using NUnit.Framework;
using System;
using ByteDev.Encoding.Base85;
using ByteDev.Encoding.Base32;

namespace ByteDev.Encoding.UnitTests.Base85;

[TestFixture]
public class Base85EncoderTests
{
    [TestFixture]
    public class Encode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base85Encoder().Encode((string)null));
        }

        [TestCase("", "")]
        [TestCase("0", "0E")]
        [TestCase("John Smith", @"8oJB\+B*,kFD(")]
        [TestCase("John Smith12345", @"8oJB\+B*,kFD*Ba1Ggr")]
        public void WhenUtf8StringIsNotNull_ThenReturnEncoded(string text, string expected)
        {
            var result = new Base85Encoder().Encode(text);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void PrintBase85Chars()
        {
            for (var i = 33; i < 118; i++)
            {
                // Console.Write($", '{(char)i}'");
                Console.Write((char)i);
                // Console.WriteLine(i + " = " + (char)i);
            }
        }
    }

    [TestFixture]
    public class Decode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base85Encoder().Decode(null));
        }

        [TestCase("", "")]
        [TestCase("0E", "0")]
        [TestCase(@"8oJB\+B*,kFD(", "John Smith")]
        [TestCase(@"8oJB\+B*,kFD*Ba1Ggr", "John Smith12345")]
        public void WithBase85_ThenReturnText(string base85, string expected)
        {
            var result = new Base85Encoder().Decode(base85);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
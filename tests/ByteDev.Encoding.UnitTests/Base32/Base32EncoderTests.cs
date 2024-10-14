using System;
using ByteDev.Encoding.Base32;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base32;

[TestFixture]
public class Base32EncoderTests
{
    [TestFixture]
    public class Encode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base32Encoder().Encode((string) null));
        }

        [TestCase("", "")]
        [TestCase("A", "IE======")]
        [TestCase("AB", "IFBA====")]
        [TestCase("ABC", "IFBEG===")]
        [TestCase("this is st", "ORUGS4ZANFZSA43U")]
        public void WhenUtf8StringIsNotNull_ThenReturnEncoded(string text, string expected)
        {
            var result = new Base32Encoder().Encode(text);

            Assert.That(result, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class Decode
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Base32Encoder().Decode(null));
        }

        [TestCase("", "")]
        [TestCase("IE======", "A")]
        [TestCase("IFBA====", "AB")]
        [TestCase("IFBEG===", "ABC")]
        [TestCase("ORUGS4ZANFZSA43U", "this is st")]
        public void WhenBase32String_ThenReturnDecodedText(string base32, string expected)
        {
            var result = new Base32Encoder().Decode(base32);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
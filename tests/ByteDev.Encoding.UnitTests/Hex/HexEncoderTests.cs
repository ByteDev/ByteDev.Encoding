using System;
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Hex
{
    [TestFixture]
    public class HexEncoderTests
    {
        private HexEncoder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HexEncoder();
        }

        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WhenDelimiterIsHexValue_ThenThrowException()
            {
                foreach (var ch in HexCharacterSet.HexChars)
                {
                    Assert.Throws<ArgumentException>(() => _ = new HexEncoder(ch));
                }
            }
        }

        [TestFixture]
        public class Encode : HexEncoderTests
        {
            [Test]
            public void WhenStringIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Encode((string) null));
            }

            [Test]
            public void WhenEncodingIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Encode("John", null));
            }

            [Test]
            public void WhenBytesIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Encode((byte[]) null));
            }

            [TestCase("", "")]
            [TestCase("John Smith", "4A6F686E20536D697468")]
            [TestCase("John Smith12345", "4A6F686E20536D6974683132333435")]
            public void WhenDecodedStringIsNotNull_ThenReturnEncoded(string text, string expected)
            {
                var result = _sut.Encode(text);

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenDelimiterSet_ThenReturnEncodedWithDelimiter()
            {
                var sut = new HexEncoder('=');

                var result = sut.Encode("John");

                Assert.That(result, Is.EqualTo("4A=6F=68=6E"));
            }
        }

        [TestFixture]
        public class Decode : HexEncoderTests
        {
            [Test]
            public void WhenStringIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Decode(null));
            }

            [Test]
            public void WhenEncodingIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Decode("1A", null));
            }

            [TestCase("", "")]
            [TestCase("4a6f686e", "John")]
            [TestCase("4A6F686E20536D697468", "John Smith")]
            [TestCase("4A6F686E20536D6974683132333435", "John Smith12345")]
            public void WhenEncodedStringIsNotNull_ThenReturnDecoded(string hex, string expected)
            {
                var result = _sut.Decode(hex);

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase("4A", "J")]
            [TestCase("4A=", "J")]
            [TestCase("4A=6F=68=6E", "John")]
            [TestCase("4a=6f=68=6e", "John")]
            [TestCase("4A=6F=68=6E=", "John")]
            public void WhenDelimiterSet_ThenReturnDecoded(string hex, string expected)
            {
                var sut = new HexEncoder('=');

                var result = sut.Decode(hex);

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
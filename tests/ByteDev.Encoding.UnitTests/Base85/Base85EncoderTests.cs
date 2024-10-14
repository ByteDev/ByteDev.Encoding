using NUnit.Framework;
using System;
using ByteDev.Encoding.Base85;

namespace ByteDev.Encoding.UnitTests.Base85;

[TestFixture]
public class Base85EncoderTests
{
    private Base85Encoder _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new Base85Encoder();
    }

    [TestFixture]
    public class Encode : Base85EncoderTests
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Encode((string)null));
        }

        [TestCase("", "")]
        [TestCase("0", "0E")]
        [TestCase("John Smith", @"8oJB\+B*,kFD(")]
        [TestCase("John Smith12345", @"8oJB\+B*,kFD*Ba1Ggr")]
        public void WhenStringIsNotNull_ThenReturnEncoded(string text, string expected)
        {
            var result = _sut.Encode(text);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void WhenIsFourNulChars_ThenReturnEncoded()
        {
            var arr = new[] { '\0', '\0', '\0', '\0' };
            var result = _sut.Encode(new string(arr));

            Assert.That(result, Is.EqualTo("z"));
        }
    }

    [TestFixture]
    public class Decode : Base85EncoderTests
    {
        [Test]
        public void WhenStringIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Decode(null));
        }

        [TestCase("", "")]
        [TestCase("0E", "0")]
        [TestCase(@"8oJB\+B*,kFD(", "John Smith")]
        [TestCase(@"8oJB\+B*,kFD*Ba1Ggr", "John Smith12345")]
        public void WhenBase85String_ThenReturnDecodedText(string base85, string expected)
        {
            var result = _sut.Decode(base85);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void WhenUsingEveryPossibleBase85Char()
        {
            var base85 = _sut.Encode(Base85CharacterSet.Base85);

            var result = _sut.Decode(base85);

            Assert.That(result, Is.EqualTo(Base85CharacterSet.Base85));
        }

        [Test]
        public void WhenEncodeAndDecode_ThenReturnOriginalDecodeString()
        {
            // const string str = "John Smith";
            string str = Base85CharacterSet.Base85;

            for (var i = 0; i < str.Length; i++)
            {
                var clearText = str.Substring(0, i + 1);
                // Console.WriteLine(clearText);

                var encoded = _sut.Encode(clearText);
                var result = _sut.Decode(encoded);

                Assert.That(result, Is.EqualTo(clearText));
            }
        }
    }
}
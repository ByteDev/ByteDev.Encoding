using System;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests
{
    [TestFixture]
    public class EncoderFactoryTests
    {
        private EncoderFactory _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EncoderFactory();
        }

        [Test]
        public void WhenEncodingTypeIsBase64_ThenReturnEncoder()
        {
            var result = _sut.CreateFor(EncodingType.Base64);

            Assert.That(result, Is.TypeOf<Base64Encoder>());
        }

        [Test]
        public void WhenEncodingTypeIsHex_ThenReturnEncoder()
        {
            var result = _sut.CreateFor(EncodingType.Hex);

            Assert.That(result, Is.TypeOf<HexEncoder>());
        }

        [Test]
        public void WhenEncodingTypeIsUnhandled_ThenThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => _sut.CreateFor((EncodingType)999));
        }
    }
}
using System;
using ByteDev.Encoding.Base32;
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
        public void WhenEncodingTypeIsBase32_ThenReturnEncoder()
        {
            var result = _sut.Create(EncodingType.Base32);

            Assert.That(result, Is.TypeOf<Base32Encoder>());
        }

        [Test]
        public void WhenEncodingTypeIsBase64_ThenReturnEncoder()
        {
            var result = _sut.Create(EncodingType.Base64);

            Assert.That(result, Is.TypeOf<Base64Encoder>());
        }

        [Test]
        public void WhenEncodingTypeIsHex_ThenReturnEncoder()
        {
            var result = _sut.Create(EncodingType.Hex);

            Assert.That(result, Is.TypeOf<HexEncoder>());
        }

        [Test]
        public void WhenEncodingTypeIsUnhandled_ThenThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => _sut.Create((EncodingType)999));
        }
    }
}
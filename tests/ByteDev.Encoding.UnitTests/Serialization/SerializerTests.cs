using System;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;
using ByteDev.Encoding.Serialization;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Serialization
{
    [TestFixture]
    public class SerializerTests
    {
        private Serializer _sut;
        private Person _person;

        [SetUp]
        public void SetUp()
        {
            _person = new Person { Name = "John Smith" };

            _sut = new Serializer(new Base64Encoder());
        }
        
        [TestFixture]
        public class Serialize : SerializerTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Serialize(null));
            }

            [Test]
            public void WhenUsingBase64Encoder_ThenReturnBase64()
            {
                var result = _sut.Serialize(_person);

                Assert.That(result.IsBase64(), Is.True);
            }

            [Test]
            public void WhenUsingHexEncoder_ThenReturnHex()
            {
                var sut = new Serializer(new HexEncoder('-'));

                var result = sut.Serialize(_person);

                Assert.That(result.IsHex('-'), Is.True);
            }
        }

        [TestFixture]
        public class Deserialize : SerializerTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Deserialize<Person>(null));
            }

            [Test]
            public void WhenIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Deserialize<Person>(string.Empty));
            }

            [Test]
            public void WhenIsSerialized_ThenReturnObject()
            {
                var base64 = _sut.Serialize(_person);
                
                var result = _sut.Deserialize<Person>(base64);

                Assert.That(result.Name, Is.EqualTo(_person.Name));
            }

            [Test]
            public void WhenIsSerialized_AndDeserializedAsDifferentType_ThenThrowException()
            {
                var base64 = _sut.Serialize(_person);

                Assert.Throws<InvalidCastException>(() => _sut.Deserialize<Customer>(base64));
            }

            [Test]
            public void WhenUsingHexEncoder_ThenReturnObject()
            {
                var sut = new Serializer(new HexEncoder('='));

                var hex = sut.Serialize(_person);

                var result = sut.Deserialize<Person>(hex);

                Assert.That(result.Name, Is.EqualTo(_person.Name));
            }
        }

        [Serializable]
        public class Person
        {
            public string Name { get; set; }
        }

        [Serializable]
        public class Customer
        {
            public string Name { get; set; }
        }
    }
}

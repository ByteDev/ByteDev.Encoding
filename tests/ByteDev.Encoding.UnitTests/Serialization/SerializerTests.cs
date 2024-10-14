using System;
using ByteDev.Encoding.Base32;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Base85;
using ByteDev.Encoding.Hex;
using ByteDev.Encoding.Serialization;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Serialization;

[TestFixture]
public class SerializerTests
{
    private Person _person;

    [SetUp]
    public void SetUp()
    {
        _person = new Person { Name = "John Smith" };
    }
    
    [TestFixture]
    public class Serialize : SerializerTests
    {
        [Test]
        public void WhenIsNull_ThenThrowException()
        {
            var sut = new Serializer(new Base64Encoder());

            Assert.Throws<ArgumentNullException>(() => sut.Serialize(null));
        }

        [Test]
        public void WhenUsingBase64Encoder_ThenReturnSerialized()
        {
            var sut = new Serializer(new Base64Encoder());

            var result = sut.Serialize(_person);

            Assert.That(result.IsBase64(), Is.True);
        }

        [Test]
        public void WhenUsingHexEncoder_ThenReturnSerialized()
        {
            var sut = new Serializer(new HexEncoder('-'));

            var result = sut.Serialize(_person);

            Assert.That(result.IsHex('-'), Is.True);
        }

        [Test]
        public void WhenUsingBase32Encoder_ThenReturnSerialized()
        {
            var sut = new Serializer(new Base32Encoder());

            var result = sut.Serialize(_person);

            Assert.That(result.IsBase32(), Is.True);
        }

        [Test]
        public void WhenUsingBase85Encoder_ThenReturnSerialized()
        {
            var sut = new Serializer(new Base85Encoder());

            var result = sut.Serialize(_person);

            Assert.That(result.IsBase85(), Is.True);
        }
    }

    [TestFixture]
    public class Deserialize : SerializerTests
    {
        [Test]
        public void WhenIsNull_ThenThrowException()
        {
            var sut = new Serializer(new Base64Encoder());

            Assert.Throws<ArgumentException>(() => sut.Deserialize<Person>(null));
        }

        [Test]
        public void WhenIsEmpty_ThenThrowException()
        {
            var sut = new Serializer(new Base64Encoder());

            Assert.Throws<ArgumentException>(() => sut.Deserialize<Person>(string.Empty));
        }

        [Test]
        public void WhenIsSerialized_ThenReturnObject()
        {
            var sut = new Serializer(new Base64Encoder());

            var base64 = sut.Serialize(_person);
                
            var result = sut.Deserialize<Person>(base64);

            Assert.That(result.Name, Is.EqualTo(_person.Name));
        }

        [Test]
        public void WhenIsSerialized_AndDeserializedAsDifferentType_ThenThrowException()
        {
            var sut = new Serializer(new Base64Encoder());

            var base64 = sut.Serialize(_person);

            Assert.Throws<InvalidCastException>(() => sut.Deserialize<Customer>(base64));
        }

        [Test]
        public void WhenUsingHexEncoder_ThenReturnObject()
        {
            var sut = new Serializer(new HexEncoder('='));

            var hex = sut.Serialize(_person);

            var result = sut.Deserialize<Person>(hex);

            Assert.That(result.Name, Is.EqualTo(_person.Name));
        }

        [Test]
        public void WhenUsingBase32Encoder_ThenReturnObject()
        {
            var sut = new Serializer(new Base32Encoder());

            var base32 = sut.Serialize(_person);

            var result = sut.Deserialize<Person>(base32);

            Assert.That(result.Name, Is.EqualTo(_person.Name));
        }

        [Test]
        public void WhenUsingBase85Encoder_ThenReturnObject()
        {
            var sut = new Serializer(new Base85Encoder());

            var base85 = sut.Serialize(_person);

            var result = sut.Deserialize<Person>(base85);

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
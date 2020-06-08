using System;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Serialization;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Serialization
{
    [TestFixture]
    public class SerializerTests
    {
        private static Serializer CreateSut()
        {
            return new Serializer(new Base64Encoder());
        }

        private Product CreateProduct()
        {
            return new Product { Name = "John Smith" };
        }

        private static string SerializeToBase64<T>(T obj)
        {
            return CreateSut().Serialize(obj);
        }

        [TestFixture]
        public class Serialize : SerializerTests
        {
            private Serializer _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenProductIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Act(null));
            }

            [Test]
            public void WhenIsProduct_ThenReturnBase64String()
            {
                var result = Act(CreateProduct());

                Assert.That(result.IsBase64(), Is.True);
            }

            private string Act(object obj)
            {
                return _sut.Serialize(obj);
            }
        }

        [TestFixture]
        public class Deserialize : SerializerTests
        {
            private Serializer _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => Act<Product>(null));
            }

            [Test]
            public void WhenIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => Act<Product>(string.Empty));
            }

            [Test]
            public void WhenProductIsSerialized_AndDeserializedAsProduct_ThenDeserialize()
            {
                var product = CreateProduct();
                var base64 = SerializeToBase64(product);
                
                var result = Act<Product>(base64);

                Assert.That(result.Name, Is.EqualTo(product.Name));
            }

            [Test]
            public void WhenProductIsSerialized_AndDeserializedAsCustomer_ThenThrowException()
            {
                var product = CreateProduct();
                var base64 = SerializeToBase64(product);

                Assert.Throws<InvalidCastException>(() => Act<Customer>(base64));
            }

            private T Act<T>(string input)
            {
                return _sut.Deserialize<T>(input);
            }
        }

        [Serializable]
        public class Product
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

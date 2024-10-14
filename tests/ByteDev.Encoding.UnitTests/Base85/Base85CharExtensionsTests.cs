using ByteDev.Encoding.Base85;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base85;

[TestFixture]
public class Base85CharExtensionsTests
{
    [TestFixture]
    public class IsBase85
    {
        [Test]
        public void WhenIsChar33To117_ThenReturnTrue()
        {
            for (var i = 33; i <= 117; i++)
            {
                var result = ((char)i).IsBase85();

                Assert.That(result, Is.True);
            }
        }

        [Test]
        public void WhenIsZ_ThenReturnTrue()
        {
            var result = 'z'.IsBase85();

            Assert.That(result, Is.True);
        }

        [Test]
        public void WhenIsChar0To32_ThenReturnFalse()
        {
            for (var i = 0; i <= 32; i++)
            {
                var result = ((char)i).IsBase85();

                Assert.That(result, Is.False, $"Char: '{(char)i}' is in the first part of ASCII char set not used");
            }
        }

        [Test]
        public void WhenIsChar118To127_ThenReturnFalse()
        {
            for (var i = 118; i <= 127; i++)
            {
                if ((char)i == 'z')
                    continue; // 'z' is special case allowed

                var result = ((char)i).IsBase85();

                Assert.That(result, Is.False, $"Char: '{(char)i}' is in the second part of ASCII char set not used");
            }
        }
    }
}
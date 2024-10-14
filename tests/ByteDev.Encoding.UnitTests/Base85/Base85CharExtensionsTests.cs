using ByteDev.Encoding.Base85;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base85;

[TestFixture]
public class Base85CharExtensionsTests
{
    [Test]
    public void WhenIsBase85Value_ThenReturnTrue()
    {
        for (var i = 33; i <= 117; i++)
        {
            var result = ((char)i).IsBase85();

            Assert.That(result, Is.True);
        }
    }

    [Test]
    public void WhenIsNotBase85Value_ThenReturnFalse()
    {
        for (var i = 0; i <= 32; i++)
        {
            var result = ((char)i).IsBase85();

            Assert.That(result, Is.False, $"Char: '{(char)i}' is in the first part of ASCII char set not used");
        }

        for (var i = 118; i <= 127; i++)
        {
            var result = ((char)i).IsBase85();

            Assert.That(result, Is.False, $"Char: '{(char)i}' is in the second part of ASCII char set not used");
        }
    }
}
using ByteDev.Encoding.Base64;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base64;

[TestFixture]
public class Base64CharExtensionsTests
{
    [Test]
    public void WhenIsBase64Value_ThenReturnTrue()
    {
        foreach (var sut in Base64CharacterSet.Base64Chars)
        {
            var result = sut.IsBase64();

            Assert.That(result, Is.True);
        }
    }

    [Test]
    public void WhenIsNotBase64Value_ThenReturnFalse()
    {
        var result = '$'.IsBase64();

        Assert.That(result, Is.False);
    }
}
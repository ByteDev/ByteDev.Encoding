using ByteDev.Encoding.Base32;
using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests.Base32;

[TestFixture]
public class Base32CharExtensionsTests
{
    [TestCase('A')]
    [TestCase('B')]
    [TestCase('C')]
    [TestCase('D')]
    [TestCase('E')]
    [TestCase('F')]
    [TestCase('G')]
    [TestCase('H')]
    [TestCase('I')]
    [TestCase('J')]
    [TestCase('K')]
    [TestCase('L')]
    [TestCase('M')]
    [TestCase('N')]
    [TestCase('O')]
    [TestCase('P')]
    [TestCase('Q')]
    [TestCase('R')]
    [TestCase('S')]
    [TestCase('T')]
    [TestCase('U')]
    [TestCase('V')]
    [TestCase('W')]
    [TestCase('X')]
    [TestCase('Y')]
    [TestCase('Z')]
    [TestCase('2')]
    [TestCase('3')]
    [TestCase('4')]
    [TestCase('5')]
    [TestCase('6')]
    [TestCase('7')]
    [TestCase('=')]
    public void WhenIsBase32Value_ThenReturnTrue(char sut)
    {
        var result = sut.IsBase32();

        Assert.That(result, Is.True);
    }

    [TestCase('a')]
    [TestCase('0')]
    [TestCase('1')]
    [TestCase('8')]
    [TestCase('9')]
    public void WhenIsNotBase32Value_ThenReturnFalse(char sut)
    {
        var result = sut.IsBase32();

        Assert.That(result, Is.False);
    }
}
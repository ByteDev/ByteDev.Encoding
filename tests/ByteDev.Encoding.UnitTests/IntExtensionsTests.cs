using NUnit.Framework;

namespace ByteDev.Encoding.UnitTests
{
    [TestFixture]
    public class IntExtensionsTests
    {
        [TestFixture]
        public class IsMultipleOf
        {
            [TestCase(-1, -1)]
            [TestCase(1, 0)]
            [TestCase(0, 1)]
            [TestCase(1, 1)]
            [TestCase(2, 1)]
            [TestCase(2, 2)]
            [TestCase(4, 2)]
            public void WhenIsMultipleOf_ThenReturnTrue(int sut, int value)
            {
                var result = sut.IsMultipleOf(value);

                Assert.That(result, Is.True);
            }

            [TestCase(-4, -3)]
            [TestCase(4, 3)]
            public void WhenIsNotMultipleOf_ThenReturnFalse(int sut, int value)
            {
                var result = sut.IsMultipleOf(value);

                Assert.That(result, Is.False);
            }
        }
    }
}
using System;
using MyNumbersLib;
using Xunit;

namespace NumbersLibTests
{
    public class ComplexNumberTests
    {
        [Fact]
        public void Sum()
        {
            AssertEqual(4, 5, new ComplexNumber(1, 1) + new ComplexNumber(3, 4));
        }

        [Fact]
        public void Subtract()
        {
            AssertEqual(4, 5, new ComplexNumber(6, 9) - new ComplexNumber(2, 4));
        }

        [Fact]
        public void Multiply()
        {
            AssertEqual(-5, -1, new ComplexNumber(2, 3) * new ComplexNumber(-1, 1));
        }

        [Fact]
        public void ComplexEquals()
        {
            Assert.True(new ComplexNumber(3, 4) == new ComplexNumber(3, 4));

            Assert.False(new ComplexNumber(3, 4) == new ComplexNumber(1, 3));
        }

        private void AssertEqual(int expectedReal, int expectedImage, ComplexNumber actual)
        {
            Assert.Equal(expectedReal, actual.Real);
            Assert.Equal(expectedImage, actual.Image);
        }
    }
}

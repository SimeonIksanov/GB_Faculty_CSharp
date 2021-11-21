using System;
using Xunit;
using MyNumbersLib;

namespace NumbersLibTests
{
    public class RationalNumberTest
    {

        [Fact]
        public void InitializeSimpleFraction()
        {
            AssertEqual(1, 2, new RationalNumber(1, 2));
        }

        [Fact]
        public void InitializeWithoutDenominator()
        {
            AssertEqual(3, 1, new RationalNumber(3));
        }

        [Fact]
        public void InitializeWithZeroDenomerator()
        {
            Assert.True(new RationalNumber(4, 0).IsNan);
        }

        [Fact]
        public void InitializeWithZeroNumerator()
        {
            AssertEqual(0, 1, new RationalNumber(0, 5));
        }

        [Theory]
        [InlineData(1, 2, 2, 4)]
        [InlineData(-1, 2, -2, 4)]
        [InlineData(-1, 2, 2, -4)]
        [InlineData(1, 2, -2, -4)]
        [InlineData(1, 2, 1, 2)]
        [InlineData(1, 2, 8, 16)]
        [InlineData(2, 3, 10, 15)]
        [InlineData(4, 7, 16, 28)]
        [InlineData(3, 256, 12, 1024)]
        [InlineData(1, 1, 1, 1)]
        public void InitializeAndReduce(int expectedNumerator, int expectedDenomenator, int actualNumerator, int actualDenomenator)
        {
            AssertEqual(expectedNumerator, expectedDenomenator, new RationalNumber(actualNumerator, actualDenomenator));
        }

        [Fact]
        public void Sum()
        {
            AssertEqual(1, 2, new RationalNumber(1, 4) + new RationalNumber(1, 4));
        }

        [Fact]
        public void SumWithNan()
        {
            Assert.True((new RationalNumber(2, 0) + new RationalNumber(1, 3)).IsNan);
        }

        [Fact]
        public void Subtract()
        {
            AssertEqual(1, 4, new RationalNumber(1, 2) - new RationalNumber(1, 4));
        }

        [Fact]
        public void SubtractWithNan()
        {
            Assert.True((new RationalNumber(1, 2) - new RationalNumber(5, 0)).IsNan);
        }

        [Fact]
        public void Multiply()
        {
            AssertEqual(-1, 4, new RationalNumber(-1, 2) * new RationalNumber(1, 2));
        }

        [Fact]
        public void MultiplyWithNan()
        {
            Assert.True((new RationalNumber(1, 2) * new RationalNumber(1, 0)).IsNan);
            Assert.True((new RationalNumber(1, 0) * new RationalNumber(1, 2)).IsNan);
        }

        [Fact]
        public void Divide()
        {
            AssertEqual(-1, 2, new RationalNumber(1, 4) / new RationalNumber(-1, 2));
        }

        [Fact]
        public void DivideWithNan()
        {
            Assert.True((new RationalNumber(1, 2) / new RationalNumber(1, 0)).IsNan);
            Assert.True((new RationalNumber(1, 0) / new RationalNumber(1, 2)).IsNan);
        }

        [Fact]
        public void DivideToZero()
        {
            Assert.True((new RationalNumber(1, 2) / new RationalNumber(0, 5)).IsNan);
        }

        [Theory]
        [InlineData(1, 2, 0.5d)]
        [InlineData(10, 5, 2d)]
        [InlineData(-1, 5, -0.2d)]
        [InlineData(10, 0, float.NaN)]
        [InlineData(-10, 0, float.NaN)]
        [InlineData(0, 0, float.NaN)]
        public void ConvertToDouble(int numerator, int denominator, double expectedValue)
        {
            double v = new RationalNumber(numerator, denominator);
            Assert.Equal(expectedValue, v, 5);
        }

        [Fact]
        public void ConvertFromInt()
        {
            RationalNumber r = 5;
            AssertEqual(5, 1, r);
            AssertEqual(6, 1, 6);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 1, 3)]
        [InlineData(2, 2, 1)]
        [InlineData(6, 3, 2)]
        [InlineData(12, 2, 6)]
        [InlineData(12, 3, 4)]
        [InlineData(12, 4, 3)]
        [InlineData(12, 6, 2)]
        [InlineData(12, 12, 1)]
        [InlineData(1000, 1, 1000)]
        public void ExplicitlyConvertToInt(int numerator, int denominator, int expectedValue)
        {
            int a = (int)new RationalNumber(numerator, denominator);
            Assert.Equal(expectedValue, a);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(12, 5)]
        [InlineData(12, 10)]
        [InlineData(25, 8)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        public void ExplicitlyConvertToIntAndFailsIfNonConvertible(int numerator, int denominator)
        {
            Assert.Throws<Exception>(() => { int a = (int)new RationalNumber(numerator, denominator); });
        }

        [Fact]
        public void EqualFractions()
        {
            Assert.True(new RationalNumber(1, 2) == new RationalNumber(2, 4));
        }

        [Fact]
        public void NotEqualFractions()
        {
            Assert.True(new RationalNumber(1, 2) < new RationalNumber(3, 4));
            Assert.True(new RationalNumber(5, 7) < new RationalNumber(6, 7));
            Assert.True(new RationalNumber(-1, 2) < new RationalNumber(1, 3));

            Assert.True(new RationalNumber(4, 7) > new RationalNumber(5, 9));

            Assert.False(new RationalNumber(1, 2) > new RationalNumber(3, 4));
            Assert.False(new RationalNumber(-1, 2) > new RationalNumber(1, 3));
        }

        [Fact]
        public void Increment()
        {
            var r = new RationalNumber(1, 2);
            r++;
            AssertEqual(3, 2, r);
        }

        [Fact]
        public void Decrement()
        {
            var r = new RationalNumber(1, 2);
            r--;
            AssertEqual(-1, 2, r);
        }

        private void AssertEqual(int expectedNumerator, int expectedDenomerator, RationalNumber actual)
        {
            Assert.False(actual.IsNan);
            Assert.Equal(expectedNumerator, actual.Numerator);
            Assert.Equal(expectedDenomerator, actual.Denominator);
        }
    }
}

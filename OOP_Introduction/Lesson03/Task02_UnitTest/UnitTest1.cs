using System;
using Xunit;
using Task02;

namespace Task02_UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("123","321")]
        [InlineData("abcdef", "fedcba")]
        [InlineData("abc123def", "fed321cba")]
        [InlineData("aa", "aa")]
        public void ReverseNotEmptyString(string value, string expected)
        {
            Assert.Equal(expected, value.ReverseString());
        }

        [Fact]
        public void ReverseEmptyString()
        {
            Assert.Throws<ArgumentException>(()=>string.Empty.ReverseString());
        }

        [Fact]
        public void ReverseNullString()
        {
            Assert.Throws<ArgumentException>(() => MyExtensionClass.ReverseString(null));
        }
    }
}

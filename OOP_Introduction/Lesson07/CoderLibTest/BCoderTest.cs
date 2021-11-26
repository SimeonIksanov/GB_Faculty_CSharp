using System;
using Xunit;
using CoderLib;

namespace CoderLibTest
{
    public class BCoderTest
    {
        ICoder _coder;

        public BCoderTest()
        {
            _coder = new BCoder();
        }

        [Fact]
        void Encode()
        {
            Assert.Equal("zyx", _coder.Encode("abc"));
            Assert.Equal("cba", _coder.Encode("xyz"));
            Assert.Equal("яа", _coder.Encode("ая"));
        }

        [Fact]
        void Decode()
        {
            Assert.Equal("zab", _coder.Decode("azy"));
            Assert.Equal("cba", _coder.Decode("xyz"));
            Assert.Equal("АЯ", _coder.Decode("ЯА"));
        }
    }
}

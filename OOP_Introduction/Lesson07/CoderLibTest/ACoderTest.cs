using System;
using CoderLib;
using Xunit;

namespace CoderLibTest
{
    public class ACoderTest
    {
        ICoder _coder;

        public ACoderTest()
        {
            _coder = new ACoder();
        }

        [Fact]
        void Encode()
        {
            Assert.Equal("bcd", _coder.Encode("abc"));
            Assert.Equal("234", _coder.Encode("123"));
            Assert.Equal("yza", _coder.Encode("xyz"));
            Assert.Equal("анб", _coder.Encode("яма"));
        }

        [Fact]
        void Decode()
        {
            Assert.Equal("zab", _coder.Decode("abc"));
            Assert.Equal("012", _coder.Decode("123"));
            Assert.Equal("wxy", _coder.Decode("xyz"));
            Assert.Equal("яма", _coder.Decode("анб"));
        }
    }
}

using System;
using System.Linq;
using Domain.Managers.Implementation;
using Domain.Managers.Interfaces;
using Xunit;

namespace ServiceTests
{
    public class PasswordManagerTest
    {
        private IPasswordManager _passwordManager;
        private byte[] _salt;
        private string _pwd;

        public PasswordManagerTest()
        {
            _passwordManager = new PasswordManager();

            _salt = _passwordManager.CreateSalt();
            _pwd = "P@ssw0rd";
        }

        [Fact]
        public void CreatedSaltNotNull()
        {
            var salt = _passwordManager.CreateSalt();

            Assert.NotNull(salt);
        }

        [Fact]
        public void CreatedSaltNotEmpty()
        {
            var salt = _passwordManager.CreateSalt();

            Assert.NotEmpty(salt);
        }


        [Fact]
        public void CreatedPasswordNotNull()
        {
            var hashedPwd = _passwordManager.GetPasswordHashed(_pwd, _salt);

            Assert.NotNull(hashedPwd);
        }

        [Fact]
        public void CreatedPasswordNotEmpty()
        {
            var hashedPwd = _passwordManager.GetPasswordHashed(_pwd, _salt);

            Assert.NotEmpty(hashedPwd);
        }

        [Fact]
        public void TwoHashesOfSamePasswordAreEqual()
        {
            var hashed01 = _passwordManager.GetPasswordHashed(_pwd, _salt);
            var hashed02 = _passwordManager.GetPasswordHashed(_pwd, _salt);

            Assert.True(hashed01.SequenceEqual(hashed02));
        }
    }
}

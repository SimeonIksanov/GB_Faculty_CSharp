using System;
using System.Collections.Generic;
using System.Security.Claims;
using Data.Interfaces;
using Domain.Managers.Implementation;
using Domain.Managers.Interfaces;
using Models.Auth;
using Models.Entities;
using Moq;
using Xunit;

namespace ServiceTests
{
    public class LoginManagerTest
    {
        private ILoginManager _loginManager;

        public LoginManagerTest()
        {
            var tokenServiceMock = new Mock<ITokenService>();
            tokenServiceMock
                .Setup<string>(ts => ts.GenerateAccessToken(new List<Claim>()))
                .Returns("accessToken");
            tokenServiceMock
                .Setup<string>(ts => ts.GenerateRefreshToken(new List<Claim>()))
                .Returns("RefreshToken");

            _loginManager = new LoginManager(tokenServiceMock.Object);
        }

        [Fact]
        public void AuthTest()
        {
            User tmpUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "aaaa"
            };
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(ur => ur.Update(tmpUser));

            _loginManager.Authenticate(userRepoMock.Object, tmpUser);

            userRepoMock.Verify(mock => mock.Update(tmpUser), Times.AtLeastOnce);
        }
    }
}

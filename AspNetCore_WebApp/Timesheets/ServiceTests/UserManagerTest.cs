using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Managers.Implementation;
using Domain.Managers.Interfaces;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using Models;
using Models.Auth;
using Models.Entities;
using Moq;
using Xunit;

namespace ServiceTests
{
    public class UserManagerTest
    {
        //private IPasswordManager _passwordManager;
        //private IUserRepository _userRepository;
        //private IUserManager _userManager;

        //public UserManagerTest()
        //{

        //}

        [Fact]
        public void CreateUserTest()
        {
            var userRequest = new CreateUserRequest
            {
                Username = "testuser",
                Password = "testpassword"
            };
            var pwdManagerMock = new Mock<IPasswordManager>();
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.Add(It.IsAny<User>()));
            IUserManager _userManager = new UserManager(
                pwdManagerMock.Object,
                userRepoMock.Object);

            _userManager.CreateUser(userRequest, CancellationToken.None);

            userRepoMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetUserTest()
        {
            //GetUser(LoginRequest request, CancellationToken cancellationToken)

            var loginRequest = new LoginRequest
            {
                Login = "test",
                Password = "test"
            };
            var pwdManager = new PasswordManager();
            var salt = pwdManager.CreateSalt();
            var hashedPwd = pwdManager.GetPasswordHashed(loginRequest.Login, salt);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.GetByUsername(loginRequest.Login, CancellationToken.None))
                .ReturnsAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Username = loginRequest.Login,
                    PasswordHash = hashedPwd,
                    Salt = salt
                });

            IUserManager userManager = new UserManager(pwdManager, userRepoMock.Object);
            var user = await userManager.GetUser(loginRequest, CancellationToken.None);

            Assert.Equal(loginRequest.Login, user.Username);
            Assert.Equal(hashedPwd, user.PasswordHash);

        }
    }
}

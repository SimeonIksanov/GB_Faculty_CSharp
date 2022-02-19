using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Data.Interfaces;
using Domain.Managers.Interfaces;
using Models.Auth;
using Models.Entities;
using Domain.Extensions;
using Models;

namespace Domain.Managers.Implementation
{
    public class UserManager : IUserManager
    {
        private IPasswordManager _passwordManager;
        private IUserRepository _userRepository;

        public UserManager(IPasswordManager passwordManager, IUserRepository userRepository)
        {
            _passwordManager = passwordManager;
            _userRepository = userRepository;
        }

        public async Task CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
        {
            var salt = _passwordManager.CreateSalt();
            User newUser = new User
            {
                Username = createUserRequest.Username,
                Salt = salt,
                PasswordHash = _passwordManager.GetPasswordHashed(createUserRequest.Password, salt)
            };
            await _userRepository.Add(newUser);
        }

        public async Task<User> FindUserByToken(string token, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByRefreshToken(token, cancellationToken);
        }

        public async Task<User> GetUser(LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUsername(request.Login, cancellationToken);
            if (user.IsEmptyObject())
            {
                return new User();
            }
            byte[] passHash = _passwordManager.GetPasswordHashed(request.Password, user.Salt);

            if (!user.PasswordHash.SequenceEqual(passHash))
            {
                return new User();
            }
            return user;
        }
    }
}

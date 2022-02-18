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
        public async Task CreateUser(IUserRepository userRepository, CreateUserRequest createUserRequest, CancellationToken cancellationToken)
        {
            var salt = PasswordManager.CreateSalt();
            User newUser = new User
            {
                Username = createUserRequest.Username,
                Salt = salt,
                PasswordHash = PasswordManager.GetPasswordHashed(createUserRequest.Password, salt)
            };
            await userRepository.Add(newUser);
        }

        public async Task<User> FindUserByToken(IUserRepository repository, string token, CancellationToken cancellationToken)
        {
            return await repository.GetByRefreshToken(token, cancellationToken);
        }

        public async Task<User> GetUser(IUserRepository repository, LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await repository.GetByUsername(request.Login, cancellationToken);
            if (user.IsEmptyObject())
            {
                return new User();
            }
            byte[] passHash = PasswordManager.GetPasswordHashed(request.Password, user.Salt);

            if (!user.PasswordHash.SequenceEqual(passHash))
            {
                return new User();
            }
            return user;

            //return await Task.Run(() => new User() { Username = "IvanPetrov", Role = "someRole" });
        }
    }
}

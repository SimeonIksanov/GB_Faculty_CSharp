using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;
using Models.Auth;
using Models.Entities;

namespace Domain.Managers.Interfaces
{
    public interface IUserManager
    {
        /// <summary>
        /// Create a new user, hash its password and save to DB
        /// </summary>
        Task CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken);

        /// <summary>
        /// Searches a user in db
        /// </summary>
        /// <returns>User instance</returns>
        Task<User> GetUser(LoginRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Searches a user by refresh token
        /// </summary>
        /// <returns>User instance</returns>
        Task<User> FindUserByToken(string token, CancellationToken cancellationToken);
    }
}

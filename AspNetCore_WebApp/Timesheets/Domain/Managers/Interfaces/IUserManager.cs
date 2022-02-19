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
        Task CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken);
        Task<User> GetUser(LoginRequest request, CancellationToken cancellationToken);
        Task<User> FindUserByToken(string token, CancellationToken cancellationToken);
    }
}

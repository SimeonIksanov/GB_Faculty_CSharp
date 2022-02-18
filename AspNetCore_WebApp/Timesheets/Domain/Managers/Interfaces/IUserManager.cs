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
        Task CreateUser(IUserRepository userRepository, CreateUserRequest createUserRequest, CancellationToken cancellationToken);
        Task<User> GetUser(IUserRepository repository, LoginRequest request, CancellationToken cancellationToken);
        Task<User> FindUserByToken(IUserRepository userRepository, string token, CancellationToken cancellationToken);
    }
}

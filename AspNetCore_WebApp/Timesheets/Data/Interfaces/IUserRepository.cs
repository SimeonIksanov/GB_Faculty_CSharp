using System;
using System.Threading;
using System.Threading.Tasks;
using Models.Entities;

namespace Data.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByUsername(string username, CancellationToken cancellationToken);
        Task<User> GetByRefreshToken(string token, CancellationToken cancellationToken);
    }
}

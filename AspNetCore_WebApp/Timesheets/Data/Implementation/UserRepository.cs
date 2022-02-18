using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.EF;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Implementation
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TimesheetDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context
                            .Set<User>()
                            .Where(u => u.Username == username)
                            .AsEnumerable()
                            .DefaultIfEmpty(new User())
                            .First(), cancellationToken);
        }

        //public async Task<User> GetByLogin(string login, CancellationToken cancellationToken)
        //{
        //    return FindBy(u => u.Username == login, cancellationToken)
        //                //.AsEnumerable()
        //                .DefaultIfEmpty(new User())
        //                .First();
        //}

        public async Task<User> GetByRefreshToken(string token, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context
                            .Set<User>()
                            .Where(u => u.RefreshToken == token)
                            .AsEnumerable()
                            .DefaultIfEmpty(new User())
                            .First(), cancellationToken);
        }
    }
}

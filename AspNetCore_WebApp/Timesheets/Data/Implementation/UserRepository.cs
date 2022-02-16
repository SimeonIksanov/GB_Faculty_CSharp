using System;
using Data.EF;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementation
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TimesheetDbContext context) : base(context)
        {
        }
    }
}

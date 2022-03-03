using System;
using Data.EF;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementation
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TimesheetDbContext context) : base(context)
        {
        }
    }
}

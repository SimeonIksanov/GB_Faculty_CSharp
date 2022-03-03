using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Data.EF;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Implementation
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity, new()
    {
        protected TimesheetDbContext _context;

        public RepositoryBase(TimesheetDbContext context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Guid id, CancellationToken token)
        {
            return await _context
                            .Set<T>()
                            .Where(i => i.Id.Equals(id))
                            .DefaultIfEmpty(new T())
                            .FirstAsync(token);
        }

        //public IQueryable<T> Find(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        //{
        //    return _context.Set<T>().Where(expression);
        //}

        public async Task<IEnumerable<T>> GetAll(CancellationToken token)
        {
            return await _context.Set<T>().ToListAsync(token);
        }

        public async Task Update(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

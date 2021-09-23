using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DB
{
    public class DbRepository<T> : IDbRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public DbRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        //public List<T> GetAll(long fromSecond, long toSecond)
        //{
        //    return _context.Set<T>()
        //                   .Where(item => item.Time > fromSecond && item.Time < toSecond)
        //                   .ToList();
        //}

        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            await Task.Run(() => _context.Set<T>().Update(item));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            await Task.Run(() => _context.Set<T>().Remove(item));
            await _context.SaveChangesAsync();
        }
    }
}

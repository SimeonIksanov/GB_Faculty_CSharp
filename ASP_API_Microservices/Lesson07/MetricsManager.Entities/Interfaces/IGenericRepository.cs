using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Entities.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task AddAsync(TEntity entity);

        public Task AddRangeAsync(IEnumerable<TEntity> entity);
        public IQueryable<TEntity> GetAll();

        public Task DeleteAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
    }
}

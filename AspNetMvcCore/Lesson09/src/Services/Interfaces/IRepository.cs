using Interfaces.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> AddAsync(T item, CancellationToken cancellationToken = default);
        Task<T?> DeleteAsync(T item, CancellationToken cancellationToken = default);
        Task<T?> UpdateAsync(T item, CancellationToken cancellationToken = default);

    }
}

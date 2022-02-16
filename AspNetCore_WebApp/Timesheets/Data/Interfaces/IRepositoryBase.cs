using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(Guid id, CancellationToken token);
        Task<IEnumerable<T>> GetAll(CancellationToken token);
        Task Add(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}

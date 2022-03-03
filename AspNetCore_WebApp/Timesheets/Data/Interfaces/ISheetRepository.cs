using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Entities;

namespace Data.Interfaces
{
    public interface ISheetRepository : IRepositoryBase<Sheet>
    {
        Task<IEnumerable<Sheet>> GetSheets(Guid contractId, DateTime dateStart, DateTime dateEnd, CancellationToken token);
    }
}

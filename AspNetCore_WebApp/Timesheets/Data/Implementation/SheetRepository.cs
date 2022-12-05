using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.EF;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementation
{
    public class SheetRepository : RepositoryBase<Sheet>, ISheetRepository
    {
        public SheetRepository(TimesheetDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sheet>> GetSheets(Guid contractId, DateTime dateStart, DateTime dateEnd, CancellationToken token)
        {
            return (await GetAll(token))
                .Where(s => s.ContractId == contractId
                    && s.Date >= dateStart
                    && s.Date <= dateEnd);
        }
    }
}

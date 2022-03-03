using System;
using Data.EF;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementation
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(TimesheetDbContext context) : base(context)
        {
        }
    }
}

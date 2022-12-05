using System;
using System.Threading;
using System.Threading.Tasks;
using Models;

namespace Domain.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        Task<Guid> Create(InvoiceRequest invoiceRequest, CancellationToken token);
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Aggregates;
using Domain.Managers.Interfaces;
using Models;

namespace Domain.Managers.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISheetRepository _sheetRepository;

        public InvoiceManager(IInvoiceRepository invoiceRepository, ISheetRepository sheetRepository)
        {
            _invoiceRepository = invoiceRepository;
            _sheetRepository = sheetRepository;
        }

        public async Task<Guid> Create(InvoiceRequest request, CancellationToken token)
        {
            var invoice = InvoiceAggregate.Create(request.ContractId, request.DateEnd, request.DateStart);

            var sheetsToInclude = await _sheetRepository
                .GetSheets(request.ContractId, request.DateStart, request.DateEnd, token);

            invoice.IncludeSheets(sheetsToInclude);
            await _invoiceRepository.Add(invoice);

            return invoice.Id;
        }
    }
}

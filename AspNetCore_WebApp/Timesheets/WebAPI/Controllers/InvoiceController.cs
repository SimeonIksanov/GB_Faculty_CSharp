using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceManager _invoiceManager;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISheetRepository _sheetRepository;

        public InvoiceController(IInvoiceManager invoiceManager, IInvoiceRepository invoiceRepository, ISheetRepository sheetRepository)
        {
            _invoiceManager = invoiceManager;
            _invoiceRepository = invoiceRepository;
            _sheetRepository = sheetRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] InvoiceRequest invoiceRequest, CancellationToken token)
        {
            var id = await _invoiceManager.Create(invoiceRequest, token);
            return Ok(id);
        }
    }


}

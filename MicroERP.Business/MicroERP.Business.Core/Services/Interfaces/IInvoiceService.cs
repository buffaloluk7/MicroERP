using System.Collections.Generic;
using System.Threading.Tasks;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceModel> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Single(int invoiceID);

        Task Export(int invoiceID);

        Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs);
    }
}
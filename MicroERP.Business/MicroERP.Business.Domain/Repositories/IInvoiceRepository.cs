using System.Collections.Generic;
using System.Threading.Tasks;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<int> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Find(int invoiceID);

        Task<string> Export(int invoiceID);

        Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs);
    }
}
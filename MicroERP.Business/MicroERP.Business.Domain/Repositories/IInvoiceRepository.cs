using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<int> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Find(int invoiceID);

        Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, decimal? minTotal = null, decimal? maxTotal = null);
    }
}

using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<int> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Single(int invoiceID);

        Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null);
    }
}

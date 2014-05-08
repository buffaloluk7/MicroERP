using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceModel>> Search(CustomerModel customer = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Single(int invoiceID);
    }
}

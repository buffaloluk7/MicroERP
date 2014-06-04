using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceModel> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> All(int customerID);

        Task<InvoiceModel> Single(int invoiceID);

        Task Export(int invoiceID, string path);

        Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minTotal = null, double? maxTotal = null);
    }
}

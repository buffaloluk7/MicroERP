using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        #region Fields

        private readonly IInvoiceRepository invoiceRepository;

        #endregion

        #region Constructors

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        #endregion

        #region IInvoiceService

        public async Task<IEnumerable<InvoiceModel>> Search(int customerID, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            return await this.invoiceRepository.Search(customerID);
        }

        public Task<InvoiceModel> Read(int customerID, int invoiceID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

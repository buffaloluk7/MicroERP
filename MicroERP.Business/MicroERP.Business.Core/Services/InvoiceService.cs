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

        public async Task<IEnumerable<InvoiceModel>> Search(CustomerModel customer = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            var customerID = customer != null ? customer.ID : null;

            return await this.invoiceRepository.Search(customerID);
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            return await this.invoiceRepository.Search(customerID);
        }

        public async Task<InvoiceModel> Single(int invoiceID)
        {
            return await this.invoiceRepository.Read(invoiceID);
        }

        #endregion
    }
}

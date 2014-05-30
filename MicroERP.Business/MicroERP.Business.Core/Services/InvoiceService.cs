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

        public async Task<int> Create(int customerID, InvoiceModel invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException("invoice");
            }

            return await this.invoiceRepository.Create(customerID, invoice);
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            if (customerID == 0)
            {
                throw new ArgumentOutOfRangeException("customerID must not be 0");
            }

            return await this.invoiceRepository.All(customerID);
        }

        public async Task<InvoiceModel> Single(int invoiceID)
        {
            if (invoiceID == 0)
            {
                throw new ArgumentOutOfRangeException("invoiceID must not be 0");
            }

            return await this.invoiceRepository.Find(invoiceID);
        }

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            if (customerID  == null &&
                begin       == null &&
                end         == null &&
                minPrice    == null &&
                maxPrice    == null)
            {
                throw new ArgumentNullException("At least one parameter needs to be not null");
            }

            return await this.Search(customerID, begin, end, minPrice, maxPrice);
        }

        #endregion
    }
}

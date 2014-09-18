using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luvi.Service.Browsing;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;

namespace MicroERP.Business.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        #region Fields

        private readonly IInvoiceRepository invoiceRepository;
        private readonly IBrowsingService browsingService;

        #endregion

        #region Constructors

        public InvoiceService(IInvoiceRepository invoiceRepository, IBrowsingService browsingService)
        {
            this.invoiceRepository = invoiceRepository;
            this.browsingService = browsingService;
        }

        #endregion

        #region IInvoiceService

        public async Task<InvoiceModel> Create(int customerID, InvoiceModel invoice)
        {
            if (customerID == default(int))
            {
                throw new ArgumentOutOfRangeException("customerID", "Invalid customer ID");
            }
            if (invoice == null)
            {
                throw new ArgumentNullException("invoice");
            }

            invoice.ID = await this.invoiceRepository.Create(customerID, invoice);

            return invoice;
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            if (customerID == 0)
            {
                throw new ArgumentOutOfRangeException("customerID", "customerID must not be 0");
            }

            return await this.invoiceRepository.All(customerID);
        }

        public async Task<InvoiceModel> Single(int invoiceID)
        {
            if (invoiceID == 0)
            {
                throw new ArgumentOutOfRangeException("invoiceID", "invoiceID must not be 0");
            }

            return await this.invoiceRepository.Find(invoiceID);
        }

        public async Task Export(int invoiceID)
        {
            if (invoiceID == 0)
            {
                throw new ArgumentOutOfRangeException("invoiceID", "invoiceID must not be 0");
            }

            string downloadLink = await this.invoiceRepository.Export(invoiceID);

            await this.browsingService.OpenLinkAsync(downloadLink);
        }

        public async Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs)
        {
            if (invoiceSearchArgs.IsEmpty())
            {
                throw new ArgumentNullException("invoiceSearchArgs", "At least one parameter needs to be not null");
            }

            return await this.invoiceRepository.Search(invoiceSearchArgs);
        }

        #endregion
    }
}
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake.Repositories
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        #region IInvoiceRepository

        public async Task<InvoiceModel> Create(int customerID, InvoiceModel invoice)
        {
            return await Task.Run(() =>
            {
                if (FakeData.Instance.Invoices.Any(i => i.ID == invoice.ID))
                {
                    throw new InvoiceAlreadyExistsException(invoice);
                }

                invoice.ID = FakeData.Instance.Invoices.Max(i => i.ID) + 1;
                FakeData.Instance.Invoices.Add(invoice);
                return invoice;
            });
        }

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            return await Task.Run(() =>
            {
                return FakeData.Instance.Invoices.Where(i => i.CustomerID == customerID);
            });
        }

        public async Task<InvoiceModel> Read(int invoiceID)
        {
            return await Task.Run(() =>
            {
                var invoice = FakeData.Instance.Invoices.FirstOrDefault<InvoiceModel>(i => i.ID == invoiceID);

                if (invoice != null)
                {
                    return invoice;
                }

                throw new InvoiceNotFoundException();
            });
        }

        #endregion
    }
}

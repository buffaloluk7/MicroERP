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

        public async Task<int> Create(int customerID, InvoiceModel invoice)
        {
            return await Task.Run(() =>
            {
                int invoiceID = FakeData.Instance.Invoices.Max(i => i.ID.Value) + 1;
                invoice.ID = invoiceID;
                invoice.CustomerID = customerID;

                FakeData.Instance.Invoices.Add(invoice);

                return invoiceID;
            });
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            return await Task.Run(() =>
            {
                return FakeData.Instance.Invoices.Where(i => i.CustomerID.Value == customerID);
            });
        }

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

        public async Task<InvoiceModel> Find(int invoiceID)
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

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            return await Task.Run(() =>
            {
                IEnumerable<InvoiceModel> invoices = null;

                if (customerID != null)
                {
                    invoices = FakeData.Instance.Invoices.Where(i => i.CustomerID.Value == customerID);
                }
                else
                {
                    invoices = FakeData.Instance.Invoices.ToList();
                }

                if (begin != null || end != null)
                {
                    invoices = invoices.Where(i => i.IssueDate.Value > begin && i.IssueDate.Value < end);
                }

                if (minPrice != null || maxPrice != null)
                {
                    invoices = invoices.Where(i => i.InvoiceItems.Sum(ii => ii.UnitPrice.Value * ii.Amount.Value * (ii.Tax.Value / 100 + 1)) > minPrice &&
                                                   i.InvoiceItems.Sum(ii => ii.UnitPrice.Value * ii.Amount.Value * (ii.Tax.Value / 100 + 1)) < maxPrice);
                }

                return invoices;
            });
        }

        #endregion
    }
}

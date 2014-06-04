using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Mock.Repositories
{
    public class MockInvoiceRepository : IInvoiceRepository
    {
        #region IInvoiceRepository

        public async Task<int> Create(int customerID, InvoiceModel invoice)
        {
            return await Task.Run(() =>
            {
                var customer = MockData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                invoice.ID = MockData.Instance.Invoices.Max(i => i.ID) + 1;
                invoice.Customer = customer;

                MockData.Instance.Invoices.Add(invoice);

                return invoice.ID;
            });
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            return await Task.Run(() =>
            {
                var customer = MockData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                return MockData.Instance.Invoices.Where(i => i.Customer.ID == customerID);
            });
        }

        public async Task<InvoiceModel> Find(int invoiceID)
        {
            return await Task.Run(() =>
            {
                var invoice = MockData.Instance.Invoices.FirstOrDefault<InvoiceModel>(i => i.ID == invoiceID);
                if (invoice == null)
                {
                    throw new InvoiceNotFoundException();
                }

                return invoice;
            });
        }

        public Task Export(int invoiceID, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, decimal? minTotal = null, decimal? maxTotal = null)
        {
            return await Task.Run(() =>
            {
                IEnumerable<InvoiceModel> invoices = MockData.Instance.Invoices;

                if (customerID.HasValue)
                {
                    invoices = invoices.Where(i => i.Customer.ID == customerID);
                }
                if (begin.HasValue)
                {
                    invoices = invoices.Where(i => DateTime.Compare(i.IssueDate.Date, begin.Value.Date) >= 0);
                }
                if (end.HasValue)
                {
                    invoices = invoices.Where(i => DateTime.Compare(i.IssueDate.Date, end.Value.Date) <= 0);
                }
                if (minTotal.HasValue)
                {
                    invoices = invoices.Where(i => Decimal.Compare(i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax + 1)), minTotal.Value) >= 0);
                }
                if (maxTotal.HasValue)
                {
                    invoices = invoices.Where(i => Decimal.Compare(i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax + 1)), maxTotal.Value) <= 0);
                }

                return invoices;
            });
        }

        #endregion
    }
}

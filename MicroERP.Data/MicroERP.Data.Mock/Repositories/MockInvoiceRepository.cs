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

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minTotal = null, double? maxTotal = null)
        {
            return await Task.Run(() =>
            {
                IEnumerable<InvoiceModel> invoices = null;

                if (customerID.HasValue)
                {
                    invoices = MockData.Instance.Invoices.Where(i => i.Customer.ID == customerID);
                }
                else
                {
                    invoices = MockData.Instance.Invoices.ToList();
                }

                if (begin.HasValue || end.HasValue)
                {
                    invoices = invoices.Where(i => i.IssueDate > begin && i.IssueDate < end);
                }

                if (minTotal.HasValue || maxTotal.HasValue)
                {
                    invoices = invoices.Where(i => i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax / 100 + 1)) > minTotal &&
                                                   i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax / 100 + 1)) < maxTotal);
                }

                return invoices;
            });
        }

        #endregion
    }
}

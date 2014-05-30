﻿using MicroERP.Business.Domain.Exceptions;
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
                var customer = FakeData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                invoice.ID = FakeData.Instance.Invoices.Max(i => i.ID) + 1;
                invoice.Number = FakeData.Instance.Invoices.Max(i => i.Number) + 1;
                invoice.Customer = customer;

                FakeData.Instance.Invoices.Add(invoice);

                return invoice.ID;
            });
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            return await Task.Run(() =>
            {
                var customer = FakeData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                return FakeData.Instance.Invoices.Where(i => i.Customer.ID == customerID);
            });
        }

        public async Task<InvoiceModel> Find(int invoiceID)
        {
            return await Task.Run(() =>
            {
                var invoice = FakeData.Instance.Invoices.FirstOrDefault<InvoiceModel>(i => i.ID == invoiceID);
                if (invoice == null)
                {
                    throw new InvoiceNotFoundException();
                }

                return invoice;
            });
        }

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            return await Task.Run(() =>
            {
                IEnumerable<InvoiceModel> invoices = null;

                if (customerID.HasValue)
                {
                    invoices = FakeData.Instance.Invoices.Where(i => i.Customer.ID == customerID);
                }
                else
                {
                    invoices = FakeData.Instance.Invoices.ToList();
                }

                if (begin.HasValue || end.HasValue)
                {
                    invoices = invoices.Where(i => i.IssueDate > begin && i.IssueDate < end);
                }

                if (minPrice.HasValue || maxPrice.HasValue)
                {
                    invoices = invoices.Where(i => i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax / 100 + 1)) > minPrice &&
                                                   i.InvoiceItems.Sum(ii => ii.UnitPrice * ii.Amount * (ii.Tax / 100 + 1)) < maxPrice);
                }

                return invoices;
            });
        }

        #endregion
    }
}

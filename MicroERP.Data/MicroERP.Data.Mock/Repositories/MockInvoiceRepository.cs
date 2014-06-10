using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;

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
                invoice.GrossTotal = invoice.InvoiceItems.Sum(ii => ii.UnitPrice*ii.Amount*(ii.Tax + 1));
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
                var invoice = MockData.Instance.Invoices.FirstOrDefault(i => i.ID == invoiceID);
                if (invoice == null)
                {
                    throw new InvoiceNotFoundException();
                }

                return invoice;
            });
        }

        public async Task<string> Export(int invoiceID)
        {
            return await Task.Run(() => "http://science.energy.gov/~/media/bes/pdf/reports/files/PDF_File_Guidelines.pdf");
        }

        public async Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs)
        {
            return await Task.Run(() =>
            {
                IEnumerable<InvoiceModel> invoices = MockData.Instance.Invoices;

                if (invoiceSearchArgs.CustomerID.HasValue)
                {
                    invoices = invoices.Where(i => i.Customer.ID == invoiceSearchArgs.CustomerID.Value);
                }
                if (invoiceSearchArgs.MinDate.HasValue)
                {
                    invoices =
                        invoices.Where(
                            i => DateTime.Compare(i.IssueDate.Date, invoiceSearchArgs.MinDate.Value.Date) >= 0 ||
                                 DateTime.Compare(i.DueDate.Date, invoiceSearchArgs.MinDate.Value.Date) >= 0);
                }
                if (invoiceSearchArgs.MaxDate.HasValue)
                {
                    invoices =
                        invoices.Where(
                            i => DateTime.Compare(i.IssueDate.Date, invoiceSearchArgs.MaxDate.Value.Date) <= 0 ||
                                 DateTime.Compare(i.DueDate.Date, invoiceSearchArgs.MaxDate.Value.Date) <= 0);
                }
                if (invoiceSearchArgs.MinTotal.HasValue)
                {
                    invoices =
                        invoices.Where(
                            i =>
                                Decimal.Compare(i.InvoiceItems.Sum(ii => ii.UnitPrice*ii.Amount*(ii.Tax + 1)),
                                    invoiceSearchArgs.MinTotal.Value) >= 0);
                }
                if (invoiceSearchArgs.MaxTotal.HasValue)
                {
                    invoices =
                        invoices.Where(
                            i =>
                                Decimal.Compare(i.InvoiceItems.Sum(ii => ii.UnitPrice*ii.Amount*(ii.Tax + 1)),
                                    invoiceSearchArgs.MaxTotal.Value) <= 0);
                }

                return invoices;
            });
        }

        #endregion
    }
}
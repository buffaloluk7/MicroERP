using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Mock.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Mock
{
    [TestClass]
    public class InvoiceRepositoryTests
    {
        private readonly CustomerModel customer;
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceRepositoryTests()
        {
            ICustomerRepository customerRepository = new MockCustomerRepository();
            this.invoiceRepository = new MockInvoiceRepository();

            this.customer = new CompanyModel
            {
                Name = "Google",
                UID = "123456789",
                Address = "Street 1",
                BillingAddress = "Street 2",
                ShippingAddress = "Street 3"
            };
            this.customer.ID = customerRepository.Create(this.customer).Result;

            IEnumerable<InvoiceModel> invoices = new[]
            {
                new InvoiceModel
                {
                    DueDate = DateTime.Now,
                    IssueDate = DateTime.Now,
                    Comment = "Kommentar #1",
                    Message = "Message #1",
                    Customer = this.customer,
                    InvoiceItems = new ObservableCollection<InvoiceItemModel>(new[]
                    {
                        new InvoiceItemModel {Name = "Artikel #1", Amount = 100, UnitPrice = 10.2m, Tax = 0.1m},
                        new InvoiceItemModel {Name = "Artikel #2", Amount = 120, UnitPrice = 5.4m, Tax = 0.2m}
                    })
                },
                new InvoiceModel
                {
                    DueDate = DateTime.Now,
                    IssueDate = DateTime.Now,
                    Comment = "Kommentar #2",
                    Message = "Message #2",
                    Customer = this.customer,
                    InvoiceItems = new ObservableCollection<InvoiceItemModel>(new[]
                    {
                        new InvoiceItemModel {Name = "Artikel #3", Amount = 70, UnitPrice = 14.2m, Tax = 0.2m},
                        new InvoiceItemModel {Name = "Artikel #4", Amount = 132, UnitPrice = 1.4m, Tax = 0.19m}
                    })
                }
            };

            foreach (var invoice in invoices)
            {
                this.invoiceRepository.Create(this.customer.ID, invoice);
            }
        }

        [TestMethod]
        public void Test_GetAllInvoices()
        {
            this.invoiceRepository.All(this.customer.ID).ContinueWith(i =>
            {
                Assert.AreEqual(2, i.Result.Count());
                Assert.AreEqual(this.customer.ID, i.Result.First().Customer.ID);
            });
        }

        [TestMethod]
        public void Test_GetAllInvoices_CustomerNotFound()
        {
            AsyncAsserts.Throws<CustomerNotFoundException>(
                () => this.invoiceRepository.All(-10)
                );
        }

        [TestMethod]
        public void Test_CreateInvoice()
        {
            var invoice = new InvoiceModel
            {
                DueDate = DateTime.Now,
                IssueDate = DateTime.Now,
                Comment = "Kommentar #3",
                Message = "Message #3",
                Customer = this.customer,
                InvoiceItems = new ObservableCollection<InvoiceItemModel>(new[]
                {
                    new InvoiceItemModel {Name = "Artikel #5", Amount = 80, UnitPrice = 14m, Tax = 0.1m},
                    new InvoiceItemModel {Name = "Artikel #6", Amount = 12, UnitPrice = 1.4m, Tax = 0.2m}
                })
            };
            this.invoiceRepository.Create(this.customer.ID, invoice)
                .ContinueWith(id => Assert.AreNotEqual(id, default(int)));
        }

        [TestMethod]
        public void Test_CreateInvoice_CustomerNotFound()
        {
            var invoice = new InvoiceModel
            {
                DueDate = DateTime.Now,
                IssueDate = DateTime.Now,
                Comment = "Kommentar #3",
                Message = "Message #3",
                InvoiceItems = new ObservableCollection<InvoiceItemModel>(new[]
                {
                    new InvoiceItemModel {Name = "Artikel #5", Amount = 80, UnitPrice = 14m, Tax = 0.1m},
                    new InvoiceItemModel {Name = "Artikel #6", Amount = 12, UnitPrice = 1.4m, Tax = 0.2m}
                })
            };

            AsyncAsserts.Throws<CustomerNotFoundException>(
                () => this.invoiceRepository.Create(-23, invoice)
                );
        }

        [TestMethod]
        public void Test_FindInvoice()
        {
            this.invoiceRepository.All(this.customer.ID).ContinueWith(i =>
            {
                this.invoiceRepository.Find(i.Result.First().ID).ContinueWith(si =>
                {
                    Assert.AreEqual(i.Result.First().ID, si.Result.ID);
                    Assert.AreEqual(i.Result.First(), si.Result);
                });
            });
        }

        [TestMethod]
        public void Test_FindInvoice_NotFound()
        {
            AsyncAsserts.Throws<InvoiceNotFoundException>(
                () => this.invoiceRepository.Find(-12)
                );
        }

        [TestMethod]
        public void Test_ExportInvoice_NotImplemented()
        {
            AsyncAsserts.Throws<NotImplementedException>(
                () => this.invoiceRepository.Export(100)
                );
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerOnly()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID
            };

            this.invoiceRepository.Search(searchArguments).ContinueWith(i => Assert.AreEqual(2, i.Result.Count()));
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerAndDateOnly()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10)
            };

            this.invoiceRepository.Search(searchArguments).ContinueWith(i => Assert.AreEqual(2, i.Result.Count()));
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerAndPriceOnly()
        {
            var searchArguments1 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };
            this.invoiceRepository.Search(searchArguments1).ContinueWith(i => Assert.AreEqual(1, i.Result.Count()));

            var searchArguments2 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1900.4m // Maximale Summe = 1899.5
            };
            this.invoiceRepository.Search(searchArguments2).ContinueWith(i => Assert.AreEqual(2, i.Result.Count()));

            var searchArguments3 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 1743.4m,
                MaxTotal = 2019.3m // Maximale Summe = 1899.5
            };
            this.invoiceRepository.Search(searchArguments3).ContinueWith(i => Assert.AreEqual(1, i.Result.Count()));
        }

        [TestMethod]
        public void Test_SearchInvoices_AllArguments()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10),
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };

            this.invoiceRepository.Search(searchArguments).ContinueWith(i => Assert.AreEqual(1, i.Result.Count()));
        }
    }
}
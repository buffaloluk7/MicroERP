using MicroERP.Business.Core.Factories;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MicroERP.Testing.Component.MockRepository
{
    [TestClass]
    public class InvoiceRepositoryTests
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly CustomerModel customer;
        private readonly IEnumerable<InvoiceModel> invoices;

        public InvoiceRepositoryTests()
        {
            var repositories = RepositoryFactory.CreateRepositories();

            this.customerRepository = repositories.Item1;
            this.invoiceRepository = repositories.Item2;

            this.customer = new CompanyModel() { Name = "Google", UID = "123456789", Address = "Street 1", BillingAddress = "Street 2", ShippingAddress = "Street 3" };
            this.customer.ID = this.customerRepository.Create(this.customer).Result;

            this.invoices = new InvoiceModel[]
            {
                new InvoiceModel() {
                    DueDate = DateTime.Now,
                    IssueDate = DateTime.Now,
                    Comment = "Kommentar #1",
                    Message = "Message #1",
                    Customer = this.customer,
                    InvoiceItems = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
                    {
                        new InvoiceItemModel() { Name = "Artikel #1", Amount = 100, UnitPrice = 10.2m, Tax = 0.1m },
                        new InvoiceItemModel() { Name = "Artikel #2", Amount = 120, UnitPrice = 5.4m, Tax = 0.2m }
                    })
                },
                new InvoiceModel() {
                    DueDate = DateTime.Now,
                    IssueDate = DateTime.Now,
                    Comment = "Kommentar #2",
                    Message = "Message #2",
                    Customer = this.customer,
                    InvoiceItems = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
                    {
                        new InvoiceItemModel() { Name = "Artikel #3", Amount = 70, UnitPrice = 14.2m, Tax = 0.2m },
                        new InvoiceItemModel() { Name = "Artikel #4", Amount = 132, UnitPrice = 1.4m, Tax = 0.19m }
                    })
                }
            };

            foreach (var invoice in this.invoices)
            {
                this.invoiceRepository.Create(this.customer.ID, invoice);
            }
        }

        [TestMethod]
        public void Test_GetAllInvoices()
        {
            var invoices = this.invoiceRepository.All(this.customer.ID).Result;

            Assert.AreEqual(2, invoices.Count());
            Assert.AreEqual(this.customer.ID, invoices.First().Customer.ID);
        }

        [TestMethod]
        public void Test_CreateInvoice()
        {
            var invoice = new InvoiceModel()
            {
                DueDate = DateTime.Now,
                IssueDate = DateTime.Now,
                Comment = "Kommentar #3",
                Message = "Message #3",
                Customer = this.customer,
                InvoiceItems = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
                    {
                        new InvoiceItemModel() { Name = "Artikel #5", Amount = 80, UnitPrice = 14m, Tax = 0.1m },
                        new InvoiceItemModel() { Name = "Artikel #6", Amount = 12, UnitPrice = 1.4m, Tax = 0.2m }
                    })
            };
            invoice.ID = this.invoiceRepository.Create(this.customer.ID, invoice).Result;

            Assert.AreNotEqual(invoice.ID, default(int));
        }

        [TestMethod]
        public void Test_FindInvoice()
        {
            var invoices = this.invoiceRepository.All(this.customer.ID).Result;
            var singleInvoice = this.invoiceRepository.Find(invoices.First().ID).Result;

            Assert.AreEqual(invoices.First().ID, singleInvoice.ID);
            Assert.AreEqual(invoices.First(), singleInvoice);
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerOnly()
        {
            var searchArguments = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID
            };
            var invoices = this.invoiceRepository.Search(searchArguments).Result;

            Assert.AreEqual(2, invoices.Count());
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerAndDateOnly()
        {
            var searchArguments = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10)
            };
            var invoices = this.invoiceRepository.Search(searchArguments).Result;

            Assert.AreEqual(2, invoices.Count());
        }

        [TestMethod]
        public void Test_SearchInvoices_CustomerAndPriceOnly()
        {
            var searchArguments1 = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };
            var invoices1 = this.invoiceRepository.Search(searchArguments1).Result;

            Assert.AreEqual(1, invoices1.Count());

            var searchArguments2 = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1899.6m // Maximale Summe = 1899.5
            };
            var invoices2 = this.invoiceRepository.Search(searchArguments2).Result;

            Assert.AreEqual(2, invoices2.Count());

            var searchArguments3 = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID,
                MinTotal = 1743.4m,
                MaxTotal = 2019.3m // Maximale Summe = 1899.5
            };
            var invoices3 = this.invoiceRepository.Search(searchArguments3).Result;

            Assert.AreEqual(1, invoices3.Count());
        }

        [TestMethod]
        public void Test_SearchInvoices_AllArguments()
        {
            var searchArguments = new InvoiceSearchArgs()
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10),
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };
            var invoices = this.invoiceRepository.Search(searchArguments).Result;

            Assert.AreEqual(1, invoices.Count());
        }
    }
}

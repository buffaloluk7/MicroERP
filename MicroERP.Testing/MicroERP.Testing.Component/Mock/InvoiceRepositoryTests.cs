using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Test_GetAllInvoices()
        {
            var invoiceModels = await this.invoiceRepository.All(this.customer.ID);

            Assert.AreEqual(2, invoiceModels.Count());
            Assert.AreEqual(this.customer.ID, invoiceModels.First().Customer.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task Test_GetAllInvoices_CustomerNotFound()
        {
            await this.invoiceRepository.All(-10);
        }

        [TestMethod]
        public async Task Test_CreateInvoice()
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
            
            var id = await this.invoiceRepository.Create(this.customer.ID, invoice);

            Assert.AreNotEqual(id, default(int));
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task Test_CreateInvoice_CustomerNotFound()
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

            await this.invoiceRepository.Create(-23, invoice);
        }

        [TestMethod]
        public async Task Test_FindInvoice()
        {
            var invoiceModels = await this.invoiceRepository.All(this.customer.ID);
            var invoiceModel = await this.invoiceRepository.Find(invoiceModels.First().ID);

            Assert.AreEqual(invoiceModels.First().ID, invoiceModel.ID);
            Assert.AreEqual(invoiceModels.First(), invoiceModel);
        }

        [TestMethod]
        [ExpectedException(typeof(InvoiceNotFoundException))]
        public async Task Test_FindInvoice_NotFound()
        {
            await this.invoiceRepository.Find(-12);
        }

        [TestMethod]
        public async Task Test_ExportInvoice_NotImplemented()
        {
            var url = await this.invoiceRepository.Export(1);

            Assert.AreEqual("http://science.energy.gov/~/media/bes/pdf/reports/files/PDF_File_Guidelines.pdf", url);
        }

        [TestMethod]
        public async Task Test_SearchInvoices_CustomerOnly()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID
            };
            var invoiceModels = await this.invoiceRepository.Search(searchArguments);
            
            Assert.AreEqual(2, invoiceModels.Count());
        }

        [TestMethod]
        public async Task Test_SearchInvoices_CustomerAndDateOnly()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10)
            };

            var invoiceModels = await this.invoiceRepository.Search(searchArguments);
            
            Assert.AreEqual(2, invoiceModels.Count());
        }

        [TestMethod]
        public async Task Test_SearchInvoices_CustomerAndPriceOnly()
        {
            var searchArguments1 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };
            var invoiceModels1 = await this.invoiceRepository.Search(searchArguments1);
            
            Assert.AreEqual(1, invoiceModels1.Count());

            var searchArguments2 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 100.0m,
                MaxTotal = 1900.4m // Maximale Summe = 1899.5
            };
            var invoiceModels2 = await this.invoiceRepository.Search(searchArguments2);

            Assert.AreEqual(2, invoiceModels2.Count());

            var searchArguments3 = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinTotal = 1743.4m,
                MaxTotal = 2019.3m // Maximale Summe = 1899.5
            };
            var invoiceModels3 = await this.invoiceRepository.Search(searchArguments3);
            
            Assert.AreEqual(1, invoiceModels3.Count());
        }

        [TestMethod]
        public async Task Test_SearchInvoices_AllArguments()
        {
            var searchArguments = new InvoiceSearchArgs
            {
                CustomerID = this.customer.ID,
                MinDate = DateTime.Now.AddDays(-1),
                MaxDate = DateTime.Now.AddDays(10),
                MinTotal = 100.0m,
                MaxTotal = 1805.7m // Maximale Summe = 1899.5
            };

            var invoiceModels = await this.invoiceRepository.Search(searchArguments);
            
            Assert.AreEqual(1, invoiceModels.Count());
        }
    }
}
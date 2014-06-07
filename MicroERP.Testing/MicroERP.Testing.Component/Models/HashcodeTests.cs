using System;
using MicroERP.Business.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Models
{
    [TestClass]
    public class HashCodeTests
    {
        [TestMethod]
        public void Test_PersonHashCode()
        {
            var person = new PersonModel
            {
                ID = 1,
                FirstName = "Dummy",
                LastName = "Dieter"
            };

            Assert.AreEqual(1, person.GetHashCode());
        }

        [TestMethod]
        public void Test_CompanyHashCode()
        {
            var company = new CompanyModel
            {
                ID = 99,
                Name = "Company X",
                UID = "Secret UID"
            };

            Assert.AreEqual(99, company.GetHashCode());
        }

        [TestMethod]
        public void Test_InvoiceHashCode()
        {
            var invoice = new InvoiceModel
            {
                ID = 47,
                Message = "Message",
                IssueDate = new DateTime(2014, 1, 1),
                DueDate = new DateTime(2014, 1, 10)
            };

            Assert.AreEqual(47, invoice.GetHashCode());
        }

        [TestMethod]
        public void Test_InvoiceItemHashCode()
        {
            var invoiceItem = new InvoiceItemModel
            {
                ID = 3,
                Name = "Artikel",
                Amount = 10,
                UnitPrice = 12.5m,
                Tax = 0.2m
            };

            Assert.AreEqual(3, invoiceItem.GetHashCode());
        }
    }
}
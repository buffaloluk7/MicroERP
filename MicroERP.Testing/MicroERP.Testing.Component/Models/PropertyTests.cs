using System;
using System.Collections.ObjectModel;
using MicroERP.Business.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Models
{
    [TestClass]
    public class PropertyTests
    {
        [TestMethod]
        public void Test_PersonProperties()
        {
            var p = new PersonModel
            {
                ID = 1,
                FirstName = "Dummy",
                LastName = "Dieter",
                Invoices = new ObservableCollection<InvoiceModel>(),
                Company = new CompanyModel(1, "A", "B", "C", "Firma X", "1234"),
                Address = "X",
                BillingAddress = "Y",
                ShippingAddress = "Z",
                BirthDate = new DateTime(1993, 6, 1),
                Suffix = "",
                Title = "Herr"
            };

            Assert.AreEqual(1, p.ID);
            Assert.AreEqual("Dummy", p.FirstName);
            Assert.AreEqual("Dieter", p.LastName);
            Assert.AreEqual(0, p.Invoices.Count);
            Assert.AreEqual("A", p.Company.Address);
            Assert.AreEqual(1, p.CompanyID.Value);
            Assert.AreEqual("X", p.Address);
            Assert.AreEqual("Y", p.BillingAddress);
            Assert.AreEqual("Z", p.ShippingAddress);
            Assert.AreEqual(1993, p.BirthDate.Value.Year);
            Assert.IsTrue(string.IsNullOrEmpty(p.Suffix));
            Assert.AreEqual("Herr", p.Title);
        }

        [TestMethod]
        public void Test_CompanyProperties()
        {
            var c = new CompanyModel
            {
                ID = 100,
                Name = "Firma X",
                UID = "1234 UID",
                Address = "Address #1",
                BillingAddress = "Address #2",
                ShippingAddress = "Address #3",
                Invoices = new ObservableCollection<InvoiceModel>
                {
                    new InvoiceModel {ID = 1}
                }
            };

            Assert.AreEqual(100, c.ID);
            Assert.AreEqual("Firma X", c.Name);
            Assert.AreEqual("1234 UID", c.UID);
            Assert.AreEqual("Address #1", c.Address);
            Assert.AreEqual("Address #2", c.BillingAddress);
            Assert.AreEqual("Address #3", c.ShippingAddress);
            Assert.AreEqual(1, c.Invoices.Count);
        }

        [TestMethod]
        public void Test_InvoiceProperties()
        {
            var i = new InvoiceModel(
                17,
                new DateTime(2000, 10, 10),
                new DateTime(2000, 11, 10),
                "Comment",
                null,
                new CompanyModel(1, "A", "B", "C", "Firma X", "1234"),
                new ObservableCollection<InvoiceItemModel>
                {
                    new InvoiceItemModel(1, "Artikel 1", 10, 10.0m, 0.2m),
                    new InvoiceItemModel(2, "Artikel 2", 1, 2.0m, 0.1m)
                }
                );

            Assert.AreEqual(17, i.ID);
            Assert.AreEqual(10, i.IssueDate.Month);
            Assert.AreEqual(11, i.DueDate.Month);
            Assert.AreEqual("Comment", i.Comment);
            Assert.IsTrue(string.IsNullOrWhiteSpace(i.Message));
            Assert.AreEqual(2, i.InvoiceItems.Count);
            Assert.AreEqual(122.2m, i.GrossTotal);
            Assert.AreEqual(1, i.Customer.ID);
        }

        [TestMethod]
        public void Test_InvoiceItemProperties()
        {
            var ii = new InvoiceItemModel
            {
                ID = 999,
                Name = "Artikel #1",
                Amount = 100,
                UnitPrice = 12.5m,
                Tax = 0.2m
            };

            Assert.AreEqual(999, ii.ID);
            Assert.AreEqual("Artikel #1", ii.Name);
            Assert.AreEqual(100, ii.Amount);
            Assert.AreEqual(12.5m, ii.UnitPrice);
            Assert.AreEqual(0.2m, ii.Tax);
            Assert.IsNull(ii.Invoice);
        }
    }
}
using System;
using System.Collections.ObjectModel;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.ViewModels
{
    [TestClass]
    public class CustomerModelViewModelTests
    {
        [TestMethod]
        public void Test_CustomerModelProperties()
        {
            var customer = new PersonModel {FirstName = "Dummy", LastName = "Dieter"};
            var vm = new CustomerModelViewModel(customer);
            vm.PropertyChanged += ((s, e) =>
            {
                if (e.PropertyName == "ShippingAddress")
                {
                    string shippingAddress = (s as CustomerModelViewModel).ShippingAddress;
                    Assert.AreEqual("Z", shippingAddress);
                }
            });

            vm.Address = "X";
            vm.BillingAddress = "Y";
            vm.ShippingAddress = "Z";

            Assert.AreEqual("X", vm.Address);
            Assert.AreEqual("Y", vm.BillingAddress);
            Assert.AreEqual("Z", vm.ShippingAddress);
            Assert.AreEqual(customer.Address, vm.Address);
            Assert.AreEqual(customer.BillingAddress, vm.BillingAddress);
            Assert.AreEqual(customer.ShippingAddress, vm.ShippingAddress);
        }

        [TestMethod]
        public void Test_CompanyModelProperties()
        {
            var vm = new CompanyModelViewModel();
            vm.PropertyChanged += ((s, e) =>
            {
                if (e.PropertyName == "Name")
                {
                    string name = (s as CompanyModelViewModel).Name;
                    Assert.AreEqual("Firma X", name);
                }
            });

            vm.Name = "Firma X";
            vm.UID = "1234";

            Assert.AreEqual("Firma X", vm.Name);
            Assert.AreEqual("1234", vm.UID);
        }

        [TestMethod]
        public void Test_PersonModelProperties()
        {
            var vm = new PersonModelViewModel();
            vm.PropertyChanged += ((s, e) =>
            {
                if (e.PropertyName == "FirstName")
                {
                    string firstName = (s as PersonModelViewModel).FirstName;
                    Assert.AreEqual("Hugo", firstName);
                }
            });

            vm.FirstName = "Hugo";
            vm.LastName = "Dieter";
            vm.BirthDate = new DateTime(1993, 6, 1);
            vm.Company = new CompanyModelViewModel(new CompanyModel(1, "A", "B", "C", "Firma X", "1234"));
            vm.Suffix = "Suffix";
            vm.Title = "Master of the universe";

            Assert.AreEqual("Hugo", vm.FirstName);
            Assert.AreEqual("Dieter", vm.LastName);
            Assert.AreEqual(new DateTime(1993, 6, 1), vm.BirthDate);
            Assert.AreEqual("Firma X", vm.Company.Name);
            Assert.AreEqual("Suffix", vm.Suffix);
            Assert.AreEqual("Master of the universe", vm.Title);
        }

        [TestMethod]
        public void Test_InvoiceModelProperties()
        {
            var invoice = new InvoiceModel(1, DateTime.Now, DateTime.Now, null, null,
                new PersonModel {FirstName = "Dummy", LastName = "Dieter"},
                new ObservableCollection<InvoiceItemModel>(new[]
                {
                    new InvoiceItemModel(1, "Artikel #1", 10, 10.0m, 0.2m),
                    new InvoiceItemModel(2, "Artikel #2", 8, 5.0m, 0.1m)
                })
                );

            var vm = new InvoiceModelViewModel(invoice);
            vm.PropertyChanged += ((s, e) =>
            {
                if (e.PropertyName == "Message")
                {
                    string message = (s as InvoiceModelViewModel).Message;
                    Assert.AreEqual("Test message", message);
                }
            });

            vm.Message = "Test message";
            vm.Comment = "Test comment";
            vm.IssueDate = new DateTime(2014, 9, 10);
            vm.DueDate = new DateTime(2014, 10, 10);

            Assert.AreEqual(1, vm.ID);
            Assert.AreEqual(2, vm.InvoiceItems.Count);
            Assert.AreEqual("Test message", vm.Message);
            Assert.AreEqual("Test comment", vm.Comment);
            Assert.AreEqual(new DateTime(2014, 9, 10), vm.IssueDate);
            Assert.AreEqual(new DateTime(2014, 10, 10), vm.DueDate);
            Assert.AreEqual(164.0m, vm.Total);
            Assert.AreEqual("Dummy Dieter", vm.DisplayName);
        }

        [TestMethod]
        public void Test_InvoiteItemModelProperties()
        {
            var vm = new InvoiceItemModelViewModel(new InvoiceItemModel(1, "Artikel #1", 10, 10.0m, 0.2m));
            vm.PropertyChanged += ((s, e) =>
            {
                if (e.PropertyName == "Name")
                {
                    string name = (s as InvoiceItemModelViewModel).Name;
                    Assert.AreEqual("Artikel #2", name);
                }
            });

            vm.Name = "Artikel #2";
            vm.Tax = 0.19m;
            vm.UnitPrice = 11.5m;
            vm.Amount = 99;

            Assert.AreEqual("Artikel #2", vm.Name);
            Assert.AreEqual(0.19m, vm.Tax);
            Assert.AreEqual(11.5m, vm.UnitPrice);
            Assert.AreEqual(99, vm.Amount);
        }

        [TestMethod]
        public void Test_InvoiceItemModelEmptyConstructor()
        {
            var vm = new InvoiceItemModelViewModel();

            Assert.IsTrue(string.IsNullOrWhiteSpace(vm.Name));
        }
    }
}
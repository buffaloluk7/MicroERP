using MicroERP.Business.Core.Factories;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Mock.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Testing.Component.Mock
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IEnumerable<CustomerModel> customers;

        public CustomerRepositoryTests()
        {
            this.customerRepository = new MockCustomerRepository();

            this.customers = new CustomerModel[]
            {
                new CompanyModel() { Name = "Google", UID = "1234", Address = "Street 1", BillingAddress = "Street 2", ShippingAddress = "Street 3" },
                new CompanyModel() { Name = "Apple", UID = "5678", Address = "Another Street 1", BillingAddress = "Another Street 2", ShippingAddress = "Another Street 3" },
                new PersonModel() { FirstName = "Dummy", LastName = "Dieter" }
            };

            foreach (var customer in this.customers)
            {
                customer.ID = this.customerRepository.Create(customer).Result;
            }
        }

        [TestMethod]
        public void Test_SearchCustomers()
        {
            var customers = this.customerRepository.Search("oogl").Result;

            Assert.AreNotEqual(0, customers.Count());
        }

        [TestMethod]
        public void Test_SearchCustomers_CompaniesOnly()
        {
            var customers = this.customerRepository.Search("apple", CustomerType.Company).Result;

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(CompanyModel));
            }
        }

        [TestMethod]
        public void Test_SearchCustomers_PersonsOnly()
        {
            var customers = this.customerRepository.Search("dummy", CustomerType.Person).Result;
            Assert.AreNotEqual(0, customers.Count());

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(PersonModel));
            }

            var dummyDieter = customers.First() as PersonModel;
            Assert.AreEqual("Dummy", dummyDieter.FirstName);
            Assert.AreEqual("Dieter", dummyDieter.LastName);
        }

        [TestMethod]
        public void Test_CreateCustomer()
        {
            var company = new CompanyModel() { Name = "Microsoft", UID = "012345", Address = "Street 1", BillingAddress = "Street 2", ShippingAddress = "Street 3" };

            Assert.AreEqual(default(int), company.ID);

            company.ID = this.customerRepository.Create(company).Result;

            Assert.AreNotEqual(default(int), company.ID);
        }

        [TestMethod]
        public void Test_FindCustomer()
        {
            var customer = this.customerRepository.Find(this.customers.First().ID).Result;

            Assert.AreEqual(this.customers.First().ID, customer.ID);
        }

        [TestMethod]
        public void Test_FindCustomer_NotFound()
        {
            AsyncAsserts.Throws<CustomerNotFoundException>(
                () => this.customerRepository.Find(-55)
            );
        }

        [TestMethod]
        public void Test_UpdateCustomer()
        {
            // Create new customer
            var person = new PersonModel() { FirstName = "Lukas", LastName = "Streiter" };
            person.ID = this.customerRepository.Create(person).Result;

            // Retrieve newly created customer
            var customer = this.customerRepository.Find(person.ID).Result as PersonModel;
            Assert.AreEqual(person.FirstName, customer.FirstName);

            // Update customer
            customer.FirstName = "Thomas";
            customer.LastName = "Eizinger";
            var updatedCustomer = this.customerRepository.Update(customer).Result as PersonModel;

            // Validate new name
            Assert.AreEqual(updatedCustomer.FirstName, "Thomas");
            Assert.AreEqual(updatedCustomer.LastName, "Eizinger");
        }

        [TestMethod]
        public void Test_UpateCustomer_NotFound()
        {
            var invalidCompany = new CompanyModel(-99, "", "", "", "Firma", "0123");
            
            AsyncAsserts.Throws<CustomerNotFoundException>(
                () => this.customerRepository.Update(invalidCompany)
            );
        }

        [TestMethod]
        public void Test_DeleteCustomer_NotFound()
        {
            AsyncAsserts.Throws<CustomerNotFoundException>(
                () => this.customerRepository.Delete(-99)
            );
        }

        [TestMethod]
        public void Test_DeleteCustomer_CascadeCompany()
        {
            // Create new customer
            var company = new CompanyModel() { Name = "Firmenname", UID = "1234" };
            var companyID = this.customerRepository.Create(company).Result;

            var person = new PersonModel() { FirstName = "Eif", LastName = "rig", CompanyID = companyID };
            var personID = this.customerRepository.Create(person).Result;

            // Delete newly created company
            this.customerRepository.Delete(companyID).Wait();

            // Retrieve person and check company for null
            var personWithoutCompany = this.customerRepository.Find(personID).Result as PersonModel;

            Assert.IsNull(personWithoutCompany.CompanyID);
        }
    }
}
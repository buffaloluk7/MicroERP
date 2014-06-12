using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Mock.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                new CompanyModel
                {
                    Name = "Google",
                    UID = "1234",
                    Address = "Street 1",
                    BillingAddress = "Street 2",
                    ShippingAddress = "Street 3"
                },
                new CompanyModel
                {
                    Name = "Apple",
                    UID = "5678",
                    Address = "Another Street 1",
                    BillingAddress = "Another Street 2",
                    ShippingAddress = "Another Street 3"
                },
                new PersonModel {FirstName = "Dummy", LastName = "Dieter"}
            };

            foreach (var customer in this.customers)
            {
                customer.ID = this.customerRepository.Create(customer).Result;
            }
        }

        [TestMethod]
        public async Task Test_SearchCustomers()
        {
            var customers = await this.customerRepository.Search("oogl");

            Assert.AreNotEqual(0, customers.Count());
        }

        [TestMethod]
        public async Task Test_SearchCustomers_CompaniesOnly()
        {
            var customers = await this.customerRepository.Search("apple", CustomerType.Company);

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof (CompanyModel));
            }
        }

        [TestMethod]
        public async Task Test_SearchCustomers_PersonsOnly()
        {
            var customers = await this.customerRepository.Search("dummy", CustomerType.Person);
            Assert.AreNotEqual(0, customers.Count());

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof (PersonModel));
            }

            var dummyDieter = customers.First() as PersonModel;
            Assert.AreEqual("Dummy", dummyDieter.FirstName);
            Assert.AreEqual("Dieter", dummyDieter.LastName);
        }

        [TestMethod]
        public async Task Test_CreateCustomer()
        {
            var company = new CompanyModel
            {
                Name = "Microsoft",
                UID = "012345",
                Address = "Street 1",
                BillingAddress = "Street 2",
                ShippingAddress = "Street 3"
            };

            Assert.AreEqual(default(int), company.ID);

            company.ID = await this.customerRepository.Create(company);

            Assert.AreNotEqual(default(int), company.ID);
        }

        [TestMethod]
        public async Task Test_FindCustomer()
        {
            var customer = await this.customerRepository.Find(this.customers.First().ID);

            Assert.AreEqual(this.customers.First().ID, customer.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task Test_FindCustomer_NotFound()
        {
            await this.customerRepository.Find(-55);
        }

        [TestMethod]
        public async Task Test_UpdateCustomer()
        {
            // Create new customer
            var person = new PersonModel {FirstName = "Lukas", LastName = "Streiter"};
            person.ID = await this.customerRepository.Create(person);

            // Retrieve newly created customer
            var customer = await this.customerRepository.Find(person.ID) as PersonModel;
            Assert.AreEqual(person.FirstName, customer.FirstName);

            // Update customer
            customer.FirstName = "Thomas";
            customer.LastName = "Eizinger";
            var updatedCustomer = await this.customerRepository.Update(customer) as PersonModel;

            // Validate new name
            Assert.AreEqual(updatedCustomer.FirstName, "Thomas");
            Assert.AreEqual(updatedCustomer.LastName, "Eizinger");
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task Test_UpateCustomer_NotFound()
        {
            var invalidCompany = new CompanyModel(-99, "", "", "", "Firma", "0123");

            await this.customerRepository.Update(invalidCompany);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task Test_DeleteCustomer_NotFound()
        {
            await this.customerRepository.Delete(-99);
        }

        [TestMethod]
        public async Task Test_DeleteCustomer_CascadeCompany()
        {
            // Create new customer
            var company = new CompanyModel {Name = "Firmenname", UID = "1234"};
            var companyID = await this.customerRepository.Create(company);

            var person = new PersonModel {FirstName = "Eif", LastName = "rig", CompanyID = companyID};
            var personID = await this.customerRepository.Create(person);

            // Delete newly created company
            await this.customerRepository.Delete(companyID);

            // Retrieve person and check company for null
            var personWithoutCompany = await this.customerRepository.Find(personID) as PersonModel;

            Assert.IsNull(personWithoutCompany.CompanyID);
        }
    }
}
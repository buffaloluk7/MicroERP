using MicroERP.Business.Core.Factories;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.FakeDAL
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerRepositoryTests()
        {
            this.customerRepository = RepositoryFactory.CreateRepositories().Item1;
        }

        [TestMethod]
        public void Test_CustomersSearch_CompanyType()
        {
            var customers = this.customerRepository.Read("i", CustomerType.Company).Result;

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(CompanyModel));
            }
        }

        [TestMethod]
        public void Test_CustomersSearch_PersonType()
        {
            var customers = this.customerRepository.Read("i", CustomerType.Person).Result;

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(PersonModel));
            }
        }

        [TestMethod]
        public void Test_CreateCustomer()
        {
            var company = new CompanyModel() { Name = "Company name", UID = "1234", Address = "Abc", BillingAddress = "Def", ShippingAddress = "Ghi" };
            var person = new PersonModel() { FirstName = "Lukas", LastName = "Streiter" };

            Assert.IsNull(company.ID);
            Assert.IsNull(person.ID);

            var companyID = this.customerRepository.Create(company);
            var personID = this.customerRepository.Create(person);

            Assert.IsNotNull(companyID);
            Assert.IsNotNull(personID);
        }

        [TestMethod]
        public void Test_UpdateCustomer()
        {
            // Create new customer
            var person = new PersonModel() { FirstName = "Lukas", LastName = "Streiter" };
            person.ID = this.customerRepository.Create(person).Result;

            // Retrieve newly created customer
            var customer = this.customerRepository.Read(person.ID.Value).Result as PersonModel;
            Assert.AreEqual(person.FirstName, customer.FirstName);

            // Update customer
            customer.FirstName = "Thomas";
            customer.LastName = "Eizinger";
            var updatedCustomer = this.customerRepository.Update(customer).Result as PersonModel;

            // Validate new name
            Assert.AreEqual(updatedCustomer.FirstName, "Thomas");
            Assert.AreEqual(updatedCustomer.LastName, "Eizinger");
        }
    }
}

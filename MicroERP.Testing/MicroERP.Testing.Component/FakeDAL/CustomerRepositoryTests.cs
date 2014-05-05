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
        public void TestCustomersSearch_CompanyType()
        {
            var customers = this.customerRepository.Read("i", CustomerType.Company).Result;

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(CompanyModel));
            }
        }

        [TestMethod]
        public void TestCustomersSearch_PersonType()
        {
            var customers = this.customerRepository.Read("i", CustomerType.Person).Result;

            foreach (var c in customers)
            {
                Assert.IsInstanceOfType(c, typeof(PersonModel));
            }
        }
    }
}

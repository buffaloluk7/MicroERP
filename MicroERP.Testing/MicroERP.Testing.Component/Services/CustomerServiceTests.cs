using System;
using System.Threading.Tasks;
using MicroERP.Business.Core.Services;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace MicroERP.Testing.Component.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private readonly ICustomerService customerService;

        public CustomerServiceTests()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customer = new CompanyModel(1, "", "", "", "Lukas", "123") as CustomerModel;

            customerRepositoryMock.Setup(r => r.Find(1)).Returns(Task.FromResult(customer));

            this.customerService = new CustomerService(customerRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Test_FindCustomer()
        {
            var actualCustomer = await this.customerService.Find(1);
            var expectedCustomer = new CompanyModel(1, "", "", "", "Lukas", "123");

            Assert.AreEqual(expectedCustomer, actualCustomer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task Test_FindCustomer_InvalidID_0()
        {
            await this.customerService.Find(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task Test_FindCustomer_InvalidID_Negative()
        {
            await this.customerService.Find(-1);
        }
    }
}

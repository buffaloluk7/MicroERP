using System;
using MicroERP.Business.Core.Factories;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Factories
{
    [TestClass]
    public class CustomerModelFactoryTests
    {
        [TestMethod]
        public void Test_CompanyType()
        {
            var model = CustomerModelFactory.FromType(CustomerType.Company);

            Assert.IsInstanceOfType(model, typeof (CompanyModel));
        }

        [TestMethod]
        public void Test_PersonType()
        {
            var model = CustomerModelFactory.FromType(CustomerType.Person);

            Assert.IsInstanceOfType(model, typeof (PersonModel));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void Test_InvalidType()
        {
            CustomerModelFactory.FromType(CustomerType.None);
        }
    }
}
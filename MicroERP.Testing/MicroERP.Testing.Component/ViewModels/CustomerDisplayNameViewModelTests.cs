using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Enums;

namespace MicroERP.Testing.Component.ViewModels
{
    [TestClass]
    public class CustomerDisplayNameViewModelTests
    {
        [TestMethod]
        public void Test_CompanyDisplayName()
        {
            var c = new CompanyModel
            {
                ID = 4,
                Name = "CompanyName",
                UID = "1234AT",
                Address = "Adresse #1"
            };
            var vm = new CustomerDisplayNameViewModel(c);

            Assert.AreEqual("CompanyName (1234AT)", vm.DisplayName);
        }

        [TestMethod]
        public void Test_PersonDisplayName()
        {
            var p = new PersonModel
            {
                ID = 3,
                FirstName = "Dummy",
                LastName = "Dieter"
            };
            var vm = new CustomerDisplayNameViewModel(p);

            Assert.AreEqual("Dummy Dieter", vm.DisplayName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_NullAsArgument()
        {
            new CustomerDisplayNameViewModel(null);
        }

        [TestMethod]
        public void Test_EmptyPersonDisplayName()
        {
            var vm = new CustomerDisplayNameViewModel(new PersonModel());

            Assert.AreEqual(" ", vm.DisplayName);
        }

        [TestMethod]
        public void Test_EmptyCompanyDisplayName()
        {
            var vm = new CustomerDisplayNameViewModel(new CompanyModel());

            Assert.AreEqual(" ()", vm.DisplayName);
        }

        [TestMethod]
        public void Test_PersonType()
        {
            var vm = new CustomerDisplayNameViewModel(new PersonModel());

            Assert.AreEqual(CustomerType.Person, vm.Type);
        }

        [TestMethod]
        public void Test_CompanyType()
        {
            var vm = new CustomerDisplayNameViewModel(new CompanyModel());

            Assert.AreEqual(CustomerType.Company, vm.Type);
        }

        [TestMethod]
        public void Test_PropertyChanged()
        {
            var person = new PersonModel { FirstName = "Dummy", LastName = "Dieter" };
            var vm = new CustomerDisplayNameViewModel(person);
            vm.PropertyChanged += ((s, e) => 
            {
                var displayName = (s as CustomerDisplayNameViewModel).DisplayName;

                Assert.AreEqual("Hugo Dieter", displayName);
            });

            person.FirstName = "Hugo";
        }
    }
}

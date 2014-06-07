using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroERP.Business.Core.Factories;

namespace MicroERP.Testing.Component.Factories
{
    [TestClass]
    public class RepositoryFactoryTests
    {
        [TestMethod]
        public void Test_CreateRepositories()
        {
            var repository = RepositoryFactory.CreateRepositories();

            Assert.IsNotNull(repository.Item1);
            Assert.IsNotNull(repository.Item2);
        }
    }
}

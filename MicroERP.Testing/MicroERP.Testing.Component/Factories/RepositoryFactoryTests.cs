using MicroERP.Business.Core.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroERP.Testing.Component.Factories
{
    [TestClass]
    public class RepositoryFactoryTests
    {
        [TestMethod]
        public void Test_CreateRepositories()
        {
            var repository = RepositoryFactory.CreateRepositories(null);

            Assert.IsNotNull(repository.Item1);
            Assert.IsNotNull(repository.Item2);
        }
    }
}
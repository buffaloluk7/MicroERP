using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Repositories;
using MicroERP.Data.Fake.Repositories;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        #region Fields

        private static readonly IApiConfiguration local = new ApiConfiguration("127.0.0.1", 8000, "http", "microerp/");
        private static readonly IApiConfiguration remote = new ApiConfiguration("10.201.94.236", 8000, "http", "microerp/");
        private static readonly IApiConfiguration activeConfiguration = RepositoryFactory.remote;

        #endregion

        #region Create repositories

        public static ICustomerRepository CreateCustomerRepository()
        {
            #if (API || !DEBUG)
                return new ApiCustomerRepository(activeConfiguration);
            #else
                return new FakeCustomerRepository();
            #endif
        }

        public static IInvoiceRepository CreateInvoiceRepository()
        {
            #if (API || !DEBUG)
                return new ApiInvoiceRepository(activeConfiguration);
            #else
                return new FakeInvoiceRepository();
            #endif
        }

        #endregion
    }
}

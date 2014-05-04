using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Repositories;
using MicroERP.Data.Fake.Repositories;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        private static readonly IApiConfiguration local = new ApiConfiguration("127.0.0.1", 8000, "http", "microerp/");
        private static readonly IApiConfiguration remote = new ApiConfiguration("10.0.0.20", 9000, "https", "microerp/");
        private static readonly IApiConfiguration activeConfiguration = RepositoryFactory.local;

        public static ICustomerRepository CreateCustomerRepository()
        {
            #if !DEBUG
                return new FakeCustomerRepository();
            #else
                return new ApiCustomerRepository(activeConfiguration);
            #endif
        }

        public static IInvoiceRepository CreateInvoiceRepository()
        {
            #if !DEBUG
                return new FakeInvoiceRepository();
            #else
                return new ApiInvoiceRepository(activeConfiguration);
            #endif
        }
    }
}

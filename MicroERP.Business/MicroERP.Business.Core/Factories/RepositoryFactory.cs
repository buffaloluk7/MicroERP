using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Repositories;
using MicroERP.Data.Fake.Repositories;
using System;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        public static Tuple<ICustomerRepository, IInvoiceRepository> CreateRepositories()
        {
            #if (API || !DEBUG)

            IApiConfiguration local = new ApiConfiguration("127.0.0.1", 8000, "http", "microerp/");
            IApiConfiguration remote = new ApiConfiguration("lukas.cc", 80, "http", "microerp/");
            IApiConfiguration activeConfiguration = remote;

            return new Tuple<ICustomerRepository,IInvoiceRepository>
            (
                new ApiCustomerRepository(activeConfiguration),
                new ApiInvoiceRepository(activeConfiguration)
            );

            #else

            return new Tuple<ICustomerRepository, IInvoiceRepository>
            (
                new FakeCustomerRepository(),
                new FakeInvoiceRepository()
            );

            #endif

        }
    }
}

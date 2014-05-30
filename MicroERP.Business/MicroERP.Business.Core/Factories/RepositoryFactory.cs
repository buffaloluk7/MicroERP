using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Repositories;
using MicroERP.Data.Mock.Repositories;
using System;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        public static Tuple<ICustomerRepository, IInvoiceRepository> CreateRepositories()
        {
            #if (API || !DEBUG)

            IApiConfiguration local = new ApiConfiguration("127.0.0.1", 8000, "http", "microerp/");
            IApiConfiguration remote = new ApiConfiguration("10.201.94.236", 8000, "http", "microerp/");
            IApiConfiguration activeConfiguration = remote;

            return new Tuple<ICustomerRepository, IInvoiceRepository>
            (
                new ApiCustomerRepository(activeConfiguration),
                new ApiInvoiceRepository(activeConfiguration)
            );

            #else

            return new Tuple<ICustomerRepository, IInvoiceRepository>
            (
                new MockCustomerRepository(),
                new MockInvoiceRepository()
            );

            #endif

        }
    }
}

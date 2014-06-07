using System;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Repositories;
using MicroERP.Data.Mock.Repositories;
using Microsoft.Practices.Unity;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        public static Tuple<ICustomerRepository, IInvoiceRepository> CreateRepositories(IApiConfiguration apiConfiguration)
        {
#if (API || !DEBUG)

            return new Tuple<ICustomerRepository, IInvoiceRepository>
            (
                new ApiCustomerRepository(apiConfiguration),
                new ApiInvoiceRepository(apiConfiguration)
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
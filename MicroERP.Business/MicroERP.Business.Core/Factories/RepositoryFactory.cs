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
        /// <summary>
        /// Create either mock repositories or API repositories to access the web service.
        /// </summary>
        /// <param name="apiConfiguration">Configuration used by the API repositories containing URL, Port, etc.</param>
        /// <returns>Tuple with repositories.</returns>
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
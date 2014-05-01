using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Fake.Repositories;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CreateCustomerRepository()
        {
            #if DEBUG
                return new FakeCustomerRepository();
            #else
                return new ApiCustomerRepository();
            #endif
        }

        public static IInvoiceRepository CreateInvoiceRepository()
        {
            #if DEBUG
                return new FakeInvoiceRepository();
            #else
                return new ApiInvoiceRepository();
            #endif
        }
    }
}

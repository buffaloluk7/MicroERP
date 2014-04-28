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
    }
}

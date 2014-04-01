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
                if (ViewModelBase.IsInDesignModeStatic)
                {
                    return new FakeCustomerRepository();
                }

                return new ApiCustomerRepository();
            #endif
        }
    }
}

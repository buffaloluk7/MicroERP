using MicroERP.Business.Domain.Interfaces;
using MicroERP.Data.Fake;

namespace MicroERP.Business.Core.Factories
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CreateRepository()
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

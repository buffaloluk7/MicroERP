using GalaSoft.MvvmLight;
using MicroERP.Data.Api;
using MicroERP.Data.Fake;
using MicroERP.Domain.Interfaces;

namespace MicroERP.Business.Factories
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

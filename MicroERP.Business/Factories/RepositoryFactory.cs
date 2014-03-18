using MicroERP.Data.Fake;
using MicroERP.Domain.Interfaces;

namespace MicroERP.Business.Factories
{
    public static class RepositoryFactory
    {
        public static IRepository CreateRepository()
        {
            //if (!ViewModelBase.IsInDesignModeStatic)
            //{
            //    return new ESCRepository();
            //}

            return new FakeRepository();
        }
    }
}

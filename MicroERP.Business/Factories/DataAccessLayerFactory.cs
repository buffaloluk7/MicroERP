using MicroERP.Business.DataAccessLayer.Fake;
using MicroERP.Business.DataAccessLayer.Interfaces;

namespace MicroERP.Business.Factory
{
    public static class DataAccessLayerFactory
    {
        public static IDataAccessLayer CreateDataAccessLayer()
        {
            //if (!ViewModelBase.IsInDesignModeStatic)
            //{
            //    return new ESCDataAccessLayer();
            //}

            return new FakeDataAccessLayer();
        }
    }
}

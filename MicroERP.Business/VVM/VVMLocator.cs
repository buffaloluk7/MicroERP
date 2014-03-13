using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Factory;
using MicroERP.Business.Interfaces;
using MicroERP.Business.Services;

namespace MicroERP.Business.VVM
{
    public sealed class VVMLocator
    {
        public VVMLocator()
        {
            if (!SimpleIoc.Default.IsRegistered<IDataAccessLayer>())
            {
                SimpleIoc.Default.Register<IDataAccessLayer>(DataAccessLayerFactory.CreateDataAccessLayer);
            }

            if (!SimpleIoc.Default.IsRegistered<MainVVM>())
            {
                SimpleIoc.Default.Register<MainVVM>();
            }

            if (!SimpleIoc.Default.IsRegistered<IMessageService>())
            {
                SimpleIoc.Default.Register<IMessageService, MessageService>();
            }
        }

        public MainVVM Main
        {
            get { return SimpleIoc.Default.GetInstance<MainVVM>(); }
        }
    }
}

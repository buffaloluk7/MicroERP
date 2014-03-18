using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.Factories;
using MicroERP.Business.ViewModels;
using MicroERP.Domain.Interfaces;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;

namespace MicroERP.Business
{
    public sealed class VMLocator
    {
        public VMLocator()
        {
            if (!SimpleIoc.Default.IsRegistered<IRepository>())
            {
                SimpleIoc.Default.Register<IRepository>(RepositoryFactory.CreateRepository);
            }

            if (!SimpleIoc.Default.IsRegistered<MainWindowViewModel>())
            {
                SimpleIoc.Default.Register<MainWindowViewModel>();
            }

            if (!SimpleIoc.Default.IsRegistered<CustomerWindowViewModel>())
            {
                SimpleIoc.Default.Register<CustomerWindowViewModel>();
            }
        }

        public MainWindowViewModel Main
        {
            get { return SimpleIoc.Default.GetInstance<MainWindowViewModel>(); }
        }

        public CustomerWindowViewModel CustomerDetail
        {
            get { return SimpleIoc.Default.GetInstance<CustomerWindowViewModel>(); }
        }
    }
}

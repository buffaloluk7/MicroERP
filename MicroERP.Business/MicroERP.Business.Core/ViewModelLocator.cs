using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Business.Domain.Interfaces;
using Ninject;
using ViHo.Service.Browsing;
using ViHo.Service.Navigation;
using ViHo.Service.Notification;

namespace MicroERP.Business.Core
{
    public sealed class ViewModelLocator
    {
        private IKernel kernel;

        public void Register(IKernel kernel, INavigationService navigationService, INotificationService notificationService, IBrowsingService browsingService)
        {
            this.kernel = kernel;

            this.kernel.Bind<INavigationService>().ToConstant(navigationService);
            this.kernel.Bind<INotificationService>().ToConstant(notificationService);
            this.kernel.Bind<IBrowsingService>().ToConstant(browsingService);
            this.kernel.Bind<ICustomerRepository>().ToMethod(c => RepositoryFactory.CreateRepository()).InSingletonScope();

            this.kernel.Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            this.kernel.Bind<CustomerWindowViewModel>().ToSelf().InTransientScope();
        }

        public MainWindowViewModel MainWindow
        {
            get { return this.kernel.Get<MainWindowViewModel>(); }
        }

        public CustomerWindowViewModel CustomerWindow
        {
            get { return this.kernel.Get<CustomerWindowViewModel>(); }
        }
    }
}

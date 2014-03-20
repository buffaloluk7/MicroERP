using MicroERP.Business.Factories;
using MicroERP.Business.ViewModels;
using MicroERP.Domain.Interfaces;
using MicroERP.Services.Core.Browser;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;
using Ninject;

namespace MicroERP.Business
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
            this.kernel.Bind<IRepository>().ToMethod(c => RepositoryFactory.CreateRepository()).InSingletonScope();

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

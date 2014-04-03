using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.Services;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Business.Domain.Repositories;
using Ninject;
using Luvi.Service.Browsing;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;

namespace MicroERP.Business.Core
{
    public sealed class ViewModelLocator
    {
        #region Properties

        private IKernel kernel;

        #endregion

        #region Register

        public void Register(IKernel kernel, INavigationService navigationService, INotificationService notificationService, IBrowsingService browsingService)
        {
            this.kernel = kernel;

            // Luvi services
            this.kernel.Bind<INavigationService>().ToConstant(navigationService);
            this.kernel.Bind<INotificationService>().ToConstant(notificationService);
            this.kernel.Bind<IBrowsingService>().ToConstant(browsingService);

            // Services
            this.kernel.Bind<ICustomerService>().To<CustomerService>().InSingletonScope();

            // Repositories
            this.kernel.Bind<ICustomerRepository>().ToMethod(r => RepositoryFactory.CreateCustomerRepository()).InSingletonScope();

            // ViewModels
            this.kernel.Bind<MainWindowViewModel>().ToSelf().InTransientScope();
            this.kernel.Bind<CustomerWindowViewModel>().ToSelf().InTransientScope();
        }

        #endregion

        #region ViewModels

        public MainWindowViewModel MainWindow
        {
            get 
            {
                if (this.kernel != null)
                {
                    return this.kernel.Get<MainWindowViewModel>(); 
                }
                return null;
            }
        }

        public CustomerWindowViewModel CustomerWindow
        {
            get
            {
                if (this.kernel != null)
                {
                    return this.kernel.Get<CustomerWindowViewModel>();
                }
                return null;
            }
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using Luvi.Service.Browsing;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.Services;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Customer;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Main;
using MicroERP.Business.Core.ViewModels.Main.Search;
using MicroERP.Business.Core.ViewModels.SearchBox;
using MicroERP.Business.Domain.Repositories;
using Microsoft.Practices.Unity;

namespace MicroERP.Business.Core
{
    public sealed class ViewModelLocator
    {
        #region Fields

        private readonly UnityContainer container;

        #endregion

        #region Properties

        public MainWindowViewModel MainWindow
        {
            get { return this.container.Resolve<MainWindowViewModel>(); }
        }

        public CustomerWindowViewModel CustomerWindow
        {
            get { return this.container.Resolve<CustomerWindowViewModel>(); }
        }

        public InvoiceWindowViewModel InvoiceWindow
        {
            get { return this.container.Resolve<InvoiceWindowViewModel>(); }
        }

        #endregion

        #region Constructors

        public ViewModelLocator()
        {
            this.container = new UnityContainer();

            // Inject sample platform services
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.container.RegisterType<INavigationService, SampleNavigationService>();
                this.container.RegisterType<INotificationService, SampleNotificationService>();
                this.container.RegisterType<IBrowsingService, SampleBrowsingService>();
            }

            // Services
            this.container.RegisterInstance<IUnityContainer>(this.container, new ContainerControlledLifetimeManager());
            this.container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IInvoiceService, InvoiceService>(new ContainerControlledLifetimeManager());

            // Repositories
            var repositories = RepositoryFactory.CreateRepositories();
            this.container.RegisterInstance<ICustomerRepository>(repositories.Item1, new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<IInvoiceRepository>(repositories.Item2, new ContainerControlledLifetimeManager());

            // Window ViewModels
            this.container.RegisterType<MainWindowViewModel>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<CustomerWindowViewModel>();
            this.container.RegisterType<InvoiceWindowViewModel>();

            // Main Search ViewModels
            this.container.RegisterType<SearchCustomersViewModel>();
            this.container.RegisterType<SearchInvoicesViewModel>();
            
            // Customer data + invoice data ViewModels
            this.container.RegisterType<CustomerDataViewModel>();
            this.container.RegisterType<InvoiceDataViewModel>();

            // SearchBox ViewModels
            this.container.RegisterType<CompanySearchBoxViewModel>();
            this.container.RegisterType<CustomerSearchBoxViewModel>();
        }

        #endregion

        #region Register

        public void Register(INavigationService navigationService, INotificationService notificationService, IBrowsingService browsingService)
        {
            // Luvi services
            this.container.RegisterInstance<INavigationService>(navigationService, new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<INotificationService>(notificationService, new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<IBrowsingService>(browsingService, new ContainerControlledLifetimeManager());
        }

        #endregion
    }
}

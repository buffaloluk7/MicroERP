using GalaSoft.MvvmLight;
using Luvi.Service.Browsing;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.Services;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Business.Core.ViewModels.Customers;
using MicroERP.Business.Core.ViewModels.Search.Company;
using MicroERP.Business.Core.ViewModels.Search.Customers;
using MicroERP.Business.Core.ViewModels.Search.Invoices;
using MicroERP.Business.Domain.Repositories;
using Microsoft.Practices.Unity;

namespace MicroERP.Business.Core
{
    public sealed class ViewModelLocator
    {
        #region Fields

        internal readonly UnityContainer container;

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

        #endregion

        #region Constructors

        public ViewModelLocator()
        {
            this.container = new UnityContainer();

            // Inject sample plattform services
            if (ViewModelBase.IsInDesignModeStatic)
            {
                container.RegisterType<INavigationService, SampleNavigationService>();
                container.RegisterType<INotificationService, SampleNotificationService>();
                container.RegisterType<IBrowsingService, SampleBrowsingService>();
            }

            // Services
            this.container.RegisterInstance<IUnityContainer>(this.container, new ContainerControlledLifetimeManager());
            this.container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IInvoiceService, InvoiceService>(new ContainerControlledLifetimeManager());

            // Repositories
            this.container.RegisterInstance<ICustomerRepository>(RepositoryFactory.CreateCustomerRepository(), new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<IInvoiceRepository>(RepositoryFactory.CreateInvoiceRepository(), new ContainerControlledLifetimeManager());

            // ViewModels
            this.container.RegisterType<MainWindowViewModel>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<CustomerWindowViewModel>();
            this.container.RegisterType<SearchCustomersViewModel>();
            this.container.RegisterType<SearchCompaniesViewModel>();
            this.container.RegisterType<SearchInvoicesViewModel>();
            this.container.RegisterType<CustomerDataViewModel>();
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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Browsing;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Exceptions;
using Newtonsoft.Json;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;
        private readonly IBrowsingService browsingService;
        private FullNameViewModel[] customers;

        public FullNameViewModel[] Customers
        {
            get { return this.customers; }
            set { base.Set<FullNameViewModel[]>(ref this.customers, value); }
        }
        
        #endregion

        #region Command Properties
        
        public RelayCommand<string> SearchCommand
        {
            get;
            private set;
        }

        public RelayCommand RepositoryCommand
        {
            get;
            private set;
        }

        public RelayCommand CreateCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand<FullNameViewModel> EditCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand<FullNameViewModel> DeleteCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(ICustomerService customerService, INotificationService notificationService, INavigationService navigationService, IBrowsingService browsingService)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.navigationService = navigationService;
            this.browsingService = browsingService;

            this.SearchCommand = new RelayCommand<string>(onSearchExecuted, onSearchCanExecute);
            this.RepositoryCommand = new RelayCommand(onRepositoryExecuted);
            this.CreateCustomerCommand = new RelayCommand(onCreateCustomerExecuted);
            this.EditCustomerCommand = new RelayCommand<FullNameViewModel>(onEditCustomerExecuted, onEditCustomerCanExecute);
            this.DeleteCustomerCommand = new RelayCommand<FullNameViewModel>(onDeleteCustomerExecuted, onDeleteCustomerCanExecute);
        }

        #endregion

        #region Commands Implementation

        private async void onSearchExecuted(string searchQuery)
        {
            var customers = await this.customerService.Read(searchQuery);
            this.Customers = customers.Select(c => new FullNameViewModel(c)).ToArray();
        }

        private bool onSearchCanExecute(string searchQuery)
        {
            return !string.IsNullOrWhiteSpace(searchQuery);
        }

        private void onRepositoryExecuted()
        {
            this.browsingService.OpenLinkAsync("https://github.com/buffaloluk7/micro_erp.git");
        }

        private void onCreateCustomerExecuted()
        {
            this.navigationService.Navigate<CustomerWindowViewModel>();
        }

        private void onEditCustomerExecuted(FullNameViewModel customer)
        {
            this.navigationService.NavigateAndSerialize<CustomerWindowViewModel>(customer.model, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects});
        }

        private bool onEditCustomerCanExecute(FullNameViewModel customer)
        {
            return customer != null;
        }

        private async void onDeleteCustomerExecuted(FullNameViewModel customer)
        {
            try
            {
                await this.customerService.Delete(customer.model.ID);

                var list = this.customers.ToList();
                list.Remove(customer);
                this.Customers = list.ToArray();
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
            }
        }

        private bool onDeleteCustomerCanExecute(FullNameViewModel customer)
        {
            return customer != null;
        }

        #endregion
    }
}

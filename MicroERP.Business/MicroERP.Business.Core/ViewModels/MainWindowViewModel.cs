using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Browsing;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Search.Customers;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using Newtonsoft.Json;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;
        private readonly IBrowsingService browsingService;
        private readonly SearchCustomersViewModel searchCustomersViewModel;

        #endregion

        #region Properties

        public SearchCustomersViewModel SearchCustomersViewModel
        {
            get { return this.searchCustomersViewModel; }
        }

        #endregion

        #region Commands

        public RelayCommand RepositoryCommand
        {
            get;
            private set;
        }

        public RelayCommand<CustomerType> CreateCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand EditCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(ICustomerService customerService, INotificationService notificationService, INavigationService navigationService, IBrowsingService browsingService, SearchCustomersViewModel searchCustomersViewModel)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.navigationService = navigationService;
            this.browsingService = browsingService;
            this.searchCustomersViewModel = searchCustomersViewModel;

            this.searchCustomersViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedCustomer")
                {
                    this.EditCustomerCommand.RaiseCanExecuteChanged();
                    this.DeleteCustomerCommand.RaiseCanExecuteChanged();
                }
            };

            this.RepositoryCommand = new RelayCommand(onRepositoryExecuted);
            this.CreateCustomerCommand = new RelayCommand<CustomerType>(onCreateCustomerExecuted);
            this.EditCustomerCommand = new RelayCommand(onEditCustomerExecuted, onEditCustomerCanExecute);
            this.DeleteCustomerCommand = new RelayCommand(onDeleteCustomerExecuted, onDeleteCustomerCanExecute);
        }

        #endregion

        #region Command Implementations

        private async void onRepositoryExecuted()
        {
            await this.browsingService.OpenLinkAsync("https://github.com/buffaloluk7/micro_erp.git");
        }

        private void onCreateCustomerExecuted(CustomerType type)
        {
            this.navigationService.Navigate<CustomerWindowViewModel>(type);
        }

        private async void onEditCustomerExecuted()
        {
            CustomerModel customer;

            try
            {
                customer = await this.customerService.Read(this.searchCustomersViewModel.SelectedCustomer.model.ID.Value);
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
                return;
            }

            await this.navigationService.NavigateAndSerialize<CustomerWindowViewModel>(customer, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects});
        }

        private bool onEditCustomerCanExecute()
        {
            return this.searchCustomersViewModel.SelectedCustomer != null;
        }

        private async void onDeleteCustomerExecuted()
        {
            var customer = this.searchCustomersViewModel.SelectedCustomer;

            try
            {
                await this.customerService.Delete(customer.model.ID.Value);
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
                return;
            }

            var customers = this.searchCustomersViewModel.Customers.ToList();
            customers.Remove(customer);
            this.searchCustomersViewModel.Customers = customers;
        }

        private bool onDeleteCustomerCanExecute()
        {
            return this.searchCustomersViewModel.SelectedCustomer != null;
        }

        #endregion
    }
}
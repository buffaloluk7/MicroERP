using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Customer;
using MicroERP.Business.Core.ViewModels.Main.Search;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using Newtonsoft.Json;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Main.Commands
{
    public class CustomerCommandsViewModel
    {
        #region Fields

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;
        private readonly SearchCustomersViewModel searchCustomersViewModel;

        #endregion

        #region Properties

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

        #region Constructor

        public CustomerCommandsViewModel(ICustomerService customerService, INotificationService notificationService, INavigationService navigationService, SearchCustomersViewModel searchCustomersViewModel)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.navigationService = navigationService;

            this.CreateCustomerCommand = new RelayCommand<CustomerType>(onCreateCustomerExecuted);
            this.EditCustomerCommand = new RelayCommand(onEditCustomerExecuted, onEditCustomerCanExecute);
            this.DeleteCustomerCommand = new RelayCommand(onDeleteCustomerExecuted, onDeleteCustomerCanExecute);

            this.searchCustomersViewModel = searchCustomersViewModel;
            this.searchCustomersViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedCustomer")
                {
                    this.EditCustomerCommand.RaiseCanExecuteChanged();
                    this.DeleteCustomerCommand.RaiseCanExecuteChanged();
                }
            };
        }

        #endregion

        #region Command Implementations

        private async void onCreateCustomerExecuted(CustomerType type)
        {
            if (this.navigationService is IWindowNavigationService)
            {
                await (this.navigationService as IWindowNavigationService).Navigate<CustomerWindowViewModel>(type, showDialog: true);
            }
            else
            {
                await this.navigationService.Navigate<CustomerWindowViewModel>(type);
            }
        }

        private async void onEditCustomerExecuted()
        {
            CustomerModel customer;

            try
            {
                customer = await this.customerService.Find(this.searchCustomersViewModel.SelectedCustomer.Model.ID);
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
                return;
            }

            if (this.navigationService is IWindowNavigationService)
            {
                await (this.navigationService as IWindowNavigationService).NavigateAndSerialize<CustomerWindowViewModel>(customer, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects }, showDialog: true);
            }
            else
            {
                await this.navigationService.NavigateAndSerialize<CustomerWindowViewModel>(customer, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            }
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
                await this.customerService.Delete(customer.Model.ID);
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

﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Domain.Exceptions;
using MicroERP.Domain.Interfaces;
using MicroERP.Services.Core.Browser;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        private readonly ICustomerRepository repository;
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

        #region Commands

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

        public MainWindowViewModel(ICustomerRepository customerRepository, INotificationService notificationService, INavigationService navigationService, IBrowsingService browsingService)
        {
            this.repository = customerRepository;
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

        private async void onSearchExecuted(string query)
        {
            var customers = await this.repository.ReadCustomers(query);
            this.Customers = customers.Select(C => new FullNameViewModel(C)).ToArray();
        }

        private bool onSearchCanExecute(string query)
        {
            return !string.IsNullOrWhiteSpace(query);
        }

        private void onRepositoryExecuted()
        {
            this.browsingService.OpenLinkAsync("https://github.com/buffaloluk7/micro_erp.git");
        }

        private void onCreateCustomerExecuted()
        {
            this.navigationService.Navigate<CustomerWindowViewModel>(showDialog: true);
        }

        private void onEditCustomerExecuted(FullNameViewModel customer)
        {
            string customerAsJson = JsonConvert.SerializeObject(customer.model, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            this.navigationService.Navigate<CustomerWindowViewModel>(customerAsJson, true);
        }

        private bool onEditCustomerCanExecute(FullNameViewModel customer)
        {
            return customer != null;
        }

        private async void onDeleteCustomerExecuted(FullNameViewModel customer)
        {
            try
            {
                await this.repository.DeleteCustomer(customer.model.ID);

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

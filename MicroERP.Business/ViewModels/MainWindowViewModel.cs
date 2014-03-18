using GalaSoft.MvvmLight;
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

        private readonly IRepository repository;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;
        private readonly IBrowsingService browsingService;
        private IEnumerable<FullNameViewModel> customers;

        public IEnumerable<FullNameViewModel> Customers
        {
            get { return this.customers; }
            set { base.Set<IEnumerable<FullNameViewModel>>(ref this.customers, value); }
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

        public MainWindowViewModel(IRepository repository, INotificationService notificationService, INavigationService navigationService, IBrowsingService browsingService)
        {
            this.repository = repository;
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
            this.Customers = customers.Select(C => new FullNameViewModel(C));
        }

        private bool onSearchCanExecute(string query)
        {
            return !string.IsNullOrWhiteSpace(query);
        }

        private void onRepositoryExecuted()
        {
            this.browsingService.OpenLink("https://github.com/buffaloluk7/micro_erp.git");
        }

        private void onCreateCustomerExecuted()
        {
            this.navigationService.Show<CustomerWindowViewModel>(showDialog: true);
        }

        private void onEditCustomerExecuted(FullNameViewModel customer)
        {
            string customerAsJson = JsonConvert.SerializeObject(customer.model, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            this.navigationService.Show<CustomerWindowViewModel>(customerAsJson, true);
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
            }
            catch (CustomerNotFoundException)
            {
                this.notificationService.Show("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }

            var list = this.customers.ToList();
            list.Remove(customer);
            this.Customers = list;
        }

        private bool onDeleteCustomerCanExecute(FullNameViewModel customer)
        {
            return customer != null;
        }

        #endregion
    }
}

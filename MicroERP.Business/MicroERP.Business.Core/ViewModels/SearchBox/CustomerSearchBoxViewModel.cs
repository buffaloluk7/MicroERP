using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.SearchBox
{
    public class CustomerSearchBoxViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private IEnumerable<CustomerDisplayNameViewModel> customers;
        private CustomerDisplayNameViewModel selectedCustomer;
        private string searchQuery;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchCustomerCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<CustomerDisplayNameViewModel> Customers
        {
            get { return this.customers; }
            set { base.Set<IEnumerable<CustomerDisplayNameViewModel>>(ref this.customers, value); }
        }

        public CustomerDisplayNameViewModel SelectedCustomer
        {
            get { return this.selectedCustomer; }
            set
            {
                base.Set<CustomerDisplayNameViewModel>(ref this.selectedCustomer, value);
                this.SearchQuery = value != null ? value.DisplayName : null;
            }
        }

        #endregion

        #region Commands

        public RelayCommand SearchCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerSearchBoxViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
            this.SearchCustomerCommand = new RelayCommand(this.onSearchCustomersExecuted, this.onSearchCustomersCanExecute);

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.searchQuery = "i";
                this.onSearchCustomersExecuted();
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private bool onSearchCustomersCanExecute()
        {
            if (string.IsNullOrWhiteSpace(this.searchQuery))
            {
                this.Customers = null;
                return false;
            }

            return true;
        }

        private async void onSearchCustomersExecuted()
        {
            var customers = await this.customerService.Search(this.searchQuery);

            if (customers.Count() == 1)
            {
                this.SelectedCustomer = new CustomerDisplayNameViewModel(customers.First());
            }
            else
            {
                this.Customers = customers.Select(c => new CustomerDisplayNameViewModel(c));
            }
        }

        #endregion
    }
}

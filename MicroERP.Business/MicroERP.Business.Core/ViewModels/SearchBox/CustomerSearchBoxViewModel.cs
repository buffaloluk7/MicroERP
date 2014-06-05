using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
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
        private CustomerType customerType;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchCustomerCommand.RaiseCanExecuteChanged();
                this.ResetCustomerCommand.RaiseCanExecuteChanged();
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
                this.SearchQuery = value == null ? null : value.DisplayName;
            }
        }

        public bool IsSelected
        {
            get { return this.selectedCustomer != null; }
        }

        #endregion

        #region Commands

        public RelayCommand SearchCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerSearchBoxViewModel(ICustomerService customerService, CustomerModel customer = null, CustomerType customerType = CustomerType.None)
        {
            this.customerService = customerService;
            this.customerType = customerType;

            this.SearchCustomerCommand = new RelayCommand(this.onSearchCustomerExecuted, this.onSearchCustomerCanExecute);
            this.ResetCustomerCommand = new RelayCommand(this.onResetCustomerExecuted, this.onResetCustomerCanExecute);

            this.SelectedCustomer = customer == null ? null : new CustomerDisplayNameViewModel(customer);

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.searchQuery = "i";
                this.onSearchCustomerExecuted();
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private bool onSearchCustomerCanExecute()
        {
            if (string.IsNullOrWhiteSpace(this.searchQuery))
            {
                this.Customers = null;
                return false;
            }

            return true;
        }

        private async void onSearchCustomerExecuted()
        {
            var customers = await this.customerService.Search(this.searchQuery, true, this.customerType);
            if (customers.Count() == 1)
            {
                this.SelectedCustomer = new CustomerDisplayNameViewModel(customers.First());
            }
            else
            {
                this.Customers = customers.Select(c => new CustomerDisplayNameViewModel(c));
            }
        }

        private bool onResetCustomerCanExecute()
        {
            return this.IsSelected;
        }

        private void onResetCustomerExecuted()
        {
            this.SelectedCustomer = null;
        }

        #endregion
    }
}

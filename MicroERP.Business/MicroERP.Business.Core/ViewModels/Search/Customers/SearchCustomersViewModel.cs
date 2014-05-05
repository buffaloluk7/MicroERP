using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Search.Customers
{
    public class SearchCustomersViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private IEnumerable<CustomerElementViewModel> customers;
        private CustomerElementViewModel selectedCustomer;
        private string searchQuery;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchCustomersCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<CustomerElementViewModel> Customers
        {
            get { return this.customers; }
            set { base.Set<IEnumerable<CustomerElementViewModel>>(ref this.customers, value); }
        }

        public CustomerElementViewModel SelectedCustomer
        {
            get { return this.selectedCustomer; }
            set { base.Set<CustomerElementViewModel>(ref this.selectedCustomer, value); }
        }

        #endregion

        #region Commands

        public RelayCommand SearchCustomersCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public SearchCustomersViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
            this.SearchCustomersCommand = new RelayCommand(this.onSearchCustomersExecuted, this.onSearchCustomersCanExecute);

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
            var customers = await this.customerService.Search(this.SearchQuery);
            this.Customers = customers.Select(customer => new CustomerElementViewModel(customer));
        }

        #endregion
    }
}

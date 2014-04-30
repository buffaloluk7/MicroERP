using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels
{
    public class SearchViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private IEnumerable<FullNameViewModel> customers;
        private FullNameViewModel selectedCustomer;
        private string searchQuery;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get
            {
                return this.searchQuery;
            }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<FullNameViewModel> Customers
        {
            get
            {
                return this.customers;
            }
            set
            {
                if (base.Set<IEnumerable<FullNameViewModel>>(ref this.customers, value))
                {
                    base.RaisePropertyChanged(() => this.CustomersCount);
                }
            }
        }

        public int? CustomersCount
        {
            get
            {
                if (this.customers != null)
                {
                    return this.customers.Count();
                }
                return null;
            }
        }

        public FullNameViewModel SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }
            set
            {
                base.Set<FullNameViewModel>(ref this.selectedCustomer, value);
            }
        }

        #endregion

        #region Commands

        public RelayCommand SearchCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public SearchViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
            this.SearchCommand = new RelayCommand(this.onSearchExecuted, this.onSearchCanExecute);
        }

        #endregion

        #region Command Implementations

        private bool onSearchCanExecute()
        {
            if (string.IsNullOrWhiteSpace(this.searchQuery))
            {
                this.Customers = null;
                return false;
            }
            return true;
        }

        private async void onSearchExecuted()
        {
            var customers = await this.customerService.Read(this.SearchQuery);

            this.Customers = customers.Select(customer => new FullNameViewModel(customer));
        }

        #endregion
    }
}

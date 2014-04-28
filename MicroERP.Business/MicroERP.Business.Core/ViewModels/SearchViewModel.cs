using Luvi.Mvvm;
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

        #endregion

        #region Properties

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

        public RelayCommand<string> SearchCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public SearchViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
            this.SearchCommand = new RelayCommand<string>(this.onSearchExecuted, this.onSearchCanExecute);
        }

        #endregion

        #region Command Implementations

        private bool onSearchCanExecute(string searchQuery)
        {
            return !string.IsNullOrWhiteSpace(searchQuery);
        }

        private async void onSearchExecuted(string searchQuery)
        {
            var customers = await this.customerService.Read(searchQuery);

            this.Customers = customers.Select(customer => new FullNameViewModel(customer));
        }

        #endregion
    }
}

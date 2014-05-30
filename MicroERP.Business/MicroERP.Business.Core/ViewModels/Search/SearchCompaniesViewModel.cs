using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Search
{
    public class SearchCompaniesViewModel : ObservableObject
    {
        #region Fields

        private readonly PersonModel person;
        private readonly ICustomerService customerService;
        private IEnumerable<CustomerDisplayNameViewModel> companies;
        private CustomerDisplayNameViewModel selectedCompany;
        private string searchQuery;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchCompaniesCommand.RaiseCanExecuteChanged();
                this.RemoveFromCompanyCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<CustomerDisplayNameViewModel> Companies
        {
            get { return this.companies; }
            set { base.Set<IEnumerable<CustomerDisplayNameViewModel>>(ref this.companies, value); }
        }

        public CustomerDisplayNameViewModel SelectedCompany
        {
            get { return this.selectedCompany; }
            set
            {
                base.Set<CustomerDisplayNameViewModel>(ref this.selectedCompany, value);
                this.person.Company = value != null ? value.Model as CompanyModel : null;
                this.SearchQuery = value != null ? value.DisplayName : null;
            }
        }

        #endregion

        #region Commands

        public RelayCommand SearchCompaniesCommand
        {
            get;
            private set;
        }

        public RelayCommand RemoveFromCompanyCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public SearchCompaniesViewModel(ICustomerService customerService, PersonModel person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            this.customerService = customerService;
            this.person = person;
            this.SearchCompaniesCommand = new RelayCommand(this.onSearchCompaniesExecuted, this.onSearchCompaniesCanExecute);
            this.RemoveFromCompanyCommand = new RelayCommand(this.onRemoveFromCompanyExecuted, this.onRemoveFromCompanyCanExecute);

            if (this.person.Company != null)
            {
                this.searchQuery = new CustomerDisplayNameViewModel(this.person.Company).DisplayName;
            }

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.searchQuery = "i";
                this.onSearchCompaniesExecuted();
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private bool onSearchCompaniesCanExecute()
        {
            if (string.IsNullOrWhiteSpace(this.searchQuery))
            {
                this.Companies = null;
                return false;
            }

            return true;
        }

        private async void onSearchCompaniesExecuted()
        {
            var companies = await this.customerService.Search(this.searchQuery, false, CustomerType.Company);

            if (companies.Count() == 1)
            {
                var company = companies.First() as CompanyModel;
                this.SelectedCompany = new CustomerDisplayNameViewModel(company);
            }
            else
            {
                this.Companies = companies.OfType<CompanyModel>().Select(c => new CustomerDisplayNameViewModel(c));
            }
        }

        private bool onRemoveFromCompanyCanExecute()
        {
            return this.person.CompanyID.HasValue;
        }

        private void onRemoveFromCompanyExecuted()
        {
            this.SelectedCompany = null;
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Customers;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.ViewModels.Search.Company
{
    public class SearchCompaniesViewModel : ObservableObject
    {
        #region Fields

        private readonly PersonViewModel person;
        private readonly ICustomerService customerService;
        private IEnumerable<CompanyElementViewModel> companies;
        private CompanyElementViewModel selectedCompany;
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
            }
        }

        public IEnumerable<CompanyElementViewModel> Companies
        {
            get { return this.companies; }
            set { base.Set<IEnumerable<CompanyElementViewModel>>(ref this.companies, value); }
        }

        public CompanyElementViewModel SelectedCompany
        {
            get { return this.selectedCompany; }
            set { base.Set<CompanyElementViewModel>(ref this.selectedCompany, value); }
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

        public SearchCompaniesViewModel(ICustomerService customerService, PersonViewModel person)
        {
            this.customerService = customerService;
            this.person = person;
            this.SearchCompaniesCommand = new RelayCommand(this.onSearchCompaniesExecuted, this.onSearchCompaniesCanExecute);
            this.RemoveFromCompanyCommand = new RelayCommand(this.onRemoveFromCompanyExecuted, this.onRemoveFromCompanyCanExecute);

            this.SearchQuery = this.person.Company.Name;

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
            var companies = await this.customerService.Search(this.searchQuery, false, true);

            if (companies.Count() == 1)
            {
                var newCompany = (companies.First() as CompanyModel);

                this.SearchQuery = newCompany.Name;
                this.Companies = null;
                this.person.Company = new CompanyViewModel(newCompany);
            }
            else
            {
                this.Companies = companies.OfType<CompanyModel>().Select(company => new CompanyElementViewModel(company));
            }
        }

        private bool onRemoveFromCompanyCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this.searchQuery);
        }

        private void onRemoveFromCompanyExecuted()
        {
            this.SearchQuery = null;
            this.Companies = null;
        }

        #endregion
    }
}

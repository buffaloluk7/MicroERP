using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Search.Company
{
    public class SearchCompaniesViewModel : ObservableObject
    {
        #region Fields

        private readonly PersonModel person;
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
                this.RemoveFromCompanyCommand.RaiseCanExecuteChanged();
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
            set
            {
                // selectedCompany is set to null, when changing the company.
                // This will clear the companies list, which calls the SelectedCompany property.
                // In this case, the value is set to null and we should not update the company.
                // Unless I know how to hide the dropdown with a behaviour when a item gets selected,
                // I will go this way. Maybe it is then possible to bind only the selectedCompany and
                // remove the searchQuery? I dont think so, where do I store the searchQuery then?
                if (value != null)
                {
                    base.Set<CompanyElementViewModel>(ref this.selectedCompany, value);
                    this.changeCompany(value == null ? null : value.Model);
                }
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
            this.customerService = customerService;
            this.person = person;
            this.SearchCompaniesCommand = new RelayCommand(this.onSearchCompaniesExecuted, this.onSearchCompaniesCanExecute);
            this.RemoveFromCompanyCommand = new RelayCommand(this.onRemoveFromCompanyExecuted, this.onRemoveFromCompanyCanExecute);

            if (this.person.Company != null)
            {
                this.SearchQuery = this.person.Company.Name;
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
            var companies = await this.customerService.Search(this.searchQuery, false, true);

            if (companies.Count() == 1)
            {
                this.changeCompany((companies.First() as CompanyModel));
            }
            else
            {
                this.Companies = companies.OfType<CompanyModel>().Select(company => new CompanyElementViewModel(company));
                this.RaisePropertyChanged(() => this.Companies);
            }
        }

        private bool onRemoveFromCompanyCanExecute()
        {
            return this.person.CompanyID.HasValue;
        }

        private void onRemoveFromCompanyExecuted()
        {
            this.changeCompany(null);
        }

        #endregion

        private void changeCompany(CompanyModel company)
        {
            //this.Companies = null;
            this.person.Company = company;
            this.SearchQuery = (company == null) ? null : company.Name;
        }
    }
}

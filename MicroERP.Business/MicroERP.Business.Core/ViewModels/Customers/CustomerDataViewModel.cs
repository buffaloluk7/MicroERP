using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Search.Company;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using Microsoft.Practices.Unity;
using System;

namespace MicroERP.Business.Core.ViewModels.Customers
{
    public class CustomerDataViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private CustomerModel customer;
        private readonly CustomerViewModel customerViewModel;
        private readonly CompanyViewModel companyViewModel;
        private readonly PersonViewModel personViewModel;


        #endregion

        #region Properties

        public bool IsCreating
        {
            get { return !this.customer.ID.HasValue; }
        }

        public CustomerViewModel Customer
        {
            get { return this.customerViewModel; }
        }

        public CompanyViewModel Company
        {
            get { return this.companyViewModel; }
        }

        public PersonViewModel Person
        {
            get { return this.personViewModel; }
        }

        public SearchCompaniesViewModel SearchCompaniesViewModel
        {
            get;
            private set;
        }

        #endregion

        #region Commands

        public RelayCommand SaveCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerDataViewModel(IUnityContainer container, ICustomerService customerService, INotificationService notificationService, CustomerModel customer)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);

            this.customer = customer;
            this.customerViewModel = new CustomerViewModel(customer);

            var company = customer as CompanyModel;
            var person = customer as PersonModel;

            if (company != null)
            {
                this.companyViewModel = new CompanyViewModel(company);
            }
            else if (person != null)
            {
                this.personViewModel = new PersonViewModel(person);
                this.SearchCompaniesViewModel = container.Resolve<SearchCompaniesViewModel>(new ParameterOverride("person", person));
            }
            else
            {
                throw new ArgumentOutOfRangeException("customer");
            }

            this.RaisePropertyChanged(() => this.IsCreating);
        }

        #endregion

        #region Command Implementations

        private bool onSaveCustomerCanExecute()
        {
            return true;
        }

        private async void onSaveCustomerExecuted()
        {
            try
            {
                if (this.customer.ID.HasValue)
                {
                    await this.customerService.Update(this.customer);
                    await this.notificationService.ShowAsync("Kunde erfolgreich aktualisiert.", "Kunde bearbeitet");
                }
                else
                {
                    await this.customerService.Create(this.customer);
                    await this.notificationService.ShowAsync("Kunde erfolgreich erstellt. Sie können nun Rechnungen für diesen Kunden ausstellen.", "Kunde erstellt");
                }

                this.RaisePropertyChanged(() => this.IsCreating);
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
            }
        }

        #endregion
    }
}

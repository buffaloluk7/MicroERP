using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Core.ViewModels.SearchBox;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using Microsoft.Practices.Unity;
using System;

namespace MicroERP.Business.Core.ViewModels.Customer
{
    public class CustomerDataViewModel : ObservableObject
    {
        #region Fields

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly CustomerModel customer;
        private readonly CustomerModelViewModel customerViewModel;
        private readonly CompanyModelViewModel companyViewModel;
        private readonly PersonModelViewModel personViewModel;

        #endregion

        #region Properties

        public bool IsCreating
        {
            get { return this.customer.ID == default(int); }
        }

        public CustomerModelViewModel Customer
        {
            get { return this.customerViewModel; }
        }

        public CompanyModelViewModel Company
        {
            get { return this.companyViewModel; }
        }

        public PersonModelViewModel Person
        {
            get { return this.personViewModel; }
        }

        public CustomerSearchBoxViewModel CustomerSearchBoxViewModel
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
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            this.customerService = customerService;
            this.notificationService = notificationService;
            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);

            this.customer = customer;
            this.customerViewModel = new CustomerModelViewModel(customer);

            var company = customer as CompanyModel;
            if (company != null)
            {
                this.companyViewModel = new CompanyModelViewModel(company);
                return;
            }

            var person = customer as PersonModel;
            if (person != null)
            {
                this.personViewModel = new PersonModelViewModel(person);

                if (person.Company == null)
                {
                    this.CustomerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>(new ParameterOverrides { { "customerType", CustomerType.Company } });
                }
                else
                {
                    this.CustomerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>(new ParameterOverrides { { "customer", person.Company }, { "customerType", CustomerType.Company } });
                }

                this.CustomerSearchBoxViewModel.PropertyChanged += ((s, e) =>
                {
                    if (e.PropertyName == "selectedCustomer")
                    {
                        var selectedCustomer = s as CustomerDisplayNameViewModel;
                        person.Company = selectedCustomer == null ? null : selectedCustomer.Model as CompanyModel;
                    }
                });

                return;
            }

            throw new ArgumentOutOfRangeException("customer");
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
                if (this.customer.ID != default(int))
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

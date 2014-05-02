using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
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

        #endregion

        #region Commands

        public RelayCommand SaveCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerDataViewModel(ICustomerService customerService, INotificationService notificationService, CustomerModel customer)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);

            this.customer = customer;
            this.customerViewModel = new CustomerViewModel(customer);

            if (customer is CompanyModel)
            {
                this.companyViewModel = new CompanyViewModel(customer as CompanyModel);
            }
            else if (customer is PersonModel)
            {
                this.personViewModel = new PersonViewModel(customer as PersonModel);
            }
            else
            {
                throw new ArgumentOutOfRangeException("customer");
            }
        }

        #endregion

        #region Command Implementations

        private bool onSaveCustomerCanExecute()
        {
            return true;
        }

        private void onSaveCustomerExecuted()
        {
            try
            {
                if (this.customer.ID.HasValue)
                {
                    this.customerService.Update(this.customer);
                }
                else
                {
                    this.customerService.Create(this.customer);
                }
            }
            catch (CustomerAlreadyExistsException)
            {
                this.notificationService.ShowAsync("Kunde existiert bereits.", "Fehler");
            }
            catch (CustomerNotFoundException)
            {
                this.notificationService.ShowAsync("Der Kunde wurde in der Datenbank nicht gefunden.", "Fehler");
            }
        }

        #endregion
    }
}

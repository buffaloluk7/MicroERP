using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Json.Extension;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Main.Search;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Customer
{
    public class CustomerWindowViewModel : ObservableObject, INavigationAware
    {
        #region Fields

        private readonly IUnityContainer container;
        private readonly ICustomerService customerService;
        private readonly IInvoiceService invoiceService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;

        #endregion

        #region Propterties

        public CustomerDataViewModel CustomerDataViewModel
        {
            get;
            private set;
        }

        public InvoiceDataViewModel InvoiceDataViewModel
        {
            get;
            private set;
        }

        #endregion

        #region Commands

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerWindowViewModel(IUnityContainer container, ICustomerService customerService, IInvoiceService invoiceService, INotificationService notificationService, INavigationService navigationService)
        {
            this.container = container;
            this.customerService = customerService;
            this.invoiceService = invoiceService;
            this.notificationService = notificationService;
            this.navigationService = navigationService;

            this.CancelCommand = new RelayCommand(onCancelExecuted);

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.customerService.Search("lukas").ContinueWith((t) =>
                {
                    this.OnNavigatedTo(t.Result.First().ToJson(new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects }), NavigationType.Forward);
                });
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private void onCancelExecuted()
        {
            if (navigationService is IFrameNavigationService)
            {
                (navigationService as IFrameNavigationService).BackCommand.Execute(null);
            }
            else if (navigationService is IWindowNavigationService)
            {
                (navigationService as IWindowNavigationService).Close(this);
            }
        }

        #endregion

        #region INavigationAware

        public async void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            var customerRaw = argument as string;
            CustomerModel customer = null;

            if (argument is CustomerType)
            {
                customer = CustomerModelFactory.FromType((CustomerType)argument);
            }
            else if (customerRaw != null)
            {
                customer = await customerRaw.FromJsonAsync<CustomerModel>(new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            }
            else
            {
                throw new ArgumentOutOfRangeException("argument");
            }

            var person = customer as PersonModel;
            if (person != null && person.CompanyID.HasValue)
            {
                person.Company = await this.customerService.Find<CompanyModel>(person.CompanyID.Value);
                customer = person;
            }

            // Retrieve invoices for the given customer
            customer.Invoices = new ObservableCollection<InvoiceModel>(await this.invoiceService.Search(customer.ID));

            this.CustomerDataViewModel = this.container.Resolve<CustomerDataViewModel>(new ParameterOverride("customer", customer));
            this.InvoiceDataViewModel = this.container.Resolve<InvoiceDataViewModel>(new ParameterOverride("invoices", customer.Invoices));
            this.RaisePropertyChanged(() => this.CustomerDataViewModel);
            this.RaisePropertyChanged(() => this.InvoiceDataViewModel);
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}

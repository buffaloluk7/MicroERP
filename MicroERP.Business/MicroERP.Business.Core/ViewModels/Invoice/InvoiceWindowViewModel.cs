using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Core.ViewModels.SearchBox;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Invoice
{
    public class InvoiceWindowViewModel : ObservableObject, INavigationAware
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private readonly ICustomerService customerService;

        private readonly INavigationService navigationService;
        private readonly INotificationService notificationService;

        private readonly InvoiceModelViewModel invoiceModelViewModel;
        private readonly CustomerSearchBoxViewModel customerSearchBoxViewModel;

        #endregion

        #region Properties

        public InvoiceModelViewModel Invoice
        {
            get { return this.invoiceModelViewModel; }
        }

        public CustomerSearchBoxViewModel CustomerSearchBoxViewModel
        {
            get { return this.customerSearchBoxViewModel; }
        }

        #endregion

        #region Commands

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveInvoiceCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public InvoiceWindowViewModel(IUnityContainer container, IInvoiceService invoiceService, ICustomerService customerService, INavigationService navigationService, INotificationService notificationService)
        {
            this.invoiceService = invoiceService;
            this.customerService = customerService;
            this.navigationService = navigationService;
            this.notificationService = notificationService;

            this.CancelCommand = new RelayCommand(onCancelExecuted);
            this.SaveInvoiceCommand = new RelayCommand(onSaveInvoiceExecuted, onSaveInvoiceCanExecute);

            this.invoiceModelViewModel = new InvoiceModelViewModel();
            this.invoiceModelViewModel.InvoiceItems.CollectionChanged += ((s, e) =>
            {
                this.SaveInvoiceCommand.RaiseCanExecuteChanged();
            });
            this.invoiceModelViewModel.IssueDate = DateTime.Now;
            this.invoiceModelViewModel.DueDate = DateTime.Now.AddDays(7);

            this.customerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>();
            this.customerSearchBoxViewModel.PropertyChanged += ((s, e) =>
            {
                this.SaveInvoiceCommand.RaiseCanExecuteChanged();
            });
        }

        #endregion

        #region Command Implementations

        private bool onSaveInvoiceCanExecute()
        {
            return this.Invoice.InvoiceItems.Count > 0 &&
                this.customerSearchBoxViewModel.SelectedCustomer != null;
        }

        private async void onSaveInvoiceExecuted()
        {
            var invoice = this.Invoice.Model;
            var invoiceItems = this.Invoice.InvoiceItems.Select(ii => ii.Model);

            invoice.InvoiceItems = new ObservableCollection<InvoiceItemModel>(invoiceItems);

            try
            {
                var customerID = this.customerSearchBoxViewModel.SelectedCustomer.Model.ID;
                await this.invoiceService.Create(customerID, invoice);
                await this.notificationService.ShowAsync("Rechnung erfolgreich erstellt.", "Rechnung erstellt");
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde konnte in der Datenbank nicht gefunden werden.", "Kunde nicht gefunden");
            }

            // Close the window by using the cancel command
            this.onCancelExecuted();
        }

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

        public void OnNavigatedFrom() { }

        public async void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            if (argument != null)
            {
                var customer = await this.customerService.Find((int)argument);
                this.customerSearchBoxViewModel.SelectedCustomer = new CustomerDisplayNameViewModel(customer);
            }
        }

        #endregion
    }
}

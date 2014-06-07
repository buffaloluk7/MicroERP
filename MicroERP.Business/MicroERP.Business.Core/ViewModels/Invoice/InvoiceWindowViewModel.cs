using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public decimal SubTotal { get; private set; }

        #endregion

        #region Commands

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand SaveInvoiceCommand { get; private set; }

        public RelayCommand InvoiceItemEditedCommand { get; private set; }

        #endregion

        #region Constructor

        public InvoiceWindowViewModel(IUnityContainer container, IInvoiceService invoiceService,
            ICustomerService customerService, INavigationService navigationService,
            INotificationService notificationService)
        {
            this.invoiceService = invoiceService;
            this.customerService = customerService;
            this.navigationService = navigationService;
            this.notificationService = notificationService;

            this.CancelCommand = new RelayCommand(onCancelExecuted);
            this.SaveInvoiceCommand = new RelayCommand(onSaveInvoiceExecuted, onSaveInvoiceCanExecute);
            this.InvoiceItemEditedCommand = new RelayCommand(onInvoiceItemEditedExecuted);

            this.invoiceModelViewModel = new InvoiceModelViewModel();
            this.invoiceModelViewModel.PropertyChanged +=
                ((s, e) => this.SaveInvoiceCommand.RaiseCanExecuteChanged());
            this.invoiceModelViewModel.InvoiceItems.CollectionChanged +=
                ((s, e) => this.SaveInvoiceCommand.RaiseCanExecuteChanged());

            this.invoiceModelViewModel.IssueDate = DateTime.Now;
            this.invoiceModelViewModel.DueDate = DateTime.Now.AddDays(7);

            this.customerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>();
            this.customerSearchBoxViewModel.PropertyChanged +=
                ((s, e) => this.SaveInvoiceCommand.RaiseCanExecuteChanged());
        }

        #endregion

        #region Command Implementations

        private bool onSaveInvoiceCanExecute()
        {
            this.SubTotal =
                this.Invoice.InvoiceItems.Where(ii => ii.IsValid()).Sum(ii => ii.UnitPrice*ii.Amount*(ii.Tax/100 + 1));
            this.RaisePropertyChanged("SubTotal");

            return this.Invoice.InvoiceItems.Count(ii => ii.IsValid()) > 0
                   && this.Invoice.IssueDate >= DateTime.Now.AddDays(-1)
                   && this.Invoice.DueDate >= this.Invoice.IssueDate
                   && this.customerSearchBoxViewModel.SelectedCustomer != null;
        }

        private async void onSaveInvoiceExecuted()
        {
            var invoice = this.Invoice.Model;
            var invoiceItems = this.Invoice.InvoiceItems.Where(ii => ii.IsValid()).Select(ii => ii.Model);

            invoice.InvoiceItems = new ObservableCollection<InvoiceItemModel>(invoiceItems);

            try
            {
                var customerID = this.customerSearchBoxViewModel.SelectedCustomer.Model.ID;
                await this.invoiceService.Create(customerID, invoice);
                await this.notificationService.ShowAsync("Rechnung erfolgreich erstellt.", "Rechnung erstellt");
            }
            catch (CustomerNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Der Kunde konnte in der Datenbank nicht gefunden werden.",
                    "Kunde nicht gefunden");
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

        private void onInvoiceItemEditedExecuted()
        {
            this.SaveInvoiceCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region INavigationAware

        public void OnNavigatedFrom()
        {
        }

        public async void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            if (argument != null)
            {
                var customer = await this.customerService.Find((int) argument);
                this.customerSearchBoxViewModel.SelectedCustomer = new CustomerDisplayNameViewModel(customer);
            }
        }

        #endregion
    }
}
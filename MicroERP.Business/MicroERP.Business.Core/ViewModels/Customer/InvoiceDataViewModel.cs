using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Customer
{
    public class InvoiceDataViewModel
    {
        #region Fields

        private readonly INavigationService navigationService;
        private readonly INotificationService notificationService;
        private readonly IInvoiceService invoiceService;

        private readonly IEnumerable<InvoiceModelViewModel> invoices;
        private InvoiceModelViewModel selectedInvoice;
        private readonly int customerID;

        #endregion

        #region Properties

        public IEnumerable<InvoiceModelViewModel> Invoices
        {
            get { return this.invoices; }
        }

        public InvoiceModelViewModel SelectedInvoice
        {
            get { return this.selectedInvoice; }
            set
            {
                this.selectedInvoice = value;
                this.ExportInvoiceCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CreateInvoiceCommand { get; private set; }

        public RelayCommand ExportInvoiceCommand { get; private set; }

        #endregion

        #region Constructor

        public InvoiceDataViewModel(INavigationService navigationService, INotificationService notificationService, IInvoiceService invoiceService, IEnumerable<InvoiceModel> invoices, int customerID)
        {
            this.invoices = invoices.Select(i => new InvoiceModelViewModel(i));
            this.customerID = customerID;

            this.navigationService = navigationService;
            this.notificationService = notificationService;
            this.invoiceService = invoiceService;

            this.CreateInvoiceCommand = new RelayCommand(onCreateInvoiceExecuted);
            this.ExportInvoiceCommand = new RelayCommand(onExportInvoiceExecuted, onExportInvoiceCanExecute);
        }

        #endregion

        #region Command Implementations

        private async void onCreateInvoiceExecuted()
        {
            if (this.navigationService is IWindowNavigationService)
            {
                await
                    (this.navigationService as IWindowNavigationService).Navigate<InvoiceWindowViewModel>(
                        this.customerID, showDialog: true);
            }
            else
            {
                await this.navigationService.Navigate<InvoiceWindowViewModel>();
            }
        }

        private bool onExportInvoiceCanExecute()
        {
            return this.SelectedInvoice != null;
        }

        private async void onExportInvoiceExecuted()
        {
            try
            {
                await this.invoiceService.Export(this.SelectedInvoice.ID);
            }
            catch (InvoiceNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Rechnung wurde nicht gefunden", "Rechnungen nicht gefunden");
            }
        }

        #endregion
    }
}
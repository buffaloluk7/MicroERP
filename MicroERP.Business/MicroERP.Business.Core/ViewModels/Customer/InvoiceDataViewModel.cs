using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.ViewModels.Customer
{
    public class InvoiceDataViewModel
    {
        #region Fields

        private readonly INavigationService navigationService;
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

        public InvoiceDataViewModel(INavigationService navigationService, IEnumerable<InvoiceModel> invoices, int customerID)
        {
            this.invoices = invoices.Select(i => new InvoiceModelViewModel(i));
            this.customerID = customerID;

            this.navigationService = navigationService;

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

        private void onExportInvoiceExecuted()
        {
            // download pdf using invoiceservice
        }

        #endregion
    }
}
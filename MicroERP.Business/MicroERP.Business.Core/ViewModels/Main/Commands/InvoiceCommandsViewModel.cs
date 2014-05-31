using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Main.Search;

namespace MicroERP.Business.Core.ViewModels.Main.Commands
{
    public class InvoiceCommandsViewModel
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private readonly SearchInvoicesViewModel searchInvoicesViewModel;

        #endregion

        #region Properties

        public RelayCommand ExportInvoiceCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public InvoiceCommandsViewModel(IInvoiceService invoiceService, SearchInvoicesViewModel searchInvoicesViewModel)
        {
            this.invoiceService = invoiceService;

            this.ExportInvoiceCommand = new RelayCommand(onExportInvoiceExecuted, onExportInvoiceCanExecute);
            
            this.searchInvoicesViewModel = searchInvoicesViewModel;
            this.searchInvoicesViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedInvoice")
                {
                    this.ExportInvoiceCommand.RaiseCanExecuteChanged();
                }
            };
        }

        #endregion

        #region Command Implementations

        private bool onExportInvoiceCanExecute()
        {
            return this.searchInvoicesViewModel.SelectedInvoice != null;
        }

        private void onExportInvoiceExecuted()
        {
            // download pdf using invoiceservice
        }

        #endregion
    }
}

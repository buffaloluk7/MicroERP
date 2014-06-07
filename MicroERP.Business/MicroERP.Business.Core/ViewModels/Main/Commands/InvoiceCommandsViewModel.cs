using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Main.Search;

namespace MicroERP.Business.Core.ViewModels.Main.Commands
{
    public class InvoiceCommandsViewModel
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private readonly INavigationService navigationService;
        private readonly SearchInvoicesViewModel searchInvoicesViewModel;

        #endregion

        #region Properties

        public RelayCommand CreateInvoiceCommand { get; private set; }

        public RelayCommand ExportInvoiceCommand { get; private set; }

        #endregion

        #region Constructor

        public InvoiceCommandsViewModel(IInvoiceService invoiceService, INavigationService navigationService,
            SearchInvoicesViewModel searchInvoicesViewModel)
        {
            this.invoiceService = invoiceService;
            this.navigationService = navigationService;

            this.CreateInvoiceCommand = new RelayCommand(onCreateInvoiceExecuted);
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

        private async void onCreateInvoiceExecuted()
        {
            if (this.navigationService is IWindowNavigationService)
            {
                await
                    (this.navigationService as IWindowNavigationService).Navigate<InvoiceWindowViewModel>(
                        showDialog: true);
            }
            else
            {
                await this.navigationService.Navigate<InvoiceWindowViewModel>();
            }
        }

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
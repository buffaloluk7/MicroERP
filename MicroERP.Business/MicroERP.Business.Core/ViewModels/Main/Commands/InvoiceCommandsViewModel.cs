using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Main.Search;
using System;
using MicroERP.Business.Domain.Exceptions;

namespace MicroERP.Business.Core.ViewModels.Main.Commands
{
    public class InvoiceCommandsViewModel
    {
        #region Fields

        private readonly INavigationService navigationService;
        private readonly INotificationService notificationService;
        private readonly IInvoiceService invoiceService;
        private readonly SearchInvoicesViewModel searchInvoicesViewModel;

        #endregion

        #region Properties

        public RelayCommand CreateInvoiceCommand { get; private set; }

        public RelayCommand ExportInvoiceCommand { get; private set; }

        #endregion

        #region Constructor

        public InvoiceCommandsViewModel(INavigationService navigationService, INotificationService notificationService, IInvoiceService invoiceService, SearchInvoicesViewModel searchInvoicesViewModel)
        {
            this.navigationService = navigationService;
            this.notificationService = notificationService;
            this.invoiceService = invoiceService;

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

        private async void onExportInvoiceExecuted()
        {
            try
            {
                await this.invoiceService.Export(this.searchInvoicesViewModel.SelectedInvoice.ID);
            }
            catch (InvoiceNotFoundException)
            {
                var x = this.notificationService.ShowAsync("Rechnung wurde nicht gefunden", "Rechnungen nicht gefunden");
            }
        }

        #endregion
    }
}
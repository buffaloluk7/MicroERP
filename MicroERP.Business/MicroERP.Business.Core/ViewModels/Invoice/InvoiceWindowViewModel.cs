using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Navigation;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.ViewModels.Invoice
{
    public class InvoiceWindowViewModel : ObservableObject, INavigationAware
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private readonly INavigationService navigationService;
        private readonly InvoiceModelViewModel invoiceModelViewModel;
        private InvoiceItemModelViewModel newInvoiceItem;

        private int customerID;

        #endregion

        #region Properties

        public InvoiceModelViewModel Invoice
        {
            get { return this.invoiceModelViewModel; }
        }

        public InvoiceItemModelViewModel NewInvoiceItem
        {
            get { return this.newInvoiceItem; }
            set { base.Set<InvoiceItemModelViewModel>(ref this.newInvoiceItem, value); }
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

        public RelayCommand AddInvoiceItemCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public InvoiceWindowViewModel(IInvoiceService invoiceService, INavigationService navigationService)
        {
            this.invoiceService = invoiceService;
            this.navigationService = navigationService;

            this.CancelCommand = new RelayCommand(onCancelExecuted);
            this.SaveInvoiceCommand = new RelayCommand(onSaveInvoiceExecuted, onSaveInvoiceCanExecute);
            this.AddInvoiceItemCommand = new RelayCommand(onAddInvoiceItemExecuted, onAddInvoiceItemCanExecute);

            this.invoiceModelViewModel = new InvoiceModelViewModel(new InvoiceModel());
            this.newInvoiceItem = new InvoiceItemModelViewModel(new InvoiceItemModel());
            this.newInvoiceItem.PropertyChanged += ((s, e) =>
            {
                this.AddInvoiceItemCommand.RaiseCanExecuteChanged();
            });
        }

        #endregion

        #region Command Implementations

        private bool onSaveInvoiceCanExecute()
        {
            return true;
        }

        private async void onSaveInvoiceExecuted()
        {
            var invoice = this.Invoice.Model;
            var invoiceItems = this.Invoice.InvoiceItems.Select(ii => ii.Model);

            invoice.InvoiceItems = new ObservableCollection<InvoiceItemModel>(invoiceItems);

            await this.invoiceService.Create(this.customerID, invoice);
        }

        private bool onAddInvoiceItemCanExecute()
        {
            return this.newInvoiceItem != null &&
                this.newInvoiceItem.Amount > 0 &&
                this.newInvoiceItem.Tax > 0 &&
                this.newInvoiceItem.UnitPrice > 0 &&
                !string.IsNullOrWhiteSpace(this.newInvoiceItem.Name);
        }

        private void onAddInvoiceItemExecuted()
        {
            this.Invoice.InvoiceItems.Add(this.newInvoiceItem);
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

        public void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            this.customerID = (int)argument;
        }

        #endregion
    }
}

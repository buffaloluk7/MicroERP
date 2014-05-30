using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Search
{
    public class SearchInvoicesViewModel : ObservableObject
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private IEnumerable<InvoiceModelViewModel> invoices;
        private InvoiceModelViewModel selectedInvoice;
        private string searchQuery;

        #endregion

        #region Properties

        public string SearchQuery
        {
            get { return this.searchQuery; }
            set
            {
                base.Set<string>(ref this.searchQuery, value);
                this.SearchInvoicesCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<InvoiceModelViewModel> Invoices
        {
            get { return this.invoices; }
            set { base.Set<IEnumerable<InvoiceModelViewModel>>(ref this.invoices, value); }
        }

        public InvoiceModelViewModel SelectedInvoice
        {
            get { return this.selectedInvoice; }
            set { base.Set<InvoiceModelViewModel>(ref this.selectedInvoice, value); }
        }

        #endregion

        #region Commands

        public RelayCommand SearchInvoicesCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public SearchInvoicesViewModel(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
            this.SearchInvoicesCommand = new RelayCommand(this.onSearchInvoicesExecuted, this.onSearchInvoicesCanExecute);

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.searchQuery = "20";
                this.onSearchInvoicesExecuted();
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private bool onSearchInvoicesCanExecute()
        {
            if (string.IsNullOrWhiteSpace(this.searchQuery))
            {
                this.Invoices = null;
                return false;
            }
            return true;
        }

        private async void onSearchInvoicesExecuted()
        {
            double? minPrice = double.Parse(this.searchQuery);
            var invoices = await this.invoiceService.Search(minPrice: minPrice);

            this.Invoices = invoices.Select(invoice => new InvoiceModelViewModel(invoice));
        }

        #endregion
    }
}

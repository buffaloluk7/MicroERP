using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Core.ViewModels.SearchBox;
using MicroERP.Business.Domain.DTO;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels.Main.Search
{
    public class SearchInvoicesViewModel : ObservableObject
    {
        #region Fields

        private readonly IInvoiceService invoiceService;
        private readonly InvoiceSearchArgs invoiceSearchArgs;
        private IEnumerable<InvoiceModelViewModel> invoices;
        private InvoiceModelViewModel selectedInvoice;

        #endregion

        #region Properties

        public DateTime? MinDate
        {
            get { return this.invoiceSearchArgs.MinDate; }
            set { this.invoiceSearchArgs.MinDate = value; }
        }

        public DateTime? MaxDate
        {
            get { return this.invoiceSearchArgs.MaxDate; }
            set { this.invoiceSearchArgs.MaxDate = value; }
        }

        public decimal? MinTotal
        {
            get { return this.invoiceSearchArgs.MinTotal; }
            set { this.invoiceSearchArgs.MinTotal = value; }
        }

        public decimal? MaxTotal
        {
            get { return this.invoiceSearchArgs.MaxTotal; }
            set { this.invoiceSearchArgs.MaxTotal = value; }
        }

        public CustomerSearchBoxViewModel CustomerSearchBoxViewModel
        {
            get;
            private set;
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

        public SearchInvoicesViewModel(IUnityContainer container, IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
            this.SearchInvoicesCommand = new RelayCommand(this.onSearchInvoicesExecuted, this.onSearchInvoicesCanExecute);

            this.invoiceSearchArgs = new InvoiceSearchArgs();
            this.invoiceSearchArgs.PropertyChanged += ((s, e) => this.SearchInvoicesCommand.RaiseCanExecuteChanged());

            this.CustomerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>();
            this.CustomerSearchBoxViewModel.PropertyChanged += CustomerSearchBoxViewModel_PropertyChanged;

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.MinTotal = 0.0m;
                this.onSearchInvoicesExecuted();
            }
            #endif
        }

        #endregion

        #region PropertyChanged

        private void CustomerSearchBoxViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedCustomer")
            {
                if (this.CustomerSearchBoxViewModel.SelectedCustomer == null)
                {
                    this.invoiceSearchArgs.CustomerID = null;
                }
                else
                {
                    this.invoiceSearchArgs.CustomerID = this.CustomerSearchBoxViewModel.SelectedCustomer.Model.ID;
                }
            }
        }

        #endregion

        #region Command Implementations

        private bool onSearchInvoicesCanExecute()
        {
            return !this.invoiceSearchArgs.IsEmpty();
        }

        private async void onSearchInvoicesExecuted()
        {
            var invoices = await this.invoiceService.Search(this.invoiceSearchArgs);
            this.Invoices = invoices.Select(invoice => new InvoiceModelViewModel(invoice));
        }

        #endregion
    }
}

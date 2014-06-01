using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Models;
using MicroERP.Business.Core.ViewModels.SearchBox;
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
        private IEnumerable<InvoiceModelViewModel> invoices;
        private InvoiceModelViewModel selectedInvoice;

        private DateTime? minDate;
        private DateTime? maxDate;
        private double? minTotal;
        private double? maxTotal;

        #endregion

        #region Properties

        public DateTime? MinDate
        {
            get { return this.minDate; }
            set
            {
                base.Set<DateTime?>(ref this.minDate, value);
                this.SearchInvoicesCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime? MaxDate
        {
            get { return this.maxDate; }
            set
            {
                base.Set<DateTime?>(ref this.maxDate, value);
                this.SearchInvoicesCommand.RaiseCanExecuteChanged();
            }
        }

        public double? MinTotal
        {
            get { return this.minTotal; }
            set
            {
                base.Set<double?>(ref this.minTotal, value);
                this.SearchInvoicesCommand.RaiseCanExecuteChanged();
            }
        }

        public double? MaxTotal
        {
            get { return this.maxTotal; }
            set
            {
                base.Set<double?>(ref this.maxTotal, value);
                this.SearchInvoicesCommand.RaiseCanExecuteChanged();
            }
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

            this.CustomerSearchBoxViewModel = container.Resolve<CustomerSearchBoxViewModel>();
            this.CustomerSearchBoxViewModel.PropertyChanged += ((s, e) => 
            {
                if (e.PropertyName == "SelectedCustomer")
                {
                    this.SearchInvoicesCommand.RaiseCanExecuteChanged();
                }
            });

            #if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.minTotal = 0.0;
                this.onSearchInvoicesExecuted();
            }
            #endif
        }

        #endregion

        #region Command Implementations

        private bool onSearchInvoicesCanExecute()
        {
            if (this.CustomerSearchBoxViewModel.SelectedCustomer == null)
            {
                this.Invoices = null;
                return false;
            }

            return true;
        }

        private async void onSearchInvoicesExecuted()
        {
            var customer = this.CustomerSearchBoxViewModel.SelectedCustomer == null ? null : this.CustomerSearchBoxViewModel.SelectedCustomer.Model;
            int? customerID = customer == null ? default(int?) : customer.ID;

            var invoices = await this.invoiceService.Search(customerID, this.minDate, this.maxDate, this.minTotal, this.maxTotal);
            this.Invoices = invoices.Select(invoice => new InvoiceModelViewModel(invoice));
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using MicroERP.Business.Common;
using MicroERP.Business.Models;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.ViewModels
{
    public abstract class CustomerVM : ObservableObject
    {
        #region Properties

        protected readonly Customer model;
        protected readonly Lazy<ObservableCollection<InvoiceVM>> invoices;

        public int ID
        {
            get { return this.model.ID; }
        }

        public string Address
        {
            get { return this.model.Address; }
        }

        public string BillingAddress
        {
            get { return this.model.BillingAddress; }
        }

        public string ShippingAddress
        {
            get { return this.model.ShippingAddress; }
        }

        public ObservableCollection<InvoiceVM> Invoices
        {
            get { return this.invoices.Value; }
        }

        #endregion

        #region Constructors

        public CustomerVM(Customer model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);

            Func<Invoice, InvoiceVM> invoiceViewModelCreator = (M) => new InvoiceVM(M);
            Func<ObservableCollection<InvoiceVM>> invoiceCollectionCreator = () => new ObservableViewModelCollection<InvoiceVM, Invoice>(this.model.Invoices, invoiceViewModelCreator);
            this.invoices = new Lazy<ObservableCollection<InvoiceVM>>(invoiceCollectionCreator);
        }

        #endregion
    }
}

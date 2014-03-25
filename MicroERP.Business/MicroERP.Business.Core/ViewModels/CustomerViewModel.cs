using GalaSoft.MvvmLight;
using MicroERP.Business.Core.Common;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.Core.ViewModels
{
    public abstract class CustomerViewModel : ObservableObject
    {
        #region Properties

        protected readonly CustomerModel model;
        protected readonly Lazy<ObservableCollection<InvoiceViewModel>> invoices;

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

        public ObservableCollection<InvoiceViewModel> Invoices
        {
            get { return this.invoices.Value; }
        }

        #endregion

        #region Constructors

        public CustomerViewModel(CustomerModel model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);

            Func<InvoiceModel, InvoiceViewModel> invoiceViewModelCreator = (M) => new InvoiceViewModel(M);
            Func<ObservableCollection<InvoiceViewModel>> invoiceCollectionCreator = () => new ObservableViewModelCollection<InvoiceViewModel, InvoiceModel>(this.model.Invoices, invoiceViewModelCreator);
            this.invoices = new Lazy<ObservableCollection<InvoiceViewModel>>(invoiceCollectionCreator);
        }

        #endregion
    }
}

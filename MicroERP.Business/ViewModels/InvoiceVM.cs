using GalaSoft.MvvmLight;
using MicroERP.Business.Common;
using MicroERP.Business.Models;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.ViewModels
{
    public class InvoiceVM : ObservableObject
    {
        #region Properties

        private readonly Invoice model;
        private readonly Lazy<ObservableCollection<InvoiceItemVM>> invoiceItems;

        public DateTime Date
        {
            get { return this.model.Date; }
        }

        public DateTime DueData
        {
            get { return this.model.DueDate; }
        }

        public int Number
        {
            get { return this.model.Number; }
        }

        public string Comment
        {
            get { return this.model.Comment; }
        }

        public string Message
        {
            get { return this.model.Message; }
        }

        public ObservableCollection<InvoiceItemVM> InvoiceItems
        {
            get { return this.invoiceItems.Value; }
        }

        public Customer Customer
        {
            get { return this.model.Customer; }
        }

        #endregion

        #region Constructors

        public InvoiceVM(Invoice model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);

            Func<InvoiceItem, InvoiceItemVM> invoiceItemViewModelCreator = (M) => new InvoiceItemVM(M);
            Func<ObservableCollection<InvoiceItemVM>> invoiceItemCollectionCreator = () => new ObservableViewModelCollection<InvoiceItemVM, InvoiceItem>(this.model.InvoiceItems, invoiceItemViewModelCreator);
            this.invoiceItems = new Lazy<ObservableCollection<InvoiceItemVM>>(invoiceItemCollectionCreator);
        }

        #endregion
    }
}

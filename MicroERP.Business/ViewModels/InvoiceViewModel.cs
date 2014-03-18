using GalaSoft.MvvmLight;
using MicroERP.Business.Common;
using MicroERP.Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.ViewModels
{
    public class InvoiceViewModel : ObservableObject
    {
        #region Properties

        private readonly InvoiceModel model;
        private readonly Lazy<ObservableCollection<InvoiceItemViewModel>> invoiceItems;

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

        public ObservableCollection<InvoiceItemViewModel> InvoiceItems
        {
            get { return this.invoiceItems.Value; }
        }

        public CustomerModel Customer
        {
            get { return this.model.Customer; }
        }

        #endregion

        #region Constructors

        public InvoiceViewModel(InvoiceModel model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);

            Func<InvoiceItemModel, InvoiceItemViewModel> invoiceItemViewModelCreator = (M) => new InvoiceItemViewModel(M);
            Func<ObservableCollection<InvoiceItemViewModel>> invoiceItemCollectionCreator = () => new ObservableViewModelCollection<InvoiceItemViewModel, InvoiceItemModel>(this.model.InvoiceItems, invoiceItemViewModelCreator);
            this.invoiceItems = new Lazy<ObservableCollection<InvoiceItemViewModel>>(invoiceItemCollectionCreator);
        }

        #endregion
    }
}

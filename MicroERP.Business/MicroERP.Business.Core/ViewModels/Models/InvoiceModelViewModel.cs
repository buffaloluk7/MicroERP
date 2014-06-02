using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class InvoiceModelViewModel : ObservableObject
    {
        #region Fields

        private readonly InvoiceModel invoice;
        private readonly ObservableCollection<InvoiceItemModelViewModel> invoiceItems;

        #endregion

        #region Properties

        public int ID
        {
            get { return this.invoice.ID; }
            set { this.invoice.ID = value; }
        }

        public string DisplayName
        {
            get { return new CustomerDisplayNameViewModel(this.invoice.Customer).DisplayName; }
        }

        public DateTime IssueDate
        {
            get { return this.invoice.IssueDate; }
            set { this.invoice.IssueDate = value; }
        }

        public DateTime DueDate
        {
            get { return this.invoice.DueDate; }
            set { this.invoice.DueDate = value; }
        }

        public string Comment
        {
            get { return this.invoice.Comment; }
            set { this.invoice.Comment = value; }
        }

        public string Message
        {
            get { return this.invoice.Message; }
            set { this.invoice.Message = value; }
        }

        public ObservableCollection<InvoiceItemModelViewModel> InvoiceItems
        {
            get { return this.invoiceItems; }
        }

        internal InvoiceModel Model
        {
            get { return this.invoice; }
        }

        #endregion

        #region Constructors

        public InvoiceModelViewModel(InvoiceModel invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException("invoice");
            }

            this.invoice = invoice;
            this.invoice.PropertyChanged += invoice_PropertyChanged;

            this.invoiceItems = new ObservableCollection<InvoiceItemModelViewModel>();
            foreach (var invoiceItem in this.invoice.InvoiceItems)
            {
                this.invoiceItems.Add(new InvoiceItemModelViewModel(invoiceItem));
            }

            var ii = new InvoiceItemModel(1, "Artikelbeschreibung xyz", 10, 12.0m, 0.2);
            this.invoiceItems.Add(new InvoiceItemModelViewModel(ii));
        }

        #endregion

        #region PropertyChanged

        private void invoice_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IssueDate":
                case "DueDate":
                case "Comment":
                case "Message":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        public decimal Total
        {
            get { return this.invoice.GrossTotal.HasValue ? this.invoice.GrossTotal.Value : default(decimal); }
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

        public InvoiceModelViewModel(InvoiceModel invoice = null)
        {
            this.invoice = invoice ?? new InvoiceModel();
            this.invoice.PropertyChanged += invoice_PropertyChanged;

            var invoiceItems = this.invoice.InvoiceItems.Select(ii => new InvoiceItemModelViewModel(ii));
            this.invoiceItems = new ObservableCollection<InvoiceItemModelViewModel>(invoiceItems);
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
                case "InvoiceItems":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

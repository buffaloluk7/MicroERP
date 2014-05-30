using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class InvoiceModelViewModel : ObservableObject
    {
        #region Fields

        private readonly InvoiceModel invoice;

        #endregion

        #region Properties

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

        public int Number
        {
            get { return this.invoice.Number; }
            set { this.invoice.Number = value; }
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
        }

        #endregion

        #region PropertyChanged

        private void invoice_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IssueDate":
                case "DueDate":
                case "Number":
                case "Comment":
                case "Message":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

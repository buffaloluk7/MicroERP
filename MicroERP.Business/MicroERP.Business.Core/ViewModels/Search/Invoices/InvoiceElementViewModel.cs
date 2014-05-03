using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Search.Invoices
{
    public class InvoiceElementViewModel : ObservableObject
    {
        #region Fields

        private readonly InvoiceModel invoice;

        #endregion

        #region Properties

        public DateTime? IssueDate
        {
            get { return this.invoice.IssueDate; }
        }

        public DateTime? DueDate
        {
            get { return this.invoice.DueDate; }
        }

        public int? Number
        {
            get { return this.invoice.Number; }
        }

        public string Comment
        {
            get { return this.invoice.Comment; }
        }

        public string Message
        {
            get { return this.invoice.Message; }
        }

        #endregion

        #region Constructors

        public InvoiceElementViewModel(InvoiceModel invoice)
        {
            this.invoice = invoice;
        }

        #endregion
    }
}

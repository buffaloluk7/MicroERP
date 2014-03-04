using System;
using MicroERP.Business.Common;
using System.Collections.Generic;

namespace MicroERP.Business.BO
{
    public class Invoice : ObservableObject
    {
        #region Properties

        private DateTime date;
        private DateTime dueDate;
        private int number;
        private string comment;
        private string message;
        private IEnumerable<InvoiceItem> invoiceItems;
        private Customer customer;

        public DateTime Date
        {
            get { return this.date; }
            set { base.Set<DateTime>(ref this.date, value); }
        }

        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { base.Set<DateTime>(ref this.dueDate, value); }
        }

        public int Number
        {
            get { return this.number; }
            set { base.Set<int>(ref this.number, value); }
        }

        public string Comment
        {
            get { return this.comment; }
            set { base.Set<string>(ref this.comment, value); }
        }

        public string Message
        {
            get { return this.message; }
            set { base.Set<string>(ref this.message, value); }
        }

        public IEnumerable<InvoiceItem> InvoiceItems
        {
            get { return this.invoiceItems; }
            set { base.Set<IEnumerable<InvoiceItem>>(ref this.invoiceItems, value); }
        }

        public Customer Customer
        {
            get { return this.customer; }
            set { base.Set<Customer>(ref this.customer, value); }
        }

        #endregion

        #region Constructors

        public Invoice(DateTime date, DateTime dueDate, int number, string comment, string message, IEnumerable<InvoiceItem> invoiceItems = null)
        {
            this.date = date;
            this.dueDate = dueDate;
            this.number = number;
            this.comment = comment;
            this.message = message;
            this.invoiceItems = invoiceItems;
        }

        #endregion
    }
}

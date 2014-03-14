using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Models
{
    [DataContract]
    public class Invoice : ObservableObject
    {
        #region Properties

        private DateTime date;
        private DateTime dueDate;
        private int number;
        private string comment;
        private string message;
        private readonly ObservableCollection<InvoiceItem> invoiceItems;
        private Customer customer;

        [DataMember]
        public DateTime Date
        {
            get { return this.date; }
            set { base.Set<DateTime>(ref this.date, value); }
        }

        [DataMember]
        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { base.Set<DateTime>(ref this.dueDate, value); }
        }

        [DataMember]
        public int Number
        {
            get { return this.number; }
            set { base.Set<int>(ref this.number, value); }
        }

        [DataMember]
        public string Comment
        {
            get { return this.comment; }
            set { base.Set<string>(ref this.comment, value); }
        }

        [DataMember]
        public string Message
        {
            get { return this.message; }
            set { base.Set<string>(ref this.message, value); }
        }

        [DataMember]
        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get { return this.invoiceItems; }
        }

        [DataMember]
        public Customer Customer
        {
            get { return this.customer; }
            set { base.Set<Customer>(ref this.customer, value); }
        }

        #endregion

        #region Constructors

        public Invoice(DateTime date, DateTime dueDate, int number, string comment, string message, Customer customer, ObservableCollection<InvoiceItem> invoiceItems = null)
        {
            this.date = date;
            this.dueDate = dueDate;
            this.number = number;
            this.comment = comment;
            this.message = message;
            this.customer = customer;
            this.invoiceItems = invoiceItems ?? new ObservableCollection<InvoiceItem>();
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var invoice = obj as Invoice;

            return base.Equals(obj)
                && obj is Invoice
                && invoice.date.Equals(this.date)
                && invoice.dueDate.Equals(this.dueDate)
                && invoice.number.Equals(this.number)
                && invoice.comment.Equals(this.comment)
                && invoice.message.Equals(this.message)
                && invoice.customer.Equals(this.customer)
                && invoice.invoiceItems.Equals(this.invoiceItems);
        }

        public override int GetHashCode()
        {
            int hash = 31 * this.date.GetHashCode();
                hash = 31 * hash + this.dueDate.GetHashCode();
                hash = 31 * hash + this.number.GetHashCode();
                hash = 31 * hash + this.comment.GetHashCode();
                hash = 31 * hash + this.message.GetHashCode();
                hash = 31 * hash + this.customer.GetHashCode();
                hash = 31 * hash + this.invoiceItems.GetHashCode();

            return hash;
        }

        #endregion
    }
}

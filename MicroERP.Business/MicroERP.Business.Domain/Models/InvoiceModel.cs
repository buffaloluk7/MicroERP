using Luvi.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class InvoiceModel : ObservableObject
    {
        #region Properties

        private DateTime issueDate;
        private DateTime dueDate;
        private int number;
        private string comment;
        private string message;
        private readonly ObservableCollection<InvoiceItemModel> invoiceItems;
        private CustomerModel customer;

        [DataMember]
        public DateTime Date
        {
            get { return this.issueDate; }
            set { base.Set<DateTime>(ref this.issueDate, value); }
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
        public ObservableCollection<InvoiceItemModel> InvoiceItems
        {
            get { return this.invoiceItems; }
        }

        [DataMember]
        public CustomerModel Customer
        {
            get { return this.customer; }
            set { base.Set<CustomerModel>(ref this.customer, value); }
        }

        #endregion

        #region Constructors

        public InvoiceModel(DateTime isseuDate, DateTime dueDate, int number, string comment, string message, CustomerModel customer, ObservableCollection<InvoiceItemModel> invoiceItems = null)
        {
            this.issueDate = isseuDate;
            this.dueDate = dueDate;
            this.number = number;
            this.comment = comment;
            this.message = message;
            this.customer = customer;
            this.invoiceItems = invoiceItems ?? new ObservableCollection<InvoiceItemModel>();
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var invoice = obj as InvoiceModel;

            return base.Equals(obj)
                && obj is InvoiceModel
                && invoice.issueDate.Equals(this.issueDate)
                && invoice.dueDate.Equals(this.dueDate)
                && invoice.number.Equals(this.number)
                && invoice.comment.Equals(this.comment)
                && invoice.message.Equals(this.message)
                && invoice.customer.Equals(this.customer)
                && invoice.invoiceItems.Equals(this.invoiceItems);
        }

        public override int GetHashCode()
        {
            int hash = 31 * this.issueDate.GetHashCode();
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

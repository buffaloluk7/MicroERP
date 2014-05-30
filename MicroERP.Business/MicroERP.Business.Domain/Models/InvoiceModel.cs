﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class InvoiceModel : ObservableObject, IEquatable<InvoiceModel>
    {
        #region Fields

        private int? id;
        private DateTime? issueDate;
        private DateTime? dueDate;
        private int? number;
        private string comment;
        private string message;
        private ObservableCollection<InvoiceItemModel> invoiceItems;
        private int? customerID;
        private CustomerModel customer;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int? ID
        {
            get { return this.id; }
            set { base.Set<int?>(ref this.id, value); }
        }

        [DataMember(Name = "issueDate")]
        public DateTime? IssueDate
        {
            get { return this.issueDate; }
            set { base.Set<DateTime?>(ref this.issueDate, value); }
        }

        [DataMember(Name = "dueDate")]
        public DateTime? DueDate
        {
            get { return this.dueDate; }
            set { base.Set<DateTime?>(ref this.dueDate, value); }
        }

        [DataMember(Name = "number")]
        public int? Number
        {
            get { return this.number; }
            set { base.Set<int?>(ref this.number, value); }
        }

        [DataMember(Name = "comment")]
        public string Comment
        {
            get { return this.comment; }
            set { base.Set<string>(ref this.comment, value); }
        }

        [DataMember(Name = "message")]
        public string Message
        {
            get { return this.message; }
            set { base.Set<string>(ref this.message, value); }
        }

        [DataMember(Name = "invoiceItems")]
        public ObservableCollection<InvoiceItemModel> InvoiceItems
        {
            get { return this.invoiceItems; }
            set { base.Set<ObservableCollection<InvoiceItemModel>>(ref this.invoiceItems, value); }
        }

        [IgnoreDataMember]
        public int? CustomerID
        {
            get { return this.customerID; }
            set { base.Set<int?>(ref this.customerID, value); }
        }

        [IgnoreDataMember]
        public CustomerModel Customer
        {
            get { return this.customer; }
            set
            {
                base.Set<CustomerModel>(ref this.customer, value);
                this.CustomerID = (value != null) ? value.ID : null;
            }
        }

        #endregion

        #region Constructors

        public InvoiceModel(int? id, DateTime? isseuDate, DateTime? dueDate, int? number, string comment, string message, CustomerModel customer, IEnumerable<InvoiceItemModel> invoiceItems = null)
        {
            this.id = id;
            this.issueDate = isseuDate;
            this.dueDate = dueDate;
            this.number = number;
            this.comment = comment;
            this.message = message;
            this.Customer = customer;
            this.invoiceItems = invoiceItems != null ? new ObservableCollection<InvoiceItemModel>(invoiceItems) : null;
        }

        #endregion

        #region IEquatable

        public override bool Equals(object obj)
        {
            return this.Equals(obj as InvoiceModel);
        }

        public override int GetHashCode()
        {
            if (!this.id.HasValue)
            {
                throw new InvalidOperationException("Hashcode cannot be calculated from an invoice without an ID");
            }

            return this.id.GetHashCode();
        }

        public bool Equals(InvoiceModel other)
        {
            return other != null &&
                other.id == this.id &&
                other.dueDate == this.dueDate &&
                other.number == this.number &&
                other.comment == this.comment &&
                other.message == this.message &&
                other.customer == this.customer &&
                object.Equals(this.invoiceItems, other.invoiceItems);
        }

        #endregion
    }
}

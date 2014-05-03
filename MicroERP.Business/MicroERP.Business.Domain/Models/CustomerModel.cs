using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public abstract class CustomerModel : ObservableObject, IEquatable<CustomerModel>
    {
        #region Fields

        private int? id;
        private string address;
        private string billingAddress;
        private string shippingAddress;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int? ID
        {
            get { return this.id; }
            set { base.Set<int?>(ref this.id, value); }
        }

        [DataMember(Name = "address")]
        public string Address
        {
            get { return this.address; }
            set { base.Set<string>(ref this.address, value); }
        }

        [DataMember(Name = "billingAddress")]
        public string BillingAddress
        {
            get { return this.billingAddress; }
            set { base.Set<string>(ref this.billingAddress, value); }
        }

        [DataMember(Name = "shippingAddress")]
        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { base.Set<string>(ref this.shippingAddress, value); }
        }

        [IgnoreDataMember]
        public ObservableCollection<InvoiceModel> Invoices
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CustomerModel() { }

        public CustomerModel(int? id, string address, string billingAddress, string shippingAddress)
        {
            this.id = id;
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
        }

        #endregion

        #region IEquatable

        public override int GetHashCode()
        {
            if (!this.id.HasValue)
            {
                throw new InvalidOperationException("Hashcode cannot be calculated from a customer without an ID");
            }

            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CustomerModel);
        }

        public virtual bool Equals(CustomerModel other)
        {
            return other.id == this.id
                && other.address == this.address
                && other.billingAddress == this.billingAddress
                && other.shippingAddress == this.shippingAddress
                && other.Invoices == this.Invoices;
        }

        #endregion
    }
}

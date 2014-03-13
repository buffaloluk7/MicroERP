using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Models
{
    [DataContract]
    public abstract class Customer : ObservableObject
    {
        #region Properties

        private int id;
        private string address;
        private string billingAddress;
        private string shippingAddress;
        private readonly ObservableCollection<Invoice> invoices;

        [DataMember]
        public int ID
        {
            get { return this.id; }
            set { base.Set<int>(ref this.id, value); }
        }

        [DataMember]
        public string Address
        {
            get { return this.address; }
            set { base.Set<string>(ref this.address, value); }
        }

        [DataMember]
        public string BillingAddress
        {
            get { return this.billingAddress; }
            set { base.Set<string>(ref this.billingAddress, value); }
        }

        [DataMember]
        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { base.Set<string>(ref this.shippingAddress, value); }
        }

        [DataMember]
        public ObservableCollection<Invoice> Invoices
        {
            get { return this.invoices; }
        }

        #endregion

        #region Constructors

        public Customer(int id, string address, string billingAddress, string shippingAddress, IEnumerable<Invoice> invoices = null)
        {
            this.id = id;
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
            this.invoices = (invoices != null) ? new ObservableCollection<Invoice>(invoices) : new ObservableCollection<Invoice>();
        }

        #endregion

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;

            return obj is Customer
                && customer.address.Equals(this.address)
                && customer.billingAddress.Equals(this.billingAddress)
                && customer.shippingAddress.Equals(this.shippingAddress)
                && customer.invoices.Equals(this.invoices);
        }

        public override int GetHashCode()
        {
            int hash = 31 * this.address.GetHashCode();
                hash = 31 * hash + this.billingAddress.GetHashCode();
                hash = 31 * hash + this.shippingAddress.GetHashCode();
                hash = 31 * hash + this.invoices.GetHashCode();

            return hash;
        }
    }
}

using Luvi.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public abstract class CustomerModel : ObservableObject
    {
        #region Properties

        private int id;
        private string address;
        private string billingAddress;
        private string shippingAddress;
        private readonly ObservableCollection<InvoiceModel> invoices;

        [DataMember(Name="id")]
        public int ID
        {
            get { return this.id; }
            set { base.Set<int>(ref this.id, value); }
        }

        [DataMember(Name="address")]
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

        [DataMember(Name = "invoices")]
        public ObservableCollection<InvoiceModel> Invoices
        {
            get { return this.invoices; }
        }

        #endregion

        #region Constructors

        public CustomerModel(int id, string address, string billingAddress, string shippingAddress, IEnumerable<InvoiceModel> invoices = null)
        {
            this.id = id;
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
            this.invoices = (invoices != null) ? new ObservableCollection<InvoiceModel>(invoices) : new ObservableCollection<InvoiceModel>();
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var customer = obj as CustomerModel;

            return obj is CustomerModel
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

        #endregion
    }
}

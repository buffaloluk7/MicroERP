using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public abstract class CustomerModel : ObservableObject
    {
        #region Fields

        private int id;
        private string address;
        private string billingAddress;
        private string shippingAddress;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int ID
        {
            get { return this.id; }
            set { base.Set<int>(ref this.id, value); }
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

        public CustomerModel(int id, string address, string billingAddress, string shippingAddress)
        {
            this.id = id;
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var customer = obj as CustomerModel;

            return obj is CustomerModel
                && customer.address.Equals(this.address)
                && customer.billingAddress.Equals(this.billingAddress)
                && customer.shippingAddress.Equals(this.shippingAddress);
        }

        public override int GetHashCode()
        {
            int hash = 31 * this.address.GetHashCode();
                hash = 31 * hash + this.billingAddress.GetHashCode();
                hash = 31 * hash + this.shippingAddress.GetHashCode();

            return hash;
        }

        #endregion
    }
}

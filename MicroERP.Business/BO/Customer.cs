using MicroERP.Business.Common;

namespace MicroERP.Business.BO
{
    public abstract class Customer : ObservableObject
    {
        #region Properties

        private string address;
        private string billingAddress;
        private string shippingAddress;

        public string Address
        {
            get { return this.address; }
            set { base.Set<string>(ref this.address, value); }
        }

        public string BillingAddress
        {
            get { return this.billingAddress; }
            set { base.Set<string>(ref this.billingAddress, value); }
        }

        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { base.Set<string>(ref this.shippingAddress, value); }
        }

        #endregion

        #region Constructors

        public Customer(string address, string billingAddress, string shippingAddress)
        {
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
        }

        #endregion
    }
}

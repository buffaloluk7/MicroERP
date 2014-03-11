using MicroERP.Business.Common;
using System.Collections.ObjectModel;

namespace MicroERP.Business.Models
{
    public abstract class Customer : ObservableObject
    {
        #region Properties

        private string address;
        private string billingAddress;
        private string shippingAddress;
        private readonly ObservableCollection<Invoice> invoices;

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

        public ObservableCollection<Invoice> Invoices
        {
            get { return this.invoices; }
        }

        #endregion

        #region Constructors

        public Customer(string address, string billingAddress, string shippingAddress, ObservableCollection<Invoice> invoices = null)
        {
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
            this.invoices = invoices ?? new ObservableCollection<Invoice>();
        }

        #endregion
    }
}

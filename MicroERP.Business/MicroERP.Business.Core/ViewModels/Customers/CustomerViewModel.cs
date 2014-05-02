using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.ViewModels.Customers
{
    public class CustomerViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerModel customer;

        #endregion

        #region Properties

        public string Address
        {
            get { return this.customer.Address; }
            set { this.customer.Address = value; }
        }

        public string BillingAddress
        {
            get { return this.customer.BillingAddress; }
            set { this.customer.BillingAddress = value; }
        }

        public string ShippingAddress
        {
            get { return this.customer.ShippingAddress; }
            set { this.customer.ShippingAddress = value; }
        }

        #endregion

        #region Constructors

        public CustomerViewModel(CustomerModel customer)
        {
            this.customer = customer;
            this.customer.PropertyChanged += customer_PropertyChanged;
        }

        #endregion

        #region PropertyChanged

        private void customer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Address":
                case "BillingAddress":
                case "ShippingAddress":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

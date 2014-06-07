using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class CustomerDisplayNameViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerModel customer;

        #endregion

        #region Properties

        public string DisplayName
        {
            get
            {
                var person = this.customer as PersonModel;
                if (person != null)
                {
                    return string.Format("{0} {1}", person.FirstName, person.LastName);
                }

                var company = this.customer as CompanyModel;
                if (company != null)
                {
                    return string.Format("{0} ({1})", company.Name, company.UID);
                }

                throw new InvalidOperationException("Invalid customer type");
            }
        }

        public CustomerType Type
        {
            get
            {
                if (this.customer is PersonModel)
                {
                    return CustomerType.Person;
                }
                else if (this.customer is CompanyModel)
                {
                    return CustomerType.Company;
                }

                throw new InvalidOperationException("Invalid customer type");
            }
        }

        internal CustomerModel Model
        {
            get { return this.customer; }
        }

        #endregion

        #region Constructors

        public CustomerDisplayNameViewModel(CustomerModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            this.customer = customer;
            customer.PropertyChanged += customer_PropertyChanged;
        }

        #endregion

        #region Property Changed

        private void customer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FirstName":
                case "LastName":
                case "Name":
                    base.RaisePropertyChanged(() => this.DisplayName);
                    break;
            }
        }

        #endregion
    }
}

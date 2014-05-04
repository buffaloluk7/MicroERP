using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Search.Customers
{
    public class CustomerElementViewModel : ObservableObject
    {
        #region Fields

        internal readonly CustomerModel customer;

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
                    return company.Name;
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

        #endregion

        #region Constructors

        public CustomerElementViewModel(CustomerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model cannot be null");
            }

            this.customer = model;
            model.PropertyChanged += model_PropertyChanged;
        }

        #endregion

        #region Property Changed

        private void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels
{
    public class FullNameViewModel : ObservableObject
    {
        #region Fields

        internal readonly CustomerModel model;

        #endregion

        #region Properties

        public string FullName
        {
            get
            {
                var person = this.model as PersonModel;
                if (person != null)
                {
                    return string.Format("{0} {1}", person.FirstName, person.LastName);
                }

                var company = this.model as CompanyModel;
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
                if (this.model is PersonModel)
                {
                    return CustomerType.Person;
                }
                else if (this.model is CompanyModel)
                {
                    return CustomerType.Company;
                }

                throw new InvalidOperationException("Invalid customer type");
            }
        }

        #endregion

        #region Constructors

        public FullNameViewModel(CustomerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model cannot be null");
            }

            this.model = model;
            model.PropertyChanged += model_PropertyChanged;
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FirstName":
                case "LastName":
                case "Name":
                    base.RaisePropertyChanged(() => this.FullName);
                    break;
            }
        }

        #endregion
    }
}

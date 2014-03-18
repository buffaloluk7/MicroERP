using GalaSoft.MvvmLight;
using MicroERP.Domain.Enums;
using MicroERP.Domain.Models;
using System;

namespace MicroERP.Business.ViewModels
{
    public class FullNameViewModel : ObservableObject
    {
        #region Properties

        internal readonly CustomerModel model;

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

                throw new Exception("Invalid customer type");
            }
        }

        public string FullName
        {
            get
            {
                if (this.model is PersonModel)
                {
                    return string.Format("{0} {1}", (this.model as PersonModel).FirstName, (this.model as PersonModel).LastName);
                }
                else if (this.model is CompanyModel)
                {
                    return (this.model as CompanyModel).Name;
                }
                else
                {
                    return this.model.ToString();
                }
            }
        }

        #endregion

        #region Constructors

        public FullNameViewModel(CustomerModel model)
        {
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

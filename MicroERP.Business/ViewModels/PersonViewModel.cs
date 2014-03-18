using GalaSoft.MvvmLight;
using MicroERP.Domain.Models;
using System;

namespace MicroERP.Business.ViewModels
{
    public class PersonViewModel : ObservableObject
    {
        #region Properties

        private readonly PersonModel model;

        public string Title
        {
            get { return this.model.Title; }
        }

        public string FirstName
        {
            get { return this.model.FirstName; }
        }

        public string LastName
        {
            get { return this.model.LastName; }
        }

        public string Suffix
        {
            get { return this.model.Suffix; }
        }

        public DateTime BirthDate
        {
            get { return this.model.BirthDate; }
        }

        public CompanyViewModel Company
        {
            get;
            private set;
        }


        #endregion

        #region Constructors

        public PersonViewModel(PersonModel model)
        {
            this.model = model;
            model.PropertyChanged += model_PropertyChanged;
            this.updateCompany();
        }

        #endregion

        private void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Company")
            {
                this.updateCompany();
            }

            base.RaisePropertyChanged(e.PropertyName);
        }

        private void updateCompany()
        {
            this.Company = model.Company != null ? new CompanyViewModel(model.Company) : null;
        }
    }
}

using GalaSoft.MvvmLight;
using MicroERP.Business.Models;
using System;

namespace MicroERP.Business.ViewModels
{
    public class PersonVM : ObservableObject
    {
        #region Properties

        private readonly Person model;

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

        public CompanyVM Company
        {
            get;
            private set;
        }


        #endregion

        #region Constructors

        public PersonVM(Person model)
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
            this.Company = model.Company != null ? new CompanyVM(model.Company) : null;
        }
    }
}

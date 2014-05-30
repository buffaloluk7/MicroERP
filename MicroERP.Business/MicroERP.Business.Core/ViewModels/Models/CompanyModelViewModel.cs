using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class CompanyModelViewModel : ObservableObject
    {
        #region Fields

        private readonly CompanyModel company;

        #endregion

        #region Properties

        public string UID
        {
            get { return this.company.UID; }
            set { this.company.UID = value; }
        }

        public string Name
        {
            get { return this.company.Name; }
            set { this.company.Name = value; }
        }

        #endregion

        #region Constructors

        public CompanyModelViewModel(CompanyModel company)
        {
            if (company == null)
            {
                throw new ArgumentNullException("company");
            }

            this.company = company;
            this.company.PropertyChanged += company_PropertyChanged;
        }

        #endregion

        #region PropertyChanged

        private void company_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "UID":
                case "Name":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

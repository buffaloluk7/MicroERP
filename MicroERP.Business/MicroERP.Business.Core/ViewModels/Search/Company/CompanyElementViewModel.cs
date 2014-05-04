using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Search.Company
{
    public class CompanyElementViewModel : ObservableObject
    {
        #region Fields

        private readonly CompanyModel company;

        #endregion

        #region Properties

        public string DisplayName
        {
            get { return this.company != null ? this.company.Name : ""; }
        }

        internal CompanyModel Model
        {
            get { return this.company; }
        }

        #endregion

        #region Constructors

        public CompanyElementViewModel(CompanyModel company)
        {
            if (company == null)
            {
                throw new ArgumentNullException("Company cannot be null");
            }

            this.company = company;
            company.PropertyChanged += model_PropertyChanged;
        }

        #endregion

        #region Property Changed

        private void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    base.RaisePropertyChanged(() => this.DisplayName);
                    break;
            }
        }

        #endregion
    }
}

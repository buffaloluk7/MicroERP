using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Search.Company
{
    public class CompanyElementViewModel : ObservableObject
    {
        #region Fields

        internal readonly CompanyModel company;

        #endregion

        #region Properties

        public string Name
        {
            get { return this.company != null ? this.company.Name : ""; }
        }

        #endregion

        #region Constructors

        public CompanyElementViewModel(CompanyModel company)
        {
            this.company = company;
            company.PropertyChanged += model_PropertyChanged;
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    base.RaisePropertyChanged(() => this.Name);
                    break;
            }
        }

        #endregion
    }
}

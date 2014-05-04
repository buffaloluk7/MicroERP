using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.ViewModels.Customers
{
    public class CompanyViewModel : ObservableObject
    {
        #region Fields

        internal readonly CompanyModel company;

        #endregion

        #region Properties

        public int? ID
        {
            get { return this.company.ID; }
            set { this.company.ID = value; }
        }

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

        public CompanyViewModel(CompanyModel company)
        {
            this.company = company;
            this.company.PropertyChanged += company_PropertyChanged;
        }

        #endregion

        #region PropertyChanged

        private void company_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ID":
                case "UID":
                case "Name":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}

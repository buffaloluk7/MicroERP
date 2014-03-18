using GalaSoft.MvvmLight;
using MicroERP.Domain.Models;

namespace MicroERP.Business.ViewModels
{
    public class CompanyViewModel : ObservableObject
    {
        #region Properties

        private readonly CompanyModel model;

        public string Name
        {
            get { return this.model.Name; }
        }

        public string Uid
        {
            get { return this.model.UID; }
        }

        #endregion

        #region Constructors

        public CompanyViewModel(CompanyModel model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);
        }

        #endregion
    }
}

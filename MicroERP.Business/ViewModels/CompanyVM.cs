using GalaSoft.MvvmLight;
using MicroERP.Business.Models;

namespace MicroERP.Business.ViewModels
{
    public class CompanyVM : ObservableObject
    {
        #region Properties

        private readonly Company model;

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

        public CompanyVM(Company model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);
        }

        #endregion
    }
}

using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Core.ViewModels
{
    public class InvoiceItemViewModel : ObservableObject
    {
        #region Properties

        private readonly InvoiceItemModel model;

        public int Amount
        {
            get { return this.model.Amount; }
        }

        public double UnitPrice
        {
            get { return this.model.UnitPrice; }
        }

        public double Tax
        {
            get { return this.model.Tax; }
        }

        public double Net
        {
            get { return this.model.Amount * this.model.UnitPrice; }
        }

        public double Gross
        {
            get { return (1 + this.model.Tax / 100) * this.Net; }
        }

        #endregion

        #region Constructors

        public InvoiceItemViewModel(InvoiceItemModel model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);
        }

        #endregion
    }
}

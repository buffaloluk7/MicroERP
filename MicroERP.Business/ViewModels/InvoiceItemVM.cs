using GalaSoft.MvvmLight;
using MicroERP.Business.Models;

namespace MicroERP.Business.ViewModels
{
    public class InvoiceItemVM : ObservableObject
    {
        #region Properties

        private readonly InvoiceItem model;

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

        public InvoiceItemVM(InvoiceItem model)
        {
            this.model = model;
            model.PropertyChanged += (s, e) => base.RaisePropertyChanged(e.PropertyName);
        }

        #endregion
    }
}

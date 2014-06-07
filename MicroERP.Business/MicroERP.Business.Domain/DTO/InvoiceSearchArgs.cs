using System;
using GalaSoft.MvvmLight;

namespace MicroERP.Business.Domain.DTO
{
    public class InvoiceSearchArgs : ObservableObject
    {
        #region Fields

        private int? customerID;
        private DateTime? minDate;
        private DateTime? maxDate;
        private decimal? minTotal;
        private decimal? maxTotal;

        #endregion

        #region Properties

        public int? CustomerID
        {
            get { return this.customerID; }
            set { base.Set(ref this.customerID, value); }
        }

        public DateTime? MinDate
        {
            get { return this.minDate; }
            set { base.Set(ref this.minDate, value); }
        }

        public DateTime? MaxDate
        {
            get { return this.maxDate; }
            set { base.Set(ref this.maxDate, value); }
        }

        public decimal? MinTotal
        {
            get { return this.minTotal; }
            set { base.Set(ref this.minTotal, value); }
        }

        public decimal? MaxTotal
        {
            get { return this.maxTotal; }
            set { base.Set(ref this.maxTotal, value); }
        }

        public bool IsEmpty()
        {
            return !this.customerID.HasValue &&
                   !this.minDate.HasValue &&
                   !this.maxDate.HasValue &&
                   !this.minTotal.HasValue &&
                   !this.maxTotal.HasValue;
        }

        #endregion
    }
}
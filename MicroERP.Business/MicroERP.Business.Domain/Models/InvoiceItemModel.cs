using GalaSoft.MvvmLight;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class InvoiceItemModel : ObservableObject
    {
        #region Fields

        private int amount;
        private double unitPrice;
        private double tax;

        #endregion

        #region Properties

        [DataMember(Name = "amount")]
        public int Amount
        {
            get { return this.amount; }
            set { base.Set<int>(ref this.amount, value); }
        }

        [DataMember(Name = "unitPrice")]
        public double UnitPrice
        {
            get { return this.unitPrice; }
            set { base.Set<double>(ref this.unitPrice, value); }
        }

        /// <summary>
        /// For example: 12.5
        /// </summary>
        [DataMember(Name = "tax")]
        public double Tax
        {
            get { return this.tax; }
            set { base.Set<double>(ref this.tax, value); }
        }

        #endregion

        #region Constructors

        public InvoiceItemModel(int amount, double unitPrice, double tax)
        {
            this.amount = amount;
            this.unitPrice = unitPrice;
            this.tax = tax;
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var item = obj as InvoiceItemModel;

            return base.Equals(obj)
                && item is InvoiceItemModel
                && item.amount.Equals(this.amount)
                && item.tax.Equals(this.tax)
                && item.unitPrice.Equals(this.unitPrice);
        }

        public override int GetHashCode()
        {
            int hash = 31 * this.amount.GetHashCode();
                hash = 31 * hash + this.tax.GetHashCode();
                hash = 31 * hash + this.unitPrice.GetHashCode();

            return hash;
        }

        #endregion
    }
}

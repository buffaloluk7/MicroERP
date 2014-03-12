using MicroERP.Business.Common;
using System.Runtime.Serialization;

namespace MicroERP.Business.Models
{
    [DataContract]
    public class InvoiceItem : ObservableObject
    {
        #region Properties

        private int amount;
        private double unitPrice;
        private double tax;
        private double? net;
        private double? gross;

        [DataMember]
        public int Amount
        {
            get { return this.amount; }
            set { base.Set<int>(ref this.amount, value); }
        }

        [DataMember]
        public double UnitPrice
        {
            get { return this.unitPrice; }
            set { base.Set<double>(ref this.unitPrice, value); }
        }

        [DataMember]
        public double Tax
        {
            get { return this.tax; }
            set { base.Set<double>(ref this.tax, value); }
        }

        [DataMember]
        public double? Net
        {
            get { return this.net; }
            set { base.Set<double?>(ref this.net, value); }
        }

        [DataMember]
        public double? Gross
        {
            get { return this.gross; }
            set { base.Set<double?>(ref this.gross, value); }
        }

        #endregion

        #region Constructors

        public InvoiceItem(int amount, double unitPrice, double tax, double? net = null, double? gross = null)
        {
            this.amount = amount;
            this.unitPrice = unitPrice;
            this.tax = tax;
            this.net = net;
            this.gross = gross;
        }

        #endregion

        public override bool Equals(object obj)
        {
            var item = obj as InvoiceItem;

            return base.Equals(obj)
                && item is InvoiceItem
                && item.amount.Equals(this.amount)
                && item.tax.Equals(this.tax)
                && item.unitPrice.Equals(this.unitPrice);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                + this.amount.GetHashCode()
                + this.tax.GetHashCode()
                + this.unitPrice.GetHashCode();
        }
    }
}

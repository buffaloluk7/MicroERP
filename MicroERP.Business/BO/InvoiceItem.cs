using MicroERP.Business.Common;

namespace MicroERP.Business.BO
{
    public class InvoiceItem : ObservableObject
    {
        #region Properties

        private int amount;
        private double unitPrice;
        private double tax;
        private double? net;
        private double? gross;

        public int Amount
        {
            get { return this.amount; }
            set { base.Set<int>(ref this.amount, value); }
        }

        public double UnitPrice
        {
            get { return this.unitPrice; }
            set { base.Set<double>(ref this.unitPrice, value); }
        }

        public double Tax
        {
            get { return this.tax; }
            set { base.Set<double>(ref this.tax, value); }
        }

        public double? Net
        {
            get { return this.net; }
            set { base.Set<double?>(ref this.net, value); }
        }

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
    }
}

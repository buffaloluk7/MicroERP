using GalaSoft.MvvmLight;
using System;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class InvoiceItemModel : ObservableObject, IEquatable<InvoiceItemModel>
    {
        #region Fields

        private int? id;
        private int? amount;
        private double? unitPrice;
        private double? tax;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int? ID
        {
            get { return this.id; }
            set { base.Set<int?>(ref this.id, value); }
        }

        [DataMember(Name = "amount")]
        public int? Amount
        {
            get { return this.amount; }
            set { base.Set<int?>(ref this.amount, value); }
        }

        [DataMember(Name = "unitPrice")]
        public double? UnitPrice
        {
            get { return this.unitPrice; }
            set { base.Set<double?>(ref this.unitPrice, value); }
        }

        /// <summary>
        /// For example: 12.5
        /// </summary>
        [DataMember(Name = "tax")]
        public double? Tax
        {
            get { return this.tax; }
            set { base.Set<double?>(ref this.tax, value); }
        }

        [IgnoreDataMember]
        public int InvoiceID
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public InvoiceModel Invoice
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public InvoiceItemModel(int? id, int? amount, double? unitPrice, double? tax)
        {
            this.id = id;
            this.amount = amount;
            this.unitPrice = unitPrice;
            this.tax = tax;
        }

        #endregion

        #region IEquatable

        public override bool Equals(object obj)
        {
            return this.Equals(obj as InvoiceItemModel);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public bool Equals(InvoiceItemModel other)
        {
            return other != null &&
                other.id == this.id &&
                other.amount == this.amount &&
                other.tax == this.tax &&
                other.unitPrice == this.unitPrice;
        }

        #endregion
    }
}

using System;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class InvoiceItemModel : ObservableObject, IEquatable<InvoiceItemModel>
    {
        #region Fields

        private int id;
        private string name;
        private int amount;
        private decimal unitPrice;
        private decimal tax;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int ID
        {
            get { return this.id; }
            set { base.Set(ref this.id, value); }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return this.name; }
            set { base.Set(ref this.name, value); }
        }

        [DataMember(Name = "amount")]
        public int Amount
        {
            get { return this.amount; }
            set { base.Set(ref this.amount, value); }
        }

        /// <summary>
        ///     Net price
        /// </summary>
        [DataMember(Name = "unitPrice")]
        public decimal UnitPrice
        {
            get { return this.unitPrice; }
            set { base.Set(ref this.unitPrice, value); }
        }

        /// <summary>
        ///     For example: 0.19
        /// </summary>
        [DataMember(Name = "tax")]
        public decimal Tax
        {
            get { return this.tax; }
            set { base.Set(ref this.tax, value); }
        }

        [IgnoreDataMember]
        public InvoiceModel Invoice { get; set; }

        #endregion

        #region Constructors

        public InvoiceItemModel()
        {
        }

        public InvoiceItemModel(int id, string name, int amount, decimal unitPrice, decimal tax)
        {
            this.id = id;
            this.name = name;
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
                   other.name == this.name &&
                   other.amount == this.amount &&
                   other.tax == this.tax &&
                   other.unitPrice == this.unitPrice;
        }

        #endregion
    }
}
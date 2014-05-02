using System;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class CompanyModel : CustomerModel, IEquatable<CompanyModel>
    {
        #region Fields

        private string name;
        private string uid;

        #endregion

        #region Properties

        [DataMember(Name = "name")]
        public string Name
        {
            get { return this.name; }
            set { base.Set<string>(ref this.name, value); }
        }

        [DataMember(Name = "uid")]
        public string UID
        {
            get { return this.uid; }
            set { base.Set<string>(ref this.uid, value); }
        }

        #endregion

        #region Constructors

        public CompanyModel() { }

        public CompanyModel(int? id, string address, string billingAddress, string shippingAddress, string name, string uid) : base(id, address, billingAddress, shippingAddress)
        {
            this.name = name;
            this.uid = uid;
        }

        #endregion

        #region IEquatable

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CompanyModel);
        }

        public bool Equals(CompanyModel other)
        {
            return other != null &&
                base.Equals(other) &&
                other.name == this.name &&
                other.uid == this.uid;
        }

        #endregion
    }
}

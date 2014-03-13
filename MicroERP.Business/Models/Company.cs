using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroERP.Business.Models
{
    [DataContract]
    public class Company : Customer
    {
        #region Properties

        private string name;
        private string uid;

        [DataMember]
        public string Name
        {
            get { return this.name; }
            set { base.Set<string>(ref this.name, value); }
        }

        [DataMember]
        public string Uid
        {
            get { return this.uid; }
            set { base.Set<string>(ref this.uid, value); }
        }

        #endregion

        #region Constructors

        public Company(int id, string address, string billingAddress, string shippingAddress, string name, string uid, IEnumerable<Invoice> invoices = null) : base(id, address, billingAddress, shippingAddress, invoices)
        {
            this.name = name;
            this.uid = uid;
        }

        #endregion

        public override bool Equals(object obj)
        {
            var company = obj as Company;

            return base.Equals(obj)
                && obj is Company
                && company.name.Equals(this.name)
                && company.uid.Equals(this.uid);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                + (this.name + this.uid).GetHashCode();
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class CompanyModel : CustomerModel
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
        public string UID
        {
            get { return this.uid; }
            set { base.Set<string>(ref this.uid, value); }
        }

        #endregion

        #region Constructors

        public CompanyModel(int id, string address, string billingAddress, string shippingAddress, string name, string uid, IEnumerable<InvoiceModel> invoices = null) : base(id, address, billingAddress, shippingAddress, invoices)
        {
            this.name = name;
            this.uid = uid;
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var company = obj as CompanyModel;

            return base.Equals(obj)
                && obj is CompanyModel
                && company.name.Equals(this.name)
                && company.uid.Equals(this.uid);
        }

        public override int GetHashCode()
        {
            int hash = 31 * base.GetHashCode();
                hash = 31 * hash + this.name.GetHashCode();
                hash = 31 * hash + this.uid.GetHashCode();

            return hash;
        }

        #endregion
    }
}

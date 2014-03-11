namespace MicroERP.Business.Models
{
    public class Company : Customer
    {
        #region Properties

        private string name;
        private int uid;

        public string Name
        {
            get { return this.name; }
            set { base.Set<string>(ref this.name, value); }
        }

        public int Uid
        {
            get { return this.uid; }
            set { base.Set<int>(ref this.uid, value); }
        }

        #endregion

        #region Constructors

        public Company(string address, string billingAddress, string shippingAddress, string name, int uid) : base(address, billingAddress, shippingAddress)
        {
            this.name = name;
            this.uid = uid;
        }

        #endregion
    }
}

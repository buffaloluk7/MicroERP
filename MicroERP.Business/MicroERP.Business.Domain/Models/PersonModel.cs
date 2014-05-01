using System;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class PersonModel : CustomerModel
    {
        #region Fields

        private string title;
        private string firstName;
        private string lastName;
        private string suffix;
        private DateTime birthDate;
        private CompanyModel company;
        private int? companyID;

        #endregion

        #region Properties

        [DataMember(Name = "title")]
        public string Title
        {
            get { return this.title; }
            set { base.Set<string>(ref this.title, value); }
        }

        [DataMember(Name = "firstName")]
        public string FirstName
        {
            get { return this.firstName; }
            set { base.Set<string>(ref this.firstName, value); }
        }

        [DataMember(Name = "lastName")]
        public string LastName
        {
            get { return this.lastName; }
            set { base.Set<string>(ref this.lastName, value); }
        }

        [DataMember(Name = "suffix")]
        public string Suffix
        {
            get { return this.suffix; }
            set { base.Set<string>(ref this.suffix, value); }
        }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { base.Set<DateTime>(ref this.birthDate, value); ; }
        }

        [DataMember(Name = "companyID")]
        public int? CompanyID
        {
            get { return this.companyID; }
            set { base.Set<int?>(ref this.companyID, value); }
        }

        [IgnoreDataMember]
        public CompanyModel Company
        {
            get { return this.company; }
            set { base.Set<CompanyModel>(ref this.company, value); }
        }

        #endregion

        #region Constructors

        public PersonModel(int id, string address, string billingAddress, string shippingAddress, string title, string firstName, string lastName, string suffix, DateTime birthDate, int? companyID = null) : base(id, address, billingAddress, shippingAddress)
        {
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.suffix = suffix;
            this.birthDate = birthDate;
            this.companyID = companyID;
        }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            var person = obj as PersonModel;

            return base.Equals(obj)
                && obj is PersonModel
                && person.title.Equals(this.title)
                && person.firstName.Equals(this.firstName)
                && person.lastName.Equals(this.lastName)
                && person.suffix.Equals(this.suffix)
                && person.birthDate.Equals(this.birthDate)
                && person.company == this.company;
        }

        public override int GetHashCode()
        {
            int hash = 31 * base.GetHashCode();
                hash = 31 * hash + this.title.GetHashCode();
                hash = 31 * hash + this.firstName.GetHashCode();
                hash = 31 * hash + this.lastName.GetHashCode();
                hash = 31 * hash + this.suffix.GetHashCode();
                hash = 31 * hash + this.birthDate.GetHashCode();
                hash = 31 * hash + (this.company != null ? this.company.GetHashCode() : 0);

            return hash;
        }

        #endregion
    }
}

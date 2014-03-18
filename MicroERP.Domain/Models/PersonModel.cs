using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroERP.Domain.Models
{
    [DataContract]
    public class PersonModel : CustomerModel
    {
        #region Properties

        private string title;
        private string firstName;
        private string lastName;
        private string suffix;
        private DateTime birthDate;
        private CompanyModel company;

        [DataMember]
        public string Title
        {
            get { return this.title; }
            set { base.Set<string>(ref this.title, value); }
        }

        [DataMember]
        public string FirstName
        {
            get { return this.firstName; }
            set { base.Set<string>(ref this.firstName, value); }
        }

        [DataMember]
        public string LastName
        {
            get { return this.lastName; }
            set { base.Set<string>(ref this.lastName, value); }
        }

        [DataMember]
        public string Suffix
        {
            get { return this.suffix; }
            set { base.Set<string>(ref this.suffix, value); }
        }

        [DataMember]
        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { base.Set<DateTime>(ref this.birthDate, value); ; }
        }

        [DataMember]
        public CompanyModel Company
        {
            get { return this.company; }
            set { base.Set<CompanyModel>(ref this.company, value); }
        }

        #endregion

        #region Constructors

        public PersonModel(int id, string address, string billingAddress, string shippingAddress, string title, string firstName, string lastName, string suffix, DateTime birthDate, CompanyModel company = null, IEnumerable<InvoiceModel> invoices = null) : base(id, address, billingAddress, shippingAddress, invoices)
        {
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.suffix = suffix;
            this.birthDate = birthDate;
            this.company = company;
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

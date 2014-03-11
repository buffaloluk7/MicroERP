using System;

namespace MicroERP.Business.Models
{
    class Person : Customer
    {
        #region Properties

        private string title;
        private string firstName;
        private string lastName;
        private string suffix;
        private DateTime birthDate;
        private Company company;

        public string Title
        {
            get { return this.title; }
            set { base.Set<string>(ref this.title, value); }
        }

        public string Firstname
        {
            get { return this.firstName; }
            set { base.Set<string>(ref this.firstName, value); }
        }

        public string LastName
        {
            get { return this.lastName; }
            set { base.Set<string>(ref this.lastName, value); }
        }

        public string Suffix
        {
            get { return this.suffix; }
            set { base.Set<string>(ref this.suffix, value); }
        }

        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { base.Set<DateTime>(ref this.birthDate, value); ; }
        }

        public Company Company
        {
            get { return this.company; }
            set { base.Set<Company>(ref this.company, value); }
        }

        #endregion

        #region Constructors

        public Person(string address, string billingAddress, string shippingAddress, string title, string firstName, string lastName, string suffix, DateTime birthDate, Company company = null) : base(address, billingAddress, shippingAddress)
        {
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.suffix = suffix;
            this.birthDate = birthDate;
            this.company = company;
        }

        #endregion
    }
}

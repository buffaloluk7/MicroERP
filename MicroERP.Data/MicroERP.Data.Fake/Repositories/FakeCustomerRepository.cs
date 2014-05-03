using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        #region Fields

        private readonly List<CustomerModel> customers;

        #endregion Properties

        #region Constructors

        public FakeCustomerRepository()
        {
            var c1 = new CompanyModel(3, "Viktorweg", "Viktorweg 1", "Viktorweg 2", "Viktor AG", "98765432");
            var c2 = new CompanyModel(4, "Simonweg", "Simonweg 1", "Simonweg 2", "Simon GmbH", "0123456789");
            var c3 = new CompanyModel(5, "Thomasweg", "Thomasweg 1", "Thomasweg 2", "Thomas GmbH", "6543217890");

            var p1 = new PersonModel(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr", "Thomas", "Eizinger", "Bsc", DateTime.Now, 3);
            var p2 = new PersonModel(2, "Lukasweg", "Lukasweg 1", "Lukasweg 2", "Dr", "Lukas", "Streiter", "Msc", DateTime.Now);
            var p3 = new PersonModel(6, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr.", "Viktor", "Hofinger", "Bsc", DateTime.Now, 4);
            var p4 = new PersonModel(7, "Anotherstreet", "Anotherstreet 1", "Anotherstreet 2", "Dr.", "Another", "Person", "Bsc", DateTime.Now, 4);
            var p5 = new PersonModel(8, "Copy ninja street", "Copy ninja street 1", "Copy ninja street 2", "DDr.", "Copy", "Ninja", "Msc", DateTime.Now, 3);

            p1.Company = c1;
            p3.Company = c2;
            p4.Company = c2;
            p5.Company = c1;

            this.customers = new List<CustomerModel>(new CustomerModel[]
            {
                c1, c2, c3,
                p1, p2, p3, p4, p5
            });
        }

        #endregion

        #region ICustomerRepository

        public async Task<int> Create(CustomerModel customer)
        {
            return await Task.Run(() => 
            {
                if (this.customers.Any(c => c.Equals(customer)))
                {
                    throw new CustomerAlreadyExistsException(customer);
                }

                customer.ID = this.customers.Max(i => i.ID) + 1;
                this.customers.Add(customer);
                return customer.ID.Value;
            });
        }

        public async Task<IEnumerable<CustomerModel>> Read(string searchQuery)
        {
            return await Task.Run(() =>
            {
                searchQuery = searchQuery.ToLower();

                var persons = this.customers.OfType<PersonModel>().Where(p => p.FirstName.ToLower().Contains(searchQuery) || p.LastName.ToLower().Contains(searchQuery));
                var companies = this.customers.OfType<CompanyModel>().Where(c => c != null && c.Name.ToLower().Contains(searchQuery));

                return persons.Concat<CustomerModel>(companies);
            });
        }

        public async Task<CustomerModel> Read(int customerID)
        {
            return await Task.Run(() =>
            {
                var customer = this.customers.FirstOrDefault(c => c.ID == customerID);

                if (customer != null)
                {
                    return customer;
                }

                throw new CustomerNotFoundException();
            });
        }

        public async Task<CustomerModel> Update(CustomerModel customer)
        {
            return await Task.Run(() =>
            {
                int index = this.customers.FindIndex(c => c.ID == customer.ID);

                if (index >= 0)
                {
                    return this.customers[index] = customer;
                }

                throw new CustomerNotFoundException();
            });
        }

        public Task Delete(int customerID)
        {
            return Task.Run(() =>
            {
                var customer = this.customers.FirstOrDefault(C => C.ID == customerID);

                if (customer != null)
                {
                    return this.customers.Remove(customer);
                }

                throw new CustomerNotFoundException();
            });
        }

        #endregion
    }
}

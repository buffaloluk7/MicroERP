using MicroERP.Business.DataAccessLayer.Exceptions;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Fake
{
    class FakeDataAccessLayer : IDataAccessLayer
    {
        private readonly List<Customer> customers;

        public FakeDataAccessLayer()
        {
            this.customers = new List<Customer>(new Customer[] {
                new Person(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr", "Thomas", "Eizinger", "Bsc", DateTime.Now),
                new Person(2, "Lukasweg", "Lukasweg 1", "Lukasweg 2", "Dr", "Lukas", "Streiter", "Msc", DateTime.Now),
                new Company(1, "Viktorweg", "Viktorweg 1", "Viktorweg 2", "Viktor AG", "98765432"),
                new Company(1, "Simonweg", "Simonweg 1", "Simonweg 2", "Simon GmbH", "0123456789"),
                new Person(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr.", "Thomas", "Eizinger", "Bsc", DateTime.Now)

            });
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            return await Task.Run(() => 
            {
                if (this.customers.Any(C => C.Equals(customer)))
                {
                    throw new CustomerAlreadyExistsException(customer);
                }

                this.customers.Add(customer);
                return customer;     
            });
        }

        public async Task<IEnumerable<Customer>> ReadCustomers(string searchQuery)
        {
            return await Task.Run(() =>
            {
                searchQuery = searchQuery.ToLower();

                var persons = this.customers.OfType<Person>().Where(P => P.FirstName.ToLower().Contains(searchQuery) || P.LastName.ToLower().Contains(searchQuery));
                var companies = this.customers.OfType<Company>().Where(C => C.Name.ToLower().Contains(searchQuery));

                return persons.Concat<Customer>(companies);
            });
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await Task.Run(() =>
            {
                int index = this.customers.FindIndex(C => C.Equals(customer));

                if (index < 0)
                {
                    throw new CustomerNotFoundException();
                }

                return this.customers[index] = customer;
            });
        }

        public Task DeleteCustomer(int customerID)
        {
            return Task.Run(() =>
            {
                var customer = this.customers.FirstOrDefault(C => C.ID == customerID);

                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                this.customers.Remove(customer);
            });
        }
    }
}

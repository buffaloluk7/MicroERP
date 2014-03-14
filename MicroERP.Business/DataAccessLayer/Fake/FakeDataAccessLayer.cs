using MicroERP.Business.DataAccessLayer.Exceptions;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Models;
using MicroERP.Business.DataAccessLayer.ESC.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace MicroERP.Business.DataAccessLayer.Fake
{
    class FakeDataAccessLayer : IDataAccessLayer
    {
        private readonly List<Customer> customers;

        public FakeDataAccessLayer()
        {
            var c1 = new Company(3, "Viktorweg", "Viktorweg 1", "Viktorweg 2", "Viktor AG", "98765432");
            var c2 = new Company(4, "Simonweg", "Simonweg 1", "Simonweg 2", "Simon GmbH", "0123456789");

            var p1 = new Person(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr", "Thomas", "Eizinger", "Bsc", DateTime.Now, c1);
            var p2 = new Person(2, "Lukasweg", "Lukasweg 1", "Lukasweg 2", "Dr", "Lukas", "Streiter", "Msc", DateTime.Now);
            var p3 = new Person(5, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr.", "Viktor", "Hofinger", "Bsc", DateTime.Now, c2);

            var ii1 = new ObservableCollection<InvoiceItem>(new InvoiceItem[]
            {
                new InvoiceItem(100, 10.2, 2.3),
                new InvoiceItem(120, 5.4, 2.7)
            });

            var ii2 = new ObservableCollection<InvoiceItem>(new InvoiceItem[]
            {
                new InvoiceItem(70, 14.2, 6.3),
                new InvoiceItem(132, 1.4, 8.7)
            });

            var i1 = new Invoice(DateTime.Now, DateTime.Now, 1, "Test-Kommentar", "Test-Message", p1, ii1);
            var i2 = new Invoice(DateTime.Now, DateTime.Now, 1, "Test-Kommentar", "Test-Message", p1, ii2);

            this.customers = new List<Customer>(new Customer[] {
                c1, c2, p1, p2, p3
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

        public async Task<IEnumerable<Customer>> ReadCustomers(string query = "")
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    throw new ArgumentException("PLEASE ENTER SOME SEARCH QUERY");
                }

                query = query.ToLower();

                var persons = this.customers.OfType<Person>().Where(P => P.FirstName.ToLower().ContainsAllButNotEmpty(query) || P.LastName.ToLower().ContainsAllButNotEmpty(query));
                var companies = this.customers.OfType<Company>().Where(C => C != null && C.Name.ToLower().ContainsAllButNotEmpty(query));

                return persons.Concat<Customer>(companies);
            });
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await Task.Run(() =>
            {
                int index = this.customers.FindIndex(C => C.ID == customer.ID);

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

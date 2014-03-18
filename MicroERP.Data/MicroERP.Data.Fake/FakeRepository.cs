using MicroERP.Domain.Exceptions;
using MicroERP.Domain.Interfaces;
using MicroERP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake
{
    public class FakeRepository : IRepository
    {
        #region Properties

        private readonly List<CustomerModel> customers;

        #endregion Properties

        #region Constructors

        public FakeRepository()
        {
            var c1 = new CompanyModel(3, "Viktorweg", "Viktorweg 1", "Viktorweg 2", "Viktor AG", "98765432");
            var c2 = new CompanyModel(4, "Simonweg", "Simonweg 1", "Simonweg 2", "Simon GmbH", "0123456789");

            var p1 = new PersonModel(1, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr", "Thomas", "Eizinger", "Bsc", DateTime.Now, c1);
            var p2 = new PersonModel(2, "Lukasweg", "Lukasweg 1", "Lukasweg 2", "Dr", "Lukas", "Streiter", "Msc", DateTime.Now);
            var p3 = new PersonModel(5, "Wehlistraße", "Wehlistraße 1", "Wehlistraße 2", "Dr.", "Viktor", "Hofinger", "Bsc", DateTime.Now, c2);

            var ii1 = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
            {
                new InvoiceItemModel(100, 10.2, 2.3),
                new InvoiceItemModel(120, 5.4, 2.7)
            });

            var ii2 = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
            {
                new InvoiceItemModel(70, 14.2, 6.3),
                new InvoiceItemModel(132, 1.4, 8.7)
            });

            var i1 = new InvoiceModel(DateTime.Now, DateTime.Now, 1, "Test-Kommentar", "Test-Message", p1, ii1);
            var i2 = new InvoiceModel(DateTime.Now, DateTime.Now, 1, "Test-Kommentar", "Test-Message", p1, ii2);

            this.customers = new List<CustomerModel>(new CustomerModel[] {
                c1, c2, p1, p2, p3
            });
        }

        #endregion

        #region Tasks

        public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
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

        public async Task<IEnumerable<CustomerModel>> ReadCustomers(string query)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    throw new ArgumentException("PLEASE ENTER SOME SEARCH QUERY");
                }

                query = query.ToLower();

                var persons = this.customers.OfType<PersonModel>().Where(P => P.FirstName.ToLower().Contains(query) || P.LastName.ToLower().Contains(query));
                var companies = this.customers.OfType<CompanyModel>().Where(C => C != null && C.Name.ToLower().Contains(query));

                return persons.Concat<CustomerModel>(companies);
            });
        }

        public async Task<CustomerModel> UpdateCustomer(CustomerModel customer)
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

        #endregion
    }
}

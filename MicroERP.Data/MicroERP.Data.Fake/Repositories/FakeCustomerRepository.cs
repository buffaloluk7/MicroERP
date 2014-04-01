using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        #region Properties

        private readonly List<CustomerModel> customers;

        #endregion Properties

        #region Constructors

        public FakeCustomerRepository()
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

        #region ICustomerRepository

        public async Task<CustomerModel> Create(CustomerModel customer)
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

        public async Task<IEnumerable<CustomerModel>> Read(string searchQuery)
        {
            return await Task.Run(() =>
            {
                searchQuery = searchQuery.ToLower();

                var persons = this.customers.OfType<PersonModel>().Where(P => P.FirstName.ToLower().Contains(searchQuery) || P.LastName.ToLower().Contains(searchQuery));
                var companies = this.customers.OfType<CompanyModel>().Where(C => C != null && C.Name.ToLower().Contains(searchQuery));

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
                int index = this.customers.FindIndex(C => C.ID == customer.ID);

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

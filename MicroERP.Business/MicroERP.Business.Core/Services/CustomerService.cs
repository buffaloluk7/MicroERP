using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;

namespace MicroERP.Business.Core.Services
{
    public class CustomerService : ICustomerService
    {
        #region Fields

        private readonly ICustomerRepository customerRepository;

        #endregion

        #region Constructors

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        #endregion

        #region ICustomerService

        public async Task<CustomerModel> Create(CustomerModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer", "Customer cannot be null");
            }

            customer.ID = await this.customerRepository.Create(customer);

            return customer;
        }

        public async Task<IEnumerable<CustomerModel>> Search(string searchQuery, bool ordered = true,
            CustomerType customerType = CustomerType.None)
        {
            if (searchQuery == null)
            {
                throw new ArgumentNullException("searchQuery");
            }

            if (searchQuery.Trim() == string.Empty)
            {
                throw new ArgumentOutOfRangeException("searchQuery");
            }

            var customers = await this.customerRepository.Search(searchQuery, customerType);

            if (ordered)
            {
                var persons = customers.OfType<PersonModel>().OrderBy(c => c.LastName + c.FirstName);
                var companies = customers.OfType<CompanyModel>().OrderBy(c => c.Name);
                customers = persons.Concat<CustomerModel>(companies);
            }

            return customers;
        }

        public async Task<CustomerModel> Find(int customerID)
        {
            if (customerID < 1)
            {
                throw new ArgumentOutOfRangeException("customerID", "customerID cannot be negative");
            }

            return await this.customerRepository.Find(customerID);
        }

        public async Task<T> Find<T>(int customerID) where T : CustomerModel
        {
            if (customerID < 1)
            {
                throw new ArgumentOutOfRangeException("customerID", "customerID cannot be negative");
            }

            return await this.customerRepository.Find(customerID) as T;
        }

        public Task<CustomerModel> Update(CustomerModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer", "customer cannot be null");
            }

            return this.customerRepository.Update(customer);
        }

        public async Task Delete(int customerID)
        {
            if (customerID < 1)
            {
                throw new ArgumentOutOfRangeException("customer", "customer cannot be negative");
            }

            await this.customerRepository.Delete(customerID);
        }

        #endregion
    }
}
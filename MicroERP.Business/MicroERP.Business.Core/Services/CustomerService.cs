using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Interfaces;
using MicroERP.Business.Domain.Models;
using System;

using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
        {
            var c = await this.customerRepository.CreateCustomer(customer);

            if (c == null)
            {
                throw new CustomerAlreadyExistsException(customer);
            }

            return c;
        }

        public Task<CustomerModel> GetCustomers(CustomerModel customer, bool ordered = false)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerModel> UpdateCustomer(CustomerModel customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}

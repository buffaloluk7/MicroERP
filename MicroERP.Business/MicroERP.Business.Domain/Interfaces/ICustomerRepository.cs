using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> CreateCustomer(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> ReadCustomers(string query);

        Task<CustomerModel> UpdateCustomer(CustomerModel customer);

        Task DeleteCustomer(int customerID);
    }
}

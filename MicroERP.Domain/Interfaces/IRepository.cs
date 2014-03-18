using MicroERP.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Domain.Interfaces
{
    public interface IRepository
    {
        Task<CustomerModel> CreateCustomer(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> ReadCustomers(string query);

        Task<CustomerModel> UpdateCustomer(CustomerModel customer);

        Task DeleteCustomer(int customerID);
    }
}

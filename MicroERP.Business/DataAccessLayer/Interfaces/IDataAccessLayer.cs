using MicroERP.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Interfaces
{
    public interface IDataAccessLayer
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<IEnumerable<Customer>> ReadCustomers(string searchQuery);

        Task<Customer> UpdateCustomer(Customer customer);

        Task DeleteCustomer(int customerID);
    }
}

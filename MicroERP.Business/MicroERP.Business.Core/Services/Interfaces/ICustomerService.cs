using MicroERP.Business.Domain.Models;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> CreateCustomer(CustomerModel customer);

        Task<CustomerModel> GetCustomers(CustomerModel customer, bool ordered = false);

        Task<CustomerModel> UpdateCustomer(CustomerModel customer);

        Task DeleteCustomer(CustomerModel customer);
    }
}

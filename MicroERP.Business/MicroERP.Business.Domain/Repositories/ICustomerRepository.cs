using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> Create(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> Read(string searchQuery);

        Task<CustomerModel> Read(int customerID);

        Task<CustomerModel> Update(CustomerModel customer);

        Task Delete(int customerID);
    }
}

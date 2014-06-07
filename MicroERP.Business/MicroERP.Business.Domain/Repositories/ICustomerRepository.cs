using System.Collections.Generic;
using System.Threading.Tasks;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> Create(CustomerModel customer);

        Task<CustomerModel> Find(int customerID);

        Task<CustomerModel> Update(CustomerModel customer);

        Task Delete(int customerID);

        Task<IEnumerable<CustomerModel>> Search(string searchQuery, CustomerType customerType = CustomerType.None);
    }
}
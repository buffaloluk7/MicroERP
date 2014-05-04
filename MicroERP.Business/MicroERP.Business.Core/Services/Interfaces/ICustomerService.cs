using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<int> Create(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> Search(string searchQuery, bool ordered = true, bool companiesOnly = false);

        Task<CustomerModel> Read(int customerID);

        Task<T> Read<T>(int customerID) where T : CustomerModel;

        Task<CustomerModel> Update(CustomerModel customer);

        Task Delete(int customerID);
    }
}

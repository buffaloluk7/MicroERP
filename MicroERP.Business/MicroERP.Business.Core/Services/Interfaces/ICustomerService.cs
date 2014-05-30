using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> Create(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> Search(string searchQuery, bool ordered = true, CustomerType customerType = CustomerType.None);

        Task<CustomerModel> Find(int customerID);

        Task<T> Find<T>(int customerID) where T : CustomerModel;

        Task<CustomerModel> Update(CustomerModel customer);

        Task Delete(int customerID);
    }
}

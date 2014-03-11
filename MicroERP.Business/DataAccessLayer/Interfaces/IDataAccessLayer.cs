using MicroERP.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Interfaces
{
    public interface IDataAccessLayer
    {
        Task<IEnumerable<Customer>> SearchCustomers(string searchQuery);

        Task SaveCustomer(Customer customer);
    }
}

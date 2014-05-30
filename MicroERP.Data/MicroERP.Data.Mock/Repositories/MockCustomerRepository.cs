using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Mock.Repositories
{
    public class MockCustomerRepository : ICustomerRepository
    {
        #region ICustomerRepository

        public async Task<int> Create(CustomerModel customer)
        {
            return await Task.Run(() =>
            {
                customer.ID = MockData.Instance.Customers.Max(i => i.ID) + 1;
                MockData.Instance.Customers.Add(customer);

                return customer.ID;
            });
        }

        public async Task<IEnumerable<CustomerModel>> Search(string searchQuery, CustomerType customerType = CustomerType.None)
        {
            return await Task.Run(() =>
            {
                searchQuery = searchQuery.ToLower();

                switch (customerType)
                {
                    case CustomerType.None:
                        var persons = MockData.Instance.Customers.OfType<PersonModel>().Where(p => p.FirstName.ToLower().Contains(searchQuery) || p.LastName.ToLower().Contains(searchQuery));
                        var companies = MockData.Instance.Customers.OfType<CompanyModel>().Where(c => c != null && c.Name.ToLower().Contains(searchQuery));

                        return persons.Concat<CustomerModel>(companies);

                    case CustomerType.Company:
                        return MockData.Instance.Customers.OfType<CompanyModel>().Where(c => c != null && c.Name.ToLower().Contains(searchQuery));

                    case CustomerType.Person:
                        return MockData.Instance.Customers.OfType<PersonModel>().Where(p => p.FirstName.ToLower().Contains(searchQuery) || p.LastName.ToLower().Contains(searchQuery));

                    default:
                        throw new ArgumentOutOfRangeException("customerType", "Unsupported filter");
                }
            });
        }

        public async Task<CustomerModel> Find(int customerID)
        {
            return await Task.Run(() =>
            {
                var customer = MockData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                return customer;
            });
        }

        public async Task<CustomerModel> Update(CustomerModel customer)
        {
            return await Task.Run(() =>
            {
                int index = MockData.Instance.Customers.FindIndex(c => c.ID == customer.ID);
                if (index >= 0)
                {
                    return MockData.Instance.Customers[index] = customer;
                }

                throw new CustomerNotFoundException();
            });
        }

        public async Task Delete(int customerID)
        {
            await Task.Run(() =>
            {
                var customer = MockData.Instance.Customers.FirstOrDefault(C => C.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                if (customer is CompanyModel)
                {
                    var employees = MockData.Instance.Customers.OfType<PersonModel>().Where(p => p.Company != null && p.Company.ID == customerID);
                    foreach (var employee in employees)
                    {
                        employee.Company = null;
                    }
                }

                MockData.Instance.Customers.Remove(customer);
            });
        }

        #endregion
    }
}

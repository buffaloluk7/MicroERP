﻿using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        #region ICustomerRepository

        public async Task<int> Create(CustomerModel customer)
        {
            return await Task.Run(() =>
            {
                customer.ID = FakeData.Instance.Customers.Max(i => i.ID) + 1;
                FakeData.Instance.Customers.Add(customer);

                return customer.ID.Value;
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
                        var persons = FakeData.Instance.Customers.OfType<PersonModel>().Where(p => p.FirstName.ToLower().Contains(searchQuery) || p.LastName.ToLower().Contains(searchQuery));
                        var companies = FakeData.Instance.Customers.OfType<CompanyModel>().Where(c => c != null && c.Name.ToLower().Contains(searchQuery));

                        return persons.Concat<CustomerModel>(companies);

                    case CustomerType.Company:
                        return FakeData.Instance.Customers.OfType<CompanyModel>().Where(c => c != null && c.Name.ToLower().Contains(searchQuery));

                    case CustomerType.Person:
                        return FakeData.Instance.Customers.OfType<PersonModel>().Where(p => p.FirstName.ToLower().Contains(searchQuery) || p.LastName.ToLower().Contains(searchQuery));

                    default:
                        throw new ArgumentOutOfRangeException("customerType", "Unsupported filter");
                }
            });
        }

        public async Task<CustomerModel> Find(int customerID)
        {
            return await Task.Run(() =>
            {
                var customer = FakeData.Instance.Customers.FirstOrDefault(c => c.ID == customerID);

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
                int index = FakeData.Instance.Customers.FindIndex(c => c.ID == customer.ID);

                if (index >= 0)
                {
                    return FakeData.Instance.Customers[index] = customer;
                }

                throw new CustomerNotFoundException();
            });
        }

        public async Task Delete(int customerID)
        {
            await Task.Run(() =>
            {
                var customer = FakeData.Instance.Customers.FirstOrDefault(C => C.ID == customerID);
                if (customer == null)
                {
                    throw new CustomerNotFoundException();
                }

                if (customer is CompanyModel)
                {
                    var employees = FakeData.Instance.Customers.OfType<PersonModel>().Where(p => p.CompanyID == customerID);

                    foreach (var employee in employees)
                    {
                        employee.Company = null;
                    }
                }

                FakeData.Instance.Customers.Remove(customer);
            });
        }

        #endregion
    }
}

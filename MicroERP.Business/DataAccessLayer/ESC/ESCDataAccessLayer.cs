using System;
using System.Threading.Tasks;
using MicroERP.Business.DataAccessLayer.Interfaces;
using System.Collections.Generic;
using MicroERP.Business.Models;

namespace MicroERP.Business.DataAccessLayer.ESC
{
    public class ESCDataAccessLayer : IDataAccessLayer
    {
        private const string baseURL = "http://localhost:8000/api/";

        public async Task<IEnumerable<Customer>> SearchCustomers(string searchQuery)
        {
            return await ESCRequest.Get<IEnumerable<Customer>>(baseURL + "customers");
        }

        public async Task SaveCustomer(Customer customer)
        {
            return await ESCRequest.Post(baseURL + "customers", customer);
        }
    }
}

﻿using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Interfaces;
using MicroERP.Business.Domain.Models;
using MicroERP.Data.Api.Exceptions;
using MicroERP.Data.Api.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MicroERP.Data.Api
{
    public class ApiCustomerRepository : ICustomerRepository
    {
        private const string baseURL = "http://localhost:8000/api/customers/";

        public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
        {
            var response = await RESTRequest.Post(baseURL, customer);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<CustomerModel>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new CustomerAlreadyExistsException(customer);
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<IEnumerable<CustomerModel>> ReadCustomers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("PLEASE ENTER SOME SEARCH QUERY");
            }

            string url = string.Format("{0}?q={1}", baseURL, query);

            var response = await RESTRequest.Get(url);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<IEnumerable<CustomerModel>>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<CustomerModel> UpdateCustomer(CustomerModel customer)
        {
            var response = await RESTRequest.Put(baseURL + customer.ID);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return customer;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<CustomerModel>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomerNotFoundException();
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task DeleteCustomer(int customerID)
        {
            var response = await RESTRequest.Delete(baseURL + customerID);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomerNotFoundException();
            }
            else if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new BadResponseException(response.StatusCode);
            }
        }
    }
}

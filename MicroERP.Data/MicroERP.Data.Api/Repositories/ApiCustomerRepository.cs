using Luvi.Http.Extension;
using Luvi.Json.Converter;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiCustomerRepository : ApiRepositoryBase, ICustomerRepository
    {
        #region Fields

        private string url;

        #endregion

        #region Constructors

        public ApiCustomerRepository(IApiConfiguration configuration) : base(configuration)
        {
            this.url = this.ConnectionString + "customer";
        }

        #endregion

        #region ICustomerRepository

        public async Task<int> Create(CustomerModel customer)
        {
            var response = await this.request.Post(this.url, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<int>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.Conflict:
                    throw new CustomerAlreadyExistsException(customer);

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<IEnumerable<CustomerModel>> Read(string searchQuery, CustomerType customerType = CustomerType.None)
        {
            if (customerType == CustomerType.Company)
            {
                searchQuery += "&type=company";
            }

            string url = string.Format("{0}?q={1}", this.url, searchQuery);
            var response = await this.request.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, Type> knownTypes = new Dictionary<string, Type>();
                knownTypes.Add("Person", typeof(PersonModel));
                knownTypes.Add("Company", typeof(CompanyModel));

                var jsonKnownTypeConverter = new JsonKnownTypeConverter<CustomerModel>(knownTypes);
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                jsonSerializerSettings.Converters.Add(jsonKnownTypeConverter);

                return await response.Content.ReadAsObjectAsync<IEnumerable<CustomerModel>>(jsonSerializerSettings);
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<CustomerModel> Read(int customerID)
        {
            string url = string.Format("{0}/{1}", this.url, customerID);
            var response = await this.request.Get(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<CustomerModel> Update(CustomerModel customer)
        {
            var response = await this.request.Put(this.url + customer.ID, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return customer;

                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }            
        }

        public async Task Delete(int customerID)
        {
            var response = await this.request.Delete(this.url + customerID);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        #endregion
    }
}

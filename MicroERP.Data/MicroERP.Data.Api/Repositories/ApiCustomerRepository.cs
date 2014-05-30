using Luvi.Http.Extension;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiCustomerRepository : ApiRepositoryBase, ICustomerRepository
    {
        #region Constructors

        public ApiCustomerRepository(IApiConfiguration configuration) : base(configuration, "customers") { }

        #endregion

        #region ICustomerRepository

        public async Task<int> Create(CustomerModel customer)
        {
            var response = await base.request.Post(base.ConnectionString, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    try
                    {
                        var jsonObject = await response.Content.ReadAsStringAsync();
                        var anonObject = new { id = default(int) };

                        return JsonConvert.DeserializeAnonymousType(jsonObject, anonObject).id;
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

        public async Task<IEnumerable<CustomerModel>> Find(string searchQuery, CustomerType customerType = CustomerType.None)
        {
            if (customerType == CustomerType.Company)
            {
                searchQuery += "&type=company";
            }

            string url = string.Format("{0}?q={1}", base.ConnectionString, searchQuery);
            var response = await base.request.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsObjectAsync<IEnumerable<CustomerModel>>(base.jsonSettings);
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<CustomerModel> Find(int customerID)
        {
            string url = string.Format("{0}/{1}", base.ConnectionString, customerID);
            var response = await base.request.Get(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>(base.jsonSettings);
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
            string url = string.Format("{0}/{1}", base.ConnectionString, customer.ID);
            var response = await base.request.Put(url, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return customer;

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }            
        }

        public async Task Delete(int customerID)
        {
            string url = string.Format("{0}/{1}", base.ConnectionString, customerID);
            var response = await base.request.Delete(url);

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

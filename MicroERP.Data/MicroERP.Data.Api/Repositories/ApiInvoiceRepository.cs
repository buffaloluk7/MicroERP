using Luvi.Http.Extension;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiInvoiceRepository : ApiRepositoryBase, IInvoiceRepository
    {
        #region Constructors

        public ApiInvoiceRepository(IApiConfiguration configuration) : base(configuration)
        {
            this.jsonSettings.TypeNameHandling = TypeNameHandling.None;
        }

        #endregion

        #region IInvoiceRepository

        public async Task<int> Create(int customerID, InvoiceModel invoice)
        {
            string url = string.Format("{0}/customers/{1}/invoices", this.ConnectionString, customerID);
            var response = await this.request.Post(url, invoice);

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

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<IEnumerable<InvoiceModel>> All(int customerID)
        {
            string url = string.Format("{0}/invoices?customerID=", this.ConnectionString, customerID);
            var response = await this.request.Get(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<IEnumerable<InvoiceModel>>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new InvoiceNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<InvoiceModel> Find(int invoiceID)
        {
            string url = string.Format("{0}/invoices/{1}", this.ConnectionString, invoiceID);
            var response = await this.request.Get(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<InvoiceModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new InvoiceNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<IEnumerable<InvoiceModel>> Search(int? customerID = null, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            var url = new StringBuilder(this.ConnectionString).Append("/invoices?");

            if (customerID.HasValue)
            {
                url.AppendFormat("customerID={0}&", customerID.Value);
            }
            if (begin.HasValue)
            {
                var timestamp = (begin.Value - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()).TotalSeconds;
                url.AppendFormat("startDate={0}&", timestamp);
            }
            if (end.HasValue)
            {
                var timestamp = (end.Value - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()).TotalSeconds;
                url.AppendFormat("endDate={0}&", timestamp);
            }
            if (minPrice.HasValue)
            {
                url.AppendFormat("minPrice={0}&", minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                url.AppendFormat("maxPrice={0}", maxPrice.Value);
            }

            var response = await this.request.Get(url.ToString());

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<IEnumerable<InvoiceModel>>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        #endregion
    }
}

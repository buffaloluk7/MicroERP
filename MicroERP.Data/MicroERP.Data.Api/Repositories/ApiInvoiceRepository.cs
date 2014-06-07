using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Luvi.Http.Extension;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiInvoiceRepository : ApiRepositoryBase, IInvoiceRepository
    {
        #region Constructors

        public ApiInvoiceRepository(IApiConfiguration configuration) : base(configuration)
        {
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
                        var anonObject = new {id = default(int)};

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

        public async Task Export(int invoiceID, string path)
        {
            using (var httpClient = new HttpClient())
            {
                //string url = string.Format("{0}/invoices/{1}/export", this.ConnectionString, invoiceID);
                string url = "http://lukas.cc/file.pdf";
                Stream response = await httpClient.GetStreamAsync(url);
            }
        }

        public async Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs)
        {
            var url = new StringBuilder(this.ConnectionString).Append("invoices?");

            if (invoiceSearchArgs.CustomerID.HasValue)
            {
                url.AppendFormat("customerID={0}&", invoiceSearchArgs.CustomerID.Value);
            }
            if (invoiceSearchArgs.MinDate.HasValue)
            {
                var timestamp =
                    (invoiceSearchArgs.MinDate.Value -
                     new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()).TotalSeconds;
                url.AppendFormat("startDate={0}&", timestamp);
            }
            if (invoiceSearchArgs.MaxDate.HasValue)
            {
                var timestamp =
                    (invoiceSearchArgs.MaxDate.Value -
                     new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime()).TotalSeconds;
                url.AppendFormat("endDate={0}&", timestamp);
            }
            if (invoiceSearchArgs.MinTotal.HasValue)
            {
                url.AppendFormat("minTotal={0}&", invoiceSearchArgs.MinTotal.Value);
            }
            if (invoiceSearchArgs.MaxTotal.HasValue)
            {
                url.AppendFormat("maxTotal={0}", invoiceSearchArgs.MaxTotal.Value);
            }

            var response = await this.request.Get(url.ToString());

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<IEnumerable<InvoiceModel>>(this.jsonSettings);
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
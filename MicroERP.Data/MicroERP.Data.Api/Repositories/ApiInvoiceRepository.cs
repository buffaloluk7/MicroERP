using Luvi.Http.Extension;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Configuration.Interfaces;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiInvoiceRepository : ApiRepositoryBase, IInvoiceRepository
    {
        #region IInvoiceRepository

        public async Task<InvoiceModel> Create(int customerID, InvoiceModel invoice)
        {
            string url = string.Format("{0}/customers/{1}/invoices", this.ConnectionString, customerID);

            var response = await this.request.Post(url, invoice);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<InvoiceModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.Conflict:
                    throw new InvoiceAlreadyExistsException(invoice);

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public Task<System.Collections.Generic.IEnumerable<InvoiceModel>> Search(int customerID, System.DateTime? begin = null, System.DateTime? end = null, double? minPrice = null, double? maxPrice = null)
        {
            string url = string.Format("{0}/customers/{1}/invoices?", this.ConnectionString, customerID);

            throw new System.NotImplementedException();
        }

        public async Task<InvoiceModel> Read(int customerID, int invoiceID)
        {
            string url = string.Format("{0}/customers/{1}/invoices/{2}", this.ConnectionString, customerID, invoiceID);
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

        #endregion

        #region Constructors

        public ApiInvoiceRepository(IApiConfiguration configuration) : base(configuration) { }

        #endregion
    }
}

using Luvi.Http.Extension;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Exceptions;
using MicroERP.Data.Api.Properties;
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
            string url = string.Format("{0}/customers/{1}/invoices", Resources.apiURL, customerID);

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

        public async Task<InvoiceModel> Read(int customerID, int invoiceID)
        {
            string url = string.Format("{0}/customers/{1}/invoices/{2}", Resources.apiURL, customerID, invoiceID);
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
    }
}

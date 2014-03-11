using MicroERP.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MicroERP.Business.DataAccessLayer.ESC.Extensions;
using MicroERP.Business.DataAccessLayer.ESC.Exceptions;
using System.Net;

namespace MicroERP.Business.DataAccessLayer.ESC
{
    public static class ESCRequest
    {
        public static TimeSpan Timeout = new TimeSpan(0, 0, 15);

        public static async Task<T> Get<T>(string url)
        {
            HttpResponseMessage response = await ESCRequest.Call<T>(HttpMethod.Get, url);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<T>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }

            throw new BadResponseException(response.StatusCode);
        }

        public static async Task Post(string url, object arguments)
        {
            var response = await ESCRequest.Call<T>(HttpMethod.Post, url, arguments);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new BadResponseException(response.StatusCode);
            }
        }

        private static async Task<HttpResponseMessage> Call<T>(HttpMethod method, string url, object arguments = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.Timeout = ESCRequest.Timeout;

            HttpRequestMessage request = new HttpRequestMessage(method, url);

            if (handler.SupportsTransferEncodingChunked())
            {
                request.Headers.TransferEncodingChunked = true;
            }

            if (arguments != null)
            {
                var json = JsonConvert.SerializeObject(arguments, arguments.GetType(), Formatting.Indented, new JsonSerializerSettings());
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            
            try
            {
                return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            catch (ServerNotAvailableException e)
            {
                throw new ServerNotAvailableException(inner: e);
            }
        }
    }
}
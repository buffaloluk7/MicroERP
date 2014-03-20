using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MicroERP.Data.Api
{
    public static class RESTRequest
    {
        public static TimeSpan Timeout = new TimeSpan(0, 0, 15);

        public static async Task<HttpResponseMessage> Get(string url)
        {
            return await RESTRequest.Call(HttpMethod.Get, url);
        }

        public static async Task<HttpResponseMessage> Post(string url, object arguments)
        {
            return await RESTRequest.Call(HttpMethod.Post, url, arguments);
        }

        public static async Task<HttpResponseMessage> Put(string url)
        {
            return await RESTRequest.Call(HttpMethod.Put, url);
        }

        public static async Task<HttpResponseMessage> Delete(string url)
        {
            return await RESTRequest.Call(HttpMethod.Delete, url);
        }

        private static async Task<HttpResponseMessage> Call(HttpMethod method, string url, object arguments = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.Timeout = RESTRequest.Timeout;

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
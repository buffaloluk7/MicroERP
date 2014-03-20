using System;
using System.Net;

namespace MicroERP.Data.Api.Exceptions
{
    public class BadResponseException : EmbeddedSensorCloudException
    {
        public HttpStatusCode StatusCode
        {
            get;
            private set;
        }

        public BadResponseException(HttpStatusCode statuscode, string message = "Unable to execute your request at the moment.", Exception inner = null) : base(message, inner)
        {
            this.StatusCode = statuscode;
        }
    }
}
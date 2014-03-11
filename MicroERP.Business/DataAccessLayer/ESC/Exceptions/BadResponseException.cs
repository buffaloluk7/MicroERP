using System;
using System.Net;

namespace MicroERP.Business.DataAccessLayer.ESC.Exceptions
{
    public class BadResponseException : ESCBaseException
    {
        HttpStatusCode StatusCode
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
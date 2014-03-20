using MicroERP.Domain.Exceptions;
using System;

namespace MicroERP.Data.Api.Exceptions
{
    public abstract class EmbeddedSensorCloudException : CustomerException
    {
        public EmbeddedSensorCloudException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}

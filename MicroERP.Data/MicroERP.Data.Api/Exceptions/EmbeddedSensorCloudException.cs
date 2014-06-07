using System;
using MicroERP.Business.Domain.Exceptions;

namespace MicroERP.Data.Api.Exceptions
{
    public abstract class EmbeddedSensorCloudException : CustomerException
    {
        protected EmbeddedSensorCloudException(string message = null, Exception inner = null) : base(message, inner)
        {
        }
    }
}
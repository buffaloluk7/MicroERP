using System;

namespace MicroERP.Data.Api.Exceptions
{
    public class ServerNotAvailableException : EmbeddedSensorCloudException
    {
        public ServerNotAvailableException(string message = "Server not available.", Exception inner = null) : base(message, inner) { }
    }
}

using System;

namespace MicroERP.Data.EmbeddedSensorCloud.Exceptions
{
    public class ServerNotAvailableException : EmbeddedSensorCloudException
    {
        public ServerNotAvailableException(string message = "Server not available.", Exception inner = null) : base(message, inner) { }
    }
}

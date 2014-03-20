using System;

namespace MicroERP.Data.Api.Exceptions
{
    public class FaultyMessageException : EmbeddedSensorCloudException
    {
        public FaultyMessageException(string message = "Message could not be parsed.", Exception inner = null) : base(message, inner) { }
    }
}

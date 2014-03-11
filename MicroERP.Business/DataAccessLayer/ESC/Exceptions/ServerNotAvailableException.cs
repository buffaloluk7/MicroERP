using System;

namespace MicroERP.Business.DataAccessLayer.ESC.Exceptions
{
    public class ServerNotAvailableException : ESCBaseException
    {
        public ServerNotAvailableException(string message = "Server not available.", Exception inner = null) : base(message, inner) { }
    }
}

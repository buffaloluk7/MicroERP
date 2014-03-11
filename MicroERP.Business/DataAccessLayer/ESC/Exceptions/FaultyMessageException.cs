using System;

namespace MicroERP.Business.DataAccessLayer.ESC.Exceptions
{
    public class FaultyMessageException : ESCBaseException
    {
        public FaultyMessageException(string message = "Message could not be parsed.", Exception inner = null) : base(message, inner) { }
    }
}

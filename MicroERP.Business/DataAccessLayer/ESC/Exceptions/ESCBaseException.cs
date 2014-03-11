using System;

namespace MicroERP.Business.DataAccessLayer.ESC.Exceptions
{
    public class ESCBaseException : Exception
    {
        public ESCBaseException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}

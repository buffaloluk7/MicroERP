using MicroERP.Business.DataAccessLayer.Exceptions;
using System;

namespace MicroERP.Business.DataAccessLayer.ESC.Exceptions
{
    public class ESCBaseException : DataAccessLayerBaseException
    {
        public ESCBaseException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}
